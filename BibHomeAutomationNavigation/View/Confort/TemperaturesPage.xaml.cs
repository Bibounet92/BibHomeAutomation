using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;

namespace BibHomeAutomationNavigation
{
	public partial class TemperaturesPage : ContentPage
	{
		static DomoticzManager domoticzManager;
		public DomoticzJsonDeviceResult items { get; set; }
		public ObservableCollection<DomoticzJsonDevice> devices { get; set; }

		public TemperaturesPage()
		{
			InitializeComponent();
			domoticzManager = new DomoticzManager();
			items = new DomoticzJsonDeviceResult();
			devices = new ObservableCollection<DomoticzJsonDevice>();

		}

		protected override async void OnAppearing()
		{
			devices.Clear();
			items = await domoticzManager.GetDeviceList("temp");
			var lstView = new ListView();
			lstView.RowHeight = 60;
			this.Title = "Temperature";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomTempCell));

			if (items.result.Count > 0)
			{
				foreach (var item in items.result)
				{
					if (item.Type.StartsWith("Temp", StringComparison.CurrentCulture))
						devices.Add(item);

				};

				lstView.ItemsSource = devices;
				lstView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
				lstView.ItemSelected += OnItemSelected;
				lstView.IsPullToRefreshEnabled = true;
				lstView.Refreshing += OnItemRefresh;
				Content = lstView;
			}
		}

		void OnItemRefresh(object sender, EventArgs e)
		{
			var list = (ListView)sender;
			OnAppearing();
			list.IsRefreshing = false;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var list = (ListView)sender;
			if (e.SelectedItem == null)
				return;

			//Do what you need
			//Navigation.PushAsync(new LightsDetailPage((LifxBulb)list.SelectedItem));
		}



		public class CustomTempCell : ViewCell
		{


			Label statusLabel { get; set; }
			Label nameLabel { get; set; }
			Label lastUpdateLabel { get; set; }

			public CustomTempCell()
			{

				statusLabel = new Label();
				nameLabel = new Label();
				lastUpdateLabel = new Label();

				var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
				horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
				var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
				verticalLayout.Orientation = StackOrientation.Horizontal;
				verticalLayout.VerticalOptions = LayoutOptions.Center;

				//set bindings
				nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
				statusLabel.SetBinding(Label.TextProperty, new Binding("Data"));
				lastUpdateLabel.SetBinding(Label.TextProperty, new Binding("LastUpdate"));

				//add views to the view hierarchy
				horizontalLayout.Children.Add(verticalLayout);
				verticalLayout.Children.Add(nameLabel);
				verticalLayout.Children.Add(statusLabel);
				verticalLayout.Children.Add(lastUpdateLabel);

				// add to parent view
				View = horizontalLayout;
			}


		}

	}

}

