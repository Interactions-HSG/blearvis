using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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


    public void StartGettingDataFromThing(ThunderboardHandler tbh)
    {
        tbh.GetDataFromThing = true;
    }

    public void StopGettingDataFromThing(ThunderboardHandler tbh)
    {

    }

    public async Task GetBatteryVoltageFromThing(string thingIP, ThunderboardHandler tbh)
    {
        var url = $"http://{thingIP}/properties/batteryvoltage";

        var result = await SendGetRequestToThing(url);
        float.TryParse(result, out float batteryVoltage);
        Debug.Log($"Received new battery voltage: {batteryVoltage}");
        tbh.BatteryVoltage = batteryVoltage;
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
