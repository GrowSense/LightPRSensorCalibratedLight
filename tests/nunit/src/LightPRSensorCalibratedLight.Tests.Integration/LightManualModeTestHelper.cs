using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class LightManualModeTestHelper : GrowSenseIlluminatorHardwareTestHelper
    {
        public LightMode LightMode = LightMode.On;

        public void TestLight ()
        {
            WriteTitleText ("Starting light test");

            Console.WriteLine ("Light command: " + LightMode);
            Console.WriteLine ("");

            ConnectDevices ();

            var cmd = "M" + (int)LightMode;

            SendDeviceCommand (cmd);

            var data = WaitForData (3);

            CheckDataValues (data [data.Length - 1]);
        }

        public void CheckDataValues (Dictionary<string, string> dataEntry)
        {
            AssertDataValueEquals (dataEntry, "M", (int)LightMode);

            switch (LightMode) {
            case LightMode.Off:
                CheckLightIsOff ();
                break;
            case LightMode.On:
                CheckLightIsOn ();
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
    }
}