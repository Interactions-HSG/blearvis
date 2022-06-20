using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using SimpleJSON;
using System.Globalization;
using System;
using TMPro;

// this script should start the ObjectRecognition process running on another computer
public class TemperatureController : MonoBehaviour
{

    // control the temerature in the office very 15 minutes
    // if temperature too hot and outside is cooler and air quality outside is fine, then recommend to topen the window
    // if temperature too cold and outside air is more hot and air quality outside is fine, then recommend to open the window
    // if outside air quality is not good enough, then recommend to adjust mechanical ventilation
    private bool endpointSet;
    private bool firstExecution;
    private bool tempTooHigh;
    private bool tempTooLow;
    private double currentTemp;


    private string apiEndpoint;
    private string apiMethod;
    private double frequencyOfCheckInSeconds;
    private DateTime timeLastExecution;
    private bool measureDone;
    
    public GameObject tempWarning;
    public SceneController sceneController;
    public OntologyReader ontologyReader;
    public OutsideAirQualityChecker outsideAirQualityChecker;

    void Start()
    {
        endpointSet = false;
        measureDone = false;
        // we want to have the warning displayed after x seconds of blind interaction
        frequencyOfCheckInSeconds = 4.0;
        firstExecution = true;

        apiEndpoint = "http://10.2.1.88:8080/temperature";
        apiMethod = "GET";
    }

    void Update()
    {
        /*
        if (!endpointSet) {
            if (ontologyReader.endpointsSet) {

                Endpoint tempThing = ontologyReader.endpoints.FirstOrDefault(o => o.thing == "temperature sensor");

                apiEndpoint = tempThing.uri;
                apiMethod = tempThing.method;
                endpointSet = true;
            } 
        }
        */

        // measure temperature if user is in lab, has interacted with the blinds and no measurement has been executed before
        if ( /* sceneController.blindInteractionDone*/ sceneController.labLightTurnOnOffDone && sceneController.inLab && !measureDone)
        {
            if (firstExecution)
            {
                // based 
                timeLastExecution = DateTime.Now; // set initial starting point
                firstExecution = false;
            }

            DateTime currentDateTime = DateTime.Now;
            double diffSeconds = (currentDateTime - timeLastExecution).TotalSeconds;
            
            if (diffSeconds > frequencyOfCheckInSeconds) {
                Debug.Log("Check temperature in lab.");
                StartCoroutine(getTemperature());
                // timeLastExecution = currentDateTime; // as we only want to have it executed once
                measureDone = true;
            }   
        }

        if ((tempTooHigh || tempTooLow) && outsideAirQualityChecker.aqSet ) {
            
            string warningText = "";

            // RECOMMEND TO OPEN WINDOW
            if (tempTooLow) {
                warningText = $"The current room temperature of {Convert.ToString(currentTemp)} is too low, please increase the heating rate.";
            } else if (tempTooHigh) {
                if (outsideAirQualityChecker.outsideAirQualityOkay) {
                    // OPEN WINDOW
                    warningText = $"The current room temperature of {Convert.ToString(currentTemp)} is too high, please open the window. Don't worry, the outside air is of good quality.";   
                } else {
                    // INCREASE COOLING
                    warningText = $"The current room temperature of {Convert.ToString(currentTemp)} is too high, please increase the cooling rate as the outside air is of bad quality.";   
                }
            } 

            tempWarning.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = warningText;
            sceneController.showTemperatureWarning = true;
            
            tempTooLow = false;
            tempTooHigh = false;
            outsideAirQualityChecker.aqSet = false;
        }
    }

    private IEnumerator getTemperature() {
        
        UnityWebRequest uwr = new UnityWebRequest(apiEndpoint, apiMethod);

        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);
        
        // set the CO2 value based on the JSON structure
        double currentTemperature = (double)data["temperature"];
        Debug.Log(currentTemperature);

        currentTemp = currentTemperature;

        List<Double> tempThreshold = new List<double>();

        if (isHeatingSeason()) {
            tempThreshold.Add(21.2);
            tempThreshold.Add(24.5);
        } else {
            tempThreshold.Add(18.4);
            tempThreshold.Add(21.8); //manipulate value
        }
        
        Debug.Log(tempThreshold[0]);

        if (currentTemperature < tempThreshold[0]) {
            tempTooLow = true;
        } else if (currentTemperature > tempThreshold[1]) {
            tempTooHigh = true;
        }

        if (tempTooHigh || tempTooLow) {
            StartCoroutine(outsideAirQualityChecker.getAQData());
        }
    }

    private bool isHeatingSeason() {
        DateTime date = DateTime.Now; 
        float value = (float)date.Month + date.Day / 100f;  // <month>.<day(2 digit)>    
        if (value < 4.30 || value >= 10.1) return true;   // Winter
        return false;   // Autumn
    }   
}
