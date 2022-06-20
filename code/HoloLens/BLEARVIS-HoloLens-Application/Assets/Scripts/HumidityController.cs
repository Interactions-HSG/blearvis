using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;
using SimpleJSON; 
using TMPro;

// this script should start the ObjectRecognition process running on another computer
public class HumidityController : MonoBehaviour
{
    private bool endpointSet;
    private string apiEndpoint;
    private string apiMethod;
    private bool firstExecution;
    private bool thresholdSet;
    private DateTime timeLastExecution;
    private int frequencyOfCheckInSeconds;
    private float humidityMinThreshold;
    private float humidityMaxThreshold;
    private bool measureDone;
    private float currentHumidity;
    private bool humiditySet;

    public OntologyReader ontologyReader;
    public SceneController sceneController;
    public GameObject humidityWarning;

    void Start()
    {
        thresholdSet = false;
        endpointSet = false; 
        frequencyOfCheckInSeconds = 120;
        firstExecution = true;
        measureDone = false; 

        humidityMinThreshold = (float)0.0;
        humidityMaxThreshold = (float)30.0; // should come from the onotlogy!

        humiditySet = false;
    }

    void Update()
    {
        if (!endpointSet) {
            if (ontologyReader.endpointsSet) {

                Endpoint humidityThing = ontologyReader.endpoints.FirstOrDefault(o => o.thing == "humidity sensor");

                apiEndpoint = humidityThing.uri;
                apiMethod = humidityThing.method;

                Debug.Log("Humidity thing");
                Debug.Log(apiEndpoint);
                Debug.Log(apiMethod);
                
                endpointSet = true;
            } 
        }
        
        /*
        if (!thresholdSet) {
            if (ontologyReader.thresholdsSet) {
                humidityMinThreshold = ontologyReader.thresholds.FirstOrDefault(o => o.thing == "humidity sensor").minThreshold;
                humidityMaxThreshold = ontologyReader.thresholds.FirstOrDefault(o => o.thing == "humidity sensor").minThreshold;
            }
        }
        */

        if (sceneController.inOffice && !measureDone && sceneController.rainWarningDone)
        {
            if (firstExecution)
            {
                timeLastExecution = DateTime.Now;
                firstExecution = false;
            }
            
            if ((DateTime.Now - timeLastExecution).TotalSeconds > 20.0) {
                Debug.Log("Check humidity concentration in office.");
                StartCoroutine(getHumidity());
                // timeLastExecution = currentDateTime;
                measureDone = true; 
            }   
        }

        if (humiditySet) {
            Debug.Log(currentHumidity);
            string warningText = "";
            if (currentHumidity < humidityMinThreshold)
            {
                warningText = $"The current humidity of {Convert.ToString(currentHumidity)} is too low. I already adjusted the mechanical ventilation accordingly to achieve good humidity. You don't have to do anything else.";
            }
            else if (currentHumidity > humidityMaxThreshold)
            {
                warningText = $"The current humidity of {Convert.ToString(currentHumidity)} is too high. I already adjusted the mechanical ventilation accordingly to achieve good humidity. You don't have to do anything else.";
            }

            humidityWarning.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = warningText;
            sceneController.showHumidityWarning = true;
            humiditySet = false;
        }
    }
    private IEnumerator getHumidity() {

        UnityWebRequest uwr = new UnityWebRequest(apiEndpoint, apiMethod);

        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);
        
        // set the humidity value based on the JSON structure
        currentHumidity = (float)data["humidity"];
        humiditySet = true;
    }
}