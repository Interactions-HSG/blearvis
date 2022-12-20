using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ThunderboardHandlerList : MonoBehaviour
{

    public List<ThunderboardHandler> thunderboardHandlerList;
    public PositionHandler PositionHandler;
    public GameObject thunderboardInfoBoxPrefab;
    public GameObject thunderboardInfoBoxOrbitalPrefab;

    //public GameObject SetYButton;

    public int numberOfThingsCurrentlyInScene;

    public Vector2 TempBBoxCoordTL = new Vector2(0, 0);
    public Vector2 TempBBoxCoordBR = new Vector2(0, 0);
    public string TempThingURI = "";

    public bool NewYoloResultArrived = false;

    public bool handleIncomingYoloResult =true;
    public bool handleIncomingAoAResult = true;


    public string pointMode = "world";
    public float GlobalTbhYValue = 0;
    public float GlobalTbhZValue = 1;

    public int tbhCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NewYoloResultArrived)
        {
            if (TempBBoxCoordTL != new Vector2(0, 0) && TempBBoxCoordBR != new Vector2(0, 0) && handleIncomingYoloResult)
            {
                HandleIncomingYoloResult(TempBBoxCoordTL, TempBBoxCoordBR, TempThingURI);
                TempBBoxCoordTL = new Vector2(0, 0);
                TempBBoxCoordBR = new Vector2(0, 0);
                TempThingURI = "";
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

        
        var newAngle = float.Parse(msg, CultureInfo.InvariantCulture);
        newAngle = (newAngle < -90f) ? -90f : (newAngle > 90f) ? 90f : newAngle;


        if (matchingTBH == null)
        {
            var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(newAngle);
            matchingTBH = GetClosestTBHForNewAoA(newOffset);

            if (matchingTBH == null)
            {
                Debug.Log("TBH did not exist, creating one");
                tbhCounter++;
                //var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                var newPrefabInstance = Instantiate(thunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newPrefabInstance.transform.localRotation = Quaternion.identity;
                newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
                matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
                matchingTBH.ThunderboardInfoBox.SetActive(false);
                matchingTBH.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = new Vector3(0,0,1);
                matchingTBH.SerialNumber = tbhCounter;
                thunderboardHandlerList.Add(matchingTBH);
                
            }
            //tbh.TBCurrentLocalOffsetInWorld = newOffset;
            //tbh.thunderboardPositionInWorldCoords = newPosition;
        }
       
        matchingTBH.UpdateFromAoAMQTTMessage(newAngle, idFromSubTopic);
    }


    public void HandleIncomingSensorDataFromMQTT(string topic, string msg)
    {
        if (!handleIncomingAoAResult)
        {
            Debug.Log("handling AoA+MQTT result is paused.");
            return;
        }
        // topic looks like this: sensor/temp/ble-pd-60A423C98C93
        // the ID is the last part: 60A423C98C93
        var msgSplit = topic.Split('/');

        var sensorType = msgSplit[1];
        var id = msgSplit[2].Split('-')[2];
        Debug.Log($"received new sensor data for ({id}): {msg}");

        ThunderboardHandler matchingTBH = ReturnThunderboardHandlerFromListIfExists(id);

        if (matchingTBH == null)
        {
            Debug.Log("no matching tbh found for sensor data");
            return;
        } else
        {
            if (float.TryParse(msg, out float value))
            {
                Debug.Log("parsed float, updating");
                matchingTBH.UpdateFromSensorDataMQTTMessage(sensorType, value);
            }
            
        }


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
        var bBoxCameraSpaceTL = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordTL);
        //var bBoxWorldCoordBR = Camera.main.ScreenToWorldPoint(bBoxCoordBR);
        var bBoxCamerSpaceBR = PositionHandler.ScreenToCameraPointThroughRaycast(bBoxCoordBR);
        log += $"\nbBoxWorldCoordTL: {bBoxCameraSpaceTL}";
        log += $"\nbBoxWorldCoordBR: {bBoxCamerSpaceBR}";

        if (bBoxCameraSpaceTL.sqrMagnitude == 0 && bBoxCamerSpaceBR.sqrMagnitude == 0)
        {
            log += "\nboth bboxInCameraSpace vectors are (0,0,0). Not continuing.";
            Debug.Log(log);
            return;
        }


        var matchingTBH = GetClosestTBHForYoloResult(bBoxCameraSpaceTL, bBoxCamerSpaceBR, thingURI);

        // only create a new InfoBox when there are currently less InfoBoxes than Things in the scene
        var lessTBHsThanThingsInScene = (thunderboardHandlerList.Count < numberOfThingsCurrentlyInScene);
        log += $"\nthunderboardHandlerList.Count: {thunderboardHandlerList.Count}";
        log += $"\nnumberOfThingsCurrentlyInScene: {numberOfThingsCurrentlyInScene}";

        if (matchingTBH == null && lessTBHsThanThingsInScene)
        {
            log += $"\nno matching TB found or no TB in list. creating one.";
            var newPrefabInstance = Instantiate(thunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newPrefabInstance.transform.localRotation = Quaternion.identity;
            newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
            matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
            matchingTBH.ThunderboardInfoBox.SetActive(false);
            matchingTBH.ThunderboardID = "undefined";
            matchingTBH.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = new Vector3(0, 0, 1);
            matchingTBH.SerialNumber = tbhCounter;
            thunderboardHandlerList.Add(matchingTBH);
            log += $"\nadded new tbh: {thunderboardHandlerList[thunderboardHandlerList.Count - 1]}";
            tbhCounter++;
        }
        if (matchingTBH != null)
        {
            log += "\nmatchinTBH != null";
            matchingTBH.UpdateParametersFromYoloResult(bBoxCoordTL, bBoxCoordBR, bBoxCameraSpaceTL, bBoxCamerSpaceBR, thingURI);
        } else
        {
            log += $"\nno matching TBH or not lessTBHsThanThingsInScene: {lessTBHsThanThingsInScene}";
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
        var maxDistanceFromExistingTBsCenter = 2f;
        //var maxDistanceFromExistingTBsCenterX = 0.5f;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForYoloResult ---";

        
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.ThunderboardID}";
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

            var receivedYoloOrBothSoFar = tbh.ThingURI != null
                                        && thingUri == tbh.ThingURI
                                        //&& tbh.ThunderboardID == null
                                        && dist < maxDistanceFromExistingTBsCenter;

            var receivedBothAlready = tbh.ThingURI != null
                                        && thingUri == tbh.ThingURI
                                        && tbh.ThunderboardID != null
                                        //&& distX < maxDistanceFromExistingTBsCenterX
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
            if (onlyReceivedAoASoFar || receivedYoloOrBothSoFar)
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
    public ThunderboardHandler GetClosestTBHForNewAoA(Vector3 newOffsetInCameraSpace)
    {
        var maxDistanceFromExistingTBsCenterX = 0.5f;
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
            log += $"\ndist: {distX}";
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
            log += $"\nminDistanceFromExistingTBsCenterX: {maxDistanceFromExistingTBsCenterX}";
            //log += $"\nnew interpolated pos: {newInterpolatedPositionBBox}";

        }
        if (thunderboardHandlerList.Count == 0)
        {
            log += $"\nno tbh in list";
        }
        log += $"\nminDistanceFromExistingTBsCenterX: {maxDistanceFromExistingTBsCenterX}";
        Debug.Log(log);

        return matchingTBH;
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


    // ------------------------------------------ DEPRECATED !!!!!! -------------------------------------------------------------
    // + not really needed. 
    // ----------------------------------------------------------------------------------------------------------------------------


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
}
