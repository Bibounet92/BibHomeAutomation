using System.Collections.ObjectModel;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects
{
	public class LifxStairs : ObservableCollection<LifxBulb>
	{
		public string Title { get; set; }
		public string ShortName { get; set; }


	}
}
