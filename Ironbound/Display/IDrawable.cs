﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelEngine;

namespace Ironbound.Display {
    public interface IDrawable {
        Sprite GetSprite();
        void Draw(Game game);
    }
}
