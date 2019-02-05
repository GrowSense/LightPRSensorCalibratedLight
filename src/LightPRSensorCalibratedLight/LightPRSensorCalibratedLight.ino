#include <Arduino.h>
#include <EEPROM.h>
#include <duinocom.h>
#include <ThreeWire.h>  
#include <RtcDS1302.h>

#include "Common.h"
#include "LightPRSensor.h"
#include "Illumination.h"

#define SERIAL_MODE_CSV 1
#define SERIAL_MODE_QUERYSTRING 2

#define VERSION "1-0-0-52"

int serialMode = SERIAL_MODE_CSV;


ThreeWire myWire(4,5,2); // IO, SCLK, CE
RtcDS1302<ThreeWire> Rtc(myWire);

void setup()
{
  Serial.begin(9600);

  setupClock();

  Serial.println("Starting light controller");

  setupLightPRSensor();

  setupIllumination();

  serialOutputIntervalInSeconds = lightPRSensorReadingIntervalInSeconds;

}

void setupClock()
{
    Rtc.Begin();
    
    // TODO: Remove if not needed
   /* 
    if (Rtc.GetIsWriteProtected())
    {
        Serial.println("RTC was write protected, enabling writing now");
        Rtc.SetIsWriteProtected(false);
    }

    if (!Rtc.GetIsRunning())
    {
        Serial.println("RTC was not actively running, starting now");
        Rtc.SetIsRunning(true);
    }

    RtcDateTime now = Rtc.GetDateTime();
    if (now < compiled) 
    {
        Serial.println("RTC is older than compile time!  (Updating DateTime)");
        Rtc.SetDateTime(compiled);
    }
    else if (now > compiled) 
    {
        Serial.println("RTC is newer than compile time. (this is expected)");
    }
    else if (now == compiled) 
    {
        Serial.println("RTC is the same as compile time! (not expected but all is fine)");
    }*/
}

void loop()
{
// Disabled. Used for debugging
//  Serial.print(".");

  loopNumber++;

  serialPrintLoopHeader();

  checkCommand();

  takeLightPRSensorReading();

  serialPrintData();

  illuminateIfNeeded(Rtc);

  serialPrintLoopFooter();

  delay(1);
}

void loopClock()
{
  RtcDateTime now = Rtc.GetDateTime();

  printDateTime(now);
  Serial.println();
}

#define countof(a) (sizeof(a) / sizeof(a[0]))

void printDateTime(const RtcDateTime& dt)
{
    char datestring[20];

    snprintf_P(datestring, 
            countof(datestring),
            PSTR("%02u/%02u/%04u %02u:%02u:%02u"),
            dt.Month(),
            dt.Day(),
            dt.Year(),
            dt.Hour(),
            dt.Minute(),
            dt.Second() );
    Serial.print(datestring);
}

/* Commands */
void checkCommand()
{
  if (isDebugMode)
  {
    Serial.println("Checking incoming serial commands");
  }

  if (checkMsgReady())
  {
    char* msg = getMsg();
        
    char letter = msg[0];

    int length = strlen(msg);

    Serial.print("Received message: ");
    Serial.println(msg);

    switch (letter)
    {
      case 'M':
        setLightMode(msg);
        break;
      case 'T':
        setThreshold(msg);
        break;
      case 'D':
        setDarkLightCalibrationValue(msg);
        break;
      case 'B':
        setBrightLightCalibrationValue(msg);
        break;
      case 'I':
        setLightPRSensorReadingInterval(msg);
        break;
      case 'X':
        restoreDefaultSettings();
        break;
/*      case 'N': // TODO: Remove if not needed. The "M" command is used to turn the light on or off so this should be obsolete.
        Serial.println("Turning light on");
        lightMode = LIGHT_MODE_ON;
        lightOn();
        break;
      case 'F':
        Serial.println("Turning light off");
        lightMode = LIGHT_MODE_OFF;
        lightOff();
        break;*/
      case 'E':
        setLightStartHour(msg);
        break;
      case 'F':
        setLightStartMinute(msg);
        break;
      case 'G':
        setLightStopHour(msg);
        break;
      case 'H':
        setLightStopMinute(msg);
        break;
      case 'Z':
        Serial.println("Toggling IsDebug");
        isDebugMode = !isDebugMode;
        break;
      case 'C':
        Serial.println("Setting clock");
        setClock(msg);
        break;
      case 'R':
        reverseLightCalibrationValues();
        break;
    }
    forceSerialOutput();
  }
  delay(1);
}

void setClock(char* msg)
{
 
  int spacePosition = 0;
  for (int i = 0; i < sizeof(msg); i++)
  {
    if (msg[i] == " ")
      spacePosition = i;
  }
  
    Serial.print("  Space position: ");
    Serial.println(spacePosition);
  
  int startPosition = 1;

  int dateLength = 10;

  if (isDebugMode)
  {
    Serial.print("  Date length: ");
    Serial.println(dateLength);
    Serial.print("  Start position: ");
    Serial.println(startPosition);
  }

  char dateValue[11];
  readCharArray(msg, dateValue, startPosition, dateLength);
  
  if (isDebugMode)
  {
    Serial.print("  Date: ");
    Serial.println(dateValue);
  }
    
  int timeStartPosition = startPosition+dateLength+1;
  
  if (isDebugMode)
  {
    Serial.print("  Time start position: ");
    Serial.println(timeStartPosition);
  }
    
  int timeLength = 8;
  
  if (isDebugMode)
  {
    Serial.print("  Time length: ");
    Serial.println(timeLength);
  }
  
  char timeValue[9];
  readCharArray(msg, timeValue, timeStartPosition, timeLength);
  
  if (isDebugMode)
  {
    Serial.print("  Date: '");
    Serial.print(dateValue);
    Serial.println("'");
    Serial.print("  Time: '");
    Serial.print(timeValue);
    Serial.println("'");
  }
  
  Rtc.SetDateTime(RtcDateTime(dateValue, timeValue));
  
  if (isDebugMode)
  {
    Serial.println("RTC time from module");
    RtcDateTime now = Rtc.GetDateTime();
    printDateTime(now);
  }
}

void readCharArray(char msg[MAX_MSG_LENGTH], char buffer[MAX_MSG_LENGTH], int startPosition, int valueLength)
{
  if (isDebugMode)
    Serial.println("Reading char array");

  for (int i = 0; i < MAX_MSG_LENGTH; i++)
  {
      buffer[i] = '\0';
  }
  
  for (int i = 0; i < valueLength; i++)
  {
    buffer[i] = msg[startPosition+i];

    if (isDebugMode)
      Serial.println(buffer[i]);
  }
}

/* Settings */
void restoreDefaultSettings()
{
  Serial.println("Restoring default settings");

  restoreDefaultLightPRSensorSettings();
  restoreDefaultIlluminationSettings();
}

/* Serial Output */
void serialPrintData()
{
  bool isTimeToPrintData = lastSerialOutputTime + secondsToMilliseconds(serialOutputIntervalInSeconds) < millis()
      || lastSerialOutputTime == 0;

  bool isReadyToPrintData = isTimeToPrintData && lightPRSensorReadingHasBeenTaken;

  if (isReadyToPrintData)
  {
    if (isDebugMode)
    {
      Serial.println("Printing serial data");
    }

    if (serialMode == SERIAL_MODE_CSV)
    {
      Serial.print("D;R:");
      Serial.print(lightLevelRaw);
      Serial.print(";L:");
      Serial.print(lightLevelCalibrated);
      Serial.print(";T:");
      Serial.print(threshold);
      Serial.print(";M:");
      Serial.print(lightMode);
      Serial.print(";I:");
      Serial.print(lightPRSensorReadingIntervalInSeconds);
      Serial.print(";LN:"); // Water needed
      Serial.print(lightIsNeeded);
      Serial.print(";LO:"); // Light on
      Serial.print(lightIsOn);
      Serial.print(";D:"); // Dark calibration value
      Serial.print(darkLightCalibrationValue);
      Serial.print(";B:"); // Bright calibration value
      Serial.print(brightLightCalibrationValue);
      Serial.print(";Z:");
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
    }
    /*else
    {
      Serial.print("raw=");
      Serial.print(lightLevelRaw);
      Serial.print("&");
      Serial.print("calibrated=");
      Serial.print(lightLevelCalibrated);
      Serial.print("&");
      Serial.print("threshold=");
      Serial.print(threshold);
      Serial.print("&");
      Serial.print("lightNeeded=");
      Serial.print(lightLevelCalibrated < threshold);
      Serial.print("&");
      Serial.print("lightMode=");
      Serial.print(lightMode);
      Serial.print("&");
      Serial.print("readingInterval=");
      Serial.print(lightPRSensorReadingIntervalInSeconds);
      Serial.print("&");
      Serial.print("lightStartHour=");
      Serial.print(lightStartHour);
      Serial.print("&");
      Serial.print("lightStartMinute=");
      Serial.print(lightStartMinute);
      Serial.print("&");
      Serial.print("lightOn=");
      Serial.print(lightIsOn);
      Serial.print("&");
      Serial.print("secondsSinceLightOn=");
      Serial.print((millis() - lastLightFinishTime) / 1000);
      Serial.print("&");
      Serial.print("dark=");
      Serial.print(darkLightCalibrationValue);
      Serial.print("&");
      Serial.print("bright=");
      Serial.print(brightLightCalibrationValue);
      Serial.print(";;");
      Serial.println();
    }*/

/*    if (isDebugMode)
    {
      Serial.print("Last light start time:");
      Serial.println(lightStartTime);
      Serial.print("Last light finish time:");
      Serial.println(lastLightFinishTime);
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
      Serial.println(lightPRSensorReadingHasBeenTaken);
      Serial.print("  Is ready to print data: ");
      Serial.println(isReadyToPrintData);

    }
  }*/
}
