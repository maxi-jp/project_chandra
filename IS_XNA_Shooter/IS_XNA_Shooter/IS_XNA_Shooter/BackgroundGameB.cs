using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class BackgroundGameB
    {
        private BackgroundLayerB bgLayer0, bgLayer1, bgLayer2, bgLayer3;

        public BackgroundGameB(Level level)
        {
            Vector2 middleScreenPos = new Vector2(level.width / 4, level.height / 2);

            bgLayer0 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg00, -50f, true, 0.8f);
            //bgLayer1 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg01, -70f, true, 1);
            bgLayer2 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg02, -90f, true, 0.7f);
            //bgLayer3 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg03, -110f, true, 0.7f);
        }

        public void Update(float deltaTime)
        {
            bgLayer0.Update(deltaTime);
            //bgLayer1.Update(deltaTime);
            bgLayer2.Update(deltaTime);
            //bgLayer3.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bgLayer0.Draw(spriteBatch);
            //bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
            //bgLayer3.Draw(spriteBatch);
        }
    }
}
