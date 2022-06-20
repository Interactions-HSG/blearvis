using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class StartProcess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendRequest()
    {
        StartCoroutine(RunRecognition());
    }

    IEnumerator RunRecognition()
    {
        var url = "some url";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("There was an error");
        }
        else
        {
            Debug.Log("Success"); // this log is returning the requested data. 
        }
    }
}
