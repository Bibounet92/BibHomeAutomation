using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BibHomeAutomationNavigation.RATP
{
	public class RatpManager : object
	{
		public HttpClient client = new HttpClient();

		public async Task<RatpTrafficJson> GetTraffic()
		{
			client.DefaultRequestHeaders.Accept.Clear();

			string url = AppConstants.RatpBaseUrl + "traffic/rers";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<RatpTrafficJson>(response.Content.ReadAsStringAsync().Result);
			}
			return null;
		}
	}
}
