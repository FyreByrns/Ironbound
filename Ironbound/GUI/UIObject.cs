using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ironbound.Display;

namespace Ironbound.GUI {
    public static class UIManager {
        public static int ScreenWidth, ScreenHeight;
        public static List<UIObject> UIObjects => uiObjects;
        static List<UIObject> uiObjects = new List<UIObject>();


    }

    public class UIObject : IUpdateable, IDrawable {
        public string name;
        public int x, y, width, height;
        Rectangle Bounds => new Rectangle(x, y, width, height);

        public virtual void Interact() { }
        public virtual bool Intersects(int x, int y) => Bounds.Intersects(new Point(x, y));

        public virtual void Update(float elapsed) { }
        public virtual void Draw(PixelEngine.Game game) { }
    }
}
