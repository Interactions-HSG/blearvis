using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using SimpleJSON;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;


public class authenticator : MonoBehaviour {
     
    public bool loggedIn {get; set; }
    public string password {get; set; }
    
    private string miroCardEndpoint;

    private MixedRealityKeyboard keyBoard;
    public DateTime timePwdCorrect {get; set; }

    public GameObject dialogAuthenticate;


    void Start()
    {
        loggedIn = false;
        password = "Hsg1234";
        miroCardEndpoint = "some endpoint"; // the MiroCard IP Adress
        keyBoard = dialogAuthenticate.GetComponent<MixedRealityKeyboard>();
        // previewText = dialogAuthenticate.transform.Find("PasswordField").Find("PreviewText").gameObject.GetComponent<TextMeshPro>();
    }

    public void resetDialog() {
        // reset dialog to inital state with button and welcometext only
        dialogAuthenticate.transform.Find("PwdResultText").gameObject.SetActive(false);
        dialogAuthenticate.transform.Find("ButtonParent").gameObject.SetActive(true);
    }

    private bool checkPassword(string enteredPwd) {

        if (enteredPwd == password)
        {
            // StartCoroutine(sendInfoToMiroCard());
            return true;
        } else
        {
            return false;
        }
    }

    private IEnumerator sendInfoToMiroCard() {
        byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes("{}");
        UnityWebRequest uwr = UnityWebRequest.Put(miroCardEndpoint, dataToPut);
        uwr.SetRequestHeader ("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
    }

    public void showKeyboard() {
        // hide the button field
        dialogAuthenticate.transform.Find("ButtonParent").gameObject.SetActive(false);
        
        // show the keyboard
        keyBoard.ShowKeyboard();

        // show the preview keyboard preview field
        // dialogAuthenticate.transform.Find("KeyboardPreview").gameObject.SetActive(true);
    }

    public void processPassword() {
        // hide keyboard and clear text
        string enteredText = keyBoard.Text;
        keyBoard.HideKeyboard();
        keyBoard.ClearKeyboardText();
        
        // hide preview field
        // dialogAuthenticate.transform.Find("KeyboardPreview").gameObject.SetActive(false);
        
        // get the result text component
        TextMeshPro resultText = dialogAuthenticate.transform.Find("PwdResultText").GetComponent<TextMeshPro>();

        if (checkPassword(enteredText)) {
            // PASSWORD IS CORRECT

            // hide the password field
            dialogAuthenticate.transform.Find("ButtonParent").gameObject.SetActive(false);
            // Instruct user to shake miro card.
            resultText.text = "PASSWORD CORRECT! I will shortly present you the commads to control the Robot.";
            dialogAuthenticate.transform.Find("PwdResultText").gameObject.SetActive(true);
            timePwdCorrect = DateTime.Now; 
            loggedIn = true;
        } else {
            // PASSWORD IS NOT CORRECT
            resultText.text = "PASSWORD INCORRECT! Please click on the button below and try again!";
            // show the button again so user can start everything from beginning
            dialogAuthenticate.transform.Find("PwdResultText").gameObject.SetActive(true);
            dialogAuthenticate.transform.Find("ButtonParent").gameObject.SetActive(true);
        }
    }

}