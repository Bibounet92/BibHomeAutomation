using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;
using BibHomeAutomationNavigation.Framework;

namespace BibHomeAutomationNavigation
{
	public partial class BlindsGroupPage : ContentPage
	{
		static DomoticzManager domoticzManager;
		public DomoticzJsonSceneResult items { get; set; }
		public ObservableCollection<DomoticzJsonScene> devices { get; set; }

		public BlindsGroupPage()
		{
			InitializeComponent();
			domoticzManager = new DomoticzManager();
			items = new DomoticzJsonSceneResult();
			devices = new ObservableCollection<DomoticzJsonScene>();

			devices.Clear();
            Device.BeginInvokeOnMainThread(async () =>
            {
                items = await domoticzManager.GetSceneList();
                var lstView = new ListView();
                lstView.RowHeight = 80;
                this.Title = "Blinds";
                lstView.ItemTemplate = new DataTemplate(typeof(CustomBlindCell));

                if (items.result.Count > 0)
                {
                    foreach (var item in items.result)
                    {
                        if (item.Name.StartsWith("Volets", StringComparison.CurrentCulture))
                            devices.Add(item);

                    };

                    lstView.ItemsSource = devices;
                    lstView.ItemSelected += OnItemSelected;
                    lstView.IsPullToRefreshEnabled = true;
                    lstView.SeparatorVisibility = SeparatorVisibility.None;
                    lstView.Refreshing += OnItemRefresh;
                    Content = lstView;
                }
            });

        }

		/*protected override async void OnAppearing()
		{
			devices.Clear();
			items = await domoticzManager.GetSceneList();
			var lstView = new ListView();
			lstView.RowHeight = 80;
			this.Title = "Blinds";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomBlindCell));

			if (items.result.Count > 0)
			{
				foreach (var item in items.result)
				{
					if (item.Name.StartsWith("Volets",StringComparison.CurrentCulture))
						devices.Add(item);

				};

				lstView.ItemsSource = devices;
				lstView.ItemSelected += OnItemSelected;
				lstView.IsPullToRefreshEnabled = true;
				lstView.Refreshing += OnItemRefresh;
				Content = lstView;
			}
		}*/

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



		public class CustomBlindCell : ViewCell
		{
			Label NameLabel { get; set; }
			MyButton Open { get; set; }
			MyButton Close { get; set; }

			public CustomBlindCell()
			{

				NameLabel = new Label();

				Open = new MyButton();
				Close = new MyButton();

				var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
				verticalLayout.Orientation = StackOrientation.Vertical;
				verticalLayout.VerticalOptions = LayoutOptions.Center; 

                var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
                horizontalLayout.Orientation = StackOrientation.Horizontal;
				horizontalLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;


				//set bindings
				NameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
                NameLabel.HorizontalOptions = LayoutOptions.Center;

				Open.Text = "Open";
				Open.CommandParameter = "Off";
				Open.Clicked += OnButtonClicked;

				Close.Text = "Close";
				Close.Clicked += OnButtonClicked;
				Close.CommandParameter = "On";

				//add views to the view hierarchy
				verticalLayout.Children.Add(NameLabel);
                verticalLayout.Children.Add(horizontalLayout);
				horizontalLayout.Children.Add(Open);
				horizontalLayout.Children.Add(Close);

				// add to parent view
				View = verticalLayout;
			}

			void OnButtonClicked(object sender, EventArgs e)
			{
				var button = (Button)sender;
				var device = (DomoticzJsonScene)this.BindingContext;
				domoticzManager.ManageScenes(device.idx, (string)button.CommandParameter);
			}
		}

	}

}
