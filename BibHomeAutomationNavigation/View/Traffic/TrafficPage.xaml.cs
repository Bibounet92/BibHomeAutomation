using Xamarin.Forms;
using System.Collections.Generic;
using BibHomeAutomationNavigation.RATP;
using BibHomeAutomationNavigation.GoogleMaps;
using System.Collections.ObjectModel;

namespace BibHomeAutomationNavigation
{
	public partial class TrafficPage : ContentPage
	{
		public RatpTrafficJson items { get; set; }
		static RatpManager ratpManager;
		public List<GoogleMapsTrafficJson> gmItems { get; set; }
		static GoogleMapsManager googleMapsManager;

		public TrafficPage()
		{
			this.InitializeComponent();
			ratpManager = new RatpManager();
			googleMapsManager = new GoogleMapsManager();
			items = new RatpTrafficJson();
			gmItems = new List<GoogleMapsTrafficJson>();

		}

		protected override async void OnAppearing()
		{
			items = await ratpManager.GetTraffic();
			gmItems = await googleMapsManager.GetAllTraffic();
			var lstView = new ListView();
			lstView.RowHeight = 80;
			this.Title = "Traffic";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomTraficCell));
			lstView.GroupHeaderTemplate = new DataTemplate(typeof(CustomTraficGroupedCell));

			if (items.Result.Rers.Count > 0 && gmItems.Count > 0)
			{
				var grouped = new ObservableCollection<TrafficType>();

				var ratp = new TrafficType() { Title = "RATP", ShortName = "RATP" };
				var voiture = new TrafficType() { Title = "Voiture", ShortName = "Voiture" };

				foreach (var item in items.Result.Rers)
				{
					if (item.Line.Equals("e") || item.Line.Equals("A"))
						ratp.Add(new Traffic(item));
				};

				foreach (var gmItem in gmItems)
				{
					voiture.Add(new Traffic(gmItem));
				};

				grouped.Add(ratp);
				grouped.Add(voiture);

				lstView.ItemsSource = grouped;
				lstView.IsGroupingEnabled = true;
				lstView.GroupDisplayBinding = new Binding("Title");
				lstView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
				lstView.IsPullToRefreshEnabled = true;

				lstView.ItemSelected += OnItemSelected;
				lstView.IsPullToRefreshEnabled = true;
				//lstView.Refreshing += OnItemRefresh;
				Content = lstView;
			}
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			//string url; 
			var list = (ListView)sender;
			if (e.SelectedItem == null)
				return;
			var item = (Traffic)list.SelectedItem;
			if (item.TypeTraffic == "RATP")
				Navigation.PushAsync(new RatpDetailPage(item.Title));
			/*else if (item.TypeTraffic == "Cars")
			{
				Device.OnPlatform(iOS: () =>
				  {
					  url = String.Format("http://maps.apple.com/maps?q={0}", address);
				  },
				  Android: () =>
				  {
					  url = String.Format("http://maps.google.com/maps?q={0}", address);
				  });

				Device.OpenUri(url);
			}*/

		}

	}
	public class CustomTraficCell : ViewCell
	{

		Label statusLabel { get; set; }
		Label nameLabel { get; set; }

		public CustomTraficCell()
		{
			statusLabel = new Label();
			nameLabel = new Label();

			var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
			horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
			var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
			//verticalLayout.Orientation = StackOrientation.Horizontal;
			verticalLayout.Orientation = StackOrientation.Vertical;
			verticalLayout.VerticalOptions = LayoutOptions.Center;

			//set bindings
			nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));
			statusLabel.SetBinding(Label.TextProperty, new Binding("Message"));
			statusLabel.FontSize = 10;
			//add views to the view hierarchy
			horizontalLayout.Children.Add(verticalLayout);
			verticalLayout.Children.Add(nameLabel);
			verticalLayout.Children.Add(statusLabel);

			// add to parent view
			View = horizontalLayout;
		}



	}

	public class CustomTraficGroupedCell : CustomTraficCell
	{
		public CustomTraficGroupedCell()
		{
			//instantiate each of our views

			var nameLabel = new Label();
			var horizontalLayout = new StackLayout() { BackgroundColor = Color.Gray };

			//set bindings
			nameLabel.SetBinding(Label.TextProperty, new Binding("title"));
			nameLabel.FontSize = 24;

			//add views to the view hierarchy
			horizontalLayout.Children.Add(nameLabel);

			// add to parent view
			View = horizontalLayout;
		}

	}
}


