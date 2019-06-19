using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StartHourCommandTestHelper : SerialCommandTestHelper
    {
        public int StartHour = 30;

        public void TestStartHourCommand ()
        {
            Letter = "E";
            Label = "start hour";
            Value = StartHour;

            TestCommand ();
        }
    }
}
