#include "angle_calc.h"
#include <iostream>
#include <mosquittopp.h>
#include <vector>
#include <queue>
#include <unordered_map>
#include <string>
#include <cstring>
#include <thread>
#include <chrono>
#include <functional>
#include <mutex>
#include <eigen3/Eigen/Eigen>

#define PUBLISH_AS_STRING               1

#define AOA_TOPIC_IQ_REPORT_SCAN        "silabs/aoa/iq_report/+/+"
#define ANGLE_TOPIC_FORMAT              "estimator/angle/%s"

#define ANGLE_PUBLISH_INTERVAL_MS       500

#define MAX_TOPIC_ANGLE_SIZE            64
#define MAX_PAYLOAD_SIZE                16

mosquitto *mqtt_handle;
std::unordered_map<std::string, std::vector<double>> angles_buffer;
std::unordered_map<std::string, std::queue<std::vector<double>>> samples_buffer;
std::mutex mtx;

void mqtt_publish_angle(std::string tag_id, double angle) {
    char topic_angle[MAX_TOPIC_ANGLE_SIZE] = {0};

    snprintf(topic_angle, MAX_TOPIC_ANGLE_SIZE, ANGLE_TOPIC_FORMAT, tag_id.c_str());

    char payload[MAX_PAYLOAD_SIZE] = {'\0'};
    snprintf(payload, MAX_PAYLOAD_SIZE, "%.2f", angle);

    if(mosquitto_publish(mqtt_handle, NULL, topic_angle, PUBLISH_AS_STRING ? strlen(payload) : sizeof(angle), PUBLISH_AS_STRING ? (void*)payload : (void*)&angle, 1, false) != MOSQ_ERR_SUCCESS) {
        std::cerr << "Error publishing to topic " << topic_angle << std::endl;
    }
}

void mqtt_on_message(struct mosquitto *mosq, void *obj, const struct mosquitto_message *message) {
    if(strstr(message->topic, "silabs/aoa/iq_report/") != NULL) {
        std::vector<int> iq_samples;

        char *tok_saveptr = NULL;
        char *tok = strtok_r(strstr((char*)message->payload, "[") + 1, ", ", &tok_saveptr);
        while(tok != NULL) {
            iq_samples.push_back(atoi(tok));
            tok = strtok_r(NULL, ", ", &tok_saveptr);
        }

        char *tag_id = message->topic;
        char *tmp = strstr(tag_id, "/");

        //find last argument of topic, aka tag_id
        while(tmp != NULL) {
            tag_id = tmp+1;
            tmp = strstr(tag_id, "/");
        }

        double ref_phase_shift = calculate_ref_phase_shift(&iq_samples[0], REFERENCE_PERIOD*2);
        iq_samples.erase(iq_samples.begin(), iq_samples.begin()+(REFERENCE_PERIOD-1)*2); //discard reference samples

        std::vector<double> normalized_samples(iq_samples.size());

        transform_data(&iq_samples[0], iq_samples.size(), ref_phase_shift, &normalized_samples[0]);

        mtx.lock();
        samples_buffer[std::string(tag_id)].push(std::vector<double>(normalized_samples));
        mtx.unlock();
    }
}

void timer_start(std::function<void(void)> func, unsigned int interval)
{
    std::thread([func, interval]()
    { 
        while (true)
        {
            func();
            std::this_thread::sleep_for(std::chrono::milliseconds(interval));
        }
    }).detach();
}

void periodic_angle_mean(std::unordered_map<std::string, std::vector<double>> *angles_buffer) {
    for(auto el=angles_buffer->begin(); el != angles_buffer->end(); ++el) {
        if(el->second.size() == 0) continue;

        double max=-180, min = 180;

        double acc = 0;
        for(auto angle : el->second) {
            max = std::max(max, angle);
            min = std::min(min, angle);
            acc += angle;
        }

        int divider = el->second.size();

        if(el->second.size() > 2) {
            acc -= max + min;
            divider -= 2;
        }

        mqtt_publish_angle(el->first, acc / divider);
        el->second.clear();
    }
}

int main(int argc, char **argv) {
    Eigen::initParallel();

    if(mosquitto_lib_init() != MOSQ_ERR_SUCCESS) {
        std::cerr << "Error initializing mosquitto library" << std::endl;
        return 1;
    }

    mqtt_handle = mosquitto_new("estimator", true, NULL);
    if(mqtt_handle == NULL) {
        std::cerr << "Error creating mosquitto client" << std::endl;
        return 1;
    }

    mosquitto_message_callback_set(mqtt_handle, mqtt_on_message);

    if(mosquitto_connect(mqtt_handle, "localhost", 1883, 30) != MOSQ_ERR_SUCCESS) {
        std::cerr << "Error connecting to mosquitto server" << std::endl;
        return 1;
    }

    if(mosquitto_subscribe(mqtt_handle, NULL, AOA_TOPIC_IQ_REPORT_SCAN, 1) != MOSQ_ERR_SUCCESS) {
        std::cerr << "Error subscribing iq_report topic" << std::endl;
        return 1;
    }

    timer_start(std::bind(periodic_angle_mean, &angles_buffer), ANGLE_PUBLISH_INTERVAL_MS);

    if(mosquitto_loop_start(mqtt_handle) != MOSQ_ERR_SUCCESS) {
        std::cerr << "Error starting looping" << std::endl;
        return 1;
    }

    while(1) {

        mtx.lock();
        for(auto el = samples_buffer.begin(); el != samples_buffer.end(); ++el) {
            //std::cout << el->second.size() << std::endl;
            if(el->second.size() > 100) std::queue<std::vector<double>>().swap(el->second); //clears the queue
            while(el->second.size() != 0) {
                std::vector<double> &samples = el->second.front();
                auto start = std::chrono::system_clock::now();
                double angle = calc_angle(samples.data(), samples.size(), 1, 4);
                auto end = std::chrono::system_clock::now();
                std::cout << std::chrono::duration<double>(end-start).count() << std::endl;
                el->second.front().clear();
                el->second.pop();
                angles_buffer[std::string(el->first)].push_back(angle);
            }
        }
        mtx.unlock();
        std::this_thread::sleep_for(std::chrono::milliseconds(ANGLE_PUBLISH_INTERVAL_MS));
    }

    return 1;
}
