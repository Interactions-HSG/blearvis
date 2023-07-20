#To make python 2 and python 3 compatible code
from __future__ import division
from __future__ import absolute_import

import cv2
import numpy as np
import requests
import time
import json
import os
import signal
import time

import VideoStream
from VideoStream import VideoStream
from OutgoingAPI import APIHandler

#import YoloInference
#from YoloInference import YoloInference

import detector
from detector.detector import Detector
from detector.core.utils import draw_bbox

class VideoCapture(object):

    def __init__(
            self,
            videoPath,
            inference,
            confidenceLevel,
            custom,
            custom_classes,
            tiny,
            show,
            result_path,
            min_time,
            holo_endpoint,
            holo_url
            ):

        self.videoPath = videoPath
        self.inference = inference
        self.confidenceLevel = confidenceLevel
        self.useStream = False
        self.useWebcam = False
        self.useMovieFile = False
        self.frameCount = 0
        self.vStream = None
        self.vCapture = None
        self.displayFrame = None
        self.captureInProgress = False
        self.custom = custom
        self.custom_classes = custom_classes
        self.tiny = tiny
        self.show_result = show
        self.result_path = result_path
        self.recommendation_thresh = min_time
        self.holo_endpoint = holo_endpoint

        print("VideoCapture::__init__()")
        print("OpenCV Version : %s" % (cv2.__version__))
        print("===============================================================")
        print("Initialising Video Capture with the following parameters: ")
        print("   - Video path       : " + str(self.videoPath))
        print("   - Inference?       : " + str(self.inference))
        print("   - ConficenceLevel  : " + str(self.confidenceLevel))
        print("   - CustomDetection? : " + str(self.custom))
        if self.custom:
            print("   - Custom Classes   : " + str(self.custom_classes))
        print("   - HololensEndpoint?: " + str(self.holo_endpoint))
        if self.holo_endpoint:
            print("   - HololensUrl      : " + str(holo_url))
        print("")

        self.yoloInference = Detector(tiny=self.tiny, custom=self.custom)  # yolov4

        if self.custom:
            self.apiHandler = APIHandler(holo_url, self.custom_classes)
        else:
            self.apiHandler =APIHandler(holo_url)

    def __IsCaptureDev(self, videoPath):
        try: 
            return '/dev/video' in videoPath.lower()
        except (ValueError, AttributeError):
            return videoPath == 0

    def __IsRtsp(self, videoPath):
        try:
            if 'rtsp:' in videoPath.lower() or '/api/holographic/stream' in videoPath.lower():  # or '0' in videoPath:
                return True
        except ValueError:
            return False

    def __enter__(self):

        self.setVideoSource(self.videoPath)

        return self

    def setVideoSource(self, newVideoPath):

        if self.captureInProgress:
            self.captureInProgress = False
            time.sleep(1.0)
            if self.vCapture:
                self.vCapture.release()
                self.vCapture = None
            elif self.vStream:
                self.vStream.stop()
                self.vStream = None

        if self.__IsRtsp(str(newVideoPath)):
            print("\r\n===> RTSP Video Source")

            self.useStream = True
            self.useMovieFile = False
            self.useWebcam = False
            self.videoPath = newVideoPath

            if self.vStream:
                self.vStream.start()
                self.vStream = None

            if self.vCapture:
                self.vCapture.release()
                self.vCapture = None

            self.vStream = VideoStream(newVideoPath).start()
            # Needed to load at least one frame into the VideoStream class
            time.sleep(1.0)
            self.captureInProgress = True

        elif self.__IsCaptureDev(newVideoPath):
            print("===> Webcam Video Source")
            if self.vStream:
                self.vStream.start()
                self.vStream = None

            if self.vCapture:
                self.vCapture.release()
                self.vCapture = None

            self.videoPath = newVideoPath
            self.useMovieFile = False
            self.useStream = False
            self.useWebcam = True
            self.vCapture = cv2.VideoCapture(newVideoPath)
            if self.vCapture.isOpened():
                self.captureInProgress = True
            else:
                print("===========================\r\nWARNING : Failed to Open Video Source\r\n===========================\r\n")
        else:
            print("===========================\r\nWARNING : No Video Source\r\n===========================\r\n")
            self.useStream = False
            self.useYouTube = False
            self.vCapture = None
            self.vStream = None
        return self

    def get_display_frame(self):
        return self.displayFrame

    def videoStreamReadTimeoutHandler(signum, frame):
        raise Exception("VideoStream Read Timeout") 

    def start(self):
        while True:
            if self.captureInProgress:
                self.__Run__()

            if not self.captureInProgress:
                time.sleep(1.0)

    def __Run__(self):

        print("===============================================================")
        print("videoCapture::__Run__()")
        print("   - Stream          : " + str(self.useStream))
        print("   - useMovieFile    : " + str(self.useMovieFile))
        print("   - useWebcam       : " + str(self.useWebcam))

        '''
        # check if stream is opened
        if self.useStream:
            print("Stream is opened {}".format(self.vStream.stream.isOpened()))
        elif self.useWebcam:
            print("Stream is opened {}".format(self.vCapture.isOpened()))
        '''

        # Check camera's FPS
        if self.useStream:
            cameraFPS = int(self.vStream.stream.get(cv2.CAP_PROP_FPS))
            frame_width = int(self.vStream.stream.get(cv2.CAP_PROP_FRAME_WIDTH))
            frame_height = int(self.vStream.stream.get(cv2.CAP_PROP_FRAME_HEIGHT))
        elif self.useWebcam:
            cameraFPS = int(self.vCapture.get(cv2.CAP_PROP_FPS))
            frame_width = int(self.vCapture.get(cv2.CAP_PROP_FRAME_WIDTH))
            frame_height = int(self.vCapture.get(cv2.CAP_PROP_FRAME_HEIGHT))

        if cameraFPS == 0:
            print("Error : Could not get FPS")
            raise Exception("Unable to acquire FPS for Video Source")
            return

        print("Frame rate (FPS)     : " + str(cameraFPS))
        print("Frame width          : " + str(frame_width))
        print("Frame height         : " + str(frame_height))
        
        if self.holo_endpoint:
            self.apiHandler.send_generic_get_request(params=["frame_width","frame_height"], values=[frame_width,frame_height])

        currentFPS = cameraFPS
        perFrameTimeInMs = 1000 / cameraFPS

        # by default VideoCapture returns float instead of int
        if self.show_result: 
            codec = cv2.VideoWriter_fourcc(*"XVID")
            out = cv2.VideoWriter(self.result_path, codec, cameraFPS, (frame_width, frame_height))

        index_boundary = 0
        detections_queue = []
        last_fps = 0

        while True:
            tFrameStart = time.time()

            if not self.captureInProgress:
                break
            try:
                if self.useStream:
                    frame = self.vStream.read()
                    
                else:
                    frame = self.vCapture.read()[1]
                    
            except Exception as e:
                print("ERROR : Exception during capturing")
                raise(e)
            
            # Run Object Detection
            if self.inference:
                
                detections, image = self.yoloInference.detect(frame)

                fps = 1.0 / (time.time() - tFrameStart)

                # update index boundary if FPS rate has changed significantly, tests have shown that all equal or below 0.3 is not significant
                if (fps - last_fps) > 0.3:
                    # how many elements back in the queue we have to scan for an object
                    # index_boundary is the index position of the frame that was processed MIN_SECONDS ago
                    index_boundary = int(round(fps / (1/self.recommendation_thresh), 0))
                    
                print("FPS: %.2f" % fps)

                last_fps = fps

                if self.show_result:
                    result = np.asarray(image)
                    result = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
                    out.write(result)
                
                # handle Object presence
                if len(detections) > 0:
                    queue_list = []
                    for detection in detections:
                        classLabel, confidence, coordinateTopLeft, coordinateBottomRight = detection[0], detection[1], detection[2], detection[3]
                        if confidence > self.confidenceLevel:
                            
                            queue_list.append(classLabel)
                            
                            print("Object: {}".format(classLabel))
                            print("Confidence: {}".format(confidence))

                            # if notification should be send out to endpoint that thing is present
                            if self.holo_endpoint:
                                try:
                                    print("Thing is there at ", time.time())
                                    numberOfThingsInScene =  len(detections)
                                    self.apiHandler.handleThing(thing=classLabel, display=1, coord1=coordinateTopLeft, coord2=coordinateBottomRight, numberOfThingsInScene=numberOfThingsInScene, framestart = time.time()) # send call to display all actions on the Hololens that are related with this object
                                
                                # when thing is not of interest to us
                                except KeyError:
                                    print("KeyError")
                                    print("Object: {}".format(classLabel))
                                    print("Confidence: {}".format(confidence))
                                    pass


                # build the new queue element
                try:
                    if len(queue_list) > 0:
                        # made some detections
                        detections_queue.append(queue_list)
                        queue_list = []
                    else:
                        # confidence too low or no detections at all
                        print("confidence too low or no detections at all")
                        detections_queue.append(["NaN"])
                except NameError:
                    # no detections at all
                    print("no detections at all")
                    detections_queue.append(["NaN"])
                         
                # calculate new first element of queue
                first_element = len(detections_queue) - index_boundary
                if first_element > 0:
                    # update queue, leave out elements that are longer than MIN_TIME seconds ago
                    detections_queue = detections_queue[first_element:]

                print(detections_queue)

                # get list of all objects that are currently there
                things_displayed = [thing for thing, pres in self.apiHandler.statusHandler.statuses.items() if pres == 1]

                # check if all object are still there
                for thing in things_displayed: 
                    # check if thing is still somewhere in the queue
                    ThingIsThere = False
                    for i in detections_queue:
                        if any(thing in sl for sl in i):
                            ThingIsThere = True
                            break
                    
                    # thing is no longer in queue
                    if not ThingIsThere:
                        # update state
                        numberOfThingsInScene =  len(detections)
                        # self.apiHandler.handleThing(thing=thing, display=0, coord1=coordinateTopLeft, coord2=coordinateBottomRight, numberOfThingsInScene=numberOfThingsInScene, framestart = time.time())
                        print("Thing {} is not present anymore.".format(thing))
                
            # Calculate FPS rate at which the VideoCapture is able to process the frames
            timeElapsedInMs = (time.time() - tFrameStart) * 1000
            currentFPS = 1000.0 / timeElapsedInMs

            if (currentFPS > cameraFPS):
                # Cannot go faster than Camera's FPS
                currentFPS = cameraFPS

            timeElapsedInMs = (time.time() - tFrameStart) * 1000

            if (1000 / cameraFPS) > timeElapsedInMs:
                # This is faster than image source (e.g. camera) can feed.  
                waitTimeBetweenFrames = perFrameTimeInMs - timeElapsedInMs
                time.sleep(waitTimeBetweenFrames/1000.0)

    def __exit__(self, exception_type, exception_value, traceback):

        if self.vCapture:
            self.vCapture.release()

        # self.imageServer.close()
        cv2.destroyAllWindows()