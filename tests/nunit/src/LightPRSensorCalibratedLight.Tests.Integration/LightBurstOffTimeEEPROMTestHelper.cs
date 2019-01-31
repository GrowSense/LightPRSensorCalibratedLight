using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightBurstOffTimeEEPROMTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public int LightBurstOffTime = 3;

		public void TestLightBurstOffTimeEEPROM()
		{
			WriteTitleText("Starting light burst off time EEPROM test");

			Console.WriteLine("Light burst off time: " + LightBurstOffTime + "%");
			Console.WriteLine("");

			ConnectDevices();

			ResetDeviceSettings ();

			SendLightBurstOffTimeCommand();

			ResetDeviceViaPin ();

			var dataEntry = WaitForDataEntry ();

			AssertDataValueEquals(dataEntry, "O", LightBurstOffTime);
		}

		public void SendLightBurstOffTimeCommand()
		{
			var command = "O" + LightBurstOffTime;

			WriteParagraphTitleText("Sending light burst off time command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking light burst off time value...");

			AssertDataValueEquals(dataEntry, "O", LightBurstOffTime);
		}
	}
}
