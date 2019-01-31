using System;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class LightBurstOnTimeCommandTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public int LightBurstOnTime = 1;

		public void TestLightBurstOnTimeCommand()
		{
			WriteTitleText("Starting light burst on time command test");

			Console.WriteLine("Light burst on time: " + LightBurstOnTime);
			Console.WriteLine("");

			ConnectDevices(false);

			var cmd = "B" + LightBurstOnTime;

			SendDeviceCommand(cmd);

			var dataEntry = WaitForDataEntry();

			AssertDataValueEquals(dataEntry, "B", LightBurstOnTime);
		}
	}
}