using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using System.Linq;
using SimpleJSON;
using System;
using System.Collections.Generic;
//using Microsoft.MixedReality.Toolkit.Examples.Experimental.HandMenu;


public class HueController : MonoBehaviour {
     
    public SceneController sceneController;
    public CO2LevelController cO2LevelController;
    public OntologyReader ontologyReader;
    public GameObject brightnessSlider;
    public GameObject redToggle;
    public GameObject greenToggle;
    public GameObject yellowToggle;
    public GameObject purpleToggle;
    public TextMeshPro hueInfoDescripton;

    private string currentColor;
    private bool hueDataSet;
    private bool hueUpdated;
    private int currentBrightness;
    private string apiStatusEndpoint = "";
    private string apiStatusMethod;
    private string apiUpdateEndpoint = "";
    private string apiUpdateMethod;
    private bool co2Requested;

    public bool adjustLamp {get; set; }
    public bool getCurrentHue {get; set; }
    public bool showHueInfoBox {get; set; }
    public bool showHueControl {get; set; }

    public string targetColor {get; set; }

    public float targetBrightness {get; set; }

    void Start() {
        adjustLamp = false;

        // set this boolean to TRUE to set the current hue data in the control box
        getCurrentHue = false;
        co2Requested = false;
        hueDataSet = false;
        showHueInfoBox = false;
        showHueControl = false;
    }

    void Update() {
        
        // set hue endpoint
        if (apiStatusEndpoint == "" && apiUpdateEndpoint == "") {
            if (ontologyReader.endpointsSet) {
                ontologyReader.endpoints.ToList().ForEach(o => {
                    if (o.thing == "hue") {
                       
                        if (o.actionDescription == "lamp status") {
                            apiStatusEndpoint = o.uri;
                            apiStatusMethod = o.method;
                        } else if (o.actionDescription == "change color") {
                            apiUpdateEndpoint = o.uri;
                            apiUpdateMethod = o.method;
                        }
                    }   
                });
            }
        }

        if (adjustLamp) {
            StartCoroutine(changeLampState());
            adjustLamp = false;
        }

        if (getCurrentHue && apiStatusEndpoint != "") {
            StartCoroutine(getHueData());
            getCurrentHue = false;
        }

        if (hueDataSet) {
            // update GUI with current values
            
            switch (currentColor)
            {
                case "green":
                    greenToggle.GetComponent<Interactable>().IsToggled = true;
                    break;
                case "red":
                    redToggle.GetComponent<Interactable>().IsToggled = true;
                    break;
                case "gold":
                    yellowToggle.GetComponent<Interactable>().IsToggled = true;
                    break;
                case "violet":
                    purpleToggle.GetComponent<Interactable>().IsToggled = true;
                    break;
            }
            
            
            float newBrightness = (float) currentBrightness;

            // SET PINCH SLIDER VALUE
            // update value
            //brightnessSlider.GetComponent<PinchSlider>().SliderValue = newBrightness;

            // update line
            //brightnessSlider.transform.Find("TrackVisuals").Find("Line_active").gameObject.transform.localScale = new Vector3(transform.localScale.x, newBrightness, transform.localScale.z);
            // update label
            //brightnessSlider.transform.Find("SliderValue").GetComponent<TextMeshPro>().text = $"{newBrightness:F2}";
            
            hueDataSet = false;
            hueUpdated = true;
        }


        if (showHueInfoBox || showHueControl) {
            getCurrentHue = true;
            if (hueUpdated) {
                // now we have the current hue data and can display the info or control box
                if (showHueInfoBox) {
                    if (!co2Requested) {
                        cO2LevelController.getCurrentCo2 = true;
                        co2Requested = true;
                    }
                    
                    if (cO2LevelController.valueUpdated) {
                        // we have now the newest co2-level in the office
                        displayHueInfoBox();
                        cO2LevelController.valueUpdated = false;
                        co2Requested = false;
                        hueUpdated = false;
                        showHueInfoBox = false;
                    }
                } else if (showHueControl) {
                    sceneController.showHueControl = true;
                    
                    showHueControl = false;
                    hueUpdated = false;
                }
            }
        }

        /*
        // we show the information only one time, when the user is standing in front of the hue lamp, this is the case as soon as we have retrieved the current CO2-level from the sensor.
        if (cO2LevelController.valueUpdated) {

            // display now the warning messages with alle the texts
            string infoText = $"This is our smart hue lamp. Its color represents the current air quality in the room. The color {currentColor} indicates a {airQuality} air quality, as the CO2 concentration is currently {cO2LevelController.co2Value}";

            sceneController.hueInformation.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = infoText;
            sceneController.showHueInformation = true;
            cO2LevelController.valueUpdated = false;
        }
        */
    }

    public void updateTargetBrightness() {
        targetBrightness = brightnessSlider.GetComponent<PinchSlider>().SliderValue;
    }

    private IEnumerator getHueData() {

        UnityWebRequest uwr = new UnityWebRequest(apiStatusEndpoint, apiStatusMethod);

        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);
        
        currentColor = data["color"];
        currentBrightness = data["brightness"];

        hueDataSet = true;
    }

    private IEnumerator changeLampState() {

        int brightness = (int) (targetBrightness * 100);
        string json = "{\"on\": true,\"color\": \"" + targetColor + "\", \"brightness\":" + brightness.ToString() + ", \"override\": true}";
        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest uwr = new UnityWebRequest();
        
        if (apiUpdateMethod == "PUT") {
            Debug.Log(apiUpdateEndpoint);
            uwr =  UnityWebRequest.Put(apiUpdateEndpoint, dataToPut);
        }
        
        uwr.SetRequestHeader ("Content-Type", "application/json");
        yield return uwr.SendWebRequest();

        Debug.Log(uwr.responseCode);
    }

    public void displayHueInfoBox() {
        // get CO2 value and set tex tin infobox and display the ifnobox

        string airQuality = "";

        switch (currentColor)
        {
            case "green":
                airQuality = "good";
                break;
            case "red":
                airQuality = "bad";
                break;
            case "violet":
                airQuality = "critical";
                break;
            case "gold":
                airQuality = "moderate";
                break;
        }

        hueInfoDescripton.text = $"This ist the smart hue-lamp. Its color indicates how well the air-quality currently is. The current color {currentColor} indicates a {airQuality} airquality as the current CO2 level is at {cO2LevelController.co2Value}. Click the button below to control the lamp yourself.";
        sceneController.showHueInformation = true; 
    }
}