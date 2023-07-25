// largely based on https://gist.github.com/amimaro/10e879ccb54b2cacae4b81abea455b10
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class HTTPListener : MonoBehaviour
{

    private HttpListener listener;
    private Thread listenerThread;
    public ThunderboardHandlerList ThunderboardHandlerList;
    public PositionHandler PositionHandler;
    public StaticDeviceHandler StaticDeviceHandler;

    void Start()
    {
        Debug.Log("start HTTPListener script");

        listener = new HttpListener();

        var ip = GetIP4Address();
        // MAKE SURE that the port number here corresponds to the port number 
        // in  code/HoloLens/BLEARVIS-Desktop-ObjectDetection/modules/YoloModule/app/config.yaml: HOLO_ENDPOINT_URL
        listener.Prefixes.Add($"http://{ip}:8090/");

        listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        listener.Start();

        listenerThread = new Thread(StartListener);
        listenerThread.Start();
        Debug.Log($"Server Started listening at http://{ip}:8090/");

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

            if ((firstKey.StartsWith("robot") || firstKey.StartsWith("tractor")) && firstValue == "1" && ThunderboardHandlerList.handleIncomingYoloResult)
            {
                var bBoxCoordTLx = int.Parse(context.Request.QueryString["coordTLx"]);  // top left x-value
                var bBoxCoordTLy = int.Parse(context.Request.QueryString["coordTLy"]);
                var bBoxCoordBRx = int.Parse(context.Request.QueryString["coordBRx"]);  // bottom right x-value
                var bBoxCoordBRy = int.Parse(context.Request.QueryString["coordBRy"]);
                var numThings = int.Parse(context.Request.QueryString["numberOfThingsInScene"]);
                var frameStartTime = context.Request.QueryString["framestart"];

                Vector2 bBoxCoordTL = new Vector2(bBoxCoordTLx, bBoxCoordTLy);
                Vector2 bBoxCoordBR = new Vector2(bBoxCoordBRx, bBoxCoordBRy);

                Debug.Log($"coords received: {bBoxCoordTL} -- {bBoxCoordBR}");

                ThunderboardHandlerList.TempBBoxCoordTL = bBoxCoordTL;
                ThunderboardHandlerList.TempBBoxCoordBR = bBoxCoordBR;
                ThunderboardHandlerList.TempThingURI = firstKey;
                ThunderboardHandlerList.NewYoloResultArrived = true;
                ThunderboardHandlerList.numberOfThingsCurrentlyInScene = numThings;

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(frameStartTime));
                ThunderboardHandlerList.TmpFrameTime = dateTimeOffset.DateTime;
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
        }
        context.Response.Close();
    }
}
