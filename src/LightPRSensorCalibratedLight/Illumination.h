#ifndef ILLUMINATION_H_
#define ILLUMINATION_H_

#include <Arduino.h>
#include <ThreeWire.h>  
#include <RtcDS1302.h>

#define LIGHT_PIN 11

#define LIGHT_MODE_OFF 0
#define LIGHT_MODE_ON 1
#define LIGHT_MODE_TIMER 2 // Timer based on clock
#define LIGHT_MODE_AUTO_ABOVETHRESHOLD 3 // Turn on when above the threshold
#define LIGHT_MODE_AUTO_BELOWTHRESHOLD 4 // Turn on when below the threshold
#define LIGHT_MODE_AUTO_PWM 5 // Fade on and off at the same brightness as outside
#define LIGHT_MODE_AUTO_SUPPLEMENT 6 // Supplement the outside light only when needed

extern int threshold;
extern bool lightIsOn;
extern bool lightIsNeeded;
extern unsigned long lightStartTime;
extern unsigned long lastLightFinishTime;

extern int lightMode;
extern int thresholdIsSetEEPROMFlagAddress;
extern int thresholdEEPROMAddress;

extern int startHour;
extern int startMinute;
extern int stopHour;
extern int stopMinute;

extern int lightStartHourIsSetEEPROMFlagAddress;
extern int lightStartHourEEPROMAddress;
extern int lightStartMinuteIsSetEEPROMFlagAddress;
extern int lightStartMinuteEEPROMAddress;
extern int lightStopHourIsSetEEPROMFlagAddress;
extern int lightStopHourEEPROMAddress;
extern int lightStopMinuteIsSetEEPROMFlagAddress;
extern int lightStopMinuteEEPROMAddress;

void setupIllumination();
void setupThreshold();
void setupStartHour();
void setupStartMinute();
void setupStopHour();
void setupStopMinute();

void illuminateIfNeeded(RtcDS1302<ThreeWire> rtc);
void illuminateByThresholdIfNeeded(bool onWhenAbove);
void illuminateByPWMIfNeeded();
void illuminateByTimerIfNeeded(RtcDS1302<ThreeWire> rtc);
void lightOn();
void lightOff();

bool checkLightNeededByThreshold();
bool checkLightNeededByThreshold(bool onWhenAbove);

void setLightMode(char* msg);
void setLightMode(int newStatus);

void setThreshold(char* msg);
void setThreshold(int newThreshold);
void setThresholdToCurrent();
int getThreshold();

void setLightStartHour(char* msg);
void setLightStartHour(int newLightStartHour);
int getLightStartHour();

void setLightStartMinute(char* msg);
void setLightStartMinute(int newLightStartMinute);
int getLightStartMinute();

void setLightStopHour(char* msg);
void setLightStopHour(int newLightStopHour);
int getLightStopHour();

void setLightStopMinute(char* msg);
void setLightStopMinute(int newLightStopMinute);
int getLightStopMinute();

void restoreDefaultThreshold();
void restoreDefaultIlluminationSettings();
void restoreDefaultLightStartHour();
void restoreDefaultLightStartMinute();

bool isNowAfterTime(int timeHour, int timeMinute, RtcDS1302<ThreeWire> rtc);
#endif
/* ILLUMINATION_H_ */
