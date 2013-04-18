using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for Enemy Weak that shots one shot
    /// </summary>
    class EnemyWeakShotADefense : EnemyShotADefense
    {

        /// <summary>
        /// EnemyWeakShotADefense's constructor
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
        /// <param name="timeToShot">Value representing the firing rate</param>
        /// <param name="shotVelocity">value representing the velocity of the bullets</param>
        /// <param name="shotPower">value representing the bullets power</param>
        public EnemyWeakShotADefense(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship,
            float timeToShot, float shotVelocity, int shotPower, House house)
            : base(camera, level, position, rotation,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime,
                texture, timeToSpawn, velocity, life, value, ship,
                timeToShot, shotVelocity, shotPower,house)
        {
            Vector2[] points = new Vector2[7];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 40);
            points[4] = new Vector2(49, 51);
            points[5] = new Vector2(32, 57);
            points[6] = new Vector2(21, 57);
            collider = new Collider(camera, true, position, rotation, points, 35, frameWidth, frameHeight);
           
            target = random.Next(-1, 2);

            this.house = house;

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
                if (target <= 0) // If the target is the player (0)
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

                    timeToShotAux -= deltaTime;
                    if (timeToShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, position, rotation, GRMng.frameWidthL2, GRMng.frameHeightL2,
                            GRMng.numAnimsL2, GRMng.frameCountL2, GRMng.loopingL2, SuperGame.frameTime12,
                            GRMng.textureL2, SuperGame.shootType.normal, shotVelocity, shotPower);
                        shots.Add(shot);
                        setAnim(2);

                        timeToShotAux = timeToShot;
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

                    timeToShotAux -= deltaTime;
                    if (timeToShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, position, rotation, GRMng.frameWidthL2, GRMng.frameHeightL2,
                            GRMng.numAnimsL2, GRMng.frameCountL2, GRMng.loopingL2, SuperGame.frameTime12,
                            GRMng.textureL2, SuperGame.shootType.normal, shotVelocity, shotPower);
                        shots.Add(shot);
                        setAnim(2);

                        timeToShotAux = timeToShot;
                    }
                }
            } // if life > 0

        } // Update

        /// <summary>
        /// Causes damage to the enemy
        /// </summary>
        /// <param name="i">The amount of damage that the enemy receives</param>
        public override void Damage(int i)
        {
            base.Damage(i);

            if (life <= 0)
            {
                setAnim(3, -1);
                Audio.PlayEffect("brokenBone01");
            }
        }
                
    } // class EnemyWeakShotA
}
