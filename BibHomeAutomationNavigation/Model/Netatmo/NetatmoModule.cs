using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoModule
	{
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "battery_percent")]
		public int BatteryPercent { get; set; }

		[JsonProperty(PropertyName = "battery_vp")]
		public int BatteryVp { get; set; }

		[JsonProperty(PropertyName = "firmware")]
		public int Firmware { get; set; }

		[JsonProperty(PropertyName = "last_message")]
		public long LastMessage { get; set; }

		[JsonProperty(PropertyName = "module_name")]
		public string ModuleName { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "rf_status")]
		public string rfStatus { get; set; }
	}
}
