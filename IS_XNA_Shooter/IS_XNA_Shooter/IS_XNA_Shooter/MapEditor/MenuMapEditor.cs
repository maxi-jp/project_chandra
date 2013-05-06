using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using IS_XNA_Shooter.MapEditor;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class manage the menu of the map editor.
    /// </summary>
    public class MenuMapEditor
    {
        /// <summary>
        /// States of the map editor
        /// </summary>
        private enum State
        {
            SelectTypeGame,
            SelectSizeGame
        }


        //---------------------------------------------------------------


        /// <summary>
        /// The current state of the menu of map editor.
        /// </summary>
        private State currentState;
        /// <summary>
        /// The SuperGame to return to the SuperGame.
        /// </summary>
        private SuperGame mainGame;

        #region MapEditor1
        /// <summary>
        /// Separator horizontal for options.
        /// </summary>
        private int horizontalSep;
        /// <summary>
        /// Background of the map editor.
        /// </summary>
        private Texture2D backgroundMapEditor;
        /// <summary>
        /// Items
        /// </summary>
        private MenuItem itemArcadeScroll,
                         itemArcadeSurvival,
                         itemArcadeDefense,
                         itemArcadeKiller;
                         
        #endregion;

        #region MapEditor2
        //sprite of background
        private Sprite spriteBackground;
        //sprite of the screen with we show the size of the map (width and height)
        private Sprite spriteWidthHeight;
        //item to set the width of the map
        private ItemChanger itemWidth;
        //item to set the height of the map
        private ItemChanger itemHeight;
        #endregion;

        private MenuItem itemBack;


        //---------------------------------------------------------------


        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="mainGame">SuperGame</param>
        public MenuMapEditor(SuperGame mainGame)
        {
            currentState = State.SelectTypeGame;

            this.mainGame = mainGame;

            horizontalSep = 46;

            backgroundMapEditor = GRMng.menuMapEditor1;
            //Button "Back"
            itemBack = new MenuItem(false, new Vector2(5, SuperGame.screenHeight - 45), GRMng.menuMain, 
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));
            //Button "Scroll"
            itemArcadeScroll = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2 - horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            //Button "Survival"
            itemArcadeSurvival = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            //Button "Defense"
            itemArcadeDefense = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            //Button "Killer"
            itemArcadeKiller = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2 + horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 360, 512, 40), new Rectangle(0, 400, 512, 40), new Rectangle(0, 440, 512, 40));
        }


        //---------------------------------------------------------------


        private void buildScreenSize()
        {
            //background
            spriteBackground = new Sprite(false, Vector2.Zero, 0f, GRMng.menuMapEditor2);
            //screen of width-height
            spriteWidthHeight = new Sprite(true, 
                new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight * GRMng.relationHeightScreenSizesMapEditor2), 0f, 
                GRMng.screenSizesMapEditor2);
            //itemWidth
            itemWidth = new ItemChanger(GRMng.numberItemsSizeMapEditor2, GRMng.numberStatesSizeMapEditor2, GRMng.sizesMapEditor2, 
                new Vector2(SuperGame.screenWidth * GRMng.relationWidthSizesMapEditor2, SuperGame.screenHeight * GRMng.relationHeightWidthMapEditor2));
            //itemHeight
            itemHeight = new ItemChanger(GRMng.numberItemsSizeMapEditor2, GRMng.numberStatesSizeMapEditor2, GRMng.sizesMapEditor2,
                new Vector2(SuperGame.screenWidth * GRMng.relationWidthSizesMapEditor2, SuperGame.screenHeight * GRMng.relationHeightHeightMapEditor2));
        }


        //---------------------------------------------------------------


        /// <summary>
        /// Test wether there is selected one Item when we do click.
        /// </summary>
        /// <param name="X">Position X of the mouse</param>
        /// <param name="Y">Position Y of the mouse</param>
        public void Click(int X, int Y)
        {
            switch (currentState)
            {
                case State.SelectTypeGame :
                    itemArcadeScroll.Click(X, Y);
                    itemArcadeKiller.Click(X, Y);
                    itemArcadeSurvival.Click(X, Y);
                    itemArcadeDefense.Click(X, Y);
                    itemBack.Click(X, Y);
                    break;

                case State.SelectSizeGame :
                    break;
            }
            
        }

        /// <summary>
        /// Test whether there is selected one Item when we do unclick.
        /// </summary>
        /// <param name="X">Position X of the mouse</param>
        /// <param name="Y">Position Y of the mouse</param>
        public void Unclick(int X, int Y)
        {
            switch (currentState)
            {
                case State.SelectTypeGame:
                    if (itemArcadeScroll.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        currentState = State.SelectSizeGame;
                        buildScreenSize();
                    }
                    else if (itemArcadeKiller.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        currentState = State.SelectSizeGame;
                        buildScreenSize();
                    }
                    else if (itemArcadeSurvival.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        currentState = State.SelectSizeGame;
                        buildScreenSize();
                    }
                    else if (itemArcadeDefense.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        currentState = State.SelectSizeGame;
                        buildScreenSize();
                    }
                    break;

                case State.SelectSizeGame :
                    break;
            }
        }


        //---------------------------------------------------------------


        /// <summary>
        /// Method Update.
        /// </summary>
        /// <param name="X">Position X of the mouse</param>
        /// <param name="Y">Position Y of the mouse</param>
        public void Update(int X, int Y)
        {
            switch (currentState)
            {
                case State.SelectTypeGame :
                    itemArcadeScroll.Update(X, Y);
                    itemArcadeSurvival.Update(X, Y);
                    itemArcadeDefense.Update(X, Y);
                    itemArcadeKiller.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;

                case State.SelectSizeGame :
                    itemWidth.Update();
                    itemHeight.Update();
                    break;
            }
        }

        /// <summary>
        /// Method draw.
        /// </summary>
        /// <param name="spriteBatch">To draw textures.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case State.SelectTypeGame:
                    spriteBatch.Draw(backgroundMapEditor, Vector2.Zero, Color.White);
                    itemArcadeScroll.Draw(spriteBatch);
                    itemArcadeSurvival.Draw(spriteBatch);
                    itemArcadeDefense.Draw(spriteBatch);
                    itemArcadeKiller.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);
                    break;

                case State.SelectSizeGame:
                    spriteBackground.Draw(spriteBatch);
                    spriteWidthHeight.Draw(spriteBatch);
                    itemWidth.Draw(spriteBatch);
                    itemHeight.Draw(spriteBatch);
                    break;
            }
        }

    }//class MenuMapEditor
 }


    

