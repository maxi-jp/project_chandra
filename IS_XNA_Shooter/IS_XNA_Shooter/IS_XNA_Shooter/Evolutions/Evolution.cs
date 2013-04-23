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
        private enum MouseClickState { nothing, clicked, unClicked };
        public enum GameState { story, scroll, killer, survival, defense }


        //-------------------------------------------------------------------------------


        private const float LIFE = 50,
                            POWER_ATTACK = 50,
                            SPEED_SHIP = 50,
                            SPEED_SHOT = 100,
                            CADENCE = 0.1f,
                            INITIAL_SPACE_BAR = 30,
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
        public const int LENGTH_FRAME = 500;


        //-------------------------------------------------------------------------------

        
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

        private List<Boolean> lifeUpdate, 
                            powerAttackUpdate, 
                            speedShipUpdate, 
                            speedShotUpdate, 
                            cadenceUpdate;

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

        private Texture2D greenPixel, bluePixel, buttons, buttonPreview, buttonContinue, previewFrame;
        private Sprite backgroundSprite, background1, measures;

        private MouseClickState mouseState;

        private SuperGame mainGame;

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

        private bool isClickedPreview;

        private ShipPreview ship;

        private ContentManager contentMng;

        private GameState gameState;


        //-------------------------------------------------------------------------------


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
            life = 100;
            powerAttack = 200;
            speedShip = 200;
            speedShot = 250;
            cadence = 1f;
            
            // evolution of base parameters
            lifeExtra = powerAttackExtra = speedShipExtra = speedShotExtra = cadenceExtra = 0;

            // number of evolutions of base parameters
            lifeUpdate = initializeList(10);
            powerAttackUpdate = initializeList(10);
            speedShipUpdate = initializeList(10);
            speedShotUpdate = initializeList(10);
            cadenceUpdate = initializeList(9);

            // textures
            greenPixel = GRMng.greenpixeltrans;
            bluePixel = GRMng.bluepixeltrans;
            buttons = content.Load<Texture2D>("Graphics/Evolution/Buttons");
            buttonPreview = content.Load<Texture2D>("Graphics/Evolution/ButtonPreview");
            buttonContinue = content.Load<Texture2D>("Graphics/Evolution/ButtonContinue");
            previewFrame = content.Load<Texture2D>("Graphics/Evolution/frame");


            Vector2 position = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2);
            backgroundSprite = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Splash/splash_mainmenu_2"));
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
            position = background1.position + new Vector2(-LENGTH_BUTTON_CONTINUE - SPACE_BUTTON_CONTINUE_RIGHT + background1.texture.Width / 2, background1.texture.Height / 2 - LENGTH_BUTTON_CONTINUE - SPACE_BUTTON_CONTINUE_BOTTOM);
            continueRectangle = new Rectangle((int)position.X, (int)position.Y, (int)LENGTH_BUTTON_CONTINUE, (int)LENGTH_BUTTON_CONTINUE);

            // position of the frame (preview)
            position = new Vector2(SuperGame.screenWidth / 2 - previewFrame.Width / 2, SuperGame.screenHeight / 2 - previewFrame.Height / 2);
            previewFrameRectangle = new Rectangle((int)position.X, (int)position.Y, previewFrame.Width, previewFrame.Height);

            // state that tell us if a button has been click, unclick or you didn't do nothing.
            mouseState = MouseClickState.nothing;

            // assigns
            this.mainGame = mainGame;
            contentMng = content;
        }


        //-------------------------------------------------------------------------------

        public float getLife() {
            return life + lifeExtra;
        }

        public float getPowerAttack() {
            return powerAttack + powerAttackExtra;
        }

        public float getSpeedShip() {
            return speedShip + speedShipExtra;
        }

        public float getSpeedShot() {
            return speedShot + speedShotExtra;
        }

        public float getCadence() {
            return cadence - cadenceExtra;
        }

        public void setGameState(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void Update(float deltaTime, MouseState mouse)
        {
            if (mouse.LeftButton != ButtonState.Pressed)
                if (mouseState == MouseClickState.clicked) mouseState = MouseClickState.unClicked;
                else mouseState = MouseClickState.nothing;
            else mouseState = MouseClickState.clicked;

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
                            mainGame.NewKiller(0);
                            break;
                        case GameState.scroll:
                            mainGame.NewScroll(1);
                            break;
                        case GameState.survival:
                            mainGame.NewSurvival(2);
                            break;
                        case GameState.defense:
                            mainGame.NewDefense(4);
                            break;
                    }
            }
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

            if (isClickedPreview && ship != null)
                ship.Update(deltaTime);

            lifeExtra = valueList(lifeUpdate, LIFE);
            powerAttackExtra = valueList(powerAttackUpdate, POWER_ATTACK);
            speedShipExtra = valueList(speedShipUpdate, SPEED_SHIP);
            speedShotExtra = valueList(speedShotUpdate, SPEED_SHOT);
            cadenceExtra = valueList(cadenceUpdate, CADENCE);
        }

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


        private List<Boolean> initializeList(int size)
        {
            List<Boolean> list = new List<Boolean>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(false);
            }
            return list;
        }

        private String getImprovements(List<Boolean> list)
        {
            String aux = "";
            foreach (Boolean b in list)
                aux += "[" + b + "]";
            return aux;
        }

        private void addList(List<Boolean> list)
        {
            int i = 0;
            while (i < list.Count && list.ElementAt(i) == true)
                i++;

            if (i < list.Count)
                list[i] = true;
        }

        private void removeList(List<Boolean> list)
        {
            int i = list.Count - 1;
            while (i >= 0 && list.ElementAt(i) == false)
                i--;

            if (i >= 0)
                list[i] = false;
        }

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

        private void gamePreview()
        {
            ship = new ShipPreview(contentMng, this);
        }

        private void drawGamePreview(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(previewFrame, previewFrameRectangle, Color.White);
            ship.Draw(spriteBatch);
        }

    }//class Evolution
}
