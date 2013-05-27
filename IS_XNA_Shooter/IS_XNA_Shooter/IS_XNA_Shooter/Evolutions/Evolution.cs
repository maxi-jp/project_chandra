using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using IS_XNA_Shooter.Evolutions;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Update our ship at the begin of each game.
    /// </summary>
    public class Evolution
    {
        /// <summary>
        /// State of the mouse in the evolution screen
        /// </summary>
        private enum MouseClickState { nothing, clicked, unClicked };

        /// <summary>
        /// State of the game when it calls this class
        /// </summary>
        public enum GameState { story, scroll, killer, survival, defense }


        //-------------------------------------------------------------------------------

        /// <summary>
        /// Extra values that you improved
        /// </summary>
        private const float LIFE = 100,
                            POWER_ATTACK = 50,
                            SPEED_SHIP = 25,
                            SPEED_SHOT = 100,
                            CADENCE = 0.01f;
        /// <summary>
        /// Parameters for the graphic components
        /// </summary>
        private const float INITIAL_SPACE_BAR = 30,
                            HIGH_BAR = 30,
                            SPACE_BAR = 40,
                            SPACE_BUTTON = 10,
                            LENGTH_BUTTON = 30,
                            WIDTH_BUTTON_PREVIEW = 100,
                            HEIGHT_BUTTON_PREVIEW = 50,
                            SPACE_BUTTON_PREVIEW_BOTTOM = 10,
                            LENGTH_BUTTON_CONTINUE = 30,
                            SPACE_BUTTON_CONTINUE_BOTTOM = 10,
                            SPACE_BUTTON_CONTINUE_RIGHT = 10;

        /// <summary>
        /// Length of the frame that is showed in the preview button
        /// </summary>
        public const int LENGTH_FRAME = 500;


        //-------------------------------------------------------------------------------

        /// <summary>
        /// Current and extra values
        /// </summary>
        private float life,
                    lifeExtra,
                    powerAttack,
                    powerAttackExtra,
                    speedShip,
                    speedShipExtra,
                    speedShot,
                    speedShotExtra,
                    cadence,
                    cadenceExtra;

        /// <summary>
        /// List of how many updates are activated in every parameter.
        /// </summary>
        private List<Boolean> lifeUpdate, 
                            powerAttackUpdate, 
                            speedShipUpdate, 
                            speedShotUpdate, 
                            cadenceUpdate;

        /// <summary>
        /// Rectangles of every button
        /// </summary>
        private Rectangle lifeRectangleAdd,
                            lifeRectangleRemove,
                            powerAttackRectangleAdd,
                            powerAttackRectangleRemove,
                            speedShipRectangleAdd,
                            speedShipRectangleRemove,
                            speedShotRectangleAdd,
                            speedShotRectangleRemove,
                            cadenceRectangleAdd,
                            cadenceRectangleRemove,
                            continueRectangle,
                            previewRectangle,
                            previewFrameRectangle;

        /// <summary>
        /// Textures
        /// </summary>
        private Texture2D greenPixel, bluePixel, buttons, buttonPreview, buttonContinue, previewFrame;

        /// <summary>
        /// Sprites
        /// </summary>
        private Sprite backgroundSprite, background1, measures;

        /// <summary>
        /// Current state of the mouse
        /// </summary>
        private MouseClickState mouseState;

        /// <summary>
        /// Class that manages all games
        /// </summary>
        private SuperGame mainGame;

        /// <summary>
        /// State of the buttons (to draw the correct image, because in the images we have the button in a normal state, click state and in a over state)
        /// 0 - normal state
        /// 1 - over state
        /// 2 - click state
        /// </summary>
        private int lifeStateAdd,
            lifeStateRemove,
            powerAttackStateAdd,
            powerAttackStateRemove,
            speedShipStateAdd,
            speedShipStateRemove,
            speedShotStateAdd,
            speedShotStateRemove,
            cadenceStateAdd,
            cadenceStateRemove,
            previewState,
            continueState;

        /// <summary>
        /// Tell us if the button preview is clicked
        /// </summary>
        private bool isClickedPreview;

        /// <summary>
        /// The ship we show in the preview frame
        /// </summary>
        private ShipPreview ship;

        /// <summary>
        /// The content manager
        /// </summary>
        private ContentManager contentMng;

        /// <summary>
        /// Current state of the game
        /// </summary>
        private GameState gameState;


        //-------------------------------------------------------------------------------

        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="content"></param>
        /// <param name="mainGame"></param>
        public Evolution(ContentManager content, SuperGame mainGame)
        {                
            // initial value from the botton preview
            isClickedPreview = false;

            // initial state of add and remove buttons
            lifeStateAdd = lifeStateRemove = 0;
            powerAttackStateAdd = powerAttackStateRemove = 0;
            speedShipStateAdd = speedShipStateRemove = 0;
            speedShotStateAdd = speedShotStateRemove = 0;
            cadenceStateAdd = cadenceStateRemove = 0;
            previewState = 0;
            continueState = 0;

            // base parameters
            life = 600;
            powerAttack = 400;
            speedShip = 300;
            speedShot = 350;
            cadence = 0.2f;
            
            // evolution of base parameters
            lifeExtra = powerAttackExtra = speedShipExtra = speedShotExtra = cadenceExtra = 0;

            // number of evolutions of base parameters
            lifeUpdate = initializeList(14);
            powerAttackUpdate = initializeList(14);
            speedShipUpdate = initializeList(10);
            speedShotUpdate = initializeList(10);
            cadenceUpdate = initializeList(15);

            // textures
            greenPixel = GRMng.greenpixeltrans;
            bluePixel = GRMng.bluepixeltrans;
            buttons = content.Load<Texture2D>("Graphics/Evolution/Buttons");
            buttonPreview = content.Load<Texture2D>("Graphics/Evolution/ButtonPreview");
            buttonContinue = content.Load<Texture2D>("Graphics/Evolution/ButtonContinue");
            previewFrame = content.Load<Texture2D>("Graphics/Evolution/frame");


            Vector2 position = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2);
            backgroundSprite = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Splash/splash_mainmenu_asset01_2"));
            background1 = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Evolution/background_1"));

            position = new Vector2(SuperGame.screenWidth / 2, 17 * SuperGame.screenHeight / 32);
            measures = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Evolution/measures"));

            // position of rectangles of add and remove buttons of all parameters
            position += new Vector2(measures.texture.Width/2, -measures.texture.Height / 2 + INITIAL_SPACE_BAR);
            lifeRectangleAdd = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            lifeRectangleRemove = new Rectangle((int)(position.X + SPACE_BUTTON + LENGTH_BUTTON), (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);

            position += new Vector2(0, SPACE_BAR + HIGH_BAR);
            powerAttackRectangleAdd = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            powerAttackRectangleRemove = new Rectangle((int)(position.X + SPACE_BUTTON + LENGTH_BUTTON), (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);

            position += new Vector2(0, SPACE_BAR + HIGH_BAR);
            speedShipRectangleAdd = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            speedShipRectangleRemove = new Rectangle((int)(position.X + SPACE_BUTTON + LENGTH_BUTTON), (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);

            position += new Vector2(0, SPACE_BAR + HIGH_BAR);
            speedShotRectangleAdd = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            speedShotRectangleRemove = new Rectangle((int)(position.X + SPACE_BUTTON + LENGTH_BUTTON), (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);

            position += new Vector2(0, SPACE_BAR + HIGH_BAR);
            cadenceRectangleAdd = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            cadenceRectangleRemove = new Rectangle((int)(position.X + SPACE_BUTTON + LENGTH_BUTTON), (int)position.Y, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON);
            
            //position of rectangle of preview button
            position = background1.position + new Vector2(-WIDTH_BUTTON_PREVIEW / 2, background1.texture.Height / 2 - HEIGHT_BUTTON_PREVIEW - SPACE_BUTTON_PREVIEW_BOTTOM);
            previewRectangle = new Rectangle((int)position.X, (int)position.Y, (int)WIDTH_BUTTON_PREVIEW, (int)HEIGHT_BUTTON_PREVIEW);

            //position of rectangle of continue button
            //position = background1.position + new Vector2(-LENGTH_BUTTON_CONTINUE - SPACE_BUTTON_CONTINUE_RIGHT + background1.texture.Width / 2, background1.texture.Height / 2 - LENGTH_BUTTON_CONTINUE - SPACE_BUTTON_CONTINUE_BOTTOM);
            position = new Vector2(SuperGame.screenWidth - (int)LENGTH_BUTTON_CONTINUE, SuperGame.screenHeight - (int)LENGTH_BUTTON_CONTINUE);
            continueRectangle = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON_CONTINUE, (int)LENGTH_BUTTON_CONTINUE);

            // position of the frame (preview)
            position = new Vector2(SuperGame.screenWidth / 2 - previewFrame.Width / 2, SuperGame.screenHeight / 2 - previewFrame.Height / 2);
            previewFrameRectangle = new Rectangle((int)position.X, (int)position.Y, previewFrame.Width, previewFrame.Height);

            // state that tell us if a button has been click, unclick or you didn't do anything.
            mouseState = MouseClickState.nothing;

            // main game and content manager
            this.mainGame = mainGame;
            contentMng = content;
        }


        //-------------------------------------------------------------------------------

        /// <summary>
        /// Return us the real life
        /// </summary>
        /// <returns></returns>
        public float getLife() {
            return life + lifeExtra;
        }

        /// <summary>
        /// Return us the real power attack
        /// </summary>
        /// <returns></returns>
        public float getPowerAttack() {
            return powerAttack + powerAttackExtra;
        }

        /// <summary>
        /// Return us the real speed of our ship
        /// </summary>
        /// <returns></returns>
        public float getSpeedShip() {
            return speedShip + speedShipExtra;
        }

        /// <summary>
        /// Return us the real speed of the ship's shot
        /// </summary>
        /// <returns></returns>
        public float getSpeedShot() {
            return speedShot + speedShotExtra;
        }

        /// <summary>
        /// Return us the real cadence of the ship's shot
        /// </summary>
        /// <returns></returns>
        public float getCadence() {
            return cadence - cadenceExtra;
        }

        /// <summary>
        /// Updates the current state of the game
        /// </summary>
        /// <param name="gameState"></param>
        public void setGameState(GameState gameState)
        {
            this.gameState = gameState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="mouse"></param>
        public void Update(float deltaTime, MouseState mouse)
        {
            // if we aren't doing anything with the mouse, our mouse state is unclicked or nothing, else is clicked.
            if (mouse.LeftButton != ButtonState.Pressed)
                if (mouseState == MouseClickState.clicked) mouseState = MouseClickState.unClicked;
                else mouseState = MouseClickState.nothing;
            else mouseState = MouseClickState.clicked;

            // We look if we have the mouse in any button and if we don't activate the preview, and, after that, we look when we click and when we unclick the mouse
            if (lifeRectangleAdd.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                lifeStateAdd = 1;

                if (mouseState == MouseClickState.clicked) lifeStateAdd = 2;

                if (mouseState == MouseClickState.unClicked) addList(lifeUpdate);
            }
            else if (lifeRectangleRemove.Contains(mouse.X, mouse.Y) && !isClickedPreview) 
            {
                lifeStateRemove = 1;

                if (mouseState == MouseClickState.clicked) lifeStateRemove = 2;

                if (mouseState == MouseClickState.unClicked) removeList(lifeUpdate);
            }
            else if (powerAttackRectangleAdd.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                powerAttackStateAdd = 1;

                if (mouseState == MouseClickState.clicked) powerAttackStateAdd = 2;

                if (mouseState == MouseClickState.unClicked) addList(powerAttackUpdate);
            }
            else if (powerAttackRectangleRemove.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                powerAttackStateRemove = 1;

                if (mouseState == MouseClickState.clicked) powerAttackStateRemove = 2;

                if (mouseState == MouseClickState.unClicked) removeList(powerAttackUpdate);
            }
            else if (speedShipRectangleAdd.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                speedShipStateAdd = 1;

                if (mouseState == MouseClickState.clicked) speedShipStateAdd = 2;

                if (mouseState == MouseClickState.unClicked) addList(speedShipUpdate);
            }
            else if (speedShipRectangleRemove.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                speedShipStateRemove = 1;

                if (mouseState == MouseClickState.clicked) speedShipStateRemove = 2;

                if (mouseState == MouseClickState.unClicked) removeList(speedShipUpdate);
            }
            else if (speedShotRectangleAdd.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                speedShotStateAdd = 1;

                if (mouseState == MouseClickState.clicked) speedShotStateAdd = 2;

                if (mouseState == MouseClickState.unClicked) addList(speedShotUpdate);
            }
            else if (speedShotRectangleRemove.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                speedShotStateRemove = 1;

                if (mouseState == MouseClickState.clicked) speedShotStateRemove = 2;

                if (mouseState == MouseClickState.unClicked) removeList(speedShotUpdate);
            }
            else if (cadenceRectangleAdd.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                cadenceStateAdd = 1;

                if (mouseState == MouseClickState.clicked) cadenceStateAdd = 2;

                if (mouseState == MouseClickState.unClicked) addList(cadenceUpdate);
            }
            else if (cadenceRectangleRemove.Contains(mouse.X, mouse.Y) && !isClickedPreview)
            {
                cadenceStateRemove = 1;

                if (mouseState == MouseClickState.clicked) cadenceStateRemove = 2;

                if (mouseState == MouseClickState.unClicked) removeList(cadenceUpdate);
            }
            // We look the preview button, but in this case, we can press it even if we activate the preview
            else if (previewRectangle.Contains(mouse.X, mouse.Y))
            {
                previewState = 1;

                if (mouseState == MouseClickState.clicked) previewState = 2;

                if (mouseState == MouseClickState.unClicked)
                {
                    isClickedPreview = !isClickedPreview;
                    gamePreview();
                }
            }
            // We loof the continue button, but in this case, we can press it even if we activate the preview
            else if (continueRectangle.Contains(mouse.X, mouse.Y))
            {
                continueState = 1;

                if (mouseState == MouseClickState.clicked) continueState = 2;

                if (mouseState == MouseClickState.unClicked)
                    switch (gameState)
                    {
                        case GameState.story :
                            mainGame.NewStory();
                            break;
                        case GameState.killer:
                            mainGame.NewKiller("LevelA1");
                            break;
                        case GameState.scroll:
                            mainGame.NewScroll("LevelB1");
                            break;
                        case GameState.survival:
                            mainGame.NewSurvival("LevelC1");
                            break;
                        case GameState.defense:
                            mainGame.NewDefense("LevelADefense1");
                            break;
                    }
            }
            // If we haven't the mouse in any button
            else
            {
                lifeStateAdd = lifeStateRemove = 0;
                powerAttackStateAdd = powerAttackStateRemove = 0;
                speedShipStateAdd = speedShipStateRemove = 0;
                speedShotStateAdd = speedShotStateRemove = 0;
                cadenceStateAdd = cadenceStateRemove = 0;
                previewState = 0;
                continueState = 0;
            }

            // If we activate de preview
            if (isClickedPreview && ship != null)
                ship.Update(deltaTime);

            // We calculate the extra value of all parameters
            lifeExtra = valueList(lifeUpdate, LIFE);
            powerAttackExtra = valueList(powerAttackUpdate, POWER_ATTACK);
            speedShipExtra = valueList(speedShipUpdate, SPEED_SHIP);
            speedShotExtra = valueList(speedShotUpdate, SPEED_SHOT);
            cadenceExtra = valueList(cadenceUpdate, CADENCE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //draw backgrounds
            backgroundSprite.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            measures.Draw(spriteBatch);

            //draw buttons continue and preview
            spriteBatch.Draw(buttonPreview, previewRectangle, new Rectangle(previewState * (int)WIDTH_BUTTON_PREVIEW, 0, (int)WIDTH_BUTTON_PREVIEW, (int)HEIGHT_BUTTON_PREVIEW), Color.White);
            spriteBatch.Draw(buttonContinue, continueRectangle, new Rectangle(continueState * (int)LENGTH_BUTTON_CONTINUE, 0, (int)LENGTH_BUTTON_CONTINUE, (int)LENGTH_BUTTON_CONTINUE), Color.White);

            //draw bars
            drawLife(spriteBatch);
            drawPowerAttack(spriteBatch);
            drawSpeedShip(spriteBatch);
            drawSpeedShot(spriteBatch);
            drawCadence(spriteBatch);

            // draw the frame (preview)
            if (isClickedPreview) drawGamePreview(spriteBatch);
        }


        //-------------------------------------------------------------------------------

        /// <summary>
        /// Return a list of booleans initialized
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private List<Boolean> initializeList(int size)
        {
            List<Boolean> list = new List<Boolean>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(false);
            }
            return list;
        }

        /// <summary>
        /// Return a string in which we can see the currrent state of the boooleans' list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private String getImprovements(List<Boolean> list)
        {
            String aux = "";
            foreach (Boolean b in list)
                aux += "[" + b + "]";
            return aux;
        }

        /// <summary>
        /// We add a true value in a boolean's list
        /// </summary>
        /// <param name="list"></param>
        private void addList(List<Boolean> list)
        {
            int i = 0;
            while (i < list.Count && list.ElementAt(i) == true)
                i++;

            if (i < list.Count)
                list[i] = true;
        }

        /// <summary>
        /// We remove a true value from a boolean's list
        /// </summary>
        /// <param name="list"></param>
        private void removeList(List<Boolean> list)
        {
            int i = list.Count - 1;
            while (i >= 0 && list.ElementAt(i) == false)
                i--;

            if (i >= 0)
                list[i] = false;
        }

        /// <summary>
        /// We return the value we have in the boolean's list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="improvement"></param>
        /// <returns></returns>
        private float valueList(List<Boolean> list, float improvement)
        {
            float improve = 0;
            int i = 0;
            while (i < list.Count && list[i])
            {
                improve += improvement;
                i++;
            }

            return improve;
        }

        /// <summary>
        /// We draw the life bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawLife(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(measures.position.X - measures.texture.Width / 2, measures.position.Y + INITIAL_SPACE_BAR - measures.texture.Height / 2);

            // base life
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = 0; j < (life / 4); j++)//columns
                {
                    spriteBatch.Draw(greenPixel, position + new Vector2(j, i), Color.White);
                }
            }

            // life extra
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = (int)(life / 4); j < ((life + lifeExtra) / 4); j++)//columns
                {
                    spriteBatch.Draw(bluePixel, position + new Vector2(j, i), Color.White);
                }
            }

            spriteBatch.Draw(buttons, lifeRectangleAdd, new Rectangle(lifeStateAdd * (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
            spriteBatch.Draw(buttons, lifeRectangleRemove, new Rectangle(lifeStateRemove * (int)LENGTH_BUTTON, 0, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
        }

        /// <summary>
        /// We draw the power attack bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawPowerAttack(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(measures.position.X - measures.texture.Width / 2, measures.position.Y + INITIAL_SPACE_BAR + HIGH_BAR + SPACE_BAR - measures.texture.Height / 2);

            //base power attack
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = 0; j < (powerAttack / 4); j++)//columns
                {
                    spriteBatch.Draw(greenPixel, position + new Vector2(j, i), Color.White);
                }
            }

            // power attack extra
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = (int)(powerAttack / 4); j < ((powerAttack + powerAttackExtra) / 4); j++)//columns
                {
                    spriteBatch.Draw(bluePixel, position + new Vector2(j, i), Color.White);
                }
            }

            spriteBatch.Draw(buttons, powerAttackRectangleAdd, new Rectangle(powerAttackStateAdd * (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
            spriteBatch.Draw(buttons, powerAttackRectangleRemove, new Rectangle(powerAttackStateRemove * (int)LENGTH_BUTTON, 0, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
        }

        /// <summary>
        /// We draw the speed ship bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawSpeedShip(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(measures.position.X - measures.texture.Width / 2, measures.position.Y + INITIAL_SPACE_BAR + 2 * SPACE_BAR + 2 * HIGH_BAR - measures.texture.Height / 2);

            // base speed ship
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = 0; j < (speedShip / 4); j++)//columns
                {
                    spriteBatch.Draw(greenPixel, position + new Vector2(j, i), Color.White);
                }
            }

            // speed ship extra
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = (int)(speedShip / 4); j < ((speedShip + speedShipExtra) / 4); j++)//columns
                {
                    spriteBatch.Draw(bluePixel, position + new Vector2(j, i), Color.White);
                }
            }

            spriteBatch.Draw(buttons, speedShipRectangleAdd, new Rectangle(speedShipStateAdd * (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
            spriteBatch.Draw(buttons, speedShipRectangleRemove, new Rectangle(speedShipStateRemove * (int)LENGTH_BUTTON, 0, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
        }

        /// <summary>
        /// We draw the speed shot bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawSpeedShot(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(measures.position.X - measures.texture.Width / 2, measures.position.Y + INITIAL_SPACE_BAR + 3 * SPACE_BAR + 3 * HIGH_BAR - measures.texture.Height / 2);

            // base speed shot
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = 0; j < (speedShot / 4); j++)//columns
                {
                    spriteBatch.Draw(greenPixel, position + new Vector2(j, i), Color.White);
                }
            }

            // speed shot extra
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = (int)(speedShot / 4); j < ((speedShot + speedShotExtra) / 4); j++)//columns
                {
                    spriteBatch.Draw(bluePixel, position + new Vector2(j, i), Color.White);
                }
            }

            spriteBatch.Draw(buttons, speedShotRectangleAdd, new Rectangle(speedShotStateAdd * (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
            spriteBatch.Draw(buttons, speedShotRectangleRemove, new Rectangle(speedShotStateRemove * (int)LENGTH_BUTTON, 0, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
        }

        /// <summary>
        /// We draw the cadence bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawCadence(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(measures.position.X - measures.texture.Width / 2, measures.position.Y + INITIAL_SPACE_BAR + 4 * SPACE_BAR + 4 * HIGH_BAR - measures.texture.Height / 2);

            // base cadence
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = 0; j < ((60 / cadence) / 4); j++)//columns
                {
                    spriteBatch.Draw(greenPixel, position + new Vector2(j, i), Color.White);
                }
            }

            // cadence extra
            for (int i = 0; i < 30; i++)//rows
            {
                for (int j = (int)((60 / cadence) / 4); j < (60 / (cadence - cadenceExtra) / 4); j++)//columns
                {
                    spriteBatch.Draw(bluePixel, position + new Vector2(j, i), Color.White);
                }
            }

            spriteBatch.Draw(buttons, cadenceRectangleAdd, new Rectangle(cadenceStateAdd * (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
            spriteBatch.Draw(buttons, cadenceRectangleRemove, new Rectangle(cadenceStateRemove * (int)LENGTH_BUTTON, 0, (int)LENGTH_BUTTON, (int)LENGTH_BUTTON), Color.White);
        }

        /// <summary>
        /// We create a simple preview game
        /// </summary>
        private void gamePreview()
        {
            ship = new ShipPreview(contentMng, this);
        }

        /// <summary>
        /// We draw the simple preview game
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawGamePreview(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(previewFrame, previewFrameRectangle, Color.White);
            ship.Draw(spriteBatch);
        }

    }//class Evolution
}
