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
	public class LightBurstOffTimeEEPROMTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightBurstOffTime_1sec()
		{
			using (var helper = new LightBurstOffTimeEEPROMTestHelper())
			{
				helper.LightBurstOffTime = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOffTimeEEPROM();
			}
		}

		[Test]
		public void Test_SetLightBurstOffTime_5sec()
		{
			using (var helper = new LightBurstOffTimeEEPROMTestHelper())
			{
				helper.LightBurstOffTime = 5;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOffTimeEEPROM();
			}
		}

	}
}
