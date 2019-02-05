using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class StopHourEEPROMTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public int StopHour = 6;

		public void TestStopHourEEPROM()
		{
			WriteTitleText("Starting stop hour EEPROM test");

			Console.WriteLine("Stop hour: " + StopHour);
			Console.WriteLine("");

			ConnectDevices();

			ResetDeviceSettings ();

			SetDeviceReadInterval (1);

			SendStopHourCommand();

			ResetDeviceViaPin ();

			// Skip the next entry in case it isn't up to date
			WaitForDataEntry ();
			WaitForDataEntry ();

			// Grab the next entry
			var dataEntry = WaitForDataEntry ();

			AssertDataValueEquals(dataEntry, "G", StopHour);
		}

		public void SendStopHourCommand()
		{
			var command = "G" + StopHour;

			WriteParagraphTitleText("Sending command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking value...");

			AssertDataValueEquals(dataEntry, "G", StopHour);
		}
	}
}
