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
	public class StopHourEEPROMTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetStopHour_16()
		{
			using (var helper = new StopHourEEPROMTestHelper())
			{
				helper.StopHour = 16;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStopHourEEPROM();
			}
		}

		[Test]
		public void Test_SetStopHour_18()
		{
			using (var helper = new StopHourEEPROMTestHelper())
			{
				helper.StopHour = 18;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStopHourEEPROM();
			}
		}

	}
}
