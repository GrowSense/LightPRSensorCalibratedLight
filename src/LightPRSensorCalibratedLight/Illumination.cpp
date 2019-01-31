#include <Arduino.h>
#include <ThreeWire.h>  
#include <RtcDS1302.h>

#include <EEPROM.h>

#include <duinocom.h>

#include "Common.h"
#include "LightPRSensor.h"
#include "Illumination.h"

int threshold = 30;

bool lightIsOn = 0;
long lightStartTime = 0;
long lastLightFinishTime = 0;

//int lightStatus = LIGHT_STATUS_AUTO_PWM;
int lightStatus = LIGHT_STATUS_TIMER;

#define thresholdIsSetEEPROMFlagAddress 20
#define thresholdEEPROMAddress 21

/* Setup */
void setupIllumination()
{
  pinMode(LIGHT_PIN, OUTPUT);

  setupThreshold();
}

void setupThreshold()
{
  bool eepromIsSet = EEPROM.read(thresholdIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    //if (isDebugMode)
    //	Serial.println("EEPROM read interval value has been set. Loading.");

    threshold = getThreshold();
  }
  else
  {
    //if (isDebugMode)
    //  Serial.println("EEPROM read interval value has not been set. Using defaults.");
    
    //setThreshold(threshold);
  }
}

/* Illumination */
void illuminateIfNeeded(RtcDS1302<ThreeWire> clock)
{
  if (isDebugMode)
  {
    Serial.println("Irrigating (if needed)");
  }

  if (lightStatus == LIGHT_STATUS_AUTO_THRESHOLD)
  {
    illuminateByThresholdIfNeeded();
  }
  if (lightStatus == LIGHT_STATUS_AUTO_PWM)
  {
    illuminateByPWMIfNeeded();
  }
  if (lightStatus == LIGHT_STATUS_TIMER)
  {
    illuminateByTimerIfNeeded(clock);
  }
  else if(lightStatus == LIGHT_STATUS_ON)
  {
    if (!lightIsOn)
      lightOn();
  }
  else
  {
    if (lightIsOn)
      lightOff();
  }
}

void illuminateByThresholdIfNeeded()
{
  bool readingHasBeenTaken = lastLightPRSensorReadingTime > 0;
  bool lightIsNeeded = lightLevelCalibrated <= threshold && readingHasBeenTaken;

  if (lightIsOn && !lightIsNeeded)
  {
    lightOff();
  }
  else if (!lightIsOn && lightIsNeeded)
  {
    lightOn();
  }
}

void lightPwm(int pwmValue)
{
  analogWrite(LIGHT_PIN, pwmValue);
}

void illuminateByPWMIfNeeded()
{
  int pwmValue = map(lightLevelCalibrated, darkLightCalibrationValue, brightLightCalibrationValue, 255, 0);
  
 // Serial.print("Light pwm value: ");
 // Serial.println(pwmValue);
  
  lightPwm(pwmValue);
}

void illuminateByTimerIfNeeded(RtcDS1302<ThreeWire> rtc)
{
 // Serial.println("Controlling illumination by timer");

  RtcDateTime now = rtc.GetDateTime();
  
  int startHour = 6;
  int startMinute = 0;
  
  int stopHour = 15;
  int stopMinute = 8;
    
  bool isAfterStartTime = now.Hour() >= startHour
    && now.Minute() >= startMinute;
    
  if (isDebugMode)
  {
    Serial.print("Is after start time: ");
    Serial.println(isAfterStartTime);
  }
    
  bool isBeforeStopTime = false;
  bool isBeforeStopHour = now.Hour() < stopHour;
  
  if (isDebugMode)
  {
    Serial.print("Is before stop hour: ");
    Serial.println(isBeforeStopHour);
  }
    
  if (isBeforeStopHour)
    isBeforeStopTime = true;
  else
  {
    bool isStopHour = now.Hour() == stopHour;
    
    if (isDebugMode)
    {
      Serial.print("Is stop hour: ");
      Serial.println(isStopHour);
    }
    isBeforeStopTime = isStopHour && now.Minute() < stopMinute;
  }
    
  if (isDebugMode)
  {
    Serial.print("Is before stop time: ");
    Serial.println(isBeforeStopTime);
  }
 
  bool lightExpected = isAfterStartTime && isBeforeStopTime;
  
  digitalWrite(LIGHT_PIN, lightExpected);
}

void lightOn()
{
  digitalWrite(LIGHT_PIN, HIGH);
  lightIsOn = true;

  lightStartTime = millis();
}

void lightOff()
{
  digitalWrite(LIGHT_PIN, LOW);
  lightIsOn = false;

  lastLightFinishTime = millis();
}

void setLightStatus(char* msg)
{
  int length = strlen(msg);

  if (length != 2)
  {
    Serial.println("Invalid light status:");
    printMsg(msg);
  }
  else
  {
    int value = readInt(msg, 1, 1);

//    Serial.println("Value:");
//    Serial.println(value);

    setLightStatus(value);
  }
}

void setLightStatus(int newStatus)
{
  lightStatus = newStatus;
}

void setThreshold(char* msg)
{
  int length = strlen(msg);

  if (length == 1)
    setThresholdToCurrent();
  else
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setThreshold(value);
  }
}

void setThreshold(int newThreshold)
{
  threshold = newThreshold;

  if (isDebugMode)
  {
    Serial.print("Setting threshold to EEPROM: ");
    Serial.println(threshold);
  }

  EEPROM.write(thresholdEEPROMAddress, newThreshold);
  
  setThresholdIsSetEEPROMFlag();
}

void setThresholdToCurrent()
{
  lastLightPRSensorReadingTime = 0;
  takeLightPRSensorReading();
  setThreshold(lightLevelCalibrated);
}

int getThreshold()
{
  int value = EEPROM.read(thresholdEEPROMAddress);

  if (value <= 0
      || value >= 100)
    return threshold;
  else
  {
    int threshold = value; // Must multiply by 4 to get the original value

    if (isDebugMode)
    {
      Serial.print("Threshold found in EEPROM: ");
      Serial.println(threshold);
    }

    return threshold;
  }
}

void setThresholdIsSetEEPROMFlag()
{
  if (isDebugMode)
  {
    Serial.print("Setting EEPROM 'threshold is set flag'");
  }

  if (EEPROM.read(thresholdIsSetEEPROMFlagAddress) != 99)
    EEPROM.write(thresholdIsSetEEPROMFlagAddress, 99);
}

/* Restore defaults */
void restoreDefaultIlluminationSettings()
{
  Serial.println("Reset default settings");

  restoreDefaultThreshold();
}

void restoreDefaultThreshold()
{
  Serial.println("Reset threshold");

  removeThresholdEEPROMIsSetFlag();

  threshold = 30;

  setThreshold(threshold);
}

void removeThresholdEEPROMIsSetFlag()
{
    EEPROM.write(thresholdIsSetEEPROMFlagAddress, 0);
}
