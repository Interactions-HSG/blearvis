// largely based on https://gist.github.com/amimaro/10e879ccb54b2cacae4b81abea455b10
using System;
using System.Net;
using System.Threading;
using UnityEngine;

public class HTTPListener : MonoBehaviour
{
	
	private HttpListener listener;
	private Thread listenerThread;
	//public ThunderboardHandler thunderboardHandler;
	public ThunderboardHandlerList thunderboardHandlerListScript;

	

	void Start ()
	{
		Debug.Log("start HTTPListener script");

		listener = new HttpListener();
		
		//listener.Prefixes.Add("http://10.2.1.85:5050/"); // labnet lab
		listener.Prefixes.Add("http://10.2.2.172:5050/"); // labnet office
		//listener.Prefixes.Add("http://10.2.2.167:5050/"); // labnet office
		//listener.Prefixes.Add("http://localhost:5050/"); // labnet office

		listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
		listener.Start ();

		listenerThread = new Thread (StartListener);
		listenerThread.Start ();
		Debug.Log ("Server Started");

		// there is only one thunderboardHandlerListScript in the scene
		if (thunderboardHandlerListScript == null)
		{
			var allTBHScripts = GameObject.FindGameObjectsWithTag("TBHList");
			thunderboardHandlerListScript = allTBHScripts[0].GetComponent<ThunderboardHandlerList>();
		} 
	}

	private void StartListener ()
	{
		while (true) {               
			var result = listener.BeginGetContext (ListenerCallback, listener);
			result.AsyncWaitHandle.WaitOne ();
		}
	}

	private void ListenerCallback (IAsyncResult result)
	{				
		var context = listener.EndGetContext(result);		
		
		Debug.Log ("Request received");

		if (context.Request.QueryString.AllKeys.Length > 0)
		{
			foreach (var key in context.Request.QueryString.AllKeys) {
				var value = context.Request.QueryString.GetValues(key)[0];

				Debug.Log($"key: {key}");
				Debug.Log($"value: {value}");

				/*
				if (key.StartsWith("coord"))
                {
					Debug.Log($"is coordinate");
                }
				*/
				//if (key.StartsWith("https://blarvis"))
				//{
				// https://blarvis.interactions.ics.unisg.ch/card
				// https://blarvis.interactions.ics.unisg.ch/lamp
				// https://blarvis.interactions.ics.unisg.ch/mac
				if (key.StartsWith("https://blarvis") & value == "1")
				{
					var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);  // top left x-value
					var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]);
					var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);  // bottom right x-value
					var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);

					Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
					Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

					Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

					thunderboardHandlerListScript.TempBBoxCoordTL = bBoxCoordTL;
					thunderboardHandlerListScript.TempBBoxCoordBR = bBoxCoordBR;

					thunderboardHandlerListScript.TempThingURI = key;
					thunderboardHandlerListScript.NewYoloResultArrived = true;
				}
				//}
				
			}
		}
		context.Response.Close ();
	}
}
