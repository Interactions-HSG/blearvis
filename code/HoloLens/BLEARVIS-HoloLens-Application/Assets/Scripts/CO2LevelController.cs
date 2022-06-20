using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Web;
using System;
using SimpleJSON;
using System.Globalization;
using TMPro;
using System.Linq;

public class CO2LevelController : MonoBehaviour
{
    public OntologyReader ontologyReader;
    public WeatherChecker weatherChecker;
    public OutsideAirQualityChecker outsideAirQualityChecker;
    public GameObject co2Warning;
    public SceneController sceneController;

    private double co2Threshold;
    public int co2Value;
    private bool checkOpeningWindowsOkay;
    private bool processRunning;
    public bool valueUpdated;

    private DateTime timeLastExecution;
    private int frequencyOfCheckInSeconds;
    private string apiEndpoint;
    private string apiMethod;

    private bool firstExecution;
    private bool endpointSet;
    private bool threshSet;
    private bool measureDone;
    public bool getCurrentCo2;

    // Start is called before the first frame update
    void Start()
    {
        checkOpeningWindowsOkay = false;
        firstExecution = true;
        processRunning = false;
        endpointSet = false;
        valueUpdated = false;
        measureDone = false;
        threshSet = false;
        getCurrentCo2 = false;
        
        frequencyOfCheckInSeconds = 120; // must come from the ontology
    }

    // Update is called once per frame
    void Update()
    {
        if (!endpointSet) {
            if (ontologyReader.endpointsSet) {

                Endpoint co2Thing = ontologyReader.endpoints.FirstOrDefault(o => o.thing == "CO2 sensor");
                
                apiEndpoint = co2Thing.uri;
                apiMethod = co2Thing.method;
                endpointSet = true;
            } 
        }

        if (!threshSet) {
            if (ontologyReader.thresholdsSet) {
                Threshold co2Thresh = ontologyReader.thresholds.FirstOrDefault(o => o.desc == "target CO2");

                co2Threshold = co2Thresh.value;
                threshSet = true;
            }
        }

        if (getCurrentCo2) {
            StartCoroutine(getCo2Data(true));
            getCurrentCo2 = false;
        }

        if (sceneController.inOffice && sceneController.hueInteractionDone && !measureDone && endpointSet && threshSet)
        {

            if (firstExecution)
            {
                timeLastExecution = DateTime.Now;
                firstExecution = false;
            }
            
            // execute CO2 check after 30s seconds
            if ((DateTime.Now - timeLastExecution).TotalSeconds > 20.0) {
                Debug.Log("Check CO2 concentration in office.");
                StartCoroutine(getCo2Data(false));
                processRunning = true;
                measureDone = true;
            }   
        }

        if (checkOpeningWindowsOkay && processRunning) {
            // CHECK OUTSIDE AIR QUALITY
            StartCoroutine(outsideAirQualityChecker.getAQData());

            // CHECK IF IT OS GOING TO RAIN
            // for te user study we disable this check as the SRF API is quite unstable
            //StartCoroutine(weatherChecker.getWeatherForecast());
            checkOpeningWindowsOkay = false;
        }

        if (outsideAirQualityChecker.aqSet /*&& weatherChecker.weatherForecastSet*/ && processRunning) {

            string warningText = "";

            // OUTSIDE AIR QUALITY OKAY AND NOT GOING TO RAIN -> OPEN WINDOW
            if (outsideAirQualityChecker.outsideAirQualityOkay && !weatherChecker.goingToRain)
            {
                warningText = $"The current CO2 pollution in the office exceeds the maximum limit. Please open the windows to achieve better air quality and productivity. Don't worry, the outdoor air quality is fine.";
            }
            // OUTSIDE AIR QUALITY OKAY BUT IT IS GOING TO RAIN -> DO NOT OPEN WINDOW
            else if (outsideAirQualityChecker.outsideAirQualityOkay && weatherChecker.goingToRain)
            {
                warningText = $"The current CO2 pollution in the office exceeds the maximum limit. As it is going to rain with a probability of more than {Convert.ToString(weatherChecker.rainProbabilityThreshold)} percent, please do not open the windows. Instead, consider increase the mechanical ventilation to achieve better air quality and productivity."; 
            }
            // OUTSIDE AIR QUALITY NOT OKAY AND IT IS NOT GOING TO RAIN -> DO NOT OPEN WINDOW
            else if (!outsideAirQualityChecker.outsideAirQualityOkay && !weatherChecker.goingToRain)
            {
                warningText = $"The current CO2 pollution in the office exceeds the maximum. As the outside air quality is not okay and could harm you health, please do not open the windows. Instead, consider increase the mechanical ventilation to achieve better air quality and productivity."; 
            }
            // OUTSIDE AIR QUALITY NOT OKAY AND IT IS GOINT TO RAIN -> DO NOT OPEN WINDOW
            else if (!outsideAirQualityChecker.outsideAirQualityOkay && weatherChecker.goingToRain)
            {
                warningText = $"The current CO2 pollution in the office exceeds the maximum limit. As the outside air quality is not okay and it is very likely going to rain, please do not open the windows. Instead, consider increase the mechanical ventilation to achieve better air quality and productivity."; 
            }

            co2Warning.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = warningText;
            sceneController.showCO2Warning = true;

            outsideAirQualityChecker.aqSet = false;
            weatherChecker.weatherForecastSet = false;
            processRunning = false;
        }
    }

    public IEnumerator getCo2Data (bool dataOnly) {

        UnityWebRequest uwr = new UnityWebRequest(apiEndpoint, apiMethod);

        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);
        
        // set the CO2 value based on the JSON structure
        co2Value = data["co2"];
        
        if (!dataOnly) {
            if (co2Value > co2Threshold) {
                checkOpeningWindowsOkay= true;
            }
        } else {
            valueUpdated = true;
        }
    }
}
