using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using IS_XNA_Shooter.MapEditor;
using IS_XNA_Shooter.Input;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class manage the menu of the map editor.
    /// </summary>
    public class MenuMapEditor
    {
        // States of the map editor.
        private enum State
        {
            SelectTypeGame,
            SelectSizeGame,
            MainMapEditor
        }


        //---------------------------------------------------------------


        // The current state of the menu of map editor.
        private State currentState;
        // The SuperGame to return to the SuperGame.
        private SuperGame mainGame;
        // The menu to return there.
        private Menu mainMenu;

        #region MapEditor1
        // Separator horizontal for options.
        private int horizontalSep;
        // Background of the map editor.
        private Texture2D backgroundMapEditor;
        // Items
        private MenuItem itemArcadeScroll,
                         itemArcadeSurvival,
                         itemArcadeDefense,
                         itemArcadeKiller;                         
        #endregion;

        #region MapEditor2
        //sprite of background.
        private Sprite spriteBackground;
        //sprite of the screen with we show the size of the map (width and height).
        private Sprite spriteWidthHeight;
        //item to set the width of the map.
        private ItemInput itemWidth;
        //item to set the height of the map.
        private ItemInput itemHeight;
        #endregion;

        // Button to go back.
        private MenuItem itemBack;
        // Button to go to next screen.
        private MenuItem itemContinue;

        //Attributes for the information message
        private String informationText;
        private Vector2 informationPosition;

        //Third screen
        MainMapEditor mainMapEditor;

        


        //---------------------------------------------------------------


        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="mainGame">SuperGame</param>
        /// <param name="mainMenu">The main menu</param>
        public MenuMapEditor(SuperGame mainGame, Menu mainMenu)
        {
            currentState = State.SelectTypeGame;

            this.mainGame = mainGame;
            this.mainMenu = mainMenu;

            horizontalSep = 46;

            backgroundMapEditor = GRMng.menuMapEditor1;
            //Button "Back"
            itemBack = new MenuItem(false, new Vector2(5, SuperGame.screenHeight - 45), GRMng.menuMain, 
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));
            //Button "Continue"
            itemContinue = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight - 45), 
                GRMng.menuStory, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            //Button "Scroll"
            itemArcadeScroll = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, 
                SuperGame.screenHeight / 2 - horizontalSep / 2 - horizontalSep), GRMng.menuArcade, 
                new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            //Button "Survival"
            itemArcadeSurvival = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, 
                SuperGame.screenHeight / 2 - horizontalSep / 2), GRMng.menuArcade, 
                new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            //Button "Defense"
            itemArcadeDefense = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, 
                SuperGame.screenHeight / 2 + horizontalSep / 2), GRMng.menuArcade, new Rectangle(0, 240, 512, 40), 
                new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            //Button "Killer"
            itemArcadeKiller = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, 
                SuperGame.screenHeight / 2 + horizontalSep / 2 + horizontalSep), GRMng.menuArcade, 
                new Rectangle(0, 360, 512, 40), new Rectangle(0, 400, 512, 40), new Rectangle(0, 440, 512, 40));
        }


        //---------------------------------------------------------------

        // It's like the builder of the size screen
        private void buildScreenSize()
        {
            //background
            spriteBackground = new Sprite(false, Vector2.Zero, 0f, GRMng.menuMapEditor2);
            //screen of width-height
            spriteWidthHeight = new Sprite(true, 
                new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight * GRMng.relationHeightScreenSizesMapEditor2), 0f, 
                GRMng.screenSizesMapEditor2);
            //itemWidth
            itemWidth = new ItemInput(new Vector2(SuperGame.screenWidth * GRMng.relationWidthSizesMapEditor2, 
                SuperGame.screenHeight * GRMng.relationHeightWidthMapEditor2));
            //itemHeight
            itemHeight = new ItemInput(new Vector2(SuperGame.screenWidth * GRMng.relationWidthSizesMapEditor2, 
                SuperGame.screenHeight * GRMng.relationHeightHeightMapEditor2));
            informationText = "Dimensions between 1000x1000 and 10000x10000";
            informationPosition = new Vector2(3*SuperGame.screenWidth/10, 2*SuperGame.screenHeight/5);
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
                    break;

                case State.SelectSizeGame :
                    itemContinue.Click(X, Y);
                    break;
                
            }

            itemBack.Click(X, Y);
            
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
                    //button "continue" goes to the screen number 3 of map editor.
                    if (itemContinue.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainMapEditor = new MainMapEditor("Arcade", itemWidth.getValue(), itemHeight.getValue());
                        currentState = State.MainMapEditor;
                        //TODO: sigue aqui Oscar!!!
                    }
                    break;
            }

            //button "back" return to other state or main screen.
            if (itemBack.Unclick(X, Y))
            {
                Audio.PlayEffect("digitalAcent01");
                if (currentState == State.SelectSizeGame)
                    currentState = State.SelectTypeGame;
                else
                    mainMenu.menuState = Menu.MenuState.main;
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
                    itemContinue.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;

                case State.MainMapEditor:
                    mainMapEditor.Update();
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
                    itemContinue.Draw(spriteBatch);
                    spriteBatch.DrawString(GRMng.fontText, informationText, informationPosition, Color.White);
                    itemBack.Draw(spriteBatch);
                    break;
                case State.MainMapEditor:
                    mainMapEditor.Draw(spriteBatch);
                    break;
            }
        }

    }//class MenuMapEditor
 }


    

