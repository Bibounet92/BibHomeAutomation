using Xamarin.Forms;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using BibHomeAutomationNavigation.LIFX.LifxObjects;
using BibHomeAutomationNavigation.LIFX;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BibHomeAutomationNavigation
{
	public partial class LightsDetailPage : ContentPage
	{

		LifxManager lifxManager;
		LifxBulb thisBulb;

		public LightsDetailPage(LifxBulb bulb)
		{
			InitializeComponent();
			lifxManager = new LifxManager();
			thisBulb = bulb;
			LightLabel.Text = bulb.Label;
			LightSwitch.IsToggled = bulb.Power; 

		}

		void onSliderValueChanged(object sender, ValueChangedEventArgs args)
		{

		}

		async void onSwitchValueChanged(object sender, ToggledEventArgs args)
		{
			var thisSwitch = (Switch)sender;
			await lifxManager.setPower(thisSwitch.IsToggled, thisBulb.Label);
		}


	}

}

