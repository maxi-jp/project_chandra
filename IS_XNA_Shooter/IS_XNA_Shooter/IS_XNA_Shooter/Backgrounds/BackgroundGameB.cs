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
        private BackgroundLayerB bgLayer0, bgLayer1, bgLayer2, bgLayer3, bgLayerC;
        private int[] rectangleMap;

        public BackgroundGameB(Level level)
        {
            rectangleMap = ((LevelB)level).GetLevelMap();

            Vector2 middleScreenPos = new Vector2(level.width / 4, level.height / 2);

            // last layer
            int[] textureIndex = new int[1];
            textureIndex[0] = 0;
            List<Texture2D> textures0 = new List<Texture2D>();
            textures0.Add(GRMng.textureBgB00);
            bgLayer0 = new BackgroundLayerB(textureIndex, textures0, 20f, true, 1.4f, false);

            //bgLayer1 = new BackgroundLayerB(true, middleScreenPos, 0,  GRMng.textureBg01, -70f, true, 1);
            //bgLayer2 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg02, -90f, true, 0.7f, false, null);
            //bgLayer3 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg03, -110f, true, 0.7f);
            
            // Layer colisionable
            List<Texture2D> texturesC = new List<Texture2D>();
            texturesC.Add(GRMng.textureBgCol1);
            texturesC.Add(GRMng.textureBgCol2);
            texturesC.Add(GRMng.textureBgCol3);
            bgLayerC = new BackgroundLayerB(rectangleMap, texturesC, 0, false, 1, true);
        }

        public void Update(float deltaTime)
        {
            bgLayer0.Update(deltaTime);
            //bgLayer1.Update(deltaTime);
            //bgLayer2.Update(deltaTime);
            //bgLayer3.Update(deltaTime);
            bgLayerC.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch, float scrollPosition)
        {
            bgLayer0.Draw(spriteBatch, scrollPosition);
            //bgLayer1.Draw(spriteBatch);
            //bgLayer2.Draw(spriteBatch);
            //bgLayer3.Draw(spriteBatch);
            bgLayerC.Draw(spriteBatch, scrollPosition);
        }

    } // class BackgroundGameB
}
