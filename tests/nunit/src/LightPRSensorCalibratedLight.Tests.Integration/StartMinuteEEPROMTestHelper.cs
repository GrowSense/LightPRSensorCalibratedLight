using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class StartMinuteEEPROMTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public int StartMinute = 0;

		public void TestStartMinuteEEPROM()
		{
			WriteTitleText("Starting start minute EEPROM test");

			Console.WriteLine("Start minute: " + StartMinute);
			Console.WriteLine("");

			ConnectDevices();

			ResetDeviceSettings ();

			SetDeviceReadInterval (1);

			SendStartMinuteCommand();

			ResetDeviceViaPin ();

			// Skip the next entry in case it isn't up to date
			WaitForDataEntry ();
			WaitForDataEntry ();

			// Grab the next entry
			var dataEntry = WaitForDataEntry ();

			AssertDataValueEquals(dataEntry, "F", StartMinute);
		}

		public void SendStartMinuteCommand()
		{
			var command = "F" + StartMinute;

			WriteParagraphTitleText("Sending command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking value...");

			AssertDataValueEquals(dataEntry, "F", StartMinute);
		}
	}
}
