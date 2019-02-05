﻿using System;
using System.Collections.Generic;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightTimerModeTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public LightMode Mode = LightMode.Timer;

		public int DurationToCheckLight = 5;
		public bool LightIsExpected = false;

		public DateTime DeviceTime = DateTime.Now;

		public int StartHour = 6;
		public int StartMinute = 30;
		public int StopHour = 18;
		public int StopMinute = 30;

		public void TestLight()
		{
			WriteTitleText("Starting light timer mode test");

			Console.WriteLine("Light mode: " + Mode);
			Console.WriteLine("");

			ConnectDevices();

			var cmd = "M" + (int)Mode;

			SendDeviceCommand(cmd);

			// Set the clock on the device
			var deviceTime = DeviceTime.ToString ("dd/MM/yyyy HH:mm:ss");

			Console.WriteLine ("Device time: " + deviceTime);

			SendDeviceCommand("C" + deviceTime);

			// Set the start and stop times for the light
			SendDeviceCommand("E" + StartHour);
			SendDeviceCommand("F" + StartMinute);
			SendDeviceCommand("G" + StopHour);
			SendDeviceCommand("H" + StopMinute);

			// Skip a line of data in case it isn't up to date yet
			WaitForData (1);

			// Get the next line of data
			var dataEntry = WaitForDataEntry();

			CheckResults(dataEntry);
		}

		public void CheckResults(Dictionary<string, string> dataEntry)
		{
			AssertDataValueEquals(dataEntry, "M", (int)Mode);

			// TODO: Check LO value matches the light

			switch (Mode)
			{
				case LightMode.Timer:
					CheckLight();
					break;
				default:
					throw new Exception("Timer test does not support light mode: " + Mode);
			}

		}

		public void CheckLightIsOff()
		{
			AssertSimulatorPinForDuration("light", SimulatorLightPin, false, DurationToCheckLight);
		}

		public void CheckLightIsOn()
		{
			AssertSimulatorPinForDuration("light", SimulatorLightPin, true, DurationToCheckLight);
		}

		public void CheckLight()
		{
			if (LightIsExpected)
			{
				CheckLightIsOn();
			}
			else
			{
				CheckLightIsOff();
			}
		}
	}
}

