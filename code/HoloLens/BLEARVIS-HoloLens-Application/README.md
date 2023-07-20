# BLEARVIS HoloLens Application

With this repository you can deploy and run the BLEARVIS HoloLens app. 
To work properly it depends on [its backend](https://github.com/Interactions-HSG/blearvis/code/HoloLens/BLEARVIS-Desktop-ObjectDetection). Thus, after having deployed this app successfully on Microsoft Hololens 2, head over to the backend folder to set it up as well.

## Getting Started

### 1 Software prerequisits

1. Make sure you have Windows 10 and Visual Studio installed according to [this guide](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/install-the-tools#installation-checklist). The Hololens emulator is not necessairily needed.
2. Download the [Unity version](https://store.unity.com/#plans-individual) that suits your needs best (most likely this is going to be the student version).
3. [Download and install](https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.dmg?_gl=1*m6afp9*_ga*MTUxNTA1NjAyNC4xNjI4Nzc5NDU0*_ga_1S78EFL1W5*MTYyODc3OTQ1NC4xLjEuMTYyODc3OTU5My41MA..&_ga=2.123804106.1133258571.1628779454-1515056024.1628779454) Unity hub.
4. Download Unity 2020.3 through the Unity Hub. The app was built on this version, however it might also run on newer ones.

### 2 Device Portal Credentials

[Configure](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal) the Hololens Device Portal as explained. Save and remember [your user credentials](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal#creating-a-username-and-password).

### 3 Clone this folder

Clone the repository and extract this folder _code\HoloLens\BLEARVIS-HoloLens-Application_.

### 4 Build & Deploy Unity Project

1. [Open](https://docs.unity3d.com/Manual/GettingStartedOpeningProjects.html) the Unity project, i.e. this folder.
2. [Switch](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02?tabs=openxr#switching-the-build-platform) the build plattform in Unity
3. [Build](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02?tabs=openxr#1-build-the-unity-project) the Unity Project (only execute the first step)
4. [Deploy](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-visual-studio?tabs=hl2#deploying-a-hololens-app-over-wi-fi) the UWP solution (over WIFI)

### 5 Modify endpoint

1. In the file *Assets/Scripts/HTTPListener.cs* make sure to insert the correct IP-address (along with port the same port you've set in the config.yml file in the backend in the HOLO_ENDPOINT_URL) for the Listener in line 33. 
2. Build the Unity application (step 3 in section 4). 
3. Deploy the Visual Studio Solution to Hololens

### 6 Run the HoloLens app

Now when both Unity applications (lab & office are deployed on the Hololens) we can run everything together. To do so, follow the steps below.

1. Start up the Hololens and sign in.
2. Start up the [backend]([https://github.com/Interactions-HSG/21-MT-JanickSpirig-DC-Holo](https://github.com/Interactions-HSG/blearvis/code/HoloLens/BLEARVIS-Desktop-ObjectDetection)). Make sure that in the [config file](https://github.com/Interactions-HSG/blearvis/code/HoloLens/BLEARVIS-Desktop-ObjectDetection/modules/YoloModule/app/config.yml) the value for `VIDEO_SOURCE` is correct (depends on the Hololens IP address, i.e. whether you are in the lab or the office).
3. Under *All Apps* start the `BLEARVIS` application.
4. Explore and get inspired 

## Included Code

- [Interactions-HSG/21-MT-JanickSpirig-Workforce-DC](https://github.com/Interactions-HSG/21-MT-JanickSpirig-Workforce-DC)
- [gpvigano/M2MqttUnity](https://github.com/gpvigano/M2MqttUnity). Released under the MIT License.
- [microsoft/MixedRealityToolkit-Unity](https://github.com/microsoft/MixedRealityToolkit-Unity/) Released under the MIT License.
