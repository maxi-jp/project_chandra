using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Update our ship at the begin of each game.
    /// </summary>
    class Evolution
    {
        private const float LIFE = 50,
                            POWER_ATTACK = 50,
                            SPEED_SHIP = 50,
                            SPEED_SHOT = 100,
                            CADENCE = 0.1f;


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
                            continueRectangle;

        private Texture2D addTexture, removeTexture, continueTexture, greenPixel;
        private Sprite backgroundSprite, background1, measures;

        private Boolean isClicked;

        private SuperGame mainGame;
        //-------------------------------------------------------------------------------


        public Evolution(ContentManager content, SuperGame mainGame)
        {
            life = 100;
            powerAttack = 200;
            speedShip = 200;
            speedShot = 250;
            cadence = 1;

            lifeExtra = powerAttackExtra = speedShipExtra = speedShotExtra = cadenceExtra = 0;

            lifeUpdate = initializeList(10);
            powerAttackUpdate = initializeList(10);
            speedShipUpdate = initializeList(10);
            speedShotUpdate = initializeList(10);
            cadenceUpdate = initializeList(9);

            int high = 20;

            lifeRectangleAdd = new Rectangle(20, high, 40, 40);
            lifeRectangleRemove = new Rectangle(70, high, 40, 40);
            high += 60;

            powerAttackRectangleAdd = new Rectangle(20, high, 40, 40);
            powerAttackRectangleRemove = new Rectangle(70, high, 40, 40);
            high += 60;

            speedShipRectangleAdd = new Rectangle(20, high, 40, 40);
            speedShipRectangleRemove = new Rectangle(70, high, 40, 40);
            high += 60;

            speedShotRectangleAdd = new Rectangle(20, high, 40, 40);
            speedShotRectangleRemove = new Rectangle(70, high, 40, 40);
            high += 60;

            cadenceRectangleAdd = new Rectangle(20, high, 40, 40);
            cadenceRectangleRemove = new Rectangle(70, high, 40, 40);
            high += 60;

            continueRectangle = new Rectangle(20, high, 100, 40);

            addTexture = content.Load<Texture2D>("Graphics/Evolution/add");
            removeTexture = content.Load<Texture2D>("Graphics/Evolution/remove");
            continueTexture = content.Load<Texture2D>("Graphics/Evolution/continue");
            greenPixel = content.Load<Texture2D>("Graphics/Evolution/greenPixel");

            Vector2 position = new Vector2(SuperGame.screenWidth/2, SuperGame.screenHeight/2);
            backgroundSprite = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Splash/splash_mainmenu_2"));
            background1 = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Evolution/background_1"));

            position = new Vector2(SuperGame.screenWidth / 2, 17 * SuperGame.screenHeight / 32);
            measures = new Sprite(true, position, 0, content.Load<Texture2D>("Graphics/Evolution/measures"));

            isClicked = false;

            this.mainGame = mainGame;
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

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton != ButtonState.Pressed)
                isClicked = false;
            if (lifeRectangleAdd.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                addList(lifeUpdate);
            }
            else if (lifeRectangleRemove.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                removeList(lifeUpdate);
            }
            else if (powerAttackRectangleAdd.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                addList(powerAttackUpdate);
            }
            else if (powerAttackRectangleRemove.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                removeList(powerAttackUpdate);
            }
            else if (speedShipRectangleAdd.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                addList(speedShipUpdate);
            }
            else if (speedShipRectangleRemove.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                removeList(speedShipUpdate);
            }
            else if (speedShotRectangleAdd.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                addList(speedShotUpdate);
            }
            else if (speedShotRectangleRemove.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                removeList(speedShotUpdate);
            }
            else if (cadenceRectangleAdd.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                addList(cadenceUpdate);
            }
            else if (cadenceRectangleRemove.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                removeList(cadenceUpdate);
            }
            else if (continueRectangle.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !isClicked)
            {
                isClicked = true;
                mainGame.NewStory();
            }

            lifeExtra = valueList(lifeUpdate, LIFE);
            powerAttackExtra = valueList(powerAttackUpdate, POWER_ATTACK);
            speedShipExtra = valueList(speedShipUpdate, SPEED_SHIP);
            speedShotExtra = valueList(speedShotUpdate, SPEED_SHOT);
            cadenceExtra = valueList(cadenceUpdate, CADENCE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            backgroundSprite.Draw(spriteBatch);
            background1.Draw(spriteBatch);
            measures.Draw(spriteBatch);

            drawLife();
            drawPowerAttack();
            drawSpeedShip();
            drawSpeedShot();
            drawCadence();

            /*Vector2 pos = new Vector2(0, 0);
            spriteBatch.DrawString(SuperGame.fontDebug, "life: " + (life + lifeExtra) + getImprovements(lifeUpdate), pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.Draw(addTexture, lifeRectangleAdd, Color.White);
            spriteBatch.Draw(removeTexture, lifeRectangleRemove, Color.White);
            pos += new Vector2(0, 40);
            spriteBatch.DrawString(SuperGame.fontDebug, "power attack: " + (powerAttack + powerAttackExtra) + getImprovements(powerAttackUpdate), pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.Draw(addTexture, powerAttackRectangleAdd, Color.White);
            spriteBatch.Draw(removeTexture, powerAttackRectangleRemove, Color.White);
            pos += new Vector2(0, 40);
            spriteBatch.DrawString(SuperGame.fontDebug, "speed ship: " + (speedShip + speedShipExtra) + getImprovements(speedShipUpdate), pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.Draw(addTexture, speedShipRectangleAdd, Color.White);
            spriteBatch.Draw(removeTexture, speedShipRectangleRemove, Color.White);
            pos += new Vector2(0, 40);
            spriteBatch.DrawString(SuperGame.fontDebug, "speed shot: " + (speedShot + speedShotExtra) + getImprovements(speedShotUpdate), pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.Draw(addTexture, speedShotRectangleAdd, Color.White);
            spriteBatch.Draw(removeTexture, speedShotRectangleRemove, Color.White);
            pos += new Vector2(0, 40);
            spriteBatch.DrawString(SuperGame.fontDebug, "cadence: " + (cadence - cadenceExtra) + getImprovements(cadenceUpdate), pos, Color.White);

            spriteBatch.Draw(addTexture, cadenceRectangleAdd, Color.White);
            spriteBatch.Draw(removeTexture, cadenceRectangleRemove, Color.White);

            spriteBatch.Draw(continueTexture, continueRectangle, Color.White);*/
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

        private void drawLife()
        {
        }

        private void drawPowerAttack()
        {
        }

        private void drawSpeedShip()
        {
        }

        private void drawSpeedShot()
        {
        }

        private void drawCadence()
        {
        }

    }//class Evolution
}
