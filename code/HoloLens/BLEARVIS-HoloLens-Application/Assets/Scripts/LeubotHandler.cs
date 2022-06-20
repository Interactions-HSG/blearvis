using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;

public class LeubotHandler : MonoBehaviour
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
        thing = "/leubot1/v1.2";
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

    public void changePosition(
        string value,
        string element
        ) {

        uri = element;
        method = "PUT";
        json = "{\"value\":" + value + "}";
        
        requestToSend = true;
    }
}