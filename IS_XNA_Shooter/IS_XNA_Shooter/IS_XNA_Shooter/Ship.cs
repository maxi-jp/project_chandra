using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase padre de jugador de la que heredan los 2 tipos de jugador
    abstract class Ship : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */
        protected Collider collider;

        protected float velocity;
        protected Vector2 movement;
        protected List<Shot> shots;
        protected float shotVelocity = 500f;
        protected int shotPower = 200;
        protected int life;

        protected float timeToShot = 0.2f; // tiempo minimo entre disparos en segundos
        protected float timeToShotAux;

        private float timeVibShot = 0.1f; // tiempo que vibra el mando tras disparo
        private float timeVibShotAux;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Ship(Camera camera, Level level, Vector2 position, float rotation, Vector2[] colliderPoints,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping, float frametime,
            Texture2D texture, float velocity, int life, List<Shot> shots)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim, frameCount, looping,
                frametime)
        {
            movement = new Vector2(1, 0);
            this.velocity = velocity;
            this.life = life;
            this.shots = shots;

            collider = new Collider(camera, true, position, rotation, colliderPoints, frameWidth, frameHeight);

            setAnim(1);
        }

        /* ------------------- MÉTODOS ------------------- */
        /*public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // movimiento del jugado
            movement = Vector2.Zero;
            float disp = 0;
            double gyre = 0;

            if (ControlMng.isControllerActive()) // GamePad
            {
                if (timeVibShotAux <= 0)
                    GamePad.SetVibration(ShipIndex.One, 0, 0);

                movement = GamePad.GetState(ShipIndex.One).ThumbSticks.Left;
                movement.Y = -movement.Y;

                disp = GamePad.GetState(ShipIndex.One).Triggers.Right;
                if (GamePad.GetState(ShipIndex.One).IsButtonDown(Buttons.RightTrigger))
                    disp = 1;

                timeVibShotAux -= deltaTime;
            }
            else // teclado
            {
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlUp))
                {
                    movement.Y = -1;
                    gyre = Math.PI / 2;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlDown))
                {
                    movement.Y = 1;
                    gyre = Math.PI / 2;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlLeft))
                {
                    movement.X = -1;
                    gyre = 0;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlRight))
                {
                    movement.X = 1;
                    gyre = 0;
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    disp = 1;
            }

            // movimiento:
            if (movement.X != 0 && movement.Y != 0)
                gyre = Math.Abs(Math.Atan(movement.Y / movement.X));

            //if ((movement.X + movement.Y) > 1)
            //movement.Normalize();

            
            position.X += movement.X * (float)Math.Cos(gyre) * velocity * deltaTime;
            position.Y += movement.Y * (float)Math.Sin(gyre) * velocity * deltaTime;

            // disparos:
            if ((disp > 0) && (timeToShotAux <= 0))
                ShipShot(disp);

            timeToShotAux -= deltaTime;

            collider.Update(position, rotation);
        }*/

        public virtual void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // movimiento del jugado
            movement = Vector2.Zero;
            float disp = 0;

            if (ControlMng.isControllerActive()) // GamePad
            {
                if (timeVibShotAux <= 0)
                    GamePad.SetVibration(PlayerIndex.One, 0, 0);

                movement = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
                movement.Y = -movement.Y;

                disp = GamePad.GetState(PlayerIndex.One).Triggers.Right;
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger))
                    disp = 1;

                timeVibShotAux -= deltaTime;
            }
            else // teclado
            {
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlUp))
                    movement.Y = -1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlDown))
                    movement.Y = 1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlLeft))
                    movement.X = -1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlRight))
                    movement.X = 1;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    disp = 1;
            }

            // movimiento:
            if ((movement.X + movement.Y) > 1)
                movement.Normalize();
            position.X += movement.X * velocity * deltaTime;
            position.Y += movement.Y * velocity * deltaTime;

            // disparos:
            if ((disp > 0) && (timeToShotAux <= 0))
                ShipShot(disp);

            timeToShotAux -= deltaTime;

            collider.Update(position, rotation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (SuperGame.debug)
                collider.Draw(spriteBatch);     
        }

        private void ShipShot(float disp)
        {
            /*if (ControlMng.isControllerActive())
            {
                GamePad.SetVibration(ShipIndex.One, 0.1f, 0.2f);
                timeVibShotAux = timeVibShot;
            }*/

            Audio.PlayEffect("laserShot01");

            setAnim(2);

            Shot nuevo = new Shot(camera, level, position, rotation, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
                GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(nuevo);
            timeToShotAux = timeToShot;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

    } // class Ship
}
