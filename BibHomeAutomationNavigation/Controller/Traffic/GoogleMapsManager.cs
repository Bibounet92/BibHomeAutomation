using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BibHomeAutomationNavigation.GoogleMaps
{
	public class GoogleMapsManager : object
	{

		public HttpClient client = new HttpClient();

		public async Task<GoogleMapsTrafficJson> GetTraffic()
		{

			client.DefaultRequestHeaders.Accept.Clear();

			string units = "units=imperial&";
			string origin = "origins=Le+Raincy,FR&";
			string destination = "destinations=La+Defense,FR&";
			string apiKey = "key=" + AppConstants.GoogleMapsApiKey;

			string url = AppConstants.GoogleMapsBaseUrl + units + origin + destination + apiKey;

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
