using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelEngine;

namespace Ironbound {
    public class Ironbound : Game {
        public Ironbound() {
            Display.GraphicsManager.RegisterGame(this);
        }

        public override void OnCreate() {
            base.OnCreate();
        }

        public override void OnUpdate(float elapsed) {


            if (!TimeManager.Paused) {

            }
        }
    }
}
