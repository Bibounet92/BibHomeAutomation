using System.Collections.Generic;

namespace BibHomeAutomationNavigation.GoogleMaps
{
	public class GoogleMapsTrafficDistance
	{
		public string text { get; set; }
		public int value { get; set; }
	}

	public class GoogleMapsTrafficDuration
	{
		public string text { get; set; }
		public int value { get; set; }
	}

	public class GoogleMapsTrafficDurationInTraffic
	{
		public string text { get; set; }
		public int value { get; set; }
	}

	public class GoogleMapsTrafficElement
	{
		public GoogleMapsTrafficDistance distance { get; set; }
		public GoogleMapsTrafficDuration duration { get; set; }
		public GoogleMapsTrafficDurationInTraffic duration_in_traffic { get; set; }
		public string status { get; set; }
	}

	public class GoogleMapsTrafficResults
	{
		public List<GoogleMapsTrafficElement> elements { get; set; }
	}

	public class GoogleMapsTrafficJson
	{
		public List<string> destination_addresses { get; set; }
		public List<string> origin_addresses { get; set; }
		public string alias { get; set; }
		public List<GoogleMapsTrafficResults> rows { get; set; }
		public string status { get; set; }
	}
}
