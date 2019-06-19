using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class LightModeCommandTestHelper : SerialCommandTestHelper
    {
        public LightMode LightCommand = LightMode.On;

        public void TestLightCommand ()
        {
            Letter = "M";
            Label = "light mode";
            Value = (int)LightCommand;
            ValueIsSavedInEEPROM = false; // TODO: Save light mode in EEPROM in sketch then change this to true

            TestCommand ();
        }
    }
}
