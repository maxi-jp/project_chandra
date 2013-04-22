using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the enemy that shoots one laser in GameB
    /// </summary>
    class EnemyLaserB : Enemy
    {
        /// <summary>
        /// Says if it has to shoot or not
        /// </summary>
        private Boolean shooting = false;
       
        /// <summary>
        /// Rotatio Matrix for the laser's points
        /// </summary>
        private Matrix rotationMatrix;

        
        //For the Laser

        /// <summary>
        /// Laser's rectanlge
        /// </summary>
        private Rectangle rect;
        
        /// <summary>
        /// Laser's actual points
        /// </summary>
        private Vector2 p1, p2;
        
        /// <summary>
        /// Laser's original points
        /// </summary>
        private Vector2 p1Orig, p2Orig;
        
        /// <summary>
        /// Shot velocity
        /// </summary>
        private float shotVelocity = 80f;
        
        /// <summary>
        /// Time between shots
        /// </summary>
        private float timeToShot = 6.0f;
        
        /// <summary>
        /// Shot power
        /// </summary>
        private int shotPower = 1000;
        
        /// <summary>
        /// Enemy's shot, in this case a laser
        /// </summary>
        private Shot shot;
        
        /// <summary>
        /// Says if the enemy is going up or down
        /// </summary>
        private Boolean goingUp = true;
        
        /// <summary>
        /// Time to set the shot
        /// </summary>
        private float shootingCont = 0.1f;

        /// <summary>
        /// To set shootingCont only once
        /// </summary>
        private Boolean shootingContSet = false;

        /// <summary>
        /// EnemyLaserB's constructor
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
        public EnemyLaserB(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, (float)Math.PI, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, 100, 100, value, ship)
        {
            setAnim(0);
            frameTime = SuperGame.frameTime10;
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(14, 37.5f);
            points[1] = new Vector2(19, 29);
            points[2] = new Vector2(31, 19);
            points[3] = new Vector2(44, 16);
            points[4] = new Vector2(69, 37.5f);
            points[5] = new Vector2(44, 59);
            points[6] = new Vector2(31, 56);
            points[7] = new Vector2(19, 46);
            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);

            //For the Laser
            Rectangle rect = new Rectangle(0, 0, 2000, 2);
            p1 = new Vector2();
            p2 = new Vector2();
            p1Orig = new Vector2(0, 0);
            p2Orig = new Vector2(675, 0);
            shot = new Shot(camera, level, p1, rotation, GRMng.frameWidthELBulletB, GRMng.frameHeightELBullet,
                GRMng.numAnimsELBullet, GRMng.frameCountELBullet, GRMng.loopingELBullet, SuperGame.frameTime8,
                GRMng.textureELBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            

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
                //We have to see if it is mooving up or down
                //Is it is going up
                if (goingUp)
                {
                    timeToShot -= deltaTime;
                    //If itsn't shooting it moves
                    if (timeToShot > 4)
                    {
                        if (position.Y >= 50)
                            position.Y -= velocity * deltaTime;
                        else
                            goingUp = false;

                    }
                    //Prepare to shoot
                    else if (timeToShot > 3)
                    {
                        if (currentAnim != 1) setAnim(1, 2);
                        if (position.Y >= 50)
                            position.Y -= velocity * 0.5f * deltaTime;
                        else
                            goingUp = false;
                    }
                    //Shoot
                    else if (timeToShot > 0)
                    {
                        if (currentAnim != 2) setAnim(2);
                        shooting = true;
                        LaserShot();
                        shot.Update(deltaTime);

                    }
                    //If it stops shooting, it has to move again
                    else
                    {
                        setAnim(0);
                        shooting = false;
                        timeToShot = 6.0f;
                    }
                }
                //if it is going down
                else
                {
                    timeToShot -= deltaTime;
                    //If itsn't shooting it mooves
                    if (timeToShot > 4)
                    {
                        if (position.Y <= level.height - 50)
                            position.Y += velocity * deltaTime;
                        else
                            goingUp = true;
                    }
                    //Prepare to shoot
                    else if (timeToShot > 3)
                    {
                        if (currentAnim != 1) setAnim(1, 2);
                        if (position.Y <= level.height - 50)
                            position.Y += velocity * 0.5f * deltaTime;
                        else
                            goingUp = true;
                    }
                    //Shoot
                    else if (timeToShot > 0)
                    {
                        if (currentAnim != 2) setAnim(2);
                        shooting = true;
                        if (!shootingContSet)
                        {
                            shootingContSet = true;
                            shootingCont = 0.1f;
                        }
                        LaserShot();
                        shot.Update(deltaTime);
                    }
                    //If it stops shooting, it has to move again
                    else
                    {
                        setAnim(0);
                        timeToShot = 6.0f;
                        shooting = false;
                        shootingCont = 0.1f;
                        shootingContSet = false;
                    }
                }
            }
            // shots: 
            if (shooting)
            {
                shot.Update(deltaTime);
                //shot-player colisions
                if (shootingCont >= 0) 
                    shootingCont -= deltaTime;
                else 
                    if (ship.collider.CollisionTwoPoints(shot.collider.points[0], shot.collider.points[1]))
                    {
                        // the player is hitted:
                        ship.Damage(shot.GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        /*if (ship.GetLife() > 0)
                            shots.RemoveAt(i);*/
                    }
                
            }

        } // Update

        /// <summary>
        /// Draws the enemy 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shooting)
                shot.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Calculates the enemy's laser points and rectangle
        /// </summary>
        private void LaserShot()
        {
            //The calculation of the rectangle
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            int width = level.width + 200;

            p1 = Vector2.Transform(p1Orig, rotationMatrix);
            p1 += position;

            p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 += position;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            shot.position = p2;
            shot.rotation = rotation;

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
                shooting = false;
                setAnim(3, -1);
                Audio.PlayEffect("brokenBone02");
            }
        }

    } // Class EnemyMineShot
}
