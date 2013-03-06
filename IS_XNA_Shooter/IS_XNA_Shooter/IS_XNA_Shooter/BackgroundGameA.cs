using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class BackgroundGameA
    {
        private BackgroundLayer bgLayer0, bgLayer1, bgLayer2, bgLayer3;

        public BackgroundGameA(Camera camera, Level level)
        {
            Vector2 middleScreenPos = new Vector2(level.width /2, level.height / 2);

            bgLayer0 = new BackgroundLayer(camera, level, true, middleScreenPos, 0, GRMng.textureBg00, 0.04f, false, 0.8f);
            bgLayer1 = new BackgroundLayer(camera, level, true, middleScreenPos, 0, GRMng.textureBg01, 0.09f, false, 1);
            bgLayer2 = new BackgroundLayer(camera, level, true, middleScreenPos, 0, GRMng.textureBg02, 0.2f, false, 0.7f);
            bgLayer3 = new BackgroundLayer(camera, level, true, middleScreenPos*1.3f, 0, GRMng.textureBg03, 0.4f, false, 0.7f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bgLayer0.Draw(spriteBatch);
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
            bgLayer3.Draw(spriteBatch);
        }

    } // BackgroundGameA
}
