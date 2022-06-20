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
    public Vector2 BBoxPixelCoordA = new Vector2(0,0);
    public Vector2 BBoxPixelCoordB = new Vector2(0,0);

    public Vector2 BBoxWorldCoordA = new Vector3(0, 0, 0);
    public Vector2 BBoxWorldCoordB = new Vector3(0, 0, 0);

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


    public void UpdateFromMQTTMessage(float msg, string id)
    {
        Angle = msg;
        thunderboardID = id;
        SetIDText();
        SetNewPositionFromAngle = true;
        SetSensorText();
    }


    public void UpdateCoordinatesFromYolo(Vector2 pixelA, Vector2 pixelB, Vector3 worldA, Vector3 worldB)
    {
        BBoxPixelCoordA = pixelA;
        BBoxPixelCoordB = pixelB;
        BBoxWorldCoordA = worldA;
        BBoxWorldCoordB = worldB;
        SetCoordiantesText(pixelA, pixelB, worldA, worldB);
        SetNewPositionFromBBox = true;
        SetIDText();
    }

    


    /// <summary>
    /// 
    /// </summary>
    public void SetNewPositionThunderboardFromBBoxCoords()
    {
        string log = $"--- SetNewPositionThunderboardFromBBoxCoords ---";

        var newX = BBoxWorldCoordA.x + (BBoxWorldCoordB.x - BBoxWorldCoordA.x) /2f;
        var newY = BBoxWorldCoordA.y + 0.3f;

        var newPositionInfoBox = new Vector3(newX, newY, thunderboardPositionInWorldCoords.z);

        var thunderboardTransform = ThunderboardInfoBox.GetComponent<Transform>();
        var curPosition = thunderboardTransform.position;
        log += $"\ncurPosition: {curPosition}";
        log += $"\nnewPosition: {newPositionInfoBox}";

        thunderboardPositionInWorldCoords = newPositionInfoBox;
        thunderboardTransform.position = newPositionInfoBox;

        SetPositionText();
        log += $"--- SetNewPositionThunderboardFromBBoxCoords --- end ---";
        Debug.Log(log);
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CoordA">from bounding box</param>
    /// <param name="CoordB">from bounding box</param>
    public void SetNewPositionThunderboardFromAngle()
    {
        try
        {
            string log = $"--- SetNewPositionThunderboardFromAngle ---";

            var newPosition = GetPositionFromAngle(Angle);

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



    public void SetCoordiantesText(Vector2 bBoxCoordA, Vector2 bBoxCoordB, Vector3 worldCoordA, Vector3 worldCoordB)
    {
        var coords = $"BBox Coordinates:<space=3.5em> {bBoxCoordA} and {bBoxCoordB}";
        coords += $"\nWorld Coordinates:<space=3em> {worldCoordA} and {worldCoordB}";
        CoordinatesText.GetComponent<TextMeshPro>().text = coords;
    }

    public void SetPositionText()
    {
        var coords = $"Position:<space=7.4em> {thunderboardPositionInWorldCoords} ";
        PositionText.GetComponent<TextMeshPro>().text = coords;
    }


    public void SetAngleText()
    {
        var angleT = $"Angle of Arrival:<space=4em> {Angle}° ";
        AngleText.GetComponent<TextMeshPro>().text = angleT;
    }

    public void SetIDText()
    {
        var idT = $"ID:<space=10em> {thunderboardID} ";
        IDText.GetComponent<TextMeshPro>().text = idT;
    }

    public void SetSensorText()
    {
        var text = $"Current Temperature:<space=1.5em> {Mathf.Round(UnityEngine.Random.Range(15f,35f) *100f)/100f} °C";
        SensorText.GetComponent<TextMeshPro>().text = text;
    }

    public Vector3 GetPositionFromAngle(float angle)
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


    public void RemoveInfoBox(GameObject InfoBox)
    {
        thunderboardHandlerListScript.thunderboardHandlerList.RemoveAll(tbh => tbh.thunderboardID == thunderboardID);
        InfoBox.SetActive(false);
        Destroy(InfoBox);
    }

}
