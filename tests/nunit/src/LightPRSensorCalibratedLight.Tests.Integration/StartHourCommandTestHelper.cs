using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class StartHourCommandTestHelper : SerialCommandTestHelper
  {
    public int StartHour = 30;

    public void TestStartHourCommand ()
    {
      Key = "E";
      Label = "start hour";
      Value = StartHour.ToString ();

      TestCommand ();
    }
  }
}
