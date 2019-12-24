#include <Arduino.h>
#include <EEPROM.h>

#include "Common.h"

#include <ThreeWire.h>  
#include <RtcDS1302.h>
#include <Clock.h>
#include <duinocom2.h>

ThreeWire myWire(4,5,2); // IO, SCLK, CE
RtcDS1302<ThreeWire> Rtc(myWire);


void setupClock()
{
  Rtc.Begin();
  
  if (isDebugMode)
  {
    Serial.println("Compile Date:");
    Serial.println(__DATE__);
    Serial.println(" Compile Time:");
    Serial.println(__TIME__);
  }
  
  if (Rtc.GetIsWriteProtected())
  {
      if (isDebugMode)
        Serial.println("RTC was write protected, enabling writing now");
        
      Rtc.SetIsWriteProtected(false);
  }

  if (!Rtc.GetIsRunning())
  {
      if (isDebugMode)
        Serial.println("RTC was not actively running, starting now");
        
      Rtc.SetIsRunning(true);
  }
  
  //RtcDateTime now = Rtc.GetDateTime();
  //RtcDateTime compiled = RtcDateTime(__DATE__, __TIME__);
  //if (now < compiled) 
  //{
  //    if (isDebugMode)
  //      Serial.println("RTC is older than compile time!  (Updating DateTime)");
        
  //    Rtc.SetDateTime(compiled);
  //}

  /*RtcDateTime now = Rtc.GetDateTime();
  
  Serial.println();
  Serial.print("Clock: ");
  printDateTime(now);
  Serial.println();
  Serial.println();*/
}

void loopClock()
{
  //RtcDateTime now = Rtc.GetDateTime();

  //printDateTime(now);
  //Serial.println();
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

void printTime(const RtcDateTime& dt)
{
    char datestring[20];

    snprintf_P(datestring, 
            countof(datestring),
            PSTR("%02u-%02u-%02u"),
            dt.Hour(),
            dt.Minute(),
            dt.Second() );
    Serial.print(datestring);
}

void setClock(char* msg)
{
  Serial.println("Setting clock");
  Serial.println(msg);
  
  int startPosition = 0;

  int dateLength = 11;

  /*if (isDebugMode)
  {
    Serial.print("  Date length: ");
    Serial.println(dateLength);
    Serial.print("  Start position: ");
    Serial.println(startPosition);
  }*/

  char dateValue[12];
  readCharArray(msg, dateValue, startPosition, dateLength);

  if (isDebugMode)
  {
    Serial.print("  Date: ");
    Serial.println(dateValue);
  }
    
  int timeStartPosition = startPosition+dateLength+1;
  
  /*if (isDebugMode)
  {
    Serial.print("  Time start position: ");
    Serial.println(timeStartPosition);
  }*/
    
  int timeLength = 8;
  
  /*if (isDebugMode)
  {
    Serial.print("  Time length: ");
    Serial.println(timeLength);
  }*/
  
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
  //if (isDebugMode)
  //  Serial.println("Reading char array");
  
  for (int i = 0; i < valueLength; i++)
  {
    buffer[i] = msg[startPosition+i];

    //if (isDebugMode)
    //  Serial.println(buffer[i]);
    
    if (i == valueLength - 1)
      buffer[i+1] = '\0';
  }
}

