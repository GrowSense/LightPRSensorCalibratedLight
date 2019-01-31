using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public LightStatus LightCommand = LightStatus.Auto;
		public int SimulatedLightPercentage = 50;
		public int BurstOnTime = 3;
		public int BurstOffTime = 3;
		public int Threshold = 30;
		public int DurationToCheckLight = 5;

		public void TestLight()
		{
			WriteTitleText("Starting light test");

			Console.WriteLine("Light command: " + LightCommand);
			Console.WriteLine("Simulated soil moisture: " + SimulatedLightPercentage + "%");
			Console.WriteLine("");

			ConnectDevices();

			var cmd = "P" + (int)LightCommand;

			SendDeviceCommand(cmd);

			SendDeviceCommand("B" + BurstOnTime);
			SendDeviceCommand("O" + BurstOffTime);
			SendDeviceCommand("T" + Threshold);

			SimulateLight(SimulatedLightPercentage);

			var data = WaitForData(3);

			CheckDataValues(data[data.Length - 1]);
		}

		public void CheckDataValues(Dictionary<string, string> dataEntry)
		{
			AssertDataValueEquals(dataEntry, "P", (int)LightCommand);
			AssertDataValueEquals(dataEntry, "B", BurstOnTime);
			AssertDataValueEquals(dataEntry, "O", BurstOffTime);
			AssertDataValueEquals(dataEntry, "T", Threshold);

			// TODO: Check PO value matches the light

			AssertDataValueIsWithinRange(dataEntry, "C", SimulatedLightPercentage, CalibratedValueMarginOfError);

			switch (LightCommand)
			{
				case LightStatus.Off:
					CheckLightIsOff();
					break;
				case LightStatus.On:
					CheckLightIsOn();
					break;
				case LightStatus.Auto:
					CheckLightIsAuto();
					break;
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

		public void CheckLightIsAuto()
		{
			var lightIsNeeded = SimulatedLightPercentage < Threshold;

			if (lightIsNeeded)
			{
				var lightStaysOn = BurstOffTime == 0;

				if (lightStaysOn)
				{
					CheckLightIsOn();
				}
				else
				{
					// Wait for the light to turn on for the first time
					WaitUntilSimulatorPinIs("light", SimulatorLightPin, true);

					// Check on time     
					var timeOn = WaitWhileSimulatorPinIs("light", SimulatorLightPin, true);
					AssertIsWithinRange("light", BurstOnTime, timeOn, TimeErrorMargin);

					// Check off time
					var timeOff = WaitWhileSimulatorPinIs("light", SimulatorLightPin, false);
					AssertIsWithinRange("light", BurstOffTime, timeOff, TimeErrorMargin);

					// Check on time
					timeOn = WaitWhileSimulatorPinIs("light", SimulatorLightPin, true);
					AssertIsWithinRange("light", BurstOnTime, timeOn, TimeErrorMargin);

					// Check off time
					timeOff = WaitWhileSimulatorPinIs("light", SimulatorLightPin, false);
					AssertIsWithinRange("light", BurstOffTime, timeOff, TimeErrorMargin);
				}
			}
			else
			{
				CheckLightIsOff();
			}
		}
	}
}