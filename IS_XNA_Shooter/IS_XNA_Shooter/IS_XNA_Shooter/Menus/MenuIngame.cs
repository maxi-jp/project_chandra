using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase para manejar el menú ingame del juego
    class MenuIngame
    {
        public enum MenuIngameState
        {
            main,
            config,
            controlls,
            graphics,
            audio,
            exit
        };

        /* ------------------- ATRIBUTOS ------------------- */
        public MenuIngameState menuState;

        private SuperGame mainGame;
        private int horizontalSep; // separación horizontal de las opciones

        private Sprite spritePause, spriteGetReady, spriteNum;
        private MenuItem itemResume, itemConfig, itemExit, itemExitYes, itemExitNo;

        private Texture2D blackpixel;
        private Rectangle screenRectangle;

        private float timeToResume, timeToResumeAux; // t de espera cuando se vuelve a la partida
        private bool isResuming;

        /* ------------------- CONSTRUCTORES ------------------- */
        public MenuIngame(SuperGame mainGame)
        {
            this.mainGame = mainGame;
            horizontalSep = 48;

            spritePause = new Sprite(false, Vector2.Zero, 0, GRMng.menuIngame, new Rectangle(0, 480, 256, 32));
            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
                GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));
            itemResume = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep),
                GRMng.menuIngame, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemConfig = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2),
                GRMng.menuIngame, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemExit = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep),
                GRMng.menuIngame, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            itemExitYes = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2),
                GRMng.menuIngame, new Rectangle(0, 360, 256, 40), new Rectangle(0, 400, 256, 40), new Rectangle(0, 440, 256, 40));
            itemExitNo = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2),
                GRMng.menuIngame, new Rectangle(256, 360, 256, 40), new Rectangle(256, 400, 256, 40), new Rectangle(256, 440, 256, 40));

            blackpixel = GRMng.blackpixeltrans;
            screenRectangle = new Rectangle(0, 0, SuperGame.screenWidth, SuperGame.screenHeight);

            timeToResume = timeToResumeAux = SuperGame.timeToResume;
            isResuming = false;
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(float deltaTime, int X, int Y)
        {
            if (isResuming)
            {
                timeToResumeAux -= deltaTime;
                if (timeToResumeAux <= 0)
                {
                    isResuming = false;
                    mainGame.Resume();
                }
                else if (timeToResumeAux >= timeToResume * 2 / 3)
                    spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                else if (timeToResumeAux >= timeToResume / 3)
                    spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));
                else
                    spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));
            }
            else
            {
                switch (menuState)
                {
                    case MenuIngameState.main:
                        itemResume.Update(X, Y);
                        itemConfig.Update(X, Y);
                        itemExit.Update(X, Y);
                        break;

                    case MenuIngameState.config:

                        break;

                    case MenuIngameState.controlls:

                        break;

                    case MenuIngameState.graphics:

                        break;

                    case MenuIngameState.audio:

                        break;

                    case MenuIngameState.exit:
                        itemExitYes.Update(X, Y);
                        itemExitNo.Update(X, Y);
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isResuming)
            {
                spriteGetReady.DrawRectangle(spriteBatch);
                spriteNum.DrawRectangle(spriteBatch);
            }
            else
            {
                switch (menuState)
                {
                    case MenuIngameState.main:
                        // aclaramos los gráficos de la partida con un sprite transparente:
                        spriteBatch.Draw(blackpixel, screenRectangle, Color.White);

                        spritePause.DrawRectangle(spriteBatch);
                        itemResume.Draw(spriteBatch);
                        itemConfig.Draw(spriteBatch);
                        itemExit.Draw(spriteBatch);
                        break;

                    case MenuIngameState.config:

                        break;

                    case MenuIngameState.controlls:

                        break;

                    case MenuIngameState.graphics:

                        break;

                    case MenuIngameState.audio:

                        break;

                    case MenuIngameState.exit:
                        spriteBatch.Draw(blackpixel, screenRectangle, Color.White);

                        spritePause.DrawRectangle(spriteBatch);
                        itemResume.Draw(spriteBatch);
                        itemConfig.Draw(spriteBatch);
                        itemExit.Draw(spriteBatch);

                        spriteBatch.Draw(blackpixel, screenRectangle, Color.White);

                        itemExitNo.Draw(spriteBatch);
                        itemExitYes.Draw(spriteBatch);
                        break;
                }
            }
        }

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
            switch (menuState)
            {
                case MenuIngameState.main:
                    itemResume.Click(X, Y);
                    itemConfig.Click(X, Y);
                    itemExit.Click(X, Y);
                    break;

                case MenuIngameState.config:

                    break;

                case MenuIngameState.controlls:

                    break;

                case MenuIngameState.graphics:

                    break;

                case MenuIngameState.audio:

                    break;

                case MenuIngameState.exit:
                    itemExitNo.Click(X, Y);
                    itemExitYes.Click(X, Y);
                    break;
            }
        }

        public void Unclick(int X, int Y)
        {
            switch (menuState)
            {
                case MenuIngameState.main:
                    if (itemResume.Unclick(X, Y))
                    {
                        timeToResumeAux = timeToResume;
                        isResuming = true;
                        //mainGame.Resume();
                    }
                    else if (itemConfig.Unclick(X, Y))
                    { }
                    else if (itemExit.Unclick(X, Y))
                        menuState = MenuIngameState.exit;
                    break;

                case MenuIngameState.config:

                    break;

                case MenuIngameState.controlls:

                    break;

                case MenuIngameState.graphics:

                    break;

                case MenuIngameState.audio:

                    break;

                case MenuIngameState.exit:
                    if (itemExitNo.Unclick(X, Y))
                        menuState = MenuIngameState.main;
                    else if (itemExitYes.Unclick(X, Y))
                        mainGame.ExitToMenu();
                    break;
            }
        }

    } // class MenuIngame
}
