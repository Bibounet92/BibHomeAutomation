
using Xamarin.Forms;
using BibHomeAutomationNavigation.Netatmo;

namespace BibHomeAutomationNavigation
{
	public partial class MeteoPage : ContentPage
	{
		readonly NetatmoManager netatmoManager;

		public MeteoPage()
		{
			this.InitializeComponent();
			netatmoManager = new NetatmoManager();
			netatmoManager.LoginSuccessful += ApiLoginSuccessful;
			netatmoManager.Login(new[] { NetatmoScope.read_station });
		}

		private async void ApiLoginSuccessful(object sender)
		{
			var data = await netatmoManager.GetStationsData();
			var measurement = await netatmoManager.GetMeasure("[DeviceId]", Netatmo.Scale.Max, new[]
				{
					MeasurementType.Co2,
					MeasurementType.Humidity,
					MeasurementType.Noise,
					MeasurementType.Pressure,
					MeasurementType.Temperature
				});
		}
	}
}

