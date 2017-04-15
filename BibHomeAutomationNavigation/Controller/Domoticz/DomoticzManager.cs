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

		public async Task<DomoticzJsonResult> GetDeviceList(string type )
		{
			// type value : light weather temp utility

			client.DefaultRequestHeaders.Accept.Clear();

			string url = AppConstants.DomoticzBaseUrl + "type=devices&filter="+ type +"&used=true&order=Name";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<DomoticzJsonResult>(response.Content.ReadAsStringAsync().Result);
			}
			return null;
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


