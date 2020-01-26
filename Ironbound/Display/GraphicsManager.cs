using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelEngine;

namespace Ironbound.Display {
    public static class GraphicsManager {
        public static Ironbound Game { get; private set; }
        public static Sprite DefaultSprite = Sprite.Load("")

        public static void RegisterGame(Ironbound game) {
            Game = game;
        }
    }
}
