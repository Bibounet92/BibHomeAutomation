using Xamarin.Forms;
using BibHomeAutomationNavigation.Domoticz;
using System.Collections.ObjectModel;
using System;


namespace BibHomeAutomationNavigation
{
	public partial class SecurityElements : ContentPage
	{

		static DomoticzManager domoticzManager;
		public DomoticzJsonDeviceResult items { get; set; }
		public ObservableCollection<DomoticzJsonDevice> devices { get; set; }

		public SecurityElements()
		{
			InitializeComponent();
			domoticzManager = new DomoticzManager();
			items = new DomoticzJsonDeviceResult();
			devices = new ObservableCollection<DomoticzJsonDevice>();

		}

		protected override async void OnAppearing()
		{
			items = await domoticzManager.GetDeviceList("light");
			var lstView = new ListView();
			lstView.RowHeight = 60;
			this.Title = "SecurityElement";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomSecurityElementsCell));
			lstView.GroupHeaderTemplate = new DataTemplate(typeof(CustomSecurityElementsGroupedCell));

			if (items.result.Count > 0)
			{
				var grouped = new ObservableCollection<DomoticzDeviceType>();

				var doorSensor = new DomoticzDeviceType() { Title = "Door Sensor", ShortName = "DS" };
				var motionSensor = new DomoticzDeviceType() { Title = "Motion Sensor", ShortName = "MS" };
				var smokeSensor = new DomoticzDeviceType() { Title = "Smoke Sensor", ShortName = "SS" };
				var floodSensor = new DomoticzDeviceType() { Title = "Flood Sensor", ShortName = "FS" };

				foreach (var item in items.result)
				{
					if (item.SwitchType != null && item.Image != null)
					{
						if (item.SwitchType.Equals("Door Lock"))
							doorSensor.Add(item);
						else if (item.Image.Equals("Water"))
							floodSensor.Add(item);
						else if (item.SwitchType.Equals("Motion Sensor"))
							motionSensor.Add(item);
						else if (item.SwitchType.Equals("Smoke Detector"))
							smokeSensor.Add(item);
					}
				};

				grouped.Add(doorSensor);
				grouped.Add(motionSensor);
				grouped.Add(smokeSensor);
				grouped.Add(floodSensor);

				lstView.ItemsSource = grouped;
				lstView.IsGroupingEnabled = true;
				lstView.GroupDisplayBinding = new Binding("Title");
				lstView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
				lstView.IsPullToRefreshEnabled = true;

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

		public class CustomSecurityElementsCell : ViewCell
		{
			Label statusLabel { get; set; }
			Label nameLabel { get; set; }

			public CustomSecurityElementsCell()
			{

				statusLabel = new Label();
				nameLabel = new Label();

				var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
				horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
				var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
				verticalLayout.Orientation = StackOrientation.Horizontal;
				verticalLayout.VerticalOptions = LayoutOptions.Center;

				//set bindings
				nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
				statusLabel.SetBinding(Label.TextProperty, new Binding("Status"));

				//add views to the view hierarchy
				horizontalLayout.Children.Add(verticalLayout);
				verticalLayout.Children.Add(nameLabel);
				verticalLayout.Children.Add(statusLabel);

				// add to parent view
				View = horizontalLayout;
			}
		}

		public class CustomSecurityElementsGroupedCell : CustomSecurityElementsCell
		{
			public CustomSecurityElementsGroupedCell()
			{
				//instantiate each of our views
				var nameLabel = new Label();
				var horizontalLayout = new StackLayout() { BackgroundColor = Color.Gray };

				//set bindings
				nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));
				nameLabel.FontSize = 24;

				//add views to the view hierarchy
				horizontalLayout.Children.Add(nameLabel);

				// add to parent view
				View = horizontalLayout;
			}
		}
	}
}