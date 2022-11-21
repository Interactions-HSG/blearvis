#!/usr/bin/python
from __future__ import print_function

from time import gmtime, strftime, sleep
from bluepy.btle import Scanner, DefaultDelegate, BTLEException
import struct
import paho.mqtt.client as mqtt
from base64 import b64decode,b64encode
import sys

DEBUG = 0
BEACON_STARTING_BYTES = "beef0215e2c56db5dffb48d2b060d0f5a71096e0"

class ScanDelegate(DefaultDelegate):
    def __init__(self, mqtt_client):
        self.mqtt_client = mqtt_client

    def handleDiscovery(self, dev, isNewDev, isNewData):
        global DEBUG
        rawbytes = str(dev.getScanData()[-1][2])
            
        if not rawbytes.startswith(BEACON_STARTING_BYTES):
            return

        print(strftime("%Y-%m-%d %H:%M:%S", gmtime()), dev.addr, dev.getScanData()[-1][2])
        maj_num = rawbytes[40:44]
        min_num = rawbytes[44:48]

        if DEBUG:
            print("maj_num: {} \t min_num: {}".format(maj_num,min_num))

        temperature_raw = (struct.unpack('>h', bytes.fromhex(maj_num))[0])
        humidity_raw = (struct.unpack('>h', bytes.fromhex(min_num))[0])

        if DEBUG:
            print("Raw H: {} \t Raw T: {}".format(humidity_raw, temperature_raw))

        temperature = (temperature_raw / 100.0)
        humidity =  (humidity_raw / 100.0)
           
        addr = "ble-pd-{}".format(dev.addr.upper().replace(":",""))
            
        self.mqtt_client.publish("sensor/temp/{}".format(addr), temperature, 1, False)
        self.mqtt_client.publish("sensor/hum/{}".format(addr), humidity, 1, False)

        print("H: {:.3f}% \t T: {:.3f} C".format(humidity, temperature))

        sys.stdout.flush()

# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

# The callback for when a PUBLISH message is received from the server.
def on_message(client, userdata, msg):
    print(msg.topic+" "+str(msg.payload))

client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

client.connect("127.0.0.1", 1883, 30)

# Not blocking call that processes network traffic, dispatches callbacks and
# handles reconnecting.
client.loop_start()

scanner = Scanner().withDelegate(ScanDelegate(client))

# listen for ADV_IND packages
scanner.scan(0, passive=True)
