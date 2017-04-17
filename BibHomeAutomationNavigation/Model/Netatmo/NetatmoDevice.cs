using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoDevice
	{
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "firmware")]
		public int Firmware { get; set; }

		[JsonProperty(PropertyName = "last_status_store")]
		public long LastStatusStore { get; set; }

		[JsonProperty(PropertyName = "wifi_status")]
		public int WifiStatus { get; set; }

		[JsonProperty(PropertyName = "station_name")]
		public string StationName { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }
	}
}
