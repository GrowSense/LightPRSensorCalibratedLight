#ifndef CLOCK_H_
#define CLOCK_H_

#include <ThreeWire.h>  
#include <RtcDS1302.h>
#include <duinocom2.h>

#define countof(a) (sizeof(a) / sizeof(a[0]))

extern ThreeWire myWire;
extern RtcDS1302<ThreeWire> Rtc;

void setupClock();

void loopClock();

void printDateTime(const RtcDateTime& dt);

void printTime(const RtcDateTime& dt);

void setClock(char* msg);

void readCharArray(char msg[MAX_MSG_LENGTH], char buffer[MAX_MSG_LENGTH], int startPosition, int valueLength);

#endif
/* CLOCK_H_ */
