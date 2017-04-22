using Xamarin.Forms;

namespace BibHomeAutomationNavigation
{
	public partial class BlindsPage : TabbedPage
	{

		public BlindsPage()
		{
			this.InitializeComponent();
			this.Children.Add(new BlindsGroupPage());
			this.Children[0].Title = "Group";
			this.Children[0].Icon = "todo.png";
			this.Children.Add(new BlindsDetailPage());
			this.Children[1].Title = "Detail";
			this.Children[1].Icon = "todo.png";
		}

	}
}