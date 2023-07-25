# Getting Started
This code had been tested under amd64 running [Arch Linux](https://archlinux.org/) and aarch64 running [Raspberry Pi OS](https://www.raspberrypi.com/software/)

# Dependencies

## Linux
Inter Process Communication is achieved through an MQTT Broker, that allows the execution of different pieces of code in separate machines if needed.
The code uses [mosquitto](https://mosquitto.org/).

> sudo apt-get install mosquitto -y  
> sudo apt-get install libmosquittopp-dev -y

## Instal eigen v3.4.0
[Eigen](https://eigen.tuxfamily.org/index.php?title=Main_Page) is a C++ template library for linear algebra: matrices, vectors, numerical solvers, and related algorithms. It is used to apply the angle estimation algorithm in an easy and effective way.

> wget https://gitlab.com/libeigen/eigen/-/archive/3.4.0/eigen-3.4.0.zip  
> unzip eigen-3.4.0.zip  
> mkdir build_dir  
> cd build_dir   
> cmake ../  
> sudo make install  

## Compile RF receiver host controller

> cd exported_aoa_locator/app/bluetooth/example_host/aoa_locator  
> make

## Build and flash Sensor Tag firmware

> cd silabs_transmitter_makefile  
> make release  
> make flash

This requires:
- [make](https://www.gnu.org/software/make/)
- [arm-none-eabi-gcc](https://developer.arm.com/downloads/-/gnu-rm)
- [JLink Commander](https://www.segger.com/products/debug-probes/j-link/tools/j-link-software/#j-link-commander)

## Build and flash RF receiver firmware

> cd silabs_receiver_makefile  
> make release  
> make flash

This requires:
- [make](https://www.gnu.org/software/make/)
- [arm-none-eabi-gcc](https://developer.arm.com/downloads/-/gnu-rm)
- [JLink Commander](https://www.segger.com/products/debug-probes/j-link/tools/j-link-software/#j-link-commander)

## Sensor Data Receiver
Install python dependencies

> pip install -r sensor_data_receiver/requirements.txt

# Starting the environment

## MQTT Broker

> mosquitto

## AoA Locator
This piece of code initializes the Antenna Array through its serial interface and forwards its data to MQTT. Make sure to replace \<tty\> with the actual serial interface used.

> ./exported_aoa_locator/app/bluetooth/example_host/aoa_locator/exe/aoa_locator -c exported_aoa_locator/app/bluetooth/example_host/aoa_locator/config/locator_config.json -u \<tty\>

## AoA Estimator
This module analyzes the AoA Locator data and estimates the actual AoA values.

> ./aoa_estimator/build/angle_calc

## Sensor data receiver
Through this script the stream of sensor data is received and associated to the AoA data

> sudo python sensor_data_receiver/main.py
