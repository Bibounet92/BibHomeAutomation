using Xamarin.Forms;

namespace BibHomeAutomationNavigation
{
	public partial class SecurityPage : TabbedPage
	{

		public SecurityPage()
		{
			this.InitializeComponent();
			this.Children.Add(new SecurityPanel());
			this.Children[0].Title = "Panel";
			this.Children[0].Icon = "todo.png";
			this.Children.Add(new SecurityElements());
			this.Children[1].Title = "Elements";
			this.Children[1].Icon = "todo.png";
			this.Children.Add(new SecurityScenes());
			this.Children[2].Title = "Scene";
			this.Children[2].Icon = "todo.png";
		}

	}
}

