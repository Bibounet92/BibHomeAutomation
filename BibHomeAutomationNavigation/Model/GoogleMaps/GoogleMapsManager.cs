using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibHomeAutomationNavigation.GoogleMaps
{
	public class GoogleMapsManager : object
	{

		public HttpClient client = new HttpClient();

		public async Task<List<GoogleMapsTrafficJson>> GetAllTraffic()
		{
			GoogleMapsTrafficJson item;
			List<GoogleMapsTrafficJson> list = new List<GoogleMapsTrafficJson>();

			int departureOffset = 0;
			int arrivalOffset = 0;
			var now = DateTime.Now;
			if (now.Hour >= 9 && now.Hour < 20)
			{
				departureOffset = 1;
			}
			else if (now.Hour >= 20)
			{
				departureOffset = 1;
				arrivalOffset = 1;
			}
			var bibDepartureDate = new DateTime(now.Year, now.Month, now.Day + departureOffset, 8, 45, 0);
			var tifinieDepartureDate = new DateTime(now.Year, now.Month, now.Day + departureOffset, 8, 30, 0);
			var bibLeavingDate = new DateTime(now.Year, now.Month, now.Day + arrivalOffset, 19, 00, 0);
			var tifinieLeavingDate = new DateTime(now.Year, now.Month, now.Day + arrivalOffset, 18, 00, 0);


			item = await GetTraffic(AppConstants.BibHome, AppConstants.BibWork,(bibDepartureDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
			item.alias = "Home to BibWork (8h45)";
			list.Add(item);
			item = await GetTraffic(AppConstants.BibWork, AppConstants.BibHome, (bibLeavingDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
			item.alias = "BibWork to Home (19h)";
			list.Add(item);
			item = await GetTraffic(AppConstants.BibHome, AppConstants.TifinieWork, (tifinieDepartureDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
			item.alias = "Home to TifinieWork (8h30)";
			list.Add(item);
			item = await GetTraffic(AppConstants.TifinieWork, AppConstants.BibHome, (tifinieLeavingDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
			item.alias = "TifinieWork to Home (18h)";
			list.Add(item);
			return list;
		}

		public async Task<GoogleMapsTrafficJson> GetTraffic(string origin, string destination, double departureTime)
		{

			client.DefaultRequestHeaders.Accept.Clear();

			string units = "units=metric&";
			origin = "origins=" + origin + "&";
			destination = "destinations=" + destination + "&";
			string departure = "departure_time="+departureTime+"&";

			string apiKey = "key=" + AppConstants.GoogleMapsApiKey;

			string url = AppConstants.GoogleMapsBaseUrl + units + departure + origin + destination + apiKey;

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<GoogleMapsTrafficJson>(response.Content.ReadAsStringAsync().Result);
			}
			return null;
		}
	}
}
