using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StartHourEEPROMTestHelper : GreenSenseIlluminatorHardwareTestHelper
    {
        public int StartHour = 6;

        public void TestStartHourEEPROM ()
        {
            WriteTitleText ("Starting start hour EEPROM test");

            Console.WriteLine ("Start hour: " + StartHour);
            Console.WriteLine ("");

            ConnectDevices ();

            ResetDeviceSettings ();

            SetDeviceReadInterval (1);

            SendStartHourCommand ();

            ResetDeviceViaPin ();

            // Skip the next entry in case it isn't up to date
            WaitForDataEntry ();
            WaitForDataEntry ();

            // Grab the next entry
            var dataEntry = WaitForDataEntry ();

            AssertDataValueEquals (dataEntry, "E", StartHour);
        }

        public void SendStartHourCommand ()
        {
            var command = "E" + StartHour;

            WriteParagraphTitleText ("Sending command...");

            SendDeviceCommand (command);

            var dataEntry = WaitForDataEntry ();

            WriteParagraphTitleText ("Checking value...");

            AssertDataValueEquals (dataEntry, "E", StartHour);
        }
    }
}
