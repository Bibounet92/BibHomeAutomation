using Newtonsoft.Json.Linq;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects
{
	public class LifxBulb : LifxObject
	{
		public string Uuid { get; set; }
		public string Label { get; set; }
		public string Connected { get; set; }
		public bool Power { get; set; }
		public LifxColor Color { get; set; }
		public float Brightness { get; set; }
		public LifxGroup Group { get; set; }
		public LifxLocation Location { get; set; }
		public string ProductName { get; set; }
		public LifxCapability Capabilities { get; set; }
		public string LastSeen { get; set; }
		public double SecondsSinceSeen { get; set; }

		public LifxBulb(string json, LifxManager manager)
		{

			JToken bulbData = JObject.Parse(json);
			this.manager = manager;
			id = bulbData.Value<string>("id");
			Uuid = bulbData.Value<string>("uuid");
			Label = bulbData.Value<string>("label");
            Connected = bulbData.Value<bool>("connected") ? "Online" : "Offline";
            Power = bulbData.Value<string>("power").Equals("on") && Connected.Equals("Online") ? true : false;
			Brightness = bulbData.Value<float>("brightness");
			Color = new LifxColor(bulbData.Value<JToken>("color").ToString());
			Group = new LifxGroup(bulbData.Value<JToken>("group").ToString(), manager);
			Location = new LifxLocation(bulbData.Value<JToken>("location").ToString(), manager);
			ProductName = bulbData.Value<string>("product_name");
			//Capabilities = new LifxCapability(bulbData.Value<JToken>("capabilities").ToString());
			LastSeen = bulbData.Value<string>("last_seen");
			SecondsSinceSeen = bulbData.Value<double>("seconds_since_seen");


		}
	}
}
