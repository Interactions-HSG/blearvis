# Expert Digital Companion for Workforce Assistance

With this repository you can deploy and run an expert Digital Companion that assists the workforce in the shopfloor (room 61-102) and office (61-402) at HSG-ICS.
To work properly the Digital Companion depends on [its backend](https://github.com/Interactions-HSG/21-MT-JanickSpirig-DC-Holo). Thus, after having deployed this repository successfully on Microsoft Hololens 2, head over to the backend repository to set it up as well.

## Geeting Started

### 1 Software prerequisits
1. Make sure you have Windows 10 and Visual Studio installed according to [this guide](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/install-the-tools#installation-checklist). The Hololens emulator is not necessairily needed.
2. Download the [Unity version](https://store.unity.com/#plans-individual) that suits your needs best (most likely this is going to be the student version).
3. [Download and install](https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.dmg?_gl=1*m6afp9*_ga*MTUxNTA1NjAyNC4xNjI4Nzc5NDU0*_ga_1S78EFL1W5*MTYyODc3OTQ1NC4xLjEuMTYyODc3OTU5My41MA..&_ga=2.123804106.1133258571.1628779454-1515056024.1628779454) Unity hub.

### 2 Device Portal Credentials
[Configure](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal) the Hololens Device Portal as explained. Save and remember [your user credentials](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-the-windows-device-portal#creating-a-username-and-password).

### 3 Clone this repository

### 4 Setup MiroCard & RPi
In the shopfloor environment, the current room temperature is measured with the help of a [MiroCard](https://mirocard.swiss). The MiroCard sends the current temperature and humidity value as BLE advertisment which can be received by any device like a Rasperry Pi. On the Rasperry Pi a simple flask web-server is executed which provides an endpoint over which the Digital Companion can access the current temperature value.
1. Get an RPi and [set it up](https://community.wia.io/d/11-how-to-set-up-a-raspberry-pi-without-an-external-monitor-or-keyboard). **IMPORTANT:** The RPi has to be connected to the same network as the Hololens (`labnet`). Remember the IP address of your RPi.
2. Open a terminal and connect with the RPi using `ssh pi@<IP>`
3. Once you have successfully established an ssh session to your RPi, [install Git](https://www.atlassian.com/git/tutorials/install-git#linux).
4. Clone the MiroCard repository with `git clone https://github.com/janick187/mirocard-scanner-python
5. Place the MiroCard close to the RPi and [follow the instructions](https://github.com/janick187/mirocard-scanner-python) in the readme to run the MiroCard reader as well as the Flask web server. **NOTE:** You need two seperate ssh sessions, one for running the reader and the second one for running the web server.
6. You should now be able to access te current temperature value with `HTTP GET http://{RPi_IP}:8080/temperature`.

### 5 Modify endpoint
As a different RPi is used then during deployment and testing of the Digital Companion. Thus, in file *Assets/Scripts/TemperatureController.cs* in *line 45* change the value of the variable `apiEndpoint` to `http://{RPi_IP}:8080/temperature`.

### 5 Build & Deploy Unity Project
1. [Open](https://docs.unity3d.com/Manual/GettingStartedOpeningProjects.html) the Unity project, i.e. this repository.
2. [Switch](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02?tabs=openxr#switching-the-build-platform) the build plattform in Unity
3. [Build](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02?tabs=openxr#1-build-the-unity-project) the Unity Project (only execute the first step)
4. [Deploy](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/using-visual-studio?tabs=hl2#deploying-a-hololens-app-over-wi-fi) the UWP solution (over WIFI)

### 5 Modify endpoint
In the office and the shopfloor the Hololens has a different IP address. Therefore, it is necessary to build and deploy two Unity applications: One to use in the shopfloor and another one to use in the office. Before the application for the shopfloor environment has been deployed. Now we need to do the same for the office environment.
1. In file *Assets/Scripts/HTTPListener.cs* add `//` in line 33 (before the variable) and remove `//` in line 34. This is necessary as the Hololens has a different IP address in the office environment, thus requiring the HTTPListener to run and receive requests on this address.
2. Build the Unity application (step 3 in section 5). Before pressing on Build button, do following adjustments:

SCREENSHOT ON HOW TO CHANGE THE NAME OF THE APP AND PACKAGE

3. Deploy the Visual Studio Solution to Hololens

### 5 Run the Digital Companion
Now when both Unity applications (lab & office are deployed on the Hololens) we can run everything together. To do so, follow the steps below.
1. Start up the Hololens and sign in.
2. Start up the [backend](https://github.com/Interactions-HSG/21-MT-JanickSpirig-DC-Holo). Make sure that in the [config file](https://github.com/Interactions-HSG/21-MT-JanickSpirig-DC-Holo/blob/master/modules/YoloModule/app/config.yml) the value for `VIDEO_SOURCE` is correct (depends on the Hololens IP address, i.e. whether you are in the lab or the office).
3. Under *All Apps* start either the `DC lab` or `DC office` application.
4. Explore and get inspired by --> link to youtube videos!

