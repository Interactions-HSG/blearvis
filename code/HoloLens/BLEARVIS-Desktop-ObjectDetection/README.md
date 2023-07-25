# BLEARVIS: Hololens YOLOv7 Object Detection 

This code is largely based on: [https://github.com/Interactions-HSG/21-MT-JanickSpirig-HoloLens-ObjectDetection](https://github.com/Interactions-HSG/21-MT-JanickSpirig-HoloLens-ObjectDetection). 
There are some adaptions concerning the Outgoing API and the YOLO version was changed to YOLOv7 using ONNX weights.

---

With the content of this folder you can access and evaluate the camera stream of your Microsoft Hololens 2 to recognize at what objects the user is currently looking at. 
The code is largely a combination of two repositories.

- Access video-stream of Hololens PV camera: [IntelligentEdgeHOL](https://github.com/Azure/IntelligentEdgeHOL)
- Run YOLOv7 object detection [/WongKinYiu/yolov7](https://github.com/WongKinYiu/yolov7) and [https://github.com/ibaiGorordo/ONNX-YOLOv7-Object-Detection](https://github.com/ibaiGorordo/ONNX-YOLOv7-Object-Detection) with ONNX weights [YOLOv7onnx.ipynb](https://colab.research.google.com/github/WongKinYiu/yolov7/blob/main/tools/YOLOv7onnx.ipynb)

## Weights

- Pre-trained weights for BLEARVIS: [./modules/YoloModule/app/detector/data/BLEARVIS_BestWeights_YOLOv7.onnx](https://github.com/Interactions-HSG/blearvis/tree/main/code/HoloLens/BLEARVIS-Desktop-ObjectDetection/modules/YoloModule/app/detector/data/BLEARVIS_BestWeights_YOLOv7.onnx) with the two robots (tractorbot and roboticarm).

## Geeting Started

### [1] Device Portal Credentials

[Configure](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal) the Hololens Device Portal. Save and remember [your user credentials](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal#creating-a-username-and-password).

### [2] Clone repository

`git clone https://github.com/Interactions-HSG/blearvis`

Extract the folder _code/HoloLens/BLEARVIS-Desktop-Object-Detection_.

### [3] Setup YOLOv7
- Train your YOLOv7 model (see: [https://github.com/WongKinYiu/yolov7](https://github.com/WongKinYiu/yolov7)) and convert it to ONNX using the notebook [YOLOv7onnx.ipynb](https://colab.research.google.com/github/WongKinYiu/yolov7/blob/main/tools/YOLOv7onnx.ipynb)
- Create and activate your conda environment by first [downloading anaconda](https://docs.anaconda.com/anaconda/install/index.html) and then creating a new environment based on the file [yolov7-env.yaml](./yolov7-env.yaml). (For using CPU instead of GPU: Run `pip uninstall onnxruntime-gpu` and `pip install onnxruntime`.)
- In _/detector/detector.py_ inlcude the link to your model in ONNX format in line 77,: `model_path = "detector/checkpoints/best.onnx"`
- If you use custom weights, place your custom `.names` file (e.g. `obj.names`) into folder _detector\data_ and comment out line 18 in file _detector\core\config.py_ and comment line 15. It should look like in the screenshot below. Remember that anytime you switch between custom wnd pretrained coco weights (pre-trained) you have to adjust these two lines of code.

<img width="541" alt="Screenshot 2021-08-15 at 15 04 22" src="https://user-images.githubusercontent.com/43849960/129479546-edf3ba64-9743-4e59-96b2-e42444e83af5.png">

### [4] Setup config.yml

Define the options in the file `config.yml` according to your needs.  

| Parameter              | Details                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `CUSTOM`               | Defines whether YOLOv4 has been trained for custom classes. Set to `TRUE` if you have set up YOLOv4 for custom classes                                                                                                                                                                                                                                                                                                                                                                                                    |
| `CUSTOM_CLASSES`       | Stores the labels of all custom classes. If `CUSTOM` is set to true, list here all class labels as bullet points (use `-` and insert line break after each label                                                                                                                                                                                                                                                                                                                                                          |
| `USE_YOLO-TINY`        | Defines whether the yolo weights are tiny or normal ones. Set to `TRUE` if you are using tiny weights                                                                                                                                                                                                                                                                                                                                                                                                                     |
| `VIDEO_SOURCE`         | Defines the URL under which the Hololens camera stream is accessible: `https://<DEVICE-PORTAL-USER>:<DEVICE-PORTAL-PWD>@<HOLOLENS-IP>/api/holographic/stream/live.mp4?holo=true&pv=true&mic=true&loopback=true` The user and pwd are the ones you have defined when setting up the device portal. To come up with the IP address of the Hololens follow [this guide](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal#connecting-over-wi-fi) |
| `USE_WEBCAM`           | Defines whether the camera stream of the Webcam should be used. Set to `TRUE` if you want to run object recognition on your webcam's stream (e.g. to see if YOLOv4 works)                                                                                                                                                                                                                                                                                                                                                 |
| `MIN_CONFIDENCE_LEVEL` | Defines the minimum confidence for YOLOv7 in order to label an object with a class name. Set to your desired confidence level (e.g., `0.7`)                                                                                                                                                                                                                                                                                                                                                                               |
| `MIN_TIME`             | Defines the minimum time in seconds that an object must be present in the camera view before the information is sent to the Hololens that a certain object is present. This can be useful to sitinguish between the user just walking around in a room and looking at things randomly and the user looking at a thing on purpose. Set this for example to `1.5`                                                                                                                                                           |
| `SHOW_OUTPUT`          | Defines whether an output video should be produced under `RESULT_PATH` which contains rectangle boxes and class labels. Set to `True` if you want to double-check the YOLOv7 detections                                                                                                                                                                                                                                                                                                                                   |
| `RESULT_PATH`          | Defines the path unser which the result video will be saved (if `SHOW_OUTPUT` is set to `True`)                                                                                                                                                                                                                                                                                                                                                                                                                           |
| `HOLO_ENDPOINT`        | Defines whether an information should be sent to a Hololens or any arbitrary endpoint everytime an object appears in the user's view. Set to `True` if you have set up an endpoint on your Hololens that is able to receive incoming requests. [This example](https://github.com/janick187/Hololens-frontend/blob/master/Assets/Scripts/HTTPListener.cs) shows you how such an endpoint can be setup within a Unity application                                                                                           |
| `HOLO_ENDPOINT_URL`    | Defines the URL and Port of the endpoint (e.g., http://192.168.0.2:5050) to which an HTTP-GET-request will be sent to whenever a new object has been detected. Remember to redefine the port in the URL if necessary and make sure it matches the one in the [HTTPListener.cs](https://github.com/Interactions-HSG/blearvis/blob/main/code/HoloLens/BLEARVIS-HoloLens-Application/Assets/Scripts/HTTPListener.cs) file.                                                                                                                                                                                                                                                        |

### [5] Run the detection

Now you are all set and ready to run YOLOv7 object detection on the Microsoft Hololens PV camera. Execute the following steps to start the detection.

1. Start up the Hololens and log in. Make sure it is charged sufficiently as the PV camera has heavy battery usage.
2. Activate the conda environment.
3. `cd modules/YoloModule/app`
4. `python main.py`
5. To terminate the programm press CTRL-C.
