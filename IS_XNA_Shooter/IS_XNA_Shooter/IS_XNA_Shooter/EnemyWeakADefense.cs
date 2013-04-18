﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for Enemy Weak in GameA Defense
    /// </summary>
    class EnemyWeakADefense : EnemyADefense
    {


        /// <summary>
        /// EnemyWeakADefense's constructor
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the enemy</param>
        /// <param name="rotation">The rotation of the enemy</param>
        /// <param name="frameWidth">The width of each frame of the enemy's animation</param>
        /// <param name="frameHeight">The height of each frame of the enemy's animation </param>
        /// <param name="numAnim">The number of the enemy's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the enemy</param>
        /// <param name="timeToSpawn">The time that the enemy has to wait for appear in the game</param>
        /// <param name="velocity">The velocity of the enemy</param>
        /// <param name="life">The life of the enemy</param>
        /// <param name="value">The points you obtain if you kill it</param>
        /// <param name="ship">The player's ship</param>
        /// <param name="house">The player's house</param>
        public EnemyWeakADefense(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship, House house)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, ship,house)
        {
            Vector2[] points = new Vector2[7];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 40);
            points[4] = new Vector2(49, 51);
            points[5] = new Vector2(32, 57);
            points[6] = new Vector2(21, 57);
            collider = new Collider(camera, true, position, rotation, points, 90, frameWidth, frameHeight);
            setAnim(1);
        }

        /// <summary>
        /// Updates the logic of the enemy
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
           
            if (life > 0)
            {
                if (base.target <= 0) // If the target is the player (0)
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
                else //The target is the ship (1)
                {
                
                    float dY = -house.position.Y + position.Y;
                    float dX = -house.position.X + position.X;

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
            }

        } // Update

        /// <summary>
        /// Causes damage to the enemy
        /// </summary>
        /// <param name="i">The amount of damage that the enemy receives</param>
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
