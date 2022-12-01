# Dependencies

## Linux

> sudo apt-get install mosquitto -y  
> sudo apt-get install libmosquittopp-dev -y

## Instal eigen v3.4.0

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
- make
- arm-none-eabi-gcc
- [JLink Commander](https://www.segger.com/products/debug-probes/j-link/tools/j-link-software/#j-link-commander)

## Build and flash RF receiver firmware

> cd silabs_receiver_makefile  
> make release  
> make flash

This requires:
- make
- arm-none-eabi-gcc
- [JLink Commander](https://www.segger.com/products/debug-probes/j-link/tools/j-link-software/#j-link-commander)

## Sensor Data Receiver
Install python dependencies

> pip install -r sensor_data_receiver/requirements.txt

# Running everything

## MQTT

> mosquitto

## Locator

> ./exported_aoa_locator/app/bluetooth/example_host/aoa_locator/exe/aoa_locator -c exported_aoa_locator/app/bluetooth/example_host/aoa_locator/config/locator_config.json -u /dev/ttyACM0 

## Estimator

> ./aoa_estimator/build/angle_calc

## Sensor data receiver

> sudo python sensor_data_receiver/main.py
