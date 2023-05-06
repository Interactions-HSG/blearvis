﻿using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
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
    public  Dictionary<string, List<Vector3>> DictOfAoaOffsetLists;
    public Dictionary<string, List<Vector3>> DictOfYoloOffsetLists;
    public Dictionary<string, List<float>> ListOfCurrentNewAoAs;

    [Header("Values from HTTPListener")]
    public Vector2 TempBBoxCoordTL = new Vector2(0, 0);
    public Vector2 TempBBoxCoordBR = new Vector2(0, 0);
    public string TempThingURI = "";
    public DateTime TmpFrameTime;
    public bool NewYoloResultArrived = false;

    [Header("If incoming results should be handled")]
    public bool handleIncomingYoloResult =false;
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
        handleIncomingYoloResult = true;
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
            } else
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

    public void TestNewMessage(string angle)
    {
        HandleIncomingAoAFromMQTT("estimator/angle/ble-pd-60A423C98C93", angle);
    }

    public void TestRaycast(GameObject g)
    {
        var pos = g.GetComponent<Transform>().position;
        var newPos = Camera.main.WorldToScreenPoint(pos);
        var worldAgain = PositionHandler.ScreenToCameraPointThroughRaycast(newPos);
        var log = $"WorldAgain: {worldAgain}";
            
        var oworld = Camera.main.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(newPos);
        Debug.DrawRay(oworld, pos);
        if (Physics.Raycast(oworld, pos, out RaycastHit hit))
        {
            log = $"\nhit point: {hit.point}";
        }
        Debug.Log(log);


    }

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
            } else if ((DateTime.UtcNow - RecentTB[i].timestamp).TotalSeconds >= 3) {
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
    public void HandleIncomingAoAFromMQTT(string topic, string msg)
    {
        if (!handleIncomingAoAResult)
        {
            Debug.Log("handling AoA result is paused.");
            return;
        }
        // topic looks like this: estimator/angle/ble-pd-60A423C98C93
        // the ID is just very last part: 60A423C98C93
        var subTopicFromTopic = topic.Split('/')[2];
        var idFromSubTopic = subTopicFromTopic.Split('-')[2];

        ThunderboardHandler matchingTBH = ReturnThunderboardHandlerFromListIfExists(idFromSubTopic);
        CompareIncomingTBIDWithIDsInScene(idFromSubTopic);


        var newAngle = float.Parse(msg, CultureInfo.InvariantCulture);
        newAngle = (newAngle < -90f) ? -90f : (newAngle > 90f) ? 90f : newAngle;


        if (matchingTBH == null)
        {
            var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(newAngle);
            matchingTBH = GetClosestTBHForNewAoA(newOffset);

            if (matchingTBH == null && thunderboardHandlerList.Count < 2)
            {
                Debug.Log($"matchingTBH is null: {matchingTBH == null} for new id: {idFromSubTopic}");
                Debug.Log("TBH did not exist, creating one");
                tbhCounter++;
                //var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                var newPrefabInstance = Instantiate(ThunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newPrefabInstance.transform.localRotation = Quaternion.identity;
                newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
                matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
                matchingTBH.ThunderboardInfoBox.SetActive(true);
                newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
                matchingTBH.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = newOffset;
                matchingTBH.SerialNumber = tbhCounter;
                
                thunderboardHandlerList.Add(matchingTBH);
                
            } else
            {
                Debug.Log($"(2) matchingTBH is null: {matchingTBH == null} for new id: {idFromSubTopic}");
            }
            if (idFromSubTopic == "60A423C98BF1")
            {
                Debug.Log("matched with spock");
                // Spock with soil condition checker
                matchingTBH.thingIP = "10.2.2.157";
            }
            else
            {
                Debug.Log("matched with uhura");
                // Uhura without soil condition checker
                matchingTBH.thingIP = "10.2.2.240";
            }
            //tbh.TBCurrentLocalOffsetInWorld = newOffset;
            //tbh.thunderboardPositionInWorldCoords = newPosition;
        }
      


        matchingTBH.UpdateFromAoAMQTTMessage(newAngle, idFromSubTopic);
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
        } else
        {
            ListOfCurrentNewAoAs.Add(idFromSubTopic, new List<float> { newAngle });
        }

        
        var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(newAngle);

        numberOfAoAsReceivedForCurrentDevices++;
        AoACounterText.GetComponent<TextMeshPro>().text = $"AoA: {numberOfAoAsReceivedForCurrentDevices}/5";
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

        if ((numberOfAoAsReceivedForCurrentDevices > 15 && !expectingYolo) 
            || (numberOfAoAsReceivedForCurrentDevices > 15 && numberOfYoloCoordinatesReceivedForCurrentDevices > 15))
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
        } else
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
        /// <param name="bBoxCoordTL">top left</param>
        /// <param name="bBoxCoordBR">bottom right</param>
        public void HandleIncomingYoloResult(Vector2 bBoxCoordTL, Vector2 bBoxCoordBR, string thingURI, DateTime FrameStartTime)
    {
        string log = "--- HandleIncomingYoloResult ---";


        //var bBoxWorldCoordTL = Camera.main.ScreenToWorldPoint(bBoxCoordTL);
        var bBoxCameraSpaceTL = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordTL);
        //var bBoxWorldCoordBR = Camera.main.ScreenToWorldPoint(bBoxCoordBR);
        var bBoxCameraSpaceBR = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordBR);
        log += $"\nbBoxWorldCoordTL: {bBoxCameraSpaceTL}";
        log += $"\nbBoxWorldCoordBR: {bBoxCameraSpaceBR}";

        if (bBoxCameraSpaceTL.sqrMagnitude == 0 && bBoxCameraSpaceBR.sqrMagnitude == 0)
        {
            log += "\nboth bboxInCameraSpace vectors are (0,0,0). Not continuing.";
            Debug.Log(log);
            return;
        }


        var matchingTBH = GetClosestTBHForYoloResult(bBoxCameraSpaceTL, bBoxCameraSpaceBR, thingURI);

        // only create a new InfoBox when there are currently less InfoBoxes than Things in the scene
        var maxNumberOfThingsInScene = Mathf.Max(numberOfTBCurrentlyInScene, numberOfThingsCurrentlyInScene);
        var lessTBHsThanThingsInScene = (thunderboardHandlerList.Count < numberOfThingsCurrentlyInScene);
        log += $"\nthunderboardHandlerList.Count: {thunderboardHandlerList.Count}";
        log += $"\nnumberOfThingsCurrentlyInScene: {numberOfThingsCurrentlyInScene}";
        log += $"\numberOfTBCurrentlyInScene: {numberOfTBCurrentlyInScene}";
        log += $"\nmaxNumberOfThingsInScene: {maxNumberOfThingsInScene}";

        if (matchingTBH == null && lessTBHsThanThingsInScene)
        {
            log += $"\nno matching TB found or no TB in list. creating one.";
            var newPrefabInstance = Instantiate(ThunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 1), Quaternion.identity);
            newPrefabInstance.transform.localRotation = Quaternion.identity;
            newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
            matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
            matchingTBH.ThunderboardID = "undefined";
            // var newOffset = Vector3.Lerp(bBoxCameraSpaceTL, bBoxCameraSpaceBR, 0.5f);
            // newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
            // matchingTBH.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = newOffset;
            matchingTBH.SerialNumber = tbhCounter;
            // matchingTBH.ThunderboardInfoBox.SetActive(true);
            thunderboardHandlerList.Add(matchingTBH);
            log += $"\nadded new tbh: {thunderboardHandlerList[thunderboardHandlerList.Count - 1]}";
            tbhCounter++;
        }
        log += $"\nmatchingTBH: {matchingTBH}";
        // log += $"\nmatchingTBH ID: {matchingTBH.ThunderboardID}";
        //log += $"\nmatchingTBH URI: {matchingTBH.ThingURI}";

        if (matchingTBH != null)
        {
            log += "\nmatchinTBH != null";
            matchingTBH.UpdateParametersFromYoloResult(bBoxCoordTL, bBoxCoordBR, bBoxCameraSpaceTL, bBoxCameraSpaceBR, thingURI, FrameStartTime);
        } else
        {
            log += $"\nno matching TBH or not lessTBHsThanThingsInScene: {lessTBHsThanThingsInScene}";
        }
        


        log += "\n--- HandleIncomingYoloResult --- done ---";
        Debug.Log(log);
    }

    /// <summary>
    /// Handles an Incoming Yolo result. The two boundingbox coordinate pairs are attached to a new/existing thunderboardHandler.
    /// </summary>
    /// <param name="bboxCameraTL">top left</param>
    /// <param name="bboxCameraBR">bottom right</param>
    public void HandleIncomingYoloResultStaticDevice(Vector2 bboxCameraTL, Vector2 bboxCameraBR, string thingURI, DateTime FrameStartTime)
    {
        string log = "--- HandleIncomingYoloResultStaticDevice ---";

        /*
        //var bBoxWorldCoordTL = Camera.main.ScreenToWorldPoint(bBoxCoordTL);
        var bBoxCameraSpaceTL = PositionHandler.ScreenToCameraPointThroughRaycast(bboxCameraTL);
        //var bBoxWorldCoordBR = Camera.main.ScreenToWorldPoint(bBoxCoordBR);
        var bBoxCameraSpaceBR = PositionHandler.ScreenToCameraPointThroughRaycast(bboxCameraBR);
        log += $"\nbBoxWorldCoordTL: {bBoxCameraSpaceTL}";
        log += $"\nbBoxWorldCoordBR: {bBoxCameraSpaceBR}";

        if (bBoxCameraSpaceTL.sqrMagnitude == 0 && bBoxCameraSpaceBR.sqrMagnitude == 0)
        {
            log += "\nboth bboxInCameraSpace vectors are (0,0,0). Not continuing.";
            StaticDeviceHandler.NewTBHfromYoloWasSuccessful = false;
            Debug.Log(log);
            return;
        }
        //*/

        log += $"\nbBoxCoordTL: {bboxCameraTL}";
        log += $"\nbBoxCoordBR: {bboxCameraBR}";

        var newOffset = PositionHandler.CalculateNewOffsetFromYoloStaticDevice(bboxCameraTL, bboxCameraBR);
        log += $"\nnewOffset: {newOffset}";
        

        //if ((newOffset.x >= 0.01 || newOffset.x <= -0.01) 
        //    && (newOffset.y >= 0.01 || newOffset.y <= -0.01)
          //  && (newOffset.z >= 0.5))
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
            YoloCounterText.GetComponent<TextMeshPro>().text = $"Yolo: {numberOfYoloCoordinatesReceivedForCurrentDevices}/20";
        }


        log += $"\nnumberOfYoloCoordinatesReceivedForCurrentDevice: {numberOfYoloCoordinatesReceivedForCurrentDevices}";
        log += $"\nnumberOfAoAsReceivedForCurrentDevice: {numberOfAoAsReceivedForCurrentDevices}";

        if ((numberOfYoloCoordinatesReceivedForCurrentDevices > 20 && !expectingAoA)
            || (numberOfYoloCoordinatesReceivedForCurrentDevices > 20 && numberOfAoAsReceivedForCurrentDevices > 10))
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

            var receivedOnlyYoloSoFar =  tbh.ThunderboardID == null
                                        && dist < maxDistanceFromExistingTBsCenter;

            var receivedAoAOrYoloOrBothSoFar = ((tbh.ThingURI != null && thingUri == tbh.ThingURI)
                                        || tbh.ThunderboardID != null)
                                        && dist < maxDistanceFromExistingTBsCenter;
            /*
            var onlyReceivedAoASoFar = (string.IsNullOrEmpty(tbh.ThingURI) || tbh.ThingURI == null)
                                        && (!string.IsNullOrEmpty(tbh.ThunderboardID) || tbh.ThunderboardID != null) 
                                        && distX < maxDistanceFromExistingTBsCenterX;

            var onlyReceivedYoloSoFar = (!string.IsNullOrEmpty(tbh.ThingURI) || tbh.ThingURI != null)
                                        && thingUri == tbh.ThingURI 
                                        && (string.IsNullOrEmpty(tbh.ThunderboardID) || tbh.ThunderboardID == null)
                                        && dist < maxDistanceFromExistingTBsCenter;

            var receivedBothAlready = (!string.IsNullOrEmpty(tbh.ThingURI) || tbh.ThingURI != null)
                                        && thingUri == tbh.ThingURI
                                        && (!string.IsNullOrEmpty(tbh.ThunderboardID) || tbh.ThunderboardID != null)
                                        && distX < maxDistanceFromExistingTBsCenterX
                                        && dist < maxDistanceFromExistingTBsCenter;
            //*/
            // || boundingBox.Contains(tbh.ThunderboardInfoBoxPositionInWorldCoords
            //if (((!string.IsNullOrEmpty(tbh.ThingURI) && thingUri == tbh.ThingURI) || string.IsNullOrEmpty(tbh.ThingURI)) 
            //   && dist < maxDistanceFromExistingTBsCenter)
            //if (onlyReceivedAoASoFar || receivedYoloOrBothSoFar || receivedBothAlready)
            log += $"\nonlyReceivedAoASoFar: {onlyReceivedAoASoFar}";
            log += $"\nreceivedAoAOrYoloOrBothSoFar: {receivedAoAOrYoloOrBothSoFar}";
            if (onlyReceivedAoASoFar || receivedAoAOrYoloOrBothSoFar)
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                log += $"\nnew maxDistanceFromExistingTBsCenter: {dist}";
                maxDistanceFromExistingTBsCenter = dist;

                /*
                if (onlyReceivedAoASoFar || receivedBothAlready)
                {
                    log += $"\nnew maxDistanceFromExistingTBsCenterX: {distX}";
                    maxDistanceFromExistingTBsCenterX = distX;
                }
                if (receivedYoloOrBothSoFar || receivedBothAlready)
                {
                    log += $"\nnew maxDistanceFromExistingTBsCenter: {dist}";
                    maxDistanceFromExistingTBsCenter = dist;
                }
                //*/
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
        //var maxDistanceFromExistingTBsCenterX = 1.5f;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForNewAoA ---";


        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            //if (tbh.ThunderboardID != null) continue;
            log += $"\nid: {tbh.ThunderboardID}";
            //Vector2 newInterpolatedPositionBBox = Vector2.Lerp(tbh.BBoxWorldTopLeft, tbh.BBoxWorldBottomRight, 0.5f);
            var curTBHOffset = tbh.TBCurrentLocalOffsetInWorld;
            log += $"\ncurrent saved TB offset: {curTBHOffset}";

            var curTBPositioninCameraSpace = PositionHandler.TBsCurrentPositionInCameraSpace(tbh);
            log += $"\ncurTBPositioninCameraSpace: {curTBPositioninCameraSpace}";

            //Vector2 curTBPos = new Vector2(tbh.thunderboardPositionInWorldCoords.x, tbh.thunderboardPositionInWorldCoords.y);

            // we're only interested in the X-value, since this is the only good info we get from the AoA.
            // Y and Z cannot be derived from the AoA.
            var distX = Mathf.Abs(newOffsetInCameraSpace.x - curTBPositioninCameraSpace.x);
            log += $"\ndistX: {distX}";
            var distZ = Mathf.Abs(newOffsetInCameraSpace.z - curTBPositioninCameraSpace.z);
            log += $"\ndistZ: {distZ}";
            //Rect boundingBox = new Rect(tbh.BBoxWorldTopLeft, tbh.BBoxWorldBottomRight);
            // || boundingBox.Contains(newOffset)

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
            //log += $"\nnew interpolated pos: {newInterpolatedPositionBBox}";

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


    // ------------------------------------------ DEPRECATED !!!!!! -------------------------------------------------------------
    // + not really needed. 
    // ----------------------------------------------------------------------------------------------------------------------------

    /*
    public void SetPointMode(string newMode)
    {
        pointMode = newMode;
        Debug.Log($"PointMode has changed: {pointMode}");
    }


    public void SetGlobalYValueForAllThunderboards()
    {
        string log = "--- SetGlobalYValueForAllThunderboards ---";
        var firstTBHandler = thunderboardHandlerList[0];
        if (firstTBHandler == null)
        {
            log += "\nNo TBH in list! Could not set value!";
            Debug.Log(log);
            return;
        }
        var currentY = firstTBHandler.GetComponent<Transform>().position.y;
        GlobalTbhYValue = currentY;
        log += "\nCould set the new global Y value successfully.";
        Debug.Log(log);
        UpdateAllTbhPositions();
       
    }

    public void SetGlobalZValueForAllThunderboards()
    {
        string log = "--- SetGlobalZValueForAllThunderboards ---";
        var firstTBHandler = thunderboardHandlerList[0];
        if (firstTBHandler == null)
        {
            log += "\nNo TBH in list! Could not set Z value!";
            Debug.Log(log);
            return;
        }
        var currentZ = firstTBHandler.GetComponent<Transform>().position.z;
        GlobalTbhZValue = currentZ;
        log += "\nCould set the new global Z value successfully.";
        Debug.Log(log);
        UpdateAllTbhPositions();
    }

    public void UpdateAllTbhPositions()
    {
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            var curTransform = tbh.GetComponent<Transform>();
            var curPosition = curTransform.position;

            var newPosition = new Vector3(curPosition.x, GlobalTbhYValue, GlobalTbhZValue);
            curTransform.position = newPosition;
        }
    }
    //*/
}
