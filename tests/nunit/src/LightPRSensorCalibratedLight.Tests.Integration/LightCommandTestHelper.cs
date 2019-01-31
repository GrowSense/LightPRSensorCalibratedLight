using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightCommandTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public LightStatus LightCommand = LightStatus.Auto;

		public void TestLightCommand()
		{
			WriteTitleText("Starting light command test");

			Console.WriteLine("Light command: " + LightCommand);
			Console.WriteLine("");

			ConnectDevices(false);

			var cmd = "P" + (int)LightCommand;

			SendDeviceCommand(cmd);

			var dataEntry = WaitForDataEntry();
			dataEntry = WaitForDataEntry();
			AssertDataValueEquals(dataEntry, "P", (int)LightCommand);
		}
	}
}