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
	public class StartMinuteCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetStartMinute_10()
		{
			using (var helper = new StartMinuteCommandTestHelper())
			{
				helper.StartMinute = 10;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStartMinuteCommand();
			}
		}

		[Test]
		public void Test_SetStartMinute_30()
		{
			using (var helper = new StartMinuteCommandTestHelper())
			{
				helper.StartMinute = 30;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestStartMinuteCommand();
			}
		}
	}
}
