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

public class WindowController : MonoBehaviour
{
    public OntologyReader ontologyReader;
    public SceneController sceneController;
    public WeatherChecker weatherChecker;
    public OutsideAirQualityChecker outsideAirQualityChecker;
    public GameObject windowOpenWarning;
    
    private bool checkClosingNeeded;
    private bool processRunning;
    private string apiEndpoint;
    private string apiMethod;
    private int frequencyOfCheckInSeconds;
    private DateTime timeLastExecution;
    private bool firstExecution;
    private bool endpointSet;
    private bool measureDone; 


    // Start is called before the first frame update
    void Start()
    {
        frequencyOfCheckInSeconds = 120;
        firstExecution = true;
        processRunning = false;
        checkClosingNeeded = false;
        endpointSet = false;
        measureDone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!endpointSet)
        {
            if (ontologyReader.endpointsSet)
            {
                Endpoint windowThing = ontologyReader.endpoints.FirstOrDefault(o => o.thing == "window status");

                apiEndpoint = windowThing.uri;
                apiMethod = windowThing.method;
                endpointSet = true;
            } 
        }
        
        if (sceneController.inOffice && sceneController.co2WarningDone && !measureDone && endpointSet)
        {    
            if (firstExecution)
            {
                timeLastExecution = DateTime.Now;
                firstExecution = false;
            }

            DateTime currentDateTime = DateTime.Now;
            double diffMinutes = (currentDateTime - timeLastExecution).TotalSeconds;
           
            // execute check 30 seconds after co2 warning was displayed
            if ((DateTime.Now - timeLastExecution).TotalSeconds > 25.0)
            {
                Debug.Log("Check if window is open..");
                StartCoroutine(checkWindow());
                timeLastExecution = currentDateTime;
                processRunning = true;
                measureDone = true;
            }  
        }
        
        if (checkClosingNeeded && processRunning)
        {
            StartCoroutine(weatherChecker.getWeatherForecast());
            StartCoroutine(outsideAirQualityChecker.getAQData());
            checkClosingNeeded = false;
        }

        if (weatherChecker.weatherForecastSet && outsideAirQualityChecker.aqSet && processRunning)
        {
            bool displayWarning = false;
            string warningText = "I just recognized that it is going to rain very soon. Therefore, please close the windows again.";
            if (weatherChecker.goingToRain || !outsideAirQualityChecker.outsideAirQualityOkay)
            {
                displayWarning = true;
            }

            if (displayWarning)
            {
                windowOpenWarning.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = warningText;
                sceneController.showWindowOpenWarning = true;
            }
            
            weatherChecker.weatherForecastSet = false;
            outsideAirQualityChecker.aqSet = false;
            processRunning = false;
        }
        
    }

    public IEnumerator checkWindow () {

        UnityWebRequest uwr = new UnityWebRequest(apiEndpoint, apiMethod);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);

        // check if window is open and set checkForecast only if window is open 

        // window is open
        if (data["window"] == 1) {
            checkClosingNeeded = true;
        }

        bool windowopen = true;
        if (windowopen)
        {
            checkClosingNeeded = true;
        }
    }
}