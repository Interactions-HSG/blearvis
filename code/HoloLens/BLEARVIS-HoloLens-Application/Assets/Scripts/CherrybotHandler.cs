using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;

public class CherrybotHandler : MonoBehaviour
{

    public NetworkHandler networkHandler;
    public string thing;
    public string json;
    public string uri;
    public string method;

    public bool requestToSend;
    public bool tokenRequested;

	void Start ()
	{
        thing = "/cherrybot";
        requestToSend = false;
        tokenRequested = false;
	}

	void Update ()
	{		
        if (requestToSend) {
            if (!networkHandler.tokenSet || networkHandler.currentThing != thing) {
                // ensure that we start the coroutine only once
                if (!tokenRequested) {
                    StartCoroutine(networkHandler.setToken(thing));
                    tokenRequested = true;
                }
            } else {
                // execute the request now
                switch (this.method)
                {
                    case "PUT":
                        StartCoroutine(networkHandler.PutRequest(thing, uri, json, true));
                        requestToSend = false;
                        tokenRequested = false;
                        break;
                }
            }
        };
	
    }

    public void changePosition(List<string> targetPosition) {

        string x = targetPosition[0];
        string y = targetPosition[1];
        string z = targetPosition[2];
        string roll = targetPosition[3];
        string pitch = targetPosition[4];
        string yaw = targetPosition[5];
        string speed = "100";

        uri = "/tcp/target";
        method = "PUT";
        json = "{\"target\": {\"coordinate\":{\"x\": " + x + ", \"y\": " + y + ",\"z\": "+z+ "},\"rotation\":{\"roll\": " + roll + ", \"pitch\": " + pitch + ", \"yaw\": " + yaw + "}}, \"speed\": " + speed + "}";
        
        requestToSend = true;
    }

    public void resetPosition() {
        
        uri = "/initialize";
        method = "PUT";
        json = "{\"none\": \"none\"}";

        requestToSend = true;
    }

    public void changeGripper(string value) {
        uri = "/gripper";
        method = "PUT";
        json = value;
        
        requestToSend = true;
    }
}