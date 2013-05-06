using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IS_XNA_Shooter.MapEditor;

namespace IS_XNA_Shooter.Map_Editor
{
    /// <summary>
    /// This class show the screen to select the size of the map
    /// </summary>
    public class SelectDimensions
    {
        private static int NUMBER_ITEMS_SIZE = 4;
        private static int NUMBER_STATES_SIZE = 2;
        private static float RELATION_HEIGHT_SCREEN_WIDTH_HEIGHT = 0.625f,
                             RELATION_WIDTH_WIDTH_HEIGHT = 0.6f,
                             RELATION_HEIGHT_WIDTH = 0.58f,
                             RELATION_HEIGHT_HEIGHT = 0.67f;
                             


        //------------------------------------------------------------------------------------


        //to mode debug.
        private Boolean debug;
        private SpriteFont fontPositionMouse;
        //sprite of background
        private Sprite spriteBackground;
        //sprite of the screen with we show the size of the map (width and height)
        private Sprite spriteWidthHeight;
        //item to set the width of the map
        private ItemChanger itemWidth;
        //item to set the height of the map
        private ItemChanger itemHeight;


        //------------------------------------------------------------------------------------


        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="Content">content that contains the resources</param>
        public SelectDimensions(ContentManager Content)
        {
            //mode debug
            fontPositionMouse = Content.Load<SpriteFont>("FontDebug");
            debug = false;
            //background
            Vector2 position = Vector2.Zero;
            Texture2D texture = Content.Load<Texture2D>("Graphics/MapEditor/Screen2/Background/backgroundMapEditor_2");
            spriteBackground = new Sprite(false, position, 0f, texture);
            //screen of width-height
            position = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight * RELATION_HEIGHT_SCREEN_WIDTH_HEIGHT);
            texture = Content.Load<Texture2D>("Graphics/MapEditor/Screen2/Objects/widthHeightMapEditor_2");
            spriteWidthHeight = new Sprite(true, position, 0f, texture);
            //itemWidth
            position = new Vector2(SuperGame.screenWidth * RELATION_WIDTH_WIDTH_HEIGHT, SuperGame.screenHeight * RELATION_HEIGHT_WIDTH);
            texture = Content.Load<Texture2D>("Graphics/MapEditor/Screen2/Objects/differentSizeMapEditor_2");
            itemWidth = new ItemChanger(NUMBER_ITEMS_SIZE, NUMBER_STATES_SIZE, texture, position);
            //itemHeight
            position = new Vector2(SuperGame.screenWidth * RELATION_WIDTH_WIDTH_HEIGHT, SuperGame.screenHeight * RELATION_HEIGHT_HEIGHT);
            itemHeight = new ItemChanger(NUMBER_ITEMS_SIZE, NUMBER_STATES_SIZE, texture, position);
        }


        //------------------------------------------------------------------------------------


        /// <summary>
        /// Method Update
        /// </summary>
        public void Update()
        {
            itemWidth.Update();
            itemHeight.Update();
        }

        /// <summary>
        /// Method Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBackground.Draw(spriteBatch);
            spriteWidthHeight.Draw(spriteBatch);
            itemWidth.Draw(spriteBatch);
            itemHeight.Draw(spriteBatch);

            //mode debug
            if (debug)
            {
                spriteBatch.DrawString(fontPositionMouse, "Mouse: (" + Mouse.GetState().X + ", " + Mouse.GetState().Y + ")", Vector2.Zero, Color.White);
            }
        }
    }//SelectDimensions
}
