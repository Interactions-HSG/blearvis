using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using SimpleJSON;
using Microsoft.MixedReality.Toolkit.UI;

public class LabLightHandler : MonoBehaviour
{

    public OntologyReader ontologyReader;
    public NetworkHandler networkHandler;
    public SceneController sceneController;
    public GameObject lightButton;

    public bool processCeilingLightAppereance = false;

    private bool sendReminder;
    private bool labLightsChecked;

    private string apiStatusEndpoint = "";
    private string apiStatusMethod;
    private string apiUpdateEndpoint = "";
    private string apiUpdateMethod;

	void Start ()
	{
        labLightsChecked = false;
	}

	void Update ()
	{	

        if (sceneController.inOffice && !labLightsChecked) {

            if ((DateTime.Now - sceneController.officeEntryTime).TotalSeconds > 5.0)
            {
                StartCoroutine(getLightState(false));
                labLightsChecked = true;
            }
            
        }

        if (sendReminder) {
            sceneController.showLabLightReminder = true;
            Debug.Log("We are here!");
            sendReminder = false;
        }


        if (processCeilingLightAppereance) {
            StartCoroutine(getLightState(true));
            processCeilingLightAppereance = false;
        }


        // set here the endpoints
        if (apiUpdateEndpoint == "" && apiStatusEndpoint == "") {
            if (ontologyReader.endpointsSet) {
                ontologyReader.endpoints.ToList().ForEach(o => {
                    if (o.thing == "ceiling-light") {
                        if (o.actionDescription == "switch ligths") {
                            // error in ontology, that's why a split is necessary
                            apiUpdateEndpoint = o.uri.Split('?')[0];
                            apiUpdateMethod = o.method;

                        } else if (o.actionDescription == "ligths status") {
                            apiStatusEndpoint = o.uri;
                            apiStatusMethod = o.method;
                        }
                    }
                });
            }
        }

        /*	
        if (requestToSend) {
                // execute the request now
                switch (this.method)
                {
                    case "PUT":
                        StartCoroutine(networkHandler.PutRequest(thing, uri, "", false));
                        requestToSend = false;
                        break;
                }
        };
        */
	
    }
    public void turnLightOnOff(string action) {

        if (action == "turn off")
        {
            StartCoroutine(modifyLightState("false"));
        } else if (action == "turn on")
        {
            StartCoroutine(modifyLightState("true"));
        }
    }

    private IEnumerator getLightState(bool getDataOnly) {
        Debug.Log(apiStatusEndpoint);
        Debug.Log(apiStatusMethod);
        UnityWebRequest uwr = new UnityWebRequest(apiStatusEndpoint, apiStatusMethod);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        JSONNode data = JSON.Parse(uwr.downloadHandler.text);
        Debug.Log(uwr.downloadHandler.text);
        if (!getDataOnly) {
            // if light is still on when user entered the office, we recommend the robot to turn off the light automatically
            if (data["state"] == "on") {
                sendReminder = true;
            } else {
                sceneController.labLightReminderDone = true;
            }
        } else {
            if (data["state"] == "on") {
                lightButton.GetComponent<Interactable>().IsToggled = true;
            } else {
                lightButton.GetComponent<Interactable>().IsToggled = false;
            }
        }
    }

    private IEnumerator modifyLightState(string state)
    {     
        string json = "{\"state\":" + state  + "}";

        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest uwr = new UnityWebRequest();

        if (apiUpdateMethod == "PUT") {
            uwr = UnityWebRequest.Put(apiUpdateEndpoint, dataToPut);
        }
       
        uwr.SetRequestHeader ("Content-Type", "application/json");
        yield return uwr.SendWebRequest();

        Debug.Log(uwr.responseCode);    

        if (uwr.responseCode != System.Convert.ToInt64(200) && uwr.responseCode != System.Convert.ToInt64(202))
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Request sent successfully!");            
        }

    }
}