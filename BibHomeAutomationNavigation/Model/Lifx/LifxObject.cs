using Xamarin.Forms;
using System.Threading.Tasks;

namespace BibHomeAutomationNavigation.LIFX.LifxObjects {
	
    public class LifxObject {
        public string id { get; set; }

        public LifxManager manager { get; set; }

        private string getSelector() {
            var selector = "id:" + id;
            if (this is LifxLocation) {
                selector = "location_" + selector;
            } else if (this is LifxGroup) {
                selector = "group_" + selector;
            }
            return selector;
        }

        public Task setColor(string color, int duration) {
            return manager.setColor(color, duration, getSelector());
        }

        public Task setColor(Color color, int duration) {
            return manager.setColor("rgb:" + color.R + "," + color.G + "," + color.B, duration, getSelector());
        }

        public Task setColor(LifxColor color, int duration) {
            return manager.setColor(color.getLifxColorString(), duration, getSelector());
        }


        public Task togglePower() {
            return manager.togglePower(getSelector());
        }

        public Task setPower(bool on) {
            return manager.setPower(on, getSelector());
        }
    }
}
