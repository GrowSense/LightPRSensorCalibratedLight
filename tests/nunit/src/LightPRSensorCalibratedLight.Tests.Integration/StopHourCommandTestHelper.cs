using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class StopHourCommandTestHelper : SerialCommandTestHelper
  {
    public int StopHour = 18;

    public void TestStopHourCommand ()
    {
      Key = "G";
      Label = "stop hour";
      Value = StopHour.ToString ();

      TestCommand ();
    }
  }
}
