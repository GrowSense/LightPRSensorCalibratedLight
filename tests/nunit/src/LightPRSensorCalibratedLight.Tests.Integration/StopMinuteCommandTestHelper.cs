using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StopMinuteCommandTestHelper : SerialCommandTestHelper
    {
        public int StopMinute = 30;

        public void TestStopMinuteCommand ()
        {
            Letter = "H";
            Label = "stop minute";
            Value = StopMinute;

            TestCommand ();
        }
    }
}
