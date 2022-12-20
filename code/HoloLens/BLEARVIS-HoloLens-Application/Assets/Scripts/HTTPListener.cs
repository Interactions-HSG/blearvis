// largely based on https://gist.github.com/amimaro/10e879ccb54b2cacae4b81abea455b10
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class HTTPListener : MonoBehaviour
{

    private HttpListener listener;
    private Thread listenerThread;
    //public ThunderboardHandler thunderboardHandler;
    public ThunderboardHandlerList ThunderboardHandlerList;
    public PositionHandler PositionHandler;



    void Start()
    {
        Debug.Log("start HTTPListener script");

        listener = new HttpListener();

        var ip = GetIP4Address();
        //listener.Prefixes.Add("http://10.2.1.85:5050/"); // labnet lab
        // listener.Prefixes.Add("http://10.2.2.172:5050/"); // labnet office
        listener.Prefixes.Add($"http://{ip}:5050/"); // labnet office
        //listener.Prefixes.Add("http://10.2.2.167:5050/"); // labnet office
        //listener.Prefixes.Add("http://localhost:5050/"); // labnet office

        listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        listener.Start();

        listenerThread = new Thread(StartListener);
        listenerThread.Start();
        Debug.Log($"Server Started listening at http://{ip}:5050/");

        // there is only one thunderboardHandlerListScript in the scene
        if (ThunderboardHandlerList == null)
        {
            var allTBHScripts = GameObject.FindGameObjectsWithTag("TBHList");
            ThunderboardHandlerList = allTBHScripts[0].GetComponent<ThunderboardHandlerList>();
        }
    }



    /// <summary>
    /// Gets the IP v4 address from the current device
    /// </summary>
    /// <returns>ip address as string</returns>
    private static string GetIP4Address()
    {
        string IP4Address = String.Empty;
        foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (IPA.AddressFamily == AddressFamily.InterNetwork)
            {
                IP4Address = IPA.ToString();
                break;
            }
        }
        return IP4Address;
    }


    private void StartListener()
    {
        while (true)
        {
            var result = listener.BeginGetContext(ListenerCallback, listener);
            result.AsyncWaitHandle.WaitOne();
        }
    }

    private void ListenerCallback(IAsyncResult result)
    {
        var context = listener.EndGetContext(result);

        Debug.Log("Request received");

        if (context.Request.QueryString.AllKeys.Length > 0)
        {
            var firstKey = context.Request.QueryString.AllKeys[0];
            //foreach (var key in context.Request.QueryString.AllKeys) {
            var firstValue = context.Request.QueryString.GetValues(firstKey)[0];

            Debug.Log($"key: {firstKey}");
            Debug.Log($"value: {firstValue}");

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
            if (firstKey.StartsWith("https://blarvis") && firstValue == "1")
            {
                var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);  // top left x-value
                var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]);
                var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);  // bottom right x-value
                var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);
                var numThings = int.Parse(context.Request.QueryString["numberOfThingsInScene"]);

                Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
                Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

                Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

                ThunderboardHandlerList.TempBBoxCoordTL = bBoxCoordTL;
                ThunderboardHandlerList.TempBBoxCoordBR = bBoxCoordBR;
                ThunderboardHandlerList.TempThingURI = firstKey;
                ThunderboardHandlerList.NewYoloResultArrived = true;
                ThunderboardHandlerList.numberOfThingsCurrentlyInScene = numThings;
                Debug.Log($"numberOfThingsCurrentlyInScene: {numThings}");
            }
            else if (firstKey.StartsWith("frame"))
            {
                var frameWidth = int.Parse(context.Request.QueryString["frame_width"]);
                var frameHeight = int.Parse(context.Request.QueryString["frame_height"]);
                PositionHandler.YoloFrameWidth = frameWidth;
                PositionHandler.YoloFrameHeight = frameHeight;
                PositionHandler.NewYoloFrameDimensions = true;
            }

            //}
        }
        context.Response.Close();
    }
}
