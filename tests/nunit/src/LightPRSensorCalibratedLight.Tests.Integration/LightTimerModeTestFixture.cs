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
	public class LightTimerModeTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_LightTimer_LightNotNeeded_JustBeforeTurningOn()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.Mode = LightMode.Timer;

				helper.DeviceTime = GetTime (6, 28);

				helper.StartHour = 6;
				helper.StartMinute = 30;

				helper.StopHour = 18;
				helper.StopMinute = 30;

				helper.LightIsExpected = false;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightTimer_LightNeeded_JustTurnedOn()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.Mode = LightMode.Timer;

				helper.DeviceTime = GetTime (6, 31);

				helper.StartHour = 6;
				helper.StartMinute = 30;

				helper.StopHour = 18;
				helper.StopMinute = 30;

				helper.LightIsExpected = true;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightTimer_LightNeeded_JustBeforeTurningOff()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.Mode = LightMode.Timer;

				helper.DeviceTime = GetTime (18, 29);

				helper.StartHour = 6;
				helper.StartMinute = 30;

				helper.StopHour = 18;
				helper.StopMinute = 30;

				helper.LightIsExpected = true;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		[Test]
		public void Test_LightTimer_LightNotNeeded_JustTurnedOff()
		{
			using (var helper = new LightTimerModeTestHelper())
			{
				helper.Mode = LightMode.Timer;

				helper.DeviceTime = GetTime (18, 31);

				helper.StartHour = 6;
				helper.StartMinute = 30;

				helper.StopHour = 18;
				helper.StopMinute = 30;

				helper.LightIsExpected = false;

				helper.DevicePort = GetDevicePort();
				helper.DeviceBaudRate = GetDeviceSerialBaudRate();

				helper.SimulatorPort = GetSimulatorPort();
				helper.SimulatorBaudRate = GetSimulatorSerialBaudRate();

				helper.TestLight();
			}
		}

		public DateTime GetTime(int hour, int minute)
		{
			var now = DateTime.Now;
			var customTime = new DateTime (now.Year, now.Month, now.Day, hour, minute, 0);

			return customTime;
		}
	}
}
