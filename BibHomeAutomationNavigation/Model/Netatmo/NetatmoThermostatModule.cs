using System;
using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoThermostatModule : NetatmoModule
	{
		[JsonProperty(PropertyName = "measured")]
		public NetatmoThermostatMeasures Measures { get; set; }

		[JsonProperty(PropertyName = "setpoint")]
		public NetatmoThermostatSetPoint SetPoint { get; set; }

	}

	public class NetatmoThermostatMeasures
	{
		[JsonProperty(PropertyName = "time")]
		public Int32 Time { get; set; }

		[JsonProperty(PropertyName = "temperature")]
		public double Temperature { get; set; }

		[JsonProperty(PropertyName = "setpoint_temp")]
		public double SetPoint { get; set; }
	}

	public class NetatmoThermostatSetPoint
	{
		[JsonProperty(PropertyName = "setpoint_mode")]
		public string SetPointMode { get; set; }	
	}		
			
}
