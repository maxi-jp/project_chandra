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
    class ShipPreview
    {
        private const int SIZE = 80,
            NUM_ANIMATION_MOVE = 4,
            LENGTH_FRAME = 500;
        private const float TIME_ANIMATION = 0.05f;

        
        //---------------------------------------------------------------------


        private float timeAnimation, life, speed, power, speedShot, cadence, timeToShot;
        private int posAnimation;
        private Texture2D texture;
        private Rectangle animationRectangle;
        private Vector2 position;
        private List<Shot> shots;


        //---------------------------------------------------------------------


        public ShipPreview(ContentManager content, Evolution evolution)
        {
            // texture
            texture = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
            position = new Vector2(SuperGame.screenWidth / 2 - SIZE / 2, SuperGame.screenHeight / 2 - SIZE / 2);

            // animation
            animationRectangle = new Rectangle(0, SIZE, SIZE, SIZE);
            timeAnimation = 0;
            posAnimation = 0;

            //life, power, speed, speedShot
            life = evolution.getLife();
            power = evolution.getPowerAttack();
            speed = evolution.getSpeedShip();
            speedShot = evolution.getSpeedShot();

            //cadence
            cadence = evolution.getCadence();
            timeToShot = 0;

            //shots
            shots = new List<Shot>();
        }
        

        //---------------------------------------------------------------------


        public void Update(float deltaTime)
        {
            timeAnimation += deltaTime;
            timeToShot += deltaTime;

            //animation
            if (timeAnimation >= TIME_ANIMATION)
            {
                if (posAnimation < NUM_ANIMATION_MOVE - 1) posAnimation++;
                else posAnimation = 0;
                animationRectangle.X = posAnimation * SIZE;
                timeAnimation = 0;
            }

            //control ship
            controlShip(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, SIZE, SIZE), animationRectangle, Color.White);
        }


        //---------------------------------------------------------------------------------------------


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

            if ((movement.X + movement.Y) > 1)
                movement.Normalize();
            position.X += movement.X * speed * deltaTime;
            position.Y += movement.Y * speed * deltaTime;

            //limit the frame
            if (position.X > SuperGame.screenWidth / 2 + LENGTH_FRAME / 2 - SIZE)
                position.X = SuperGame.screenWidth / 2 + LENGTH_FRAME / 2 - SIZE;
            if (position.X < SuperGame.screenWidth / 2 - LENGTH_FRAME / 2)
                position.X = SuperGame.screenWidth / 2 - LENGTH_FRAME / 2;
            if (position.Y > SuperGame.screenHeight / 2 + LENGTH_FRAME / 2 - SIZE)
                position.Y = SuperGame.screenHeight / 2 + LENGTH_FRAME / 2 - SIZE;
            if (position.Y < SuperGame.screenHeight / 2 - LENGTH_FRAME / 2)
                position.Y = SuperGame.screenHeight / 2 - LENGTH_FRAME / 2;

            //shots
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && timeToShot > cadence)
            {
                timeToShot = 0;
                Shot shot = new Shot
            }                
        }

    }//class ShipPreview
}
