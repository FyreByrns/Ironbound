using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprite = PixelEngine.Sprite;

namespace Ironbound.Display {
    public static class SpriteManager {
        public static string SpritePath => @"sprites\";
        public static string ListFile => @"default.txt";
        public static string SpriteListPath => $"{SpritePath}\\{ListFile}";
        public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

        static SpriteManager() {

        }
    }
}
