using System;
using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.Netatmo
{
	public class NetatmoThermostat : NetatmoDevice
	{
		[JsonProperty(PropertyName = "modules")]
		public NetatmoThermostatModule[] Modules { get; set; }
	}
}
