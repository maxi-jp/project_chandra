using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class that manage the background of game B
    /// </summary>
    class BackgroundGameB
    {
        /// <summary>
        /// Layer 0
        /// </summary>
        private BackgroundLayerB bgLayer0;

        /// <summary>
        /// Layer 1
        /// </summary>
        private BackgroundLayerB bgLayer1;

        /// <summary>
        /// Layer 2
        /// </summary>
        private BackgroundLayerB bgLayer2;

        /// <summary>
        /// Layer 3
        /// </summary>
        private BackgroundLayerB bgLayer3;

        /// <summary>
        /// Layer of collisions
        /// </summary>
        private BackgroundLayerB bgLayerC;

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Builds the BarckgroundGameB
        /// </summary>
        /// <param name="level"> The level </param>
        public BackgroundGameB(Level level)
        {
            Vector2 middleScreenPos = new Vector2(level.width / 4, level.height / 2);

            bgLayer0 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg00, 0, true, 0.8f, false, null);
            //bgLayer1 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg01, -70f, true, 1);
            bgLayer2 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg02, 0, true, 0.7f, false, null);
            //bgLayer3 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg03, -110f, true, 0.7f);
            //bgLayerC =new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBgCol1,-200f,true,0.7f, true, ((LevelB)level).getRectangles());
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the background
        /// </summary>
        /// <param name="deltaTime"> Time between the last time and this </param>
        public void Update(float deltaTime)
        {
            bgLayer0.Update(deltaTime);
            //bgLayer1.Update(deltaTime);
            bgLayer2.Update(deltaTime);
            //bgLayer3.Update(deltaTime);
            //bgLayerC.Update(deltaTime);
        }

        /// <summary>
        /// Draw the background
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            bgLayer0.Draw(spriteBatch);
            //bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
            //bgLayer3.Draw(spriteBatch);
            //bgLayerC.Draw(spriteBatch);
        }

        //-----------------------------------------------------------------------------------------------------------------

        internal void Dispose()
        {
            //TO DO
        }
    }
}
