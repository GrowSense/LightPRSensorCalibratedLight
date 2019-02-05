using System;
using System.Collections.Generic;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightTimerModeTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public LightMode LightMode = LightMode.Timer;
		public int SimulatedLightPercentage = 60;
		public int Timer = 50;
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

			Console.WriteLine("Light command: " + LightMode);
			Console.WriteLine("Simulated light: " + SimulatedLightPercentage + "%");
			Console.WriteLine("");

			ConnectDevices();

			var cmd = "M" + (int)LightMode;

			SendDeviceCommand(cmd);

			//SendDeviceCommand("T" + Timer);

			var deviceTime = DeviceTime.ToString ("dd/MM/yyyy HH:mm:ss");

			Console.WriteLine ("Device time: " + deviceTime);

			SendDeviceCommand("C" + deviceTime);

			SendDeviceCommand("H" + deviceTime);

			/*SimulateLight(SimulatedLightPercentage);

			var data = WaitForData(3);

			CheckDataValues(data[data.Length - 1]);*/
		}

		public void CheckDataValues(Dictionary<string, string> dataEntry)
		{
			AssertDataValueEquals(dataEntry, "M", (int)LightMode);
			AssertDataValueEquals(dataEntry, "T", Timer);

			// TODO: Check LO value matches the light

			AssertDataValueIsWithinRange(dataEntry, "L", SimulatedLightPercentage, CalibratedValueMarginOfError);

			switch (LightMode)
			{
			case LightMode.Timer:
				CheckLightIsAccurate();
				break;
			default:
				throw new Exception("Test does not support light mode: " + LightMode);
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

		public void CheckLightIsAccurate()
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

