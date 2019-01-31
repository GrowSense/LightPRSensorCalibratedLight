using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightBurstOnTimeEEPROMTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public int LightBurstOnTime = 3;

		public void TestLightBurstOnTimeEEPROM()
		{
			WriteTitleText("Starting light burst on time EEPROM test");

			Console.WriteLine("Light burst on time: " + LightBurstOnTime + "%");
			Console.WriteLine("");

			ConnectDevices();

			ResetDeviceSettings ();

			SendLightBurstOnTimeCommand();

			ResetDeviceViaPin ();

			var dataEntry = WaitForDataEntry ();

			AssertDataValueEquals(dataEntry, "B", LightBurstOnTime);
		}

		public void SendLightBurstOnTimeCommand()
		{
			var command = "B" + LightBurstOnTime;

			WriteParagraphTitleText("Sending light burst on time command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking light burst on time value...");

			AssertDataValueEquals(dataEntry, "B", LightBurstOnTime);
		}
	}
}
