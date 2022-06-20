using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class LeubotButtonController : MonoBehaviour
{
    public LeubotHandler leubotHandler;
    // public ButtonConfigHelper buttonConfigHelper;

    private bool stateWristAngle;
    private bool stateWristRotation;
    private bool stateElbow;
    private bool stateGripper;

    private string moveIcon;
    private string resetIcon;

    public GameObject elbowButton;
    public GameObject wristAngleButton;
    public GameObject wristRotationButton;
    public GameObject gripperButton;
 
    
    void Start() {
        // TO DO: Get current state of light to set toggle accordingly.
        // <gameObject.SetActive(false);
        stateWristAngle = false;
        stateWristRotation = false;
        stateElbow = false;
        stateGripper = false;
        
        moveIcon = "IconHide";
        resetIcon = "IconRefresh";
    
        setInitialIcons();
    }

    private void setInitialIcons ()
    {
        wristAngleButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        wristRotationButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        elbowButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        gripperButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
    }
    

    public void moveWristAngle() {
    
        string value;
        if (!stateWristAngle) {
            value = "830";
            stateWristAngle = true;
            wristAngleButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(resetIcon);
        } else {
            value = "200";
            stateWristAngle = false;
            wristAngleButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        }
        leubotHandler.changePosition(value, "/wrist/angle");
    }

    public void moveWristRotation() {
        string value;
        if (!stateWristRotation) {
            value = "1023";
            stateWristRotation = true;
            wristRotationButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(resetIcon);
        } else {
            value = "0";
            stateWristRotation = false;
            wristRotationButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        }
        leubotHandler.changePosition(value, "/wrist/rotation");
    }

    public void moveElbow() {
        string value;
        if (!stateElbow) {
            value = "650";
            stateElbow = true;
            elbowButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(resetIcon);
        } else {
            value = "400";
            stateElbow = false;
            elbowButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        }
        leubotHandler.changePosition(value, "/elbow");
    }

    public void moveGripper() {
        string value;
        if (!stateGripper) {
            value = "0";
            stateGripper = true;
            gripperButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(resetIcon);

        } else {
            value = "512";
            stateGripper = false;
            gripperButton.GetComponent<ButtonConfigHelper>().SetQuadIconByName(moveIcon);
        }
        leubotHandler.changePosition(value, "/gripper");
    }
}