using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Events;

public class mqttTest : MonoBehaviour
{

	[Header("Client Settings")] 
	public string ipAddress;
	public int broker_port;

	[Header("Topics - # WildCard")]
	public string topicPrefix;
	public List<string> topics = new List<string>();
	private MqttClient client;
	private MQTTRouting routing;
	// Use this for initialization


	void Start()
	{
		// create client instance 
		client = new MqttClient(IPAddress.Parse(ipAddress), broker_port, false, null);

		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

		string clientId = Guid.NewGuid().ToString();
		client.Connect(clientId);

		// subscribe to the topics set in inspector 
		foreach (string topic in topics)
		{
			Debug.Log("TopicSub: "+ (topicPrefix + topic));
			client.Subscribe(new string[] {(topicPrefix + topic)}, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
		}
		
		routing = new MQTTRouting();
		

		//client.Subscribe(new string[] { "hello/world" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

	}

	private void OnDestroy()
	{
		client.Disconnect();
		
	}

	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{
		if(e.Topic != null)
			routing.routeMessage(e);
		//Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message) + "," + e.Topic );
	} 


}
