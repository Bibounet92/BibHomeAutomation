using Xamarin.Forms;
using BibHomeAutomationNavigation.Netatmo;

namespace BibHomeAutomationNavigation
{
	public class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }
		public static NetatmoManager netatmoManager;

		public App()
		{
			//MainPage = new BibHomeAutomationNavigation.MainPage();
			netatmoManager = new NetatmoManager();
			//netatmoManager.LoginSuccessful += ApiLoginSuccessful;
			netatmoManager.Login(new[] { NetatmoScope.read_station, NetatmoScope.read_thermostat });

			if (!IsUserLoggedIn)
			{
				MainPage = new NavigationPage(new LoginPage())
				{
					BarBackgroundColor = Color.Transparent,
					BarTextColor = Color.White
				};
			}
			else
			{
				MainPage = new BibHomeAutomationNavigation.MainPage();
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

