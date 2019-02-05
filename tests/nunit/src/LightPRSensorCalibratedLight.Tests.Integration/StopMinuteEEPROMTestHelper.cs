using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class StopMinuteEEPROMTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public int StopMinute = 00;

		public void TestStopMinuteEEPROM()
		{
			WriteTitleText("Starting stop minute EEPROM test");

			Console.WriteLine("Stop minute: " + StopMinute);
			Console.WriteLine("");

			ConnectDevices();

			ResetDeviceSettings ();

			SetDeviceReadInterval (1);

			SendStopMinuteCommand();

			ResetDeviceViaPin ();

			// Skip the next entry in case it isn't up to date
			WaitForDataEntry ();
			WaitForDataEntry ();

			// Grab the next entry
			var dataEntry = WaitForDataEntry ();

			AssertDataValueEquals(dataEntry, "H", StopMinute);
		}

		public void SendStopMinuteCommand()
		{
			var command = "H" + StopMinute;

			WriteParagraphTitleText("Sending command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking value...");

			AssertDataValueEquals(dataEntry, "H", StopMinute);
		}
	}
}
