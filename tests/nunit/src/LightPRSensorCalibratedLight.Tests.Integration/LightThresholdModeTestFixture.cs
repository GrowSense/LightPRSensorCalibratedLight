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
	public class LightThresholdModeTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightAboveThreshold_LightNeeded()
		{
			using (var helper = new LightThresholdModeTestHelper())
			{
				helper.LightMode = LightMode.AboveThreshold;
				helper.Threshold = 50;
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
		public void Test_LightAboveThreshold_LightNotNeeded()
		{
			using (var helper = new LightThresholdModeTestHelper())
			{
				helper.LightMode = LightMode.AboveThreshold;
				helper.Threshold = 50;
				helper.SimulatedLightPercentage = 40;
				helper.LightIsExpected = false;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightBelowThreshold_LightNeeded()
		{
			using (var helper = new LightThresholdModeTestHelper())
			{
				helper.LightMode = LightMode.BelowThreshold;
				helper.Threshold = 50;
				helper.SimulatedLightPercentage = 40;
				helper.LightIsExpected = true;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightBelowThreshold_LightNotNeeded()
		{
			using (var helper = new LightThresholdModeTestHelper())
			{
				helper.LightMode = LightMode.BelowThreshold;
				helper.Threshold = 50;
				helper.SimulatedLightPercentage = 60;
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
