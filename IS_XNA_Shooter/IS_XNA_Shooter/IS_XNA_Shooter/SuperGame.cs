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
        /// <summary>
        /// String that indicates the current version of the project
        /// </summary>
        public String currentVersion = "0.5";

        /// <summary>
        /// XNA's atribute for graphics
        /// </summary>
        GraphicsDeviceManager graphics;
        
        /// <summary>
        /// The screen's canvas
        /// </summary>
        SpriteBatch spriteBatch;

 

        /// <summary>
        /// Game's states
        /// </summary>
        public enum gameState
        {
            starting,
            mainMenu,
            playing,
            pause,
            gameOver,
            playingVideo
        };

        /// <summary>
        /// Video introduction mode story
        /// </summary>
        public VideoPlayer videoPlayer;

        public float duration;

        /// <summary>
        /// Game's current state
        /// </summary>
        public gameState currentState;
       
        /// <summary>
        /// Says if it's in debug's mode
        /// </summary>
        public static bool debug = true;

        /// <summary>
        /// Indicates if the god mode is active
        /// </summary>
        public static bool godMode = false;

        /// <summary>
        /// Screen's width
        /// </summary>
        public static int screenWidth;
        
        /// <summary>
        /// Screen's height
        /// </summary>
        public static int screenHeight;
       
        /// <summary>
        /// Resources' manager
        /// </summary>
        private GRMng grManager;     
        
        /// <summary>
        /// Controlls' manager
        /// </summary>
        public ControlMng controlMng;
        
        /// <summary>
        /// XML's manager
        /// </summary>
        private XMLLvlMng LvlMng;
        
        /// <summary>
        /// Audio's manager
        /// </summary>
        private Audio audio; 
        
        /// <summary>
        /// Mouse's position
        /// </summary>
        public Vector2 pointer;
        
        /// <summary>
        /// Total time
        /// </summary>
        protected float totalTime;

        /// <summary>
        /// Draw frames' counter
        /// </summary>
        private int     drawFramesCounter;
        
        /// <summary>
        /// Draw frames' auxiliar counter
        /// </summary>
        private int     drawFramesCounterAux;
        
        /// <summary>
        /// Update frames' counter
        /// </summary>
        private int     updateFramesCounter;
        
        /// <summary>
        /// Update frames' auxiliar counter
        /// </summary>
        private int     updateFramesCounterAux;
        
        /// <summary>
        /// Time frames' counter
        /// </summary>
        private float   timeCounterSecond;
        
        /// <summary>
        /// Time frames' auxiliar counter
        /// </summary>
        private float   timeCounterSecondAux;

        // Refresh time between frames:

        /// <summary>
        /// Refresh time between frames = 24
        /// </summary>
        public static float frameTime24 =   ((float)1 / 24);
        
        /// <summary>
        /// Refresh time between frames = 12
        /// </summary>
        public static float frameTime12 =   ((float)1 / 12);
        
        /// <summary>
        /// Refresh time between frames = 10
        /// </summary>
        public static float frameTime10 =   ((float)1 / 10);
        
        /// <summary>
        /// Refresh time between frames = 8
        /// </summary>
        public static float frameTime8 =    ((float)1 / 8);
        
        /// <summary>
        /// Time to resume after pause
        /// </summary>
        public static float timeToResume = 2f;

        // Game's objects:

        /// <summary>
        /// Start Menu
        /// </summary>
        private MenuStart menuStart;

        /// <summary>
        /// Menu
        /// </summary>
        private Menu        menu;
        
        /// <summary>
        /// Menu of the pause
        /// </summary>
        private MenuIngame  menuIngame;
        
        /// <summary>
        /// Menu of game over
        /// </summary>
        private MenuGameOver menuGameOver;
        
        /// <summary>
        /// Game
        /// </summary>
        private Game        game;
       
        /// <summary>
        /// Player
        /// </summary>
        public Player       player;
       
        /// <summary>
        /// Player lifes
        /// </summary>
        private int         playerLifes = 4;
        
        /// <summary>
        /// Shoot type
        /// </summary>
        public enum shootType
        {
            normal,
        };

        // fuentes:
        /// <summary>
        /// courier new 12 regular
        /// </summary>
        public static SpriteFont fontDebug; 

        /// <summary>
        /// SuperGame's constructor
        /// </summary>
        public SuperGame ()
        {
            graphics = new GraphicsDeviceManager(this);
            LvlMng = new XMLLvlMng();
            grManager = new GRMng(Content);
            controlMng = new ControlMng();
            audio = new Audio(Content);

            int resX = 1280, resY = 720;
            //int resX = 1366, resY = 768;
            //int resX = 1024, resY = 768;
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
            IsMouseVisible = true;

            drawFramesCounter = drawFramesCounterAux = 0;
            updateFramesCounter = updateFramesCounterAux = 0;
            timeCounterSecond = timeCounterSecondAux = 1;

            currentState = gameState.starting; // puts game's state to starting
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

            // Screen dimensions
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            // ALL: use this.Content to load your game content here

            grManager.LoadContent("MenuStart"); // se cargan los recursos del menu start
            grManager.LoadContent("MenuMain"); // se cargan los recursos del menu
            grManager.LoadContent("MenuIngame"); // se cargan los recursos del menu ingame
            grManager.LoadContent("MenuGameOver");// se cargan los recursos del menu gameover
            grManager.LoadContent("Other"); // all type of "little" resources
            audio.LoadContent(0);

            fontDebug = Content.Load<SpriteFont>("FontDebug");

            // Create the Menus
            menuStart =     new MenuStart(this);
            menu =          new Menu(this);
            menuIngame =    new MenuIngame(this);
            menuGameOver =  new MenuGameOver(this);

            // Create the player
            player = new Player(playerLifes);
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

            // Time since the last method's execution
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // update the frames counters:
            timeCounterSecondAux -= deltaTime;
            if (timeCounterSecondAux <= 0)
            {
                drawFramesCounter = drawFramesCounterAux;
                drawFramesCounterAux = 0;
                updateFramesCounter = updateFramesCounterAux;
                updateFramesCounterAux = 0;
                timeCounterSecondAux = timeCounterSecond;
            }
            updateFramesCounterAux++;

            // update the controls manager
            controlMng.Update(deltaTime);

            // active/deactive the debug mode:
            if (ControlMng.fPreshed && !debug)
                debug = true;
            else if (ControlMng.fPreshed && debug)
                debug = false;

            // active/deactive the god mode:
            if (ControlMng.gPreshed && !godMode)
                godMode = true;
            else if (ControlMng.gPreshed && godMode)
                godMode = false;

            // actual position of the mouse:
            pointer.X = Mouse.GetState().X;
            pointer.Y = Mouse.GetState().Y;

            // Game's update:
            switch (currentState)
            {
                case gameState.starting:

                    menuStart.Update(Mouse.GetState().X, Mouse.GetState().Y);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menuStart.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menuStart.Unclick(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case gameState.mainMenu:

                    if (debug && Keyboard.GetState().IsKeyDown(Keys.T))
                        NewGameATest();

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

                    menuGameOver.Update(Mouse.GetState().X, Mouse.GetState().Y);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menuGameOver.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menuGameOver.Unclick(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case gameState.playingVideo:
                    duration += deltaTime;
                    if (duration > GRMng.videoIntroStory.Duration.Seconds || Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentState = gameState.playing;
                        videoPlayer.Stop();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>fe
        /// 
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            drawFramesCounterAux++;

            switch (currentState)
            {
                case gameState.starting:
                    menuStart.Draw(spriteBatch);
                    spriteBatch.DrawString(fontDebug, "build version: " + currentVersion,
                        new Vector2((screenWidth / 2) - fontDebug.MeasureString("build version: " + currentVersion).X/2, 6),
                        Color.White, 0, Vector2.Zero, 1,
                        SpriteEffects.None, 0);
                    break;

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
                    menuGameOver.Draw(spriteBatch);
                    break;

                case gameState.playingVideo:
                    spriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    break;
            }

            if (debug)
            {
                // Frame counters
                spriteBatch.DrawString(SuperGame.fontDebug, "Draw FPS=" + drawFramesCounter + ".",
                    new Vector2(screenWidth - 150, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Update FPS=" + updateFramesCounter + ".",
                    new Vector2(screenWidth - 150, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                if (godMode)
                    spriteBatch.DrawString(SuperGame.fontDebug, "God mode=ON",
                        new Vector2(screenWidth - 150, 27), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        /* ---------------------------- SUPERGAME METHODS ---------------------------- */

        /// <summary>
        /// Create a GameA for tests
        /// </summary>
        private void NewGameATest()
        {
            grManager.LoadContent("LevelA1"); // Load the gameA's level 1 resources
            audio.LoadContent(1);
            game = new GameA(this, player, 0, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100000);
            currentState = gameState.playing; // Change game's state to game mode
            grManager.UnloadContent("LevelA1"); // Unload the menu's resources
        }

        /// <summary>
        /// Create a new Story
        /// </summary>
        public void NewStory()
        {
            grManager.LoadVideo(1);
            // introduction video
            videoPlayer = new VideoPlayer();
            videoPlayer.Play(GRMng.videoIntroStory);
            currentState = gameState.playingVideo;

            duration = 0;

            game = new GameStory(this, grManager, audio, LvlMng, player,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            //currentState = gameState.playing; // Change game's state to game mode
            //grManager.UnloadContent(2); 
            grManager.UnloadContent("MenuMain"); /// Unload the main menu's resources
        }

        /// <summary>
        /// Create a new Survival
        /// </summary>
        /// <param name="lvl">The number of the level</param>
        public void newSurvival(int lvl)
        {
            grManager.LoadContent("LevelA1");  // Load the gameA's level 1 resources
            audio.LoadContent(1);
            LvlMng.LoadContent(lvl); // Load XML

            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // Change game's state to game mode

            LvlMng.UnloadContent(lvl);
            grManager.UnloadContent("MenuMain"); /// Unload the main menu's resources
        }

        /// <summary>
        /// Create a new Killer
        /// </summary>
        /// <param name="lvl">The number of the level</param>
        public void newKiller(int lvl)
        {
            grManager.LoadContent("LevelA1"); // Load the gameB's level 1 resources
            audio.LoadContent(1);
            LvlMng.LoadContent(lvl); // Load XML

            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // Change game's state to game mode

            LvlMng.UnloadContent(lvl);
            grManager.UnloadContent("MenuMain"); /// Unload the main menu's resources
        }

        /// <summary>
        /// Create a new Defense
        /// </summary>
        /// <param name="lvl">The number of the level</param>
        public void newDefense(int lvl)
        {
            grManager.LoadContent("LevelB1"); // Load the gameB's level 1 resources
            audio.LoadContent(1);
            LvlMng.LoadContent(lvl);

            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // Change game's state to game mode

            LvlMng.UnloadContent(lvl);
            grManager.UnloadContent("MenuMain"); /// Unload the main menu's resources
        }

        /// <summary>
        /// Create a new Scroll
        /// </summary>
        /// <param name="lvl">The number of the level</param>
        public void newScroll(int lvl)
        {
            grManager.LoadContent("LevelB1"); // Load the gameB's level 1 resources
            audio.LoadContent(1);
            LvlMng.LoadContent(lvl); // Load the rectangles

            game = new GameB(this, player, 1, GRMng.textureAim,
                /*ShipVelocity*/200f + 200, /*ShipLife*/100);

            currentState = gameState.playing; // Change game's state to game mode

            LvlMng.UnloadContent(lvl);
            grManager.UnloadContent("MenuMain"); /// Unload the main menu's resources
        }

        /// <summary>
        /// Puts gameState = playing
        /// </summary>
        public void Resume()
        {
            currentState = gameState.playing;
        }

        /// <summary>
        /// Enter in the main menu from the start splash screen
        /// </summary>
        public void EnterToMenu()
        {
            grManager.UnloadContent("MenuStart"); // unload the gamestartmenu
            currentState = gameState.mainMenu;
            menu.menuState = Menu.MenuState.main;
        }

        /// <summary>
        /// Load the main menu content
        /// </summary>
        public void ExitToMenu()
        {
            grManager.LoadContent("MenuMain");
            currentState = gameState.mainMenu;
            menu.menuState = Menu.MenuState.main;
            grManager.UnloadContentGame();
            audio.UnloadContent(1);

            player = new Player(playerLifes);
        }

        /// <summary>
        /// Puts gameState = gameOver
        /// </summary>
        public void GameOver()
        {
            currentState = gameState.gameOver;
        }

    } // class SuperGame

} // namespace IS_XNA_Shooter
