namespace BibHomeAutomationNavigation.LIFX.LifxObjects
{
	static class LifxConstants
	{

		public const string togglePower = " curl -H \"Authorization: Bearer %AUTH\" -X POST \"https://api.lifx.com/v1beta1/lights/%SELECTOR/toggle\"";
		public const string listLights = " curl -H \"Authorization: Bearer %AUTH\" \"https://api.lifx.com/v1/lights/all\"";
		public const string setPower = " curl -H \"Authorization: Bearer %AUTH\" - X PUT - d \"state=%STATE\" \"https://api.lifx.com/v1beta1/lights/%SELECTOR/power\"";
		public const string setColor = " curl -H \"Authorization: Bearer %AUTH\" - X PUT - d \"color=%COLOR&amp;duration=%DURATION\" \"https://api.lifx.com/v1beta1/lights/%SELECTOR/color\"";
	}

	public class LifxJsonState
	{
		string power { get; set; }
		string color { get; set; }
		double brightness { get; set; }
		double duration { get; set; }
		double infrared { get; set; }

	}
}
