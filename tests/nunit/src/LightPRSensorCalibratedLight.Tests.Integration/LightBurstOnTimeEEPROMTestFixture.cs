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
	public class LightBurstOnTimeEEPROMTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightBurstOnTime_1sec()
		{
			using (var helper = new LightBurstOnTimeEEPROMTestHelper())
			{
				helper.LightBurstOnTime = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOnTimeEEPROM();
			}
		}

		[Test]
		public void Test_SetLightBurstOnTime_5sec()
		{
			using (var helper = new LightBurstOnTimeEEPROMTestHelper())
			{
				helper.LightBurstOnTime = 5;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOnTimeEEPROM();
			}
		}

	}
}
