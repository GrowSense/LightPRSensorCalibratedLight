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
	public class LightTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightOn()
		{
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.On;

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
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.Off;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightAuto_WaterNeeded_Burst1Off0()
		{
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.Auto;
				helper.SimulatedLightPercentage = 10;
				helper.BurstOnTime = 1;
				helper.BurstOffTime = 0;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightAuto_WaterNeeded_Burst1Off1()
		{
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.Auto;
				helper.SimulatedLightPercentage = 10;
				helper.BurstOnTime = 1;
				helper.BurstOffTime = 1;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightAuto_WaterNeeded_Burst1Off2()
		{
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.Auto;
				helper.SimulatedLightPercentage = 10;
				helper.BurstOnTime = 1;
				helper.BurstOffTime = 2;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightAuto_WaterNotNeeded()
		{
			using (var helper = new LightTestHelper())
			{
				helper.LightCommand = LightStatus.Auto;
				helper.SimulatedLightPercentage = 80;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}
	}
}
