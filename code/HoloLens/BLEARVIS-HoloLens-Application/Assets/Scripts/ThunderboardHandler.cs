﻿using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// This script is attached to one ThunderboardInfoBox. 
/// It handles the incoming data for one specific thunderboard.
/// </summary>
public class ThunderboardHandler : MonoBehaviour
{
    [Header("UI Elements")]
    

    public GameObject ThingURIText;
    public GameObject IDText;
    public GameObject AngleText;
    public GameObject SensorTextTemperature;
    public GameObject SensorTextHumidity;
    public GameObject PositionText;
    public GameObject CoordinatesText;
    public GameObject SerialNumberText;
    public GameObject IndicatorBLE;
    public GameObject IndicatorObjectDetection;

    [Header("Tractor")]
    public GameObject TractorInfo;
    public GameObject BatteryText;
    public GameObject Soil;
    public GameObject SoilText;

    [Header("Prefab")]
    public GameObject ThunderboardInfoBox;
    public GameObject BBoxCorner;
    //public GameObject ThunderboardInfoBoxPrefab;

    [Header("Scripts")]
    public ThunderboardHandlerList ThunderboardHandlerList;
    public PositionHandler PositionHandler;
    public ThingHandler ThingHandler;
    public StaticDeviceHandler StaticDeviceHandler;

    [Header("Parameters")]
    public float AngleOfArrival = 0.0f;
    public (float temperature, float humidity) SensorData;
    public DateTime LastTemperatureSensorUpdate;
    public DateTime LastHumiditySensorUpdate;
    public string ThunderboardID = null;
    public string ThingURI = null;
    public DateTime LastAoAUpdate;
    public DateTime LastYoloUpdate;
    public Vector2 BBoxPixelTopLeft = new Vector2(0, 0);
    public Vector2 BBoxPixelBottomRight = new Vector2(0, 0);
    public Vector3 BBoxCameraTopLeft = new Vector3(0, 0, 0);
    public Vector3 BBoxCameraBottomRight = new Vector3(0, 0, 0);
    public Vector3 TBCurrentLocalOffsetInWorld = new Vector3(0, 0, 0);
    public (Vector3 offset, DateTime timestamp) LastYoloOffset;
    public DateTime LastYoloFrameProcessedAtTime;
    public (Vector3 offset, DateTime timestamp) LastAoAOffset;
    public int SerialNumber;
    public (int ph, int moisture, int density, int nitrate) SoilCondition;
    public float BatteryVoltage;
    public string thingIP;
    public int NumberOfYoloCoordinatesReceived;
    public int NumberOfAoAsReceived;
    public List<Vector3> Last5AoAOffsets = new List<Vector3>();
    public List<Vector3> Last5YoloOffsets = new List<Vector3>();

    public static bool SetNewPositionFromAoA;
    public static bool SetNewPositionFromYolo;
    public bool GetDataFromThing;
    public bool GetDataFromThingCourutineRunning;
    //public float newAngle;
  

    // Start is called before the first frame update
    void Awake()
    {
        // there is only one thunderboardHandlerList in the scene
        if (ThunderboardHandlerList == null)
        {
            var allTBHScripts = GameObject.FindGameObjectsWithTag("TBHList");
            ThunderboardHandlerList = allTBHScripts[0].GetComponent<ThunderboardHandlerList>();
        }
        if (PositionHandler == null)
        {
            var allPosHandlers = GameObject.FindGameObjectsWithTag("PositionHandler");
            // There is only one position Handler in the scene
            PositionHandler = allPosHandlers[0].GetComponent<PositionHandler>();
            Debug.Log($"position Handler: {PositionHandler}"); 
        }
        if (ThingHandler == null)
        {
            var allThingHandlers = GameObject.FindGameObjectsWithTag("ThingHandler");
            // There is only one ThingHandler in the scene
            ThingHandler = allThingHandlers[0].GetComponent<ThingHandler>();
            Debug.Log($"ThingHandler: {ThingHandler}");
        }

        Debug.Log($"position Handler: {PositionHandler}");
        LastYoloUpdate = DateTime.UtcNow;
        LastAoAUpdate = DateTime.UtcNow;
        LastHumiditySensorUpdate = DateTime.UtcNow;
        LastTemperatureSensorUpdate = DateTime.UtcNow;
        SetNewPositionFromAoA = false;
        SetNewPositionFromYolo = false;
        GetDataFromThing = false;
        GetDataFromThingCourutineRunning = false;
        SoilCondition.ph = 0;
        SoilCondition.moisture = 0;
        SoilCondition.density = 0;
        SoilCondition.nitrate = 0;
        BatteryVoltage = 0;
        thingIP = "";
        NumberOfYoloCoordinatesReceived = 0;
        NumberOfAoAsReceived = 0;
    }

    // Update is called once per frame
    void Update()
    {
      

        /*
        if ((DateTime.UtcNow - LastYoloUpdate).TotalSeconds > 10 && (DateTime.UtcNow - LastAoAUpdate).TotalSeconds > 10) {
            Debug.Log($"Removing tb because of inactivity: {ThunderboardID}/{ThingURI}");
            ThunderboardInfoBox.SetActive(false);
            RemoveInfoBox(ThunderboardInfoBox);
        }
        //*/
        if (!GetDataFromThingCourutineRunning)
        {
            GetDataFromThing = true;
            StartCoroutine(StartGettingDataFromThing());
        }


        /*
        if ((DateTime.UtcNow -LastYoloOffset.timestamp).TotalSeconds < 3)
        {
            IndicatorObjectDetection.SetActive(true);
        }
        else
        {
            IndicatorObjectDetection.SetActive(false);
        }

        if ((DateTime.UtcNow - LastAoAOffset.timestamp).TotalSeconds < 3)
        {
            IndicatorBLE.SetActive(true);
        }
        else
        {
            IndicatorBLE.SetActive(false);
        }
        //*/
    }

  


    /// <summary>
    /// Receives the data from a MQTT message
    /// </summary>
    /// <param name="aoa">i.e. the angle f arrival</param>
    /// <param name="id">the ID from the thunderboard that has sent the message</param>
    public void UpdateFromAoAMQTTMessage(float aoa, string id)
    {
        // var currentAngle = Angle;
        var curAngleDiff = Mathf.Abs(AngleOfArrival - aoa);
        Debug.Log($"curAngle: {AngleOfArrival}, newAngle: {aoa}, diff: {curAngleDiff}");

        /*
        //  curAngle: 6.17, newAngle: -68.12, last diff: 54.29
        // curAngle: 6.17, newAngle: -70.05, cur diff: 76.22
        if (curAngleDiff > 10 && Math.Abs(lastAngleDifference - curAngleDiff) > 10)
        {
            Debug.Log("not setting new angle, diff > 10");
            SetIDText();
            SetSensorText();
            return;
        }
        //*/
        //lastAngleDifference = curAngleDiff;
        if (AngleOfArrival == 0 || (curAngleDiff < 20 && !PositionHandler.CameraIsMovingTooMuch()) || curAngleDiff < 40)
        {
            AngleOfArrival = aoa;
            ThunderboardID = id;
            SetNewPositionFromAoA = true;
            
            SetIDText();
            // SetSerialNumberText();
            SetAngleText();
            NumberOfAoAsReceived++;
            //if (NumberOfAoAsReceived >= 5 && (NumberOfYoloCoordinatesReceived >= 5|| NumberOfYoloCoordinatesReceived == 0))
            if (NumberOfAoAsReceived == 1)
            {
                SetNewPositionFromAoA = false;
            }
            SetNewPosition();

            //SetOffsetText();

            //ThunderboardInfoBox.SetActive(true);
            LastAoAUpdate = DateTime.UtcNow;
        } else
        {
            Debug.Log("AoA diff to large. not updating"); 
        }
        
        Debug.Log("--- UpdateFromMQTTMessage --- end ---");
    }


    /// <summary>
    /// Receives and stores a new value from a sensor on the thunderboard.
    /// </summary>
    /// <param name="sensorType">can be: temperature or humidity</param>
    /// <param name="value">the value read by the sensor</param>
    public void UpdateFromSensorDataMQTTMessage(string sensorType, float value)
    {
        //LastTemperatureSensorUpdate = (LastTemperatureSensorUpdate == default(DateTime)) ? DateTime.UtcNow : LastTemperatureSensorUpdate;
        //LastHumiditySensorUpdate = (LastHumiditySensorUpdate == default(DateTime)) ? DateTime.UtcNow : LastHumiditySensorUpdate;

        if (sensorType == "temp")
        {
            if ((DateTime.UtcNow - LastTemperatureSensorUpdate).TotalSeconds < 0.25)
            {
                //Debug.Log($"Last sensor update was less than 0.5s ago. Not updating.");
                return;
            }
            // only update if the value has changed.
            if (SensorData.temperature == value) { return; }
            SensorData.temperature = value;
            Debug.Log($"Updated temperature: {value}");
            LastTemperatureSensorUpdate = DateTime.UtcNow;
            SetSensorTemperatureText();
        } 
        else if (sensorType == "hum")
        {
            if ((DateTime.UtcNow - LastHumiditySensorUpdate).TotalSeconds < 0.25)
            {
                //Debug.Log($"Last sensor update was less than 0.5s ago. Not updating.");
                return;
            }
            if (SensorData.humidity == value) { return; }
            SensorData.humidity = value;
            LastHumiditySensorUpdate = DateTime.UtcNow;
            Debug.Log($"Updated humidity: {value}");
            SetSensorHumidityText();
        }
        LastTemperatureSensorUpdate = DateTime.Now;

    }

    /// <summary>
    /// Receives Coordinates from the YOLO result. These are saved in this ThunderboardHandler's fields.
    /// </summary>
    /// <param name="pixelTL"></param>
    /// <param name="pixelBR"></param>
    /// <param name="cameraTL"></param>
    /// <param name="cameraBR"></param>
    /// <param name="thingURI"></param>
    public void UpdateParametersFromYoloResult(Vector2 pixelTL, Vector2 pixelBR, Vector3 cameraTL, Vector3 cameraBR, string thingURI, 
        DateTime frameTime)
    {
        var log = "--- UpdateParametersFromYoloResult ---";
        BBoxPixelTopLeft = pixelTL;
        BBoxPixelBottomRight = pixelBR;
        BBoxCameraTopLeft = cameraTL;
        BBoxCameraBottomRight = cameraBR;
        ThingURI = thingURI;
        SetNewPositionFromYolo = true;
        LastYoloFrameProcessedAtTime = frameTime;

        log += $"\nSetNewPositionFromYolo: {SetNewPositionFromYolo}";
        log += $"\nNumberOfYoloCoordinatesReceived: {NumberOfYoloCoordinatesReceived}";
        NumberOfYoloCoordinatesReceived++;
        log += $"\nNumberOfYoloCoordinatesReceived++: {NumberOfYoloCoordinatesReceived}";
        if (NumberOfYoloCoordinatesReceived >= 5 && (NumberOfAoAsReceived == 0 || NumberOfAoAsReceived >= 5))
        {
            SetNewPositionFromYolo = false;
        }
        SetNewPosition();

        SetThingURIText();
        //SetCoordiantesText();
        SetIDText();
        //SetSerialNumberText();
        LastYoloUpdate = DateTime.UtcNow;
        log +="\n--- UpdateParametersFromYoloResult --- end ---";
        Debug.Log(log);
        
    }

    private void SetNewPosition()
    {
        Debug.Log("set new position");
        if (SetNewPositionFromAoA)
        {
            CalculateNewOffsetFromAoA();
            /*
            if (!PositionHandler.CameraIsMovingTooMuch())
            {
                CalculateNewOffsetFromAoA();
            }
            else
            {
                Debug.Log("AoA: Camera is moving too much. Not updating the offset.");
            }
            */
            SetNewPositionFromAoA = false;
        }

        if (SetNewPositionFromYolo)
        {
            if (PositionHandler == null)
            {
                var allPosHandlers = GameObject.FindGameObjectsWithTag("PositionHandler");
                PositionHandler = allPosHandlers[0].GetComponent<PositionHandler>();
                Debug.Log($"position Handler: {PositionHandler}");
            }
            //PositionHandler.CalculateNewOffsetFromYoloStaticDevice();

            Debug.Log($"SetNewPositionFromYolo: {SetNewPositionFromYolo}");
            /*
            if (!PositionHandler.CameraIsMovingTooMuch())
            {
                Debug.Log($"!PositionHandler.CameraIsMovingTooMuch(): {!PositionHandler.CameraIsMovingTooMuch()}");
                CalculateNewOffsetFromYolo();
            }
            else
            {
                Debug.Log("Yolo: Camera is moving too much. Not updating the offset.");
            }
            //*/
            SetNewPositionFromYolo = false;
        }
        else
        {
            var billboard = ThunderboardInfoBox.GetComponent<Billboard>();
            billboard.enabled = true;
            Debug.Log($"SetNewPositionFromYolo: {SetNewPositionFromYolo}");
        }
    }



    /*
    /// <summary>
    /// Updates the ThunderboardInfoBox's orbital.localOffset.z based on a raycast in the 
    /// center of the bounding box coordinates from YOLO.
    /// </summary>
    public void CalculateNewOffsetFromYolo()
    {
        string log = $"--- CalculateNewOffsetFromYolo ---";

        // do a Raycast in each corner of the BoundingBox and the center.
        // choose the smalles Z -> possibly closest point of object to user
        Vector2 centerBBox = Vector2.Lerp(BBoxPixelTopLeft, BBoxPixelBottomRight, 0.5f);

        var newOffsetFromRayCenter = PositionHandler.ScreenToCameraPointThroughRaycast(centerBBox, $"C_{_raycastCounter}");
        // from the corners of the bbox go more towards the center. might make hitting the object more likely
        var tl = Vector2.Lerp(BBoxPixelTopLeft, centerBBox, 0.4f);
        var newOffsetFromRayTopLeft = PositionHandler.ScreenToCameraPointThroughRaycast(tl, $"TL_{_raycastCounter}");
        //
        var br = Vector2.Lerp(BBoxPixelBottomRight, centerBBox, 0.4f);
        var NewOffsetFromRayBottomRight = PositionHandler.ScreenToCameraPointThroughRaycast(br, $"BR_{_raycastCounter}");

        var topRight = new Vector2(BBoxPixelBottomRight.x, BBoxPixelTopLeft.y);

        topRight = Vector2.Lerp(topRight, centerBBox, 0.4f);
        var newOffsetFromRayTopRight = PositionHandler.ScreenToCameraPointThroughRaycast(topRight, $"TR_{_raycastCounter}");

        var bottomLeft = new Vector2(BBoxPixelTopLeft.x, BBoxPixelBottomRight.y);
        bottomLeft = Vector2.Lerp(bottomLeft, centerBBox, 0.4f);
        var newOffsetFromRayBottomLeft = PositionHandler.ScreenToCameraPointThroughRaycast(bottomLeft, $"BL_{_raycastCounter}");
        _raycastCounter++;


        var meanX = (newOffsetFromRayCenter.x + newOffsetFromRayTopLeft.x + NewOffsetFromRayBottomRight.x +
           newOffsetFromRayTopRight.x + newOffsetFromRayBottomLeft.x) / 5f;

        var maxY = Mathf.Max(newOffsetFromRayCenter.y, newOffsetFromRayTopLeft.y, NewOffsetFromRayBottomRight.y,
           newOffsetFromRayTopRight.y, newOffsetFromRayBottomLeft.y);

        var minY = Mathf.Min(newOffsetFromRayCenter.y, newOffsetFromRayTopLeft.y, NewOffsetFromRayBottomRight.y,
           newOffsetFromRayTopRight.y, newOffsetFromRayBottomLeft.y);

        // get the z-value that is closest to the HL2 that is not 0
        var zs = new[] {newOffsetFromRayCenter.z, newOffsetFromRayTopLeft.z, NewOffsetFromRayBottomRight.z,
            newOffsetFromRayTopRight.z, newOffsetFromRayBottomLeft.z};
        float closestZ = zs.Where(z => z != 0).DefaultIfEmpty().Min();
        // var closestZ = Mathf.Min(newOffsetFromRayCenter.z, newOffsetFromRayTopLeft.z, NewOffsetFromRayBottomRight.z, newOffsetFromRayTopRight.z, newOffsetFromRayBottomLeft.z);

        var bboxHeight = Mathf.Abs(maxY - minY);
        log += $"\nbboxHeight: {bboxHeight}";



        // keep the InfoBox at least 0.5m away from the user
        //closestZ += 0.2f;
        closestZ = (closestZ <= 0.7f) ? 0.7f : closestZ;

        // +(bboxHeight / 2)
        var newLocalOffset = new Vector3(meanX, maxY+bboxHeight/2, closestZ);
        log += $"\nnewLocalOffset: {meanX}, {maxY}, {closestZ}";
        log += $"\nnewLocalOffset + y-bbox: {newLocalOffset}";

        var thunderboardInfoBoxTransform = ThunderboardInfoBox.GetComponent<Transform>();
        var curPosition = thunderboardInfoBoxTransform.position;
        log += $"\ncurPosition: {curPosition}";

        var orbital = ThunderboardInfoBox.GetComponent<Orbital>();
        orbital.enabled = true;
        orbital.UpdateLinkedTransform = false;
        var curLocalOffset = orbital.LocalOffset;
        log += $"\ncurLocalOffset: {curLocalOffset}";
        //log += $"\nnewPosition: {newPosFromRay}";

        ///*
        if (newZ > 2)
        {
            log += $"\nz > 1: {newZ}";
            var newScale = (newZ > 3) ? 3 : newZ;
            thunderboardInfoBoxTransform.localScale = new Vector3(newScale, newScale, newScale);
        }
        ///

        log += $"\ntime diff in s: {(DateTime.UtcNow - LastYoloUpdate).TotalSeconds}";
        //if ((DateTime.UtcNow - LastYoloUpdate).TotalSeconds > 2)
        //{
        //var orbital = ThunderboardInfoBox.GetComponent<Orbital>();
        

        var billboard = ThunderboardInfoBox.GetComponent<Billboard>();
        billboard.enabled = false;
        var radialView = ThunderboardInfoBox.GetComponent<RadialView>();
        radialView.enabled = true;
        radialView.UpdateLinkedTransform = true;



        // position the InfoBox in the middle of the object (x), above it (y+0.5*bboxHeight), and closest z (newZ)
        // var newRawOffset = new Vector3(newOffsetFromRayCenter.x, newOffsetFromRayCenter.y, closestZ);
        // log += $"\nnewRawOffset: {newRawOffset}";
        //newRawOffset = Camera.main.transform.position - newRawOffset;
        //  newRawOffset.y+0.7f*bboxHeight
        // var newLocalOffset = new Vector3(newRawOffset.x, newRawOffset.y, newRawOffset.z);
        //var newLocalOffset = new Vector3(newRawOffset.x, newRawOffset.y, newZ);
        // log += $"\nnewLocalOffset: {newLocalOffset}";


        var zHasChangedMuch = (Mathf.Abs(closestZ - curLocalOffset.z) > 0.5); //&& (Vector3.Distance(curLocalOffset, newLocalOffset) < 0.1);
        log += $"\nzHasChangedTooMuch: {zHasChangedMuch}";
        log += $"\nnew vs cur offset: {Vector3.Distance(curLocalOffset, newLocalOffset)}";

        // Vector3.Distance(curLocalOffset, newLocalOffset) > 0.1 &&
        //if (!zHasChangedMuch)
        //{

        // save new Offset
        LastYoloOffset = (newLocalOffset, LastYoloUpdate);
        //ThunderboardInfoBox.SetActive(true);
        Last5YoloOffsets.Add(newLocalOffset);

        

        if ((Last5YoloOffsets.Count > 1 && ThunderboardHandlerList.expectingAoA && Last5AoAOffsets.Count > 5) 
            || (Last5YoloOffsets.Count > 1 && !ThunderboardHandlerList.expectingAoA))
        {
            ThunderboardHandlerList.handleIncomingYoloResult = false;
            StaticDeviceHandler.SetTBPositionForStaticDevice(this, orbital, billboard);
        }

        
        log += $"\n--- CalculateNewOffsetFromYolo --- end ---";
        Debug.Log(log);

        SetOffsetText();

        // PositionHandler.UpdateLocalOffsetStaticObject(this, orbital, billboard); 

        // call function to apply the new offset
        //PositionHandler.UpdateLocalOffsetSensorFusion(this, orbital, billboard, radialView);

        //StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curLocalOffset, newLocalOffset, billboard));
        //LastYoloUpdate = DateTime.UtcNow;
        //}
        //}


        
       

    }
    //*/



    /// <summary>
    /// Updates the ThunderboardInfoBox's position based on the received angle of arrival from this thunderboard.
    /// </summary>
    public void CalculateNewOffsetFromAoA()
    {
        try
        {

            string log = $"--- CalculateNewOffsetFromAoA ---";

            var newOffset = PositionHandler.CalculateLocalOffsetFromAngle(AngleOfArrival);
            newOffset = new Vector3(newOffset.x, 0, newOffset.z);

            Debug.Log($"new angle: {AngleOfArrival} --- newX: {newOffset.x}");

            var thunderboardInfoBoxTransform = ThunderboardInfoBox.GetComponent<Transform>();
            var curPosition = thunderboardInfoBoxTransform.position;
            log += $"\ncurPosition: {curPosition}";

            log += $"\nnew Offset: {newOffset}";
            //TBCurrentLocalOffsetInWorld = newOffset;

            var orbital = ThunderboardInfoBox.GetComponent<Orbital>();
            orbital.enabled = true;
            orbital.UpdateLinkedTransform = false;
            var billboard = ThunderboardInfoBox.GetComponent<Billboard>();
            billboard.enabled = false;
            
            var curLocalOffset = orbital.LocalOffset;
            log += $"\ncurLocalOffset: {curLocalOffset}";
            var newLocalOffset = newOffset;
            log += $"\nnewLocalOffset: {newLocalOffset}";

            var radialView = ThunderboardInfoBox.GetComponent<RadialView>();
            radialView.enabled = true;
            radialView.UpdateLinkedTransform = true;


            LastAoAOffset = (newLocalOffset, LastAoAUpdate);
            Last5AoAOffsets.Add(newLocalOffset);

            log += $"--- CalculateNewOffsetFromAoA --- end ---";
            Debug.Log(log);

            /*
            if ((Last5AoAOffsets.Count > 5 && ThunderboardHandlerList.expectingYolo && Last5YoloOffsets.Count > 2)
            || (Last5YoloOffsets.Count > 5 && !ThunderboardHandlerList.expectingYolo))
            {
                StaticDeviceHandler.SetTBPositionForStaticDevice(this, orbital, billboard);
            }
            */

            // PositionHandler.UpdateLocalOffsetStaticObject(this, orbital, billboard);

            PositionHandler.UpdateLocalOffsetSensorFusion(this, orbital, billboard);

            // StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curLocalOffset, newLocalOffset, billboard));

            //thunderboardInfoBoxTransform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

            //var newRotation = CalculateRotationFromAngle(thunderboardInfoBoxTransform, AngleOfArrival);
            //thunderboardInfoBoxTransform.LookAt(Camera.main.transform);
            //var n = Camera.main.transform.position - thunderboardInfoBoxTransform.position;

            //thunderboardInfoBoxTransform.localRotation = newRotation;

           

           

        }
        catch (Exception e)
        {
            Debug.Log($"Exception: {e}");
        }

    }


    IEnumerator StartGettingDataFromThing()
    {
        if (thingIP == "")
        {
            yield return null;
        }
        Debug.Log($"GetDataFromThing: {GetDataFromThing}");
        while (GetDataFromThing)
        {
            Debug.Log("StartGettingDataFromThing --- start ---");
            GetDataFromThingCourutineRunning = true;
            Task task = ThingHandler.GetBatteryVoltageFromThing(thingIP, this);
            Task task2 = ThingHandler.GetSoilconditionFromThing(thingIP, this);
            yield return new WaitUntil(() => task.IsCompleted && task2.IsCompleted);
            if (BatteryVoltage != 0f)
            {
                Debug.Log("new battery voltage");
                SetBatteryVoltageText();
                TractorInfo.SetActive(true);
            }

            if (SoilCondition.ph != 0f || SoilCondition.moisture != 0f || SoilCondition.density != 0f || SoilCondition.nitrate != 0f )
            {
                Debug.Log("new soil condition");
                SetSoilconditionText();
                Soil.SetActive(true);
                TractorInfo.SetActive(true);
            } else
            {
                Soil.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
        GetDataFromThingCourutineRunning = false;
    }

    public void SetBatteryVoltageText()
    {
        var batteryText = $"{BatteryVoltage} V";
        BatteryText.GetComponent<TextMeshPro>().text = batteryText;
    }

    public void SetSoilconditionText()
    {
        var soilText = $"{SoilCondition.ph}\n{SoilCondition.moisture}\n{SoilCondition.density}\n{SoilCondition.nitrate}\n";
        SoilText.GetComponent<TextMeshPro>().text = soilText;
    }


    /// <summary>
    ///  Updates the "Thunderboard ID" text in the ThunderboardInfoBox
    /// </summary>
    public void SetThingURIText()
    {
        var splitted = ThingURI.Split('/');
        var name = splitted[splitted.Length - 1];
        if (name == "spock")
        {
            name = "Tractor";
        } else if (name == "cherrybot")
        {
            name = "xArm 7";
        }
        var thingURIT = $"{name}";
        ThingURIText.GetComponent<TextMeshPro>().text = thingURIT;
    }


    /// <summary>
    ///  Updates the "Thunderboard ID" text in the ThunderboardInfoBox
    /// </summary>
    public void SetIDText()
    {
        var idT = $"ID:<space=6.7em> {ThunderboardID} ";
        IDText.GetComponent<TextMeshPro>().text = idT;
    }


    /// <summary>
    ///  Updates the "SerialNumber" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSerialNumberText()
    {
        var snT = $"No.: {SerialNumber} ";
        SerialNumberText.GetComponent<TextMeshPro>().text = snT;
    }

    /// <summary>
    ///  Updates the "Angle of Arrival" text in the ThunderboardInfoBox
    /// </summary>
    public void SetAngleText()
    {
        var angleT = $"Angle of Arrival:<space=4em> {AngleOfArrival}° ";
        AngleText.GetComponent<TextMeshPro>().text = angleT;
    }

    /// <summary>
    ///  Updates the "position" text in the ThunderboardInfoBox
    /// </summary>
    public void SetOffsetText()
    {
        var coords = $"Offset:<space=7.4em> {TBCurrentLocalOffsetInWorld} ";
        PositionText.GetComponent<TextMeshPro>().text = coords;
    }


    /// <summary>
    /// Updates the "bounding box coordinates" text in the ThunderboardInfoBox
    /// </summary>
    public void SetCoordiantesText()
    {
        var coords = $"BBox Pixel Coordinates:<space=1.5em> {BBoxPixelTopLeft} and {BBoxPixelBottomRight}";
        coords += $"\nBBox World Coordinates:<space=2em> {BBoxCameraTopLeft} and {BBoxCameraBottomRight}";
        CoordinatesText.GetComponent<TextMeshPro>().text = coords;
    }

    /// <summary>
    ///  Updates the "Sensor data" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSensorTemperatureText()
    {
        var text = $"Temperature:<space=2.8em> {SensorData.temperature} °C";
        SensorTextTemperature.GetComponent<TextMeshPro>().text = text;
    }   
    
    /// <summary>
    ///  Updates the "Sensor data" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSensorHumidityText()
    {
        var text = $"Humidity:<space=4.3em> {SensorData.humidity} %";
        SensorTextHumidity.GetComponent<TextMeshPro>().text = text;
    }

    

   

    /// <summary>
    /// Hides a given ThunderboardInfobox and deletes it's associated ThunderboardHandler.
    /// </summary>
    /// <param name="InfoBox"></param>
    public void RemoveInfoBox(GameObject InfoBox)
    {
        ThunderboardHandlerList.thunderboardHandlerList.RemoveAll(tbh => tbh.ThunderboardID == ThunderboardID);
        InfoBox.SetActive(false);
        Destroy(InfoBox);
    }

}
