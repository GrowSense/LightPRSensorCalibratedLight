using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StopHourCommandTestHelper : SerialCommandTestHelper
    {
        public int StopHour = 18;

        public void TestStopHourCommand ()
        {
            Letter = "G";
            Label = "stop hour";
            Value = StopHour;

            TestCommand ();
        }
    }
}
