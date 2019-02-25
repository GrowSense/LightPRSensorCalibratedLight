using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class StopMinuteCommandTestHelper : GreenSenseIlluminatorHardwareTestHelper
    {
        public int StopMinute = 30;

        public void TestStopMinuteCommand ()
        {
            WriteTitleText ("Start stop minute command test");

            Console.WriteLine ("Stop minute: " + StopMinute);
            Console.WriteLine ("");

            ConnectDevices (false);

            SetDeviceReadInterval (1);

            SendStopMinuteCommand ();
        }

        public void SendStopMinuteCommand ()
        {
            var command = "H" + StopMinute;

            WriteParagraphTitleText ("Sending command...");

            SendDeviceCommand (command);

            var dataEntry = WaitForDataEntry ();

            WriteParagraphTitleText ("Checking threshold value...");

            AssertDataValueEquals (dataEntry, "H", StopMinute);
        }
    }
}
