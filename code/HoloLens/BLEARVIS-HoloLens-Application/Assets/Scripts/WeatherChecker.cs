using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Web;
using System;
using SimpleJSON;
using System.Globalization;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using System.Linq;

public class WeatherChecker : MonoBehaviour
{
    private string consumerKey;
    private string consumerSecret;
    private string geoLocationId;
    private string apiAuthEndpoint = "";
    private string apiForecastEndpoint = "";
    private string apiAuthMethod;
    private string apiForecastMethod;
    private DateTime timeLastExecution;

    public OntologyReader ontologyReader;
    public bool weatherForecastSet;
    public bool goingToRain; 
    public int rainProbabilityThreshold;

    // Start is called before the first frame update
    void Start()
    {
        consumerKey = "";
        consumerSecret = "";
        geoLocationId = "47.4238,9.3739";

        apiAuthEndpoint = "https://api.srgssr.ch/oauth/v1/accesstoken?grant_type=client_credentials";
        apiAuthMethod = "POST";
        // forecastEndpoint = "https://api.srgssr.ch/srf-meteo/forecast/" + geoLocationId;

        weatherForecastSet = false;
        goingToRain = false;

        rainProbabilityThreshold = 90;
    }

    void Update() {
        if (/*apiAuthEndpoint == "" && */apiForecastEndpoint == "") {
            if (ontologyReader.endpointsSet) {
                ontologyReader.endpoints.ToList().ForEach(o => {
                    if (o.thing == "meteo") {
                        //Debug.Log(o);
                        //Debug.Log(o.actionName);
                        
                        if (o.actionDescription.Contains("forecast")) {
                            // becasue of ',' in location id  
                            apiForecastEndpoint = o.uri + "," + o.actionName;
                            apiForecastMethod = o.method;
                            Debug.Log(apiForecastEndpoint);
                            Debug.Log(apiForecastMethod);
                        } 
                        /*else if (o.actionName.Contains("authentication")) {
                            apiAuthEndpoint = o.uri;
                            apiAuthMethod = o.method;
                        } */
                    }
                });
            }
        }
    }

    public IEnumerator getWeatherForecast () {
        // AUTHENTICATION
        var uwr = new UnityWebRequest(apiAuthEndpoint, apiAuthMethod);

        string auth = consumerKey + ":" + consumerSecret;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;

        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Authorization", auth);
        
        yield return uwr.SendWebRequest();

        Debug.Log("Weather authentication result");
        Debug.Log(uwr.responseCode);

        string token = JSON.Parse(uwr.downloadHandler.text)["access_token"];
        string bearerToken = "";
        yield return bearerToken = "Bearer " + token;

        UnityWebRequest uwr2 = UnityWebRequest.Get(apiForecastEndpoint);
        // var uwr2 = new UnityWebRequest(apiForecastEndpoint, apiForecastMethod);
        uwr2.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr2.SetRequestHeader("Authorization", bearerToken);
        uwr2.SetRequestHeader("Accept", "*/*");
        uwr2.chunkedTransfer=false;
        uwr2.useHttpContinue = false;
        // yield return uwr2.SendWebRequest();
        Debug.Log(uwr2.error);
        Debug.Log(uwr2.responseCode);
        
        Debug.Log(uwr2.downloadHandler.text);

        if (Convert.ToInt32(uwr2.responseCode) == 200)
        {
            var data = JSON.Parse(uwr2.downloadHandler.text);

            // GET CLOSEST FORECAST
            DateTime currentDateTime = DateTime.Now.ToUniversalTime();

            JSONArray allForecasts = (JSONArray) data["forecast"]["60minutes"];

            int time_diff = 10000;
            JSONNode closestForecast = null;

            foreach (var forecast in allForecasts) {
                DateTime forecastDT = Convert.ToDateTime((string) forecast.Value["local_date_time"]).ToUniversalTime();
                
                int diffMinutes = Convert.ToInt32((forecastDT - currentDateTime).TotalMinutes);
                
                if (diffMinutes < time_diff && diffMinutes > -1) {
                    time_diff = diffMinutes;
                    closestForecast = forecast.Value;
                }
            }

            // CHECK IF IT IS GOING TO RAIN
            if (closestForecast["PROBPCP_PERCENT"] > rainProbabilityThreshold) {
                goingToRain = true;
            } else {
                goingToRain = false;
            }
            weatherForecastSet = true;
        }
        else
        {
            //Debug.Log("Error at Weather Forecast API!");
            //Debug.Log(uwr2.responseCode);

            // for testing purpose only
            goingToRain = true;
            weatherForecastSet = true;
        }
    }
}
