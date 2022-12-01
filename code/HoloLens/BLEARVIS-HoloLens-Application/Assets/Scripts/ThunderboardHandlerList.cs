﻿using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ThunderboardHandlerList : MonoBehaviour
{

    public List<ThunderboardHandler> thunderboardHandlerList;
    public PositionHandler PositionHandler;
    public GameObject thunderboardInfoBoxPrefab;
    public GameObject thunderboardInfoBoxOrbitalPrefab;

    //public GameObject SetYButton;

    public float GlobalTbhYValue = 0;
    public float GlobalTbhZValue = 1;

    public Vector2 TempBBoxCoordTL = new Vector2(0, 0);
    public Vector2 TempBBoxCoordBR = new Vector2(0, 0);
    public string TempThingURI = "";

    public bool NewYoloResultArrived = false;

    public string pointMode = "world";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NewYoloResultArrived)
        {
            if (TempBBoxCoordTL != new Vector2(0, 0) && TempBBoxCoordBR != new Vector2(0, 0))
            {
                HandleIncomingYoloResult(TempBBoxCoordTL, TempBBoxCoordBR, TempThingURI);
                TempBBoxCoordTL = new Vector2(0, 0);
                TempBBoxCoordBR = new Vector2(0, 0);
                TempThingURI = "";
            }
            NewYoloResultArrived = false;
        }
    }


    // ------------------------------------------ Incoming Data ----------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------

    public void TestNewMessage(string angle)
    {
        HandleIncomingMQTTMessage("estimator/angle/ble-pd-60A423C98C93", angle);
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
    /// Handles an incoming MQTT message. Extracts the Thunderboard ID from the topic and the angle of arrival (AoA) from the message.
    /// Attaches the AoA to a (new/existing) thunderboardHandler.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="msg"></param>
    public void HandleIncomingMQTTMessage(string topic, string msg)
    {
        // topic looks like this: estimator/angle/ble-pd-60A423C98C93
        // the ID is just very last part: 60A423C98C93
        var subTopicFromTopic = topic.Split('/')[2];
        var idFromSubTopic = subTopicFromTopic.Split('-')[2];

        ThunderboardHandler tbh = ReturnThunderboardHandlerFromListIfExists(idFromSubTopic);

        
        var newAngle = float.Parse(msg, CultureInfo.InvariantCulture);
        newAngle = (newAngle < -90f) ? -90f : (newAngle > 90f) ? 90f : newAngle;


        if (tbh == null)
        {
            var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(newAngle);
            tbh = GetClosestTBHForMQTTResult(newOffset);

            if (tbh == null)
            {
                Debug.Log("TBH did not exist, creating one");
                //var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                var newPrefabInstance = Instantiate(thunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newPrefabInstance.transform.localRotation = Quaternion.identity;
                newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
                tbh = newPrefabInstance.GetComponent<ThunderboardHandler>();
                thunderboardHandlerList.Add(tbh);
            }
            //tbh.TBCurrentLocalOffsetInWorld = newOffset;
            //tbh.thunderboardPositionInWorldCoords = newPosition;
        }
       
        tbh.UpdateFromMQTTMessage(newAngle, idFromSubTopic);
    }


    /// <summary>
    /// Handles an Incoming Yolo result. The two boundingbox coordinate pairs are attached to a new/existing thunderboardHandler.
    /// </summary>
    /// <param name="bBoxCoordTL">top left</param>
    /// <param name="bBoxCoordBR">bottom right</param>
    public void HandleIncomingYoloResult(Vector2 bBoxCoordTL, Vector2 bBoxCoordBR, string thingURI)
    {
        string log = "--- HandleIncomingYoloResult ---";


        //var bBoxWorldCoordTL = Camera.main.ScreenToWorldPoint(bBoxCoordTL);
        var bBoxWorldCoordTL = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordTL);
        //var bBoxWorldCoordBR = Camera.main.ScreenToWorldPoint(bBoxCoordBR);
        var bBoxWorldCoordBR = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordBR);
        log += $"\nbBoxWorldCoordTL: {bBoxWorldCoordTL}";
        log += $"\nbBoxWorldCoordBR: {bBoxWorldCoordBR}";


        var matchingTBH = GetClosestTBHForYoloResult(bBoxWorldCoordTL, bBoxWorldCoordBR, thingURI);

        if (matchingTBH == null)
        {
            log += $"\nno matching TB found or no TB in list. creating one.";
            var newPrefabInstance = Instantiate(thunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 1), Quaternion.identity);
            newPrefabInstance.transform.localRotation = Quaternion.identity;
            newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
            matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
            matchingTBH.ThunderboardID = "undefined";
            thunderboardHandlerList.Add(matchingTBH);
        }

        matchingTBH.UpdateParametersFromYoloResult(bBoxCoordTL, bBoxCoordBR, bBoxWorldCoordTL, bBoxWorldCoordBR, thingURI);


        log += "\n--- HandleIncomingYoloResult --- done ---";
        Debug.Log(log);
    }



    // ------------------------------------------ Helper Functions -------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------

   

    /// <summary>
    /// Based on 2 bounding box coordinates, returns the closest existing TunderboardHandler.
    /// </summary>
    /// <param name="bBoxWorldCoordA"></param>
    /// <param name="bBoxWorldCoordB"></param>
    /// <returns></returns>
    public ThunderboardHandler GetClosestTBHForYoloResult(Vector3 bBoxWorldCoordA, Vector3 bBoxWorldCoordB, string thingUri)
    {
        var minDistanceFromExistingTBsCenter = Mathf.Infinity;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForYoloResult ---";

        
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.ThunderboardID}";
            Vector2 centerBBox = Vector2.Lerp(bBoxWorldCoordA, bBoxWorldCoordB, 0.5f);

            Vector2 curTBPos = new Vector2(tbh.TBCurrentLocalOffsetInWorld.x, tbh.TBCurrentLocalOffsetInWorld.y);
            
            var dist = Vector2.Distance(curTBPos, centerBBox);

            Rect boundingBox = new Rect(bBoxWorldCoordA, bBoxWorldCoordB);

            // || boundingBox.Contains(tbh.ThunderboardInfoBoxPositionInWorldCoords
            if ((!string.IsNullOrEmpty(tbh.ThingURI) && thingUri == tbh.ThingURI) || (string.IsNullOrEmpty(tbh.ThingURI) && dist < minDistanceFromExistingTBsCenter))
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                minDistanceFromExistingTBsCenter = dist;
               
            }
                
            log += $"\nsaved pos: {tbh.TBCurrentLocalOffsetInWorld}";
            log += $"\nnew interpolated pos: {centerBBox}";
            log += $"\ndist: {dist}";
            
        }
        log += $"\nminDistanceFromExistingTBsCenter: {minDistanceFromExistingTBsCenter}";
        Debug.Log(log);
        return matchingTBH;
    }

    /// <summary>
    /// Based on a position returns the closest existing TunderboardHandler.
    /// </summary>
    /// <param name="newOffset"></param>
    /// <returns></returns>
    public ThunderboardHandler GetClosestTBHForMQTTResult(Vector3 newOffset)
    {
        var minDistanceFromExistingTBsCenter = Mathf.Infinity;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForMQTTResult ---";


        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            if (tbh.ThunderboardID != null) continue;
            log += $"\nid: {tbh.ThunderboardID}";
            //Vector2 newInterpolatedPositionBBox = Vector2.Lerp(tbh.BBoxWorldTopLeft, tbh.BBoxWorldBottomRight, 0.5f);
            var curTBHOffset = tbh.TBCurrentLocalOffsetInWorld;

            //Vector2 curTBPos = new Vector2(tbh.thunderboardPositionInWorldCoords.x, tbh.thunderboardPositionInWorldCoords.y);

            var dist = Vector2.Distance(newOffset, curTBHOffset);

            //Rect boundingBox = new Rect(tbh.BBoxWorldTopLeft, tbh.BBoxWorldBottomRight);
            // || boundingBox.Contains(newOffset)

            if (dist < minDistanceFromExistingTBsCenter)
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                minDistanceFromExistingTBsCenter = dist;

            }

            log += $"\ncur offset: {tbh.TBCurrentLocalOffsetInWorld}";
            log += $"\nnew offset: {newOffset}";
            //log += $"\nnew interpolated pos: {newInterpolatedPositionBBox}";
            log += $"\ndist: {dist}";

        }
        log += $"\nminDistanceFromExistingTBsCenter: {minDistanceFromExistingTBsCenter}";
        Debug.Log(log);

        return matchingTBH;
    }


    public void SetPointMode(string newMode)
    {
        pointMode = newMode;
        Debug.Log($"PointMode has changed: {pointMode}");
    }




    

    public ThunderboardHandler ReturnThunderboardHandlerFromListIfExists(string searchedForTBHandlerID)
    {
        Debug.Log($"searchedForTBHandlerID: {searchedForTBHandlerID}");
        ThunderboardHandler returnTBH = null;
        if (thunderboardHandlerList == null) return null;
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            Debug.Log($"this tbh: {tbh.ThunderboardID}");
            if (tbh.ThunderboardID == null) return null;
            if (tbh.ThunderboardID == searchedForTBHandlerID)
            {
                Debug.Log($"found matching tbh: {tbh.ThunderboardID}");
                returnTBH = tbh;
            }
        }
        return returnTBH;
    }


    // ------------------------------------------ Experimental !!!!!! -------------------------------------------------------------
    // + not really needed. 
    // ----------------------------------------------------------------------------------------------------------------------------

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
}
