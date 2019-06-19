using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class ThresholdCommandTestHelper : SerialCommandTestHelper
    {
        public int Threshold = 30;

        public void TestThresholdCommand ()
        {
            Letter = "T";
            Label = "light threshold";
            Value = Threshold;

            TestCommand ();
        }
    }
}
