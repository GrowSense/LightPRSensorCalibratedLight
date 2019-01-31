using System;
using System.Threading;
using NUnit.Framework;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightPRSensorPowerTestHelper : GreenSenseHardwareTestHelper
	{
		public int ReadInterval = 1;

		public void TestLightPRSensorPower()
		{
			WriteTitleText("Starting soil moisture sensor power test");

			ConnectDevices();

			SetDeviceReadInterval(ReadInterval);

			var data = WaitForDataEntry();

			AssertDataValueEquals(data, "V", ReadInterval);

			var sensorDoesTurnOff = ReadInterval > DelayAfterTurningLightPRSensorOn;

			if (sensorDoesTurnOff)
			{
				Console.WriteLine("The soil moisture sensor should turn off when not in use.");

				TestLightPRSensorPowerTurnsOnAndOff();
			}
			else
			{
				Console.WriteLine("The soil moisture sensor should stay on permanently.");

				TestLightPRSensorPowerStaysOn();
			}
		}

		public void TestLightPRSensorPowerStaysOn()
		{
			WriteParagraphTitleText("Waiting until the soil moisture sensor is on before starting the test...");

			WaitUntilLightPRSensorPowerPinIs(On);

			var durationInSeconds = ReadInterval * 5;

			WriteParagraphTitleText("Checking that soil moisture sensor power pin stays on...");

			AssertLightPRSensorPowerPinForDuration(On, durationInSeconds);
		}

		public void TestLightPRSensorPowerTurnsOnAndOff()
		{
			WriteParagraphTitleText("Waiting until the soil moisture sensor has turned on then off before starting the test...");

			WaitUntilLightPRSensorPowerPinIs(On);
			WaitUntilLightPRSensorPowerPinIs(Off);
			WaitUntilLightPRSensorPowerPinIs(On);

			CheckLightPRSensorOnDuration();
			CheckLightPRSensorOffDuration();
		}

		public void CheckLightPRSensorOnDuration()
		{
			WriteParagraphTitleText("Getting the total on time...");

			var totalOnTime = WaitWhileLightPRSensorPowerPinIs(On);

			WriteParagraphTitleText("Checking the total on time is correct...");

			var expectedOnTime = DelayAfterTurningLightPRSensorOn;

			AssertIsWithinRange("total on time", expectedOnTime, totalOnTime, TimeErrorMargin);
		}

		public void CheckLightPRSensorOffDuration()
		{
			WriteParagraphTitleText("Getting the total off time...");

			var totalOffTime = WaitWhileLightPRSensorPowerPinIs(Off);

			WriteParagraphTitleText("Checking the total off time is correct...");

			var expectedOffTime = ReadInterval - DelayAfterTurningLightPRSensorOn;

			AssertIsWithinRange("total off time", expectedOffTime, totalOffTime, TimeErrorMargin);
		}
	}
}
