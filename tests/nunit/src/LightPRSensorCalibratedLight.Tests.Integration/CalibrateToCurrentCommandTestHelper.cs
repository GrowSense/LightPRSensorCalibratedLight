using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class CalibrateToCurrentCommandTestHelper : GrowSenseHardwareTestHelper
  {
    public string Label;
    public string Letter;
    public int SimulatedLightPercentage = -1;
    public int RawLightValue = 0;

    public CalibrateToCurrentCommandTestHelper ()
    {
    }

    public void TestCalibrateCommand ()
    {
      WriteTitleText ("Starting calibrate " + Label + " command test");


      Console.WriteLine ("Simulated light: " + SimulatedLightPercentage + "%");

      if (RawLightValue == 0)
        RawLightValue = SimulatedLightPercentage * AnalogPinMaxValue / 100;

      Console.WriteLine ("Raw light value: " + RawLightValue);
      Console.WriteLine ("");

      var simulatorIsNeeded = SimulatedLightPercentage > -1;

      ConnectDevices (simulatorIsNeeded);

      if (SimulatorIsEnabled) {
        SimulateLight (SimulatedLightPercentage);

        // Skip the first X entries to give the value time to stabilise
        WaitForData (1);

        var dataEntry = WaitForDataEntry ();

        AssertDataValueIsWithinRange (dataEntry, "R", RawLightValue, RawValueMarginOfError);
      }

      SendCalibrationCommand ();
    }

    public void SendCalibrationCommand ()
    {
      var command = Letter;

      // If the simulator isn't enabled then the raw value is passed as part of the command to specify it directly
      if (!SimulatorIsEnabled)
        command = command + RawLightValue;

      SendDeviceCommand (command);

      // Skip the first X entries to give the value time to stabilise
      WaitForData (1);

      var dataEntry = WaitForDataEntry ();

      // If using the light simulator then the value needs to be within a specified range
      if (SimulatorIsEnabled)
        AssertDataValueIsWithinRange (dataEntry, Letter, RawLightValue, RawValueMarginOfError);
      else // Otherwise it needs to be exact
        AssertDataValueEquals (dataEntry, Letter, RawLightValue);
    }
  }
}

