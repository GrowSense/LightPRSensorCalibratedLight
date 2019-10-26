#ifndef LIGHTPRSENSOR_H_
#define LIGHTPRSENSOR_H_

extern int lightLevelCalibrated;
extern int lightLevelRaw;

extern unsigned long lastLightPRSensorReadingTime;
extern long lightPRSensorReadingIntervalInSeconds;
extern int lightPRSensorReadIntervalIsSetFlagAddress;

extern int darkLightCalibrationValue;
extern int brightLightCalibrationValue;

extern bool lightPRSensorReadingHasBeenTaken;

void setupLightPRSensor();

void setupCalibrationValues();

void setupLightPRSensorReadingInterval();

void takeLightPRSensorReading();

double getAverageLightPRSensorReading();

double calculateLightLevel(int lightPRSensorReading);

void setEEPROMIsCalibratedFlag();

void setLightPRSensorReadingInterval(char* msg);
void setLightPRSensorReadingInterval(long readInterval);

long getLightPRSensorReadingInterval();

void setEEPROMLightPRSensorReadingIntervalIsSetFlag();

void setDarkLightCalibrationValue(char* msg);

void setDarkLightCalibrationValueToCurrent();

void setDarkLightCalibrationValue(int darkLightCalibrationValue);

void setBrightLightCalibrationValue(char* msg);

void setBrightLightCalibrationValueToCurrent();

void setBrightLightCalibrationValue(int brightLightCalibrationValue);

void reverseLightCalibrationValues();

int getDarkLightCalibrationValue();

int getBrightLightCalibrationValue();

void setEEPROMIsCalibratedFlag();

void removeEEPROMIsCalibratedFlag();

void restoreDefaultLightPRSensorSettings();
void restoreDefaultLightPRSensorReadingIntervalSettings();
void restoreDefaultCalibrationSettings();
#endif
/* LIGHTPRSENSOR_H_ */
