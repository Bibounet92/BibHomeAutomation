using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoThermostatModuleBody
	{
		[JsonProperty(PropertyName = "devices")]
		public NetatmoThermostat[] Devices { get; set; }
	}

	public class NetatmoWSModuleBody
	{
		[JsonProperty(PropertyName = "devices")]
		public NetatmoWeatherStation[] Devices { get; set; }
	}
}
