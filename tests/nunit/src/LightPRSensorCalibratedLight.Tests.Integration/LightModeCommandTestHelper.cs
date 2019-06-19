using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class LightModeCommandTestHelper : SerialCommandTestHelper
    {
        public LightMode LightCommand = LightMode.On;

        public void TestLightCommand ()
        {
            Letter = "m";
            Label = "light mode";
            Value = (int)LightCommand;

            TestCommand ();
        }
    }
}