using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ironbound.GUI {
    public static class UIManager {
        public static int ScreenWidth, ScreenHeight;
        public static List<UIObject> UIObjects => uiObjects;
        static List<UIObject> uiObjects = new List<UIObject>();


    }

    public class UIObject {
        public string name;
        public int x, y, width, height;

        public virtual void Interact() { }
    }
}
