using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class GreenSenseIrrigatorHardwareTestHelper : GreenSenseHardwareTestHelper
	{
		public int SimulatorLightPin = 2;

		public GreenSenseIrrigatorHardwareTestHelper()
		{
		}

		public override void PrepareDeviceForTest(bool consoleWriteDeviceOutput)
		{
			base.PrepareDeviceForTest(false);

			SetDeviceLightOffTime(0);

			if (consoleWriteDeviceOutput)
				ReadFromDeviceAndOutputToConsole();
		}

		public void SetDeviceLightOffTime(int lightOffTime)
		{
			var cmd = "O" + lightOffTime;

			Console.WriteLine("");
			Console.WriteLine("Setting light off time to " + lightOffTime + " seconds...");
			Console.WriteLine("  Sending '" + cmd + "' command to device");
			Console.WriteLine("");

			SendDeviceCommand(cmd);
		}
	}
}
