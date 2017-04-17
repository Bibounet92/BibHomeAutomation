using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoWeatherStationModule : NetatmoModule
	{
		[JsonProperty(PropertyName = "dashboard_data")]
		public NetatmoWSDashboardData DashboardData { get; set; }

		[JsonProperty(PropertyName = "data_type")]
		public string[] DataType { get; set; }

		[JsonProperty(PropertyName = "last_seen")]
		public long LastSeen { get; set; }
	}
}
