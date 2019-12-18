#include <Arduino.h>
#include <EEPROM.h>

#include <ThreeWire.h>  
#include <RtcDS1302.h>

#include "Common.h"
#include "LightPRSensor.h"
#include "Illumination.h"
#include "DeviceName.h"
#include "Clock.h"

void serialPrintDeviceInfo()
{
  Serial.println("");
  Serial.println("-- Start Device Info");
  Serial.println("Family: GrowSense");
  Serial.println("Group: illuminator");
  Serial.println("Project: LightPRSensorCalibratedLight");
  Serial.print("Device name: ");
  Serial.println(deviceName);
  Serial.print("Board: ");
  Serial.println(BOARD_TYPE);
  Serial.print("Version: ");
  Serial.println(VERSION);
  Serial.println("ScriptCode: illuminator");
  Serial.println("-- End Device Info");
  Serial.println("");
}

void serialPrintData()
{
  bool isTimeToPrintData = millis() - lastSerialOutputTime >= secondsToMilliseconds(serialOutputIntervalInSeconds)
      || lastSerialOutputTime == 0;

  bool isReadyToPrintData = isTimeToPrintData && lightPRSensorReadingHasBeenTaken;

  if (isReadyToPrintData)
  {
    Serial.print("D;Name:");
    Serial.print(deviceName);
    Serial.print(";R:");
    Serial.print(lightLevelRaw);
    Serial.print(";L:");
    Serial.print(lightLevelCalibrated);
    Serial.print(";T:");
    Serial.print(threshold);
    Serial.print(";M:");
    Serial.print(lightMode);
    Serial.print(";I:");
    Serial.print(lightPRSensorReadingIntervalInSeconds);
    Serial.print(";LN:"); // Light needed
    Serial.print(lightIsNeeded);
    Serial.print(";LO:"); // Light on
    Serial.print(lightIsOn);
    Serial.print(";D:"); // Dark calibration value
    Serial.print(darkLightCalibrationValue);
    Serial.print(";B:"); // Bright calibration value
    Serial.print(brightLightCalibrationValue);
    Serial.print(";V:");
    Serial.print(VERSION);
    Serial.print(";E:");
    Serial.print(startHour);
    Serial.print(";F:");
    Serial.print(startMinute);
    Serial.print(";G:");
    Serial.print(stopHour);
    Serial.print(";H:");
    Serial.print(stopMinute);
    Serial.print(";C:");
    printDateTime(Rtc.GetDateTime());
    Serial.print(";;");
    Serial.println();


/*    if (isDebugMode)
    {
      Serial.print("Last pump start time:");
      Serial.println(pumpStartTime);
      Serial.print("Last pump finish time:");
      Serial.println(lastPumpFinishTime);
    }*/

    lastSerialOutputTime = millis();
  }
/*  else
  {
    if (isDebugMode)
    {    
      Serial.println("Not ready to serial print data");

      Serial.print("  Is time to serial print data: ");
      Serial.println(isTimeToPrintData);
      if (!isTimeToPrintData)
      {
        Serial.print("    Time remaining before printing data: ");
        Serial.print(millisecondsToSecondsWithDecimal(lastSerialOutputTime + secondsToMilliseconds(serialOutputIntervalInSeconds) - millis()));
        Serial.println(" seconds");
      }
      Serial.print("  Soil moisture sensor reading has been taken: ");
      Serial.println(lightSensorReadingHasBeenTaken);
      Serial.print("  Is ready to print data: ");
      Serial.println(isReadyToPrintData);

    }
  }*/
}

void forceSerialOutput()
{
    // Reset the last serial output time 
    lastSerialOutputTime = 0;//millis()-secondsToMilliseconds(serialOutputIntervalInSeconds);
}
