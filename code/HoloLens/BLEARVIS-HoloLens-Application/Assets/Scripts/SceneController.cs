using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.MixedReality.Toolkit;

public class SceneController : MonoBehaviour
{
    public GameObject cherrybotItemBox;
    public GameObject dialogAuthenticate;
    public GameObject leubotItemBox;
    public GameObject labWelcomeBox;

    public GameObject CO2Warning;
    public GameObject temperatureWarning;
    public GameObject windowOpenWarning;
    public GameObject officeWelcomeBox;
    public GameObject curtainsControl;
    public GameObject ceilingLightControl;
    public GameObject labLightReminder;
    public GameObject lablightReminderConfirm;
    public GameObject hueInformation; 
    public GameObject hueControl;
    public GameObject robotInfobox;
    public GameObject labLightQuestion;
    public GameObject miroCardInfoBox;
    public GameObject ThunderboardInfoBox;
    public GameObject labByeMessage;
    public GameObject officeByeMessage;
    public GameObject humidityWarning;
    public GameObject deskBulbInfo;
    public GameObject deskLampInfo;
    public GameObject cherrybotNextStep;
    public GameObject leubotNextStep;
    public GameObject hueNextStep;
    public GameObject windowNextStep;
    
    public DateTime officeEntryTime;
    public DateTime labEntryTime;

    public bool showCherrybotControl {get; set;}
    public bool showLeubotControl {get; set;}
    public bool showLabWelcomeBox {get; set;}
    public bool showOfficeWelcomeBox {get; set; }
    public bool showCO2Warning {get; set; }
    public bool inLab {get; set; }
    public bool inOffice {get; set; }
    public bool locationUndefined {get; set; }
    public bool showWindowOpenWarning  {get; set; }
    public bool showCurtainsControl {get; set; }
    public bool showCeilingLightControl {get; set; }
    public bool showTemperatureWarning {get; set; }
    public bool showLabLightReminder {get; set; }
    public bool showLabLightReminderConfirm {get; set; }
    public bool showHueControl {get; set; }
    public bool showHueInformation {get; set; }
    public bool showDialogAuthenticate {get; set; }
    public bool showRobotInfobox {get; set; }
    public bool showLabLightQuestion {get; set; }
    public bool showMiroCardInfo {get; set; }
    public bool showThunderboardInfo {get; set; }
    public bool showLabByeMessage {get; set; }
    public bool showOfficeByeMessage {get; set; }
    public bool showHumidityWarning {get; set; }
    public bool showDeskLampInfo {get; set; }
    public bool showDeskBulbInfo {get; set; }
    public bool showCherrybotNextStep {get; set; }
    public bool showLeubotNextStep {get; set; }
    public bool showHueNextStep {get; set; }
    public bool showWindowNextStep {get; set; }

    public bool blindInteractionDone {get; set; }
    public bool cherrybotInteractionDone {get; set; }
    public bool temperatureWarningDone {get; set; }
    public bool miroCardInfoDone {get; set; }
    public bool thunderboardInfoDone {get; set; }
    public bool leubotInteractionDone {get; set; }
    public bool blindOfficeInteractionDone {get; set; }
    public bool hueInteractionDone {get; set; }
    public bool labLightReminderDone {get; set; }
    public bool labLightTurnOnOffDone {get; set; }
    public bool co2WarningDone {get; set; }
    public bool rainWarningDone {get; set; }
    public bool humidityWarningDone {get; set; }
    public bool deskBulbDone {get; set; }
    public bool deskLampDone {get; set; }

    public bool cherrybotNextStepDone {get; set; }

    private DateTime commandReceived;


	void Start ()
	{
        // WHICH HOLOGRAMS TO SHOW BY DEFAULT
        showLabLightQuestion = false;
        showCherrybotControl = false;
        showLeubotControl = false;
        showLabWelcomeBox = false;
        showOfficeWelcomeBox = false;
        
        showCO2Warning = false;
        showWindowOpenWarning = false;
        showCurtainsControl = false;
        showCeilingLightControl = false;
        showTemperatureWarning = false;
        showLabLightReminder = false;
        showHueControl = false;
        showHueInformation = false;
        showDialogAuthenticate = false;
        showRobotInfobox = false;
        showMiroCardInfo = false;
        showThunderboardInfo = false; // new
        showLabByeMessage = false;
        showOfficeByeMessage = false;
        showHumidityWarning = false;
        showDeskBulbInfo = false;
        showDeskLampInfo = false;
        showCherrybotNextStep = false;
        showLeubotNextStep = false;
        showHueNextStep = false;
        showWindowNextStep = false;

        // CONTROL PREVIOUS INTERACTIONS
        blindInteractionDone = false;
        temperatureWarningDone = false;
        cherrybotInteractionDone = false;
        miroCardInfoDone = false;
        thunderboardInfoDone = false;
        leubotInteractionDone = false;
        hueInteractionDone = false;
        labLightReminderDone = false;
        labLightTurnOnOffDone = false;
        co2WarningDone = false; // teting
        rainWarningDone = false;
        humidityWarningDone = false;
        deskBulbDone = false;
        deskLampDone = false;

        // USER LOCATION AT START
        locationUndefined = true;
        inOffice = false; // testing
        inLab = false;
	}   

	void Update ()
	{
        /*
        if (showHueNextStep)
        {
            hueNextStep.SetActive(true);
        }
        else
        {
            hueNextStep.SetActive(false);
        }

        
        if (showWindowNextStep)
        {
            windowNextStep.SetActive(true);
        }
        else
        {
            windowNextStep.SetActive(false);
        }

        if (showCherrybotNextStep)
        {
            cherrybotNextStep.SetActive(true);
        }
        else
        {
            cherrybotNextStep.SetActive(false);
        }

        if (showLeubotNextStep)
        {
            leubotNextStep.SetActive(true);
        }
        else
        {
            leubotNextStep.SetActive(false);
        }

        if (showDeskBulbInfo)
        {
            deskBulbInfo.SetActive(true);
        }
        else
        {
            deskBulbInfo.SetActive(false);
        }

        if (showDeskLampInfo)
        {
            deskLampInfo.SetActive(true);
        }
        else
        {
            deskLampInfo.SetActive(false);
        }
        if (showHumidityWarning)
        {
            humidityWarning.SetActive(true);
        }
        else
        {
            humidityWarning.SetActive(false);
        }

        if (showOfficeByeMessage)
        {
            officeByeMessage.SetActive(true);
        }
        else
        {
            officeByeMessage.SetActive(false);
        }

        if (showLabByeMessage)
        {
            labByeMessage.SetActive(true);
        }
        else
        {
            labByeMessage.SetActive(false);
        }

        if (showMiroCardInfo)
        {
            miroCardInfoBox.SetActive(true);
        }
        else
        {
            miroCardInfoBox.SetActive(false);
        }
        */

        /* not working yet for multiple boards +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        if (showThunderboardInfo)
        {
            ThunderboardInfoBox.SetActive(true);
        }
        else
        {
            ThunderboardInfoBox.SetActive(false);
        }
        */

        /*
        if (showLabLightQuestion)
        {
            labLightQuestion.SetActive(true);
        }
        else
        {
            labLightQuestion.SetActive(false);
        }

        // TEMPERATURE WARNING
        if (showTemperatureWarning)
        {
            temperatureWarning.SetActive(true);
        }
        else
        {
            temperatureWarning.SetActive(false);
        }

        // ROBOT INFO BOX
        if (showRobotInfobox)
        {
            robotInfobox.SetActive(true);
        }
        else
        {
            robotInfobox.SetActive(false);
        }

        // CHERRYBOT AUTHENTICATION
        if (showDialogAuthenticate)
        {
            dialogAuthenticate.SetActive(true);
        }
        else
        {
            dialogAuthenticate.SetActive(false);
        }
        
        if (showHueControl)
        {
            hueControl.SetActive(true);
        }
        else
        {
            hueControl.SetActive(false);
        }

        if (showHueInformation)
        {
            hueInformation.SetActive(true);
        }
        else
        {
            hueInformation.SetActive(false);
        }

        if (showLabLightReminderConfirm)
        {
            lablightReminderConfirm.SetActive(true);
        }
        else
        {
            lablightReminderConfirm.SetActive(false);
        }

        if (showLabLightReminder)
        {
            labLightReminder.SetActive(true);
        }
        else
        {
            labLightReminder.SetActive(false);
        }

        // CO2 WARNING
        if (showCO2Warning)
        {
            CO2Warning.SetActive(true);
        }
        else
        {
            CO2Warning.SetActive(false);
        }

        // TEMPERATURE WARNING
        if (showCO2Warning)
        {
            CO2Warning.SetActive(true);
        }
        else
        {
            CO2Warning.SetActive(false);
        }

        // WINDOW OPEN WARNING AND EITHER GOING TO RAIN, BAD AQ OR BOTH AND OUTSIDE AQ BAD WARNING
        if (showWindowOpenWarning)
        {
            windowOpenWarning.SetActive(true);
        }
        else
        {
            windowOpenWarning.SetActive(false);
        }

        */
        // OFFICE WELCOMEBOX
        if (showOfficeWelcomeBox)
        {
            officeWelcomeBox.SetActive(true);
        }
        else
        {
            officeWelcomeBox.SetActive(false);
        }

        /*
        // CURTAINS CONTROL
        if (showCurtainsControl)
        {
            curtainsControl.SetActive(true);
        }
        else
        {
            curtainsControl.SetActive(false);
        }

        // CEILING LIGHT CONTROL
        if (showCeilingLightControl)
        {
            ceilingLightControl.SetActive(true);
        }
        else
        {
            ceilingLightControl.SetActive(false);
        }

        // CHERRYBOT
        if (showCherrybotControl)
        {
            cherrybotItemBox.SetActive(true);
        }
        else
        {
            cherrybotItemBox.SetActive(false);
        }  

        // LEUBOT
        if (showLeubotControl)
        {
            leubotItemBox.SetActive(true);
        }
        else
        {
            leubotItemBox.SetActive(false);
        }

        // LAB
        if (showLabWelcomeBox)
        {
            labWelcomeBox.SetActive(true);
        }
        else
        {
            labWelcomeBox.SetActive(false);
        }
        */


	}

    public void uponOfficeEntrance()
    {
        showOfficeWelcomeBox = false;
        officeEntryTime = DateTime.Now;

        inOffice = true;
        inLab = false;
        locationUndefined = false;
    }

    public void uponLabEntrance()
    {
        inOffice = false;
        inLab = true;
        locationUndefined = false;

        showLabWelcomeBox = false;
      
        labEntryTime = DateTime.Now;

    }
}