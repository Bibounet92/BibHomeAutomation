using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects
{
	public class LifxHome : ObservableCollection<LifxStairs>
	{
		public string Title { get; set; }
		public string ShortTitle { get; set; }
		public List<LifxStairs> stairs { get; set; }

		public LifxHome(List<LifxBulb> bulbs)
		{
			Title = "Ma Maison";
			ShortTitle = "BibHome";

			stairs = new List<LifxStairs>();


		}
	}
}