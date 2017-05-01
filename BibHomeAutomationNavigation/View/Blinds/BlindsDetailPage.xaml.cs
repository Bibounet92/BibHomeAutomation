using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;
using BibHomeAutomationNavigation.Framework;

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

            Device.BeginInvokeOnMainThread(async () =>{

			    devices.Clear();
			    items = await domoticzManager.GetDeviceList("light");
			    var lstView = new ListView();
			    lstView.RowHeight = 80;
			    this.Title = "Detail";
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
                    lstView.SeparatorVisibility = SeparatorVisibility.None;
			        lstView.IsPullToRefreshEnabled = true;
			        lstView.Refreshing += OnItemRefresh;
			        Content = lstView;
			    }
			});

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
            Label NameLabel { get; set; }
            MyButton Open { get; set; }
            MyButton Close { get; set; }
            MyButton Stop { get; set; }

            public CustomSystemCell()
            {

                NameLabel = new Label();
                Open = new MyButton();
                Close = new MyButton();
                Stop = new MyButton();

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

                Stop.Text = "Stop";
                Stop.Clicked += OnButtonClicked;
                Stop.CommandParameter = "Stop";

                //add views to the view hierarchy
                verticalLayout.Children.Add(NameLabel);
                verticalLayout.Children.Add(horizontalLayout);
                horizontalLayout.Children.Add(Open);
                horizontalLayout.Children.Add(Stop);
                horizontalLayout.Children.Add(Close);

                // add to parent view
                View = verticalLayout;
            }

            void OnButtonClicked(object sender, EventArgs e)
            {
                var button = (Button)sender;
                var device = (DomoticzJsonDevice)this.BindingContext;
                domoticzManager.ManageBlinds(device.idx, (string)button.CommandParameter);
            }
        }

    }

}
