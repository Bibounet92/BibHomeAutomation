using Xamarin.Forms;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using BibHomeAutomationNavigation.RATP;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BibHomeAutomationNavigation
{
	public partial class RatpDetailPage : ContentPage
	{

		RatpManager ratpManager;
		RatpRer ratpRer;

		public RatpDetailPage(string line)
		{
			InitializeComponent();
			ratpManager = new RatpManager();
			RatpLineLabel.Text = line;

		}




	}

}

