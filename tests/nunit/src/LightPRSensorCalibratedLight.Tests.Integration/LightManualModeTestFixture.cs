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
	public class LightManualModeTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightOn()
		{
			using (var helper = new LightManualModeTestHelper())
			{
				helper.LightMode = LightMode.On;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightOff()
		{
			using (var helper = new LightManualModeTestHelper())
			{
				helper.LightMode = LightMode.Off;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}
	}
}
