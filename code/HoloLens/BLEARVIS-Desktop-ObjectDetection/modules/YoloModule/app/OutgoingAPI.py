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
    
    def handleThing(self, thing, display, coord1, coord2):

        if self.statusHandler.statuses[thing] != display:
            
            url = "{}/?{}={}&coordTLx={}&coordTLy={}&coordBRx={}&coordBRy={}".format(self.holo_url, urllib.parse.quote(thing), str(display), str(int(coord1[0])), str(int(coord1[1])), str(int(coord2[0])), str(int(coord2[1])))
            print(url)
            
            # only send request to hololens if thing is there
            if (display == 1):
                try:
                    r = requests.get(url, timeout=120)
                    print(r)
                    if r.status_code == 200:
                        print("Notified Hololens that thing {} is present successfully!".format(thing))
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


            
            # update the current state, whether we are displaying the thing on the Hololens or not
            self.statusHandler.statuses[thing] = display
        else:
            return
