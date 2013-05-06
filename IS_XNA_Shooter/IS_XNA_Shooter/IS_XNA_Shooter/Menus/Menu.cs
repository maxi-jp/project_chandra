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
            story,
            arcade,
            mapEditor,
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

        private Texture2D backgroundMapEditor;

        private Texture2D splash01;
        private Sprite splash02;
        private float splash02RotationVelocity = -0.02f;
        private Asteroid asteroid;

        private Sprite spriteHistTitle, spriteArcadeTitle, spriteConfigTitle;
        private MenuItem itemBack;
        private MenuItem itemMainStory, itemMainArcade, itemMainScores; //itemMainMapEditor
        private MenuItem itemMainChangeProfile, itemMainConfig, itemMainQuit;
        private MenuItem itemStoryContinue, itemStoryLoad, itemStoryNew;
        private MenuItem itemArcadeScroll, itemArcadeSurvival, itemArcadeDefense, itemArcadeKiller;
        private MenuItem itemConfigControlls, itemConfigGraphics, itemConfigAudio, itemConfigProfile;

        private Evolution evolution;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Menu(SuperGame mainGame)
        {
            this.mainGame = mainGame;
            menuState = MenuState.main;

            horizontalSep = 46;
            backButtonPosition = new Vector2(5, SuperGame.screenHeight - 45);

            splash01 = GRMng.menuSplash01; // the main background
            splash02 = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight + 10),
                1, GRMng.menuSplash02); // the planet

            backgroundMapEditor = GRMng.menuMapEditor1;

            
            asteroid = new Asteroid(true, new Vector2(SuperGame.screenWidth+100, -100),
                1, GRMng.menuSplash03);

            itemBack = new MenuItem(false, backButtonPosition, GRMng.menuMain,
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));

            // main Menu
            itemMainStory = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep),
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

            // menu Story
            spriteHistTitle = new Sprite(false, Vector2.Zero, 0, GRMng.menuStory, new Rectangle(0, 360, 160, 40));
            itemStoryContinue = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - horizontalSep),
                GRMng.menuStory, new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40), new Rectangle(0, 80, 512, 40));
            itemStoryLoad = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2),
                GRMng.menuStory, new Rectangle(0, 120, 512, 40), new Rectangle(0, 160, 512, 40), new Rectangle(0, 200, 512, 40));
            itemStoryNew = new MenuItem(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + horizontalSep),
                GRMng.menuStory, new Rectangle(0, 240, 512, 40), new Rectangle(0, 280, 512, 40), new Rectangle(0, 320, 512, 40));

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
            spriteConfigTitle = new Sprite(false, new Vector2(0, 10), 0, GRMng.menuConfig, new Rectangle(0, 480, 330, 32));
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
        public void Update(int X, int Y, float deltaTime)
        {
            splash02.rotation += splash02RotationVelocity * deltaTime;
            asteroid.Update(deltaTime);

            switch (menuState)
            {
                case MenuState.main:
                    itemMainStory.Update(X, Y);
                    itemMainArcade.Update(X, Y);
                    //itemMainMapEditor.Update(X, Y);
                    itemMainScores.Update(X, Y);
                    itemMainChangeProfile.Update(X, Y);
                    itemMainConfig.Update(X, Y);
                    itemMainQuit.Update(X, Y);
                    break;

                case MenuState.story:
                    itemStoryContinue.Update(X, Y);
                    itemStoryLoad.Update(X, Y);
                    itemStoryNew.Update(X, Y);
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
                    itemArcadeScroll.Update(X, Y);
                    itemArcadeSurvival.Update(X, Y);
                    itemArcadeDefense.Update(X, Y);
                    itemArcadeKiller.Update(X, Y);
                    itemBack.Update(X, Y);
                 /*   itemConfigControlls.Update(X, Y);
                    itemConfigGraphics.Update(X, Y);
                    itemConfigAudio.Update(X, Y);
                    itemConfigProfile.Update(X, Y);
                    itemBack.Update(X, Y);*/
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splash01, Vector2.Zero, Color.White);
            splash02.Draw(spriteBatch);

            asteroid.Draw(spriteBatch);

            switch (menuState)
            {
                case MenuState.main:
                    itemMainStory.Draw(spriteBatch);
                    itemMainArcade.Draw(spriteBatch);
                    itemMainScores.Draw(spriteBatch);
                    itemMainChangeProfile.Draw(spriteBatch);
                    itemMainConfig.Draw(spriteBatch);
                    itemMainQuit.Draw(spriteBatch);
                    break;

                case MenuState.story:
                    spriteHistTitle.DrawRectangle(spriteBatch);
                    itemStoryContinue.Draw(spriteBatch);
                    itemStoryLoad.Draw(spriteBatch);
                    itemStoryNew.Draw(spriteBatch);
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

                    spriteBatch.Draw(backgroundMapEditor, Vector2.Zero, Color.White);
                    itemArcadeScroll.Draw(spriteBatch);
                    itemArcadeSurvival.Draw(spriteBatch);
                    itemArcadeDefense.Draw(spriteBatch);
                    itemArcadeKiller.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);

          /*          spriteConfigTitle.DrawRectangle(spriteBatch);
                    itemConfigControlls.Draw(spriteBatch);
                    itemConfigGraphics.Draw(spriteBatch);
                    itemConfigAudio.Draw(spriteBatch);
                    itemConfigProfile.Draw(spriteBatch);
                    itemBack.Draw(spriteBatch);*/
                    break;
            }
        } // Draw

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
            switch (menuState)
            {
                case MenuState.main:
                    itemMainStory.Click(X, Y);
                    itemMainArcade.Click(X, Y);
                    //itemMainMapEditor.Click(X, Y);
                    itemMainScores.Click(X, Y);
                    itemMainConfig.Click(X, Y);
                    itemMainQuit.Click(X, Y);
                    break;

                case MenuState.story:
                    itemBack.Click(X, Y);
                    itemStoryNew.Click(X, Y);
                    break;

                case MenuState.arcade:
                    itemBack.Click(X, Y);
                    itemArcadeScroll.Click(X, Y);
                    itemArcadeKiller.Click(X, Y);
                    itemArcadeSurvival.Click(X, Y);
                    itemArcadeDefense.Click(X, Y);
                    break;

                case MenuState.config:
                    itemArcadeScroll.Click(X, Y);
                    itemArcadeKiller.Click(X, Y);
                    itemArcadeSurvival.Click(X, Y);
                    itemArcadeDefense.Click(X, Y);
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
                    if (itemMainStory.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.story;
                    }
                    else if (itemMainArcade.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.arcade;
                    }
                /*    else if (itemMainMapEditor.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.mapEditor;
                    
                    }*/
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

                case MenuState.story:
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;
                    }
                    else if (itemStoryNew.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.currentState = SuperGame.gameState.evolution;
                        evolution.setGameState(Evolution.GameState.story);
                        //mainGame.NewStory();
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
                        mainGame.currentState = SuperGame.gameState.evolution;
                        evolution.setGameState(Evolution.GameState.scroll);
                        //mainGame.newScroll(1);
                    }
                    else if (itemArcadeKiller.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.currentState = SuperGame.gameState.evolution;
                        evolution.setGameState(Evolution.GameState.killer);
                        //mainGame.newKiller(0);
                    }
                    else if (itemArcadeSurvival.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.currentState = SuperGame.gameState.evolution;
                        evolution.setGameState(Evolution.GameState.survival);
                        //mainGame.NewSurvival(2);
                    }
                    else if (itemArcadeDefense.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.currentState = SuperGame.gameState.evolution;
                        evolution.setGameState(Evolution.GameState.defense);
                        //mainGame.newDefense(0);
                    }
                    break;

                case MenuState.config:
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;

                        //Escribe aqui
                    }
                    else if (itemArcadeScroll.Unclick(X, Y))
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
                    break;
                    /*if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        menuState = MenuState.main;
                    }
                    break;*/
            }
        }

        public void setEvolution(Evolution evolution)
        {
            this.evolution = evolution;
        }

    } // class Menu
}
