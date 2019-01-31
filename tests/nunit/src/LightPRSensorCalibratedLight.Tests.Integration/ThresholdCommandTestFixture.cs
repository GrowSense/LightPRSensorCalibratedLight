using System;
using NUnit.Framework;
using duinocom;
using System.Threading;
using ArduinoSerialControllerClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Ports;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
	[TestFixture(Category = "Integration")]
	public class ThresholdCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetThresholdToSpecifiedValueCommand_15Percent()
		{
			using (var helper = new ThresholdCommandTestHelper())
			{
				helper.Threshold = 15;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestThresholdCommand();
			}
		}

		[Test]
		public void Test_SetThresholdToSpecifiedValueCommand_25Percent()
		{
			using (var helper = new ThresholdCommandTestHelper())
			{
				helper.Threshold = 25;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestThresholdCommand();
			}
		}

		[Test]
		public void Test_SetThresholdToCurrentLightValueCommand_15Percent()
		{
			using (var helper = new ThresholdCommandTestHelper())
			{
				var value = 15;

				helper.SimulatedLightPercentage = value;
				helper.Threshold = value;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestThresholdCommand();
			}
		}

		[Test]
		public void Test_SetThresholdToCurrentLightValueCommand_25Percent()
		{
			using (var helper = new ThresholdCommandTestHelper())
			{
				var value = 25;

				helper.SimulatedLightPercentage = value;
				helper.Threshold = value;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestThresholdCommand();
			}
		}
	}
}
