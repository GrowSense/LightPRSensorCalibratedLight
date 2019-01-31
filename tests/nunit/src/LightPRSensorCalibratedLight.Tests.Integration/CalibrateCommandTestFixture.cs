using NUnit.Framework;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	[TestFixture(Category = "Integration")]
	public class CalibrateCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_CalibrateDarkToCurrentLightValueCommand_20Percent()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.SimulatedLightPercentage = 20;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateDarkToCurrentLightValueCommand_30Percent()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.SimulatedLightPercentage = 30;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateDarkToSpecifiedValueCommand_200()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.RawLightValue = 200;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateDarkToSpecifiedValueCommand_220()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.RawLightValue = 220;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateBrightToCurrentLightValueCommand_80Percent()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.SimulatedLightPercentage = 80;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateBrightToCurrentLightValueCommand_90Percent()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.SimulatedLightPercentage = 90;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateBrightToSpecifiedValueCommand_880()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.RawLightValue = 880;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}

		[Test]
		public void Test_CalibrateBrightToSpecifiedValueCommand_900()
		{
			using (var helper = new CalibrateCommandTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.RawLightValue = 900;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrateCommand();
			}
		}
	}
}
