using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightBurstOffTimeCommandTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public int LightBurstOffTime = 1;

		public void TestLightBurstOffTimeCommand()
		{
			WriteTitleText("Starting light burst off time command test");

			Console.WriteLine("Light burst off time: " + LightBurstOffTime);
			Console.WriteLine("");

			ConnectDevices(false);

			var cmd = "O" + LightBurstOffTime;

			SendDeviceCommand(cmd);

			var dataEntry = WaitForDataEntry();

			AssertDataValueEquals(dataEntry, "O", LightBurstOffTime);
		}
	}
}