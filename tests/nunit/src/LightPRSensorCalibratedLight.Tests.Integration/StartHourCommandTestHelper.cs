using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class StartHourCommandTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public int StartHour = 30;

		public void TestStartHourCommand()
		{
			WriteTitleText("Starting start hour command test");

			Console.WriteLine("StartHour: " + StartHour + "%");
			Console.WriteLine("");

			ConnectDevices(false);

			SetDeviceReadInterval (1);

			SendStartHourCommand();
		}

		public void SendStartHourCommand()
		{
			var command = "E" + StartHour;

			WriteParagraphTitleText("Sending command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking threshold value...");

			AssertDataValueEquals(dataEntry, "E", StartHour);
		}
	}
}
