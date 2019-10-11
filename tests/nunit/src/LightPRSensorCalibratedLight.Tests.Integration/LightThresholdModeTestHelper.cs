using System;
using System.Collections.Generic;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class LightThresholdModeTestHelper : GrowSenseIlluminatorHardwareTestHelper
    {
        public LightMode LightMode = LightMode.AboveThreshold;
        public int SimulatedLightPercentage = 60;
        public int Threshold = 50;
        public int DurationToCheckLight = 5;
        public bool LightIsExpected = false;

        public void TestLight ()
        {
            WriteTitleText ("Starting light threshold mode test");

            Console.WriteLine ("Light command: " + LightMode);
            Console.WriteLine ("Simulated light: " + SimulatedLightPercentage + "%");
            Console.WriteLine ("");

            ConnectDevices ();

            var cmd = "M" + (int)LightMode;

            SendDeviceCommand (cmd);

            SendDeviceCommand ("T" + Threshold);

            SimulateLight (SimulatedLightPercentage);

            var data = WaitForData (3);

            CheckDataValues (data [data.Length - 1]);
        }

        public void CheckDataValues (Dictionary<string, string> dataEntry)
        {
            AssertDataValueEquals (dataEntry, "M", (int)LightMode);
            AssertDataValueEquals (dataEntry, "T", Threshold);

            // TODO: Check LO value matches the light

            AssertDataValueIsWithinRange (dataEntry, "L", SimulatedLightPercentage, CalibratedValueMarginOfError);

            switch (LightMode) {
            case LightMode.AboveThreshold:
                CheckLightIsAccurate ();
                break;
            case LightMode.BelowThreshold:
                CheckLightIsAccurate ();
                break;
            default:
                throw new Exception ("Test does not support light mode: " + LightMode);
            }

        }

        public void CheckLightIsOff ()
        {
            AssertSimulatorPin ("light", SimulatorLightPin, false);
        }

        public void CheckLightIsOn ()
        {
            AssertSimulatorPin ("light", SimulatorLightPin, true);
        }

        public void CheckLightIsAccurate ()
        {
            if (LightIsExpected) {
                CheckLightIsOn ();
            } else {
                CheckLightIsOff ();
            }
        }
    }
}

