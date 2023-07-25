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
    public GameObject CheckboxMultiple;
    public GameObject ThunderboardInfoBoxOrbitalPrefab;
    public GameObject Menu;



    public bool NewTBHfromYoloWasSuccessful;
    public bool NewPositionSuccessful;
    public float MaxDistanceToThing;

    // Start is called before the first frame update
    void Start()
    {
        NewTBHfromYoloWasSuccessful = true;
        NewPositionSuccessful = false;
        MaxDistanceToThing = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!NewTBHfromYoloWasSuccessful)
        {
            // display warning message
            NewTBHfromYoloWasSuccessful = true; 
        }
        
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
        ButtonAddNewDevice.SetActive(false);
        Menu.SetActive(true);
    }



    public void AddNewDevice(bool isReDo = false)
    {

        if (isReDo && ThunderboardHandlerList.thunderboardHandlerList.Count > 0)
        {
            
            var lastTBH = ThunderboardHandlerList.thunderboardHandlerList.Last();
            var infoBox = lastTBH.ThunderboardInfoBox;
            infoBox.SetActive(false);
            Destroy(infoBox);
            ThunderboardHandlerList.thunderboardHandlerList.Remove(lastTBH); 
            if (ThunderboardHandlerList.expectingMultipleThings)
            {
                // remove also the second last added thing
                lastTBH = ThunderboardHandlerList.thunderboardHandlerList.Last();
                infoBox = lastTBH.ThunderboardInfoBox;
                infoBox.SetActive(false);
                Destroy(infoBox);
                ThunderboardHandlerList.thunderboardHandlerList.Remove(lastTBH);
            }
        }

        // turn on 
        // - get data from Yolo
        // - get data from MQTT
        var expectingAoA = CheckboxAoA.GetComponent<Interactable>().IsToggled;
        var expectingYolo = CheckboxYolo.GetComponent<Interactable>().IsToggled;
        var expectingMultipleThings = CheckboxMultiple.GetComponent<Interactable>().IsToggled;
        ThunderboardHandlerList.expectingAoA = expectingAoA;
        ThunderboardHandlerList.expectingYolo = expectingYolo;
        ThunderboardHandlerList.expectingMultipleThings = expectingMultipleThings;

        string log = "--- AddNewDevice ---";
        log += $"\n expectingAoA: {expectingAoA}";
        log += $"\n expectingYolo:{expectingYolo}";

        ThunderboardHandlerList.handleIncomingYoloResult = true;
        ThunderboardHandlerList.handleIncomingAoAResult = true;
        ThunderboardHandlerList.ListOfCurrentNewOffsets = new Dictionary<string, List<Vector3>>();
        ThunderboardHandlerList.ListOfCurrentNewAoAs = new Dictionary<string, List<float>>();
        ThunderboardHandlerList.DictOfAoaOffsetLists = new Dictionary<string, List<Vector3>>();
        ThunderboardHandlerList.DictOfYoloOffsetLists = new Dictionary<string, List<Vector3>>();
        ThunderboardHandlerList.numberOfAoAsReceivedForCurrentDevices = 0;
        ThunderboardHandlerList.numberOfYoloCoordinatesReceivedForCurrentDevices = 0;
        PositionHandler.RaycastCounter = 0;

        Debug.Log(log);

    }

    private Vector3 AverageOfVector3List(List<Vector3> list)
    {
        return new Vector3(
                 list.Average(x => x.x),
                 list.Average(x => x.y),
                 list.Average(x => x.z));
    }

    /// <summary>
    /// If there are two Things in the scene from the same class, 
    /// this function separates all points into an averaged centroid for each thing.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private (string uri, List<List<Vector3>> lists) SeparateListOfVector3IfThereAreMultipleObjects(string uri, List<Vector3> list)
    {
        var listForFirstThing = new List<Vector3>();
        var listForSecondThing = new List<Vector3>();
        string log = "\n SeparateVectorsIfThereAreMultipleObjects ---";

        log += $"\nMaxDistanceToThing: {MaxDistanceToThing}";
        listForFirstThing.Add(list[0]);

        for (int i = 1; i < list.Count - 1; i++)
        {

            var distToFirstList = (AverageOfVector3List(listForFirstThing) - list[i]).sqrMagnitude;
            log += $"\ndistToFirstList ({i}): {distToFirstList}";

            if (listForSecondThing.Count > 0)
            {
                log += "\ncompare between 1. and 2.";
                // if we have a point in the second list take the one with the smaller distance to the current point
                var distToSecondList = (AverageOfVector3List(listForSecondThing) - list[i]).sqrMagnitude;
                log += $"\ndistToFirstList ({i}): {distToFirstList}";

                if (distToFirstList < distToSecondList)
                {
                    listForFirstThing.Add(list[i]);
                } else {
                    listForSecondThing.Add(list[i]);
                }
            } else  if (distToFirstList > MaxDistanceToThing)
            {
                log += "\nin 2.";
                // if not add it to the second list
                listForSecondThing.Add(list[i]);
            } else 
            {
                log += "\nin 1.";
                // otherwise check with the threshold if the current point is closer to the first list than the threshold.
                listForFirstThing.Add(list[i]);
            }
        }

        Debug.Log(log);
        var lists = new List<List<Vector3>> { listForFirstThing, listForSecondThing };
        return (uri, lists);
    }

    private ThunderboardHandler CreateNewTBH()
    {
        var newPrefabInstance = Instantiate(ThunderboardInfoBoxOrbitalPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        newPrefabInstance.transform.localRotation = Quaternion.identity;
        newPrefabInstance.transform.localScale = new Vector3(1, 1, 1);
        var tbh = newPrefabInstance.GetComponent<ThunderboardHandler>();

        tbh.SerialNumber = ThunderboardHandlerList.thunderboardHandlerList.Count + 1;
        // tbh.SetSerialNumberText();
        tbh.ThunderboardInfoBox.SetActive(true);
        ThunderboardHandlerList.thunderboardHandlerList.Add(tbh);
        
        ThunderboardHandlerList.tbhCounter++;
        return tbh;
    }

    /// <summary>
    /// For new incoming values separate the vectors in 1 or 2 devices, 
    /// then calculate the position of the MR panel(s).
    /// </summary>
    public void SetTBPositionForStaticDevices()
    {

        // If AoA is faster in collecting data points, wait for Yolo.
        // The Yolo function will call this function again once it has enough data points
        if (ThunderboardHandlerList.expectingYolo && ThunderboardHandlerList.ListOfCurrentNewOffsets.Select(x => x.Key.StartsWith("http")).Count() < 0)
        {
            return;
        }

        string log = "--- SetTBPositionForStaticDevice ---";

        var offsetList = ThunderboardHandlerList.ListOfCurrentNewOffsets;
      
        var yoloOffsetList = new List<(string uri, Vector3 offset)>();
        var aoaOffsetList = new List<(string id, Vector3 offset)>();
        // var listOfAoaOffsetLists = new List<(int index, List<(string id, Vector3 offset)>)>();
        var dictOfAoaOffsetLists = ThunderboardHandlerList.DictOfAoaOffsetLists;
        var dictOfYoloOffsetLists = ThunderboardHandlerList.DictOfYoloOffsetLists;

        var lastYoloOffset = new Vector3(0, 0, 0);
        var dictOfAVERAGEAoaOffsetLists = new Dictionary<string, Vector3>();
        var dictOfAVERAGEYoloOffsetLists = new Dictionary<string, Vector3>();

        // for yolo: 
        // - each class: see if points come from multiple devices BEFORE averaging
        // - see how many devices are in scene, only if there are 2 do this

        foreach (var list in dictOfYoloOffsetLists)
        {

            var yoloOffsetListContainsAnyElements = list.Value.Any(i => i != null);

            if (yoloOffsetListContainsAnyElements)
            {
                log += $"\nnumberOfThingsCurrentlyInScene: {ThunderboardHandlerList.numberOfThingsCurrentlyInScene}";
                if (ThunderboardHandlerList.expectingMultipleThings)
                {
                    log += $"\n multiple things in scene";
                    var separatedYoloOffsets = SeparateListOfVector3IfThereAreMultipleObjects(list.Key, list.Value);
                    log += $"\nfirst list: {separatedYoloOffsets.lists[0][0]}";
                    log += $"\nseparatedYoloOffsets.lists.Count: {separatedYoloOffsets.lists.Count}";
                    log += $"\nseparatedYoloOffsets.lists[0].Count: {separatedYoloOffsets.lists[0].Count}";
                   
                    var i = 0;
                    foreach (var sublist in separatedYoloOffsets.lists)
                    {
                        log += $"\nnew list [{i}]:";
                        foreach (var obj in sublist)
                        {
                            log += $"\nitem: {obj}";
                        }
                        log += "\n----------";
                        i++;
                    }


                    if (separatedYoloOffsets.lists[0].Count > 0)
                    {
                        log += $"\nfirst thing: {separatedYoloOffsets.uri}";
                        var avg = AverageOfVector3List(separatedYoloOffsets.lists[0]);
                        dictOfAVERAGEYoloOffsetLists.Add(separatedYoloOffsets.uri + "_0", avg);
                    }
                    if (separatedYoloOffsets.lists[1].Count > 0)
                    {
                        log += $"\nsecond list: {separatedYoloOffsets.lists[1][0]}";
                        log += $"\nsecond thing: {separatedYoloOffsets.uri}";
                        log += $"\nseparatedYoloOffsets.lists[1].Count: {separatedYoloOffsets.lists[1].Count}";
                        var avg = AverageOfVector3List(separatedYoloOffsets.lists[1]);
                        dictOfAVERAGEYoloOffsetLists.Add(separatedYoloOffsets.uri + "_1", avg);
                    }
                } else
                {
                    var avg = AverageOfVector3List(list.Value);
                    dictOfAVERAGEYoloOffsetLists.Add(list.Key, avg);
                }
              

            }
        }

        foreach (var list in dictOfAoaOffsetLists)
        {
            var aoaOffsetListContainsElements = list.Value.Any(i => i != null);

            if (aoaOffsetListContainsElements)
            {
                var avg = AverageOfVector3List(list.Value);
                //  yoloOffsetList.Last();
                dictOfAVERAGEAoaOffsetLists.Add(list.Key, avg);
            }
        }

        Vector3 newOffset = new Vector3();
        var id = "";
        var uri = "";

        log += $"\nnumber of avg AoA Offsets: {dictOfAVERAGEAoaOffsetLists.Count}";
        log += $"\nnumber of avg Yolo Offsets: {dictOfAVERAGEYoloOffsetLists.Count}";

        if (ThunderboardHandlerList.expectingMultipleThings)
        {
            // IF there are multiple objects (n=2) in the scene,
            // we assume there are two objects in the scene.
            log += $"\n multiple things in scene -> now creating TBHs";
            var firstYoloURI = "";
            var firstYoloAvgOffset = new Vector3(0,0,0);

            var secondYoloURI = "";
            var secondYoloAvgOffset = new Vector3(0, 0, 0);

            var i = 0;
            foreach (var item in dictOfAVERAGEYoloOffsetLists)
            {
                if (i == 0)
                {
                    firstYoloURI = item.Key;
                    firstYoloAvgOffset = item.Value;
                } else if (i == 1)
                {
                    secondYoloURI = item.Key;
                    secondYoloAvgOffset = item.Value;
                }
                i++;
            }

            var firstAoAID = "";
            var firstAoAAvgOffset = new Vector3(0, 0, 0);

            var secondAoAID = "";
            var secondAoAAvgOffset = new Vector3(0, 0, 0);

            i = 0;
            foreach (var item in dictOfAVERAGEAoaOffsetLists)
            {
                if (i == 0)
                {
                    firstAoAID = item.Key;
                    firstAoAAvgOffset = item.Value;
                }
                else if (i == 1)
                {
                    secondAoAID = item.Key;
                    secondAoAAvgOffset = item.Value;
                }
                i++;
            }

            var distFirstYoloFirstAoA = (firstYoloAvgOffset - firstAoAAvgOffset).sqrMagnitude;
            var distFirstYoloSecondAoA = (firstYoloAvgOffset - secondAoAAvgOffset).sqrMagnitude;
            var distSecondYoloFirstAoA = (secondYoloAvgOffset - firstAoAAvgOffset).sqrMagnitude;
            var distSecondYoloSecondAoA = (secondYoloAvgOffset - secondAoAAvgOffset).sqrMagnitude;

            var firstThing = ("", "", new Vector3(0, 0, 0));
            var secondThing = ("", "", new Vector3(0, 0, 0));

            if (distFirstYoloFirstAoA > distFirstYoloSecondAoA && distSecondYoloFirstAoA < distSecondYoloSecondAoA 
                && ThunderboardHandlerList.expectingAoA && ThunderboardHandlerList.expectingYolo)
            {
                firstThing.Item1 = firstYoloURI;
                firstThing.Item2 = firstAoAID;
                firstThing.Item3 = new Vector3(Mathf.Lerp(firstYoloAvgOffset.x, firstAoAAvgOffset.x, 0.3f), firstYoloAvgOffset.y, Mathf.Lerp(firstYoloAvgOffset.z, firstAoAAvgOffset.z, 0.3f));
                secondThing.Item1 = secondYoloURI;
                secondThing.Item2 = secondAoAID;
                secondThing.Item3 = new Vector3(Mathf.Lerp(secondYoloAvgOffset.x, secondAoAAvgOffset.x, 0.3f), secondYoloAvgOffset.y, Mathf.Lerp(secondYoloAvgOffset.z, secondAoAAvgOffset.z, 0.3f));
            } else if (ThunderboardHandlerList.expectingAoA && ThunderboardHandlerList.expectingYolo)
            {
                firstThing.Item1 = firstYoloURI;
                firstThing.Item2 = secondAoAID;
                firstThing.Item3 = new Vector3(Mathf.Lerp(firstYoloAvgOffset.x, secondAoAAvgOffset.x, 0.3f), firstYoloAvgOffset.y, Mathf.Lerp(secondAoAAvgOffset.z, secondAoAAvgOffset.z, 0.3f));
                secondThing.Item1 = secondYoloURI;
                secondThing.Item2 = firstAoAID;
                secondThing.Item3 = new Vector3(Mathf.Lerp(secondYoloAvgOffset.x, firstAoAAvgOffset.x, 0.3f), secondYoloAvgOffset.y, Mathf.Lerp(secondYoloAvgOffset.z, firstAoAAvgOffset.z, 0.3f));
            } else if (ThunderboardHandlerList.expectingAoA && !ThunderboardHandlerList.expectingYolo)
            {
                firstThing.Item1 = "";
                firstThing.Item2 = firstAoAID;
                firstThing.Item3 = firstAoAAvgOffset;
                secondThing.Item1 = "";
                secondThing.Item2 = secondAoAID;
                secondThing.Item3 = secondAoAAvgOffset;
            } else if (!ThunderboardHandlerList.expectingAoA && ThunderboardHandlerList.expectingYolo)
            {
                firstThing.Item1 = firstYoloURI;
                firstThing.Item2 = "";
                firstThing.Item3 = firstYoloAvgOffset;
                secondThing.Item1 = secondYoloURI;
                secondThing.Item2 = "";
                secondThing.Item3 = secondYoloAvgOffset;
            }

            var bothThings = new List<(string, string, Vector3)>() { firstThing, secondThing };

            foreach (var thing in bothThings)
            {
                var tbh = CreateNewTBH();
                log += $"\nadded new tbh: {ThunderboardHandlerList.thunderboardHandlerList[ThunderboardHandlerList.thunderboardHandlerList.Count - 1]}";

                // the URI might have an index at the end like "_0". We delete it here again
                tbh.ThingURI = thing.Item1.Split('_')[0];
                tbh.ThunderboardID = thing.Item2;
                newOffset = thing.Item3;

                if (newOffset == new Vector3(0,0,0))
                {
                    continue;
                }
                log += $"\nnewOffset: {newOffset}";
                log += $"\ntbh.ThunderboardID: {tbh.ThunderboardID}";

                if (tbh.ThunderboardID != "")
                {
                    if (ThunderboardHandlerList.thunderboardHandlerList.Count == 1)
                    {
                        ThunderboardHandlerList.MACofFirstBLETag = id;
                        log += $"\nThunderboardHandlerList.MACofFirstBLETag: {ThunderboardHandlerList.MACofFirstBLETag}";
                    }
                    if (tbh.ThunderboardID == "60A423C98BF1")
                    {
                        // Spock
                        tbh.thingIP = "10.2.2.157";
                        log += $"\nid: {id} and ip: {tbh.thingIP}";
                    }
                    else if (tbh.ThunderboardID == "588E81440788")
                    {
                        tbh.thingIP = "10.2.2.240";
                        log += $"\nid: {id} and ip: {tbh.thingIP}";
                    }
                    log += $"\nip: {tbh.thingIP}";
                    tbh.ThingHandler.StartGettingDataFromThingStarter(tbh);
                }

                tbh.SetIDText();
                tbh.SetThingURIText();
                var orbital = tbh.ThunderboardInfoBox.GetComponent<Orbital>();
                var billboard = tbh.ThunderboardInfoBox.GetComponent<Billboard>();
                billboard.enabled = false;
                orbital.enabled = true;
                newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
                var curOffset = tbh.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset;
                log += "\nabout to move orbital offset";
                log += $"\nOrbital status before: {orbital.isActiveAndEnabled}";
                StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curOffset, newOffset, billboard));
                log += $"\nOrbital status after: {orbital.isActiveAndEnabled}";
                if (orbital.LocalOffset == new Vector3(0,0,0) || orbital.LocalOffset == curOffset)
                {
                    log += "\ncorrected offset";
                    orbital.enabled = true;
                    orbital.LocalOffset = newOffset;
                    orbital.enabled = false;
                }
               
                tbh.TBCurrentLocalOffsetInWorld = newOffset;
                tbh.ThunderboardInfoBox.SetActive(true);

                tbh.ThunderboardInfoBox.GetComponent<Billboard>().enabled = true;

                tbh.TBCurrentLocalOffsetInWorld = newOffset;
                tbh.SetOffsetText();
                
            }


        }
        else
        {
            // If there is only one object in the scene
            if (dictOfAVERAGEAoaOffsetLists.Count > 0 && dictOfAVERAGEYoloOffsetLists.Count > 0)
            {
                var avgAoAOffset = dictOfAVERAGEAoaOffsetLists.First().Value;
                var avgYoloOffset = dictOfAVERAGEYoloOffsetLists.First().Value;
                newOffset = new Vector3(Mathf.Lerp(avgYoloOffset.x, avgAoAOffset.x, 0.3f), avgYoloOffset.y, Mathf.Lerp(avgYoloOffset.z, avgAoAOffset.z, 0.3f));
                uri = dictOfAVERAGEYoloOffsetLists.First().Key;
                id = dictOfAVERAGEAoaOffsetLists.First().Key;
            }
            else if (dictOfAVERAGEAoaOffsetLists.Count <= 0 && dictOfAVERAGEYoloOffsetLists.Count > 0)
            {
                // if we only have information from Yolo
                var avgYoloOffset = dictOfAVERAGEYoloOffsetLists.First().Value;
                newOffset = avgYoloOffset;
                uri = dictOfAVERAGEYoloOffsetLists.First().Key;
            }
            else if (dictOfAVERAGEAoaOffsetLists.Count > 0 && dictOfAVERAGEYoloOffsetLists.Count <= 0)
            {
                // if we only have information from AoA
                var avgAoAOffset = dictOfAVERAGEAoaOffsetLists.First().Value;
                newOffset = avgAoAOffset;
                id = dictOfAVERAGEAoaOffsetLists.First().Key;
            }
            var tbh = CreateNewTBH();
            log += $"\nadded new tbh: {ThunderboardHandlerList.thunderboardHandlerList[ThunderboardHandlerList.thunderboardHandlerList.Count - 1]}";

            tbh.ThingURI = uri;
            tbh.ThunderboardID = id;

            if (tbh.ThunderboardID != "")
            {
                if (ThunderboardHandlerList.thunderboardHandlerList.Count == 1)
                {
                    ThunderboardHandlerList.MACofFirstBLETag = id;
                    log += $"\nThunderboardHandlerList.MACofFirstBLETag: {ThunderboardHandlerList.MACofFirstBLETag}";
                }
                if (id == "60A423C98BF1")
                {
                    tbh.thingIP = "10.2.2.157";
                    log += $"id: {id} and ip: {tbh.thingIP}";
                } else if (id == "588E81440788")
                {
                    tbh.thingIP = "10.2.2.240";
                    log += $"id: {id} and ip: {tbh.thingIP}";
                }

                StartCoroutine(StartGettingDataFromThingStaticDevice(tbh));

            }
            tbh.SetIDText();

            tbh.SetThingURIText();

            var orbital = tbh.ThunderboardInfoBox.GetComponent<Orbital>();
            var billboard = tbh.ThunderboardInfoBox.GetComponent<Billboard>();
            billboard.enabled = false;
            orbital.enabled = true;
            newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
            var curOffset = tbh.ThunderboardInfoBox.GetComponent<Orbital>().LocalOffset;
            log += $"curOffset: {curOffset}";
            StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curOffset, newOffset, billboard));
            tbh.ThunderboardInfoBox.SetActive(true);

            tbh.ThunderboardInfoBox.GetComponent<Billboard>().enabled = true;

            // StartCoroutine(PositionHandler.MoveLocalOffset(orbital, curOffset, newOffset, billboard));

            tbh.TBCurrentLocalOffsetInWorld = newOffset;
            tbh.SetOffsetText();



        }
            log += $"\ncreating TBH.";

        AfterNewPositionSuccessful();

        Debug.Log(log);
    }

    /// <summary>
    /// Gets data continuously from a Thing using the Thing's URL
    /// </summary>
    /// <param name="tbh">The TBH associated with the Thing</param>
    /// <returns></returns>
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
            Task task3 = ThingHandler.GetWaterLevelFromThing(tbh.thingIP, tbh);
            yield return new WaitUntil(() => task.IsCompleted && task2.IsCompleted && task3.IsCompleted);
            if (tbh.BatteryVoltage != 0f)
            {
                Debug.Log("new battery voltage");
                tbh.SetBatteryVoltageText();
                tbh.TractorInfo.SetActive(true);
            }
            if (tbh.WaterLevel != 0f)
            {
                Debug.Log("new water level");
                tbh.WaterText.SetActive(true);
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
