using Xamarin.Forms;
using BibHomeAutomationNavigation.Netatmo;

namespace BibHomeAutomationNavigation
{
	public partial class ThermostatPage : ContentPage
	{
		//readonly NetatmoManager netatmoManager;
		Response<NetatmoThermostatModuleData> data;
		NetatmoThermostatModule therm;

		public ThermostatPage()
		{
			this.InitializeComponent();
			//netatmoManager = new NetatmoManager();
			//netatmoManager.LoginSuccessful += ApiLoginSuccessful;
			//netatmoManager.Login(new[] { NetatmoScope.read_station, NetatmoScope.read_thermostat });
		}


		protected override async void OnAppearing()
		{
			//netatmoManager.Login(new[] { NetatmoScope.read_station, NetatmoScope.read_thermostat });
			var test = App.netatmoManager.OAuthAccessToken.AccessToken;
			data = await App.netatmoManager.GetThermostatData(); 
			therm = (NetatmoThermostatModule)data.Result.Data.Devices[0].Modules[0];
			nameLabel.Text = data.Result.Data.Devices[0].StationName;
			AskedTemp.Text = therm.Measures.SetPoint + "°C";
			ActualTemp.Text = therm.Measures.Temperature + "°C";
		}

	}
}

