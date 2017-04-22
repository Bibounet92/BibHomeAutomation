using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;

namespace BibHomeAutomationNavigation.Domoticz
{
	public class DomoticzScene
	{
		public DomoticzScene()
		{
		}

	}

	public class DomoticzSceneType : ObservableCollection<DomoticzJsonScene>
	{
		public string Title { get; set; }
		public string ShortName { get; set; }

	}
}
