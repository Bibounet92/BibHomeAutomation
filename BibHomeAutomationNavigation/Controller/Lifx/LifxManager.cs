using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BibHomeAutomationNavigation.LIFX.LifxObjects;
using Newtonsoft.Json;

namespace BibHomeAutomationNavigation.LIFX
{
	public class LifxManager : object
	{

		private readonly String _auth;
		public HttpClient client = new HttpClient();

		public LifxManager()
		{
			_auth = LifxConstants.authKey;
			var webServiceManager = new WebServiceManager(_auth, "Bearer");

		}

		public LifxManager(string auth)
		{

			if (auth.Length != 64)
			{
				throw new FormatException("Authentication token should be 64 characters long");
			}
			var webServiceManager = new WebServiceManager(auth, "Bearer");
		}

		public async Task<List<LifxBulb>> listBulbs()
		{

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth);

			string url = AppConstants.LifxBaseUrl + "lights/all";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			List<LifxBulb> list2 = null;
			if (response.IsSuccessStatusCode)
			{
				var list = JsonConvert.DeserializeObject<List<Object>>(response.Content.ReadAsStringAsync().Result);
				list2 = list.Select(str => new LifxBulb(str.ToString(), this)).ToList();
				return list2;
			}

			return new List<LifxBulb>();
		}

		public async Task setColor(string color, int duration, string selector = "all")
		{
			var request = LifxConstants.setColor.Replace("%AUTH", _auth).Replace("%SELECTOR", selector).Replace("%COLOR", color).Replace("%DURATION", duration.ToString());
			await client.GetStringAsync(request);
		}

		public async Task togglePower(string selector = "all")
		{
			string url = AppConstants.LifxBaseUrl + "lights/all/toggle";
			client.BaseAddress = new Uri(url);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth);

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
			await client.SendAsync(request);

		}

		public async Task setPower(bool on, string selector = "all")
		{

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth);
			string url = AppConstants.LifxBaseUrl + "lights / label:% SELECTOR / state".Replace(" % SELECTOR", selector);
			Uri uri = new Uri(url);
			StringContent stringContent = new StringContent("{ \"power\": \"%STATE\" }".Replace("%STATE", on ? "on" : "off"), Encoding.UTF8, "application/json");
			await client.PutAsync(uri, stringContent);

		}
	}

}
