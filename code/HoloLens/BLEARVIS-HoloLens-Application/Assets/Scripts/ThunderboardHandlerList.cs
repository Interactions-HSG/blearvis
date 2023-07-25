using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ThunderboardHandlerList : MonoBehaviour
{
    [Header("Global Scripts")]
    public List<ThunderboardHandler> thunderboardHandlerList;
    public PositionHandler PositionHandler;
    public StaticDeviceHandler StaticDeviceHandler;

    [Header("Prefabs")]
    //public GameObject thunderboardInfoBoxPrefab;
    public GameObject ThunderboardInfoBoxOrbitalPrefab;

    //public GameObject SetYButton;
    [Header("Counter")]
    public int numberOfThingsCurrentlyInScene;
    public int numberOfTBCurrentlyInScene;
    public int numberOfYoloCoordinatesReceivedForCurrentDevices;
    public int numberOfAoAsReceivedForCurrentDevices;
    public GameObject AoACounterText;
    public GameObject YoloCounterText;
    public string MACofFirstBLETag;

    // public List<(string idORuri, Vector3 offset)> ListOfCurrentNewOffsets;
    // public List<(string idORuri, float aoa)> ListOfCurrentNewAoAs;

    public Dictionary<string, List<Vector3>> ListOfCurrentNewOffsets;
    public Dictionary<string, List<Vector3>> DictOfAoaOffsetLists;
    public Dictionary<string, List<Vector3>> DictOfYoloOffsetLists;
    public Dictionary<string, List<float>> ListOfCurrentNewAoAs;

    [Header("Values from HTTPListener")]
    public Vector2 TempBBoxCoordTL = new Vector2(0, 0);
    public Vector2 TempBBoxCoordBR = new Vector2(0, 0);
    public string TempThingURI = "";
    public DateTime TmpFrameTime;
    public bool NewYoloResultArrived = false;

    [Header("If incoming results should be handled")]
    public bool handleIncomingYoloResult = false;
    public bool handleIncomingAoAResult = false;
    public bool handleIncomingAoASensorData = false;

    public bool expectingAoA;
    public bool expectingYolo;
    public bool expectingMultipleThings;

    /*
    public string pointMode = "world";
    public float GlobalTbhYValue = 0;
    public float GlobalTbhZValue = 1;
    */

    public int tbhCounter = 0;

    public List<(string tbID, DateTime timestamp)> RecentTB;

    // Start is called before the first frame update
    void Awake()
    {
        numberOfThingsCurrentlyInScene = 0;
        numberOfTBCurrentlyInScene = 0;
        RecentTB = new List<(string tbID, DateTime timestamp)>();
        handleIncomingYoloResult = false;
        handleIncomingAoAResult = false;
        handleIncomingAoASensorData = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (NewYoloResultArrived && handleIncomingYoloResult)
        {
            if (TempBBoxCoordTL != new Vector2(0, 0) && TempBBoxCoordBR != new Vector2(0, 0))
            {
                // HandleIncomingYoloResult(TempBBoxCoordTL, TempBBoxCoordBR, TempThingURI, TmpFrameTime);
                HandleIncomingYoloResultStaticDevice(TempBBoxCoordTL, TempBBoxCoordBR, TempThingURI, TmpFrameTime);

                TempBBoxCoordTL = new Vector2(0, 0);
                TempBBoxCoordBR = new Vector2(0, 0);
                TempThingURI = "";
            }
            else
            {
                StaticDeviceHandler.NewTBHfromYoloWasSuccessful = false;
            }

            NewYoloResultArrived = false;
        }
    }

    public void StopHandlingYoloResult()
    {
        handleIncomingYoloResult = false;
    }
    public void StartHandlingYoloResult()
    {
        handleIncomingYoloResult = true;
    }


    public void StopHandlingAoAResult()
    {
        handleIncomingAoAResult = false;
    }
    public void StartHandlingAoAResult()
    {
        handleIncomingAoAResult = true;
    }

    // ------------------------------------------ Incoming Data ----------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------
    

    /// <summary>
    /// This 
    /// </summary>
    /// <param name="id"></param>
    public void CompareIncomingTBIDWithIDsInScene(string id)
    {
        var isInList = false;
        for (int i = 0; i < RecentTB.Count; i++)
        {
            if (RecentTB[i].tbID == id)
            {
                RecentTB[i] = (id, DateTime.UtcNow);
                isInList = true;
            }
            else if ((DateTime.UtcNow - RecentTB[i].timestamp).TotalSeconds >= 3)
            {
                RecentTB.RemoveAt(i);
            }
        }

        if (!isInList)
        {
            RecentTB.Add((id, DateTime.UtcNow));
        }

        numberOfTBCurrentlyInScene = RecentTB.Count;

    }


    /// <summary>
    /// Handles an incoming MQTT message. Extracts the Thunderboard ID from the topic and the angle of arrival (AoA) from the message.
    /// Attaches the AoA to a (new/existing) thunderboardHandler.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="msg"></param>
    public void HandleIncomingAoAFromMQTTStaticDevice(string topic, string msg)
    {
        string log = "--- HandleIncomingAoAFromMQTTStaticDevice ---";
        if (!handleIncomingAoAResult)
        {
            log += "\nhandling AoA result is paused.";
            Debug.Log(log);
            return;
        }


        // topic looks like this: estimator/angle/ble-pd-60A423C98C93
        // the ID is just very last part: 60A423C98C93
        var subTopicFromTopic = topic.Split('/')[2];
        var idFromSubTopic = subTopicFromTopic.Split('-')[2];

        ThunderboardHandler existingTBH = ReturnThunderboardHandlerFromListIfExists(idFromSubTopic);
        if (existingTBH != null)
        {
            log += "\nincoming AoA belongs to existing TBH. Not continuing.";
            Debug.Log(log);
            return;
        }

        var newAngle = float.Parse(msg, CultureInfo.InvariantCulture);
        newAngle = (newAngle < -90f) ? -90f : (newAngle > 90f) ? 90f : newAngle;

        if (ListOfCurrentNewAoAs.ContainsKey(idFromSubTopic))
        {
            ListOfCurrentNewAoAs[idFromSubTopic].Add(newAngle);
        }
        else
        {
            ListOfCurrentNewAoAs.Add(idFromSubTopic, new List<float> { newAngle });
        }


        var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(newAngle);

        numberOfAoAsReceivedForCurrentDevices++;
        if (numberOfAoAsReceivedForCurrentDevices < 11)
        {
            AoACounterText.GetComponent<TextMeshPro>().text = $"AoA: {numberOfAoAsReceivedForCurrentDevices}/10";
        }
        // ListOfCurrentNewOffsets.Add((idFromSubTopic, newOffset));

        if (DictOfAoaOffsetLists.ContainsKey(idFromSubTopic))
        {
            DictOfAoaOffsetLists[idFromSubTopic].Add(newOffset);
        }
        else
        {
            DictOfAoaOffsetLists.Add(idFromSubTopic, new List<Vector3> { newOffset });
        }

        log += $"\nnumberOfAoAsReceivedForCurrentDevices: {numberOfAoAsReceivedForCurrentDevices}";
        log += $"\nnumberOfYoloCoordinatesReceivedForCurrentDevices: {numberOfYoloCoordinatesReceivedForCurrentDevices}";

        if ((numberOfAoAsReceivedForCurrentDevices > 10 && !expectingYolo)
            || (numberOfAoAsReceivedForCurrentDevices > 10 && numberOfYoloCoordinatesReceivedForCurrentDevices > 40))
        {
            log += "\ncalling StaticDeviceHandler";
            StaticDeviceHandler.SetTBPositionForStaticDevices();
            handleIncomingAoAResult = false;
        }
        Debug.Log(log);

    }

    public void HandleIncomingSensorDataFromMQTT(string topic, string msg)
    {
        if (!handleIncomingAoASensorData)
        {
            Debug.Log("handling AoA+MQTT result is paused.");
            return;
        }
        // topic looks like this: sensor/temp/ble-pd-60A423C98C93
        // the ID is the last part: 60A423C98C93
        var msgSplit = topic.Split('/');

        var sensorType = msgSplit[1];
        var id = msgSplit[2].Split('-')[2];
        //Debug.Log($"received new sensor data for ({id}): {msg}");

        ThunderboardHandler matchingTBH = ReturnThunderboardHandlerFromListIfExists(id);

        if (matchingTBH == null)
        {
            Debug.Log($"no matching tbh found for sensor data with id {id}");
            return;
        }
        else
        {
            if (float.TryParse(msg, out float value))
            {
                //Debug.Log("parsed float, updating");
                matchingTBH.UpdateFromSensorDataMQTTMessage(sensorType, value);
            }

        }


    }



    /// <summary>
    /// Handles an Incoming Yolo result. The two boundingbox coordinate pairs are attached to a new/existing thunderboardHandler.
    /// </summary>
    /// <param name="bboxCameraTL">top left</param>
    /// <param name="bboxCameraBR">bottom right</param>
    public void HandleIncomingYoloResultStaticDevice(Vector2 bboxCameraTL, Vector2 bboxCameraBR, string thingURI, DateTime FrameStartTime)
    {
        string log = "--- HandleIncomingYoloResultStaticDevice ---";

        log += $"\nbBoxCoordTL: {bboxCameraTL}";
        log += $"\nbBoxCoordBR: {bboxCameraBR}";

        var newOffset = PositionHandler.CalculateNewOffsetFromYoloStaticDevice(bboxCameraTL, bboxCameraBR);
        log += $"\nnewOffset: {newOffset}";

        if (newOffset.z > 0)
        {
            if (DictOfYoloOffsetLists.ContainsKey(thingURI))
            {
                DictOfYoloOffsetLists[thingURI].Add(newOffset);
            }
            else
            {
                DictOfYoloOffsetLists.Add(thingURI, new List<Vector3> { newOffset });
            }
            // ListOfCurrentNewOffsets.Add((thingURI, newOffset));
            numberOfYoloCoordinatesReceivedForCurrentDevices++;
            if (numberOfYoloCoordinatesReceivedForCurrentDevices < 41)
            {
                YoloCounterText.GetComponent<TextMeshPro>().text = $"Yolo: {numberOfYoloCoordinatesReceivedForCurrentDevices}/40";
            }

        }


        log += $"\nnumberOfYoloCoordinatesReceivedForCurrentDevice: {numberOfYoloCoordinatesReceivedForCurrentDevices}";
        log += $"\nnumberOfAoAsReceivedForCurrentDevice: {numberOfAoAsReceivedForCurrentDevices}";

        if ((numberOfYoloCoordinatesReceivedForCurrentDevices > 40 && !expectingAoA)
            || (numberOfYoloCoordinatesReceivedForCurrentDevices > 40 && numberOfAoAsReceivedForCurrentDevices > 10))
        {
            StaticDeviceHandler.SetTBPositionForStaticDevices();
            handleIncomingYoloResult = false;
        }

        log += "\n--- HandleIncomingYoloResult --- done ---";
        Debug.Log(log);
    }


    // ------------------------------------------ Helper Functions -------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------



    /// <summary>
    /// Based on 2 bounding box coordinates, returns the closest existing TunderboardHandler.
    /// </summary>
    /// <param name="bboxCameraSpaceTopLeft"></param>
    /// <param name="bboxCameraSpaceBottomRight"></param>
    /// <returns></returns>
    public ThunderboardHandler GetClosestTBHForYoloResult(Vector3 bboxCameraSpaceTopLeft, Vector3 bboxCameraSpaceBottomRight, string thingUri)
    {
        var maxDistanceFromExistingTBsCenter = 0.75f;
        //var maxDistanceFromExistingTBsCenterX = 0.5f;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForYoloResult ---";


        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.ThunderboardID}";
            log += $"\nthingUri: {thingUri}";
            log += $"\ntbh.thingUri: {tbh.ThingURI}";
            Vector3 centerBBoxInCameraSpace = Vector3.Lerp(bboxCameraSpaceTopLeft, bboxCameraSpaceBottomRight, 0.5f);
            log += $"\ncenterBBoxInCameraSpace: {centerBBoxInCameraSpace}";

            //Vector2 curTBPos = new Vector2(tbh.TBCurrentLocalOffsetInWorld.x, tbh.TBCurrentLocalOffsetInWorld.y);

            var curTBPositioninCameraSpace = PositionHandler.TBsCurrentPositionInCameraSpace(tbh);
            log += $"\ncurTBPositioninCameraSpace: {curTBPositioninCameraSpace}";


            var dist = Vector3.Distance(curTBPositioninCameraSpace, centerBBoxInCameraSpace);

            var distX = Mathf.Abs(curTBPositioninCameraSpace.x - centerBBoxInCameraSpace.x);

            Rect boundingBox = new Rect(bboxCameraSpaceTopLeft, bboxCameraSpaceBottomRight);

            var onlyReceivedAoASoFar = tbh.ThingURI == null
                                        && tbh.ThunderboardID != null
                                        // && distX < maxDistanceFromExistingTBsCenterX
                                        && dist < maxDistanceFromExistingTBsCenter;

            var receivedOnlyYoloSoFar = tbh.ThunderboardID == null
                                        && dist < maxDistanceFromExistingTBsCenter;

            var receivedAoAOrYoloOrBothSoFar = ((tbh.ThingURI != null && thingUri == tbh.ThingURI)
                                        || tbh.ThunderboardID != null)
                                        && dist < maxDistanceFromExistingTBsCenter;

            log += $"\nonlyReceivedAoASoFar: {onlyReceivedAoASoFar}";
            log += $"\nreceivedAoAOrYoloOrBothSoFar: {receivedAoAOrYoloOrBothSoFar}";
            if (onlyReceivedAoASoFar || receivedAoAOrYoloOrBothSoFar)
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                log += $"\nnew maxDistanceFromExistingTBsCenter: {dist}";
                maxDistanceFromExistingTBsCenter = dist;
            }

            log += $"\nsaved pos: {tbh.TBCurrentLocalOffsetInWorld}";
            log += $"\ndist: {dist}";

        }
        log += $"\nfinal maxDistanceFromExistingTBsCenter: {maxDistanceFromExistingTBsCenter}";
        Debug.Log(log);
        return matchingTBH;
    }

    /// <summary>
    /// Based on a position in camera space returns the closest existing TunderboardHandler.
    /// </summary>
    /// <param name="newOffsetInCameraSpace"></param>
    /// <returns>A ThunderboardHandler</returns>
    public ThunderboardHandler GetClosestTBHForNewAoA(Vector3 newOffsetInCameraSpace, float maxDistanceFromExistingTBsCenterX = 1f)
    {
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForNewAoA ---";


        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.ThunderboardID}";

            var curTBHOffset = tbh.TBCurrentLocalOffsetInWorld;
            log += $"\ncurrent saved TB offset: {curTBHOffset}";

            var curTBPositioninCameraSpace = PositionHandler.TBsCurrentPositionInCameraSpace(tbh);
            log += $"\ncurTBPositioninCameraSpace: {curTBPositioninCameraSpace}";


            // we're only interested in the X-value, since this is the only good info we get from the AoA.
            // Y and Z cannot be derived from the AoA.
            var distX = Mathf.Abs(newOffsetInCameraSpace.x - curTBPositioninCameraSpace.x);
            log += $"\ndistX: {distX}";
            var distZ = Mathf.Abs(newOffsetInCameraSpace.z - curTBPositioninCameraSpace.z);
            log += $"\ndistZ: {distZ}";

            log += $"\nstring.IsNullOrEmpty(tbh.ThunderboardID): {string.IsNullOrEmpty(tbh.ThunderboardID)}";
            log += $"\ntbh.ThunderboardID == null: {(tbh.ThunderboardID == null)}";
            // This function is only called if the newOffset (new AoA) is sent from a thunderboard that does not have an infobox yet.
            // Therefore we're only interested in the ThunderboardIDs that are null here (TBs that are generated by Yolo results without any AoA so far).
            if (distX < maxDistanceFromExistingTBsCenterX && (tbh.ThunderboardID == null))
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                maxDistanceFromExistingTBsCenterX = distX;
            }
            log += $"\nnew offset: {newOffsetInCameraSpace}";
            log += $"\nmaxDistanceFromExistingTBsCenterX: {maxDistanceFromExistingTBsCenterX}";

        }
        if (thunderboardHandlerList.Count == 0)
        {
            log += $"\nno tbh in list";
        }
        log += $"\nmaxDistanceFromExistingTBsCenterX: {maxDistanceFromExistingTBsCenterX}";
        Debug.Log(log);

        return matchingTBH;
    }


    public ThunderboardHandler ReturnThunderboardHandlerFromListIfExists(string searchedForTBHandlerID)
    {
        //Debug.Log($"searchedForTBHandlerID: {searchedForTBHandlerID}");
        ThunderboardHandler returnTBH = null;
        if (thunderboardHandlerList == null) return null;
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            //Debug.Log($"this tbh: {tbh.ThunderboardID}");
            if (tbh.ThunderboardID == null) continue;
            if (tbh.ThunderboardID == searchedForTBHandlerID)
            {
                //Debug.Log($"found matching tbh: {tbh.ThunderboardID}");
                returnTBH = tbh;
            }
        }
        return returnTBH;
    }
}
