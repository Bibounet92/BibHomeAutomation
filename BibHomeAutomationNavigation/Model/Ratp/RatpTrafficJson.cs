using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BibHomeAutomationNavigation.RATP
{
	public class RatpTrafficJson
	{
		public RatpTrafficResult Result { get; set; }
		public RatpMetadata Metadata { get; set; }
	}

	public class RatpRer
	{
		public string Line { get; set; }
		public string Slug { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
	}

	public class RatpTrafficResult
	{
		public List<RatpRer> Rers { get; set; }
	}

	public class RatpMetadata
	{
		public string call { get; set; }
		public string date { get; set; }
		public int version { get; set; }
	}
}
