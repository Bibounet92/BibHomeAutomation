using System;
using System.Collections.Generic;

namespace BibHomeAutomationNavigation.Domoticz
{
	public class DomoticzJsonScene
	{
		public string Description { get; set; }
		public int Favorite { get; set; }
		public string LastUpdate { get; set; }
		public string Name { get; set; }
		public string OffAction { get; set; }
		public string OnAction { get; set; }
		public bool Protected { get; set; }
		public string Status { get; set; }
		public string Timers { get; set; }
		public string Type { get; set; }
		public bool UsedByCamera { get; set; }
		public string idx { get; set; }

	}

	public class DomoticzJsonSceneResult
	{
		public int ActTime { get; set; }
		public bool AllowWidgetOrdering { get; set; }
		public string ServerTime { get; set; }
		public string Sunrise { get; set; }
		public string Sunset { get; set; }
		public List<DomoticzJsonScene> result { get; set; }
		public string status { get; set; }
		public string title { get; set; }

	}
}
