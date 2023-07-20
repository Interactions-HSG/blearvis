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

import cv2
import random
import numpy as np
from PIL import Image
import tensorflow as tf

from detector.core.config import cfg

import cv2
import time
import requests
import random
import numpy as np
import onnxruntime as ort
from PIL import Image
from pathlib import Path
from collections import OrderedDict,namedtuple

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
        model_path = "detector/data/BLEARVIS_BestWeights_YOLOv7.onnx"
   
        cuda = True # set to false for cpu
        self.providers = ['CUDAExecutionProvider', 'CPUExecutionProvider'] if cuda else ['CPUExecutionProvider']
        self.session = ort.InferenceSession(model_path, providers=self.providers)

        print("Initialized ONNX Detector")
        print(f"ONNX model path: {model_path}")

      
    def detect(self, frame):
        classes=utils.read_class_names(cfg.YOLO.CLASSES)
        colors = {name:[random.randint(0, 255) for _ in range(3)] for i,name in enumerate(classes)}

        img = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

        image = img.copy()
        image, ratio, dwdh = utils.letterbox(image, auto=False)
        image = image.transpose((2, 0, 1))
        image = np.expand_dims(image, 0)
        image = np.ascontiguousarray(image)

        im = image.astype(np.float32)
        im /= 255
        im.shape

        outname = [i.name for i in self.session.get_outputs()]
        outname

        inname = [i.name for i in self.session.get_inputs()]
        inname

        inp = {inname[0]:im}
        
        # ONNX inference
        outputs = self.session.run(outname, inp)[0]
        outputs
        
        ori_images = [img.copy()]
        detections = []
        
        for i,(batch_id,x0,y0,x1,y1,cls_id,score) in enumerate(outputs):
            image = ori_images[int(batch_id)]
            box = np.array([x0,y0,x1,y1])
            box -= np.array(dwdh*2)
            box /= ratio
            box = box.round().astype(np.int32).tolist()
            cls_id = int(cls_id)
            score = round(float(score),3)
            name = classes[cls_id]
            color = colors[cls_id]
            name += ' '+str(score)
            cv2.rectangle(image,box[:2],box[2:],color,2)
            cv2.putText(image,name,(box[0], box[1] - 2),cv2.FONT_HERSHEY_SIMPLEX,0.75,[225, 255, 255],thickness=2)  
            detections.append((classes[cls_id], score, box[:2],box[2:]))

        combined_img = ori_images[0]
        
        return detections, combined_img
        
        
        
        

