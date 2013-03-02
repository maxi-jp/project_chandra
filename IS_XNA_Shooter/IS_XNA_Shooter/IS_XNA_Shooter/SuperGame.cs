using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SuperGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // estado del juego
        public enum gameState
        {
            mainMenu,
            playing,
            pause,
            gameOver
        };

        /* ------------------------------------------------------------- */
        /*                           ATRIBUTOS                           */
        /* ------------------------------------------------------------- */
        public gameState currentState; // estado actual del juego

        public static bool debug = true;

        public static int screenWidth;  // ancho de la pantalla
        public static int screenHeight; // alto de la pantalla

        private GRMng grManager;        // gestor de recursos gráficos
        public ControlMng controlMng;   // gestor de controles
        private XMLLvlMng LvlMng; // gestor de XML
        private Audio audio;            // gestor del Audio del juego

        public Vector2 pointer; // posicion del raton

        protected float totalTime; // contador del tiempo total

        // tiempo de duración de un frame en una animación:
        public static float frameTime24 = ((float)1 / 24);
        public static float frameTime12 = ((float)1 / 12);
        public static float frameTime8 = ((float)1 / 8);

        public static float timeToResume = 2f; // t que tarda en volver después de pause

        // objetos del juego:
        private Menu menu;
        private MenuIngame menuIngame;
        private Game game;

        public enum shootType
        {
            normal,
        };

        // fuentes:
        public static SpriteFont fontDebug; // courier new 12 regular

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public SuperGame ()
        {
            graphics = new GraphicsDeviceManager(this);
            LvlMng = new XMLLvlMng();
            grManager = new GRMng(Content);
            controlMng = new ControlMng();
            audio = new Audio(Content);

            //int resX = 1280, resY = 720;
            //int resX = 1366, resY = 768;
            int resX = 1024, resY = 768;
            graphics.PreferredBackBufferWidth = resX;
            graphics.PreferredBackBufferHeight = resY;
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            currentState = gameState.mainMenu; // ponemos el estado de juego a modo menu
            pointer = new Vector2();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // dimensiones de la pantalla:
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            // TODO: use this.Content to load your game content here

            grManager.LoadContent(0); // se cargan los recursos del menu
            grManager.LoadContent(1); // se cargan los recursos del menu ingame
            audio.LoadContent(0);

            fontDebug = Content.Load<SpriteFont>("FontDebug");

            // creamos el menu
            menu = new Menu(this);
            menuIngame = new MenuIngame(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            // tiempo que ha pasado desde la ultima vez que ejecutamos el metodo
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                debug = !debug;

            // posición actual del ratón:
            pointer.X = Mouse.GetState().X;
            pointer.Y = Mouse.GetState().Y;

            // actualizamos el juego:
            switch (currentState)
            {
                case gameState.mainMenu:
                    menu.Update(Mouse.GetState().X, Mouse.GetState().Y);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menu.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menu.Unclick(Mouse.GetState().X, Mouse.GetState().Y);
                    break;

                case gameState.playing:
                    game.Update(gameTime);
                    totalTime += deltaTime;

                    if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                        GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                        currentState = gameState.pause;
                    break;

                case gameState.pause:
                    menuIngame.Update(deltaTime, Mouse.GetState().X, Mouse.GetState().Y);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menuIngame.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menuIngame.Unclick(Mouse.GetState().X, Mouse.GetState().Y);
                    break;

                case gameState.gameOver:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // TODO: Add your drawing code here
            switch (currentState)
            {
                case gameState.mainMenu:
                    menu.Draw(spriteBatch);
                    break;

                case gameState.playing:
                    game.Draw(spriteBatch);
                    break;

                case gameState.pause:
                    game.Draw(spriteBatch);
                    menuIngame.Draw(spriteBatch);
                    break;

                case gameState.gameOver:
                    break;
            }

            // fps:
            if (debug)
                spriteBatch.DrawString(SuperGame.fontDebug,
                    "FPS=" + (float)1 / gameTime.ElapsedGameTime.Milliseconds * 1000 + ".",
                    new Vector2(screenWidth-100, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /* ------------------------------------------------------------- */
        /*                            MÉTODOS                            */
        /* ------------------------------------------------------------- */
        public void newHistory()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(1); // cargamos los rectangulos
            LvlMng.LoadContent(0); // cargamos enemigos del levelA

            game = new GameB( GRMng.textureAim, 200f);
           
            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            grManager.UnloadContent(0); // descargamos los recursos del menú
        }

        public void newSurvival()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0); // cargamos los XML

            game = new GameA(1, GRMng.textureAim, GRMng.textureCell, 200f);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del menú
        }

        public void newKiller()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0); // cargamos los XML

            game = new GameA(1, GRMng.textureAim, GRMng.textureCell, 200f);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del menú

        }

        public void newDefense()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0);

            game = new GameA(1, GRMng.textureAim, GRMng.textureCell, 200f);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del menú
        }

        public void newScroll()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(1); // cargamos los rectangulos

            game = new GameScroll(GRMng.textureAim, 200f);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(1);
            grManager.UnloadContent(0); // descargamos los recursos del menú
        }

        public void Resume()
        {
            currentState = gameState.playing;
        }

        public void ExitToMenu()
        {
            grManager.LoadContent(0);
            currentState = gameState.mainMenu;
            menu.menuState = Menu.MenuState.main;
            grManager.UnloadContent(2);
            audio.UnloadContent(1);
        }

    } // class SuperGame

} // namespace IS_XNA_Shooter
