# BLEARVIS

‼️ WORK IN PROGRESS‼️

by
Author 1,
Author 2,
etc

> 

> Fill out the sections below with the information for your paper.

This paper has been submitted for publication in *Some Journal/Conference*.

> Brief description of what this paper is about (2-3 sentences). Include a
> figure as well with the main result of your paper.



*Caption for the example figure with the main results.*


## Abstract
(Please review & revise!)
We propose an augmented reality (AR) system that can detect and localize objects of interest. The visual object detection is maintained by a state-of-the-art and real-time approach (YOLO v4) that makes use of the front facing camera of a head-mounted display (or HMD, e.g., HoloLens 2). The localization is addressed by a transmitter (i.e., Thunderboard) that is attached to the object of interest and an antenna array (i.e., Bluetooth 5.1. receiver) that detects the angle of arrival (AOA) of the signals. The antenna array is mounted on top of the HMD. The AOA data are sent (via MQTT publish-subscribe protocol) with low latency from the antenna to a Unity app that is installed on the HMD. The system reliably localize an object even if it remains beyond the visual line of sight (BVLOS). 


## Software implementation

> Briefly describe the software that was written to produce the results of this
> paper.

All source code used to generate the results and figures in the paper are in
the `code` folder.
...
See the `README.md` files in each directory for a full description.


## Getting the code

You can download a copy of all the files in this repository by cloning the
[git](https://git-scm.com/) repository:

    git clone https://github.com/Interactions-HSG/blearvis.git

or [download a zip archive](https://github.com/Interactions-HSG/blearvis/archive/master.zip).

A copy of the repository is also archived at *insert DOI here*


## Dependencies




## Reproducing the results




## License

This repository's structure is based on this repository: [https://github.com/pinga-lab/paper-template](https://github.com/pinga-lab/paper-template)

All source code is made available under a ... license. You can freely
use and modify the code, without warranty, so long as you provide attribution
to the authors. See `LICENSE.md` for the full license text.

The manuscript text is not open source. The authors reserve the rights to the
article content, which is currently submitted for publication in the
JOURNAL NAME.
