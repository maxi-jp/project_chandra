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
        private BackgroundLayerB bgLayerC;
        private List<BackgroundLayerB> bgLayerList;
        private int[] rectangleMap;

        public BackgroundGameB(LevelB level)
        {
            rectangleMap = level.GetLevelMap();

            Vector2 middleScreenPos = new Vector2(level.width / 4, level.height / 2);

            bgLayerC = level.InitializeBGLayerC();
            bgLayerList = level.InitializeBGLayers();


            //level.InitializedBGLayers(bgLayerList, bgLayerC);

            // last layer
            /*int[] textureIndex = new int[1];
            textureIndex[0] = 0;
            List<Texture2D> textures0 = new List<Texture2D>();
            textures0.Add(GRMng.textureBgB00);
            bgLayer0 = new BackgroundLayerB(textureIndex, textures0, 20f, true, 1.4f, false);*/

            //bgLayer1 = new BackgroundLayerB(true, middleScreenPos, 0,  GRMng.textureBg01, -70f, true, 1);
            //bgLayer2 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg02, -90f, true, 0.7f, false, null);
            //bgLayer3 = new BackgroundLayerB(true, middleScreenPos, 0, GRMng.textureBg03, -110f, true, 0.7f);
            
            // Layer colisionable
            /*List<Texture2D> texturesC = new List<Texture2D>();
            texturesC.Add(GRMng.textureBgCol1);
            texturesC.Add(GRMng.textureBgCol2);
            texturesC.Add(GRMng.textureBgCol3);
            bgLayerC = new BackgroundLayerB(rectangleMap, texturesC, 0, false, 1, true);*/
        }

        public void Update(float deltaTime)
        {
            foreach (BackgroundLayerB bgLayer in bgLayerList)
                bgLayer.Update(deltaTime);
            if (bgLayerC != null)
                bgLayerC.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch, float scrollPosition)
        {
            foreach (BackgroundLayerB bgLayer in bgLayerList)
                bgLayer.Draw(spriteBatch, scrollPosition);
            if (bgLayerC != null)
                bgLayerC.Draw(spriteBatch, scrollPosition);
        }

        public float GetRectanglesScale()
        {
            return bgLayerC.GetScale();
        }

    } // class BackgroundGameB
}
