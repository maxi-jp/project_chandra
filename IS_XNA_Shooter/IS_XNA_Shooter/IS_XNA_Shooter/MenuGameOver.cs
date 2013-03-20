using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    // clase para manejar el menú de game over del juego
    public class MenuGameOver
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private SuperGame mainGame;
        private int horizontalSep; // separación horizontal de las opciones
        private Vector2 mainMenuPosition; // posicion de la opcion "back"

        private Texture2D splash;

        private MenuItem itemMainMenu;

        /* ------------------- CONSTRUCTORS ------------------- */
        public MenuGameOver(SuperGame mainGame)
        {
            this.mainGame = mainGame;

            horizontalSep = 48;
            mainMenuPosition = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight - 100);

            splash = GRMng.gameOverSplash;

            itemMainMenu = new MenuItem(true, mainMenuPosition, GRMng.menuGameOver,
                new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40),
                new Rectangle(0, 80, 512, 40));
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(int X, int Y)
        {
            itemMainMenu.Update(X, Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splash, Vector2.Zero, Color.White);
            itemMainMenu.Draw(spriteBatch);
        }

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
            itemMainMenu.Click(X, Y);
        }

        // comprueba si se ha "soltado" la selección
        public void Unclick(int X, int Y)
        {
            if (itemMainMenu.Unclick(X, Y))
            {
                Audio.PlayEffect("digitalAcent01");
                mainGame.ExitToMenu();
            }
        }

    } // class Menu
}
