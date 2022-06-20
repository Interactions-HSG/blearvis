using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MQTTRouting : MonoBehaviour
{
   // Tracking Variables
   //200 is the max tracking amount as defined in the python script : centroidtracker.py (line:54)
   [ReadOnly(true)]
   public int maxTrackers = 200;

   public List<int> ActiveIDs
   {
      get { return ActiveTrackingID; }
   }
   private List<int> ActiveTrackingID = new List<int>();
   public struct TrackingObject
   {
      public Vector2 position;
      public bool isActive;

      public TrackingObject(Vector2 position, bool isActive)
      {
         this.position = new Vector2(-1,-1);
         this.isActive = false;
      }
   }

   public int TotalPeopleOverall;
   
   public TrackingObject[] trackobjects = new TrackingObject[200];

   public void routeMessage(MqttMsgPublishEventArgs MqttMessage)
   {
      try
      {
         //Route infomation about cords and active tracking to the correct ID track object;

         // OC/TrackingPeople/IDs/{ID}/X, y, active
         if (MqttMessage.Topic.Contains("OC/TrackingPeople/IDs/"))
         {
            //Get the ID out of the topic
            string substring = MqttMessage.Topic.Substring(22);
            int ID = int.Parse(substring.Substring(0, substring.IndexOf('/')));
            //If position then store the position into the vector2 position for the trackobject
            if (substring.Substring(substring.LastIndexOf('/') + 1) == "Position")
            {
               string Messagestring = System.Text.Encoding.UTF8.GetString(MqttMessage.Message);
               trackobjects[ID].position =
                  new Vector2(int.Parse(Messagestring.Substring(0, Messagestring.IndexOf(','))),
                     int.Parse(Messagestring.Substring(Messagestring.IndexOf(',') + 1)));
            }

            //If active then set the bool to whether its active or not
            if (substring.Substring(substring.LastIndexOf('/') + 1) == "Active")
            {
               bool Messagebool = bool.Parse(System.Text.Encoding.UTF8.GetString(MqttMessage.Message));
               trackobjects[ID].isActive = Messagebool;
               if (Messagebool)
               {
                  ActiveTrackingID.Add(ID);
               }
               else
               {
                  ActiveTrackingID.Remove(ID);
               }
            }

         }

         if (MqttMessage.Topic.Contains("OC/TrackingPeople/Overall/"))
         {
            string substring = MqttMessage.Topic.Substring(MqttMessage.Topic.LastIndexOf('/') + 1);
            if (substring == "All")
            {
               TotalPeopleOverall = int.Parse(System.Text.Encoding.UTF8.GetString(MqttMessage.Message));
            }
         }
      }
      catch (System.Exception e)
      {
         Debug.LogError("Error Routing Of MQTT: " + e);
      }
   }
}
