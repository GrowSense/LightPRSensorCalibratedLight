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
	public class CalibrationEEPROMTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_CalibrateDarkEEPROM_10()
		{
			using (var helper = new CalibrationEEPROMTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.RawLightValue = 10;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrationEEPROM();
			}
		}

		[Test]
		public void Test_CalibrateDarkEEPROM_200()
		{
			using (var helper = new CalibrationEEPROMTestHelper())
			{
				helper.Label = "dark";
				helper.Letter = "D";
				helper.RawLightValue = 200;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrationEEPROM();
			}
		}

		[Test]
		public void Test_CalibrateBrightEEPROM_950()
		{
			using (var helper = new CalibrationEEPROMTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.RawLightValue = 950;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrationEEPROM();
			}
		}

		[Test]
		public void Test_CalibrateBrightEEPROM_1020()
		{
			using (var helper = new CalibrationEEPROMTestHelper())
			{
				helper.Label = "bright";
				helper.Letter = "W";
				helper.RawLightValue = 1020;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestCalibrationEEPROM();
			}
		}
	}
}
