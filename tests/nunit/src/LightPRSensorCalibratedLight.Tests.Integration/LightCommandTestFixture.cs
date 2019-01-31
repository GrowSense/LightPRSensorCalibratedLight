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
	public class LightCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightToOn()
		{
			using (var helper = new LightCommandTestHelper())
			{
				helper.LightCommand = LightStatus.On;

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
			using (var helper = new LightCommandTestHelper())
			{
				helper.LightCommand = LightStatus.Off;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}

		[Test]
		public void Test_SetLightToAuto()
		{
			using (var helper = new LightCommandTestHelper())
			{
				helper.LightCommand = LightStatus.Auto;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightCommand();
			}
		}
	}
}
