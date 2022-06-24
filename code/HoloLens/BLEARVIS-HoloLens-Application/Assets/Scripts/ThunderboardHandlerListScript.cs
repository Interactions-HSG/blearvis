using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ThunderboardHandlerListScript : MonoBehaviour
{

    public List<ThunderboardHandler> thunderboardHandlerList;
    public GameObject thunderboardInfoBoxPrefab;

    public GameObject SetYButton;

    public float GlobalTbhYValue = 0;
    public float GlobalTbhZValue = 1;

    public Vector2 TempBBoxCoordTL = new Vector2(0, 0);
    public Vector2 TempBBoxCoordBR = new Vector2(0, 0);
    public string TempThingURI = "";

    public bool NewYoloResultArrived = false;

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
        newAngle = (newAngle < -90) ? -90f : (newAngle > 90) ? 90f : newAngle;


        if (tbh == null)
        {
            var newPosition = CalculatePositionFromAngle(newAngle);
            tbh = GetClosestTBHForMQTTResult(newPosition);

            if (tbh == null)
            {
                Debug.Log("TBH did not exist, creating one");
                var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                tbh = newPrefabInstance.GetComponent<ThunderboardHandler>();
                thunderboardHandlerList.Add(tbh);
            }
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


        var bBoxWorldCoordTL = Camera.main.ScreenToWorldPoint(bBoxCoordTL);
        var bBoxWorldCoordBR = Camera.main.ScreenToWorldPoint(bBoxCoordBR);
        log += $"\nbBoxWorldCoordA: {bBoxWorldCoordTL}";
        log += $"\nbBoxWorldCoordB: {bBoxWorldCoordBR}";


        var matchingTBH = GetClosestTBHForYoloResult(bBoxWorldCoordTL, bBoxWorldCoordBR, thingURI);

        if (matchingTBH == null)
        {
            log += $"\nno matching TB found or no TB in list. creating one.";
            var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>calculated position in world space</returns>
    public Vector3 CalculatePositionFromAngle(float angle)
    {

        var newPosition = new Vector3(0, 0, 1);
        string log = "--- CalculatePositionFromAngle --";
        // tan(angle) = x/y
        // -> x = tan(angle) * y
        // z = 1
        // a = angleFloat
        // b = 90°
        /*
         *     x
         *  ________
         *  |b     /
         *  |     /
         *  |    /          
         * z|   /
         *  |  /
         *  |a/
         *  |/ 
         */
        // only x-value:
        // negate because +90 is on left and -90 is on right of receiver
        //var newX = Mathf.Tan(Mathf.Deg2Rad * angle);

        // x- and z-value. keeps the info box on a circle around the HL2.
        var newXCircle = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
        var newZCircle = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));

        newPosition.x = newXCircle;
        newPosition.z = newZCircle;
        log += $"newPosition: {newPosition}";

        var curCameraPosition = Camera.main.transform.position;
        log += $"curCameraPosition: {curCameraPosition}";

        var newPosPlusCam = newPosition + curCameraPosition;
        log += $"newPosition +camPos: {newPosPlusCam}";

        log += "--- CalculatePositionFromAngle --- end ---";
        Debug.Log(log);
        return newPosition;
    }

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

            Vector2 curTBPos = new Vector2(tbh.ThunderboardInfoBoxPositionInWorldCoords.x, tbh.ThunderboardInfoBoxPositionInWorldCoords.y);
            
            var dist = Vector2.Distance(curTBPos, centerBBox);

            Rect boundingBox = new Rect(bBoxWorldCoordA, bBoxWorldCoordB);

            // || boundingBox.Contains(tbh.ThunderboardInfoBoxPositionInWorldCoords
            if (thingUri == tbh.ThingURI && (dist < minDistanceFromExistingTBsCenter))
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                minDistanceFromExistingTBsCenter = dist;
               
            }
                
            log += $"\nsaved pos: {tbh.ThunderboardInfoBoxPositionInWorldCoords}";
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
    /// <param name="position"></param>
    /// <param name="threshold">Optionally. Might not be necessary.</param>
    /// <returns></returns>
    public ThunderboardHandler GetClosestTBHForMQTTResult(Vector3 newPosition)
    {
        var minDistanceFromExistingTBsCenter = Mathf.Infinity;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBHForMQTTResult ---";


        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.ThunderboardID}";
            Vector2 newInterpolatedPositionBBox = Vector2.Lerp(tbh.BBoxWorldCoordTL, tbh.BBoxWorldCoordBR, 0.5f);

            //Vector2 curTBPos = new Vector2(tbh.thunderboardPositionInWorldCoords.x, tbh.thunderboardPositionInWorldCoords.y);

            var dist = Vector2.Distance(newPosition, newInterpolatedPositionBBox);

            Rect boundingBox = new Rect(tbh.BBoxWorldCoordTL, tbh.BBoxWorldCoordBR);

            if (dist < minDistanceFromExistingTBsCenter || boundingBox.Contains(newPosition))
            {
                log += $"\nfound new closest TBH";
                matchingTBH = tbh;
                minDistanceFromExistingTBsCenter = dist;

            }

            log += $"\nsaved pos: {tbh.ThunderboardInfoBoxPositionInWorldCoords}";
            log += $"\nnew interpolated pos: {newInterpolatedPositionBBox}";
            log += $"\ndist: {dist}";

        }
        log += $"\nminDistanceFromExistingTBsCenter: {minDistanceFromExistingTBsCenter}";
        Debug.Log(log);

        return matchingTBH;
    }



    /// <summary>
    /// Converts a screen space vector2 to a world space vector2 by raycasting.
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public Vector3 ScreenToWorldPointRaycast(Vector2 newPos)
    {
        var tc = Camera.main.transform;
        string log = "--- ScreenToWorldPointRaycast ---";

        //1280x720
        // following this paper "Augmented Reality Controlled Smart Wheelchair Using Dynamic Signifiers for Affordance Representation"
        // chacon-quesada2019
        // https://doi.org/10.1109/IROS40897.2019.8968290
        var newX = (2 * newPos.x / 1280f) - 1;
        var newY = (-2 * newPos.y / 720) + 1;
        // pixel in normalised device coordinates (ndc)
        var pndc = new Vector4(newX, newY, 1, 1);
        log += $"\nin normalised device coordinates: {pndc}";
        var Mproj = Camera.main.previousViewProjectionMatrix;

        // pixel in the camera coordinate system
        var pc =  Mproj.inverse.MultiplyVector(pndc);
        log += $"\nin camera coordinates: {pc}";

        var Mworld = Camera.main.cameraToWorldMatrix;
        // pixel in application coordinate system (aka world coordinates)
        var pworld = Mworld.MultiplyVector(pc);
        log += $"\nin app coordinates: {pworld}";

        Vector3 newPosinWorldCoord = Camera.main.ScreenToWorldPoint(newPos);
        log += $"\nresult from ScreenToWorldPoint: {newPosinWorldCoord}";

        var curCameraPosition = Camera.main.transform.position;
        log += $"\ncurCameraPosition: {curCameraPosition}";

        var plusCamPos = newPosinWorldCoord + curCameraPosition;
        log += $"\nplusCamPos: {plusCamPos}";


        Ray ray = Camera.main.ScreenPointToRay(newPos);
        if (Physics.Raycast(newPosinWorldCoord, tc.TransformDirection(Vector3.forward), out RaycastHit hit))
        //if (Physics.Raycast(pworld, Vector3.forward, out RaycastHit hit))
        //if (Physics.Raycast(ray, out RaycastHit hit))
        {
            log += "\nhit point";
            log += "\n--- ScreenToWorldPointRaycast --- end ---";
            
            Debug.Log(log);
            return hit.point;
        }
        /*
            if (xyPlane.Raycast(mouseRay, out float distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            return hitPoint;
        }
        */
        log += "\n--- ScreenToWorldPointRaycast --- end ---";
        Debug.Log(log);
        return new Vector3(0, 0, 0);

    }

    public ThunderboardHandler ReturnThunderboardHandlerFromListIfExists(string correctTBHandlerID)
    {
        Debug.Log($"correctTBHandlerID: {correctTBHandlerID}");
        ThunderboardHandler returnValue = null;
        if (thunderboardHandlerList == null) return null;
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            Debug.Log($"this tbh: {tbh.ThunderboardID}");
            if (tbh.ThunderboardID == null) return null;
            if (tbh.ThunderboardID == correctTBHandlerID)
            {
                returnValue = tbh;
            }
        }
        return returnValue;
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
