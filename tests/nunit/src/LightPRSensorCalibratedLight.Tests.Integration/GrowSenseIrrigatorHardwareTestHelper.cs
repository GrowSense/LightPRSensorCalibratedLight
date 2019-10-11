using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
    public class GrowSenseIlluminatorHardwareTestHelper : GrowSenseHardwareTestHelper
    {
        public int SimulatorLightPin = 2;

        public GrowSenseIlluminatorHardwareTestHelper ()
        {
        }

        public override void PrepareDeviceForTest (bool consoleWriteDeviceOutput)
        {
            base.PrepareDeviceForTest (false);

            if (consoleWriteDeviceOutput)
                ReadFromDeviceAndOutputToConsole ();
        }

        public void SendClockCommand ()
        {
            SendClockCommand (DateTime.Now);
        }

        public void SendClockCommand (DateTime dateTime)
        {
            //var cmd = "C" + dateTime.ToString ("dd/MM/yyyy HH:mm:ss");
            var cmd = "C" + dateTime.ToString ("MMM dd yyyy-HH:mm:ss");

            WriteToDevice (cmd);

            WaitForMessageReceived (cmd);
        }

    }
}
