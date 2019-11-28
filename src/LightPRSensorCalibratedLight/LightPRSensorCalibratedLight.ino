#include <Arduino.h>
#include <EEPROM.h>
#include <duinocom2.h>

#include "Common.h"
#include "LightPRSensor.h"
#include "Illumination.h"
#include "Commands.h"
#include "Clock.h"
#include "SerialOutput.h"
#include "DeviceName.h"

bool isDebug = false;

void setup()
{
  Serial.begin(9600);

  Serial.println("Light controller");
  
  loadDeviceNameFromEEPROM();
  
  serialPrintDeviceInfo();
  
  setupClock();

  setupLightPRSensor();

  setupIllumination();

  serialOutputIntervalInSeconds = lightPRSensorReadingIntervalInSeconds;

  Serial.println("Online");
}

void loop()
{
// Disabled. Used for debugging
//  Serial.print(".");

  if (isDebugMode)
    loopNumber++;

  serialPrintLoopHeader();

  checkCommand();

  takeLightPRSensorReading();

  serialPrintData();

  illuminateIfNeeded(Rtc);

  serialPrintLoopFooter();

  delay(1);
}

