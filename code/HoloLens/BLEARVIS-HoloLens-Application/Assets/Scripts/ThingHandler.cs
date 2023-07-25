using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ThingHandler : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void StartGettingDataFromThingStarter(ThunderboardHandler tbh)
    {
        Debug.Log("-- StartGettingDataFromThingStarter");
        tbh.GetDataFromThing = true;
        StartCoroutine(StartGettingDataFromThing(tbh));
    }

    public void StopGettingDataFromThing(ThunderboardHandler tbh)
    {

    }

    /// <summary>
    /// Continously gets data from a Thing, every 1s.
    /// Here: get battery voltage and soil condition from a tractorbot.
    /// </summary>
    /// <returns></returns>
    IEnumerator StartGettingDataFromThing(ThunderboardHandler tbh)
    {
        if (tbh.thingIP == "")
        {
            Debug.Log("No IP set!");
            yield return null;
        }
        Debug.Log($"GetDataFromThing: {tbh.GetDataFromThing}");

        while (tbh.GetDataFromThing)
        {
            Debug.Log("StartGettingDataFromThing --- start ---");
            tbh.GetDataFromThingCourutineRunning = true;
            Task task = GetBatteryVoltageFromThing(tbh.thingIP, tbh);
            Task task2 = GetSoilconditionFromThing(tbh.thingIP, tbh);
            Task task3 = GetWaterLevelFromThing(tbh.thingIP, tbh);
            Task task4 = GetMotorCurrentFromThing(tbh.thingIP, tbh);
            yield return new WaitUntil(() => task.IsCompleted && task2.IsCompleted && task3.IsCompleted && task4.IsCompleted);

            if (tbh.BatteryVoltage != 0f)
            {
                Debug.Log("new battery voltage");
                tbh.BatteryText.GetComponent<TextMeshPro>().text = $"{tbh.BatteryVoltage} V";
                tbh.TractorInfo.SetActive(true);
                tbh.BatteryText.SetActive(true);

            }

            if (tbh.WaterLevel != 0f)
            {
                Debug.Log("new water level");
                tbh.WaterText.GetComponent<TextMeshPro>().text = $"{tbh.WaterLevel} L";
                tbh.WaterText.SetActive(true);
                tbh.TractorInfo.SetActive(true);
            }
            if (tbh.MotorCurrent != 0f && tbh.thingIP == "10.2.2.240")
            {
                tbh.MotorText.GetComponent<TextMeshPro>().text = $"{tbh.MotorCurrent} A";
                Debug.Log("new motor current");
                tbh.Motor.SetActive(true);
                tbh.TractorInfo.SetActive(true);
            } else
            {
                tbh.Motor.SetActive(false);
            }

            if (tbh.SoilCondition.ph != 0f || tbh.SoilCondition.moisture != 0f || tbh.SoilCondition.density != 0f || tbh.SoilCondition.nitrate != 0f)
            {
                var text = $"{tbh.SoilCondition.ph}\n{tbh.SoilCondition.moisture}\n{tbh.SoilCondition.density}\n{tbh.SoilCondition.nitrate}";
                tbh.SoilText.GetComponent<TextMeshPro>().text = text;
                Debug.Log("new soil condition");
                tbh.TractorInfo.SetActive(true);
                tbh.Soil.SetActive(true);
            }
            else
            {
                tbh.Soil.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }
        tbh.GetDataFromThingCourutineRunning = false;
    }

    public async Task GetBatteryVoltageFromThing(string thingIP, ThunderboardHandler tbh)
    {
        var url = $"http://{thingIP}/properties/batteryvoltage";

        var result = await SendGetRequestToThing(url);
        float.TryParse(result, out float batteryVoltage);
        Debug.Log($"Received new battery voltage: {batteryVoltage}");
        tbh.BatteryVoltage = batteryVoltage;
    }

    public async Task GetWaterLevelFromThing(string thingIP, ThunderboardHandler tbh)
    {
        Debug.Log("-- GetWaterLevelFromThing");
        var url = $"http://{thingIP}/properties/waterlevel";

        var result = await SendGetRequestToThing(url);
        int.TryParse(result, out int waterLevel);
        Debug.Log($"Received new water level: {waterLevel}");
        tbh.WaterLevel = waterLevel;
    }

    

    public async Task GetMotorCurrentFromThing(string thingIP, ThunderboardHandler tbh)
    {
        Debug.Log("-- GetMotorCurrentFromThing");
        var url = $"http://{thingIP}/properties/motorcurrent";

        var result = await SendGetRequestToThing(url);
        float.TryParse(result, out float motorcurrent);
        Debug.Log($"Received new motorcurrent: {motorcurrent}");
        tbh.MotorCurrent = motorcurrent;
    }


    public async Task GetSoilconditionFromThing(string thingIP, ThunderboardHandler tbh)
    {
        var url = $"http://{thingIP}/properties/soilcondition";

        var result = await SendGetRequestToThing(url);

        var node = QueryResultToJson(result);
        if (node.Count > 0)
        {
            Debug.Log($"received new soilcondition, ph: {node["ph"]}");
            //(int ph, int moisture, int density, int nitrate) SoilCondition;
            tbh.SoilCondition.ph = node["ph"];
            tbh.SoilCondition.moisture = node["moisture"];
            tbh.SoilCondition.density = node["density"];
            tbh.SoilCondition.nitrate = node["nitrate"];  
        }   
    }

    public JSONNode QueryResultToJson(string result)
    {
        JSONNode items = JSON.Parse(result);
        return items;
    }

    public async Task<string> SendGetRequestToThing(string url)
    {
        UnityWebRequest www = new UnityWebRequest(url, "GET");

        //byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(json);
        //www.uploadHandler = (UploadHandler)new UploadHandlerRaw(encodedPayload);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        //www.SetRequestHeader("Content-Type", "application/json");
        //www.certificateHandler = null;

        await www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
            var res = www.GetResponseHeaders();

            foreach (var pair in res)
            {
                Debug.Log($"Response {pair.Key}: {pair.Value}");
            }
            return "";
        }
        else
        {
            Debug.Log("GetQuery: worked!");
            //File.Delete(filePath);

            return www.downloadHandler.text;

        }
    }

}
