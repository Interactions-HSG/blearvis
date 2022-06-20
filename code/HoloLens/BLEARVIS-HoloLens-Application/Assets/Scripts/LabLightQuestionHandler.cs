using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization; 
using System;
using TMPro;

public class LabLightQuestionHandler : MonoBehaviour
{


    public SceneController sceneController;
    // Start is called before the first frame update

    private bool questionAsked;
    private bool labByeMessageShown;
    private bool officeByeMessageShown;
    private bool firstExecutionLabByeMessage;
    private bool firstExecutionOfficeByeMessage;
    private bool firstExecutionLightConfirmMessage;
    private bool firstExecutionWindowsNextStep;
    private bool firstExecutionHueNextStep;
    private DateTime referencePoint;
    private double secondsAfterEntry;
    private double secondsUntilByeMessage;
    private double scecondsUntilLightConfirmMessage;
    private double secondsUntilCherryBotNextStep;
    private bool firstExecutionCherrybotNextStep;
    private bool cherrybotNextStepShown;
    private double secondsUntilHueNextStep;
    private double secondsUntilWindowsNextStep;
    private bool windowsNextStepShown;
    private bool hueNextStepShown;

    private double secondsUntilLeubotNextStep;
    private bool firstExecutionLeubotNextStep;
    private bool leubotNextStepShown;
    
    public bool questionAnswered {get; set; }
    public string answer {get; set; }
    public GameObject questionDialogBox;

    void Start()
    {
        secondsAfterEntry = 7.0;
        secondsUntilByeMessage = 5.0;
        scecondsUntilLightConfirmMessage = 1.0;
        secondsUntilCherryBotNextStep = 12.0;
        secondsUntilLeubotNextStep = 3.0;
        secondsUntilHueNextStep = 3.0;
        secondsUntilWindowsNextStep = 3.0;

        questionAsked = false;  
        officeByeMessageShown = false;
        labByeMessageShown = false;
        firstExecutionCherrybotNextStep = true;

        cherrybotNextStepShown = false;

        firstExecutionLeubotNextStep = true;
        leubotNextStepShown = false;
        hueNextStepShown = false;
        windowsNextStepShown = false;

        firstExecutionLabByeMessage = true;
        firstExecutionOfficeByeMessage = true;
        firstExecutionLightConfirmMessage = true;
        firstExecutionWindowsNextStep = true;
        firstExecutionHueNextStep = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (sceneController.inLab && sceneController.temperatureWarningDone && !cherrybotNextStepShown)
        {

            if (firstExecutionCherrybotNextStep)
            {
                referencePoint = DateTime.Now;
                firstExecutionCherrybotNextStep = false;
            }

            if ((DateTime.Now - referencePoint).TotalSeconds > secondsUntilCherryBotNextStep)
            {
                // display next message
                sceneController.showCherrybotNextStep = true;
                cherrybotNextStepShown = true;
            }
        }

        if (sceneController.inLab && sceneController.cherrybotInteractionDone && !leubotNextStepShown)
        {

            if (firstExecutionLeubotNextStep)
            {
                referencePoint = DateTime.Now;
                firstExecutionLeubotNextStep = false;
            }

            if ((DateTime.Now - referencePoint).TotalSeconds > secondsUntilLeubotNextStep)
            {
                // display next message
                sceneController.showLeubotNextStep = true;
                leubotNextStepShown = true;
            }
        }

        if (sceneController.inLab && !questionAsked) {
            double difference = (DateTime.Now - sceneController.labEntryTime).TotalSeconds;
           
            if (difference > secondsAfterEntry) {
                sceneController.showLabLightQuestion = true;
                questionAsked = true;
            }
        }

        if (sceneController.inLab && sceneController.leubotInteractionDone && !labByeMessageShown) {
            
            if (firstExecutionLabByeMessage)  {
                referencePoint = DateTime.Now;
                firstExecutionLabByeMessage = false;
            }

            double difference = (DateTime.Now - referencePoint).TotalSeconds;

            if (difference > secondsUntilByeMessage) {
                sceneController.showLabByeMessage = true;
                labByeMessageShown = true;
            }
    
        }

        if (sceneController.labLightReminderDone && sceneController.inOffice && !windowsNextStepShown) {
            if (firstExecutionWindowsNextStep)  {
                referencePoint = DateTime.Now;
                firstExecutionWindowsNextStep = false;
            }

            double difference = (DateTime.Now - referencePoint).TotalSeconds;

            if (difference > secondsUntilWindowsNextStep) {
                sceneController.showWindowNextStep = true;
                windowsNextStepShown = true;
            }
        }

        if (sceneController.blindInteractionDone && sceneController.inOffice && !hueNextStepShown) {
            if (firstExecutionHueNextStep)  {
                referencePoint = DateTime.Now;
                firstExecutionHueNextStep = false;
            }

            double difference = (DateTime.Now - referencePoint).TotalSeconds;

            if (difference > secondsUntilHueNextStep) {
                sceneController.showHueNextStep = true;
                hueNextStepShown = true;
            }
        }



        if (sceneController.inOffice && sceneController.humidityWarningDone && !officeByeMessageShown) {
            // display bye message 
            if (firstExecutionOfficeByeMessage)  {
                referencePoint = DateTime.Now;
                firstExecutionOfficeByeMessage = false;
            }

            double difference = (DateTime.Now - referencePoint).TotalSeconds;

            if (difference > secondsUntilByeMessage) {
                sceneController.showOfficeByeMessage = true;
                officeByeMessageShown = true;
            }


        }

        if (sceneController.inLab && questionAnswered) {

            if (firstExecutionLightConfirmMessage) {
                referencePoint = DateTime.Now;
                firstExecutionLightConfirmMessage = false;
            }

            double difference = (DateTime.Now - referencePoint).TotalSeconds;

            if (difference > scecondsUntilLightConfirmMessage) {

                GameObject buttonParrent = questionDialogBox.transform.Find("ButtonParent").gameObject;

                buttonParrent.transform.Find("ButtonTwoA").gameObject.SetActive(false);
                buttonParrent.transform.Find("ButtonTwoB").gameObject.SetActive(false);
                buttonParrent.transform.Find("ButtonOne").gameObject.SetActive(true);

                string text = "";
                if (answer == "No") {
                    text = "Alright, no problem. You can also turned it on yourself. Just look up at the ceiling light and I will show you an option to do so!";
                }

                if (answer == "Yes") {
                    text = "Perfect, I turned it on for you. Now just keep experiencing and simply be yourself.";
                }

                // update description text
                questionDialogBox.transform.Find("DescriptionText").GetComponent<TextMeshPro>().text = text;

                questionAnswered = false;
            }



        }
    }
}
