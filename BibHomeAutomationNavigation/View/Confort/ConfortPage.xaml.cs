using Xamarin.Forms;

namespace BibHomeAutomationNavigation
{
	public partial class ConfortPage : TabbedPage
	{

		public ConfortPage()
		{
			this.InitializeComponent();
			this.Children.Add(new ThermostatPage());
			this.Children[0].Title = "Thermostat";
			this.Children[0].Icon = "todo.png";
			this.Children.Add(new TemperaturesPage());
			this.Children[1].Title = "Températures";
			this.Children[1].Icon = "todo.png";
			this.Children.Add(new MeteoPage());
			this.Children[2].Title = "Météo";
			this.Children[2].Icon = "todo.png";		}

	}
}