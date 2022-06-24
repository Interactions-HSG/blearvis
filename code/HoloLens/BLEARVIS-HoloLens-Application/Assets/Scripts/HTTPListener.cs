// largely based on https://gist.github.com/amimaro/10e879ccb54b2cacae4b81abea455b10
using System;
using System.Net;
using System.Threading;
using UnityEngine;

public class HTTPListener : MonoBehaviour
{
	public SceneController sceneController;
	private HttpListener listener;
	private Thread listenerThread;
	public HueController hueController;
	public CurtainsController curtainsController;
	public authenticator authenticator;
	public RobotInSceneHandler robotInSceneHandler;
	public LabLightHandler labLightHandler;
	//public ThunderboardHandler thunderboardHandler;
	public ThunderboardHandlerListScript thunderboardHandlerListScript;

	private bool cherryBotReceived = false;
	private bool leubotReceived = false;
	private bool hueReceived = false;
	private bool ceilingLightReceived = false;
	private bool windowDone = false;

	void Start ()
	{
		Debug.Log("start HTTPListener script");

		listener = new HttpListener();
		
		//listener.Prefixes.Add("http://10.2.1.85:5050/"); // labnet lab
		listener.Prefixes.Add("http://10.2.2.172:5050/"); // labnet office
		//listener.Prefixes.Add("http://localhost:5050/"); // labnet office

		listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
		listener.Start ();

		listenerThread = new Thread (StartListener);
		listenerThread.Start ();
		Debug.Log ("Server Started");

		cherryBotReceived = false;
		leubotReceived = false;
		hueReceived = false;
		ceilingLightReceived = false;
		windowDone = false;

		// there is only one thunderboardHandlerListScript in the scene
		if (thunderboardHandlerListScript == null)
		{
			var allTBHScripts = GameObject.FindGameObjectsWithTag("TBHList");
			thunderboardHandlerListScript = allTBHScripts[0].GetComponent<ThunderboardHandlerListScript>();
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
				if (key == "bottle" & value == "1")
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
				

				/*
				switch (key)
				{	
					case "Cherrybot":
						// 1 is equal to show and 0 is equal to hide
						if (value == "1")
						{
							// only show Cherrybot if interaction with blinds already happended
							if (sceneController.cherrybotNextStepDone && sceneController.inLab && !cherryBotReceived) {
								robotInSceneHandler.thing = "Cherrybot";
								robotInSceneHandler.processRobot = true;
								cherryBotReceived = true;
							}
							
						}
						else if (value == "0")
						{
							sceneController.showCherrybotControl = false;
						}
						break;
					case "Leubot":
						if (value == "1")
						{
							// only show Leubot it interaction with blinds already happened
							if (sceneController.cherrybotInteractionDone && sceneController.inLab && !leubotReceived) {
								robotInSceneHandler.thing = "Leubot";
								robotInSceneHandler.processRobot = true;
								leubotReceived = true;
							}	
							
						}
						else if (value == "0")
						{
							sceneController.showLeubotControl = false;
						}
						break;
					case "desk-bulb":
						if (value == "1")
						{
							if (!sceneController.deskBulbDone) {
								sceneController.showDeskBulbInfo = true;
							}
						}
						break;
					case "desk-lamp":
						if (value == "1")
						{
							if (!sceneController.deskLampDone) {
								sceneController.showDeskLampInfo = true;
							}
						}
						break;
					case "lab":
						if (value == "1")
						{
							sceneController.showLabWelcomeBox = true;
						}
						else if (value == "0")
						{
							sceneController.showLabWelcomeBox = false;
						}
						break;
					case "office":
						if (value == "1")
						{
							sceneController.showOfficeWelcomeBox = true;
							// check if user has turned off light in Lab
							
						}
						else if (value == "0")
						{
							sceneController.showOfficeWelcomeBox = false;
						}
						break;
					case "ceiling-light":
						if (value == "1")
						{	
							if (!ceilingLightReceived) {
								labLightHandler.processCeilingLightAppereance = true;
								sceneController.showCeilingLightControl = true;
								ceilingLightReceived = true;
							}
							
						}
						else if (value == "0")
						{
							sceneController.showCeilingLightControl = false;
						}
						break;
					case "hue":
						if (value == "1")
						{
							if (sceneController.inOffice && sceneController.blindInteractionDone && !hueReceived){
								hueController.showHueInfoBox = true;
								hueReceived = true;
							}
						}
						break;
					case "hue-control":
						if (value == "1")
						{
							sceneController.showHueControl = true; // testing 
							if (sceneController.inOffice && sceneController.blindInteractionDone){
								hueController.showHueInfoBox = true;
							}
						}
						break;
					/*
					case "hue-red":
						if (value == "1")
						{
							hueController.showHueInfoBox = true;
						}
						else if (value == "0")
						{
							sceneController.showHueInformation = false;
							sceneController.showHueControl = false;
						}
						break;
					case "hue-green":
						if (value == "1")
						{
							hueController.showHueInfoBox = true;
						}
						else if (value == "0")
						{
							sceneController.showHueInformation = false;
							sceneController.showHueControl = false;	
						}
						break;
					case "hue-yellow":
						if (value == "1")
						{
							hueController.showHueInfoBox = true;
						}
						else if (value == "0")
						{
							sceneController.showHueInformation = false;
							sceneController.showHueControl = false;
						}
						break;
					case "hue-purple":
						if (value == "1")
						{
							hueController.showHueInfoBox = true;
						}
						else if (value == "0")
						{
							sceneController.showHueInformation = false;
							sceneController.showHueControl = false;
						}
						break;
					*/
					/* 
					// ENDPOINT FOR MIRO CARD AUTHENTICATION
					case "miroAuth":
					if (value == "1")
					{
						// display info box that tells the user that the authentication was successful
						authenticator.loggedIn = true;
					}
					else if (value == "0")
					{
						
					}
					break;
					
					case "cbEnded":
						if (value == "1")
						{
							// display info box that tells the user that the authentication was successful
							robotInSceneHandler.tbProcessFinished = true;
						}
					break;
					/
					case "window":
						if (value == "1")
						{
							if (!windowDone)
							{
								// display the curtains control button
								curtainsController.processWindow = true;
								windowDone = true;
							}
							
						}
						break;
					case "smartcard":
						if (value == "1")
						{
							// display the info boy only after temperature warning has been done
							if (sceneController.temperatureWarningDone && sceneController.inLab && !sceneController.miroCardInfoDone) {
								sceneController.showMiroCardInfo = true;
							}
						}
						break;

					case "https://blarvis.interactions.ics.unisg.ch/card":
						if (value == "1")
						{
							// NOT yet tested for multiple boards!
							Debug.Log("detected card");


							var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);	// top left x-value
							var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]); 
							var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);	// bottom right x-value
							var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);
							
							Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
							Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

							Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

							thunderboardHandlerListScript.TempBBoxCoordTL = bBoxCoordTL;
							thunderboardHandlerListScript.TempBBoxCoordBR = bBoxCoordBR;
							thunderboardHandlerListScript.TempThingURI = key;
							thunderboardHandlerListScript.NewYoloResultArrived = true;
								
							
						}
						break;
					case "https://blarvis.interactions.ics.unisg.ch/lamp":
						if (value == "1")
						{
							Debug.Log("detected lamp");
							var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);  // top left x-value
							var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]);
							var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);  // bottom right x-value
							var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);

							Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
							Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

							Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

						}
						break;
					case "https://blarvis.interactions.ics.unisg.ch/mac":
						if (value == "1")
						{
							Debug.Log("detected mac");
							var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);  // top left x-value
							var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]);
							var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);  // bottom right x-value
							var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);

							Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
							Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

							Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

						}
						break;

				}
			*/
			}
		}
		context.Response.Close ();
	}
}
