using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightModeCommandTestHelper : GreenSenseIlluminatorHardwareTestHelper
	{
		public LightMode LightCommand = LightMode.On;

		public void TestLightCommand()
		{
			WriteTitleText("Starting light command test");

			Console.WriteLine("Light mode command: " + LightCommand);
			Console.WriteLine("");

			ConnectDevices(false);

			var cmd = "M" + (int)LightCommand;

			SendDeviceCommand(cmd);

			var dataEntry = WaitForDataEntry();
			dataEntry = WaitForDataEntry();
			AssertDataValueEquals(dataEntry, "M", (int)LightCommand);
		}
	}
}