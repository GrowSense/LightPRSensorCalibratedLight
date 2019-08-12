#ifndef COMMON_H_
#define COMMON_H_

extern const int ANALOG_MAX;

extern unsigned long lastSerialOutputTime; // Milliseconds
extern long serialOutputIntervalInSeconds;

extern bool isDebugMode;

extern long loopNumber;

void serialPrintLoopHeader();
void serialPrintLoopFooter();

void EEPROMWriteLong(int address, long value);
long EEPROMReadLong(int address);

void setEEPROMFlag(int address);
void removeEEPROMFlag(int address);

unsigned long secondsToMilliseconds(int seconds);
float millisecondsToSecondsWithDecimal(int milliseconds);

void forceSerialOutput();

#endif
