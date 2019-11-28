#include <Arduino.h>
#include <EEPROM.h>

#include <duinocom2.h>

#include "Common.h"
#include "LightPRSensor.h"
#include "EEPROMHelper.h"

#define lightPRSensorPin A0
#define lightPRSensorPowerPin 12

bool lightPRSensorIsOn = true;
unsigned long lastSensorOnTime = 0;

bool lightPRSensorReadingHasBeenTaken = false;
long lightPRSensorReadingIntervalInSeconds = 1;
unsigned long lastLightPRSensorReadingTime = 0; // Milliseconds

int lightLevelCalibrated = 0;
int lightLevelRaw = 0;

bool reverseLightPRSensor = false;
//int darkLightCalibrationValue = ANALOG_MAX;
int darkLightCalibrationValue = (reverseLightPRSensor ? 800 : 0);
//int brightLightCalibrationValue = 0;
int brightLightCalibrationValue = (reverseLightPRSensor ? 0 : 800);

#define lightPRSensorIsCalibratedFlagAddress 1
#define darkLightCalibrationValueAddress 2
#define brightLightCalibrationValueAddress 6

#define lightPRSensorReadIntervalIsSetFlagAddress 10
#define lightPRSensorReadingIntervalAddress 13

/* Setup */
void setupLightPRSensor()
{
  setupCalibrationValues();

  setupLightPRSensorReadingInterval();

  pinMode(lightPRSensorPowerPin, OUTPUT);
}

/* Sensor Readings */
void takeLightPRSensorReading()
{
  bool sensorReadingIsDue = millis() - lastLightPRSensorReadingTime >= secondsToMilliseconds(lightPRSensorReadingIntervalInSeconds)
    || lastLightPRSensorReadingTime == 0;

  if (sensorReadingIsDue)
  {
    /*if (isDebugMode)
      Serial.println("Sensor reading is due");

    if (isDebugMode)
      Serial.println("Preparing to take reading");*/

    lastLightPRSensorReadingTime = millis();

    lightLevelRaw = getAverageLightPRSensorReading();

    lightLevelCalibrated = calculateLightLevel(lightLevelRaw);

    if (lightLevelCalibrated < 0)
      lightLevelCalibrated = 0;

    if (lightLevelCalibrated > 100)
      lightLevelCalibrated = 100;

    lightPRSensorReadingHasBeenTaken = true;
  }
}

double getAverageLightPRSensorReading()
{
  int readingSum  = 0;
  int totalReadings = 10;

  for (int i = 0; i < totalReadings; i++)
  {
    int reading = analogRead(lightPRSensorPin);

    readingSum += reading;
  }

  double averageReading = readingSum / totalReadings;

  return averageReading;
}

double calculateLightLevel(int lightPRSensorReading)
{
  return map(lightPRSensorReading, darkLightCalibrationValue, brightLightCalibrationValue, 0, 100);
}

/* Reading interval */
void setupLightPRSensorReadingInterval()
{
  bool eepromIsSet = EEPROM.read(lightPRSensorReadIntervalIsSetFlagAddress) == 99;

  if (eepromIsSet)
  {
    //if (isDebugMode)
    //	Serial.println("EEPROM read interval value has been set. Loading.");

    lightPRSensorReadingIntervalInSeconds = getLightPRSensorReadingInterval();
  }
  /*else
  {
    if (isDebugMode)
      Serial.println("EEPROM read interval value has not been set. Using defaults.");
  }*/
}

void setLightPRSensorReadingInterval(char* msg)
{
    int value = readInt(msg, 1, strlen(msg)-1);

    setLightPRSensorReadingInterval(value);
}

void setLightPRSensorReadingInterval(long newValue)
{
  /*if (isDebugMode)
  {
    Serial.print("Set sensor reading interval: ");
    Serial.println(newValue);
  }*/

  EEPROMWriteLong(lightPRSensorReadingIntervalAddress, newValue);

  setEEPROMLightPRSensorReadingIntervalIsSetFlag();

  lightPRSensorReadingIntervalInSeconds = newValue; 

  serialOutputIntervalInSeconds = newValue;
}

long getLightPRSensorReadingInterval()
{
  long value = EEPROMReadLong(lightPRSensorReadingIntervalAddress);

  if (value == 0
      || value == 255)
    return lightPRSensorReadingIntervalInSeconds;
  else
  {
    /*if (isDebugMode)
    {
      Serial.print("Read interval found in EEPROM: ");
      Serial.println(value);
    }*/

    return value;
  }
}

void setEEPROMLightPRSensorReadingIntervalIsSetFlag()
{
  if (EEPROM.read(lightPRSensorReadIntervalIsSetFlagAddress) != 99)
    EEPROM.write(lightPRSensorReadIntervalIsSetFlagAddress, 99);
}

/* Calibration */
void setupCalibrationValues()
{
  bool eepromIsSet = EEPROM.read(lightPRSensorIsCalibratedFlagAddress) == 99;

  if (eepromIsSet)
  {
    //if (isDebugMode)
    //	Serial.println("EEPROM calibration values have been set. Loading.");

    darkLightCalibrationValue = getDarkLightCalibrationValue();
    brightLightCalibrationValue = getBrightLightCalibrationValue();
  }
  /*else
  {
    //if (isDebugMode)
    //  Serial.println("EEPROM calibration values have not been set. Using defaults.");
    
    //setDarkLightCalibrationValue(darkLightCalibrationValue);
    //setBrightLightCalibrationValue(brightLightCalibrationValue);
  }*/
}

void setDarkLightCalibrationValue(char* msg)
{
  int length = strlen(msg);

  if (length == 1)
    setDarkLightCalibrationValueToCurrent();
  else
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setDarkLightCalibrationValue(value);
  }
}

void setDarkLightCalibrationValueToCurrent()
{
  lastLightPRSensorReadingTime = 0;
  takeLightPRSensorReading();
  setDarkLightCalibrationValue(lightLevelRaw);
}

void setDarkLightCalibrationValue(int newValue)
{
  /*if (isDebugMode)
  {
    Serial.print("Setting dark soil moisture sensor calibration value: ");
    Serial.println(newValue);
  }*/

  darkLightCalibrationValue = newValue;
  
  EEPROMWriteLong(darkLightCalibrationValueAddress, newValue);

  setEEPROMIsCalibratedFlag();
}

void setBrightLightCalibrationValue(char* msg)
{
  int length = strlen(msg);

  if (length == 1)
    setBrightLightCalibrationValueToCurrent();
  else
  {
    int value = readInt(msg, 1, length-1);

//    Serial.println("Value:");
//    Serial.println(value);

    setBrightLightCalibrationValue(value);
  }
}

void setBrightLightCalibrationValueToCurrent()
{
  lastLightPRSensorReadingTime = 0;
  takeLightPRSensorReading();
  setBrightLightCalibrationValue(lightLevelRaw);
}

void setBrightLightCalibrationValue(int newValue)
{
  /*if (isDebugMode)
  {
    Serial.print("Setting bright soil moisture sensor calibration value: ");
    Serial.println(newValue);
  }*/

  brightLightCalibrationValue = newValue;

  EEPROMWriteLong(brightLightCalibrationValueAddress, newValue);
  
  setEEPROMIsCalibratedFlag();
}

void reverseLightCalibrationValues()
{
  //if (isDebugMode)
  //  Serial.println("Reversing soil moisture sensor calibration values");

  int tmpValue = darkLightCalibrationValue;

  darkLightCalibrationValue = brightLightCalibrationValue;

  brightLightCalibrationValue = tmpValue;

  if (EEPROM.read(lightPRSensorIsCalibratedFlagAddress) == 99)
  {
    setBrightLightCalibrationValue(brightLightCalibrationValue);
    setDarkLightCalibrationValue(darkLightCalibrationValue);
  }
}

int getDarkLightCalibrationValue()
{
  int value = EEPROMReadLong(darkLightCalibrationValueAddress);

  if (value < 0
      || value > ANALOG_MAX)
    return darkLightCalibrationValue;
  else
  {
    int darkLightPRSensorValue = value;
  
    /*if (isDebugMode)
    {
      Serial.print("Dark calibration value found in EEPROM: ");
      Serial.println(darkLightPRSensorValue);
    }*/

    return darkLightPRSensorValue;
  }
}

int getBrightLightCalibrationValue()
{
  int value = EEPROMReadLong(brightLightCalibrationValueAddress);

  if (value < 0
      || value > ANALOG_MAX)
    return brightLightCalibrationValue;
  /*else
  {
    if (isDebugMode)
    {
      Serial.print("Bright calibration value found in EEPROM: ");
      Serial.println(value);
    }
  }*/

  return value;
}

void setEEPROMIsCalibratedFlag()
{
  if (EEPROM.read(lightPRSensorIsCalibratedFlagAddress) != 99)
    EEPROM.write(lightPRSensorIsCalibratedFlagAddress, 99);
}

void removeEEPROMIsCalibratedFlag()
{
    EEPROM.write(lightPRSensorIsCalibratedFlagAddress, 0);
}

void restoreDefaultLightPRSensorSettings()
{
  restoreDefaultCalibrationSettings();
  restoreDefaultLightPRSensorReadingIntervalSettings();
}

void restoreDefaultLightPRSensorReadingIntervalSettings()
{
  EEPROMRemoveFlag(lightPRSensorReadIntervalIsSetFlagAddress);

  lightPRSensorReadingIntervalInSeconds = 5;
  serialOutputIntervalInSeconds = 5;

  setLightPRSensorReadingInterval(lightPRSensorReadingIntervalInSeconds);
}

void restoreDefaultCalibrationSettings()
{
  EEPROMRemoveFlag(lightPRSensorIsCalibratedFlagAddress);

  darkLightCalibrationValue = (reverseLightPRSensor ? 0 : ANALOG_MAX);
  brightLightCalibrationValue = (reverseLightPRSensor ? ANALOG_MAX : 0);

  setDarkLightCalibrationValue(darkLightCalibrationValue);
  setBrightLightCalibrationValue(brightLightCalibrationValue);
}
