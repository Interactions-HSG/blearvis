# from os import initgroups
import initgroups
import time
import tensorflow as tf
import colorsys
from detector.yolov7 import YOLOv7
import os
physical_devices = tf.config.experimental.list_physical_devices('GPU')
if len(physical_devices) > 0:
    tf.config.experimental.set_memory_growth(physical_devices[0], True)
    # tf.config.experimental.set_memory_growth(physical_devices[1], True)     ### changed by Jannis
    # os.environ["CUDA_VISIBLE_DEVICES"]="GPU"
    
from absl import app, flags, logging
from absl.flags import FLAGS

import detector.core.utils as utils

from detector.core.yolov4 import filter_boxes

from tensorflow.python.saved_model import tag_constants
from PIL import Image
import cv2
import numpy as np
from tensorflow.compat.v1 import ConfigProto
from tensorflow.compat.v1 import InteractiveSession

# https://github.com/VikasOjha666/yolov7_to_tflite/blob/main/yoloV7_to_TFlite%20.ipynb

import cv2
import random
import numpy as np
from PIL import Image
import tensorflow as tf

from detector.core.config import cfg
'''
################################################################################
                            For YOLOv7 model in ONNX format
################################################################################
'''
class Detector:
    def __init__(self,
            tiny=False,
            framework='tf',
            model='yolov4',
            custom=False,
            iou=0.45,
            score=0.25
            ):
        # '''
        self.config = config = ConfigProto()
        self.config.gpu_options.allow_growth = True
        self.session = InteractiveSession(config=config)
        self.input_size = 416   # was 416
        self.framework = framework
        self.model = model
        self.custom = custom
        self.tiny = tiny
        self.iou = iou
        self.score = score
        self.STRIDES, self.ANCHORS, self.NUM_CLASS, self.XYSCALE = utils.load_config(self.tiny, self.model)
        
        
        # Initialize YOLOv7 object detector
        ############################################################
        # CHANGE THIS TO POINT TO YOUR MODEL !!!!!
        ############################################################
        model_path = "detector/checkpoints/best_blearvis.onnx"
        self.yolov7_detector = YOLOv7.YOLOv7(model_path, conf_thres=0.5, iou_thres=0.5,  official_nms=True)
        
        print("Initialized ONNX Detector")
        print(f"ONNX model path: {model_path}")

      
    def detect(self, frame):
        classes=utils.read_class_names(cfg.YOLO.CLASSES)
        
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        boxes, scores, class_ids = self.yolov7_detector(frame)

        combined_img = self.yolov7_detector.draw_detections(frame)
        
        detections = []
        for box, score, class_id in zip(boxes, scores, class_ids):
            x1, y1, x2, y2 = box.astype(int)
            top_left = (x1,y1)
            bottom_right = (x2,y2)
            detections.append((classes[class_id], score, top_left,bottom_right))


        # print(f"detections: {detections}")
        
        return detections, combined_img
        
        
        
        

