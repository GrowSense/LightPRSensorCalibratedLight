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
	public class StartHourCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetStartHour_4()
		{
			using (var helper = new StartHourCommandTestHelper())
			{
				helper.StartHour = 4;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStartHourCommand();
			}
		}

		[Test]
		public void Test_SetStartHour_8()
		{
			using (var helper = new StartHourCommandTestHelper())
			{
				helper.StartHour = 8;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStartHourCommand();
			}
		}
	}
}
