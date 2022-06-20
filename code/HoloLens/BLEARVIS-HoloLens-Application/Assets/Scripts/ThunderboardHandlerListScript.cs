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

    public Vector2 TempBBoxCoordA = new Vector2(0, 0);
    public Vector2 TempBBoxCoordB = new Vector2(0, 0);

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
            if (TempBBoxCoordA != new Vector2(0, 0) && TempBBoxCoordB != new Vector2(0, 0))
            {
                HandleIncomingYoloResult(TempBBoxCoordA, TempBBoxCoordB);
                TempBBoxCoordA = new Vector2(0, 0);
                TempBBoxCoordB = new Vector2(0, 0);
            }
            NewYoloResultArrived = false;
        }
    }

    public ThunderboardHandler ReturnThunderboardHandlerFromListIfExists(string correctTBHandlerID)
    {
        Debug.Log($"correctTBHandlerID: {correctTBHandlerID}");
        ThunderboardHandler returnValue = null;
        if (thunderboardHandlerList == null) return null;
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            Debug.Log($"this tbh: {tbh.thunderboardID}");
            if (tbh.thunderboardID == null) return null;
            if (tbh.thunderboardID == correctTBHandlerID)
            {
                returnValue = tbh;
            }
        }
        return returnValue;
    }



    // ------------------------------------------ Incoming Data ----------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------


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
            var newPosition = GetPositionFromAngle(newAngle);
            tbh = GetClosestTBH(newPosition);
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


    public void HandleIncomingYoloResult(Vector2 bBoxCoordA, Vector2 bBoxCoordB)
    {
        string log = "--- HandleIncomingYoloResult ---";


        var bBoxWorldCoordA = ScreenToWorldPointRaycast(bBoxCoordA);
        var bBoxWorldCoordB = ScreenToWorldPointRaycast(bBoxCoordB);

        Vector2 newInterpolatedPosition = Vector2.Lerp(bBoxCoordA, bBoxCoordB, 0.5f);
        var interpolatedWorldPosition = ScreenToWorldPointRaycast(newInterpolatedPosition);

        var matchingTBH = GetClosestTBH(interpolatedWorldPosition);

        if (matchingTBH == null)
        {
            log += $"\nno matching TB found or no TB in list.";
            log += "\nTBH did not exist, creating one";
            var newPrefabInstance = Instantiate(thunderboardInfoBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            matchingTBH = newPrefabInstance.GetComponent<ThunderboardHandler>();
            matchingTBH.thunderboardID = "undefined";
            thunderboardHandlerList.Add(matchingTBH);
        }

        matchingTBH.UpdateCoordinatesFromYolo(bBoxCoordA, bBoxCoordB, bBoxWorldCoordA, bBoxWorldCoordB);


        log += "\n--- HandleIncomingYoloResult --- done ---";
        Debug.Log(log);
    }



    // ------------------------------------------ Helper Functions -------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------------

    private Vector3 GetPositionFromAngle(float angle)
    {

        var newPosition = new Vector3(0, 0, 1);

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
        var newX = Mathf.Tan(Mathf.Deg2Rad * angle);
        //var newXCircle = Mathf.Cos(Mathf.Deg2Rad * (angleFloat + 90));
        //var newZCircle = Mathf.Sin(Mathf.Deg2Rad * (angleFloat + 90));


        // negate because +90 is on left and -90 is on right of receiver
        newPosition.x = -newX;

        return newPosition;
    }

    public ThunderboardHandler GetClosestTBH(Vector3 position, int threshold = 0)
    {
        var minDistanceFromExistingTBs = Mathf.Infinity;
        ThunderboardHandler matchingTBH = null;
        string log = "--- GetClosestTBH ---";
        foreach (ThunderboardHandler tbh in thunderboardHandlerList)
        {
            log += $"\nid: {tbh.thunderboardID}";
            var dist = Mathf.Abs(tbh.thunderboardPositionInWorldCoords.x - position.x);

            log += $"\nsaved pos: {tbh.thunderboardPositionInWorldCoords}";
            log += $"\nnew pos: {position}";
            log += $"\ndist: {dist}";
            if (dist < minDistanceFromExistingTBs)
            {
                matchingTBH = tbh;
                minDistanceFromExistingTBs = dist;
            }
        }
        log += $"\nminDistanceFromExistingTBs: {minDistanceFromExistingTBs}";
        Debug.Log(log);

        if (minDistanceFromExistingTBs > threshold)
        {
            return matchingTBH;
        }
        else
        {
            return null;
        }


    }

    public Vector3 ScreenToWorldPointRaycast(Vector2 sp)
    {


        var tc = Camera.main.transform;
        Vector3 newPosinWorldCoord = Camera.main.ScreenToWorldPoint(sp);
        newPosinWorldCoord.z = 0;

        RaycastHit hit;

        if (Physics.Raycast(newPosinWorldCoord, tc.TransformDirection(Vector3.forward), out hit))
        {
            return hit.point;
        }
        /*
            if (xyPlane.Raycast(mouseRay, out float distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            return hitPoint;
        }
        */
        return new Vector3(0, 0, 0);

    }



    // ------------------------------------------ Experimental !!!!!! -------------------------------------------------------------
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
