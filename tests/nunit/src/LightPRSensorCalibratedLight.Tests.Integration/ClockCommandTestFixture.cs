using System;
using NUnit.Framework;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  [TestFixture(Category="Integration")]
  public class ClockCommandTestFixture : BaseTestFixture
  {
    [Test]
    public void Test_SetReadIntervalCommand_6_30 ()
    {
      using (var helper = new ClockCommandTestHelper()) {
        var now = DateTime.Now;
        helper.ClockSetting = new DateTime (now.Year, now.Month, now.Day + 1, 6, 30, 0);

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestClockCommand ();
      }
    }

    [Test]
    public void Test_SetReadIntervalCommand_18_20 ()
    {
      using (var helper = new ClockCommandTestHelper()) {
        var now = DateTime.Now;
        helper.ClockSetting = new DateTime (now.Year, now.Month, now.Day + 1, 18, 20, 0);

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestClockCommand ();
      }
    }
  }
}

