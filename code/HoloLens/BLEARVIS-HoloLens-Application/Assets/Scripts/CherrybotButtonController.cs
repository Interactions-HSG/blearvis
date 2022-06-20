using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Globalization;
using System;
using TMPro;

public class CherrybotButtonController : MonoBehaviour
{
    public CherrybotHandler cherrybotHandler;
    public bool updateButtons;

    public GameObject moveButton;

    public GameObject statusText;

    private DateTime lastInteraction;
    private double timeout;
    private int currentStep;
    private int targetPosition;
    private string resetIcon;
    private string moveIcon;
    private string gripperIcon;
    private int gripperOpenValue;
    private int gripperCloseValue;
    private List<List<string>> positions = new List<List<string>>();
    private ButtonConfigHelper helper;

    void Start() {
    
        helper = moveButton.GetComponent<ButtonConfigHelper>();
        Debug.Log(helper);
        resetIcon = "IconRefresh";
        moveIcon = "IconHide";
        gripperIcon = "IconShow";

        updateButtons = true;
        lastInteraction = DateTime.Now;
        timeout = 0.0;

        statusText.SetActive(false);

        currentStep = 0;

        gripperOpenValue = 850;
        gripperCloseValue = 620;

        statusText.GetComponent<TextMeshPro>().text = "Executing...";
        
        // safety stop to move robot back to tennis ball later
        positions.Add(new List<string> {"500", "-2.2034161e-14", "280", "2.4067882e-7", "-89.99999", "180.0"});
        // positions.Add(new List<string> {"460.9", "-10.5", "472.4", "-179.6", "-3.3", "-7.6"});
        // first target position (above tennis ball)        
        positions.Add(new List<string> {"111.2", "529.8", "334.2", "-176.7", "-5.1", "89.7"});
        // second target position (tennis ball inside gripper)
        positions.Add(new List<string> {"112", "541.2", "235.7", "-176", "-0.7", "90.4"});
        // third target position (safety stop)
        positions.Add(new List<string> {"116.7", "529.5", "365.9", "-177.1", "-34.1", "89.4"});
        // initial position to prevent any colission of the robot
        positions.Add(new List<string> {"460.9", "-10.5", "472.4", "-179.6", "-3.3", "-7.6"});
        // fourth target position (over case)
        positions.Add(new List<string> {"17.7", "-474.8", "423.8", "177.7", "-38.4", "-87.5"});
        // fifth target position (initial position)
        positions.Add(new List<string> {"227", "-2.2034161e-14", "293.5", "2.4067882e-7", "-89.99999", "180.0"});

        // setInitialIcons();
    }

    void Update() {
        // define which button to display based on current position and update the buttons after x seconds

        // update buttons after three seconds since last click from user

        double difference = (DateTime.Now - lastInteraction).TotalSeconds;
        
        if (updateButtons) {
            if (difference > timeout) { 

                switch (currentStep) {
                    // move robot into safety stop
                    case 0:
                        moveButton.SetActive(true);
                        statusText.SetActive(false);

                        helper.MainLabelText = "Move Cherrybot";
                        helper.SetQuadIconByName(moveIcon);
                        timeout = 8.0;
                        updateButtons = false;
                        break;
                    // move robot into position just above the tennis ball
                    case 1:
                        moveButton.SetActive(false);
                        statusText.SetActive(true);
                        updateButtons = false;
                        // helper.MainLabelText = "Move Cherrybot";
                        // helper.SetQuadIconByName(moveIcon);
                        takeAction();
                        
                        timeout = 9.0;
                        break;

                    // move robot into position to grab tennis ball
                    case 2:

                        moveButton.SetActive(true);
                        statusText.SetActive(false);

                        helper.MainLabelText = "Move Cherrybot";
                        helper.SetQuadIconByName(moveIcon);
                        moveButton.SetActive(true);
                        timeout = 2.0;
                        updateButtons = false;
                        break;

                    // close gripper to grab tennis ball
                    case 3:
                        moveButton.SetActive(true);
                        statusText.SetActive(false);

                        helper.MainLabelText = "Close Gripper";
                        helper.SetQuadIconByName(gripperIcon);
                        
                        timeout = 1;
                        updateButtons = false;
                        break;

                    // move robot into safety stop
                    case 4:

                        moveButton.SetActive(true);
                        statusText.SetActive(false);

                        helper.MainLabelText = "Move Cherrybot";
                        helper.SetQuadIconByName(moveIcon);
                        moveButton.SetActive(true);
                        timeout = 2.0;
                        updateButtons = false;
                        break;

                    // move robot into temporary position to prevent any colission
                    case 5:

                        moveButton.SetActive(false);
                        statusText.SetActive(true);
                        updateButtons = false;
                        takeAction();
                        // helper.MainLabelText = "Move Cherrybot";
                        // helper.SetQuadIconByName(moveIcon);
                        // moveButton.SetActive(true);
                        timeout = 10.0;
                        break;

                    // move robot into position to release the tennis ball
                    case 6:
                        moveButton.SetActive(false);
                        statusText.SetActive(true);

                        takeAction();
                        //helper.MainLabelText = "Move Cherrybot";
                        //helper.SetQuadIconByName(moveIcon);
                        //moveButton.SetActive(true);
                        timeout = 7.0;
                        break;
                    
                    // release the tennis ball
                    case 7:

                        moveButton.SetActive(true);
                        statusText.SetActive(false);

                        helper.MainLabelText = "Release tennis ball";
                        helper.SetQuadIconByName(gripperIcon);
                        timeout = 1.0;
                        updateButtons = false;
                        break;

                    case 8:
                        statusText.SetActive(true);
                        moveButton.SetActive(false);
                        statusText.GetComponent<TextMeshPro>().text = "Very well done!";
                        // helper.SetQuadIconByName(resetIcon);
                        // helper.MainLabelText = "Reset Robot";
                        updateButtons = false;
                        break;
                }
                
            } else {
                moveButton.SetActive(false);
                statusText.SetActive(true);
                
                // moveButton.GetComponent<Interactable>().enabled = false;
                // helper.MainLabelText = "Executing...";
            }
        }
    }

    private void setInitialIcons ()
    {
        helper.MainLabelText = "Move Cherrybot";
        helper.SetQuadIconByName(moveIcon);
    }

    public void takeAction() { 

        string label = helper.MainLabelText;
        
        if (label.Contains("Move") || label.Contains("Reset")) { moveRobot();}
        else if (label.Contains("Gripper") || label.Contains("Release")) {moveGripper();}
    }

    public void moveRobot() {

        if (currentStep == 5) {
            cherrybotHandler.resetPosition();
            // cherrybotHandler.changePosition(positions[targetPosition]);
        } else {
            cherrybotHandler.changePosition(positions[targetPosition]);
        }

        if (currentStep < 8) { currentStep  = currentStep + 1; }
        if (targetPosition < positions.Count) { targetPosition += 1; }

        lastInteraction = DateTime.Now;
        updateButtons = true;
    }

    public bool openOrClose() {

        bool result = false;

        switch (currentStep) {
            // close gripper
            case 3 : result = false;
            break;
            // open gripper
            case 7 : result = true;
            break;
        }
        return result;
    }

    public void moveGripper() {

        // true -> open, false -> close
        bool open = openOrClose();

        if (open) {
            // open the gripper
            cherrybotHandler.changeGripper("800");
        } else {
            cherrybotHandler.changeGripper("620");
        }

        if (currentStep < 8) { currentStep  = currentStep + 1; }
        lastInteraction = DateTime.Now;
        updateButtons = true;
    }
}
