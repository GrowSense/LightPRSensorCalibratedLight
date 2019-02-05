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
	public class LightModeCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightToOn()
		{
			using (var helper = new LightModeCommandTestHelper())
			{
				helper.LightCommand = LightMode.On;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}

		[Test]
		public void Test_SetLightToOff()
		{
			using (var helper = new LightModeCommandTestHelper())
			{
				helper.LightCommand = LightMode.Off;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}

		[Test]
		public void Test_SetLightToAboveThreshold()
		{
			using (var helper = new LightModeCommandTestHelper())
			{
				helper.LightCommand = LightMode.AboveThreshold;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}

		[Test]
		public void Test_SetLightToBelowThreshold()
		{
			using (var helper = new LightModeCommandTestHelper())
			{
				helper.LightCommand = LightMode.BelowThreshold;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}
	}
}
