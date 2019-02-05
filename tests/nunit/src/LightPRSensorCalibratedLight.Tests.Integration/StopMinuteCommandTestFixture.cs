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
	public class StopMinuteCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetStopMinute_10()
		{
			using (var helper = new StopMinuteCommandTestHelper())
			{
				helper.StopMinute = 10;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStopMinuteCommand();
			}
		}

		[Test]
		public void Test_SetStopMinute_30()
		{
			using (var helper = new StopMinuteCommandTestHelper())
			{
				helper.StopMinute = 30;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStopMinuteCommand();
			}
		}
	}
}
