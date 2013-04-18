using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the enemy scared that shoots 2 shots
    /// </summary>
    class EnemyScaredADefense : EnemyShotADefense
    {
        /// <summary>
        /// Says if it's rotating or not
        /// </summary>
        private bool isRotating = false;

        /// <summary>
        /// Says if it's fleeing or not
        /// </summary>
        private bool isFleeing = false;

        /// <summary>
        /// The final position when it's rotating
        /// </summary>
        private float rotationPosition;

        /// <summary>
        /// Rotation matrix for the two shots
        /// </summary>
        private Matrix rotationMatrix;

        /// <summary>
        /// EnemyScaredADefense's constructor
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
        /// <param name="house">The player's house</param>
        public EnemyScaredADefense(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life, int value,
            Ship ship,
            float timeToShot, float shotVelocity, int shotPower, House house)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, ship,
                timeToShot, shotVelocity, shotPower, house)
        {
            setAnim(3);

            Vector2[] points = new Vector2[5];
            points[0] = new Vector2(20, 0);
            points[1] = new Vector2(70, 40);
            points[2] = new Vector2(20, 80);
            points[3] = new Vector2(10, 71);
            points[4] = new Vector2(10, 9);

            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);
        }

        /// <summary>
        /// Updates the logic of the enemy
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            /*
             * if the player's ship doesn't look at the enemy in a range of +- PI/8, the enemy has to chase and shoot him.
             * if it doesn't the enemy has to change the color to red, turns and flee.
             */
            
            if (life > 0)
            {
                
                float dY = -ship.position.Y + position.Y;
                float dX = -ship.position.X + position.X;

                float gyre = (float)Math.Atan(dY / dX);

                if (isRotating)
                {
                    //If it's the final position of the rotation it has to stop rotating
                    if ((rotation < (rotationPosition + ((float)Math.PI) / 12)) && (rotation > rotationPosition - ((float)Math.PI) / 12))
                    {
                        isRotating = false;

                        //Change the color
                        if (isFleeing)
                            setAnim(4);
                        else
                            setAnim(3);

                    }
                    //If itsn't it has to keep rotating
                    else
                    {
                        rotation += 10 * deltaTime;
                        rotation = rotation % (2 * (float)Math.PI);
                    }
                }
                //If itsn't in the rotation phase
                else
                {
                    float r = ((float)Math.PI) / 8;
                    float h = (float)Math.Sqrt(dY * dY + dX * dX);
                    float ang = 0;
                    float rot = ship.rotation;

                    //If its fleeing
                    if (isFleeing)
                    {

                        // Tests that the player doesn't go out of the level
                        position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
                        position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);

                        //First quadrant
                        if (dX < 0 && dY > 0)
                        {

                            ang = (float)Math.Abs(Math.Acos(dY / h));
                            ang += (float)Math.PI / 2;
                            //If the player's ship doesn't look the enemy, it has to rotate to pursuit him
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = false;
                                //Change the color
                                setAnim(2);
                            }

                        }
                        //Second quadrant
                        else if (dX >= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Change the color
                                setAnim(2);
                            }
                        }
                        //Third quadrant
                        else if (dX > 0 && dY < 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang = -ang;
                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Change the color
                                setAnim(2);
                            }
                        }
                        //Fourth quadrant
                        else if (dX <= 0 && dY <= 0)
                        {

                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang += (float)Math.PI;

                            if (ship.rotation < 0)
                                rot += 2 * (float)Math.PI;

                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Change the color
                                setAnim(2);
                            }
                        }

                        //Keep fleeing
                        if (ang < rot + r && ang > rot - r)
                        {

                            Flee(dX, gyre, deltaTime);
                        }

                    }
                    // If the enemy isn't fleeing
                    else
                    {
                        
                        // It shots if it has to
                        timeToShot -= deltaTime;
                        if (timeToShot <= 0)
                        {
                            TwoShots();
                            timeToShot = 1.7f;
                        }
                        //First quadrant
                        if (dX <= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Acos(dY / h));
                            ang += (float)Math.PI / 2;

                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Change the color
                                setAnim(2);
                            }
                        }
                        //Second quadrant
                        else if (dX >= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));

                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {

                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Change the color
                                setAnim(2);
                            }
                        }
                        //Third quadrant
                        else if (dX >= 0 && dY <= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang = -ang;

                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Change the colorr
                                setAnim(2);
                            }
                        }
                        //Fourth quadrant if (dX <= 0 && dY <= 0)
                        else
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang += (float)Math.PI;

                            //If the player's ship doesn't look the enemy, it has to rotate to chase him
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Change the color
                                setAnim(2);
                            }
                        }

                        if (!(ang < ship.rotation + r && ang > ship.rotation - r))
                        {
                            if (target >= 1)
                            {
                                dY = -house.position.Y + position.Y;
                                dX = -house.position.X + position.X;

                                gyre = (float)Math.Atan(dY / dX);
                            }
                            Chase(dX, gyre, deltaTime);
                        }
                    
                    }
                }
            } // if (isRotating)

        } // Update

        /// <summary>
        /// Calculates the shots' points and create the shots
        /// </summary>
        private void TwoShots()
        {
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            Vector2 p1Orig = new Vector2(20, 13);
            Vector2 p2Orig = new Vector2(20, -13);

            Vector2 p1 = new Vector2();
            Vector2 p2 = new Vector2();

            p1 = Vector2.Transform(p1Orig, rotationMatrix);
            p1 += position;

            p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 += position;

            //base.EnemyShot(p1);
            //base.EnemyShot(p2);
            Shot nuevo = new Shot(camera, level, p1, rotation, GRMng.frameWidthESBullet,
                GRMng.frameHeightESBullet, GRMng.numAnimsESBullet, GRMng.frameCountESBullet,
                GRMng.loopingESBullet, SuperGame.frameTime8, GRMng.textureESBullet,
                SuperGame.shootType.normal, shotVelocity, shotPower);
            shots.Add(nuevo);
            nuevo = new Shot(camera, level, p2, rotation, GRMng.frameWidthESBullet,
                GRMng.frameHeightESBullet, GRMng.numAnimsESBullet, GRMng.frameCountESBullet,
                GRMng.loopingESBullet, SuperGame.frameTime8, GRMng.textureESBullet,
                SuperGame.shootType.normal, shotVelocity, shotPower);
            shots.Add(nuevo);
        }

        /// <summary>
        /// Chasing function
        /// </summary>
        /// <param name="dX">Position's x difference between player's ship and the enemy</param>
        /// <param name="gyre">The new rotation</param>
        /// <param name="deltaTime">The time since the last update</param>
        private void Chase(float dX, float gyre, float deltaTime)
        {

            
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

        /// <summary>
        /// Fleeing function
        /// </summary>
        /// <param name="dX">Position's x difference between player's ship and the enemy</param>
        /// <param name="gyre">The new rotation</param>
        /// <param name="deltaTime">The time since the last update</param>
        private void Flee(float dX, float gyre, float deltaTime)
        {

            if (dX < 0)
            {

                rotation = (float)Math.PI + gyre;
                position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime * 2);
                position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime * 2);
            }
            else
            {
                rotation = gyre;
                position.X += (float)(velocity * Math.Cos(gyre) * deltaTime * 2);
                position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime * 2);
            }

        }

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
                setAnim(5, -1);
                Audio.PlayEffect("brokenBone02");
            }
        }

    } // class EnemyScared
}
