using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class StartMinuteCommandTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public int StartMinute = 30;

		public void TestStartMinuteCommand()
		{
			WriteTitleText("Starting start minute command test");

			Console.WriteLine("StartMinute: " + StartMinute + "%");
			Console.WriteLine("");

			ConnectDevices(false);

			SetDeviceReadInterval (1);

			SendStartMinuteCommand();
		}

		public void SendStartMinuteCommand()
		{
			var command = "F" + StartMinute;

			WriteParagraphTitleText("Sending command...");

			SendDeviceCommand(command);

			var dataEntry = WaitForDataEntry();

			WriteParagraphTitleText("Checking threshold value...");

			AssertDataValueEquals(dataEntry, "F", StartMinute);
		}
	}
}
