using Xamarin.Forms;
using BibHomeAutomationNavigation.LIFX.LifxObjects;
using BibHomeAutomationNavigation.LIFX;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;


namespace BibHomeAutomationNavigation
{
	public partial class LightsPage : ContentPage
	{

		static LifxManager lifxManager;
		public List<LifxBulb> items { get; set; }
		public ObservableCollection<LifxBulb> bulbs { get; set; }

		public LightsPage()
		{
			this.InitializeComponent();
			lifxManager = new LifxManager();
			items = new List<LifxBulb>();
			bulbs = new ObservableCollection<LifxBulb>();

		}

	    protected override async void OnAppearing()
		{
			items = await lifxManager.listBulbs();
			var lstView = new ListView();
			lstView.RowHeight = 60;
			this.Title = "Lights";
			lstView.ItemTemplate = new DataTemplate(typeof(CustomLightsCell));
			lstView.GroupHeaderTemplate = new DataTemplate(typeof(CustomLightsGroupedCell));

			if (items.Count > 0)
			{
				var grouped = new ObservableCollection<LifxStairs>();

				var rdc = new LifxStairs() { Title = "Rez de chaussée", ShortName = "RDC" };
				var etage = new LifxStairs() { Title = "1er Etage", ShortName = "1Et" };

				foreach (var item in items)
				{
					if (item.group.name.Equals("RDC"))
						rdc.Add(item);
					else if (item.group.name.Equals("Etage"))
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
			Navigation.PushAsync(new LightsDetailPage((LifxBulb) list.SelectedItem));
		}



		public class CustomLightsCell : ViewCell
		{

			Switch onOff { get; set; }
			Label statusLabel { get; set; }
			Label nameLabel { get; set; }

			public CustomLightsCell()
			{
				onOff = new Switch();
				statusLabel = new Label();
				nameLabel = new Label();

				var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
				horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
				var verticalLayout = new StackLayout() { BackgroundColor = Color.White };
				verticalLayout.Orientation = StackOrientation.Horizontal;
				verticalLayout.VerticalOptions = LayoutOptions.Center;

				//set bindings
				nameLabel.SetBinding(Label.TextProperty, new Binding("label"));
				statusLabel.SetBinding(Label.TextProperty, new Binding("connected"));

				onOff.SetBinding(Switch.IsToggledProperty, new Binding("power"));
				onOff.Toggled += onSwitchValueChanged;

				//add views to the view hierarchy
				horizontalLayout.Children.Add(verticalLayout);
				verticalLayout.Children.Add(nameLabel);
				verticalLayout.Children.Add(statusLabel);
				verticalLayout.Children.Add(onOff);

				// add to parent view
				View = horizontalLayout;
			}

			async void onSwitchValueChanged(object sender, ToggledEventArgs args)
			{
				var item = (Switch)sender;
				if (statusLabel.Text.Equals("False") && args.Value){
					
					await Application.Current.MainPage.DisplayAlert("Error","Light is not connected !", "Cancel");
					item.IsToggled = false;
					}
				await lifxManager.setPower(args.Value, this.nameLabel.Text);
			}

		}

		public class CustomLightsGroupedCell : CustomLightsCell
		{
			public CustomLightsGroupedCell()
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

}

