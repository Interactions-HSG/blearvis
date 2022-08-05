using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

/// <summary>
/// This script is attached to one ThunderboardInfoBox. 
/// It handles the incoming data for one specific thunderboard.
/// </summary>
public class ThunderboardHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject ThunderboardInfoBox;

    public GameObject ThingURIText;
    public GameObject IDText;
    public GameObject AngleText;
    public GameObject SensorText;
    public GameObject PositionText;
    public GameObject CoordinatesText;

    [Header("Prefab")]
    public GameObject thunderboardInfoBoxPrefab;

    [Header("Scripts")]
    public ThunderboardHandlerListScript thunderboardHandlerListScript;

    [Header("Parameters")]
    public Vector2 BBoxPixelCoordTL = new Vector2(0,0);
    public Vector2 BBoxPixelCoordBR = new Vector2(0,0);
    public Vector2 BBoxWorldCoordTL = new Vector3(0, 0, 0);
    public Vector2 BBoxWorldCoordBR = new Vector3(0, 0, 0);
    public float Angle = 0.0f;
    public string ThunderboardID;
    public string ThingURI;
    public Vector3 ThunderboardInfoBoxPositionInWorldCoords = new Vector3(0, 0, 0);


    public bool SetNewPositionFromAngle = false;
    public bool SetNewPositionFromBBox = false;
    //public float newAngle;

    // Start is called before the first frame update
    void Start()
    {
        // there is only one thunderboardHandlerListScript in the scene
        if (thunderboardHandlerListScript == null)
        {
            var allTBHScripts = GameObject.FindGameObjectsWithTag("TBHList");
            thunderboardHandlerListScript = allTBHScripts[0].GetComponent<ThunderboardHandlerListScript>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SetNewPositionFromAngle)
        {            
            SetNewPositionThunderboardInfoBoxFromAngle();
            SetNewPositionFromAngle = false;
        } else if(SetNewPositionFromBBox)
        {
            SetNewPositionThunderboardInfoBoxFromBBoxCoords();
            SetNewPositionFromBBox = false;
        }
    }

    /// <summary>
    /// Receives the data from a MQTT message
    /// </summary>
    /// <param name="msg">i.e. the angle f arrival</param>
    /// <param name="id">the ID from the thunderboard that has sent the message</param>
    public void UpdateFromMQTTMessage(float msg, string id)
    {
        Angle = msg;
        ThunderboardID = id;
        SetIDText();
        SetNewPositionFromAngle = true;
        SetSensorText();
    }


    /// <summary>
    /// Receives Coordinates from the YOLO result. These are saved in this ThunderboardHandler's fields.
    /// </summary>
    /// <param name="pixelTL"></param>
    /// <param name="pixelBR"></param>
    /// <param name="worldTL"></param>
    /// <param name="worldBR"></param>
    /// <param name="thingURI"></param>
    public void UpdateParametersFromYoloResult(Vector2 pixelTL, Vector2 pixelBR, Vector3 worldTL, Vector3 worldBR, string thingURI)
    {
        BBoxPixelCoordTL = pixelTL;
        BBoxPixelCoordBR = pixelBR;
        BBoxWorldCoordTL = worldTL;
        BBoxWorldCoordBR = worldBR;
        ThingURI = thingURI;
        SetNewPositionFromBBox = true;
        SetThingURIText();
        SetCoordiantesText();
        SetIDText();
        ThunderboardInfoBox.SetActive(true);
    }

    


    /// <summary>
    /// Updates the ThunderboardInfoBox's position based on the bounding box coordinates from YOLO.
    /// </summary>
    public void SetNewPositionThunderboardInfoBoxFromBBoxCoords()
    {
        string log = $"--- SetNewPositionThunderboardInfoBoxFromBBoxCoords ---";

        var newX = BBoxWorldCoordTL.x + (BBoxWorldCoordBR.x - BBoxWorldCoordTL.x) /2f;
        var newY = BBoxWorldCoordTL.y + (BBoxWorldCoordTL.y - BBoxWorldCoordBR.y) /4f;

        Vector2 centerBBox = Vector2.Lerp(BBoxPixelCoordTL, BBoxPixelCoordBR, 0.5f);
        var newPosFromRay = thunderboardHandlerListScript.ScreenToWorldPointRaycast(centerBBox);
        //var newZ = thunderboardHandlerListScript.ScreenToWorldPointRaycast(centerBBox).z;
        // keep the InfoBox at least 0.5m away from the user
        // newZ = (newZ <= 0.5f) ? 0.5f : newZ;
        var newZ  = (newPosFromRay.z < 0.5) ? 0.5f : newPosFromRay.z;
        newPosFromRay.z = newZ;
          //var newPositionInfoBox = new Vector3(newX, newY, newZ);

        var thunderboardInfoBoxTransform = ThunderboardInfoBox.GetComponent<Transform>();
        var curPosition = thunderboardInfoBoxTransform.position;
        log += $"\ncurPosition: {curPosition}";
        log += $"\nnewPosition: {newPosFromRay}";

        if (newZ > 1)
        {
            log += $"\nz > 1: {newZ}";
            var newScale =  (newZ > 3) ? 3 : newZ;
            thunderboardInfoBoxTransform.localScale = new Vector3(newScale, newScale, newScale);
        }

        ThunderboardInfoBoxPositionInWorldCoords = newPosFromRay;
        thunderboardInfoBoxTransform.position = newPosFromRay;

        SetPositionText();
        log += $"\n--- SetNewPositionThunderboardInfoBoxFromBBoxCoords --- end ---";
        Debug.Log(log);
    }



    /// <summary>
    /// Updates the ThunderboardInfoBox's position based on the received angle of arrival from this thunderboard.
    /// </summary>
    public void SetNewPositionThunderboardInfoBoxFromAngle()
    {
        try
        {
            string log = $"--- SetNewPositionThunderboardInfoBoxFromAngle ---";

            var newPosition = thunderboardHandlerListScript.CalculatePositionFromAngle(Angle);

            Debug.Log($"new angle: {Angle} --- newX: {newPosition.x}");

            var thunderboardInfoBoxTransform = ThunderboardInfoBox.GetComponent<Transform>();
            var curPosition = thunderboardInfoBoxTransform.position;
            log += $"\ncurPosition: {curPosition}";


            log += $"\nnew Position: {newPosition}";
            ThunderboardInfoBoxPositionInWorldCoords = newPosition;
            thunderboardInfoBoxTransform.position = newPosition;

            var newRotation = CalculateRotationFromAngle(Angle);
            thunderboardInfoBoxTransform.rotation = newRotation;
            
            SetPositionText();
            SetAngleText();

            log += $"--- SetNewPositionThunderboardInfoBoxFromAngle --- end ---";
            Debug.Log(log);

        } catch (Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
        
    }

    

    /// <summary>
    ///  Updates the "Thunderboard ID" text in the ThunderboardInfoBox
    /// </summary>
    public void SetThingURIText()
    {
        var thingURIT = $"Thing URI:<space=3em> {ThingURI} ";
        ThingURIText.GetComponent<TextMeshPro>().text = thingURIT;
    }


    /// <summary>
    ///  Updates the "Thunderboard ID" text in the ThunderboardInfoBox
    /// </summary>
    public void SetIDText()
    {
        var idT = $"ID:<space=10em> {ThunderboardID} ";
        IDText.GetComponent<TextMeshPro>().text = idT;
    }

    /// <summary>
    ///  Updates the "Angel of Arrival" text in the ThunderboardInfoBox
    /// </summary>
    public void SetAngleText()
    {
        var angleT = $"Angle of Arrival:<space=4em> {Angle}° ";
        AngleText.GetComponent<TextMeshPro>().text = angleT;
    }

    /// <summary>
    ///  Updates the "position" text in the ThunderboardInfoBox
    /// </summary>
    public void SetPositionText()
    {
        var coords = $"Position:<space=7.4em> {ThunderboardInfoBoxPositionInWorldCoords} ";
        PositionText.GetComponent<TextMeshPro>().text = coords;
    }
    /// <summary>
    /// Updates the "bounding box coordinates" text in the ThunderboardInfoBox
    /// </summary>
    public void SetCoordiantesText()
    {
        var coords = $"BBox Pixel Coordinates:<space=1.5em> {BBoxPixelCoordTL} and {BBoxPixelCoordBR}";
        coords += $"\nBBox World Coordinates:<space=2em> {BBoxWorldCoordTL} and {BBoxWorldCoordBR}";
        CoordinatesText.GetComponent<TextMeshPro>().text = coords;
    }

    /// <summary>
    ///  Updates the "Sensor data" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSensorText()
    {
        var text = $"Sensor Value:<space=2em> {Mathf.Round(UnityEngine.Random.Range(15f,35f) *100f)/100f} °C";
        SensorText.GetComponent<TextMeshPro>().text = text;
    }

    /*
    /// <summary>
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Vector3 CalculatePositionFromAngle(float angle)
    {
        string log = "--- CalculatePositionFromAngle ---";
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
         ///
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
*/

    public Quaternion CalculateRotationFromAngle(float angle)
    {
        // negate because +90 is on left and -90 is on right of receiver
        return Quaternion.Euler(0, -angle, 0);
    }

    /// <summary>
    /// Hides a given ThunderboardInfobox and deletes it's associated ThunderboardHandler.
    /// </summary>
    /// <param name="InfoBox"></param>
    public void RemoveInfoBox(GameObject InfoBox)
    {
        thunderboardHandlerListScript.thunderboardHandlerList.RemoveAll(tbh => tbh.ThunderboardID == ThunderboardID);
        InfoBox.SetActive(false);
        Destroy(InfoBox);
    }

}
