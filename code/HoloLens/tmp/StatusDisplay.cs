using Unity;
using UnityEngine;
using System;
using TMPro;

public class StatusDisplay : MonoBehaviour {

    public bool displayMessage;

    public string message;

    public TextMeshPro statusMessage;

    /*
    void Update() {
        if (displayMessage) {
            statusMessage.text = message;
            displayMessage = false;
        }
    }
    */

    public void Display(string m) {
        displayMessage = true;
        this.message = m;
    }
}