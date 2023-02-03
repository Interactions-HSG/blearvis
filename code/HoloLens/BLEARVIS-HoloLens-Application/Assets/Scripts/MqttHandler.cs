/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using System.Globalization;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt).
/// 
/// Adpapted by Jannis Strecker. 
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class MqttHandler : M2MqttUnityClient
    {
        
        [Header("Custom for BLEARVIS")]
        public string AoATopic;
        public string SensorDataTopic;

        //public ThunderboardHandler thunderboardHandler;

        public GameObject thunderboardInfoBoxPrefab;
        public ThunderboardHandlerList thunderboardHandlerListScript;
        

        private List<string> eventMessages = new List<string>();
      

        protected override void OnConnecting()
        {
            base.OnConnecting();
            Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("Connected to broker on " + brokerAddress + "\n");
            /*
            if (autoTest)
            {
                TestPublish();
            }
            */
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { AoATopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { SensorDataTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Debug.Log("Subscribed to topics.");
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { AoATopic, SensorDataTopic });
            Debug.Log("Unubscribed to topics.");
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }

        

        protected override void Start()
        {
            Debug.Log("Ready.");
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            //Debug.Log("received new message");
            string msg = System.Text.Encoding.UTF8.GetString(message);

            if (msg == null || msg == "") return;

            //Debug.Log($"topic: {topic}");

            if (topic.StartsWith("estimator/angle"))
            {
                // thunderboardHandlerListScript.HandleIncomingAoAFromMQTT(topic, msg);
                thunderboardHandlerListScript.HandleIncomingAoAFromMQTTStaticDevice(topic, msg);

            } else if (topic.StartsWith("sensor"))
            {
                thunderboardHandlerListScript.HandleIncomingSensorDataFromMQTT(topic, msg);
            }
            StoreMessage(msg);
        }


        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            //Debug.Log("Received: " + msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
            
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        
    }
}
