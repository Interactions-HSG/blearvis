using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RobotInSceneHandler : MonoBehaviour
{

    public SceneController sceneController;
    public authenticator authenticator;
    public GameObject dialogBox;
    public GameObject infoBox;
    public bool processRobot {get; set; }
    public bool tbProcessFinished;

    public TMPro.TextMeshPro titleText;
    public TMPro.TextMeshPro descriptionText;

    public string thing;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update() {

        if (processRobot)
        {
            handleRobotAppereance();
            processRobot = false;
        }

        if (authenticator.loggedIn && tbProcessFinished)
        { 
            if ((DateTime.Now - authenticator.timePwdCorrect).TotalSeconds > 3.0) {

                // reset the dialog authenticate first
                authenticator.resetDialog();
                sceneController.showDialogAuthenticate = false;
                
                if (thing == "Cherrybot")
                {
                    sceneController.showCherrybotControl = true;
                }
                else if (thing == "Leubot")
                {
                    sceneController.showLeubotControl = true;
                }
                authenticator.loggedIn = false;
            }
        }
    }

    public void handleRobotAppereance() {
        sceneController.showRobotInfobox = true;
        
        string titleText = "";
        string descriptionText = "";

        if (thing == "Cherrybot")
        {
            titleText = $"Hi, {thing}!";
            descriptionText = $"This is the {thing} which is currently executing process 657. As you can see, he automates the process of putting tenis balls into its case. If you want to control the robot yourself, just click below!";
        }
        else if (thing == "Leubot")
        {
            titleText = $"Hi, {thing}!";
            descriptionText = "This is another very cool robot, just try it out by clicking below to authenticate yourself again!";
        }

        infoBox.transform.Find("TitleText").GetComponent<TextMeshPro>().text = titleText;
        infoBox.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = descriptionText;
    }


    public void handleRobotControl() {
        // set tile and text of authenticate dialogbox
        string masterPwd = authenticator.password;
        sceneController.showRobotInfobox = false;
        sceneController.showDialogAuthenticate = true;

        dialogBox.transform.Find("TitleText").GetComponent<TextMeshPro>().text = $"Let's play!";
        dialogBox.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = $"To control the {thing} yourself, click on the button, enter the password {masterPwd} and hit enter.";
    }
}