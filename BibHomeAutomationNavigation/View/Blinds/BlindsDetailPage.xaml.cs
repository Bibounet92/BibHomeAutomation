using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;

namespace BibHomeAutomationNavigation
{
	public partial class BlindsDetailPage : ContentPage
	{
		static DomoticzManager domoticzManager;
		public DomoticzJsonDeviceResult items { get; set; }
		public ObservableCollection<DomoticzJsonDevice> devices { get; set; }

		public BlindsDetailPage()
		{
			InitializeComponent();
			domoticzManager = new DomoticzManager();
			items = new DomoticzJsonDeviceResult();
			devices = new ObservableCollection<DomoticzJsonDevice>();

		}

		protected override async void OnAppearing()
		{
			devices.Clear(); 
			items = await domoticzManager.GetDeviceList("light");
			var lstView = new ListView();
			lstView.RowHeight = 60;
			this.Title = "Blinds";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomSystemCell));

			if (items.result.Count > 0)
			{
				foreach (var item in items.result)
				{
					if (item.SwitchType.Equals("Blinds"))
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



		public class CustomSystemCell : ViewCell
		{
			Label nameLabel { get; set; }
			Label idx { get; set; }
			Button open { get; set; }
			Button close { get; set; }
			Button stop { get; set; }

			public CustomSystemCell()
			{

				nameLabel = new Label();
				idx = new Label();
				open = new Button();
				close = new Button();
				stop = new Button();
				var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
				horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
				var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
				verticalLayout.Orientation = StackOrientation.Horizontal;
				verticalLayout.VerticalOptions = LayoutOptions.Center;

				//set bindings
				nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
				idx.SetBinding(Label.TextProperty,new Binding("idx"));
				open.Text = "Open";
				open.CommandParameter = "Off";
				open.Clicked += OnButtonClicked;
				close.Text = "Close";
				close.Clicked += OnButtonClicked;
				close.CommandParameter = "On";
				stop.Text = "Stop";
				stop.Clicked += OnButtonClicked;
				stop.CommandParameter = "Stop";
				//add views to the view hierarchy
				horizontalLayout.Children.Add(verticalLayout);
				verticalLayout.Children.Add(nameLabel);
				verticalLayout.Children.Add(open);
				verticalLayout.Children.Add(stop);
				verticalLayout.Children.Add(close);

				// add to parent view
				View = horizontalLayout;
			}

			void OnButtonClicked(object sender, EventArgs e)
			{
				var button = (Button)sender;
				var device = (DomoticzJsonDevice)this.BindingContext;
				domoticzManager.ManageBlinds(device.idx,(string) button.CommandParameter);
			}
		}

	}

}
