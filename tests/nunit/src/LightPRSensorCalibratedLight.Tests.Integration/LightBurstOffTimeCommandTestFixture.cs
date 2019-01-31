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
	public class LightBurstOffTimeCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_SetLightBurstOffTime_0Seconds()
		{
			using (var helper = new LightBurstOffTimeCommandTestHelper())
			{
				helper.LightBurstOffTime = 0;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOffTimeCommand();
			}
		}

		[Test]
		public void Test_SetLightBurstOffTime_1Seconds()
		{
			using (var helper = new LightBurstOffTimeCommandTestHelper())
			{
				helper.LightBurstOffTime = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOffTimeCommand();
			}
		}

		[Test]
		public void Test_SetLightBurstOffTime_5Seconds()
		{
			using (var helper = new LightBurstOffTimeCommandTestHelper())
			{
				helper.LightBurstOffTime = 5;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLightBurstOffTimeCommand();
			}
		}
	}
}
