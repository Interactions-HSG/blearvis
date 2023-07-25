using Microsoft.MixedReality.Toolkit.UI;
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
    public GameObject WaterText;
    public GameObject MotorText;
    public GameObject Motor;

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
    public int WaterLevel;
    public float MotorCurrent;
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
     
        if (GetDataFromThingCourutineRunning && GetDataFromThing)
        {
            GetDataFromThing = false;
            // StartCoroutine(StartGettingDataFromThing());

            if (ThunderboardID == "60A423C98BF1")
            {
                
                System.Random rnd = new System.Random();

                
                var comma = rnd.Next(0, 9);
                BatteryVoltage = float.Parse($"{rnd.Next(5, 10)}.{comma}");
                SetBatteryVoltageText();
                BatteryText.SetActive(true);

                SoilCondition.ph = rnd.Next(0,255);
                SoilCondition.nitrate = rnd.Next(0,255);
                SoilCondition.moisture = rnd.Next(0,255);
                SoilCondition.density = rnd.Next(0,255);
                SetSoilconditionText();
                Soil.SetActive(true);
                TractorInfo.SetActive(true);
                GetDataFromThingCourutineRunning = false;
            } else if (ThingURI == "tractorbot"){

                    System.Random rnd = new System.Random();

                    var comma = rnd.Next(0, 9);
                    BatteryVoltage = float.Parse($"{rnd.Next(5, 10)}.{comma}");
                    SetBatteryVoltageText();
                    BatteryText.SetActive(true);
                    TractorInfo.SetActive(true);
            }
        }
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
        
        Debug.Log($"ThunderboardHandlerList.MACofFirstBLETag: {ThunderboardHandlerList.MACofFirstBLETag}");
        Debug.Log($"ThunderboardID: {ThunderboardID}");

        if (sensorType == "temp" && ThunderboardHandlerList.MACofFirstBLETag == this.ThunderboardID)
        {
          
            // only update if the value has changed.
            if (SensorData.temperature == value) { return; }
            SensorData.temperature = value;
            Debug.Log($"Updated temperature: {value}");
            LastTemperatureSensorUpdate = DateTime.UtcNow;
            SetSensorTemperatureText();
        } 
        else if (sensorType == "hum" && ThunderboardHandlerList.MACofFirstBLETag != this.ThunderboardID)
        {
           
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
            SetNewPositionFromYolo = false;
        }
        else
        {
            var billboard = ThunderboardInfoBox.GetComponent<Billboard>();
            billboard.enabled = true;
            Debug.Log($"SetNewPositionFromYolo: {SetNewPositionFromYolo}");
        }
    }


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

            PositionHandler.UpdateLocalOffsetSensorFusion(this, orbital, billboard);
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
        //*
        var splitted = ThingURI.Split('/');
        var name = splitted[splitted.Length - 1];
        /*
        if (name == "spock")
        {
            name = "Tractor";
        } else if (name == "cherrybot")
        {
            name = "xArm 7";
        }
        //*/ 
        // var thingURIT = $"{name}";
        //var name = ThingURI;
        ThingURIText.GetComponent<TextMeshPro>().text = $"{name}";
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
        var snT = $"({SerialNumber})";
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
        SensorTextTemperature.SetActive(true);
        var text = $"Temperature:<space=2.8em> {SensorData.temperature} °C";
        SensorTextTemperature.GetComponent<TextMeshPro>().text = text;
    }   
    
    /// <summary>
    ///  Updates the "Sensor data" text in the ThunderboardInfoBox
    /// </summary>
    public void SetSensorHumidityText()
    {
        SensorTextHumidity.SetActive(true);
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
