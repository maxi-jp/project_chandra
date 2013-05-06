using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    public class MenuMapEditor
    {
/*        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;*/


        /* ------------------- ATRIBUTOS ------------------- */
        

        private SuperGame mainGame;
        private int horizontalSep; // separación horizontal de las opciones
        private Vector2 backButtonPosition; // posicion de la opcion "back"

        private Texture2D backgroundMapEditor;

                               
        private MenuItem itemArcadeScroll, itemArcadeSurvival, itemArcadeDefense, itemArcadeKiller, itemBack;
      

        private Evolution evolution;

        /* ------------------- CONSTRUCTORES ------------------- */
        public MenuMapEditor(SuperGame mainGame)
        {
            this.mainGame = mainGame;
            
            horizontalSep = 46;
            backButtonPosition = new Vector2(5, SuperGame.screenHeight - 45);

            
            backgroundMapEditor = GRMng.menuMapEditor1;

               
            itemBack = new MenuItem(false, backButtonPosition, GRMng.menuMain,
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));
            itemArcadeScroll = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2 - horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemArcadeSurvival = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemArcadeDefense = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2),
                GRMng.menuArcade, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));
            itemArcadeKiller = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep / 2 + horizontalSep),
                GRMng.menuArcade, new Rectangle(0, 360, 512, 40), new Rectangle(0, 400, 512, 40), new Rectangle(0, 440, 512, 40));

            
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(int X, int Y)
        {

                    itemArcadeScroll.Update(X, Y);
                    itemArcadeSurvival.Update(X, Y);
                    itemArcadeDefense.Update(X, Y);
                    itemArcadeKiller.Update(X, Y);
                    itemBack.Update(X, Y);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
                    spriteBatch.Draw(backgroundMapEditor, Vector2.Zero, Color.White);
                    itemArcadeScroll.Draw(spriteBatch);
                    itemArcadeSurvival.Draw(spriteBatch);
                    itemArcadeDefense.Draw(spriteBatch);
                    itemArcadeKiller.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);

        } // Draw

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
          
                    itemArcadeScroll.Click(X, Y);
                    itemArcadeKiller.Click(X, Y);
                    itemArcadeSurvival.Click(X, Y);
                    itemArcadeDefense.Click(X, Y);
                    itemBack.Click(X, Y);
          
        
        }

        // comprueba si se ha "soltado" la selección
        public void Unclick(int X, int Y)
        {
    
                    /*if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;

                        //Escribe aqui
                    }*/
                   // else
                    if (itemArcadeScroll.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        //Escribe aqui
                    }
                    else if (itemArcadeKiller.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        //Escribe aqui
                    }
                    else if (itemArcadeSurvival.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        //Escribe aqui
                    }
                    else if (itemArcadeDefense.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        //Escribe aqui
                    }
            }
        }


 } // class MenuMapEditor


    

