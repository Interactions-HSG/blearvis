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
 '-DSL_BOARD_NAME="BRD4185A"' \
 '-DSL_BOARD_REV="A01"' \
 '-DSL_COMPONENT_CATALOG_PRESENT=1' \
 '-DMBEDTLS_CONFIG_FILE=<mbedtls_config.h>' \
 '-DSL_BT_API_FULL=1' \
 '-DMBEDTLS_PSA_CRYPTO_CONFIG_FILE=<psa_crypto_config.h>' \
 '-DSL_RAIL_LIB_MULTIPROTOCOL_SUPPORT=0' \
 '-DSL_RAIL_UTIL_PA_CONFIG_HEADER=<sl_rail_util_pa_config.h>' \
 '-DSLI_RADIOAES_REQUIRES_MASKING=1'

ASM_DEFS += \
 '-DEFR32BG22C224F512IM40=1' \
 '-DSL_BOARD_NAME="BRD4185A"' \
 '-DSL_BOARD_REV="A01"' \
 '-DSL_COMPONENT_CATALOG_PRESENT=1' \
 '-DMBEDTLS_CONFIG_FILE=<mbedtls_config.h>' \
 '-DSL_BT_API_FULL=1' \
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
 -I$(COPIED_SDK_PATH)/platform/common/inc \
 -I$(COPIED_SDK_PATH)/protocol/bluetooth/inc \
 -I$(COPIED_SDK_PATH)/hardware/board/inc \
 -I$(COPIED_SDK_PATH)/platform/bootloader \
 -I$(COPIED_SDK_PATH)/platform/bootloader/api \
 -I$(COPIED_SDK_PATH)/platform/CMSIS/Include \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_cryptoacc_library/include \
 -I$(COPIED_SDK_PATH)/platform/service/device_init/inc \
 -I$(COPIED_SDK_PATH)/platform/emdrv/dmadrv/inc \
 -I$(COPIED_SDK_PATH)/platform/emdrv/common/inc \
 -I$(COPIED_SDK_PATH)/platform/emlib/inc \
 -I$(COPIED_SDK_PATH)/platform/emlib/host/inc \
 -I$(COPIED_SDK_PATH)/platform/emdrv/gpiointerrupt/inc \
 -I$(COPIED_SDK_PATH)/platform/service/hfxo_manager/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/config \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/include \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/mbedtls/library \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_mbedtls_support/inc \
 -I$(COPIED_SDK_PATH)/platform/service/mpu/inc \
 -I$(COPIED_SDK_PATH)/hardware/driver/mx25_flash_shutdown/inc/sl_mx25_flash_shutdown_usart \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/ncp \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/ncp_evt_filter \
 -I$(COPIED_SDK_PATH)/platform/emdrv/nvm3/inc \
 -I$(COPIED_SDK_PATH)/platform/service/power_manager/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_psa_driver/inc \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/common \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/ble \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/ieee802154 \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/protocol/zwave \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/chip/efr32/efr32xg2x \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_aox \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions \
 -I$(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/pa-conversions/efr32xg22 \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/se_manager/src \
 -I$(COPIED_SDK_PATH)/util/silicon_labs/silabs_core/memory_manager \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/simple_com \
 -I$(COPIED_SDK_PATH)/app/bluetooth/common/simple_timer \
 -I$(COPIED_SDK_PATH)/platform/common/toolchain/inc \
 -I$(COPIED_SDK_PATH)/platform/service/system/inc \
 -I$(COPIED_SDK_PATH)/platform/service/sleeptimer/inc \
 -I$(COPIED_SDK_PATH)/util/third_party/crypto/sl_component/sl_protocol_crypto/src \
 -I$(COPIED_SDK_PATH)/platform/emdrv/uartdrv/inc \
 -I$(COPIED_SDK_PATH)/platform/service/udelay/inc

GROUP_START =-Wl,--start-group
GROUP_END =-Wl,--end-group

PROJECT_LIBS = \
 -lgcc \
 -lc \
 -lm \
 -lnosys \
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
$(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp/sl_ncp.o: $(COPIED_SDK_PATH)/app/bluetooth/common/ncp/sl_ncp.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/ncp/sl_ncp.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/ncp/sl_ncp.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp/sl_ncp.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp/sl_ncp.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.o: $(COPIED_SDK_PATH)/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/ncp_evt_filter/sl_ncp_evt_filter.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_com/sl_simple_com.o: $(COPIED_SDK_PATH)/app/bluetooth/common/simple_com/sl_simple_com.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/simple_com/sl_simple_com.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/simple_com/sl_simple_com.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_com/sl_simple_com.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_com/sl_simple_com.o

$(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_timer/sl_simple_timer.o: $(COPIED_SDK_PATH)/app/bluetooth/common/simple_timer/sl_simple_timer.c
	@echo 'Building $(COPIED_SDK_PATH)/app/bluetooth/common/simple_timer/sl_simple_timer.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/app/bluetooth/common/simple_timer/sl_simple_timer.c
CDEPS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_timer/sl_simple_timer.d
OBJS += $(OUTPUT_DIR)/sdk/app/bluetooth/common/simple_timer/sl_simple_timer.o

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

$(OUTPUT_DIR)/sdk/platform/emdrv/dmadrv/src/dmadrv.o: $(COPIED_SDK_PATH)/platform/emdrv/dmadrv/src/dmadrv.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/dmadrv/src/dmadrv.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/dmadrv/src/dmadrv.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/dmadrv/src/dmadrv.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/dmadrv/src/dmadrv.o

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

$(OUTPUT_DIR)/sdk/platform/emdrv/uartdrv/src/uartdrv.o: $(COPIED_SDK_PATH)/platform/emdrv/uartdrv/src/uartdrv.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emdrv/uartdrv/src/uartdrv.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emdrv/uartdrv/src/uartdrv.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emdrv/uartdrv/src/uartdrv.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emdrv/uartdrv/src/uartdrv.o

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

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_eusart.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_eusart.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_eusart.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_eusart.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_eusart.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_eusart.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_gpio.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_gpio.o

$(OUTPUT_DIR)/sdk/platform/emlib/src/em_ldma.o: $(COPIED_SDK_PATH)/platform/emlib/src/em_ldma.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/emlib/src/em_ldma.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/emlib/src/em_ldma.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_ldma.d
OBJS += $(OUTPUT_DIR)/sdk/platform/emlib/src/em_ldma.o

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

$(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.o: $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.c
	@echo 'Building $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ $(COPIED_SDK_PATH)/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.c
CDEPS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.d
OBJS += $(OUTPUT_DIR)/sdk/platform/radio/rail_lib/plugin/rail_util_aox/sl_rail_util_aox.o

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

$(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.o: autogen/sl_power_manager_handler.c
	@echo 'Building autogen/sl_power_manager_handler.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_power_manager_handler.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_power_manager_handler.o

$(OUTPUT_DIR)/project/autogen/sl_simple_com_isr.o: autogen/sl_simple_com_isr.c
	@echo 'Building autogen/sl_simple_com_isr.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_simple_com_isr.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_simple_com_isr.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_simple_com_isr.o

$(OUTPUT_DIR)/project/autogen/sl_uartdrv_init.o: autogen/sl_uartdrv_init.c
	@echo 'Building autogen/sl_uartdrv_init.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ autogen/sl_uartdrv_init.c
CDEPS += $(OUTPUT_DIR)/project/autogen/sl_uartdrv_init.d
OBJS += $(OUTPUT_DIR)/project/autogen/sl_uartdrv_init.o

$(OUTPUT_DIR)/project/main.o: main.c
	@echo 'Building main.c'
	@mkdir -p $(@D)
	$(ECHO)$(CC) $(CFLAGS) -c -o $@ main.c
CDEPS += $(OUTPUT_DIR)/project/main.d
OBJS += $(OUTPUT_DIR)/project/main.o

# Automatically-generated Simplicity Studio Metadata
# Please do not edit or delete these lines!
# SIMPLICITY_STUDIO_METADATA=eJzkvQmT5DaSJvpXZGVrz/ZQZvAOslbqMXXp2NpVjfSUpZ0dmxqjMRiICHbyeiQjM6vH5r8/gPcBkCAJ8Jjd7VFlRJDu3+e4AYf7v737/Y/f/udPHz6bT7/9+ceHn57evX/33T+8ee43LyCKncD//ss78VH48u4b4NvB2fGv8Is/P//8oH959w9/+RJ98b8Lo+BvwE6+gS/58fu32IFP3JIkfH84vL6+Pr7Kj0F0PUiCIB7+z6dfn+wb8KwHx48Ty7cBFAzfeB+n3/4a2FaS6fTvrvtNTUrsuNYpfrQD7xDHh6fkfnaCR2AHEZIAEYQgSr4+2fBf+HKO6Mu7FOA333x3CdwziL7xLQ/9bAf+xbmWv6LfHRcUv8au6QEviL6a2XOPN6jhHiFW6LH3hx/fH/70nUPiRIHt+E5wOFlJAqKvLohj0wqsw2sQPcehZYNDhtqMgA0caE/Ts54BEnLIRB9wug5EWJHluOY9gf+xgrdl0JFUkkF6b5JqXlwrvpnx7Z6cg1ffvMdWlCxkTgr1ZPBn8OLYwIQwEtO9RHawDOg+tXRgz/bZXh5rSysZqm+HJnhJTPg11LwMUKJOMsyTewdJECS3ZRDi1JHB3S5vAZTiW9elLEjQSIB4h43sHL0sAK2rqadIAys6oyeTKHAXKlaCSjLIglDWUb3AQW4ZpL16KZuJDwdaOGSv0GK6mgmQrTA0rTgGi4xBWGVkW8YuAGHieEu1aaw+MrwweE3lLNjrkFTS1Ec4C3TgJNU246/+QuPhsHLa6cbbKrONN7rJBhpNFxu2BwGdPWuZoaajiADIf/FkaNiLdXeX6GQI6nr6v3yJ5IB4oT4Qq5CmDVvnF/RevFR/M6CZrvUC7758420q7RljHC90gbnYtAKrj86Ot1V6wRttL1gth0Nr6QV4QyMBoncC58Rdool3NREgAc91TibaqIEmP92vC2DrUUkAGcbQuNHXMFmi8mGVFcC+O2R7VfidK+ueBFfg92xdlb3Zo82BQa7/0FZE0a9zsSgWD8V6sBg6UevnbiisRjJCOHi6TrYPaiZfQ8DbcDh9BHR/fP7J/BB4YeADP4m5AuuqIlvMLp4zIQ3LDfi03prJsArpBjjbDeznmHedw2skIwQviMzN8s8uiHhbr6OMFhdno3WUUQ2yZi6Bq9XIKns24pvvJJHlx5cg8hYBTKmfduNhoRpAVEo7bVjEtr1aqVYBTszdlF1tBGSu4z+DCH3z6J45YmrpGd6MXWJu0NZFgyo7N+U7+pIUEvA92lG54QX/5Aiso4lu3nwF9nNgxudnU3kUHqXG/LnxYOhaCeqkak90nvkxHVwbT3SeeXLgnCrwf4VsWg92Hv3p5z9k6a+/SFLnwa7U4B51NGOf/OXDB+xj7YqVwFK+hya4RLJ0ukoSlwrfsv6hMPIhs+ShZqxDaY1DRvYAmRywMA84KzRrAZn31zgB3pZp4xB2GRP5tqrDR99272dSzamv1gt9tiQpF1WUHE8RuHQ106yTEzkQcWJrBZahaXHqRBkxK/CNYXS1t80owzeGkXd/s8NtkyohjuB1ul+2XVQFwFGcIsvbOKkc4ThWydaLKkc4gpXt3TfNKcc3hlG6IrLsbZdVA+UIdsjTatPECoBjOEEN/iXYNq0K4xhmngXfjO3IgYXNZz+NGcEO1DE8Q9fdNrsc4AhOYOM9IxjdM4LUvWvbpEqII3hdom13iTm+MYziyN52f1giHMHqGtobL6kS4ShWzraLqgA4kpMZBhvvLBooR7C7XbbeukqEo1i9bZ3U21hOjrTt/iLHN4aRtfH5ewFwDCfbsm983BOYsSohjuDlwjnxplkVAEdyejtZ216L1EFO4IZ8bBx/45u6OLBjuILUd3/bFCuMY5htfnR2J4zO7tZHZ3f86OxZjnsK3jZNq4ZxDLPgDLa9g10iHMMq3vbUI8c3glF43nYp5fjGMIqC7XfsdZCjuG17SM7xjWNkxs7Vt9zNM6vjHMEwsrbdZ+T4xjC6bH0XqkQ4ilUM/Hjbi7AaxjHMko0f5RUAR3CKN362EI8+W4i/+pyudjDjVCAcxSq2L3xuEbCjVUAcwWv7U4wp84v79peO9ylrx+0f2005tXs9c7qfw4pUAXCYk5ffs9kinTq2QSYdZ9PtEcJBpPaHxX6N+RL3VTt0mOcFfr/vt+PbGJ/vurld00oCz1l4fpNhP0B4hwaCthk7l7D8+8LL7SbSQv8Qzth14oX7yybQEsAg0sRK7gsvHVtQSwSHoUbRqd9JELj2zXLazaDzYBx1GwIp2N6yHvi5MUomBwj10MDSKUJCzzLc9EmMId6rE/C5KkZPPK8PHUiYvhVfbhuCT4f7attmGAEnG1i2gb+Liar+0bRWXCPE95xrtMC83ZUABnuj4SH6FASJG1jwm/5huhmkpt9GzWeXtVTFpxVX59BFNb4zt0Knn/opcU3Hh2wuEP6KzJ1DB0nvMNt4Go53QWRdN4S/jqiXRz08Q62wF+22WkTIkAZLBERRAFfCS/e7mJJoIKGvSavjbiAZ0QJCK4qX3vzpbQAVoAnteDs0aogGeUQgBigEzNKXHjAEmlAYDLsfPj19fOofcUm3UJv9XYSihqHJgZlYJ3fpri6lUe46YMH0d9fpG36cwIoR3W1OUXJGwW+BGYTvWUtv6XdRFxh6wdpe7MRpMB6HV9gaSshdJBTA8wD76+OuAaGAfV36XA4H+Yo/e2vCRQHgbE+WV4VbB9EL1wvvJqz6L/qacBsgGIwKcIQfDpZRi1Y1tKVEt8GCCX+1qE1z1ocajmKlj4c2vGvRDru/SULkS6ADsVM3yYZ4024ogOkm2ZCvYQwmwdgkn54T1aEw2xvlM6V8/JelT5Eo6RTApu2j050cdFKRxAsH8sHZIt/RJGAb3TNulVMFbXz/uFVONWxTesktUiqRje8lt1pK7tRSSrukLTIqgDE786kn7mFzSFuXuI4N6wgKI7ZRDdeHRkqjm+WuVsmH6NTADbJymm+me5O+tXBkjh5iPfh4HrM3asf6tsgnSW1UI0t3M0S6sJh1X154Z+RaEt7Xad1QcelSkmHg6k8SrrSARjQLD4wQu1ieXAcaoZjZ1IaGyHXqRQNCUUM6uIaHsWag6jTJxVYJVejG0lpzeKbgNWqAbr4aRs6LlSx8itlHrQcgz46rWfM3YI28M+vgmtYiN0qoQje24m6HEQYYO4+51NWczZiTu607vGLJDxovA1D62TXhDFfr/IUQQkxRpRk3N8SkC4xnf1U33ypNIbdB4cTdhDO1NLfDpAuMXaMu854yatilvJVaQ6m/8qGtI6KoDFUmWDSVSa+zbo5KA9poTrA+odDI22RVgRvNK4v4vE1aJTaKyUXz1ZU6ojYhHCyug0rVbtc2QHmPqY5oVDluhEIbErNh5H4GrvWVzRCSyVqnGWe6iyZcIRnuirJnUychzUv9sJ62QqAFimejzS22Sm3PqeeNtUIytZIPe3EB7xy9DPhwpYmyGTSMTNCyrSLll+f6TqtUBYJbJcpVLFqDGjxR/alAMOshsTekpxgILH9NJLNP7aYsIF4QmWoeFLw4PQCL7uGgtyNFc2nIW6PVNACkjacDiVsbampaoao0yaMa04HErOb4L57MoMIgMWU24vxuQ5YbcY3ag9CklaYf1sC0JH0ZLT4urhXzyXw9ikcDCg125Ai8PuwCBbf2Wi/kNZprShe10jYQijJaHS8lzuVX0jjbEtbN/e12C6grKNTtdnXYBYpp7dZ1TkPtFj6SKvrwSZbT1ZW1EmUI5IADw259n2W9ZTDO5pLW6NVz1WnHXoPBrVsvdKzQEAqqqC3UYPBcCnfby9iIG8BbYwc3RZ7Wibr+3itp8EGUiG89mLn2IZBWHINo8dVPDWcFYNCeQbRwFIqmQXP1QzDBqsUO6Io9zxq2Is4SwBDUNBnTekAL9UMw08wi68Es1A/BRBHj10OZax8CicKKrwcy1z4EcvlT3wZK0sluG2a8Zp8ZU/WYmQvDmjBLAENQ1+42Cb0mzfx8MLZmObdZeDKK6KU7xh45H3NnLnBzFk5t3sBZqKeZAq6IEn8LGwMyjb9hLb6x1MRaA0E5a10PbgVgCOpp6WilrQY1HLgGFIF61iz+EsDgsGt5l7u/ZidVQzAEdpU4NQ2wtAFqigXOmlUgGg4Flq9w1kNJiGxBWF+tiJMYYB4Pdf0RoIODamG4Hl5SkmDswnA9mKSMl7iF4XooCYnMMCDXr6hNEDRr2fWwEhJ0YdeyK46ohHxAmLXseiDxaZgIa9kVYZYA6Nay6yEljFCTzhYi6+wE/WcLkeW4Ju7Ejs4Lq2U/JG1Z26UUDwWLwtWqwDFw5Jq+5l0WrphkyAUWGtjZWiuLOmwir7KFO9QeFnhoNKSSr+GGeJRoaKBfgJXcow2hrwOadlKL0nMGduBiW30zRL7bDcKLNxN8clULFZwOEMihjog69093CxEAoAuSqCqUNqhe2IYpKjwHDL7phvn7q/VCWy/SZ7dhjhTKoYlqXmao7mh6c8LhVpXmpiLYr/vg21V6wz6MMzcCYMYhsJ3L0vH+2l0WRJIlSzuUNA54jN1C6KuLc8rHuifBFeCnO21fpxRrBFxgxRSVvXih4CqlqVry15d1g2oVRM750KJ0GETMuG2E7v2KSQWFn7neEzTfCPAVv33HpvHGssdV7b4m5XhoADrgEGKrfC+tVbtQKlqzxpTQerADP9+/7SbZwbxR1luazrEBNrRM+x69rDyny43aJF72ldKBDHlUf9kyBBJUqcsa/xaaTMsMJJiD7aY01KaptRBSseqYY3uVlwSTutSypdmm+eFgjq6V26XWy4p11tTuF53Ma43OfUKOVdR7Du2BofRo2d7G0D5Y9eQiRQjVFUt/xONQqT80kQzfxt4O8g4Yfj7YJ/cOkiBIbv11hO4mqm/jl3ZNO8OnFhlzkIFLeoWpofJDhWG4UqAHl6oNfWAn72TBl03wkphQa4IJ2INlXHth1ZKq4ThgkVGVX/2dNYuynw1VF9V8Z0kvnUmk+rx46HIXOF7oAiSGJvhL+fB6lbbCcOggoghNVD2/WpkSGcwtRHzAMKIRlosH1meGPJhRFxV1YS4Xymc8D5Yzi6GJKvZEZdpkZNBxGQUtOiXmFdr0fEJ3k5dZTVTnK0UhFBGUOmB6j+JhMZ1QmkHLfkaLxYuzUPhTEn4cGgoCrrsZ9A0og9CvVuisjbnAQGHnDaAtQdDBzb2lnJPjOslCkZ8GwHchUTfR5UKYDjdQUvxSDPgFHQ76cBM9DTCQF/We7gNNdKKmuXdD4XL0089/yNJff8Fu3Lce/eXDB6qjt5LGMidtXeuhjbWS1wHCPrRRrb+bdrOi86sVgd5JSgAfmnlrHdUiJGa5YNYFs0Oqt4j11wQx2ALTp+FAmkSBu9w13D7obTAcbsG1dS3S+7Q4Fz1PGwddiS02QPWhxo9Mk/YszxGC0d8EvTdJzeLPmPHtnpyD18HdS1IgklbsaxMjOnNdpTqJx7y9bCvKrHfA4CjT7JAIEn4krIfneEKQIqXwL4tF20lPWRS5gMaWxSZOpTpHSq3fk5sDu4QQ0mjH/22fO0Rfw6Ttx42rCGj6HvjAx59PdZ5OxVq2baYuPlE3CjHuPWz/0KlXJ0sRFRAWC84l2nZ6flSz6SHjd6jb5YCjnbb4LmDcYW2XpSzuimMDLgXDTNDZs6oke9vniQdNzRaKyhdke6HaQEzBM34zrV3Qq4DSsTq5z6bthLeFNs1nk2vipeN4vpmWu4fupoGVjhuAInL/tV3wa+Kl5/gMvl6Bv59y7GKm5XqOrT3RrMGlY3hbKj7wbHI3YvhgHC/PSnbCq0BKyQt4treM6818ZiVWem7hMvk/mHALCRlCcNzCyPGcxNnNyNDES8cx8nfST+ZA6Vglu6GV9PDq9Ztv79q49zP+KhBmlSyCJQ+Ip9sop3XoYKZbJ4vy/mjK41nWthD2w7MJmpLpcX88j+NZ6vtjqY9mWW0K7YZlAzLNzg6UDD9GJnhLoHx0lWEnZAnIp+3d7YQyFvio/bul3N1Yka1hnrBPuTemPYE1OlzzLcB9UKzAjt6v3A3BJuaxPJd012dMt8eRH896V4NqAy/lTjQ43XdEr4Q7ap99P/RKvJT8FgrAyIYcPlwjgVl0umbbnfvhV4c8+qxkNzSbmCedl+yKaxP32DOTHVGtQaZkiYLD7anmlnjp+C0aDo4Jw754cXiO++pix/Wut1fTvuyoCVZ4R5zv7YZdgXbUGd9+2JV4R53z7YpfiL1OhOdXOzvbDccmZvrzvt0QzMGOOPPbDbVkDLcIXPdTLQuwLCInYj3PT+CcuLEZ38MwIPqbt4KooE0GmtPR7MmHM3hxbPAACQIXRFay1N03Wvu3bHDIYB/60VMdWaQCYvchU/xgue5erdBPYt55O6VHeoVxU84IbbsVt08qqNTnAagL2JpbL45eBy0Fw0KObS8T12QOvxbWMew8a5nkiUzo5WBH8Mv2CeB/t+Va2MeyCXlUW7zuoK520M72faLpi2G7Ny13meuYU62Drp7VcNKUvO3tglUNJw0r2Mz3QasGlGYmf7PEXfCqA6XjJanaXpjVoNJxU0VpL9xqUGm4VZO+zXNrQKXgltwiYJ0d/7qLouugpWB43Unff+3v+1nsCYRwvoQNToB/gXb15tQEF54YW5pdVejyNRwBMFVXkL27Oc95Asc+v/n+clws8gubUiTFiMEQrSbWnuVbV+BBBVtmSsJLWabV60lk+RByBN8vzLa5O3KYUqYiwMIYG7yePNYc424sD0mz4GC7a3MUBFgYY2sbQGNtQb8nNCjqGXw14UTBecn2ifdslS4VVgbax+AygQoLA8XO1U99WnZtmwYLuoUkUuEksem/ePK2uXeg8tt/bAZiji3Cgy1rFjdQYMnc4wSczTgJIlhPN7XSq5kVLfLgx0MfbnwlahFPHd62zrIESWgX5IpDtb7cMHsi4Klrkw1zJeGdNUZc7r69vXuAmFKmgD/LENu7QkZthN57ZPP3skBe39huZJmV3A2NzSWmYmBuoqSbd1TvbG+Z20Nw7IK2/ipAYWg3FXSlj2gN7WimG9vF6aM5ar+m/uJm15x9bCeuLtsibpZ/dp1N7TgPsa5DHs15i6vDPsIT1oG1t5H+nRAtoI7miMDHyZ6abgsx5TyuJiCINxXVoc0Vi3UaS/ME53weSPbFt4l6Eeeq5txti7bKfQiaKMfPMG9g2dRQs2l2IE+dVe+E7aggEc1Xd1Wq08vzDDH5m1p99xGtoZ28TtoH0xraaeukfdAccyW2b8mxD7Zd0PPWSfthXYc8Y520D8INvKPZbm03tI/pqDBa3SXWPkgWUGetBvdBtYV4NOPyzA+2+b3U4Q7m8avDDUY3bDMm4uXmhJunHswPTlh74jalm5F1doKtXT1sgaz8OXvAU1a+4pXNut8QqBNw0/rtNqVuqLlRF/aIDgZnqz0wJuBm0dHQZa3Lrx0MpyDrzzpWK47yQsaqjSzndSiiLDRQYc1b5xC6VnIJonVvhLYp1EFRM1j/KIFEo/fkoB0Oem3/njaJfg+epmuWHcJJxIsVQ82b4tAGNswk22cxw8Bdd/rWYdICNtw6ns3XyFo36FSnXVSYBvF75+3hr2EaxB/DIXGTbaINjIoJFCDK6y/jcFya0AbZnHzTu2+rbVeQhseJ9b2YOuNEr68SBn3WhrZHocQ13LfGVjF/3xKNJqwRLNa/8kSm0nubicRng80Ei24Mp9yxGlXSEETrbm30kMPAHMMydoNkK7sYPSwxMEexzO8+bJVdBW8MKzjb3iqjHNoYNqu7l/bQ6fMnJfFZ+yZnD52eS5okNlFsbZVNDm1Uf7DdroCuF8gdifLxbX1nmc4MD49v1Ix1U3yauEbP9bbEBYNtwlxvo4z6fc6InIKVvRv6GAVkTwbauetGqWFgjp3/bJRZDm30/GejdPr8xEh8NrkZR8A3iley2faUQxs7S90omxzaqFmq5Z8Dz3S8cFs7jmSIY+fgW2UVE9Nz9czBN0omHt0ntLcntkqsC3PKLspW2fUHuCDO/VxnwxtfFbouJ1p3sL6E9NS+Gx0Trp2UtTBakeyi+Eyff9WKnG10qB0KOTAaDrEvbpNDDoySw2vkJNvoWXBESnQUbE5WDDRlk1QqaDQ8nKt/97bJo4RGw+PuuGc427+s67BI5NKARxO8B/7juhvtuerg6CLmb5OGTVu77JsF/ycJ26RRA0fNJQzcdW/p9bMp4FHx2cgFYTKj0deBN7T72mEz5rqvvZWNhw6Lnm2HrhNpaCUP0uPbNpk04FEnHEPRtzbKpw6Phk8SpXm1t8mmBo6CS5m7fnNESmRULLa5ZjxTrxnPt23OWHJcFAzSVFZbpFAAo+Ow0U6qREbF4m8h1LtRHiU2KibbOInrsqDNqryF8BREFqOCUawft5fIYyBYb53FdaMrwyv1yvD2fL5skkIBjIYDSqm22SlUAx0FmzJx5XZXhF2INLy24V/U4UJ0LergVzdKQKVlALwg+mqe7pcLCuPgusE2F7cEnBQMfZCYcWA/g414P7SZtfDRMHLixHx+3SabChsFk8DZZgeQ46JgEIJtjvY5LhoGz9sk8EyN345FaaMcCmiUPLY5mpTIaFgU9/I3SaQGbgQXM3G8ba51OwjHsFo9ktYgqxEBtNDhiigLG21ANXA0XNJAf8C0bBuC3CajDkQaXrG14UpXA0fBJXLgCHsWtW0enzbQ0bDZ6J5kj3NmO/TAzdqmN1EBjI6DpGpbZZFDo+OhbnRGVkGj4RFvs6vKcdExMG3Lvm1z9tJAR8smPaqP706y0QM5HEhabkHw7Gy4qEp4lHwSB+1qbJZPBY+CTxUybYt0Guho2DjeZqmU0Ch4QM3x2kFySURq2CiYvKmCsUkaBTBKDqYdbXPQrIOj57LN3qsOjppLvM2j1Tq4eUlmyIlxm75VKZ5N2QLlg61g0fiHZVdskAd24vhZdNV4c1OiihYB6XimYQSHhwTC2zzTJlJ6ppnv41bZVejGMNrcoXWD0Rgf5sbt/w2FYsUQm55euXrVT8BbEu+gf+lHPJ359vubfsT0zOHL0bY2AWskS3D0fDZ5EFWjNPIsqrzgnRf4Vmk1AI7g5fx9u02sBDeCTxLd7c2O4hU6ekbrZxfqITQmnVD+yovl3rdLqELHLfsB5kvcV61MK47rwKHGRMxbi63uk8g6KAZUZ1XWvrqf+XaRM9a3Ewd5mf/XYoEP6qwPNWKHJvJDAxom6gGexmJVcDwNTP2bXJdaX7Q/1m3jWQ6fNMiF4ANOqxXyCUqayyXq5FH+uVysTjsC6Oj+5Jro2/gRiueAAKuFCk/MwyA4JVg0aCPdA6bjXYXH0L9ygNLRMIBD5I5DpMIhccchDeJ49M7cEKSyS93ZvsA9SnPaFSB+BBfr7qLmAiUBt/HNKbCi84d0f8Q5wZ42+Qp/PkVnRdRV670Au2HhG/RREMT84+MPgghfRHOC9nuefX+04OIAXCJZejxdJSn7E/1lS5JyUUXJ8RQBvp4EgWvfYMfWlgHXwI8Z5cc4frxEkAKyxWMYBX8DcPqXhmf0g8/w/Q/o/QwUlAiHjT5Z5+fHOLFs+N97CKL36QDzaIqyKhqKKgtabQD/7gxiO3JCZMO/fHeofypGgYad02+/O+QI00/vvn339PHT779+/PDx8z+bT5///PHjb+an337889efnt69f/cv/4aqhhe8AFh47y+WG4Nvs8gZieP/9JZOrOBM5f2//Gv19VNwj+zsW9gD35PgCvzDox3ZxWaQjfLJoRe84Hx34fzl/Zd33+Wg3n/6lH75zZvn+vH7/Nvvv3z58u6WJOH7w+H19bWwFTTbIY4Pv+cWB+ls6AuqLBmS9DU4Gc++dM7p57v9mOl9jEFyDx/vdvE+qQI/Xm07lRCevYbIv3yBJkztnM4x0XlUDOsbaiCZ7sf/iv57yJ8rLV+Q/MuXd5XZoBmQ3H//drbJswHx23L4+5YwKGG+j7PH2/MbKOhwglNm2BiS2yHb8zr4dojmMr5dqBv71jhdJnhJEMokm0I1vxmJoF8WQ1y1/UFakTGKAAjMtG67ZvVpDEWijPE4kOtrVJOSfp6ABS+HjCd/O51Zw8+mFccgSmp/jn8XKh98/QZHmVcLTtrT8aZIS5p+SDcBo8Cd8CYcOSkVZsn6mgrNa+gEBJP3vZ5qHXgt2006eG+Sal5cK76Z8e2enINXv2CA+cm8x1ZmT8yPQzx7NOboR2vEcyy2AA8fPj19fDp8zLcB4Lift0ozsU5utg0w9nU/TvJtngmve1ZyG/eW7cVOtusOe2lSq+l9Fw5hU16reVmMeBUOw6btyfK417zwbkLzvOhDr/0IXhw433zK1tu/ouX2Tz//IUt//UWSSmnlfM60hot4rMTroDHHSvTub8QRcarQ0/3CGucJziRJo8gMmQlrnLZ3Zy0x3Tm0hpvRSLln+8xcJHw8DxvHUqpnmcUKIxjsg8YKD13SuDpVJGBeBUA6CDEWeolYF/8FDqOsC/8a2sxxprMa9iLNMGBeSLcLe5PeLm+sRToS6zJyLOa9k1P6pDMU6sLeiYPIt5PFuqcrxMK+9OL4zKcmLuhbWE2WyqH+u+zrv2c57il4Yy01OAPWcx4vZt2swjNrjCiPPIe6FEas6zyUaMbO1bdc1pIji3UxRRf2A2l0iYEfs+5S4aScNdCY+ZQs/uoPr6VHy4zty5WxUB4t6c6jW+Yxx309B4ztWT+kYSPZS5dNNptWBKtQAiWWcGcJzTb3D798+HCIE1g497AmeGDri0ZwB+yAzFMQJG5gnUF0sEIHbbBC4en5EhoyQhAlzvC0piXklLhmGgXOhoPthHcdPwHRxbLBSPDNd2foRa5H8XAL7xVRy/Y6W8Z4HBGIQWLSbBw0Xg5rhd76OEQk36bP95mtJPCcwRGn+Q7Io/3TvxG7TjzYvbVeSazkPlihi9OOqKaGjn95wlwovNo2tCLI/dYoFXekZF5H896GleJKsf3bEVJs5GcQBswAvHP0Urc5oOkEsrfgYgr9g97K/hz1GoKZv0aFEW0vpE0tuodJqrTxDZ3uphAEoSmECon/4skpAPQHnd7GK2j1iRwrprx6swa36givZec2U152A/t5xHuuc0L/l7764ZMsp8cfFvXrqFDqVirOi1zHfyYefvYKqshPeDklT/XeHU4PivaQ/01nteJFpLN4cVAjsnLaXr3+M1X8K1Tb/q03BnvhxvP2zRk8Umm+MLw+aT9fu7JF/1qfTwDplYiiR6y9QLH73Xyeat6PeWWKAWh2fhsv0GzvNV6g2F9pPz+FCMVeRuP5yPIud38cNJp1eOOF4Z2A5uPp9H/UK+PrCuU5cvYO6oSqPoWqG8pfyfqUMW+gJj/qedQQx7wARirIG+KYV3rcQ/AvpM1pzAuoOY15HrWLMc+ndXzMC/G4Qsjr+JhXqIohss5OAP/ruCZ6tXB2TNM1OmhJ5QIrBuXndIn9lh5UwSl+/uvQvKSlAw1q2T7IoRD3lv2KfjHjENjOZXgd1Zaazb7R5+lv5i02W8ibaBI/2Dv2CLsAuOyKZonwLoP9Ws/b5a2nEe+H7v0K1z+h9WAHft7pxWVBSWhRlD6LXNPM0DLte/TCSAmSVn3M6trI+jtCMCvEiH9maQ6CWVuhLnMSzqrsreCtWRngF9NwDsgciTMKksAO3MMJ+e2jb+EfU2U4AABdkERVyX6sPk+V+PdX6yXHlf45JAd2R+n+Z7a1mzpEFhscta9MG62wBlshpTAahx5KURSTeEpJNM4GlKKoDh2oZTGD5b8MD3o4UflWVbsEzXiw26CUBguRnTBUjuykZUXJShZLZGlpUopKbVJcrsxrRv072lqBkePMF5STa8iZQgwjJ91Vojc5RlyTX3aM4A9v7xUSvfBe7heH1F0VeqvYIA4HF0fFW2Hwml7JaRRz40ta/cOSzDJ13VR5DgtouZGakiaZCycpJ8lO3rjaiBPYspqZh8GmtV7sAhBm90zK057im8kynHlCyvOgUgalffpkpKam2uuglganWCzF0SzoicKcljRqs6er/LLss2Ptnqs3dAKgZez0squdUOxgtUQV1qlhobUKTkALC6Wo+xm41teCVvaJlkf+bg4jf3ec3sa76Q0PzUsPZJ7wYqolSHGTrLhLdbVChwSc/FZiTn8t3xTOb+pOEeK6tRhoY19OLwLPfr+vCfS93bsB0vfiwG4v6VUHvXu1kuR8Qidv9K+jNWLl1oIcZeA35c+EDbbpoUfYCcMzHBvjk3zBabSkyCGds4wWFfsiQ1GvkUOcCIyVd7JioJH2IEYLc65kJ5TRwu6Oe+7zwRmdTNrygOsyK1TbZsXUvlnwfxLJeW+aOJQIhplAMNTzjhaZhq1nJcwjuibPSwfPRGA9HzsLgbWU6AzE9a3uJiYIZyHpxqptFfmy2YhiVoq1zNFMpJF8O2ZkUWYhrchlzEDWlVl3W6T2ZSGqnlyXgbz8X7Z9bZZilokglZUkfIpVBpJbKU5ZSKzSjDKQlqf7ZCApJDqqTMt/yURQkYSSkTBW9a1YjjMWV+ZCZCmzyAzHQmYtGSALcZ1MfCyE1pLhMRDXSEfHQh6zIb/IzsZGVJ4ijY0wlVmjzfOFsZFk9t2OZpEsi5XYMlsVI4FVuigGAhsJm1jIK7MmMRDWvzs2NYEQI1FFDh924liVaT2TzQxxzWQwTAQR0q+wld1MPMBGdq/H86xkH2zkdXJsMBaLzWbBRweP4ivTNrARx2jCSMpWwEhkkSyAkbgyVD8beX1HJrPi1E+Ql3odR18PJ9/07lM79UJIvh/KoJ2XErPt0J7zFGoh5mtkkYKQTpE0k9ic7qoQAuzQdPwXK3aIXo7UorL9LROujObWAu/MwkDhMxMpRXc1r9yba9CZsuC6Lp9bsBJkWnAiy1jaTLtX0pg04a48dvhcB1Z+hvjIt/AmSMunCKgphIB4YYaBYGaIATHU80RhzJDdyFdrp0pjho1RN16XOHmjFyMMHSQys9z0U0mMsMjyz4FnonjRDIXGpKtvE4UxQ0a80zZNFjtcbpDkvise0+60LZgd4t5AKjMEzkOItrkYdQbpjpkbi7L5DL7O7Qyq3bJxBku3AeHS2YeFd4BrvZZLdfXNSHzUck2UagGFZxm/nUavY4YnyAglU2Y+I8TzRd8fynK+/Gkn9PTyJ4z39MKLSxVTWiq9FigcFkN6WMWzMSA1Nzgsu+O3w+mVpLEc0d1fbhqmbNPQS59wtNcv3MHVJ0YaCgfzqrNmMwh05DY6a146Jq1I6cUXHREv+ROWMfTCW10ETzVlF8FLSdVF8NIwYTuoX3ijEQcx4wbckW6erAhOq5OZetwq8YFZzBeLjeqTpYgiYDG5GNIiL6FEAeESao5LKNEXUCKLvJUUCW/QSRd8GMVh4Kcr/QUluWAywA6rgt/23HJhqWX+bKdPT/zWk2GHifiT+8xigUStZdqB8yhlnBsO1DDFyX2cgptpuXw1EIO+sREfna7zl34DSgD8ujfIDzMtcN51BT7vMkkvBHBXgnzouRpsILgUEx28q9bt1STHqWeioSdRHRv5wLO9sadAozXM3Dga0DDZsWeUlmjmdsuA+ISv/Ahc2VsHrXta0/Y5y7UeFdVEl4cCzMSTn5pqOshDRz4V5CS6Ng3kpCGf0nCSXpsKcNRQmwZw01JMATgpmL0L1iM7HdJ4yc6GM47SZ2499kivDWOcNEQztwN7RCfzZRe3EON7iPIH5neY838estBkD+gSkAui+UcsQ/pi9yF778FyXZ5q0fkC7LJhax57/E4j2bY9XpLRJVQ+oq/cQKPbUPxES6rGT7gqSryEl19zEF56NnAAX02qUI80f97TLx9dBWcvv/jO5it9vIvZGPHZfCQNesBeSa16zhNehC0yi9+LiHfN72dWUkotZhpgd36dJakrxE93G5ujaaYJYWXKPGizkaZsgrw0oPseU6JGUIgtfU+S6B4n4DzRkW1AEyqHykxJZPkxfB0+a6JkFyyObagVsjjqwCirvmJzNlBT0ezm2bWXpob8GyeJzTRjFGPhpOLIbTbhDsNclUw2CsYqZbBwHauSoVPHNOX8auyA8vmTirEa2TidYPV2OxieKnrikM5UUFuJe5ZTsECTcA88eufaJ9PxrsJj6F9b34mY76Tsu39FUoPz3QVf3r3/8u47OCf4G7CT958+pV9+8+a5fvw+//b7L1++vLslSfj+cHh9fX3M4jE+QhKHOD78nj30CNCVHvTkN/k96/Q1OFpmXzrn9PPdfsz0PsYguYePpSWe0o95qMcI2CCvmc/g4sCnr7adignPXkPuX758ib588b/55rt0jzxNMPFNiPzjogzA439F/z3kz313aDH9S2qfHDG0BZL779/+GzKZF7wAaOb3F8uNQfXQT2/pXnwMf/mXf62+zjLApt9+eVdkr/nj80/mh4JhMZhWqW1QCryUnXtu/FLO9rPob/nXrdcbl6l6noM1rJ5XFg3r3SeqCJ82+afuW4EFK3eR2a/WFGrPlAVsQgCWW2Yvrj2CyR/REQNekIjUGa4ci4g/dzS0DJp2SOlNRaLJ2vG4CXpjdKMG+W15phNjfs8TABJsU/0aJ5ZvZ9OtzTTNu/0ZeOhOJ/iP0izznUGUWjfPr9RwHqn9XGXexT6SZTrF/pRmvjKRwTO/EexDzfrY/K2VLbP7QKfhN3+uN1nTOr8gGnH74gLuWZoHfJAuQoafhdZzgrNjo7TyNvHxtP9AgR0itBzFP9RJJ0LxHEizSQ4+lmZsoHguTe5B9yBZYCM9BOGZPLAd6dc3Sc1Sn5rx7Z6cg1ffLBJHYl/w7bDvJ/ACl5WwlZDxNHtBwkONJEnDD2U5sHDP1DpT0hNVMHvCE0WXmtnlhSSreKz6bUPd7ocM1H+ITnczZs17VDjNhG9c441Y91/fffvu6eOn33/9+OHj5382nz7/+ePH38zff/z09O79u+/+AVrny5dv8ihX3395Jz4KX97Bb4BvB2gvHn715+efH/Qv7/4BqkV6oeJcL3zMtzzw/dQahvTkw+DXJxv+CyUVot+V2uBD8H/fXQIXLp0rlVm76jxXPA11VM/i+j34K5wjoYM6AIszKwP0ZTpnRt+kFTGzNo0CUh/FWg9NF81aZ99IxVNXazBmrYo4QrFWhJsBsdZBGPyZqumOacwtRZivsdbTO4hzLf7u/JapOuzKgzUh7CSJtRLSzJBf6ZBWFHy70zeevWlzfs5UemeFylQ6YY3KvK1gl+H8qhh2tcyzfjXXqcw7Atx6iiedG9/mQlhDMlXT3ZlhKr5nd4ipHuwe0VQN3x2y6X3769wr/XcrucGP98hBIiCM94cf3x/+9J1D4kSBDWtGcDil66SvbppxLbAOr0H0HIcWSs5GWIMUbnx1OOWaqvzmG35rJLrNz1mrpHwrmHqZ1Nilh7+U69+qLLsFPLobzKsKW+HYkwLWWnDnHEx1dM9zWFPAHpewVoI/cGGtpXMmw1cBawbkQzjWPGhOp1jrJJ5vMVXUe0bJmlL3LI6phtZxLWv07ZNCfvJrZ41TlawzIyiHy1VmBGM8FWbNClp+JMTZQeu1Ivou4XnCWz+mQ0HvO4Q3n7IUnb9CK1C8ThBSphqlFkFCkx45jJRCkPXLhw+TBOGbXgIb3j00wSWSpdNVkvLG3Z50l+0lTu5nJ3h/gBWgygOcFdOhZvNaltaMe5qsFasN30UMsMC38CkGyPIt8+ePUzSO+iTSnerzMesEJ9bF5nKx4GJLknJRRcnxFCHvuBkaMEd8IKqbVIMIVIoQJ5wpFGrYQr/ai0DP1LCF7t3fsjDR3NGXmpgSON0vixi/0MMYfGR5y6DPFbGGnyxk/FwRU/i2d18CfK6GLfTCi3oRAnVlTGmgs+IlGBR62IKHGPI84tzxV6rYUvAsKDu2IwcWcLQIk45GtoTCLAMLdxq5HqbgwTL9EeDQH4H0TH4R9KUmpgQu0SIdUa6GLfQ4shfphUpFTOFfQ3sZ25eKGMN3FjF+oYc5eBPdxF6KQamMKY3bZaEWUCpiDP9tIfRv7ME70iKNN1fDFrq1zOyz0MMWfJnGmDv8UhNTAm4efJQ3/EIPc/BvJ2uRKXNdFxcS9ewiS5Gp62RLCqQ+jotwqVSxpbDUcOZyGc7chYYzl8dw5lmOewrelsBfU8WWQnAGi2wqlorYwo8XGZRzNUyhh+dF7J6rYQs9ChbrN+u6GJNYZAzL1bCGnkW/cJeiUFfHlEpkLdKAczVsoV8W2osoFTGGHwM/XmRRUFPFlkKyzJlGoYcp+HiZDdyYwwZu/NUvXXz5gi8UMYYf55H6+eMvNDElsNjgy2fkvS+2ZrnzWbQsdn7B5/ji9RwsUvsLPSzAe7nDOUfcdRUMIHdcybghx2ni6rRG/TjFgzSPdCMDoJhsUxxOHd8e4WjauRuSBJ4zbcaRQS5CLFeCaMsJc3PBv09bQDahFGKmA4ldJ57WGzaRlHJmQEms5D5tTdTCUgoaBkPfGtq1MQkC175ZDk1VJoiII/oKTbJaFjljkk9tbrWSSBmkuRRJXZgju6A5LbvfEGYErnlo//n2KGKqtyWP7J9JZcYeIwtwV9s2wwjkNzmYguyKZl7BprfnMY2R1I/OaIVF1txCDpOua8r4fAqCxA0s9N2EMbp5aX+qQZtSJpm1otGK53foCuc5TFihM9UKp8TN8lhdLBvMN4Jz6AicOFg35JRx0FkDrAueCLR+H7hW5FP6tBZSsuQZNk0j19vBxF4XY8uGQBaFzQpYQyCTWhhaUTxxX6W3ElZymTYW5jhrgmcAjUAM0PX8ic7OGIRNiauNah8+PX18mjKgjb3M1e6BIi+PQm4m1smd2Pmk6MstA6zMqT1kKsuPE1iBorudsMPXkjkDX5E6lQGsQtRENLYXO7FZpFJngKkrcBay/MozM2A1ebNwXSce9eAwXccd57TxoMg7tifLLPDUZU3E44V3E1bMF50Bnoas1XpaOFpOvVFeC0syfVOFxV4CJj7KlOLJTXGoiSuWwngNc9fu7eicPFFPu2k1GA6NJ+RJ92GGI57xhDzNk5siRixP0BMP7oZDMfIFzd7S/svEEwdKzIX8pfZpWWxYdyIIx9MiQuBMlG+iEVQw7qk4A680sO6vOAOvqWDfa3HEXSpg3WtxtrfLx95p38IRdiF/Q0cA9SDd657q1ZHMKoS6oKIU2sLn1ppGbPOb5c6t70OYazpmQneacosUn7OGbAz6HjV7OmFt1BtmJsqnNW3hTEuWNdqu9A31Yl54X9klIbzPav/w/dIVIRO1p1aCIM+pboh9cagfjlqvLlC3GlFD161lDSiz6ltDUpnity1+7ijZDLiaBv/mjLpSwhY7gyGeAjzDQb4pOEwz/E47guvD36NnT/1Xs+KzM1Lep3XE82hXfFFXStjWTOawMfI3NJZkns/rDiK593Ut5vI062dySieuptS5NTwXF0aBnQZLtov05GzhduXvqduq23xOO8pNU7gSN6XyKUnmcLvyt9Tsy+xUKzf9Ese8plSKqVw464JnV5kqmReaEaW3GXnhbWhgDBzWSRTZkyv0Sgdj8FlUUq7YSxWz5xRNwfN6lzZqnPRdjRJV22Rkl/L+S10wwzJki7MteUPjwv0MXOvrumNChmFWQ89EFI28Eji3R8okpf49mpc6QT0xRtmSvadmnZt5TlPJLZI350rgGi1kitsV8M7RyySnqzRR5IrtLgMwqdGlpA+ZgLQyV7J2U31zyFPqboM+qrmVrA117NQXfnlZGEy+UpEZuHa7FIy+TMHfvijaanqkGN3DGX6Ts9txA8eM5tyQk7bqjuTdNO4m8ulVsGkTVBM7kjdUI1F24BUrYis5cXpRIkveNqNWIqFpZeyXPmual4pGi8GLa8U3ZmAbEucDRN7KzLAVwnbTouvFP6NBp1ZA7bgtb3b5sALFBMzkHQiclUbuN9A0MYbQKomMmhgrbIWwpZqY65zmNTEoIAX+4ZMsp6tRa54loLwDTuaGRsw8OeeKg2aOYEbHnktI+/aatN307AXm6e2usABqejVp+9lCoGu6LENnAG/G5noKOK1vdTETb+JBESh/12wcuZDpKKw4BtHUBVQNSCVnhkWCaFpgh6ZJcinTcQAWJQPmlkyekWg+kFLOdCxplpjZSAop03GkSRdm4yikTMeBonzPhpELmY4CBYyejSIXMh3F5EPiBoyxB8FdHDGDviOe2XNkLhEMcJRypmNh1H2M6D2mz0hnBFssB+NpMynENN1r9cZnA8WMYDdnWtLbBpBCyry5xXwY4y4wY1Gk0SasqXsXTTA1WbPnO7PxVHKmYzlNDEDZqrVzIp6AIrgLgxIq5cwYSyzvcvcZtOWaoOlo5gQ4aaCZH9mkmNcyKKVoTtimfGI7G8bI4AjE2fF8IKMDZ5OwMOvqOuJmzttnAxqbI5Iwb5+NY2zKNfy8fTaMkSl+sCiYVZamrHlridlgRmawIawl5nf+I5NwYNcSs1GMy2JCXEvMx1HKmbuWmA1lRF/LadcxsiDEKbuOkeW45pjDhrmeKdiiQCgmFUPK+1CQKJxMCnGzzpBSod5lWk0l4ypEzseWTdWz8KQmcqmZ1tP1QMVrmI88+RqyB1sKnY/vAqzkHrGHWJe71IkOyh4X2IE7uoG2I1q79OE7h+wLZbEwbUHtAOUd6oK5pv4g2McBAOiCJKoKMzNVIplaqxJ7wKhZw3Z/f7Ve2NWuVBpTi6USD03h20gvQzAo2mmb2+DTZDoTCwUn6u0qvU0SRy5pRNOMQ2A7l4kh4tqdNRSYpW86lJgPeFXjyn9Oa1m26lj3JLiC8fO5rv9KarcIuMCKmTTuQmRRMlKaCiNXMMm9pVX6OfVDC/thUPGm+4LQvV9H5NchSEkNcE/QpDCY1oy7F04aMied6LQ77JTqoSH3gFM0qfEO4Gcx4FDhX2mQDq0HO/DzLV/6pCS9MssWxW5gaJgrtEz7Hr2wmcnnpdO0QzlOSAey5sXGCqxFEJQKcNaLMWxtLXuQtDFocqVNl+DQUsQIfscy3OolSRvDcsiW3EsQwWnjUKG4c5gMf6vpKft/xOS16u3oGaa5RJ3w9D1NlN0q2/yat69ZyRlTt+BbxbYRonGopByaAudeh2YOryNzL167J/cOkiBIblPq2ty7mb49frHeLkooY8x4iMqw5FyUJpRxqETNrVxIzMha1YdowS1TqM4EL4kJKSUjAuz0GKImjkUp1cQdsAoYlF1dIoNi7IfMoC9rSpzg/TMJ+RTvoLnpAxwvdAFSPD+QSylqdq2sRB06gmfHEaqkzS1PIszlC3Bc9K4B24wOwdVnnTxWUVc4o4IcHZJnPNgtTj3GzZWpzu3YzmNmpYZ3zFNiXq0kOZ/QTdxRa6nqHK8o4yLQUkfmRF8PWD9OKJOeZT+jZfHFGRfelAQQJ3QWQtdlDa8hcQa2qxU6jEAVomZZih2cUtZcPLnDmnNyXCcZF0hpAF1XMoN2MDrK53ArGBviE4tuvHdIH7DRbiFYTFNcpvtQjfKcnn7/ZJZj108//yFLf/1l9OZ8R9AvHz4wOv8rTTnquK9bEGinrqR3gPgObeH/N27P3azo/GpF/Ye1mFT38LWFLzyjRoTUjo45XVA8pK8XofuasmZ0FKkcONwmUeCOvlzah60tc6OX1tp4x/SXLfpFX9kWN7d0xo56fbDohztO25bnyHmhWkR289u8SWoWbMWMb/fkHLzO2MCcEjejE9DaxEDKPKsZuRtg5E9qnpnRDxhxZcIdEhPCjxPW1sv6kEwJC7KNAp7S0nsKuMgpNLaA/y898Bs8heu8kdwc2LGG0JQ0UYy7RzHR1zChuQhBrqVoqRX4wB9/9IeRlcKxbNtMfcgi+sjM/ZJH97c9DedkKaICwmKXYESPmJ7+1QrskNE91I14wFkh7Se7eqec6JMoyeIahBpamdHJ1J09q0p6uBgpvG7G1KDCfJ2+MK+GYmak4jfTWpJLpY8lhZP7bNpOeBt3rjCbSVMtS0Lnm2m5C/YHDZUsiQCoKHcuXJJMUy1rQs/g6xX4i5dQVzVbYufYWoFTTStLOreRAXVnM7lNirfbT8KzkmVJFAqZkgCe7Y3y+JlPo1TJmkg4KlkIEyLhhHQi/UTCyPGcxFm6U26qZUko8pfttXJ9LCkkS3NIZpCY5fDf3a9x7+dpt66wxkSLNRFMOJuebtKcw6GjmuUyUZRX4yTzolRbVC9OqqmbKa3jaqSOvCjpq1HSOVGqNkCWptTQzG5/AuqHX0QmeEsgCnSZY1lmBAA895aW5YfVz2F/aaRPGitmNdXcNs1WojUx4EoPsXwja1E+lU5Om2dLs2mq5kNqgns9Y24THO9pKK4xeDXUMt3wBKf78lxKrRz2bhfnUqplSmZc0Eg2TMaFmKSiEZ2u2Q7d4mTqmjntrC/Nqama4+76GsSa6vnssC/Pq6aZKSUUbm+FCliqZUlmSig9JnSmxNqjIbRKh8ejr7u9mvZl+WZTqWV+qLM0lUIph4OdxamUajkc7qxBJhx1S4aGTO2kZWlCTdWsD3mWZpPrZH7QszSPhD2RCFwXr12Fzq0GNCxMhfEnPoFz4sZmfA/DYLIXcSeYB1qKszs5y+Q9nMGLY4MHWATABZGVjLxMRlvQLZMcMu2HfhAMd8tTNbH7kMF7sFx3Zcr9WNY+p2XqyFvZgseZd9vMxVWHSiPj/W3UW3Fyo8Rx6ShlRqfQZtujwmzMIdNSyZ6KZ43KjceES66TOZlsIQ//y8UtrI9SUzOH9nNdrsp1lK7dt46/s9RjUtgnmNa4XPVTjYmuGNXUsasVtrckhZo6dhRgF7Aoh5o+drP8myUuSaKujyUJSdUWplHTyJKIKkoLE6lpZEekmostRaShkRmR5BYB6+z41yULpaOUGZ3rst3udV63u+6CPoSzIeor7TQi2S6vnBrAwv+Aw+yqUpIvsgh6GXYdmQZe7tIEQnOcpWlKaGxsEDblMzaKyCCramLtWb51BR6EsQAtklqmpVUpSSLLjyE+qKWwJK/bRpjyo8KxHHN+1y7HcudxE3NIpwVH4i1wL3Asx5zTrspY4qw3WgYVPoOvJhy4nZdsY3YDJugiWtYai3b1ExAtZ43YufqpJ8cWDNEAw3LphoA4SWz6L568CNGOxr1v17XD58bWDHHYYiouKMCKcY8TcDbjJIhgm+CxtKuVF1rVwY+HPvXTqyKWaeoKthCtUteM5jS9GhLaI27Fxd8cRL181xL8iZHULjCKXO6+ze3aFqb8KFAswJrbvSBqxrMuB625CwXyOrrNLSizwsd+mlCKLuYITWUs5zuVZG6L3R42fJa1dQUAxSvlEdeij1VNKSdafLZk+jhx2Hypi+e98uyjxnWN2VZ0s/wz5MVhf3eIYl0zJ4Ic14h97LitBms6EMplWRUaORGykgTEyQrNraWY6SyspiaIeVyhbxPDquRJyTzBeZ0HklXINZXvfaeiZ0rH0bT5iX1TGa8p3Q1MyrMzm1NHM98567LUONzpbypYo7x4l9QZXByfx1q4j1VNKef1xaK0akp5ri8W5cT+ZmXfHH9Ral3dS6wvFqdY18x9fbEou4ZaTtQ47RT20eIQN6i7gFmUUaFxgSXTorxaijnRKw+2YGteuCp2VPNaQvELrtamR1S7t4141ywSt+VHDJvckHfaKM3IgiXI6eJaS1flf9iDgWmVLgTzdlAh8CSoZ+tB2tTN5RSNrhiZd0g48y1Ij6B+qx3T3KRZuXv+3DRC8zIH4a9WsGizOb1DEVGgIXxSmTbBhq6VXIKIyQXGNta6bIZQme3Wk/DO2pzvhrZl5NbSRjvPcaXti2SHcBrzYsXOC5PlVxtsWz4LyNk+iRkGLpMJXwdySz6LGvxsvkYWk7hDnbpbiWYA1DtzA1oTzQBoDAdCnvW2LZ8RZKhClJmtxHCgmxoYwD75pnfn0tAqySz6W2Z+Lp3+dpY3CxZm1hK4YS3Fs+i6YquYL/MYeRvSmcJldtGFjHnWHRYycH5VGauELfjcuxbVwBBETJbpPSww2tjSid0gYbwa76GD0caYTu7UzplGpYUtfDib5Aw918AWNiu/vh7ccxz5yMAZ3ZjrwT3jMhwZdhRbnGHnGhg3Tu7tklWTzP1A8pGEmWNDZzaDV8N4GsYDeFM8h3kNj5VkVwWXeQ1f6PP8eHrAB2zOp/ugB9PPouknZHw5YLSxnwLwpZBr4DAF4It7jq8NGTjPTRaCGsYEEt51PtfAfurFF3augfHUy/LPgWc6Xshly4isif0MkjP8eHLGmN4ZJF/UMYcG2l70cmbQ1cZnic2Zxrzb6T3zHNfhv89RKRkHno3vy5z0vYyOoXvKglGKvsL6RST64jPrbHxW5DDtKjt4c/nsAMe+yBVwLp8p4NfISZh2KTjUpRJm0E9WDDSFJ+5KAzvQztW/e1xBlxrYgb477hnOoi9MPKqIwBta2MXNgP+4Lt9+pK6DZdBnrphttpXEvlnwf5LAFXNNB2PgYeAyuZLTD73QwhA82+t6ZPicLuex37frQGd/+c5mvIDuQJ6xfMYDDrzQSh6kxzeusBtaGKemQfFr+IKva2EHPonSnKdcodd0MAPOLAMxCTX7ZMNnvqubM+PVzfnGdYDPxTODm+Y64Yi3kM8SMN8uo1TAEPLfQuuZ62KspoIhbKbnJl3IbNNbMryBTYTM4b41s9CORNAM4jl2MijwBHxlvIa5PZ8vPPEW8tkBRllueE8vGkqYQS9zg3Ffu3Q1sSPB1FmiA3yynwQBrMoXrcoWLvCC6Kt5ul8u6D6z6wZc11wEdczo+CAx48B+BmyPjNs0WmrYwXfixHx+5Qq9UsEMduBwbaG5eGZwQ8B1vMzFs4P7zBXtM2OwdixKfAEXGpiC5tprlwrYQS4uaPJEXdPBHLiZOB7XJVhHEXsKrAKpDFJgHj8FbZGLssC30td0sAOehmsCJkpUHXMd47ua2JGILf51p6aDGfDIgYPbWdS4nj81lLCDzneXaoYzGBYuSkrKE28hnyVgSdU4Q841sASt8p2tVBrYgY65dhy5eJZwTduyb1wH+4YSttDTg8347iR8zz1wutgSCYJnh38hlFqYgk8ctNjmDb7Swgx8FfSFI/aGEnbQHY837lIDM9AvsP0wCgNIQl1TwQz2myoYPDEX8pkCNu2I60hU18EaONe+pK6DMfCY69lUXcfaUeTnJahrO4ikvHmYDqVoq6Sz82jJ3OyRu2bi+Fn4uJjXHKTiQFDIixZcY3tO4rwsRqupkDWtzDWLM5VKCXv4vA4JG/DZezc2Lq6yD9uGYcE7A2GlwE/AWxIv1wH0K+ZNc7EOoV8xa5pQRcRl+6nGqNTBGjzPo4Yafi6nDeWlx7zAOXNo6GFOwvk792ZR6mAOPonuNu+hsVLCGj6zLAA96NmH/c8Fv1junTv6SsnuwxVTPEjzSCewuuM6cLwxXetEc9kU9z58M40xQr0w6l52zRxwpqVw7WYp8DIXnrF3jeumONR4HZrwDg0NIy8ak7CObQbjsY5oA4vVScKPpK9bTmKWMyoXoRWGhxPsDpIgSG4H8GZ5oQsOvh2aVmCZ0ERWEkSHQmrbVu3Ls+GoiHF0unOhFKpHVBd61Zga0um6ATp8Prkm+j5+PFnJVCCxHTlhcsBKHIkinmwNAoh42BJoa9sDpuNdhcfQv04FcA7sOwoXkSZrIRZORxk1OHFJcOJIcNKS4KQR4B698xKwUjUdQPks5ncruXX0hVHwN2Anh4mvXYH9DOej52dTeRQepdKT5/AjeHFscHjKBpVf0Zjy089/yNJff5Gkw8cirAUTnchc2QZFNpDBz6YVxyDCtftZpHItad5PNpLzBBm14mYn/GZF51cLDuCnAP7FEnVhjxNE7AYWmnhxkwyL02Et/cOnp49PjGshaSnRzomSfW3ZtllEm3GY4ihJwgaQNsFz2hJNx3cSHnUAeOfo5XD2LPQPN/nsW14lH5YDP8G3IOZo9mvoBGlqr+geclFTVKLb5S2o5xdbuNEUl27iexgGUZJfr+YMorUJsJC2MsvPqgbmWJW88M5jkMv2Iw/em6SaF9eKb2Z8uyfn4NUv0v1hfjLvscVsmtCctuU9FpyncRZvgpcErTIS9qNw1sn4L57Ms0KEwSuIVuxckAdxXns4sExzncH/Oi4a8PNy46ykNq1kPq0gqXIAALogiaqylMa/v1ovvOnZNyc8gEskS9l/367SG29+7v3q+NlnVIXhMm8hlaH1AAfW3O0rXkNnaWRp0T4ArNb7VJrTdKTsNFPu5vIcm2IU8BYgtgtoQZefmI9/uQqoz7VvlsNl/VGMgvHXOAEeVw0uAGFqpzUG2W4qUD6zlTtEwWkpWhgSCnCtrwQNnmVHwY/ggtbbsEv7Um4ElttfHyRJ+VkVpY+fFGGMgKdfzb/+9sMfP5r/+MOnn9Ktw/QsEv7y//x/9yD573/940dFEMQfsk+TJP/x0//GCP5BkGfI7EEr6ioPtOIkmR9++/T7b//40z9+Nj/88PmHX3/7xfz9j5+e4Ocxcj799acfP//6BIX9488ffzF//vhri7ub/Pd2BIm/jCX/2fzh94/mz3/++usUaL8//WB++OOff//8Wy/KRraHaUD/+OHjr+avH/9qfvrz188ff//jt8+/ffjtV/Ppz99//+2Pzw2NYxtCKvrPz/A/v/9Q0PgfP/3w409/dIjAzqeaS4XWVDYfoc4fP/72w09PsN79v39+hHXD/PTD0//6+I+/YATlq/ifs6OArJ9B5wLvDz++P/zpO4fEiQIbqgkOJyuBS7evLohjtKN/eA2i5zi0UIedDdsRsEHqxOVZzwAJodlGRlOsasP9lw8f0Dflz4/W+pgxi0wEGv4f+tv88EmWzattbwlqawpr3ZPgCvxsywZ+FwEXWDEoP5eTWcSj+JWCTxnuCDe+YB+lfhA3FcM+6AdwQoJ5GM2FfgvzxoE+fESnWdW3j3f7sZwvpW0xSL/ve+zRDu+NZgsnqgl4e/BkeSkElxaCS/iiPsS4rRs+6t3ASkzr5DRAoL2tiQjQ3AuWZ9QPoHjqMYxAucuJd6tAL/7qxEmpuAAJu1dUuysJ2EP/Q6V4Ch10nOedBvmUj22eUHoOPMAmeyb/5yn1K2hUj/9UTFirfgg9ibqwR3ehinO5+zb60oxB+m/cQJhEd9wGDQccZyuxmGMYUUi+5cN5EhoV1rJA4DmJeYlg722G2ZnQSkB85KJtg3DN6gAxREnirKc9v4ezQk2EHQGIrAR8ssJ0RF/HBLYZJ5Z/Tgew+sBuGAvpf3sjIPhv/00Ul8HwakW+41/jR8t1VyqGEkJ6aWJtECE4W37i2M2ZVuYItWyBRMBMw5PGa0FBT3jO31OfrgYGdDGBGkKxYoGEvEe0Vkqs6AqSNgbCY51p94MHv/l+wuR7Jo7kdvdOLST5d8sAaM/+Hzz4zff5GuDhLGqLAcGuAyAc9P0D/P77UWuCjpqqfxxEVD1K6swf4uT8/ZgevUdHGI4AhHyaSf17BmpUJ88YFm7u8XDxg4fs29VAESZlKbT6b8vWraJPNtuD5MM/pd8sby7+iCbZpzuCP/xT/t2KNuKKaoydiAPqw29M6vRYC/HGM8Y25MXxwwX99lD9tryhFgU3xmr9WxsPl+L3Va23CshR7bJ/W+Lhgh54SB94KB9YobmuA3OMJQm7Cun4Xf6wvOUWgjXGUtvb/GVo7x2QI+7Wk5/cAy38JhzhMfK2/bzd+pmo0n3qzi71A1zmAjv+Hv36mP65FJ5it9LMPpueFTaR/Z9c3pdvHj5Z4ff/6T//9ufn3//8bP748Y//cvhP//n3P377nz99+Ix8TP7LY/oyJe7M6efRgbUlP0ZpQ87vzQRhs2NLD3VPV0myJUm5qKLkeFg3nnnmcmJ7dDt4+Cf324eHq9031rPrudhApqh4gzJw90B4dmKTiL7BD6XIh1cnuT2kXf985LBAHjNXhrQ2x44sZQV1Th4zL89zmgw3PW5+vPr3x7L1oZTEbXozxNlOZN9dKzqDEPhn4Ntfp+84b4eVH8TJudNljtsrnqAeDvX4vmGOrNWLqJzKDBRS6+nsoUfU9B6D5AYiFzay8Y2QGFyj552LB+IYcnlwgX9Nbt/j+vol2izayBtjvPrzOzTfd4U7bvnNN9/9w5vnItnZZQEoXXwUUjPAehygsMDwqz8///ygf3n3D5WgYngvXZvu9qMXnO+wr49Bcg8fz+Bi3d3kCSRJejpE8gt7TB2yoBwoMQRR8vXJhv9+j3I15BOIA3+wd/v37JEZMNv2b8fOyWeopArSjG/waEd24VNpR5nmfN1Xdijwq2Kidyb3Mu0QLd0q8O7bd08fP/3+68cPHz//s/n0+c8fP/5mwing7z/98fnjT0/v3r/7tzJ8UMcuX969/wJ//wKfsF7A+SkJ7Of/bUWOBUfkGH39Hv0HPYD+3zvkzf4bnPvlH98Xf+Dnf/mP3xZ/QJM7UMf5+VcUnyFtEy05LSfD4ut/z/4DJb37MauX+8L977CMMiHI+yCGRfIv/4ZeyksfCkKl/21ZG1NPfMQwq0LpPc3shkoVQBM+HUTO1fEtN3/cv7vut2UQbPiF+G0qIQE+speh6IKiGILx79+OVd64djUHw4Nh6OJRUKUJIIg3VWcBEnVDlmVBPB7HI6oFETDdS2QHs5AcFeOowv8p84Cc7bM9zyJHWdI1VZPV8UCaN29nwZAFGRpEGI+h9OeepV6SRUM1NEEcD6AeFmAOBlFTVEkQ1bHlkF/2mUdflFRV1nVNm1AAKLJKGlw1QlebZhhAkiRVPwrKhEIojJD1EC9w1jcHiaxrR1EQDXlmdfSzTYh57VOVBU03VG1sH1qFAJpVKKoiK7piSONNUd21m2UATZA1WRLkCT1241b9vAaiwl5S1OVZNQLORB047bXN+Ks/q9MWFVU4yupRn9Bcm6PY26xBTD/KhiEI4oQGi4aOWbVCFAVBk0cXSBakZ16LFI66KgiCNFZ3eqcnX2nNQ6DJR9goRXFsm0B9Qr4kckA8b7SCUwYRDpoTOoaqRVjnFwQlnjlyyjKcRx2hTeY1B+Dd58BQJEXWROU4YSJVXROf1y0oqiLBdjEBQd0Ot5ndgiSKiqiKwpRpP/aa4qT6eVSPmngcPaPp5jaf0DkZcCoHVxpj20YaLisNUQDL43S/zuqbpSOcTir62C4Cc+d1ShWA4xOaUtJXgeJIsN4/oCPvKbUP9kvyCOZY1dM6ZkmHw+HRUCbRTifSxfiAWuIk/rKkiZJi0K8jahDg+OA62aZHGqJ8Ws3TFekoSEfq2l8A+OPzT+aHImBCPHWBfzRUQVM06jlJjX4ZrcGENrDcYGLzVwxBgygM6uGoBqHeD9tuYD/H0xqBIMIlrSqpU8wAXpAJbpZ/drPsBhN6XwOuoWRFnGKCpvop7EXFOMIFvXEczb7Z+5v511NXboIhwUFIoJ4n1ozQApJElh+n6aHnQILD4VEXYAuZgqi5mJpTPA+ioqmCrh7pV5UFjs7oNK+IJMEwtKOiCVPqaW3S5sRTK6oo6poqjh8vWp4zU6gLcL6oHOkXbzXqxXbL5EFKPMLFNJwiqfos7ej6gw2mDRWioauwdarGaAN0jnvGj9EinJ0qmtLd70zP1DG6pwWZfgruEfwRhb6ApoqSe2iWZyD4cvvy7kN6zPEnXJOhk5UvP4MziKBo9PcTqvGO7SRfn1LXofS7H/9X+lwJUESfCnjo7wxh9n4JEn0scaa/pVDRXxAt+ocEmHjgcdRlXVHFbo3iYtMsVtUOzEkASuwYdEEWFLhu6jYLtnbMQ08fsIdy+Ba9mjWLMNnw//XB7dmwgQ1eEwyxOynnbFTTInWPmzJmDSbJiKqsHyVVwqwouZvwau/ChCXMnlM2Ae2mY04/udvQu7/Z4S7MWEdKPquSFUGSDcweMG9Lnu6XXVTHGk7yWKMeVdQzdtcB/K0YWd4+zFgBJbdrVVI0UdZ5zyexdkx2Uh0roOSDBNGQj4KqLN8/2t59D1asYJI3v2VdFNDOx/I2LNKK7MKSLbDktq0LsItU9OVHGuRbtAdT1nD2jDSyvMb0G+3n+pdgF2ZsQO3b1tQk2MDV7skad2N6FkSZpZcLCDvTG7MpDnHP8CNLcPjBnJxwt2zouruwZ4WTbMWjohmKcFy+wwT7GMTB8CB+1HVJVpefToLUgW8XRqwjJe+yK7KmqYa8vCUv0S7G7gpmz6E67BINuNRefufnEkf2LsbuOlDynoWgy0dNwngN8rbjNbT3UR3rQMlzSU1WFF1ffofcRLnu9mFGZ6g2aoog6aKKuXawhBFNlOFtL5asgyXujiN7GoLG+/Sra87bZSd9ZB1oz0mNJhhHXTDWMOTbTuz4NmxGQ9dlSZIxN3p4m9GRdjHSVDB7Jj5w7qiqmr58XXSsfWz91HAS3bJVONAYygp7uo5t2TeCD+XGzFhH2nMTR5IVWcb4NfK2pHv2rD3YsYazz+VLkw1YIVex4tvJ2sXGWQtrjyshnIsbii4t30cWEJHLtuPvw82CgLnHvIqmSaJuLN97uiC9O7gLqzag9tyylY9H0ZAwXtHcjbmXSbpLMUkXVU1SVNiDLr9P7u5kju4Oz9HFo4L2gnAuuLyt6FmOewre9mDIJlRyoAtREbSjvMIBoxecwS4cWepAiT68oigYqiisUCXjXax3Kpg9N6AEEc7UNWX586/wvIuaWMEk3yGUJUNTDHn5kRrlsdzLvKeFtec2mCijcETLH+CE0S6m5RVM4i4GXDGqGpw9rmFCM3au0HR7MWULbl9oFUmSdVwMJt5GjaxdDDcVTLK3haaiu76YQBzcbXjZyaFiHSjRjgIKOaQZ+vINPLrEwI93sU/ZhNrTsNFVMHRZZHljJvtwP63hJJ7QHuWjLh4xcRR4GzHehx9VPOhHJWmSgtzyl18Wxl99UhCLjRmxBpQczUBQdMNQlTXsGNsXQhyIrRmyhpR86VmRRU2SMBEAeFtyN6saqiWNIR8NWZUwIX542/G+m73cO9Vmri7pgiHD8Xp5S+7F15TK1RRtncmiKKnLN+3XMylUzrbsWMNJrI6iLgu6JmqLDTVeHvNnwwZsQexzMBUMUcHEXONju05og82akICUvGMmSaqqqQaDWmgHnhf4B8e30whfSeA5y61QMuXoL6g/NUQLArE7U+HgehRZuOE1DQD8+3Kb1Dj6NQDE+a4iqEdFYNCMmtxjlPNiVfJ1BOT4caqmw2kqA/fqFv3ESu7LbWJi+dchEBfeoqQdVcNgV/4ouYh9sxz/EEd2lV9gsZg1lSlKJKlBosIuDTw9Qe3gEHMUWYwvHcPkdSRPvBCBK8oWs3xVadinqjc4WD1x1FFUZkXibqat2WdwTDlKKKo7g5UGyS7dJIzbsA8WV0/Md0nSZZnh4Jt3O1n3v3yvU3U0dQTEAUiRJUGQMIHhRtM/BUHiBhZk0oq93fq4mE0qQOhTE0T3m6FYYbCr0dFtKBaL94apnMMpcc00x/DFgguPtczjpF9ioPScPaiqKB51Fheb+kwCpxJBZF23ZJoWpL6UM0fdENUjg/6lZaJ6AONaNV6qH8baqBcTeRJ8PMrKUWSwmYOpRSCKgsgOFhyhiLWnDaXHr0lU4HrwyOIudW9fs7pJ2lD6LqLKRxUuFzh3NaEVxQvu3VP0NA1E5PQk6lGWRc71pez1NmSeJiTyXqdyNHRNxwSoZmGhCMQAxbFfMPoN0TQdLCSbHCVFUjSNxbnOh09PH5/KvUorQrk90ITUTNK0hYsZJcWB/qjtR5LQEB1zBdiURBZx+nBW8eMEVtnobpOi/i9qlS6anrRxqiIbEotz6a5dPGvBE3ySOWogyHVD0ARdYBEtpmkE24ud2Cwz+K5pCiwU4v6dbAhHxdAYOM7gDJKr3YA9mkjIJ7yCqMCWwmIgxtnjuqDHFdkW12F/KkPVJThfY7Ct0DIDShhke7K8rhlaKMi+9YKgogRJzDtNL7ybsM960Ve1QxsFMQeAJuuapggMeglIKD0trSWPKTYkMflkljJOjgr9WUOBPlb7kkR4PQE6BElAMzQG1YfSbovGwRxrNZrgl7CqqaqusIjYSGmyJSPhjbXYcPg7URZRnhxBYrCtSWmwRYO/jLUYRcQX0ZC0oyCpLGKVUJpsWT+4sTaj8n2TZeEoH2UWbh7URttwPaO4tfwgGnDJJeoqi21ASpv5Lwt6zIy1WQ1cz4Ew7Mp0XebU/+eHe+1RyYyXS4pDtlp19EfG13cQCidp6pHBpRxKw6EctBu2WwNejyuqYuhHXMpuXmZLU9Zu2G5NfOQIS0cJTtMUFq5PlIbLhqmtmq2OjjzzQEeuhsBgb5LaZtuubC5dZdMlTdV0JnECKA2XjlVbtVoNHHGnUzKOooDL7jvZYmnHkCfVLExW/24Nc9X1N+2FQdbjSK8bhiKxCMNOayzzZrkrNUw6mzUBkhMIC6qK0tkzHAYwpnOa0NLTQ2iuNWa6ROv1Y+wxoCHJgsziXAZrwHyR0GgOGzBbtULAICPH+kcxwVWZxY2XHmM527UWFlqPH4qiikeJScSDwl5eeC89s8P7Gv0XVNvstioc5KBX6FKuYBwZLiuRIQoX43CVLcTcEDXX4nAwW4YIZ1JwzSOyyAZYWKKRbruoHI0v16gmDQDNCoPD1rNCVFGKeJHl6cewxeB873S/btluDYREvyZBk5DjLcNpKIXt1ptbURuPbnZlQOsd2W664szntMCFkfMC9azRp5EN2A+SPLeHXf9R1FjcMMGbMB8Dmn3KFixXjQs4bD0uEIagK0wiltIbLO9MNmy2BsKeXVhB0mWDhZtrn/GcLVc3PLieGYlsGLDWsUglV9gsu8NcXuXJbjSjrZM1xoRMfXMw6EIid2CCcpQ0lvM1rHWgFBvEsWnZyPt/Y3bCgiPfSNdVFc5wBYZH4rnJitu5tdJboelVhqrd1u1A6pnQohgmMsuTIqx5WoW2LUNhwZH37Q3kyyMJLE3mAhCm8XuqG4fFN6u0vlJ7qwW2UZE9BAy4SBIFlqfdfUZKp9BpbLhNWqsNj+jFf9RRzgIWt6eorQYlogzj27VbAyDRcoouiprGInIiteWy1OzbNVwdH9GbUzI0dNrBcMu+YzenBWyV3h9vMgK0vjRrhiAaRxZXQDH2KuOdlN3s6qaqx0BpoSLHhFWPgqgyXfh0jORs00oYWD2pDw1DYOkPfD8D1/pa9FbZpzX6p0xzs29qoOlJNSPpcNXMJM5jn1VST3fNS28/PG3JQF1gPTknJA2liGNuqbwXyktshbZVmafqfRpoyP2zYEiGLmss9g+Ad45eDmfPQv+g2pP9uViDSvWjPzK9tdrSAEL2kxENVTWYLO0apkDVI0ewVN3AmCKvGQ0gPenA4JikqAaLi6CZLWoRusCiAQlKU3TCdAGKaATiUdZVFS5eGZxfZYZAiX5T94boHmbOU41vlm8tDfW1RoODRd5nUwXZ0FQWd0NxZkLVpoln8erTMVNei3CwyMcDqF0ZAouckJmZ/BdPTisR+gMl+7PublLc+HYd/3nB/ZDSVAhLrSINQuuJWwDHa0Vj1yM3zYXWfBfXim/bMFEbDvma1xH2zYrB4lQOZxZ0PW8bFqkh6dnh10VNYZEWtGYL1N/Uq+3y3U1hjryXwYAhrwkEwzBQHiY+NtmELYYzg0iyKqNQFnzqxZK7Mr11gmIPRhHFo6ApOoOFM8EUebe1FYM04JCn+7KA9vKOzGZ2TbOkfdcmLFJDQh5SBO2oGxq7+X5qDNc5of9LUXz4JMvpKt1ayyYQSf4PDhD5+ApFaJNkdqPt3YqSYomc/738cJsrRn/mI24TSs+CSNRhu2ER1aFpDtRyCgyLt5uaOfKm04TSE+NQUWQUEJ/JlB41GFQtgLfwYUmqulYZWgDICxpNU2QZVgjG7G1vOe9oDPdKPXmWdYRtQGOSU7DB3IohsyW3ATrkGwjIVxgNQdePEgvvymbJB9FysU5xRV/pJ8dt1wQdzrBZFz1Yt9KD4UqvCgJs7LrGZGVRZ55lvlmTfB0B+aBOFGXhqLBwjmrwR5tIa7Kv6Sc6o8MZkGaILFwtGtTds2etSb2mv2d3E014ZJXJUqHGHWX5XpF6pb7nEpQkHmG5q6y7eZQJeUXqlfoetzVdUkSDRWygBvNFva4w1Cncqh5E4Qh7eYnFVcEG93jVoT0eGtjhZF6XZZQtmjXv1G9yVe51BD3NXT/qcMXL4v5CwwCrj+9Uw/tRhv9PEdlsnCL26XFnsZRablGbsy/OOZsA+rIpC5oqsBvlcvb2zQnXJF/T3+OnpiJXCFlnNr0pyC8YIA/HfTAG3oNowFU8HOhYpKFqU08j6VpLHp/gLNBEQTKEAie4iq6JzMb73Az5cnpFEzQQ9DQB+YjiRyqs2/9pwVxs2L5vKNYoXNTo0tE4snANa7aA9LB71dpfR9AXuNkQmNzybNCPLO9y91cd+JoQiJ6liqwbmsJuLy83wNIRvDEGoAvdLYkSypmms4hZ3WoB0ZKuXpj6Hw26eCkoRoGqsDn7qnFfMjYuhvpw+FsUY1NUhaN2ZF3xwbIpt3Hs6XJpG4qgGYrOfNDP1G9g+oMDQl7/6NASssQifnTDGun24opGqOknczcUEbYHFmHMGtzT/cUVudf09+Vb1UVRYnG1osEdbTCuSL1S33d/QpUMQWBxP6lNfQOtv4OCuLuvqTrK/cPEf7dmBrTPuiL/Sj15CJDRzFdjPfFJd1nXnPgmg3lKRAFOelG8aMbU41VnfPFgNj3lCOd6umawXufmW6xrcq8jIKenOaKL5uxOdHL6q0/5aGZ8cLKPLnQcWdzniCyIFv7XcU1khvyGC/q8mBVSCNkfGQr0d3XRpYaFHDNekfQj7PoZVAeyQUzvslzDoDBKDU/PduBRko4s7uT22CXbmsuSo5roPtJywyWNmYjwyJ2rlO6ksVhQ9Zgt+RpuzFJ1ROR5tqpKqqGxOF3rMc4FWMk92ph9WqDI/bOgaZJwFBlsxbZMBN9LAjtwDycXZN/CP9a1UQEJ/Q3BlD9XwHpuaOqipqmyxr6hlYZyAAC6IImqkv1Yfd6O2SpM5VMdmOSIyAK04lE/sh/7Shv+/dV6yatb+ud2LJfCKR+ogyOfE6jaUZNFFouldv91c8IDuESylP337Sq9Zb+iX8w4BLZzWTA7D75Lg1DQvynC8g8EtXwSh5YYgkfXDZmFr0HLltY9CaA8dPMi/S4CLrBiUH4uUEvo9kPx62LXMrCWzSGjP1uo61/1Ae+JkKiKKpzzCwy2NtqN3L1DY2Wf7wmaLgVvKBBE44vFnF7wDT6FWH5bgEJfEID2bJTAti/JBot46JMMuW7POcKQQ7NAGc6QtSOLfCx4O4bWA1SX64/LDlVqmjS0TPsevaw9T6wM24Rd71+ljpnb4MnJV1XY/BWNW+Nv2Rrhqj5mPdZWeoCufXvQkjcN4QpPliQWVwCoLZqW9B6M2QVKvkqhqMiKLAI3Ta+Zm235PWh7YxmpinhkkadiVNXMFv97MCkBbc/1vqOROoWu1Ng3b0xKO8qiKKu6Nr2tW2FY7PGg8Q99zjfnan9yNRdUg/6ptnUQjvyHXH/z09AxhGKoEjTJ9L2wHpvAucK2zIID1DPTFnVRgM1u+t4Oss3JvYMkCJJbYSXfDpFl4D9cR9HcJKX6pn2g9twgFY6e29WabAi6NuOW6ZAlFqgcVJYYOp9HXYihz2stODuY4CUxL46boPCzKZTaN+vWkxqQylBddH3OHUdR1Y8zrnBNs9qqdWrQagMrpqNoyII6YxI1yWZL+ErPNh2lO7Usa5oqGTPClmFNGCOiAGFIYx6Xn1ZspBWI3F4dVOT4AQKcEenajCuG44y0XsXqM9LQTOCoy0dJOM64ltRnpTzoeImIf/4EKktVYcexyIg9lyyhhGnCkrZavVYRbTXkBKTI4lGY4wNVHacWBisCjZ8S82olyfmEItbxXcM1DlBrdqrFF8ehIV/6Fo6SKkgSB7sgIHFi2c9oZ+PicM7XNWgYApyexDeSrBiaMSeNS59pXHc7dmljIacYFCXFMFR1xmYo2SZXK3RWN0YNBNkKR1E0NG3OxcG+mrEFM9RR9AXeRLNncU4+7wFD5N7czslxIbNtmAWLiXj6pWlwSqPOcSEY7mD55xYb0b1SZBWD61TUs0pzYo30WWUBdzkqg9A4yRmyqOiCNOdKWp8pFrmSSWUMupuZhqodoTHm3E/pmgNtwP/08x+y9NdfJOnwy4cP6JvyZ76uICTb5JvuJSz0ASLLf2qD67nRh3bJ5BkuXTcrOr9aETicAvhXkQYl/cA/C2ahPDUPUpnWlzLnSQdFzx6EoMCJ7IxtnD47IEVR4PKPW0ZlDwwaole3Cuewx6M4vTm1zFL0LHUgXDsXrEVqHQsGCHkPRtF1HZ01cLEG91GYyhQUw690lAQRdbTzzXCOHKjl4L1JahaF2oxv9+QcvPpFA8L8lN1cIfy4WPPKoKO/MCiazY7Igfz7QLuEBSAZyB2Xbxnk9XN0GSxViwfLoKrdU8pgoDtQj7IqqccZV4HSc9jk5sB2F0IoXw929DVMAmRWtEgIfKgp/ZB+bdm2mfp2Rlm2sZOliAoIi7U3z4pfnNLWsKKPGa7cgiXi4nMbdK1RYKH3bOuiUENwljt9GJptaVncqZ3bwPsilUuqIc243zrPyNm3Z88y0/xA0La7MjURPnn/Q5MVWVuv/8i+hd/kC839WbuNnTxcHlVJmeUhMs/U8Ztp7czCDcjkOiwd1aOuzogAOtuwJ/fZtJ3wxvkojoN9O8iJAbfQ1TVBnnH8NNvK55tpufsa/Nqo+zaidcEQj8b0hd1s+wL4Ve6ivzMbd5CTD1HVI7T0jFQoTKz8DL5C3XuszVj0fcGJ4awZ5R9Y097n2NqnqZvAey4WiygK6HHGmeZsK99452TkYOAbReJGURd1Q5MNY/oZ+mzbelDy3mxbw0zey5Q0QZ6RhWm+YYFne3w9q3mYto6a7BajKaKkzUnzxMS84dc9mrdETT7jU46CoM4IvjrbumHkeE7i7HC21kFOrsSyBqcQhrLipDjydzdxqCD3pAJDmSPlFatvskPDJsOWRZvx6PrC9EPc6ZZ1fNu9n0G6KS+CJXzzmNk4h47+xKEnb6rBimygDO0rm1ves7VlOmOjMxDdMGRjRoA1NtauHdvs0d4d+ORuWhQkQ5hzPZqNwY97NveRztiiJmiKOieyFBtb63u2tU5pa0U6KiiBzRq7yTVjVweTOzR2Gzy5YosqMvYMB975xkaPOS6ITPCGYKFL+buzOZkDeQGjHGVVk+U1duZK03dPhXdneRKFnrSAimAIMy4FsbI7Oh/mfaeKn8mb6HviuEiCrovyKgvKrr35O8/ztTiN072qaQZaaq5Zw/Pz+b0ZugGbfMFDloWjqMtrzr2bB/Q7tHMHPTlukSzJiqzPuIHP2NxLhDfgbnW68AcPoqzrknA0ZtwHZGL9nU7G28j7ThM1EfbZ0prjJPJJAaf7Ls1cB0708RBVWZV0YeWOO/f72aOR68iJdVmHa3hhVug+NmbmnKuLl40HU3w9KNrxKGjHGeHW2Bg4Ol0zV4k9mrkFnmxsQxPhCLj2bK/myLZDa3fQk/f/ZENXZVlZw/mjZe6aN9tOTd5hQDwuMyRNnRO/kZHRC7+2Xdq7Cb4nfqiswxWkuHaHgvIA7bMzqSMnJmRQYZWGs+qV1y2L5O/hZGfaND9wfFQERZqRc5mJqfc6E6GZhIiaIRtHwdBW7jRur6Z92WX33EDeE3FCkFF01hmxWZnYOXWS3aGVa7jJ+3lw/NMUcRUPybqJM2fZPRq5jpzsIaLDHgNdHlrfzCHf4FYczRwOhcB60FACUkmZkWyZiZlrnrM7NHUHfc9+tKEL8D8zQkoxsTfy99yhoSvY5MARsixJkrzKTfuagZOdWjgZNrEiqzqcZihr+pPFbxG47rGvqMEmH9AakmSIS/TJ3gmcEzc243sYBlFyyA578n8ezuDFscEDpANcBIF3rLopFm8xSJ9P0Vd/9fAg7ieJiqBqxyWud/YXQew+ZO89WK67/5IYpENsE6KmG6IsLXBbsV0gRays7OvN3ezAmL0WHKsBumcHRBcNXdT5r85xxq06zS2G8SDbF4ebfAwjy4pgaAL//TyciYvvbJtvhga2Bu6i7jlSNBRDU5Tl++uGeT3L3qN9K9hE1wNdEwXVWMANss++2RkF/O/2bo0PW7kDnhwC0jAUTVfWGeqqXu26q84Ch5s8vZNgVyHBlfjiJkZRH+FwYVou33ikjIybx3hsIu7JIyopKlq5rGJWOEzszKxNxD1hUDTxKKlHffkeIbUrHB72ZtgmZPKdZgXtfS6wV4Sza3yzxJ3ZtQWZvKCQRVERVWn5BUVuWEnV9mfaJuieRC+CpgjKAjfxCcZVRWl/xm2CJl+vkmRF1cQZKcVnGbdasu/IuG3QPW4AR1UT5DkJNeZYN7lFwDo7/nVntReHm3w8fVRFUVUM/h4AOBNfdzcHu9LOweCIBqdgCzjfh3CZmMeNz/YendpXxb2XrS3MKoTpF8UOJBk6MQDVUVHSWJZr2XmT8Xt6zUsRvedBlmVD01V1ekJKNtWXe9YaXpWXJtGNrmnSUdRV/gd3LSNXmx+e5VtX4MHH9mHlHuR96R9VQT5q0uJ2RnWiQpxElh/D1+GzRS3ZZLRQYvWmpUJ0UpaOunI8Gqt0Kr3gNxqzfFpJUIUxf1BlXVI0Rd5eYVhw9vofpChqVIgTGPEoCqK2wE3YseWwxdOpacUwfGAlqoaoHmVR314xPIOvJjLvS+b48B+jRLCkyMe1giDB/mpGsjeehbO3OdQ0UuTQTkcdBQiZkeaTV+HEztVP7/T8BymXNh/i6bsm6Crszvjv6XRKJP3GSWLTf/HkvZgdB5q8mpBkQ1cUQ1x0xwFtlsGPhyLsF6we9xhqNeMkiGBD3dwOWtPU+eYZ/DL71EuCaHo4UVJ0Q1sgUj7G8uk1zF2ZuY64x1FSkw1JWnZozc4ucJt9u7BvH3SSoWVFOxq6vsCt0ZadsTsmuzBzD3LybXNNNyRFkRfvIsiD9+Xu29uM80ms3HREyEEtjnC6LmnL13Uy9m1GQhxZADThEB9kUZFUQ+Oe7wTkrbKc+VXfbGzeV+JKP5WTvg7entsdinTUDYN3p9Jj0m3uUA5alnIvEoXBMTRD4e4v3GdhgPLOby5Nz7CJm7jJG1yageK6cz9s6jPxBo87hu1Lkx3tKBjGURe4h7fuM+6m9wmHzTxuR1AXpSMKbL2yvW+Wf3adzTkA0Fm7BZ54aqcfZV0/cr+/22frre7kDRuads9O1FX4/2VhzR4ESdidgWugyWtv2D3DKryiaa0kAXGyz565i53sDiuieEI6d7ehtqmdOt4g3lxmB7yRSaiJ+/qSJGuwKi/dE3dwmie4IPVAslc7d/ATkwsoqqGj9KGLWTx3664tTjds4cqju4O3x6lbRkMd90x1PSY17RsUaW4yyPqgfXHgicaWBGhuSePuHdtr7C0mbaAwM1W2BkWC1VnQDd5u3r0G3mk9pqnBOjpqlQXugSD77HsGF8ff3O7xsIGbuMlzNkFRdUnXlltLd01c7GHtzcRN3EQTC8IRVmFNXHEascV4scP2pQkWe9Q1UVRl7v4YfcZtbV7tzcxY+GTPYd2QDUFftTY39q/2aO4W+J7LC6KG/IMXPHzqWrvaxNqbqdvIey486fr/X92ZL7dtA3H4Xfq/U9zHw3Q4NCXbrCVRQ1I5OtN3LyDL5rkg3RjQspMZx27i/PgZBHYXe1DF7noKYrykXka86lZaCSm0uSfca5xwa2x7omG02s/GVjJdeHMKtx8i3BrjqfZAO17FjDCM3HMhf2RPumNke7vFnHr4ToRJpQxjqWNF5ZziTaAOKYcz+IUg0jKboPalrtqqqN4733YVv8OvZ3XuaGFs7zgSev1qr/43/BhgghGxTGvGEwzdgn4A70pRF7ME2cNPEApFu63c7ekJskXXLXxke8wn1/vypETpe8cmaJbzmWW+LeTwE8DL3DDqFrpWX7/Mb31S/nzvt/7Reua+m8dNlv9tr7H6WBzcjkw7ZEpFqJ8a8zof8vapqu/cOHMe10gbHD9WzkATIsKdP0QLQaJKGNmapBTnEjNijInQl23MDUNtzTywVaUzjDJNlWT86yO8Y1L74uys5u95U36/c6xrnteMPvi0dQcttSTCbK8JtbdbkOxcHe7sJQHUpvoCh6UWjGkbwUiZbGev2Y86v/NMLmAjG0iDtzCqhdYk/s5/3KFFNZQWKBuW3BCiIsRAx7AaZ0Ri3sRm9MGhCaKkEiZCovccNfc55QjiajC3iUL4ls5ZZVwRGn/7fzxlxwvKjX+gDK4ZtdbZYprG38YwFHEBhti6nlHUEKWFiL+m3vS87axocfXlBVLwDOWaMpHAP2ry90gMQmYTdfBZqY1SbuviX3+lBSND0HZsiduadmIPPpRsfD+elOzwbmyQSPjOiRMmiI5QLRLgd+sk4feTsxOGHOS8WjhAJLTvfSEThNM6jc2hatFcZiwSnVcLJsVaxSmz9OuvRUNAbz16kIMcqIQAOgtGKmMjTMwMANwXKE2ZWYVBf0wopVMaMwjKxhfRrSkSp4RwImSMZPYAvLt38Vxkt6Y7p/M6qBQJYpk9XXWTIyfXKQzEnQzVXNkITZdDhwX6c2LpiHDHq6OmdXwz8FbPdbNREdTyAC4vKBNOa2GKUs5t/FB63x/HyG4iDyxVV+58NTxCV+4lzxchtnmJcByPuRdWyAgp3MueL256q2oZHxiR1Lm+lqcFWN27rGAZX7VcP+BMYsK5pEltu7EjjhvjvFq46bdkwmoVodvBgpOGm2KnMFAzpKTv6xphnuKSl4ab3apyQsm5czQi5OIG2GG+roVlBhJrGaPWmgh9B0IQW+w7YKcQtpmVNlqmuGIbeeC4yXUK4Qo+y33iZoQ56KEYQX7aVcfMk0BOcKoUbHrDjBSMsaQ3ID6UgRzgh0LYeeNU0sQOSIP90GgWk3wUI87rMAnyyAJXLsghzqoNHMBEc2rlXW6IkJNcN2uCG62dHSiTmoHFocR/VzkQGViBglsqbIRS2XeA5ak4XHb7j8/zexfP99DdtI2+mi8WymslqB8GF69UZ0KtLvGcvAC2TmLock25t1WkJNecKHZyncTADRE1hBEdYaxOiNyPumzxnBUwvr5OOHRKheJCkIhZV2OGj3mzVwI5wIHIQL6flpb4OZjp6JXPp8sRO72+SDjybK0fuBLxlm0C71Iedll5erpzvfIywLFQ2NyTimtFE1orRX7cHw7oz96RzEDM2RhpbYQJCCDAAvv72ykMdH0n1wqXdNRecveLEezohjIDk2MUkczqiAlp8wTP1eHO7SpXMuwJhes6hNbKKJvOesHTgXkFxonUgNdrFGfc8Hjh+wlJXAkIAMNVCQhUcMaIUhGTrib0MF19AOxW3H1QdW3gxG3CdVcdz3n7wL79xM5vLDSQw+E8YT/sJiFEv6v4OYDoIY6Ewq6woEYoJhNCbOtsVz+iP0eGMiGAVipiuCLxAs9jfrv94wU7vL7GQByBU3f4GvP1jexgdthjzrvlDvBCKkWEMQmX3At2361TCOebSkOp1Ol8t32xw5NhNY+tJxFML5BcO683Yl7fDDf0B2xfI7zBWe4nTTKSboPbF3+f81fsgfqhStjAk0pKn6ibEB+efFII3VI+qZBGU81EOq8MxTiQZXKrhn88eHuEMxuxP94EX11X2CMCfY3gvDtuJLcJD9hn9CHl5+WQMjOCWUF4uvP15XX3hJxbTyI8648ZqlK+py/HvNiC5zrWGWieZywhhEZs1zVmePu4jVDyrFiYplRGUkEjtiKZ0MRTBwgQXKoCfLBaciVMxG5eU2gSPTW5dGoo68y8qPXOE2z7Y1X/yh4vT09+CsfhUGGPxMOKA0VsXFKmI+bSj7Ge9m3WVMXrHlE1zDzOqdKA46ulUVqqeMUdE45l02avP7AzHKiE0wyuEz5IxKqiMb6qxH6UdArht9f6hq0pc0rPe+weSKcQ7tdNJeNE84TYXrFTe120W7ihxkZsDzxlVjSUoefWEwnGCaSmmtuIHTbn2GE3+voa4eCU5JwRmzB59GOsBHJ6Q5mw3Uy0b7ScMPHiY8hEWx6xx+XntMLXkJo5E4UkrEMYzuvYCMqeVhAl5ZRr33czHcrq8Itygn5THMoMTB7kzB0qCUMv5+v45X2WF8W+we66zYqFG8BywamfFZEOZpNv4p0eygSLAh09ZxkmjOjXpTPzd+5wQw5wrBOu71CCOAuRpLNzMDU0AOAtJhxQQakgzCasT2hecuxFlT2J8HoT3Eql0oJjUuFH14kE3TkmjOUJQ6ZOlUTvCQ9Ewi+sH7ROTMRurxN4DfZTtlMIJy1LIRl3211KbFmRFy/YPbixTjBJyL2xiimbcMPzyq51Jc2lbNHn4QJy4TdZSMKNjTi/aRZoVb2Wm1iTfaGhToiEEi4ShgavU7pKf6u1AYoDoYG6LKokETZic9MxxW5WL26IY50wQ86dHU0sS3fEtOVxAwD7IgNl0tw394s4QmYM7yYFOb2hykBnZ2ulZc6uTsbvpyQWObyexFBzHOvWno04C3yOXFbU2K3qkUx4YowxXFmasJ7tpgz7+TuSCRctEN+JTiTMqXxT1mBPIR/JDORRamqFsQl6RJyb/Pa/MLJz6ro/tnyFZJUz+eKbKx2zzPfractT5ku2qwarUzfACGsOnClMMspkgmZDM2zPtbO42vL7lthONAe8Zt/4NOLg0Bmybw0G8NMc6ATvmgQhWqbIAx4QxJq8Pya4Im/fEOGnJCdoAdMjeJut4X7ER5xOyxDknNyQ96KJMjrtWfQh0en42TabOpMWtcOONqHS+DKee6Le0hG1qD1QLEWssW5Zxw+Z91g7kTXKi+kh1r7MQL8PJbjiJkGMvEcQcwLjEOLKHEamuCDe3E9JsdnfXh38GMdSAweVMUbKBG1s+yDLf7awT/Zlwsm0gnLnHamUjnrWtPWl2ID5PtAJzw8izB3gUiY9VNpf5y2swb7MUCtvYSWxLOmh8j0/XLaAcKAz0JJa+7rl3y03a0r3ZNUpO+SPjf/EfbjOYnwvHX2bh1L/2Rzcb6/VmCkmZ/RV3T5/F3alONB2/QMjeYFApWHULT36m9c0n+eWYt39BrfFW34HzgjxicaNx7w8RV0t+fnsPzy616WtKved3X/7n7n769enPRXnLK/yzD1d3lbXx+1JguegSyqklnr9gzodyJ6zUxT4gVqm3HvwiXJf/11jruL/95wLkaJrzpT8xK1sUe992v3jIXsqD24ffszbxM/cFHV5bt9OhHktcGMazgXTVq9PYx//E03qnzD8tM3CnuQe1ZkRgqx/WJ9Ictxn5fGZfDufnhM/6a4qLn6yWN76x1lc3HNiA311nV2v7CfqgbtvT7fEgq5gIYjxfV3Wuzjdt2dbYsEWWThTxyf1qE/c1rx9+2/H3RYovMsMrAXfX0BJ+u9ff/z7Hxcnd8c==END_SIMPLICITY_STUDIO_METADATA
# END OF METADATA