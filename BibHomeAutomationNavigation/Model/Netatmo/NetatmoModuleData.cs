using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoModuleData
	{
		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		[JsonProperty(PropertyName = "time_exec")]
		public float TimeExec { get; set; }

		[JsonProperty(PropertyName = "time_server")]
		public long TimeServer { get; set; }
	}

	public class NetatmoThermostatModuleData : NetatmoModuleData
	{
		[JsonProperty(PropertyName = "body")]
		public NetatmoThermostatModuleBody Data { get; set; }
	}

	public class NetatmoWSModuleData : NetatmoModuleData
	{
		[JsonProperty(PropertyName = "body")]
		public NetatmoWSModuleBody Data { get; set; }
	}
}
