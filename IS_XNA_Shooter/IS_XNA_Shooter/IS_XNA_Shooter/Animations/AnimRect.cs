using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class AnimRect
    {
        public Rectangle rect;
        public Texture2D texture;

        public AnimRect(int X, int Y, int W, int H, Texture2D texture)
        {
            this.rect = new Rectangle(X, Y, W, H);
            this.texture = texture;
        }
    } // class AnimRect
}
