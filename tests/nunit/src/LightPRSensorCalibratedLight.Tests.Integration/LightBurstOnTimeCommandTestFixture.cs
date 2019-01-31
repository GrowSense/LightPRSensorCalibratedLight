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
	public class LightBurstOnTimeCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightBurstOnTime_1Seconds()
		{
			using (var helper = new LightBurstOnTimeCommandTestHelper())
			{
				helper.LightBurstOnTime = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOnTimeCommand();
			}
		}

		[Test]
		public void Test_SetLightBurstOnTime_5Seconds()
		{
			using (var helper = new LightBurstOnTimeCommandTestHelper())
			{
				helper.LightBurstOnTime = 5;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOnTimeCommand();
			}
		}
	}
}
