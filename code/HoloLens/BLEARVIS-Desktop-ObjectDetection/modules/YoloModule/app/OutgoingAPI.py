import urllib.parse

import requests
from StatusHandler import StatusHandler

class APIHandler:
    def __init__(self, holo_url, custom_classes=[]):

        self.holo_url = holo_url

        if len(custom_classes) > 0:
            self.statusHandler = StatusHandler(custom_classes)
        else:
            self.statusHandler = StatusHandler()
    
    
    def send_get_request(self, url):
        try:
            r = requests.get(url, timeout=120)
            print(r)
            if r.status_code == 200:
                print("Request {} succeded with status code {}".format(url, r.status_code))
            else:
                print("Request {} failed with status code {}".format(url, r.status_code))
        except requests.ConnectionError as e:
            print("OOPS!! Connection Error. Make sure you are connected to Internet. Technical Details given below.\n")
            print(str(e))
        except requests.Timeout as e:
            print("OOPS!! Timeout Error")
            print(str(e))
        except requests.RequestException as e:
            print("OOPS!! General Error")
            print(str(e))
        else:
            return
        
    def handleThing(self, thing, display, coord1, coord2, numberOfThingsInScene, framestart):

        framestart = int(framestart)
        url = "{}/?{}={}&coordTLx={}&coordTLy={}&coordBRx={}&coordBRy={}&&numberOfThingsInScene={}&framestart={}".format(self.holo_url, urllib.parse.quote(thing), str(display), str(int(coord1[0])), str(int(coord1[1])), str(int(coord2[0])), str(int(coord2[1])), str(numberOfThingsInScene), str(framestart))
              
        print(url)
            
        self.send_get_request(url) 
        
        # update the current state, whether we are displaying the thing on the Hololens or not
        self.statusHandler.statuses[thing] = display

    def send_generic_get_request(self, params, values):

        parameters = ""
        for param, value in zip(params, values):
            parameters += f"{param}={value}&"

        url = "{}/?{}".format(self.holo_url,parameters)
        print(url)
        self.send_get_request(url)
            
            
    