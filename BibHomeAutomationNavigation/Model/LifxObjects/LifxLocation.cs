using Newtonsoft.Json.Linq;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects {
    public class LifxLocation : LifxObject {
  
        public string name { get; set; }
          public LifxLocation(string json, LifxManager manager) {
              this.manager = manager;
              var bulbData = JToken.Parse(json);
            this.name = bulbData.Value<string>("name");
            this.id = bulbData.Value<string>("id");
        }
    }
}
