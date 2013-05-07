using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter.Evolutions
{
    /// <summary>
    /// Ship class fot he preview frame in the evolution screen
    /// </summary>
    class ShipPreview
    {
        /// <summary>
        /// Parameters for the animation of the ship
        /// </summary>
        private const int SIZE = 80,
            NUM_ANIMATION_MOVE = 4,
            NUM_ANIMATION_SHOT = 5;

        /// <summary>
        /// Parameter for the animation of the ship
        /// </summary>
        private const float TIME_ANIMATION = 0.05f;


        //---------------------------------------------------------------------

        /// <summary>
        /// States of the ship (move or shot)
        /// </summary>
        private enum TypeAnimation { move, shot };

        
        //---------------------------------------------------------------------

        /// <summary>
        /// How many time have the curent animation
        /// </summary>
        private float timeAnimation;

        /// <summary>
        /// Parameter for the ship
        /// </summary>
        private float life, speed, power, speedShot, cadence;

        /// <summary>
        /// Countdown until next shot
        /// </summary>
        private float timeToShot;

        /// <summary>
        /// Position of the current animation
        /// </summary>
        private int posAnimation;

        /// <summary>
        /// What animation we have
        /// 1 - move
        /// 2 - shot
        /// </summary>
        private int animation;

        /// <summary>
        /// Current animation (move or shot)
        /// </summary>
        private TypeAnimation typeAnimation;

        /// <summary>
        /// Thexture of the ship
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Rectangle that select the animation
        /// </summary>
        private Rectangle animationRectangle;

        /// <summary>
        /// Position of the ship in the preview frame
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// List of ship's shot
        /// </summary>
        private List<ShotPreview> shots;

        /// <summary>
        /// The content manager
        /// </summary>
        private ContentManager content;


        //---------------------------------------------------------------------

        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="content"></param>
        /// <param name="evolution"></param>
        public ShipPreview(ContentManager content, Evolution evolution)
        {
            // initialize the content
            this.content = content;

            // texture
            texture = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
            position = new Vector2(SuperGame.screenWidth / 2 - SIZE / 2, SuperGame.screenHeight / 2 - SIZE / 2);

            // animation
            animationRectangle = new Rectangle(0, SIZE, SIZE, SIZE);
            timeAnimation = 0;
            posAnimation = 0;
            animation = 0;
            typeAnimation = TypeAnimation.move;

            //life, power, speed, speedShot
            life = evolution.getLife();
            power = evolution.getPowerAttack();
            speed = evolution.getSpeedShip();
            speedShot = evolution.getSpeedShot();

            //cadence
            cadence = evolution.getCadence();
            timeToShot = 0;

            //shots
            shots = new List<ShotPreview>();
        }
        

        //---------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            timeAnimation += deltaTime;
            timeToShot -= deltaTime;

            //animation
            if (timeAnimation >= TIME_ANIMATION)
            {
                //move
                if (typeAnimation == TypeAnimation.move)
                {
                    if (posAnimation < NUM_ANIMATION_MOVE - 1) posAnimation++;
                    else posAnimation = 0;
                    animation = 1;
                }
                //shot
                else if (typeAnimation == TypeAnimation.shot)
                {
                    if (posAnimation < NUM_ANIMATION_SHOT - 1) posAnimation++;
                    else
                    {
                        posAnimation = 0;
                        typeAnimation = TypeAnimation.move;
                    }
                    animation = 2;
                }

                animationRectangle.X = posAnimation * SIZE;
                animationRectangle.Y = animation * SIZE;
                timeAnimation = 0;
            }

            //control ship
            controlShip(deltaTime);

            // update shots
            updateShots(deltaTime);

            // delete shots
            deleleteShots();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw ship
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, SIZE, SIZE), animationRectangle, Color.White);

            // draw shots
            drawShots(spriteBatch);
        }


        //---------------------------------------------------------------------------------------------

        /// <summary>
        /// Manage the control for the ship
        /// </summary>
        /// <param name="deltaTime"></param>
        private void controlShip(float deltaTime)
        {
            Vector2 movement = Vector2.Zero;
            
            //keys to move the ship
            if (Keyboard.GetState().IsKeyDown(ControlMng.controlUp))
                movement.Y = -1;
            if (Keyboard.GetState().IsKeyDown(ControlMng.controlDown))
                movement.Y = 1;
            if (Keyboard.GetState().IsKeyDown(ControlMng.controlLeft))
                movement.X = -1;
            if (Keyboard.GetState().IsKeyDown(ControlMng.controlRight))
                movement.X = 1;

            if ((Math.Abs(movement.X) + Math.Abs(movement.Y)) > 1)
                movement.Normalize();
            position.X += movement.X * speed * deltaTime;
            position.Y += movement.Y * speed * deltaTime;

            //limit the frame
            if (position.X > SuperGame.screenWidth / 2 + Evolution.LENGTH_FRAME / 2 - SIZE)
                position.X = SuperGame.screenWidth / 2 + Evolution.LENGTH_FRAME / 2 - SIZE;
            if (position.X < SuperGame.screenWidth / 2 - Evolution.LENGTH_FRAME / 2)
                position.X = SuperGame.screenWidth / 2 - Evolution.LENGTH_FRAME / 2;
            if (position.Y > SuperGame.screenHeight / 2 + Evolution.LENGTH_FRAME / 2 - SIZE)
                position.Y = SuperGame.screenHeight / 2 + Evolution.LENGTH_FRAME / 2 - SIZE;
            if (position.Y < SuperGame.screenHeight / 2 - Evolution.LENGTH_FRAME / 2)
                position.Y = SuperGame.screenHeight / 2 - Evolution.LENGTH_FRAME / 2;

            //shots
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && timeToShot <= 0)
            {
                typeAnimation = TypeAnimation.shot;
                timeToShot = cadence;
                ShotPreview shot = new ShotPreview(content);
                shot.setPosition(position + new Vector2(SIZE , SIZE / 2));
                shot.setSpeed(speedShot);
                shots.Add(shot);
            }                
        }

        /// <summary>
        /// Draw the ship's shots
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void drawShots(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shots.Count; i++)
            {
                shots[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Update the ship's shots
        /// </summary>
        /// <param name="deltaTime"></param>
        private void updateShots(float deltaTime)
        {
            for (int i = 0; i < shots.Count; i++)
            {
                shots[i].Update(deltaTime);
            }
        }

        /// <summary>
        /// Delete the ship's shots
        /// </summary>
        private void deleleteShots()
        {
            for (int i = 0; i < shots.Count; i++)
            {
                if (shots[i].getPosition().X > SuperGame.screenWidth / 2 + Evolution.LENGTH_FRAME / 2 - ShotPreview.WIDTH_SHOT) 
                    shots.RemoveAt(i) ;
            }
        }

    }//class ShipPreview
}
