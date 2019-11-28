using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class ClockCommandTestHelper : SerialCommandTestHelper
  {
    public DateTime ClockSetting = DateTime.MinValue;

    public ClockCommandTestHelper ()
    {
    }

    public void TestClockCommand ()
    {
      Key = "C";
      Label = "clock";
      Value = ClockSetting.ToString ("MMM dd yyyy hh:mm:ss");
      ExpectedValue = ClockSetting.ToString ("MM/dd/yyyy hh:mm:ss");
      
      // TODO: See if this can be removed. Causes test to fail if it's true because the clock time increases by a few seconds changing the value
      ValueIsSavedAfterReset = false;
      
      SeparateKeyValueWithColon = true;

      TestCommand ();
    }
  }
}

