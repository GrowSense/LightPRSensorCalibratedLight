#ifndef IRRIGATION_H_
#define IRRIGATION_H_

#include <Arduino.h>
#include <ThreeWire.h>  
#include <RtcDS1302.h>

#define LIGHT_PIN 11

#define LIGHT_STATUS_OFF 0
#define LIGHT_STATUS_ON 1
#define LIGHT_STATUS_AUTO_THRESHOLD 2 // Turn on at a particular threshold
#define LIGHT_STATUS_AUTO_PWM 2 // Fade on and off at the same brightness as outside
#define LIGHT_STATUS_AUTO_SUPPLEMENT 3 // Supplement the outside light only when needed
#define LIGHT_STATUS_TIMER 5 // Supplement the outside light only when needed

extern int threshold;
extern bool lightIsOn;
extern long lightStartTime;
extern long lastLightFinishTime;
extern int lightBurstOnTime;
extern int lightBurstOffTime;
extern int lightStatus;
extern int thresholdIsSetEEPROMFlagAddress;
extern int thresholdEEPROMAddress;
extern int lightBurstOnTimeIsSetEEPROMFlagAddress;
extern int lightBurstOnTimeEEPROMAddress;
extern int lightBurstOffTimeIsSetEEPROMFlagAddress;
extern int lightBurstOffTimeEEPROMAddress;

void setupIllumination();
void setupThreshold();
void illuminateIfNeeded(RtcDS1302<ThreeWire> rtc);
void illuminateByThresholdIfNeeded();
void illuminateByPWMIfNeeded();
void illuminateByTimerIfNeeded(RtcDS1302<ThreeWire> rtc);
void lightOn();
void lightOff();

void setLightStatus(char* msg);
void setLightStatus(int newStatus);

void setThreshold(char* msg);
void setThreshold(int newThreshold);
void setThresholdToCurrent();
int getThreshold();
void setThresholdIsSetEEPROMFlag();

void setLightBurstOnTime(char* msg);
void setLightBurstOnTime(long newLightBurstOnTime);
long getLightBurstOnTime();
void setLightBurstOnTimeIsSetEEPROMFlag();
void removeLightBurstOnTimeEEPROMIsSetFlag();

void setLightBurstOffTime(char* msg);
void setLightBurstOffTime(long newLightBurstOffTime);
long getLightBurstOffTime();
void setLightBurstOffTimeIsSetEEPROMFlag();
void removeLightBurstOffTimeEEPROMIsSetFlag();


void removeThresholdEEPROMIsSetFlag();
void restoreDefaultThreshold();
void restoreDefaultIlluminationSettings();
void restoreDefaultLightBurstOnTime();
void restoreDefaultLightBurstOffTime();
#endif
/* IRRIGATION_H_ */
