using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// this script should start the ObjectRecognition process running on another computer
public class StartRecognition : MonoBehaviour
{
    IEnumerator StartRecognitionProcess() {
        var url = "http://192.168.43.239:5000/start";
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Start yolo");
        UnityWebRequest www = UnityWebRequest.Put(url, data);

        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Process started successfully!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartRecognitionProcess());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
