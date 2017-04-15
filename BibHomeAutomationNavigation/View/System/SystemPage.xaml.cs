using Xamarin.Forms;
using BibHomeAutomationNavigation.Domoticz;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;


namespace BibHomeAutomationNavigation
{
	public partial class SystemPage : ContentPage
	{

		static DomoticzManager domoticzManager;
		public DomoticzJsonResult items { get; set; }
		public ObservableCollection<DomoticzJsonDevice> devices { get; set; }

		public SystemPage()
		{
			InitializeComponent();
			domoticzManager = new DomoticzManager();
			items = new DomoticzJsonResult();
			devices = new ObservableCollection<DomoticzJsonDevice>();

		}

		protected override async void OnAppearing()
		{
			items = await domoticzManager.GetDeviceList("utility");
			var lstView = new ListView();
			lstView.RowHeight = 60;
			this.Title = "System";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomSystemCell));
			lstView.GroupHeaderTemplate = new DataTemplate(typeof(CustomSystemGroupedCell));

			if (items.result.Count > 0)
			{
				var grouped = new ObservableCollection<DomoticzDeviceType>();

				var rdc = new DomoticzDeviceType() { Title = "Raspberry", ShortName = "Pi3" };
				var etage = new DomoticzDeviceType() { Title = "Freebox", ShortName = "Fbx" };

				foreach (var item in items.result)
				{
					if (item.HardwareName.Equals("BibRaspberry"))
						rdc.Add(item);
					else if (item.HardwareName.Equals("Freebox Server"))
						etage.Add(item);
				};

				grouped.Add(rdc);
				grouped.Add(etage);

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



		public class CustomSystemCell : ViewCell
		{


			Label statusLabel { get; set; }
			Label nameLabel { get; set; }

			public CustomSystemCell()
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
				statusLabel.SetBinding(Label.TextProperty, new Binding("Data"));

				//add views to the view hierarchy
				horizontalLayout.Children.Add(verticalLayout);
				verticalLayout.Children.Add(nameLabel);
				verticalLayout.Children.Add(statusLabel);

				// add to parent view
				View = horizontalLayout;
			}


		}

		public class CustomSystemGroupedCell : CustomSystemCell
		{
			public CustomSystemGroupedCell()
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