# from os import initgroups
import initgroups
import time
import tensorflow as tf
import colorsys
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

class Detector_TFLite:
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
     # Load the TFLite model
        self.interpreter = tf.lite.Interpreter(model_path="detector/checkpoints/yolov7-blearvis-new.tflite")
        
        if not self.custom:
            if self.tiny:
                self.weights = 'detector/checkpoints/yolov4-tiny-416'
            else:
                self.weights = 'detector/checkpoints/yolov4-416'
        else:
            if self.tiny:
                self.weights = 'detector/checkpoints/custom-tiny-416'
            else:
                # self.weights = 'detector/checkpoints/custom-416'
                self.weights = 'detector/checkpoints/weights-best-blearvis'   
                # self.weights = 'detector/checkpoints/blearvis-best-416'   # THIS ARE THE WORKING WEIGHTS !!!!!!!!!!!!!!
                # self.weights = 'detector/checkpoints/blearvis-608'   # ADDED
                # self.weights = 'detector/checkpoints/custom-608'
                
                # print("path with 608")
        '''
        if self.framework == 'tflite':
            self.interpreter = tf.lite.Interpreter(model_path=self.weights)
            self.interpreter.allocate_tensors()
            self.input_details = self.interpreter.get_input_details()
            self.output_details = self.interpreter.get_output_details()
        else:
            self.saved_model_loaded = tf.saved_model.load(self.weights, tags=[tag_constants.SERVING])
            self.infer = self.saved_model_loaded.signatures['serving_default']
        '''
        print("TensorFlow Yolo::__init__()")
        print("TensorFlow Model : %s" % (self.model))
        print("===============================================================")
        print("Initialising the TensorFlow model with the following parameters: ")
        print("   - Tiny            : " + str(self.tiny))
        print("   - Weights         : " + self.weights)
        print("")
        # '''
        print("Initialized Detector")

    '''
    This detect-function only works with a Yolov7 tflite file!!!!
    '''    
    def detect(self, frame):
        classes=utils.read_class_names(cfg.YOLO.CLASSES)
        #########################################
        # print("TensorFlow Yolo::__init__()")
        # print("TensorFlow Model : %s" % (self.model))
        # print("===============================================================")
        # print("Initialising the TensorFlow model with the following parameters: ")
        # print("   - Tiny            : " + str(self.tiny))
        # print("   - Weights         : " + self.weights)
        # print("")
        
       

        #Name of the classes according to class indices.
        # names = ['m1', 'm2', 'm3', 'm4', 'm5']
        names = classes
        #Creating random colors for bounding box visualization.
        # colors = {name:[random.randint(0, 255) for _ in range(3)] for i,name in enumerate(names)}
        
        num_classes = len(classes)
        hsv_tuples = [(1.0 * x / num_classes, 1., 1.) for x in range(num_classes)]
        colors = list(map(lambda x: colorsys.hsv_to_rgb(*x), hsv_tuples))
        colors = list(map(lambda x: (int(x[0] * 255), int(x[1] * 255), int(x[2] * 255)), colors))

        '''
        #Load and preprocess the image.
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        frame_size = frame.shape[:2]
        image_data = cv2.resize(frame, (self.input_size, self.input_size))
        image_data = image_data / 255.
        image_data = image_data[np.newaxis, ...].astype(np.float32)
        '''
        # img = cv2.imread(frame)
        img = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        # img_shape = img.shape[:2]
        image = img.copy()
        # print("image type: ", type(image))
        # print("print image:")
        # print(image)
        if (img.size ==0):
            print("Image size is zero. Aborting.")
            return [], img
        image, ratio, dwdh = utils.letterbox(image, auto=False)
        image = image.transpose((2, 0, 1))
        image = np.expand_dims(image, 0)
        image = np.ascontiguousarray(image)

        im = image.astype(np.float32)
        im /= 255


        #Allocate tensors.
        self.interpreter.allocate_tensors()
        # Get input and output tensors.
        input_details = self.interpreter.get_input_details()
        output_details = self.interpreter.get_output_details()

        # Test the model on random input data.
        input_shape = input_details[0]['shape']
        self.interpreter.set_tensor(input_details[0]['index'], im)

        self.interpreter.invoke()

        # The function `get_tensor()` returns a copy of the tensor data.
        # Use `tensor()` in order to get a pointer to the tensor.
        output_data = self.interpreter.get_tensor(output_details[0]['index'])

        ori_images = [img.copy()]
        detections = []
        
        for i,(batch_id,x0,y0,x1,y1,cls_id,score) in enumerate(output_data):
            image = ori_images[int(batch_id)]
            box = np.array([x0,y0,x1,y1])
            box -= np.array(dwdh*2)
            box /= ratio
            box = box.round().astype(np.int32).tolist()
            cls_id = int(cls_id)
            score = round(float(score),3)
            name = names[cls_id]
            color = colors[cls_id]
            name += ' '+str(score)
            cv2.rectangle(image,box[:2],box[2:],color,2)
            cv2.putText(image,name,(box[0], box[1] - 2),cv2.FONT_HERSHEY_SIMPLEX,0.75,[225, 255, 255],thickness=2)  
            if (score > 0):
                detections.append((classes[cls_id], score, box[:2], box[2:]))

        # prediction = Image.fromarray(ori_images[0])
        # prediction.show()
        
        print(f"detections: {detections}")
        
         # EDIT_JANNIS_Coordinates  
        # return detections, image, coord1, coord2
        return detections, ori_images[0]
        
        ''' Code for Yolov4
        #########################################
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        frame_size = frame.shape[:2]
        image_data = cv2.resize(frame, (self.input_size, self.input_size))
        image_data = image_data / 255.
        image_data = image_data[np.newaxis, ...].astype(np.float32)
        start_time = time.time()

        if self.framework == 'tflite':
            self.interpreter.set_tensor(self.input_details[0]['index'], image_data)
            self.interpreter.invoke()
            pred = [self.interpreter.get_tensor(self.output_details[i]['index']) for i in range(len(self.output_details))]
            if self.model == 'yolov3' and self.tiny == True:
                boxes, pred_conf = filter_boxes(pred[1], pred[0], score_threshold=0.25,
                                                input_shape=tf.constant([self.input_size, self.configinput_size]))
            else:
                boxes, pred_conf = filter_boxes(pred[0], pred[1], score_threshold=0.25,
                                                input_shape=tf.constant([self.input_size, self.input_size]))
        else:
            batch_data = tf.constant(image_data)
            pred_bbox = self.infer(batch_data)
            for key, value in pred_bbox.items():
                boxes = value[:, :, 0:4]
                pred_conf = value[:, :, 4:]

        boxes, scores, classes, valid_detections = tf.image.combined_non_max_suppression(
            boxes=tf.reshape(boxes, (tf.shape(boxes)[0], -1, 1, 4)),
            scores=tf.reshape(
                pred_conf, (tf.shape(pred_conf)[0], -1, tf.shape(pred_conf)[-1])),
            max_output_size_per_class=50,
            max_total_size=50,
            iou_threshold=self.iou,
            score_threshold=self.score
        )

        pred_bbox = [boxes.numpy(), scores.numpy(), classes.numpy(), valid_detections.numpy()]
        
        # EDIT_JANNIS_Coordinates  
        # detections, image, coord1, coord2 = utils.draw_bbox(frame, pred_bbox)
        detections, image = utils.draw_bbox(frame, pred_bbox)
        print(f"detections: {detections}")
        
        # EDIT_JANNIS_Coordinates  
        # return detections, image, coord1, coord2
        return detections, image
        '''
        

