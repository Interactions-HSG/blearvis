/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using System.Globalization;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt).
/// 
/// Adpapted by Jannis Strecker: function DecodeMessage(). 
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class MqttHandler : M2MqttUnityClient
    {
        /*
        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;


        [Header("User Interface")]
        public InputField consoleInputField;
        public Toggle encryptedToggle;
        public InputField addressInputField;
        public InputField portInputField;
        public Button connectButton;
        public Button disconnectButton;
        public Button testPublishButton;
        public Button clearButton;
         */

        [Header("Custom for BLEARVIS")]
        public string MyTopic;

        public SceneController sceneController;
        //public ThunderboardHandler thunderboardHandler;

        public GameObject thunderboardInfoBoxPrefab;
        public ThunderboardHandlerListScript thunderboardHandlerListScript;
        

        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;

        /*
       public void TestPublish()
       {
           client.Publish("M2MQTT_Unity/test", System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
           Debug.Log("Test message published");
           Debug.Log("Test message published.");
       }


       public void SetBrokerAddress(string brokerAddress)
       {
           if (addressInputField && !updateUI)
           {
               this.brokerAddress = brokerAddress;
           }
       }

       public void SetBrokerPort(string brokerPort)
       {
           if (portInputField && !updateUI)
           {
               int.TryParse(brokerPort, out this.brokerPort);
           }
       }

       public void SetEncrypted(bool isEncrypted)
       {
           this.isEncrypted = isEncrypted;
       }


       public void Debug.Log(string msg)
       {
           if (consoleInputField != null)
           {
               consoleInputField.text = msg;
               updateUI = true;
           }
       }

       public void Debug.Log(string msg)
       {
           if (consoleInputField != null)
           {
               consoleInputField.text += msg + "\n";
               updateUI = true;
           }
       }
       */

        protected override void OnConnecting()
        {
            base.OnConnecting();
            Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("Connected to broker on " + brokerAddress + "\n");
            /*
            if (autoTest)
            {
                TestPublish();
            }
            */
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { MyTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Debug.Log("Subscribed to topics.");
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { MyTopic });
            Debug.Log("Unubscribed to topics.");
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }

        /*
        private void UpdateUI()
        {
            if (client == null)
            {
                if (connectButton != null)
                {
                    connectButton.interactable = true;
                    disconnectButton.interactable = false;
                    testPublishButton.interactable = false;
                }
            }
            else
            {
                if (testPublishButton != null)
                {
                    testPublishButton.interactable = client.IsConnected;
                }
                if (disconnectButton != null)
                {
                    disconnectButton.interactable = client.IsConnected;
                }
                if (connectButton != null)
                {
                    connectButton.interactable = !client.IsConnected;
                }
            }
            if (addressInputField != null && connectButton != null)
            {
                addressInputField.interactable = connectButton.interactable;
                addressInputField.text = brokerAddress;
            }
            if (portInputField != null && connectButton != null)
            {
                portInputField.interactable = connectButton.interactable;
                portInputField.text = brokerPort.ToString();
            }
            if (encryptedToggle != null && connectButton != null)
            {
                encryptedToggle.interactable = connectButton.interactable;
                encryptedToggle.isOn = isEncrypted;
            }
            if (clearButton != null && connectButton != null)
            {
                clearButton.interactable = connectButton.interactable;
            }
            updateUI = false;
        }
        */

        protected override void Start()
        {
            Debug.Log("Ready.");
            updateUI = true;
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            Debug.Log("received new message");
            string msg = System.Text.Encoding.UTF8.GetString(message);

            if (msg == null || msg == "") return;

            Debug.Log($"topic: {topic}");

            if (topic.StartsWith("estimator/angle"))
            {
                thunderboardHandlerListScript.HandleIncomingMQTTMessage(topic, msg);


            }
            StoreMessage(msg);

            /*
            if (topic == "M2MQTT_Unity/test")
            {
                if (autoTest)
                {
                    autoTest = false;
                    Disconnect();
                }
            }
            */
        }


        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            Debug.Log("Received: " + msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
            /*
            if (updateUI)
            {
                UpdateUI();
            }
            */
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            /*
            if (autoTest)
            {
                autoConnect = true;
            }
            */
        }
    }
}
