using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class ReadIntervalCommandTestHelper : SerialCommandTestHelper
  {
    public int ReadingInterval = 1;

    public ReadIntervalCommandTestHelper ()
    {
    }

    public void TestSetReadIntervalCommand ()
    {
      Key = "I";
      Label = "reading interval";
      Value = ReadingInterval.ToString ();

      TestCommand ();
    }
  }
}
