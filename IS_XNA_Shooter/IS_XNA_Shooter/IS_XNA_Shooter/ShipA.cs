using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    // clase ShipA para nave con mapas con vista cenital
    class ShipA : Ship
    {
        /* ------------------- ATRIBUTOS ------------------- */
        private Vector2 pointer;
        private float prevRotation;

        /* ------------------- CONSTRUCTORES ------------------- */
        public ShipA(Game game, Camera camera, Level level, Vector2 position, float rotation,
            Vector2[] colliderPoints,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture,
            float velocity, int life, List<Shot> shots)
            : base(game, camera, level, position, rotation, colliderPoints, frameWidth, frameHeight,
                numAnim, frameCount, looping, frametime, texture, velocity, life, shots)
        {
            pointer = new Vector2();
            prevRotation = 0;
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update (float deltaTime)
        {
            base.Update(deltaTime);

            if (currentState != shipState.ONDYING)
            {
                if (ControlMng.isControllerActive()) // GamePad
                {
                    Vector2 rot = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right;
                    rot.Normalize();

                    if (rot.Y > 0)
                        rotation = -(float)Math.Acos(rot.X);
                    else if (rot.Y <= 0)
                        rotation = (float)Math.Acos(rot.X);
                    else
                        rotation = prevRotation;

                    prevRotation = rotation;
                }
                else // teclado
                {
                    pointer.X = Mouse.GetState().X;
                    pointer.Y = Mouse.GetState().Y;

                    float dY = pointer.Y - position.Y - camera.displacement.Y;
                    float dX = pointer.X - position.X - camera.displacement.X;

                    float gyre = (float)Math.Atan(Math.Abs(dY) / Math.Abs(dX));

                    if (dX > 0 && dY > 0)
                        rotation = gyre;
                    else if (dX > 0 && dY < 0)
                        rotation = -gyre;
                    else if (dX < 0 && dY < 0)
                        rotation = (float)(Math.PI) + gyre;
                    else if (dX < 0 && dY > 0)
                        rotation = (float)Math.PI - gyre;
                }

                // comprobamos que el Ship no se salga del nivel
                position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
                position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);
            }

        } // Update

    } // class ShipA
}