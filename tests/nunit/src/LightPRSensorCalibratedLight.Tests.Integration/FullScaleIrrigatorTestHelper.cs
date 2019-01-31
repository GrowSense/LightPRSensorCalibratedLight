using System;
using NUnit.Framework;
using System.Collections.Generic;
namespace LightPRSensorCalibratedLight.Tests.Integration
{
	public class FullScaleIrrigatorTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public int LightOnIncreaseValue = 10;
		public int LightOffDecreaseValue = 5;

		public int LightPercentageMaximum = 60;
		public int LightPercentageMinimum = 20;

		public void RunFullScaleTest()
		{
			WriteTitleText("Starting full scale test");

			ConnectDevices();

			int lightPercentage = 30;

			int totalCyclesToRun = 15;

			for (int i = 0; i < totalCyclesToRun; i++)
			{
				lightPercentage = RunFullScaleTestCycle(lightPercentage);
			}
		}


		public int RunFullScaleTestCycle(int lightPercentage)
		{
			WriteSubTitleText("Starting full scale test cycle");

			WriteParagraphTitleText("Reading the value of the soil moisture sensor light pin...");

			var lightPinValue = SimulatorDigitalRead(SimulatorLightPin);

			Console.WriteLine("Light pin value: " + GetOnOffString(lightPinValue));

			WriteParagraphTitleText("Simulating specified soil moisture percentage...");

			SimulateLight(lightPercentage);

			WriteParagraphTitleText("Getting data to check that values are correct...");

			var data = WaitForData(3); // Wait for 3 entries to allow the simulator to stabilise

			var dataEntry = data[data.Length - 1];

			AssertLightValuesAreCorrect(lightPercentage, dataEntry);

			var lightPercentageReading = Convert.ToInt32(dataEntry["C"]);

			AssertLightValueIsWithinRange(lightPercentageReading);

			var newLightPercentage = AdjustLightPercentageBasedOnLightPin(lightPercentage, lightPinValue);

			return newLightPercentage;
		}

		public void AssertLightValuesAreCorrect(int lightPercentage, Dictionary<string, string> dataEntry)
		{
			WriteParagraphTitleText("Checking calibrated value...");

			AssertDataValueIsWithinRange(dataEntry, "C", lightPercentage, CalibratedValueMarginOfError);

			WriteParagraphTitleText("Checking raw value...");

			var expectedRawValue = lightPercentage * AnalogPinMaxValue / 100;

			AssertDataValueIsWithinRange(dataEntry, "R", expectedRawValue, RawValueMarginOfError);

		}

		public void AssertLightValueIsWithinRange(int lightPercentage)
		{
			WriteParagraphTitleText("Checking soil moisture percentage is between " + LightPercentageMinimum + " and " + LightPercentageMaximum);

			Console.WriteLine("  Current soil moisture level: " + lightPercentage + "%");

			if (lightPercentage > LightPercentageMaximum)
				Assert.Fail("Soil moisture went above " + LightPercentageMaximum + "%");

			if (lightPercentage < LightPercentageMinimum)
				Assert.Fail("Soil moisture dropped below " + LightPercentageMinimum + "%");
		}

		public int AdjustLightPercentageBasedOnLightPin(int lightPercentage, bool lightPinValue)
		{
			WriteParagraphTitleText("Adjusting simulated soil moisture sensor based on whether light pin is on/off.");

			if (lightPinValue)
			{
				Console.WriteLine("  Light pin is high. Increasing simulated soil moisture.");
				lightPercentage += LightOnIncreaseValue;
			}
			else
			{
				Console.WriteLine("  Light pin is low. Decreasing simulated soil moisture.");
				lightPercentage -= LightOffDecreaseValue;
			}

			Console.WriteLine("  New soil moisture level: " + lightPercentage + "%");

			return lightPercentage;
		}
	}
}
