using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Web;
using System;

public class NetworkHandler : MonoBehaviour
{
    public string token;
    public long responseCode { get; set; }
    public string responseJson { get; set; }
    public string baseUrl;
    public bool tokenSet;
    public string currentThing  {get; set;}
    public float waitForSeconds;

    void Start() {
       baseUrl = "https://api.interactions.ics.unisg.ch";
       tokenSet = false;
       waitForSeconds = (float)1.0;
    }

    void Update() { 

    }

    public IEnumerator setToken(string thing) {
        string json = "{\"name\": \"Janick Spirig\",\"email\": \"janick.spirig@student.unisg.ch\"}";
        if (thing == "/cherrybot")
        {
        
            var uri = baseUrl + thing + "/operator";
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            yield return new WaitForSeconds(waitForSeconds);

            if (uwr.responseCode == System.Convert.ToInt64(204)) {
                // log in here
                
                Debug.Log("Let's do login....");
                
                UnityWebRequest uwr2 = new UnityWebRequest(uri, "POST");
                
                byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(json);
                uwr2.uploadHandler = (UploadHandler) new UploadHandlerRaw(encodedPayload);
                uwr2.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                
                uwr2.SetRequestHeader("Content-Type", "application/json");
                
                yield return uwr2.SendWebRequest();
                yield return new WaitForSeconds(waitForSeconds);
                
                UnityWebRequest uwr3 = UnityWebRequest.Get(uri);
                yield return uwr3.SendWebRequest();
                yield return new WaitForSeconds(waitForSeconds);

                Debug.Log("Login done successfully!");

                token = JsonUtility.FromJson<Token>(uwr3.downloadHandler.text).token;
                tokenSet = true;
                currentThing = thing;
            }
            else {
                token = JsonUtility.FromJson<Token>(uwr.downloadHandler.text).token;
                tokenSet = true;
                currentThing = thing;
            }
        }
        else if (thing == "/leubot1/v1.2")
        {
            // do Login for Leubot
            var uri = baseUrl + thing + "/user";

            // set token with POST request
                
            UnityWebRequest uwr = new UnityWebRequest(uri, "POST");
                
            byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(encodedPayload);
            uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
                
            uwr.SetRequestHeader("Content-Type", "application/json");
            yield return uwr.SendWebRequest();
            yield return new WaitForSeconds(waitForSeconds);
            
            var location = (string)uwr.GetResponseHeader("Location");
            var split_string = location.Split('/');
            token = split_string[split_string.Length-1];
            tokenSet = true;
            currentThing = thing;
            Debug.Log(token);
        }  
    }

    public IEnumerator PutRequest(string thing, string uri, string json, bool tokenRequired)
    {       

        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest uwr = UnityWebRequest.Put(baseUrl + thing + uri, dataToPut);
        if (tokenRequired)
        {
            switch (thing)
            {
                case "/cherrybot":
                    uwr.SetRequestHeader("Authentication", this.token);
                    break;
                case "/leubot1/v1.2":
                    uwr.SetRequestHeader("X-API-KEY", this.token);
                    break;
            }
        }

        uwr.SetRequestHeader ("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        yield return new WaitForSeconds(waitForSeconds);

        Debug.Log(uwr.responseCode);    

        if (uwr.responseCode != System.Convert.ToInt64(200) && uwr.responseCode != System.Convert.ToInt64(202))
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Request sent successfully!");            
        }

    }
    
    public IEnumerator PostRequest(string url, string json, string token="")
        {
            var uwr = new UnityWebRequest(url, "POST");

            if (token != "") {
                uwr.SetRequestHeader("Authentication", token);
            }

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();
            yield return new WaitForSeconds(waitForSeconds);

            if (uwr.isNetworkError) {
                Debug.Log("Error While Sending: " + uwr.error);
            } else {
                Debug.Log("Request sent successfully!");   
            }
        }
    }

[System.Serializable]
public class Token
{
    public string name;
    public string email;
    public string token;
}