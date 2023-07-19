# MR Object Identification and Interaction: Fusing Object Situation Information from Heterogeneous Sources

This is the accompanying repository for the publication:

Jannis Strecker, Khakim Akhunov, Federico Carbone, Kimberly García, Kenan Bektaş, Andres Gomez, Simon Mayer, and Kasim Sinan Yildirim. 2023. MR Object Identification and Interaction: Fusing Object Situation Information from Heterogeneous
Sources. Proc. ACM Interact. Mob. Wearable Ubiquitous Technol. 7, 3 (September 2023), 25 pages.

![header](./header.png)

_BLEARVIS can identify objects (left) while differentiating between visually identical devices (center), and create
interaction possibilities for users by accessing real-time data from these devices (right). The red circles indicate the positions
of the Bluetooth Low Energy (BLE) tags._

## Abstract

> The increasing number of objects in ubiquitous computing environments creates a need for effective object detection and identification mechanisms that permit users to intuitively initiate interactions with these objects. While multiple approaches to such object detection – including through visual object detection, fiducial markers, relative localization, or absolute spatial referencing – are available, each of these suffers from drawbacks that limit their applicability. In this paper, we propose ODIF, an architecture that permits the fusion of object situation information from such heterogeneous sources and that remains vertically and horizontally modular to allow extending and upgrading systems that are constructed accordingly. We furthermore present BLEARVIS, a prototype system that builds on the proposed architecture and integrates computer-vision (CV) based object detection with radio-frequency (RF) angle of arrival (AoA) estimation to identify BLE-tagged objects. In our system, the front camera of a Mixed Reality (MR) head-mounted display (HMD) provides a live image stream to a vision-based object detection module, while an antenna array that is mounted on the HMD collects AoA information from ambient devices. In this way, BLEARVIS is able to differentiate between visually identical objects in the same environment and can provide an MR overlay of information (data and controls) that relates to them. We include experimental evaluations of both, the CV-based object detection and the RF-based AoA estimation, and discuss the applicability of the combined RF and CV pipelines in different ubiquitous computing scenarios. BLEARVIS can form a starting point to spawn the integration of diverse object detection, identification, and interaction approaches that function across the electromagnetic spectrum, and beyond.

## Software implementation

> 

All source code used to run the BLEARVIS prototype are in
the `code` folder. See the `README.md` files in each directory for a full description.

## License

All source code in this repository is licensed under the Apache License 2.0 (see [LICENSE](https://github.com/Interactions-HSG/blearvis/blob/main/LICENSE)).

## Reference

```bibtex
@article{strecker2023,
author = {Strecker, Jannis and Akhunov, Kahkim and Carbone, Federico and García, Kimberly and Bekta\c{s}, Kenan and Gomez, Andres and Mayer, Simon and Y\i{}ld\i{}r\i{}m, Kas\i{}m Sinan},
title = {MR Object Identification and Interaction: Fusing Object Situation Information from Heterogeneous Sources},
year = {2023},
issue_date = {September 2023},
publisher = {ACM},
address = {New York, NY, USA},
volume = {7},
number = {3},
url = {https://doi.org/},
doi = {},
abstract = {The increasing number of objects in ubiquitous computing environments creates a need for effective object detection and identification mechanisms that permit users to intuitively initiate interactions with these objects. While multiple approaches to such object detection – including through visual object detection, fiducial markers, relative localization, or absolute spatial referencing – are available, each of these suffers from drawbacks that limit their applicability. In this paper, we propose ODIF, an architecture that permits the fusion of object situation information from such heterogeneous sources and that remains vertically and horizontally modular to allow extending and upgrading systems that are constructed accordingly. We furthermore present BLEARVIS, a prototype system that builds on the proposed architecture and integrates computer-vision (CV) based object detection with radio-frequency (RF) angle of arrival (AoA) estimation to identify BLE-tagged objects. In our system, the front camera of a Mixed Reality (MR) head-mounted display (HMD) provides a live image stream to a vision-based object detection module, while an antenna array that is mounted on the HMD collects AoA information from ambient devices. In this way, BLEARVIS is able to differentiate between visually identical objects in the same environment and can provide an MR overlay of information (data and controls) that relates to them. We include experimental evaluations of both, the CV-based object detection and the RF-based AoA estimation, and discuss the applicability of the combined RF and CV pipelines in different ubiquitous computing scenarios. This research can form a starting point to spawn the integration of diverse object detection, identification, and interaction approaches that function across the electromagnetic spectrum, and beyond.},
journal = {Proc. ACM Interact. Mob. Wearable Ubiquitous Technol.},
month = {Sep},
articleno = {},
numpages = {25},
keywords = {mixed reality, detection, identification, computer vision}
}
```
