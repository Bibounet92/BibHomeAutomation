using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BibHomeAutomationNavigation.LIFX.LifxObjects;

namespace BibHomeAutomationNavigation
{
	public class WebServiceManager
	{
		public HttpClient client = new HttpClient();
		private readonly string authKey, authType;

		public WebServiceManager(string auth, string authType)
		{
			this.authKey = auth;
			this.authType = authType;
		}

		public void initializeClient()
		{
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType,authKey);
		}

		public async void GetData(Uri uri)
		{
			initializeClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
			HttpResponseMessage response = await client.SendAsync(request);
		}

		public async void PostData(Uri uri)
		{
			initializeClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
			await client.SendAsync(request);
		}

		public async void PutData(Uri uri, string args)
		{
			initializeClient();

			StringContent stringContent = new StringContent(args,UnicodeEncoding.UTF8,"application/json");
			await client.PutAsync(uri, stringContent);
		}

	}
}
