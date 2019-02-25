using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StopHourCommandTestHelper : GreenSenseIlluminatorHardwareTestHelper
    {
        public int StopHour = 30;

        public void TestStopHourCommand ()
        {
            WriteTitleText ("Stoping stop hour command test");

            Console.WriteLine ("Stop hour: " + StopHour);
            Console.WriteLine ("");

            ConnectDevices (false);

            SetDeviceReadInterval (1);

            SendStopHourCommand ();
        }

        public void SendStopHourCommand ()
        {
            var command = "G" + StopHour;

            WriteParagraphTitleText ("Sending command...");

            SendDeviceCommand (command);

            var dataEntry = WaitForDataEntry ();

            WriteParagraphTitleText ("Checking threshold value...");

            AssertDataValueEquals (dataEntry, "G", StopHour);
        }
    }
}
