/***************************************************************************//**
 * @file
 * @brief Core application logic.
 *******************************************************************************
 * # License
 * <b>Copyright 2020 Silicon Laboratories Inc. www.silabs.com</b>
 *******************************************************************************
 *
 * SPDX-License-Identifier: Zlib
 *
 * The licensor of this software is Silicon Laboratories Inc.
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 *
 ******************************************************************************/
#include "em_common.h"
#include "app_assert.h"
#include "sl_bluetooth.h"
#include "gatt_db.h"
#include "sl_simple_button_btn0_config.h"
#include "sl_simple_button_instances.h"
#include "sli_gatt_service_cte_adv.h"
#include "sl_sensor_rht.h"
#include "app.h"
#include "gpiointerrupt.h"
#include "em_gpio.h"
#include "sl_power_manager.h"

#define ENERGY_TRIGGER_PORT gpioPortA
#define ENERGY_TRIGGER_PIN 8

// Macros.
#define UINT16_TO_BYTES(n)            ((uint8_t) (n)), ((uint8_t)((n) >> 8))
#define UINT16_TO_BYTE0(n)            ((uint8_t) (n))
#define UINT16_TO_BYTE1(n)            ((uint8_t) ((n) >> 8))

// The advertising set handle allocated from Bluetooth stack.
static uint8_t advertising_set_handle = 0xff;

static void bcn_setup_adv_beaconing(void);

typedef struct {
    uint8_t flags_len;     // Length of the Flags field.
    uint8_t flags_type;    // Type of the Flags field.
    uint8_t flags;         // Flags field.
    uint8_t mandata_len;   // Length of the Manufacturer Data field.
    uint8_t mandata_type;  // Type of the Manufacturer Data field.
    uint8_t comp_id[2];    // Company ID field.
    uint8_t beac_type[2];  // Beacon Type field.
    uint8_t uuid[16];      // 128-bit Universally Unique Identifier (UUID). The UUID is an identifier for the company using the beacon.
    uint8_t maj_num[2];    // Beacon major number. Used to group related beacons.
    uint8_t min_num[2];    // Beacon minor number. Used to specify individual beacons within a group.
    uint8_t tx_power;      // The Beacon's measured RSSI at 1 meter distance in dBm. See the iBeacon specification for measurement guidelines.
  } bcn_beacon_adv_data;

bcn_beacon_adv_data beacon_data
    = {
    // Flag bits - See Bluetooth 4.0 Core Specification , Volume 3, Appendix C, 18.1 for more details on flags.
    2,            // Length of field.
    0x01,         // Type of field.
    0x04 | 0x02,  // Flags: LE General Discoverable Mode, BR/EDR is disabled.

    // Manufacturer specific data.
    26,   // Length of field.
    0xFF, // Type of field.

    // The first two data octets shall contain a company identifier code from
    // the Assigned Numbers - Company Identifiers document.
    // 0x004C = Apple
    { UINT16_TO_BYTES(0xEFBE) },

    // Beacon type.
    // 0x0215 is iBeacon.
    { UINT16_TO_BYTE1(0x0215), UINT16_TO_BYTE0(0x0215) },

    // 128 bit / 16 byte UUID
    { 0xE2, 0xC5, 0x6D, 0xB5, 0xDF, 0xFB, 0x48, 0xD2, \
      0xB0, 0x60, 0xD0, 0xF5, 0xA7, 0x10, 0x96, 0xE0 },

    // Beacon major number.
    // Set to 34987 and converted to correct format.
    { UINT16_TO_BYTE1(34987), UINT16_TO_BYTE0(34987) },

    // Beacon minor number.
    // Set as 1025 and converted to correct format.
    { UINT16_TO_BYTE1(1025), UINT16_TO_BYTE0(1025) },

    // The Beacon's measured RSSI at 1 meter distance in dBm.
    // 0xD7 is -41dBm
    0xD7
    };

uint8_t _activate_tx = 0;

void GPIO_EnergyTrigger_Callback(uint8_t);

/**************************************************************************//**
 * Application Init.
 *****************************************************************************/
SL_WEAK void app_init(void)
{
  /////////////////////////////////////////////////////////////////////////////
  // Put your additional application init code here!                         //
  // This is called once during start-up.                                    //
  /////////////////////////////////////////////////////////////////////////////
  GPIO_PinModeSet(ENERGY_TRIGGER_PORT, ENERGY_TRIGGER_PIN, gpioModeInputPull, 1);
  GPIO_PinOutClear(ENERGY_TRIGGER_PORT, ENERGY_TRIGGER_PIN);
  GPIO_ExtIntConfig(ENERGY_TRIGGER_PORT, ENERGY_TRIGGER_PIN, ENERGY_TRIGGER_PIN, 1, 0, 1);
  GPIO_IntEnable(ENERGY_TRIGGER_PIN%2 ? GPIO_EVEN_IRQn : GPIO_ODD_IRQn);
  GPIO_IntClear(ENERGY_TRIGGER_PIN%2 ? GPIO_EVEN_IRQn : GPIO_ODD_IRQn);
  
  sl_power_manager_remove_em_requirement(SL_POWER_MANAGER_EM1);
  sl_power_manager_add_em_requirement(SL_POWER_MANAGER_EM2);

  GPIOINT_CallbackRegister(ENERGY_TRIGGER_PIN, GPIO_EnergyTrigger_Callback);
  GPIO_IntConfig(SL_SIMPLE_BUTTON_BTN0_PORT, SL_SIMPLE_BUTTON_BTN0_PIN, 0, 1, 1); //config the btn0 interrupt to be triggered only on btn press
  sl_sensor_rht_init();
}

/**************************************************************************//**
 * Application Process Action.
 *****************************************************************************/
SL_WEAK void app_process_action(void)
{
  /////////////////////////////////////////////////////////////////////////////
  // Put your additional application code here!                              //
  // This is called infinitely.                                              //
  // Do not call blocking functions from here!                               //
  /////////////////////////////////////////////////////////////////////////////
  uint32_t rh;
  int32_t t;

  if(_activate_tx) {
    sl_sensor_rht_get(&rh, &t);
    t /= 10;
    rh /= 10;

    beacon_data.maj_num[0] = UINT16_TO_BYTE1(t);
    beacon_data.maj_num[1] = UINT16_TO_BYTE0(t);
    beacon_data.min_num[0] = UINT16_TO_BYTE1(rh);
    beacon_data.min_num[1] = UINT16_TO_BYTE0(rh);

    sl_bt_advertiser_set_data(advertising_set_handle,
                              0,
                              sizeof(beacon_data),
                              (uint8_t *)(&beacon_data));

    sl_bt_advertiser_start(
      advertising_set_handle,
      sl_bt_advertiser_user_data,
      sl_bt_advertiser_non_connectable);

    adv_cte_start();
    _activate_tx = 0;
  }
}

void GPIO_EnergyTrigger_Callback(uint8_t intNo) {
  _activate_tx = 1;
  (void)intNo;
}

void sl_button_on_change(const sl_button_t *handle) {
  if(handle == &sl_button_btn0) {
      _activate_tx = 1;
  }
}

/**************************************************************************//**
 * Bluetooth stack event handler.
 * This overrides the dummy weak implementation.
 *
 * @param[in] evt Event coming from the Bluetooth stack.
 *****************************************************************************/
void sl_bt_on_event(sl_bt_msg_t *evt)
{
  sl_status_t sc;
  int16_t ret_power_min, ret_power_max;

  switch (SL_BT_MSG_ID(evt->header)) {
    // -------------------------------
    // This event indicates the device has started and the radio is ready.
    // Do not call any stack command before receiving this boot event!
    case sl_bt_evt_system_boot_id:
      // Set 0 dBm maximum Transmit Power.
      sc = sl_bt_system_set_tx_power(SL_BT_CONFIG_MIN_TX_POWER, 0,
                                     &ret_power_min, &ret_power_max);
      app_assert_status(sc);
      (void)ret_power_min;
      (void)ret_power_max;
      // Initialize iBeacon ADV data.
      bcn_setup_adv_beaconing();
      break;

    ///////////////////////////////////////////////////////////////////////////
    // Add additional event handlers here as your application requires!      //
    ///////////////////////////////////////////////////////////////////////////
    case sl_bt_evt_advertiser_timeout_id:
      break;
    // -------------------------------
    // Default event handler.
    default:
      break;
  }
}


static void bcn_setup_adv_beaconing(void)
{
  sl_status_t sc;

  // Create an advertising set.
  sc = sl_bt_advertiser_create_set(&advertising_set_handle);
  app_assert_status(sc);

  // Set custom advertising data.
  sc = sl_bt_advertiser_set_data(advertising_set_handle,
                                 0,
                                 sizeof(beacon_data),
                                 (uint8_t *)(&beacon_data));
  app_assert_status(sc);

  // Set advertising parameters. 100ms advertisement interval.
  sc = sl_bt_advertiser_set_timing(
    advertising_set_handle,
    160,     // min. adv. interval (milliseconds * 1.6)
    160,     // max. adv. interval (milliseconds * 1.6)
    0,       // adv. duration
    0);      // max. num. adv. events
  app_assert_status(sc);
}
