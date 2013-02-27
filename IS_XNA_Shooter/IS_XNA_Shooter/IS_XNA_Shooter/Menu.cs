using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    // clase para manejar el menú principal del juego
    public class Menu
    {
        public enum MenuState
        {
            profile,
            main,
            history,
            arcade,
            config,
            controlls,
            graphics,
            audio,
            confprofile
        };

        /* ------------------- ATRIBUTOS ------------------- */
        public MenuState menuState;

        private SuperGame mainGame;
        private int horizontalSep; // separación horizontal de las opciones
        private Vector2 backButtonPosition; // posicion de la opcion "back"

        private Sprite spriteHistTitle, spriteArcadeTitle, spriteConfigTitle;
        private MenuItem itemBack;
        private MenuItem itemMainHistory, itemMainArcade, itemMainScores;
        private MenuItem itemMainChangeProfile, itemMainConfig, itemMainQuit;
        private MenuItem itemHistoryContinue, itemHistoryLoad, itemHistoryNew;
        private MenuItem itemArcadeScroll, itemArcadeSurvival, itemArcadeDefense, itemArcadeKiller;
        private MenuItem itemConfigControlls, itemConfigGraphics, itemConfigAudio, itemConfigProfile;


        /* ------------------- CONSTRUCTORES ------------------- */
        public Menu(SuperGame mainGame)
        {
            this.mainGame = mainGame;
            menuState = MenuState.main;

            horizontalSep = 48;
            backButtonPosition = new Vector2(5, SuperGame.screenHeight - 45);

            itemBack = new MenuItem(false, backButtonPosition, GRMng.menuMain,
                new Rectangle(120, 360, 100, 40), new Rectangle(220, 360, 100, 40), new Rectangle(320, 360, 100, 40));

            // main Menu
            itemMainHistory = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep),
                GRMng.menuMain, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemMainArcade = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2),
                GRMng.menuMain, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemMainScores = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep),
                GRMng.menuMain, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            itemMainConfig = new MenuItem(false, new Vector2(10, SuperGame.screenHeight - 70), GRMng.menuMain,
                new Rectangle(0, 400, 220, 30), new Rectangle(0, 430, 220, 30), new Rectangle(0, 460, 220, 30));
            itemMainChangeProfile = new MenuItem(false, new Vector2(10, SuperGame.screenHeight - 40), GRMng.menuMain,
                new Rectangle(220, 400, 220, 30), new Rectangle(220, 430, 220, 30), new Rectangle(220, 460, 220, 30));
            itemMainQuit = new MenuItem(false, new Vector2(SuperGame.screenWidth-45, 5), GRMng.menuMain,
                new Rectangle(0, 360, 40, 40), new Rectangle(40, 360, 40, 40), new Rectangle(80, 360, 40, 40));

            // menu History
            spriteHistTitle = new Sprite(false, Vector2.Zero, 0, GRMng.menuHistory, new Rectangle(0, 360, 160, 40));
            itemHistoryContinue = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep),
                GRMng.menuHistory, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemHistoryLoad = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2),
                GRMng.menuHistory, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemHistoryNew = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep),
                GRMng.menuHistory, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));

            // menu Arcade
            spriteArcadeTitle = new Sprite(false, Vector2.Zero, 0, GRMng.menuArcade, new Rectangle(0, 480, 160, 32));
            itemArcadeScroll = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2 - horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemArcadeSurvival = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemArcadeDefense = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            itemArcadeKiller = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2 + horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 360, 512, 40), new Rectangle(0, 400, 512, 40), new Rectangle(0, 440, 512, 40));

            // menu Config
            spriteConfigTitle = new Sprite(false, new Vector2(0, 10), 0, GRMng.menuConfig, new Rectangle(0, 480, 300, 32));
            itemConfigControlls = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2 - horizontalSep),
                GRMng.menuConfig, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemConfigGraphics = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2),
                GRMng.menuConfig, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemConfigAudio = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2),
                GRMng.menuConfig, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            itemConfigProfile = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2 + horizontalSep),
                GRMng.menuConfig, new Rectangle(0, 360, 512, 40), new Rectangle(0, 400, 512, 40), new Rectangle(0, 440, 512, 40));
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(int X, int Y)
        {
            switch (menuState)
            {
                case MenuState.main:
                    itemMainHistory.Update(X, Y);
                    itemMainArcade.Update(X, Y);
                    itemMainScores.Update(X, Y);
                    itemMainChangeProfile.Update(X, Y);
                    itemMainConfig.Update(X, Y);
                    itemMainQuit.Update(X, Y);
                    break;

                case MenuState.history:
                    itemHistoryContinue.Update(X, Y);
                    itemHistoryLoad.Update(X, Y);
                    itemHistoryNew.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;

                case MenuState.arcade:
                    itemArcadeScroll.Update(X, Y);
                    itemArcadeSurvival.Update(X, Y);
                    itemArcadeDefense.Update(X, Y);
                    itemArcadeKiller.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;

                case MenuState.config:
                    itemConfigControlls.Update(X, Y);
                    itemConfigGraphics.Update(X, Y);
                    itemConfigAudio.Update(X, Y);
                    itemConfigProfile.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (menuState)
            {
                case MenuState.main:
                    itemMainHistory.Draw(spriteBatch);
                    itemMainArcade.Draw(spriteBatch);
                    itemMainScores.Draw(spriteBatch);
                    itemMainChangeProfile.Draw(spriteBatch);
                    itemMainConfig.Draw(spriteBatch);
                    itemMainQuit.Draw(spriteBatch);
                    break;

                case MenuState.history:
                    spriteHistTitle.DrawRectangle(spriteBatch);
                    itemHistoryContinue.Draw(spriteBatch);
                    itemHistoryLoad.Draw(spriteBatch);
                    itemHistoryNew.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);
                    break;

                case MenuState.arcade:
                    spriteArcadeTitle.DrawRectangle(spriteBatch);
                    itemArcadeScroll.Draw(spriteBatch);
                    itemArcadeSurvival.Draw(spriteBatch);
                    itemArcadeDefense.Draw(spriteBatch);
                    itemArcadeKiller.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);
                    break;

                case MenuState.config:
                    spriteConfigTitle.DrawRectangle(spriteBatch);
                    itemConfigControlls.Draw(spriteBatch);
                    itemConfigGraphics.Draw(spriteBatch);
                    itemConfigAudio.Draw(spriteBatch);
                    itemConfigProfile.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);
                    break;
            }
        }

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
            switch (menuState)
            {
                case MenuState.main:
                    itemMainHistory.Click(X, Y);
                    itemMainArcade.Click(X, Y);
                    itemMainScores.Click(X, Y);
                    itemMainConfig.Click(X, Y);
                    itemMainQuit.Click(X, Y);
                    break;

                case MenuState.history:
                    itemBack.Click(X, Y);
                    itemHistoryNew.Click(X, Y);
                    break;

                case MenuState.arcade:
                    itemBack.Click(X, Y);
                    itemArcadeScroll.Click(X, Y);
                    itemArcadeKiller.Click(X, Y);
                    itemArcadeSurvival.Click(X, Y);
                    itemArcadeDefense.Click(X, Y);
                    break;

                case MenuState.config:
                    itemBack.Click(X, Y);
                    break;
            }
        }

        // comprueba si se ha "soltado" la selección
        public void Unclick(int X, int Y)
        {
            switch (menuState)
            {
                case MenuState.main:
                    if (itemMainHistory.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.history;
                    }
                    else if (itemMainArcade.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.arcade;
                    }
                    else if (itemMainScores.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                    }
                    else if (itemMainConfig.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.config;
                    }
                    else if (itemMainQuit.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.Exit();
                    }
                    break;

                case MenuState.history:
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;
                    }
                    else if (itemHistoryNew.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.newHistory();
                    }
                    break;

                case MenuState.arcade:
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;
                    }
                    else if (itemArcadeScroll.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.newScroll();
                    }
                    else if (itemArcadeKiller.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.newKiller();
                    }
                    else if (itemArcadeSurvival.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.newSurvival();
                    }
                    else if (itemArcadeDefense.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.newDefense();
                    }
                    break;

                case MenuState.config:
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;
                    }
                    break;
            }
        }

    } // class Menu
}
