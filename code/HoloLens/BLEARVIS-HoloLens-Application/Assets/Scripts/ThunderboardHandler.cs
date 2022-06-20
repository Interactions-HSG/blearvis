using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ThunderboardHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject ThunderboardInfoBox;

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
    public string thunderboardID;
    public Vector3 thunderboardPositionInWorldCoords = new Vector3(0, 0, 0);


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
            SetNewPositionThunderboardFromAngle();
            SetNewPositionFromAngle = false;
        } else if(SetNewPositionFromBBox)
        {
            SetNewPositionThunderboardFromBBoxCoords();
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
        thunderboardID = id;
        SetIDText();
        SetNewPositionFromAngle = true;
        SetSensorText();
    }


    /// <summary>
    /// Receives Coordinates from the YOLO result. These are saved in this ThunderboardHandler's fields.
    /// </summary>
    /// <param name="pixelA"></param>
    /// <param name="pixelB"></param>
    /// <param name="worldA"></param>
    /// <param name="worldB"></param>
    public void UpdateCoordinatesFromYolo(Vector2 pixelA, Vector2 pixelB, Vector3 worldA, Vector3 worldB)
    {
        BBoxPixelCoordTL = pixelA;
        BBoxPixelCoordBR = pixelB;
        BBoxWorldCoordTL = worldA;
        BBoxWorldCoordBR = worldB;
        SetCoordiantesText(pixelA, pixelB, worldA, worldB);
        SetNewPositionFromBBox = true;
        SetIDText();
        ThunderboardInfoBox.SetActive(true);
    }

    


    /// <summary>
    /// Updates the ThunderboardInfoBox's position based on the bounding box coordinates from YOLO.
    /// </summary>
    public void SetNewPositionThunderboardFromBBoxCoords()
    {
        string log = $"--- SetNewPositionThunderboardFromBBoxCoords ---";

        var newX = BBoxWorldCoordTL.x + (BBoxWorldCoordBR.x - BBoxWorldCoordTL.x) /2f;
        var newY = BBoxWorldCoordTL.y + (BBoxWorldCoordTL.y - BBoxWorldCoordBR.y) /4f;

        var newPositionInfoBox = new Vector3(newX, newY, thunderboardPositionInWorldCoords.z);

        var thunderboardTransform = ThunderboardInfoBox.GetComponent<Transform>();
        var curPosition = thunderboardTransform.position;
        log += $"\ncurPosition: {curPosition}";
        log += $"\nnewPosition: {newPositionInfoBox}";

        thunderboardPositionInWorldCoords = newPositionInfoBox;
        thunderboardTransform.position = newPositionInfoBox;

        SetPositionText();
        log += $"\n--- SetNewPositionThunderboardFromBBoxCoords --- end ---";
        Debug.Log(log);
    }



    /// <summary>
    /// Updates the ThunderboardInfoBox's position based on the received angle of arrival from this thunderboard.
    /// </summary>
    public void SetNewPositionThunderboardFromAngle()
    {
        try
        {
            string log = $"--- SetNewPositionThunderboardFromAngle ---";

            var newPosition = CalculatePositionFromAngle(Angle);

            Debug.Log($"new angle: {Angle} --- newX: {newPosition.x}");

            var thunderboardTransform = ThunderboardInfoBox.GetComponent<Transform>();
            var curPosition = thunderboardTransform.position;
            log += $"\ncurPosition: {curPosition}";


            log += $"\nnew Position: {newPosition}";
            thunderboardPositionInWorldCoords = newPosition;
            thunderboardTransform.position = newPosition;
            
            SetPositionText();
            SetAngleText();

            log += $"--- SetNewPositionThunderboardFromAngle --- end ---";
            Debug.Log(log);

        } catch (Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
        
    }


    /// <summary>
    /// Updates the "bounding box coordinates" text in the ThunderboardInfoBox
    /// </summary>
    /// <param name="bBoxCoordA"></param>
    /// <param name="bBoxCoordB"></param>
    /// <param name="worldCoordA"></param>
    /// <param name="worldCoordB"></param>
    public void SetCoordiantesText(Vector2 bBoxCoordA, Vector2 bBoxCoordB, Vector3 worldCoordA, Vector3 worldCoordB)
    {
        var coords = $"BBox Coordinates:<space=3.5em> {bBoxCoordA} and {bBoxCoordB}";
        coords += $"\nWorld Coordinates:<space=3em> {worldCoordA} and {worldCoordB}";
        CoordinatesText.GetComponent<TextMeshPro>().text = coords;
    }

    /// <summary>
    ///  Updates the "position" text in the ThunderboardInfoBox
    /// </summary>
    public void SetPositionText()
    {
        var coords = $"Position:<space=7.4em> {thunderboardPositionInWorldCoords} ";
        PositionText.GetComponent<TextMeshPro>().text = coords;
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
    ///  Updates the "Thunderboard ID" text in the ThunderboardInfoBox
    /// </summary>
    public void SetIDText()
    {
        var idT = $"ID:<space=10em> {thunderboardID} ";
        IDText.GetComponent<TextMeshPro>().text = idT;
    }

    /// <summary>
    ///  Updates the "Sensor data" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSensorText()
    {
        var text = $"Current Temperature:<space=1.5em> {Mathf.Round(UnityEngine.Random.Range(15f,35f) *100f)/100f} °C";
        SensorText.GetComponent<TextMeshPro>().text = text;
    }

    /// <summary>
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Vector3 CalculatePositionFromAngle(float angle)
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
        //var newX = Mathf.Tan(Mathf.Deg2Rad * angle);
        var newXCircle = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
        var newZCircle = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));


        // negate because +90 is on left and -90 is on right of receiver
        newPosition.x = newXCircle;
        newPosition.z = newZCircle;

        return newPosition;
    }

    /// <summary>
    /// Hides a given ThunderboardInfobox and deletes it's associated ThunderboardHandler.
    /// </summary>
    /// <param name="InfoBox"></param>
    public void RemoveInfoBox(GameObject InfoBox)
    {
        thunderboardHandlerListScript.thunderboardHandlerList.RemoveAll(tbh => tbh.thunderboardID == thunderboardID);
        InfoBox.SetActive(false);
        Destroy(InfoBox);
    }

}
