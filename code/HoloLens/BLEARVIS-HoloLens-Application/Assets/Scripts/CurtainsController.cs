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

public class CurtainsController : MonoBehaviour
{
    public OntologyReader ontologyReader;
    public SceneController sceneController;
    public GameObject curtainsControl;
    
    public GameObject stopButton;
    public GameObject upButton;
    public GameObject downButton;

    private GameObject statusText;

    private DateTime buttonClicked;
    private string apiEndpoint;
    private string apiMethod;
    private string curtainStopEndpoint;
    private string curtainUpEndpoint;
    private string curtainDownEndpoint;
    private bool endpointsSet;
    private bool executingUp;
    private bool executingStop;
    private bool executingDown;

    public bool processWindow;
    
    void Start()
    {
        endpointsSet = false;
        processWindow = false;
        statusText = curtainsControl.transform.Find("StatusText").gameObject;
        statusText.SetActive(false);
    }

    // one script for both, curtains in lab and in office!

    void Update()
    {
        if (executingDown || executingUp || executingStop) {

            double difference = (DateTime.Now - buttonClicked).TotalSeconds;

            if (executingDown || executingUp) {
                // wait for 8 seconds
                if (difference > 8.0) {
                    upButton.SetActive(true);
                    downButton.SetActive(true);

                    statusText.SetActive(false);
                    executingUp = false;
                    executingDown = false;
                }
            } else if (executingStop) {
                // wait for 3 seconds
                if (difference > 3.0) {
                    stopButton.SetActive(true);
                    
                    statusText.SetActive(false);
                    executingStop = false;
                }
            }
        }

        if (processWindow) {
            
            GameObject stopButton = curtainsControl.transform.Find("ButtonCollection").Find("Stop").gameObject;

            if (sceneController.inOffice && sceneController.labLightReminderDone) {
                // display curtain control with all three buttons and set endpoints accordingly
                setEndpoints("402");
                stopButton.SetActive(true);
                sceneController.showCurtainsControl = true;

                processWindow = false;
            } else if (sceneController.inLab && sceneController.labLightTurnOnOffDone) {
                // display curtain control with only two buttons and set endpoints accordingly
                setEndpoints("lab");
                stopButton.SetActive(false);
                // sceneController.showCurtainsControl = true;
                
                processWindow = false;
            }
            
            processWindow = false;
        }       
    }

    public void setEndpoints(string location) {

        if (ontologyReader.endpointsSet) {
            ontologyReader.endpoints.ToList().ForEach(o => {
                if (o.thing == "window") {
                    if (o.actionName.Contains(location)) {
                        apiEndpoint = o.uri;
                        apiMethod = o.method;
                    }
                }
            });

            // set endpoint for each button
            curtainUpEndpoint = apiEndpoint + "up";
            curtainDownEndpoint = apiEndpoint + "down";
            curtainStopEndpoint = apiEndpoint + "stop";

        }
    }

    public void curtainsUp()
    {
        StartCoroutine(modifyCurtainState(curtainUpEndpoint));

        buttonClicked = DateTime.Now;
        executingUp = true;

        Vector3 pos = new Vector3((float)-0.047, (float)0.0249, (float)-0.008200049);
        statusText.GetComponent<RectTransform>().anchoredPosition = pos; 

        upButton.SetActive(false);
        statusText.SetActive(true);
    }

    public void curtainsDown()
    {
        StartCoroutine(modifyCurtainState(curtainDownEndpoint));
        
        buttonClicked = DateTime.Now;
        executingDown = true;

        Vector3 pos = new Vector3((float)-0.047, (float)-0.0299, (float)-0.008200049);
        statusText.GetComponent<RectTransform>().anchoredPosition = pos; 
        
        downButton.SetActive(false);
        statusText.SetActive(true);
    }

    public void curtainsStop()
    {
        StartCoroutine(modifyCurtainState(curtainStopEndpoint));

        buttonClicked = DateTime.Now;
        executingStop = true;

        Vector3 pos = new Vector3((float)-0.047, (float)-0.0043, (float)-0.008200049);
        statusText.GetComponent<RectTransform>().anchoredPosition = pos; 

        stopButton.SetActive(false);
        statusText.SetActive(true);
    }

    private IEnumerator modifyCurtainState(string uri)
    {
        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes("{}");
        UnityWebRequest uwr = new UnityWebRequest();

        if (apiMethod == "PUT") {
            uwr = UnityWebRequest.Put(uri, dataToPut);
        }

        yield return uwr.SendWebRequest();
        yield return new WaitForSeconds((float)1.0);

        Debug.Log(uwr.responseCode);    
    }
}