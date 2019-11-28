using System;
using System.Threading;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class CalibrateCommandTestHelper : SerialCommandTestHelper
  {
    public int RawLightValue = 0;

    public void TestCalibrateCommand ()
    {
      Value = RawLightValue.ToString ();

      TestCommand ();
    }
  }
}