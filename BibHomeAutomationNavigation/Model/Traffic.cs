using System;
using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.GoogleMaps;
using BibHomeAutomationNavigation.RATP;


namespace BibHomeAutomationNavigation
{
	public class Traffic
	{

		public string TypeTraffic { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public string ApiCallDate { get; set; }

		public Traffic(RatpRer rer)
		{
			TypeTraffic = "RATP";
			Title = "RER " + rer.Line.ToUpper();
			Message = rer.Message;
			ApiCallDate = DateTime.Now.ToString();

		}

		public Traffic(GoogleMapsTrafficJson gmJson)
		{
			TypeTraffic = "Cars";
			if (gmJson.alias is null)
				Title = gmJson.origin_addresses[0] + " - " + gmJson.destination_addresses[0];
			else
				Title = gmJson.alias;
			Message = "Duration : " + gmJson.rows[0].elements[0].duration_in_traffic.text + " for " + gmJson.rows[0].elements[0].distance.text + " (normally : " + gmJson.rows[0].elements[0].duration.text + ")";
			ApiCallDate = DateTime.Now.ToString();
		}
	}

	public class TrafficType : ObservableCollection<Traffic>
	{
		public string Title { get; set; }
		public string ShortName { get; set; }

	}
}
