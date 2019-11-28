using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class StartMinuteCommandTestHelper : SerialCommandTestHelper
  {
    public int StartMinute = 30;

    public void TestStartMinuteCommand ()
    {
      Key = "F";
      Label = "start minute";
      Value = StartMinute.ToString ();

      TestCommand ();
    }
  }
}
