using Newtonsoft.Json.Linq;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects {

    public class LifxGroup : LifxObject {
        public string name { get; set; }
        public LifxGroup(string json, LifxManager manager) {
            this.manager = manager;
            JToken bulbData = JToken.Parse(json);
            this.name = bulbData.Value<string>("name");
            this.id = bulbData.Value<string>("id");
        }
    }
}
