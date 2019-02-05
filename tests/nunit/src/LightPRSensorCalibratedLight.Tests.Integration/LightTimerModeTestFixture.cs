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
	public class LightTimerModeTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightTimer_LightNeeded()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.LightMode = LightMode.Timer;
				helper.Timer = 50;
				helper.SimulatedLightPercentage = 55;
				helper.LightIsExpected = true;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightTimer_LightNotNeeded()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.LightMode = LightMode.Timer;
				helper.Timer = 50;
				helper.SimulatedLightPercentage = 45;
				helper.LightIsExpected = false;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}
	}
}
