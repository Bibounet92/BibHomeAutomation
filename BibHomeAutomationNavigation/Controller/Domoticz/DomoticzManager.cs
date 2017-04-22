using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using BibHomeAutomationNavigation.Domoticz;

namespace BibHomeAutomationNavigation
{
	public class DomoticzManager : object
	{
		public HttpClient client = new HttpClient();

		public async Task<DomoticzJsonDeviceResult> GetDeviceList(string type )
		{
			// type value : light weather temp utility

			client.DefaultRequestHeaders.Accept.Clear();

			string url = AppConstants.DomoticzBaseUrl + "type=devices&filter="+ type +"&used=true&order=Name";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<DomoticzJsonDeviceResult>(response.Content.ReadAsStringAsync().Result);
			}
			return null;
		}

		//Action : Off (Open) , On (Close), Stop (Stop)
		public async void ManageBlinds(string idxItem,string action)
		{
			string url = AppConstants.DomoticzBaseUrl + "type=command&param=switchlight&idx="+idxItem+"&switchcmd="+action;
			client.BaseAddress = new Uri(url);
			client.DefaultRequestHeaders.Accept.Clear();
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			await client.SendAsync(request);
		}


		public async Task<DomoticzJsonSceneResult> GetSceneList()
		{
			// type value : light weather temp utility

			client.DefaultRequestHeaders.Accept.Clear();

			string url = AppConstants.DomoticzBaseUrl + "type=scenes";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<DomoticzJsonSceneResult>(response.Content.ReadAsStringAsync().Result);
			}
			return null;
		}

		//Action : Off (Open) , On (Close)
		public async void ManageScenes(string idxItem, string action)
		{
			string url = AppConstants.DomoticzBaseUrl + "type=command&param=switchscene&idx=" + idxItem + "&switchcmd=" + action;
			client.BaseAddress = new Uri(url);
			client.DefaultRequestHeaders.Accept.Clear();
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			await client.SendAsync(request);
		}


		public DomoticzManager()
		{
		}

		/*public ICommand Command
		{
			get
			{
				return new CommandHandler(async () =>
					await toggleSwitch()
				);
			}
		}

		private async Task toggleSwitch()
		{
			await APIService.Instance.toggleSwitch(idx);
			Device d = await APIService.Instance.getDeviceByIdx(idx);
			if (d != null)
			{
				Status = d.Status;
				Image = d.Image;
				TypeImg = d.TypeImg;
				LastUpdate = d.LastUpdate;
				Data = d.Data;
				reloadDevices(true);
			}
		}*/



		/*public BitmapImage IconPath
	{
		get
		{

			if (Status == "Set Level")
			{
				Status = "Off";
			}
			String imagePNG = Image + "48_" + Status + ".png";
			switch (TypeImg)
			{
				case "counter":
					imagePNG = "counter.png";
					break;
			}
			return new BitmapImage(new Uri(APIService.Instance.apiURL + "/images/" + imagePNG));
		}*/
	}
}


