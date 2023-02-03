using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class StaticDeviceHandler : MonoBehaviour
{
    [Header("General Scripts")]
    public ThunderboardHandlerList ThunderboardHandlerList;
    public PositionHandler PositionHandler;
    public ThingHandler ThingHandler;


    [Header("GameObjects")]
    public GameObject RedoAddDeviceText;
    public GameObject ButtonAddNewDevice;
    public GameObject ButtonRedoAddDevice;
    public GameObject ButtonConfirm;
    public GameObject ProgressIndicator;
    public GameObject CheckboxYolo;
    public GameObject CheckboxAoA;
    public GameObject ThunderboardInfoBoxOrbitalPrefab;


    public bool NewTBHfromYoloWasSuccessful;
    public bool NewPositionSuccessful;

    // Start is called before the first frame update
    void Start()
    {
        NewTBHfromYoloWasSuccessful = true;
        NewPositionSuccessful = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!NewTBHfromYoloWasSuccessful)
        {
            // display warning message
            NewTBHfromYoloWasSuccessful = true; 
        }

        /*
        if (NewPositionSuccessful)
        {
            ThunderboardHandlerList.handleIncomingYoloResult = false;
            ThunderboardHandlerList.handleIncomingAoAResult = false;
            ProgressIndicator.SetActive(false);
            ProgressIndicator.GetComponent<ProgressIndicatorObjectDisplay>().enabled = false;
            RedoAddDeviceText.SetActive(true);
            ButtonRedoAddDevice.SetActive(true);
            ButtonConfirm.SetActive(true);
            
        }
        //*/
        
    }

    private void AfterNewPositionSuccessful()
    {
        ThunderboardHandlerList.handleIncomingYoloResult = false;
        ThunderboardHandlerList.handleIncomingAoAResult = false;
        ProgressIndicator.SetActive(false);
        ProgressIndicator.GetComponent<ProgressIndicatorObjectDisplay>().enabled = false;
        RedoAddDeviceText.SetActive(true);
        ButtonRedoAddDevice.SetActive(true);
        ButtonConfirm.SetActive(true);
    }



    public void AddNewDevice(bool isReDo = false)
    {

        if (isReDo)
        {
            var lastTBH = ThunderboardHandlerList.thunderboardHandlerList.Last();
            var infoPanel = lastTBH.ThunderboardInfoBox;
            infoPanel.SetActive(false);
            Destroy(infoPanel);
            ThunderboardHandlerList.thunderboardHandlerList.Remove(lastTBH); 
        }

        // turn on 
        // - get data from Yolo
        // - get data from MQTT
        var expectingAoA = CheckboxAoA.GetComponent<Interactable>().IsToggled;
        var expectingYolo = CheckboxYolo.GetComponent<Interactable>().IsToggled;
        ThunderboardHandlerList.expectingAoA = expectingAoA;
        ThunderboardHandlerList.expectingYolo = expectingYolo;

        string log = "--- AddNewDevice ---";
        log += $"\n expectingAoA: {expectingAoA}";
        log += $"\n expectingYolo:{expectingYolo}";

        ThunderboardHandlerList.handleIncomingYoloResult = true;
        ThunderboardHandlerList.handleIncomingAoAResult = true;
        ThunderboardHandlerList.ListOfCurrentNewOffsets = new List<(string idORuri, Vector3 offset)>();
        ThunderboardHandlerList.ListOfCurrentNewAoAs = new List<(string idORuri, float aoa)>();
        ThunderboardHandlerList.numberOfAoAsReceivedForCurrentDevice = 0;
        ThunderboardHandlerList.numberOfYoloCoordinatesReceivedForCurrentDevice = 0;
        PositionHandler.RaycastCounter = 0;

        Debug.Log(log);
        // for Yolo
        // - take the first incoming bounding box
        // - raycast etc
        // - turn yolo off


        // for MQTT 
        // - take first 10 (?) values? 
        // - average them
        // - calc positions etc
        // - then turn it off


    }

    public void SetTBPositionForStaticDevice()
    {

        string log = "--- SetTBPositionForStaticDevice ---";

        

        log += $"\ncreating TBH.";
        var newPrefabInstance = Instantiate(ThunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        newPrefabInstance.transform.localRotation = Quaternion.identity;
        newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
        var tbh = newPrefabInstance.GetComponent<ThunderboardHandler>();
        // newTBH.ThunderboardID = "undefined";
        // var newOffset = Vector3.Lerp(bBoxCameraSpaceTL, bBoxCameraSpaceBR, 0.5f);
        // newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
        // matchingTBH.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = newOffset;
        tbh.SerialNumber = ThunderboardHandlerList.tbhCounter;
        tbh.ThunderboardInfoBox.SetActive(false);
        ThunderboardHandlerList.thunderboardHandlerList.Add(tbh);
        log += $"\nadded new tbh: {ThunderboardHandlerList.thunderboardHandlerList[ThunderboardHandlerList.thunderboardHandlerList.Count - 1]}";
        ThunderboardHandlerList.tbhCounter++;

        log += $"\nmatchingTBH: {tbh}";
        // log += $"\nmatchingTBH ID: {matchingTBH.ThunderboardID}";
        //log += $"\nmatchingTBH URI: {matchingTBH.ThingURI}";


        var offsetList = ThunderboardHandlerList.ListOfCurrentNewOffsets;
        var id = "";
        var uri = "";
        var yoloOffsetList = new List<Vector3>();
        var aoaOffsetList = new List<Vector3>();

        foreach (var item in offsetList)
        {
            if (item.idORuri.StartsWith("http"))
            {
                uri = item.idORuri;
                yoloOffsetList.Add(item.offset);
            } else
            {
                id = item.idORuri;
                aoaOffsetList.Add(item.offset);
                log += $"\nitem offset: {item.offset}";
            }
        }

        if (ThunderboardHandlerList.ListOfCurrentNewAoAs.Count >0)
        {           
            var angleSum = 0f;
            foreach (var item in ThunderboardHandlerList.ListOfCurrentNewAoAs)
            {
                angleSum += item.aoa;
            }
            var avgAngle = angleSum/ ThunderboardHandlerList.ListOfCurrentNewAoAs.Count;
            log += $"\navgAngle: {avgAngle}";

            var offsetFromAvgAngle = PositionHandler.CalculateLocalOffsetFromAngle(avgAngle);
            log += $"\noffsetFromAvgAngle: {offsetFromAvgAngle}";

            tbh.AngleOfArrival = avgAngle;
            tbh.SetAngleText();

        }

        
        tbh.ThingURI = uri;
        tbh.ThunderboardID = id;

        if (id != "")
        {
            if (id == "60A423C98BF1")
            {
                tbh.thingIP = "10.2.2.240";
            }

            tbh.GetDataFromThing = true;
            
        }

        tbh.SetIDText();
        tbh.SetThingURIText();



        var lastYoloOffset = new Vector3(0, 0, 0);
        Vector3 avgYoloOffset = new Vector3(0, 0, 0);
        var yoloOffsetListContainsElements = yoloOffsetList.Any(i => i != null);

        if (yoloOffsetListContainsElements)
        {
            avgYoloOffset = new Vector3(
              yoloOffsetList.Average(x => x.x),
              yoloOffsetList.Average(x => x.y),
              yoloOffsetList.Average(x => x.z));
             yoloOffsetList.Last();
        }

        Vector3 avgAoAOffset = new Vector3(0, 0, 0);
        var aoaOffsetListContainsElements = aoaOffsetList.Any(i => i != null);
        if (aoaOffsetListContainsElements)
        {
            avgAoAOffset = new Vector3(
             aoaOffsetList.Average(x => x.x),
             aoaOffsetList.Average(x => x.y),
             aoaOffsetList.Average(x => x.z));
        }

        

        Vector3 newOffset = new Vector3();

        if (yoloOffsetListContainsElements && aoaOffsetListContainsElements)
        {
            newOffset = new Vector3(Mathf.Lerp(avgYoloOffset.x, avgAoAOffset.x, 0.3f), avgYoloOffset.y, Mathf.Lerp(avgYoloOffset.z, avgAoAOffset.z, 0.3f));
        } else if (yoloOffsetListContainsElements && !aoaOffsetListContainsElements)
        {
            if (Vector3.Distance(lastYoloOffset, avgYoloOffset) > 0.5)
            {
                newOffset = lastYoloOffset;
            } else
            {
                newOffset = avgYoloOffset;
            }
            
            // 
        }
        else if (!yoloOffsetListContainsElements  && aoaOffsetListContainsElements)
        {
            newOffset = avgAoAOffset;
        }

        log += $"\nnewOffset: {newOffset}";
        log += $"\navgYoloOffset: {avgYoloOffset}";
        log += $"\nlastYoloOffset: {lastYoloOffset}";

        tbh.ThunderboardInfoBox.GetComponent<Billboard>().enabled = false;

        tbh.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset = newOffset;
        tbh.ThunderboardInfoBox.SetActive(true);

        tbh.ThunderboardInfoBox.GetComponent<Billboard>().enabled = true;

        // StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curOffset, newOffset, billboard));
        
        tbh.TBCurrentLocalOffsetInWorld = newOffset;
        tbh.SetOffsetText();
        AfterNewPositionSuccessful();
        Debug.Log(log);


    }

    IEnumerator StartGettingDataFromThingStaticDevice(ThunderboardHandler tbh)
    {
        if (tbh.thingIP == "")
        {
            yield return null;
        }
        Debug.Log($"GetDataFromThing: {tbh.GetDataFromThing}");
        while (tbh.GetDataFromThing)
        {
            Debug.Log("StartGettingDataFromThing --- start ---");
            tbh.GetDataFromThingCourutineRunning = true;
            Task task = ThingHandler.GetBatteryVoltageFromThing(tbh.thingIP, tbh);
            Task task2 = ThingHandler.GetSoilconditionFromThing(tbh.thingIP, tbh);
            yield return new WaitUntil(() => task.IsCompleted && task2.IsCompleted);
            if (tbh.BatteryVoltage != 0f)
            {
                Debug.Log("new battery voltage");
                tbh.SetBatteryVoltageText();
                tbh.TractorInfo.SetActive(true);
            }

            if (tbh.SoilCondition.ph != 0f || tbh.SoilCondition.moisture != 0f || tbh.SoilCondition.density != 0f || tbh.SoilCondition.nitrate != 0f)
            {
                Debug.Log("new soil condition");
                tbh.SetSoilconditionText();
                tbh.Soil.SetActive(true);
                tbh.TractorInfo.SetActive(true);
            }
            else
            {
                tbh.Soil.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
        tbh.GetDataFromThingCourutineRunning = false;
    }




}
