using System;
using NUnit.Framework;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	[TestFixture(Category = "Integration")]
	public class LightPRSensorPowerTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightPRSensorPower_AlwaysOn_1SecondReadInterval()
		{
			using (var helper = new LightPRSensorPowerTestHelper())
			{
				helper.ReadInterval = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightPRSensorPower();
			}
		}

		[Test]
		public void Test_LightPRSensorPower_AlwaysOn_3SecondReadInterval()
		{
			using (var helper = new LightPRSensorPowerTestHelper())
			{
				helper.ReadInterval = 3;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightPRSensorPower();
			}
		}

		[Test]
		public void Test_LightPRSensorPower_OnAndOff_4SecondReadInterval()
		{
			using (var helper = new LightPRSensorPowerTestHelper())
			{
				helper.ReadInterval = 4;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightPRSensorPower();
			}
		}
		[Test]
		public void Test_LightPRSensorPower_OnAndOff_6SecondReadInterval()
		{
			using (var helper = new LightPRSensorPowerTestHelper())
			{
				helper.ReadInterval = 6;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightPRSensorPower();
			}
		}
	}
}
