####################################################################
# Automatically-generated file. Do not edit!                       #
# Makefile Version 10                                              #
####################################################################

BASE_SDK_PATH = C:/Users/Federico/SimplicityStudio/SDKs/gecko_sdk1
UNAME:=$(shell uname -s | sed -e 's/^\(CYGWIN\).*/\1/' | sed -e 's/^\(MINGW\).*/\1/')
ifeq ($(UNAME),MINGW)
# Translate "C:/super" into "/C/super" for MinGW make.
SDK_PATH := /$(shell echo $(BASE_SDK_PATH) | sed s/://)
endif
SDK_PATH ?= $(BASE_SDK_PATH)
COPIED_SDK_PATH ?= gecko_sdk_4.0.2

# This uses the explicit build rules below
PROJECT_SOURCE_FILES =

C_SOURCE_FILES   += $(filter %.c, $(PROJECT_SOURCE_FILES))
CXX_SOURCE_FILES += $(filter %.cpp, $(PROJECT_SOURCE_FILES))
CXX_SOURCE_FILES += $(filter %.cc, $(PROJECT_SOURCE_FILES))
ASM_SOURCE_FILES += $(filter %.s, $(PROJECT_SOURCE_FILES))
ASM_SOURCE_FILES += $(filter %.S, $(PROJECT_SOURCE_FILES))
LIB_FILES        += $(filter %.a, $(PROJECT_SOURCE_FILES))

C_DEFS += \
 '-DEFR32BG22C224F512IM40=1' \
 '-DSL_BOARD_NAME="BRD4184A"' \
 '-DSL_BOARD_REV="A02"' \
 '-DSL_COMPONENT_CATALOG_PRESENT=1' \
 '-DMBEDTLS_CONFIG_FILE=<mbedtls_config.h>' \
 '-DMBEDTLS_PSA_CRYPTO_CONFIG_FILE=<psa_crypto_config.h>' \
 '-DSL_RAIL_LIB_MULTIPROTOCOL_SUPPORT=0' \
 '-DSL_RAIL_UTIL_PA_CONFIG_HEADER=<sl_rail_util_pa_config.h>' \
 '-DSLI_RADIOAES_REQUIRES_MASKING=1'

ASM_DEFS += \
 '-DEFR32BG22C224F512IM40=1' \
 '-DSL_BOARD_NAME="BRD4184A"' \
 '-DSL_BOARD_REV="A02"' \
 '-DSL_COMPONENT_CATALOG_PRESENT=1' \
 '-DMBEDTLS_CONFIG_FILE=<mbedtls_config.h>' \
 '-DMBEDTLS_PSA_CRYPTO_CONFIG_FILE=<psa_crypto_config.h>' \
 '-DSL_RAIL_LIB_MULTIPROTOCOL_SUPPORT=0' \
 '-DSL_RAIL_UTIL_PA_CONFIG_HEADER=<sl_rail_util_pa_config.h>' \
 '-DSLI_RADIOAES_REQUIRES_MASKING=1'

INCLUDES += \
 -Iconfig \
 -Iautogen \
 -I. \
 -I$(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Include \
 -I$(COPIED_SDK_PATH)/app/common/util/app_assert \
 -I$(COPIED_SDK_PATH)/app/common/util/app_log \
 -I$(COPIED_SDK_PATH)/platform/common/inc \
 -I$(COPIED_SDK_PATH)/protocol/bluetooth/inc \
 -I$(COPIED_SDK_PATH)/hardware/board/inc \
 -I$(COPIED_SDK_PATH)/platform/bootloader \
 -I$(COPIED_SDK_PATH)/platform/bootloader/api \
 -I$(COPIED_SDK_PATH)/platform/driver/button/inc \
 -I$(COPIED_SDK_PATH)/platform/CMSIS/Include \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/include \
 -I$(COPIED_SDK_PATH)/platform/service/device_init/inc \
 -I$(COPIED_SDK_PATH)/platform/emdrv/common/inc \
 -I$(COPIED_SDK_PATH)/platform/emlib/inc \
 -I$(COPIED_SDK_PATH)/platform/emlib/host/inc \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv \
 -I$(COPIED_SDK_PATH)/platform/emdrv/gpiointerrupt/inc \
 -I$(COPIED_SDK_PATH)/platform/service/hfxo_manager/inc \
 -I$(COPIED_SDK_PATH)/platform/driver/i2cspm/inc \
 -I$(COPIED_SDK_PATH)/platform/service/iostream/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/config \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/include \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/inc \
 -I$(COPIED_SDK_PATH)/platform/service/mpu/inc \
 -I$(COPIED_SDK_PATH)/hardware/driver/mx25_flash_shutdown/inc/sl_mx25_flash_shutdown_usart \
 -I$(COPIED_SDK_PATH)/platform/emdrv/nvm3/inc \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/ota_dfu \
 -I$(COPIED_SDK_PATH)/platform/service/power_manager/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/inc \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/common \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/ble \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/ieee802154 \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/zwave \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/chip/efr32/efr32xg2x \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/efr32xg22 \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_pti \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/sensor_rht \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/sensor_select \
 -I$(COPIED_SDK_PATH)/hardware/driver/si70xx/inc \
 -I$(COPIED_SDK_PATH)/util/silicon_labs/silabs_core/memory_manager \
 -I$(COPIED_SDK_PATH)/platform/common/toolchain/inc \
 -I$(COPIED_SDK_PATH)/platform/service/system/inc \
 -I$(COPIED_SDK_PATH)/platform/service/sleeptimer/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src \
 -I$(COPIED_SDK_PATH)/platform/service/udelay/inc

GROUP_START =-Wl,--start-group
GROUP_END =-Wl,--end-group

PROJECT_LIBS = \
 -lgcc \
 -lc \
 -lm \
 -lnosys \
 $(COPIED_SDK_PATH)/protocol/bluetooth/lib/EFR32BG22/GCC/binapploader.o \
 $(COPIED_SDK_PATH)/protocol/bluetooth/lib/EFR32BG22/GCC/libbluetooth.a \
 $(COPIED_SDK_PATH)/platform/emdrv/nvm3/lib/libnvm3_CM33_gcc.a \
 $(COPIED_SDK_PATH)/platform/radio/rail_lib/autogen/librail_release/librail_efr32xg22_gcc_release.a

LIBS += $(GROUP_START) $(PROJECT_LIBS) $(GROUP_END)

LIB_FILES += $(filter %.a, $(PROJECT_LIBS))

C_FLAGS += \
 -mcpu=cortex-m33 \
 -mthumb \
 -mfpu=fpv5-sp-d16 \
 -mfloat-abi=hard \
 -std=c99 \
 -Wall \
 -Wextra \
 -Os \
 -fdata-sections \
 -ffunction-sections \
 -fomit-frame-pointer \
 -fno-builtin \
 -imacros sl_gcc_preinclude.h \
 --specs=nano.specs \
 -g

CXX_FLAGS += \
 -mcpu=cortex-m33 \
 -mthumb \
 -mfpu=fpv5-sp-d16 \
 -mfloat-abi=hard \
 -std=c++11 \
 -fno-rtti \
 -fno-exceptions \
 -Wall \
 -Wextra \
 -Os \
 -fdata-sections \
 -ffunction-sections \
 -fomit-frame-pointer \
 -fno-builtin \
 -imacros sl_gcc_preinclude.h \
 --specs=nano.specs \
 -g

ASM_FLAGS += \
 -mcpu=cortex-m33 \
 -mthumb \
 -mfpu=fpv5-sp-d16 \
 -mfloat-abi=hard \
 -imacros sl_gcc_preinclude.h \
 -x assembler-with-cpp

LD_FLAGS += \
 -mcpu=cortex-m33 \
 -mthumb \
 -mfpu=fpv5-sp-d16 \
 -mfloat-abi=hard \
 -T"autogen/linkerfile.ld" \
 --specs=nano.specs \
 -Xlinker -Map=$(OUTPUT_DIR)/$(PROJECTNAME).map \
 -Wl,--gc-sections


####################################################################
# SDK Build Rules                                                  #
####################################################################
$(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.o: $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte/sl_gatt_service_cte.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.o: $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_adv.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.o: $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/gatt_service_cte_adv/sl_gatt_service_cte_silabs.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/ota_dfu/sl_ota_dfu.o: $(COPIED_SDK_PATH)/app/bluetooth/common/ota_dfu/sl_ota_dfu.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/ota_dfu/sl_ota_dfu.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/ota_dfu/sl_ota_dfu.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ota_dfu/sl_ota_dfu.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ota_dfu/sl_ota_dfu.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_rht/sl_sensor_rht.o: $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_rht/sl_sensor_rht.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_rht/sl_sensor_rht.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_rht/sl_sensor_rht.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_rht/sl_sensor_rht.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_rht/sl_sensor_rht.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_select/sl_sensor_select.o: $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_select/sl_sensor_select.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_select/sl_sensor_select.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/sensor_select/sl_sensor_select.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_select/sl_sensor_select.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/sensor_select/sl_sensor_select.o

$(OUTPUT_DIR)/sdk/app/common/util/app_log/app_log.o: $(COPIED_SDK_PATH)/app/common/util/app_log/app_log.c
	@echo 'Building $(COPIED_SDK_PATH)/app/common/util/app_log/app_log.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/common/util/app_log/app_log.c
CDEPS += $(OUTPUT_DIR)/sdk/app/common/util/app_log/app_log.d
OBJS += $(OUTPUT_DIR)/sdk/app/common/util/app_log/app_log.o

$(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_control_gpio.o: $(COPIED_SDK_PATH)/hardware/board/src/sl_board_control_gpio.c
	@echo 'Building $(COPIED_SDK_PATH)/hardware/board/src/sl_board_control_gpio.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/hardware/board/src/sl_board_control_gpio.c
CDEPS += $(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_control_gpio.d
OBJS += $(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_control_gpio.o

$(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_init.o: $(COPIED_SDK_PATH)/hardware/board/src/sl_board_init.c
	@echo 'Building $(COPIED_SDK_PATH)/hardware/board/src/sl_board_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/hardware/board/src/sl_board_init.c
CDEPS += $(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_init.d
OBJS += $(OUTPUT_DIR)/sdk/hardware/board/src/sl_board_init.o

$(OUTPUT_DIR)/sdk/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.o: $(COPIED_SDK_PATH)/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.c
	@echo 'Building $(COPIED_SDK_PATH)/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.c
CDEPS += $(OUTPUT_DIR)/sdk/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.d
OBJS += $(OUTPUT_DIR)/sdk/hardware/driver/mx25_flash_shutdown/src/sl_mx25_flash_shutdown_usart/sl_mx25_flash_shutdown.o

$(OUTPUT_DIR)/sdk/hardware/driver/si70xx/src/sl_si70xx.o: $(COPIED_SDK_PATH)/hardware/driver/si70xx/src/sl_si70xx.c
	@echo 'Building $(COPIED_SDK_PATH)/hardware/driver/si70xx/src/sl_si70xx.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/hardware/driver/si70xx/src/sl_si70xx.c
CDEPS += $(OUTPUT_DIR)/sdk/hardware/driver/si70xx/src/sl_si70xx.d
OBJS += $(OUTPUT_DIR)/sdk/hardware/driver/si70xx/src/sl_si70xx.o

$(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface.o: $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface.d
OBJS += $(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface.o

$(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface_storage.o: $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface_storage.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface_storage.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/bootloader/api/btl_interface_storage.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface_storage.d
OBJS += $(OUTPUT_DIR)/sdk/platform/bootloader/api/btl_interface_storage.o

$(OUTPUT_DIR)/sdk/platform/bootloader/app_properties/app_properties.o: $(COPIED_SDK_PATH)/platform/bootloader/app_properties/app_properties.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/bootloader/app_properties/app_properties.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/bootloader/app_properties/app_properties.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/bootloader/app_properties/app_properties.d
OBJS += $(OUTPUT_DIR)/sdk/platform/bootloader/app_properties/app_properties.o

$(OUTPUT_DIR)/sdk/platform/common/src/sl_slist.o: $(COPIED_SDK_PATH)/platform/common/src/sl_slist.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/common/src/sl_slist.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/common/src/sl_slist.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/common/src/sl_slist.d
OBJS += $(OUTPUT_DIR)/sdk/platform/common/src/sl_slist.o

$(OUTPUT_DIR)/sdk/platform/common/toolchain/src/sl_memory.o: $(COPIED_SDK_PATH)/platform/common/toolchain/src/sl_memory.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/common/toolchain/src/sl_memory.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/common/toolchain/src/sl_memory.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/common/toolchain/src/sl_memory.d
OBJS += $(OUTPUT_DIR)/sdk/platform/common/toolchain/src/sl_memory.o

$(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.o: $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.d
OBJS += $(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/GCC/startup_efr32bg22.o

$(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.o: $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.d
OBJS += $(OUTPUT_DIR)/sdk/platform/Device/SiliconLabs/EFR32BG22/Source/system_efr32bg22.o

$(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_button.o: $(COPIED_SDK_PATH)/platform/driver/button/src/sl_button.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/driver/button/src/sl_button.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/driver/button/src/sl_button.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_button.d
OBJS += $(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_button.o

$(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_simple_button.o: $(COPIED_SDK_PATH)/platform/driver/button/src/sl_simple_button.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/driver/button/src/sl_simple_button.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/driver/button/src/sl_simple_button.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_simple_button.d
OBJS += $(OUTPUT_DIR)/sdk/platform/driver/button/src/sl_simple_button.o

$(OUTPUT_DIR)/sdk/platform/driver/i2cspm/src/sl_i2cspm.o: $(COPIED_SDK_PATH)/platform/driver/i2cspm/src/sl_i2cspm.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/driver/i2cspm/src/sl_i2cspm.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/driver/i2cspm/src/sl_i2cspm.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/driver/i2cspm/src/sl_i2cspm.d
OBJS += $(OUTPUT_DIR)/sdk/platform/driver/i2cspm/src/sl_i2cspm.o

$(OUTPUT_DIR)/sdk/platform/emdrv/gpiointerrupt/src/gpiointerrupt.o: $(COPIED_SDK_PATH)/platform/emdrv/gpiointerrupt/src/gpiointerrupt.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/gpiointerrupt/src/gpiointerrupt.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/gpiointerrupt/src/gpiointerrupt.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/gpiointerrupt/src/gpiointerrupt.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/gpiointerrupt/src/gpiointerrupt.o

$(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_default_common_linker.o: $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_default_common_linker.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_default_common_linker.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_default_common_linker.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_default_common_linker.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_default_common_linker.o

$(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_hal_flash.o: $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_hal_flash.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_hal_flash.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_hal_flash.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_hal_flash.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_hal_flash.o

$(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_lock.o: $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_lock.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_lock.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/nvm3/src/nvm3_lock.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_lock.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/nvm3/src/nvm3_lock.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_assert.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_assert.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_assert.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_assert.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_assert.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_assert.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_burtc.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_burtc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_burtc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_burtc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_burtc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_burtc.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_cmu.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_cmu.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_cmu.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_cmu.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_cmu.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_cmu.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_core.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_core.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_core.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_core.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_core.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_core.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_emu.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_emu.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_emu.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_emu.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_emu.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_emu.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_i2c.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_i2c.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_i2c.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_i2c.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_i2c.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_i2c.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_msc.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_msc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_msc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_msc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_msc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_msc.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_prs.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_prs.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_prs.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_prs.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_prs.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_prs.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_rtcc.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_rtcc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_rtcc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_rtcc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_rtcc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_rtcc.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_se.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_se.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_se.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_se.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_se.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_se.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_system.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_system.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_system.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_system.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_system.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_system.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_usart.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_usart.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_usart.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_usart.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_usart.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_usart.o

$(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.o: $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.d
OBJS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_conversions_efr32.o

$(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.o: $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.d
OBJS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/pa-conversions/pa_curves_efr32.o

$(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.o: $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.d
OBJS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_pti/sl_rail_util_pti.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_dcdc_s2.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_dcdc_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_dcdc_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_dcdc_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_dcdc_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_dcdc_s2.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_emu_s2.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_emu_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_emu_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_emu_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_emu_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_emu_s2.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_hfxo_s2.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_hfxo_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_hfxo_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_hfxo_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_hfxo_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_hfxo_s2.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfrco.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfrco.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfrco.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfrco.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfrco.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfrco.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfxo_s2.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfxo_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfxo_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_lfxo_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfxo_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_lfxo_s2.o

$(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_nvic.o: $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_nvic.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_nvic.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/device_init/src/sl_device_init_nvic.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_nvic.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/device_init/src/sl_device_init_nvic.o

$(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager.o: $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager.o

$(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.o: $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/hfxo_manager/src/sl_hfxo_manager_hal_s2.o

$(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream.o: $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream.o

$(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_retarget_stdio.o: $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_retarget_stdio.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_retarget_stdio.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_retarget_stdio.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_retarget_stdio.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_retarget_stdio.o

$(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_stdlib_config.o: $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_stdlib_config.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_stdlib_config.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_stdlib_config.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_stdlib_config.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_stdlib_config.o

$(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_uart.o: $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_uart.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_uart.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_uart.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_uart.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_uart.o

$(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_usart.o: $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_usart.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_usart.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/iostream/src/sl_iostream_usart.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_usart.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/iostream/src/sl_iostream_usart.o

$(OUTPUT_DIR)/sdk/platform/service/mpu/src/sl_mpu.o: $(COPIED_SDK_PATH)/platform/service/mpu/src/sl_mpu.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/mpu/src/sl_mpu.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/mpu/src/sl_mpu.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/mpu/src/sl_mpu.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/mpu/src/sl_mpu.o

$(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager.o: $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager.o

$(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_debug.o: $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_debug.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_debug.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_debug.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_debug.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_debug.o

$(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_hal_s2.o: $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_hal_s2.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_hal_s2.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/power_manager/src/sl_power_manager_hal_s2.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_hal_s2.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/power_manager/src/sl_power_manager_hal_s2.o

$(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer.o: $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer.o

$(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.o: $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_burtc.o

$(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.o: $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_prortc.o

$(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.o: $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/sleeptimer/src/sl_sleeptimer_hal_rtcc.o

$(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_init.o: $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_init.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_init.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_init.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_init.o

$(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_process_action.o: $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_process_action.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_process_action.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/system/src/sl_system_process_action.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_process_action.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/system/src/sl_system_process_action.o

$(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay.o: $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay.o

$(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay_armv6m_gcc.o: $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay_armv6m_gcc.S
	@echo 'Building $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay_armv6m_gcc.S'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(ASMFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/service/udelay/src/sl_udelay_armv6m_gcc.S
ASMDEPS_S += $(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay_armv6m_gcc.d
OBJS += $(OUTPUT_DIR)/sdk/platform/service/udelay/src/sl_udelay_armv6m_gcc.o

$(OUTPUT_DIR)/sdk/util/silicon_labs/silabs_core/memory_manager/sl_malloc.o: $(COPIED_SDK_PATH)/util/silicon_labs/silabs_core/memory_manager/sl_malloc.c
	@echo 'Building $(COPIED_SDK_PATH)/util/silicon_labs/silabs_core/memory_manager/sl_malloc.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/silicon_labs/silabs_core/memory_manager/sl_malloc.c
CDEPS += $(OUTPUT_DIR)/sdk/util/silicon_labs/silabs_core/memory_manager/sl_malloc.d
OBJS += $(OUTPUT_DIR)/sdk/util/silicon_labs/silabs_core/memory_manager/sl_malloc.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher_wrap.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher_wrap.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher_wrap.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/cipher_wrap.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher_wrap.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/cipher_wrap.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform_util.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform_util.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform_util.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/platform_util.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform_util.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/platform_util.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_aead.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_aead.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_aead.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_aead.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_aead.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_aead.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_cipher.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_client.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_client.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_client.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_client.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_client.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_client.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_driver_wrappers.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_ecp.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_hash.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_hash.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_hash.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_hash.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_hash.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_hash.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_mac.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_mac.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_mac.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_mac.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_mac.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_mac.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_rsa.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_se.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_se.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_se.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_se.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_se.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_se.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_slot_management.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_storage.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_storage.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_storage.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/psa_crypto_storage.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_storage.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/psa_crypto_storage.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/threading.o: $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/threading.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/threading.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library/threading.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/threading.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/mbedtls/library/threading.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_attestation.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_cipher.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_entropy.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_hash.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_derivation.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_key_handling.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_signature.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/se_manager/src/sl_se_manager_util.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba414ep_config.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/ba431_config.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptodma_internal.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/cryptolib_types.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_aes.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_blk_cipher.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_dh_alg.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_curves.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecc_keygen_alg.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_ecdsa_alg.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_hash.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_math.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcmp.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_memcpy.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_primitives.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_rng.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_cryptoacc_library/src/sx_trng.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_aes.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/cryptoacc_gcm.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ccm.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_cmac.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/mbedtls_ecdsa_ecdh.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_mbedtls_support/src/sl_mbedtls.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_protocol_crypto_radioaes.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_protocol_crypto/src/sli_radioaes_management.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/cryptoacc_management.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sl_psa_its_nvm3.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_aead.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_cipher.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_hash.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_derivation.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_key_management.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_mac.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_cryptoacc_transparent_driver_signature.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_common.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_driver_init.o

$(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.o: $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.c
	@echo 'Building $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.c
CDEPS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.d
OBJS += $(OUTPUT_DIR)/sdk/util/third_party/crypto/sl_component/sl_psa_driver/src/sli_psa_trng.o

$(OUTPUT_DIR)/project/app.o: app.c
	@echo 'Building app.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ app.c
CDEPS += $(OUTPUT_DIR)/project/app.d
OBJS += $(OUTPUT_DIR)/project/app.o

$(OUTPUT_DIR)/project/autogen/gatt_db.o: autogen/gatt_db.c
	@echo 'Building autogen/gatt_db.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/gatt_db.c
CDEPS += $(OUTPUT_DIR)/project/autogen/gatt_db.d
OBJS += $(OUTPUT_DIR)/project/autogen/gatt_db.o

$(OUTPUT_DIR)/project/autogen/sl_bluetooth.o: autogen/sl_bluetooth.c
	@echo 'Building autogen/sl_bluetooth.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_bluetooth.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_bluetooth.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_bluetooth.o

$(OUTPUT_DIR)/project/autogen/sl_board_default_init.o: autogen/sl_board_default_init.c
	@echo 'Building autogen/sl_board_default_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_board_default_init.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_board_default_init.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_board_default_init.o

$(OUTPUT_DIR)/project/autogen/sl_device_init_clocks.o: autogen/sl_device_init_clocks.c
	@echo 'Building autogen/sl_device_init_clocks.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_device_init_clocks.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_device_init_clocks.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_device_init_clocks.o

$(OUTPUT_DIR)/project/autogen/sl_event_handler.o: autogen/sl_event_handler.c
	@echo 'Building autogen/sl_event_handler.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_event_handler.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_event_handler.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_event_handler.o

$(OUTPUT_DIR)/project/autogen/sl_i2cspm_init.o: autogen/sl_i2cspm_init.c
	@echo 'Building autogen/sl_i2cspm_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_i2cspm_init.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_i2cspm_init.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_i2cspm_init.o

$(OUTPUT_DIR)/project/autogen/sl_iostream_handles.o: autogen/sl_iostream_handles.c
	@echo 'Building autogen/sl_iostream_handles.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_iostream_handles.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_iostream_handles.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_iostream_handles.o

$(OUTPUT_DIR)/project/autogen/sl_iostream_init_usart_instances.o: autogen/sl_iostream_init_usart_instances.c
	@echo 'Building autogen/sl_iostream_init_usart_instances.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_iostream_init_usart_instances.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_iostream_init_usart_instances.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_iostream_init_usart_instances.o

$(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.o: autogen/sl_power_manager_handler.c
	@echo 'Building autogen/sl_power_manager_handler.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_power_manager_handler.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.o

$(OUTPUT_DIR)/project/autogen/sl_simple_button_instances.o: autogen/sl_simple_button_instances.c
	@echo 'Building autogen/sl_simple_button_instances.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_simple_button_instances.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_simple_button_instances.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_simple_button_instances.o

$(OUTPUT_DIR)/project/main.o: main.c
	@echo 'Building main.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ main.c
CDEPS += $(OUTPUT_DIR)/project/main.d
OBJS += $(OUTPUT_DIR)/project/main.o

# Automatically-generated Simplicity Studio Metadata
# Please do not edit or delete these lines!
# SIMPLICITY_STUDIO_METADATA=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END_SIMPLICITY_STUDIO_METADATA
# END OF METADATA