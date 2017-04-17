using System.Collections.ObjectModel;
using BibHomeAutomationNavigation.Domoticz;

namespace BibHomeAutomationNavigation.Domoticz
{
	public class DomoticzDevice
	{
		public DomoticzDevice()
		{
		}

	}

	public class DomoticzDeviceType : ObservableCollection<DomoticzJsonDevice>
	{
		public string Title { get; set; }
		public string ShortName { get; set; }

	}
}
