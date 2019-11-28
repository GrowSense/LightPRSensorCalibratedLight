using System;

namespace LightPRSensorCalibratedLight.Tests.Integration
{
  public class SerialCommandTestHelper : GrowSenseHardwareTestHelper
  {
    public string Key = "";
    public string Value = "0";
    public string Label = "";
    public string ExpectedValue = "";
    public bool ValueIsOutputAsData = true;
    public bool ValueIsSavedAfterReset = true;
    public string ExpectedSerialOutputAfterCommand;
    public bool CheckExpectedSerialOutput = false;
    public bool SeparateKeyValueWithColon = false;

    public void TestCommand ()
    {
      if (CheckExpectedSerialOutput && String.IsNullOrEmpty (ExpectedSerialOutputAfterCommand))
        ExpectedSerialOutputAfterCommand = Label + ": " + Value;
        
      if (String.IsNullOrEmpty (ExpectedValue))
        ExpectedValue = Value;

      WriteTitleText ("Starting " + Label + " command test");

      Console.WriteLine ("Value for " + Label + ": " + Value);
      Console.WriteLine ("");

      ConnectDevices ();

      SendCommand ();

      if (ValueIsSavedAfterReset)
        ResetAndCheckSettingIsPreserved ();
    }

    public void SendCommand ()
    {
      WriteParagraphTitleText ("Sending " + Label + " command...");

      var command = Key + Value;

      if (SeparateKeyValueWithColon)
        command = Key + ":" + Value;

      SendDeviceCommand (command);

      WriteParagraphTitleText ("Checking " + Label + " value was set...");

      if (ValueIsOutputAsData) {
        var dataEntry = WaitForDataEntry ();

        AssertDataValueEquals (dataEntry, Key, ExpectedValue);
      }

      if (!String.IsNullOrEmpty (ExpectedSerialOutputAfterCommand))
        WaitForText (ExpectedSerialOutputAfterCommand);
    }

    public void ResetAndCheckSettingIsPreserved ()
    {
      ResetDeviceViaPin ();

      WriteParagraphTitleText ("Checking " + Label + " value is preserved after reset...");

      if (ValueIsOutputAsData) {
        var dataEntry = WaitForDataEntry ();

        AssertDataValueEquals (dataEntry, Key, ExpectedValue);
      }

      if (!String.IsNullOrEmpty (ExpectedSerialOutputAfterCommand))
        WaitForText (ExpectedSerialOutputAfterCommand);
    }
  }
}

