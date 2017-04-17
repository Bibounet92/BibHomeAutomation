using System;
using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoWeatherStation : NetatmoDevice
	{
		[JsonProperty(PropertyName = "modules")]
		public NetatmoWeatherStationModule[] Modules { get; set; }

		[JsonProperty(PropertyName = "co2_calibrating")]
		public bool Co2Calibrating { get; set; }

		[JsonProperty(PropertyName = "dashboard_data")]
		public NetatmoWSDashboardData DashboardData { get; set; }

		[JsonProperty(PropertyName = "data_type")]
		public string[] DataType { get; set; }

		[JsonProperty(PropertyName = "last_upgrade")]
		public long LastUpgrade { get; set; }

		[JsonProperty(PropertyName = "module_name")]
		public string ModuleName { get; set; }
	}
}
