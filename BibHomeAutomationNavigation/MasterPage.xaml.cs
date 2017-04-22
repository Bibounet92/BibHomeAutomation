using System.Collections.Generic;
using Xamarin.Forms;

namespace BibHomeAutomationNavigation
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage ()
		{
			InitializeComponent ();

			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Home",
				IconSource = "contacts.png",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Lumières",
				IconSource = "contacts.png",
				TargetType = typeof(LightsPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Volets",
				IconSource = "contacts.png",
				TargetType = typeof(BlindsPage)
			});
			masterPageItems.Add (new MasterPageItem 
			{
				Title = "Confort",
				IconSource = "todo.png",
				TargetType = typeof(ConfortPage)
			});
			masterPageItems.Add (new MasterPageItem 
			{
				Title = "Camera",
				IconSource = "reminders.png",
				TargetType = typeof(WebcamPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Multimedia",
				IconSource = "reminders.png",
				TargetType = typeof(MultimediaPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Traffic",
				IconSource = "reminders.png",
				TargetType = typeof(TrafficPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "System",
				IconSource = "reminders.png",
				TargetType = typeof(SystemPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Securite",
				IconSource = "reminders.png",
				TargetType = typeof(SecurityPage)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
