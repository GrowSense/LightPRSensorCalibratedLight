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
bool lightIsNeeded = 0;
unsigned long lightStartTime = 0;
unsigned long lastLightFinishTime = 0;

//int lightMode = LIGHT_MODE_AUTO_PWM;
int lightMode = LIGHT_MODE_TIMER;

#define thresholdIsSetEEPROMFlagAddress 20
#define thresholdEEPROMAddress 21

int startHour = 6;
int startMinute = 0;
int stopHour = 18;
int stopMinute = 0;

#define lightStartHourIsSetEEPROMFlagAddress 24
#define lightStartHourEEPROMAddress 25

#define lightStartMinuteIsSetEEPROMFlagAddress 28
#define lightStartMinuteEEPROMAddress 29

#define lightStopHourIsSetEEPROMFlagAddress 32
#define lightStopHourEEPROMAddress 33

#define lightStopMinuteIsSetEEPROMFlagAddress 36
#define lightStopMinuteEEPROMAddress 37

/* Setup */
void setupIllumination()
{
  pinMode(LIGHT_PIN, OUTPUT);

  setupThreshold();
  setupStartHour();
  setupStartMinute();
  setupStopHour();
  setupStopMinute();
}

void setupThreshold()
{
  bool eepromIsSet = EEPROM.read(thresholdIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    threshold = getThreshold();
  }
}

void setupStartHour()
{
  bool eepromIsSet = EEPROM.read(lightStartHourIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    startHour = getLightStartHour();
  }
}

void setupStartMinute()
{
  bool eepromIsSet = EEPROM.read(lightStartMinuteIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    startMinute = getLightStartMinute();
  }
}

void setupStopHour()
{
  bool eepromIsSet = EEPROM.read(lightStopHourIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    stopHour = getLightStopHour();
  }
}

void setupStopMinute()
{
  bool eepromIsSet = EEPROM.read(lightStopMinuteIsSetEEPROMFlagAddress) == 99;

  if (eepromIsSet)
  {
    stopMinute = getLightStopMinute();
  }
}

/* Illumination */
void illuminateIfNeeded(RtcDS1302<ThreeWire> clock)
{
  if (isDebugMode)
  {
    Serial.println("Irrigating (if needed)");
  }

  if (lightMode == LIGHT_MODE_AUTO_ABOVETHRESHOLD)
  {
    illuminateByThresholdIfNeeded(true);
  }
  else if (lightMode == LIGHT_MODE_AUTO_BELOWTHRESHOLD)
  {
    illuminateByThresholdIfNeeded(false);
  }
  else if (lightMode == LIGHT_MODE_AUTO_PWM)
  {
    illuminateByPWMIfNeeded();
  }
  else if (lightMode == LIGHT_MODE_TIMER)
  {
    illuminateByTimerIfNeeded(clock);
  }
  else if(lightMode == LIGHT_MODE_ON)
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

void illuminateByThresholdIfNeeded(bool onWhenAbove)
{
  bool readingHasBeenTaken = lastLightPRSensorReadingTime > 0;
  
  if (readingHasBeenTaken)
  {
    bool lightIsNeeded = checkLightNeededByThreshold(onWhenAbove);    
    
    if (isDebugMode)
    {
      Serial.print("Light is needed:");
      Serial.println(lightIsNeeded);
    }

    if (lightIsOn && !lightIsNeeded)
    {
      lightOff();
    }
    else if (!lightIsOn && lightIsNeeded)
    {
      lightOn();
    }
  }
}

bool checkLightNeededByThreshold(bool onWhenAbove)
{
  if (isDebugMode)
  {
    Serial.println("Checking if light is needed in threshold mode");
      
    Serial.print("Light level: ");
    Serial.println(lightLevelCalibrated);
    Serial.print("Threshold: ");
    Serial.println(threshold);  
  }
    
  if (onWhenAbove)
  {  
    lightIsNeeded = lightLevelCalibrated >= threshold;
    
    if (isDebugMode)
      Serial.println("Light is needed when above threshold");
  }
  else
  {
    lightIsNeeded = lightLevelCalibrated < threshold;
    
    if (isDebugMode)
      Serial.println("Light is needed when above threshold");
  }
      
  return lightIsNeeded;
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

  /*if (isDebugMode)
  {
  Serial.println("Controlling illumination by timer");
  }*/

  RtcDateTime now = rtc.GetDateTime();
    
  bool isAfterStartTime = isNowAfterTime(startHour, startMinute, rtc);
     
  bool isBeforeStopTime = !isNowAfterTime(stopHour, stopMinute, rtc); 
  
  /*if (isDebugMode)
  {
    Serial.print("Is after start time: ");
    Serial.println(isAfterStartTime);
    
    Serial.print("Is before stop time: ");
    Serial.println(isBeforeStopTime);
  }*/
 
  lightIsNeeded = isAfterStartTime && isBeforeStopTime;
    
  /*if (isDebugMode)
  {
    Serial.print("Setting light pin: ");
    Serial.println(lightIsNeeded);
  }*/
  
  if (lightIsNeeded)
    lightOn();
  else
    lightOff();
}

bool isNowAfterTime(int timeHour, int timeMinute, RtcDS1302<ThreeWire> rtc)
{
  RtcDateTime now = rtc.GetDateTime();
  
  bool isAfterTime = false;
  bool isAfterHour = now.Hour() > timeHour;
  bool isHour = now.Hour() == timeHour;
  
  /*if (isDebugMode)
  {
    Serial.println("Checking whether 'now' is after the specified time:");
    
    Serial.print("Current hour (now): ");
    Serial.println(now.Hour());
    
    Serial.print("Current minute (now): ");
    Serial.println(now.Minute());
    
    Serial.print("Target minute: ");
    Serial.println(timeMinute);
    
    Serial.print("Target hour: ");
    Serial.println(timeHour);
    
    Serial.print("Target minute: ");
    Serial.println(timeMinute);
    
    Serial.print("Is after hour: ");
    Serial.println(isAfterHour);
  
    Serial.print("Is hour: ");
    Serial.println(isHour);
  }*/
    
  if (isHour)
    isAfterTime = now.Minute() >= timeMinute;
  else if (isAfterHour)
    isAfterTime = true;
    
  /*if (isDebugMode)
  {
    Serial.print("Now is after target time: ");
    Serial.println(isAfterTime);
  }*/
 
  return isAfterTime;
    
}

void lightOn()
{
  if (!lightIsOn)
  {
    if (isDebugMode)
      Serial.println("Turning light on");

    digitalWrite(LIGHT_PIN, HIGH);
    lightIsOn = true;

    lightStartTime = millis();
  }
}

void lightOff()
{
  if (lightIsOn)
  {
    if (isDebugMode)
      Serial.println("Turning light off");
    
    digitalWrite(LIGHT_PIN, LOW);
    lightIsOn = false;

    lastLightFinishTime = millis();
  }
}

void setLightMode(char* msg)
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

    setLightMode(value);
  }
}

void setLightMode(int newStatus)
{
  lightMode = newStatus;
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

  /*if (isDebugMode)
  {
    Serial.print("Setting threshold to EEPROM: ");
    Serial.println(threshold);
  }*/

  EEPROM.write(thresholdEEPROMAddress, newThreshold);
  
  setEEPROMFlag(thresholdIsSetEEPROMFlagAddress);
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
    int threshold = value;

    /*if (isDebugMode)
    {
      Serial.print("Threshold found in EEPROM: ");
      Serial.println(threshold);
    }*/

    return threshold;
  }
}

/* Timing */
void setLightStartHour(char* msg)
{
  int length = strlen(msg);

  if (length > 0)
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setLightStartHour(value);
  }
}

void setLightStartHour(int newStartHour)
{
  startHour = newStartHour;

  /*if (isDebugMode)
  {
    Serial.print("Setting start hour to EEPROM: ");
    Serial.println(startHour);
  }*/

  EEPROM.write(lightStartHourEEPROMAddress, newStartHour);
  
  setEEPROMFlag(lightStartHourIsSetEEPROMFlagAddress);
}

int getLightStartHour()
{
  int value = EEPROM.read(lightStartHourEEPROMAddress);

  if (value < 0
      || value > 24)
    return startHour;
  else
  {
    int startHour = value;

    /*if (isDebugMode)
    {
      Serial.print("Start hour found in EEPROM: ");
      Serial.println(startHour);
    }*/

    return startHour;
  }
}

void setLightStartMinute(char* msg)
{
  int length = strlen(msg);

  if (length > 0)
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setLightStartMinute(value);
  }
}

void setLightStartMinute(int newStartMinute)
{
  startMinute = newStartMinute;

  if (isDebugMode)
  {
    Serial.print("Setting start minute to EEPROM: ");
    Serial.println(startMinute);
  }

  EEPROM.write(lightStartMinuteEEPROMAddress, newStartMinute);
  
  setEEPROMFlag(lightStartMinuteIsSetEEPROMFlagAddress);
}

int getLightStartMinute()
{
  int value = EEPROM.read(lightStartMinuteEEPROMAddress);

  if (value < 0
      || value > 60)
    return startMinute;
  else
  {
    int startMinute = value;

    /*if (isDebugMode)
    {
      Serial.print("Start minute found in EEPROM: ");
      Serial.println(startMinute);
    }*/

    return startMinute;
  }
}

void setLightStopHour(char* msg)
{
  int length = strlen(msg);

  if (length > 1)
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setLightStopHour(value);
  }
}

void setLightStopHour(int newStopHour)
{
  stopHour = newStopHour;

  /*if (isDebugMode)
  {
    Serial.print("Setting stop hour to EEPROM: ");
    Serial.println(stopHour);
  }*/

  EEPROM.write(lightStopHourEEPROMAddress, newStopHour);
  
  setEEPROMFlag(lightStopHourIsSetEEPROMFlagAddress);
}

int getLightStopHour()
{
  int value = EEPROM.read(lightStopHourEEPROMAddress);

  if (value < 0
      || value > 24)
    return stopHour;
  else
  {
    int stopHour = value;

    /*if (isDebugMode)
    {
      Serial.print("Stop hour found in EEPROM: ");
      Serial.println(stopHour);
    }*/

    return stopHour;
  }
}

void setLightStopMinute(char* msg)
{
  int length = strlen(msg);

  if (length > 1)
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setLightStopMinute(value);
  }
}

void setLightStopMinute(int newStopMinute)
{
  stopMinute = newStopMinute;

  /*if (isDebugMode)
  {
    Serial.print("Setting light stop minute to EEPROM: ");
    Serial.println(stopMinute);
  }*/

  EEPROM.write(lightStopMinuteEEPROMAddress, newStopMinute);
  
  setEEPROMFlag(lightStopMinuteIsSetEEPROMFlagAddress);
}

int getLightStopMinute()
{
  int value = EEPROM.read(lightStopMinuteEEPROMAddress);

  if (value < 0
      || value > 60)
    return stopMinute;
  else
  {
    int stopMinute = value;

    /*if (isDebugMode)
    {
      Serial.print("Stop minute found in EEPROM: ");
      Serial.println(stopMinute);
    }*/

    return stopMinute;
  }
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

  removeEEPROMFlag(thresholdIsSetEEPROMFlagAddress);

  threshold = 30;

  setThreshold(threshold);
}
