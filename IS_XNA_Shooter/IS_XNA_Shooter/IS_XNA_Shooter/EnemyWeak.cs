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
        /* ------------------- ATTRIBUTES ------------------- */

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyWeak(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value,int shotPower, float shotVelocity, float timeToShot, Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value , shotPower, shotVelocity, timeToShot, ship)
        {
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            /*Vector2[] points = new Vector2[4];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(60, 35);
            points[2] = new Vector2(60, 45);
            points[3] = new Vector2(20, 60);*/
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);

            setAnim(1);

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            /*Vector2[] points = new Vector2[4];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(60, 35);
            points[2] = new Vector2(60, 45);
            points[3] = new Vector2(20, 60);*/
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
                float dY = -ship.position.Y + position.Y;
                float dX = -ship.position.X + position.X;

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

        } // Update

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                setAnim(2, -1);
                Audio.PlayEffect("brokenBone01");
            }
        }

    } // class EnemyWeak
}
