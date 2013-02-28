using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase para el tipo de enemigo debil
    class EnemyWeak : Enemy
    {
        /* ------------------- ATRIBUTOS ------------------- */

        /* ------------------- CONSTRUCTORES ------------------- */
        public EnemyWeak(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Ship Ship)
            : base (camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, Ship)
        {
            setAnim(1);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            float dY = -Ship.position.Y + position.Y;
            float dX = -Ship.position.X + position.X;

            float gyre = (float)Math.Atan(dY / dX);

            if (dX < 0)
            {
                rotation = gyre;
                position.X += (float)(velocity * Math.Cos(gyre) * deltaTime);
                position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime);
            }
            else
            {
                rotation = (float)Math.PI + gyre;
                position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime);
                position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime);
            }
        }

    } // class EnemyWeak
}
