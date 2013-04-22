using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the enemy that shoots 6 balls
    /// </summary>
    class EnemyMineShotA : EnemyShot
    {
        /// <summary>
        /// Time between one direction and other
        /// </summary>
        private float timeToMove = 3.0f;
        
        /// <summary>
        /// Random displacement in the X axis
        /// </summary>
        private float despX = 1.0f;
        
        /// <summary>
        /// Random displacement in the Y axis
        /// </summary>
        private float despY = -1.0f;

        /// <summary>
        /// Global Random
        /// </summary>
        public static Random random = new Random();

        /// <summary>
        /// EnemyMineShotA's constructor
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
        public EnemyMineShotA(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship,
            float timeToShot, float shotVelocity, int shotPower)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, 10, life, value, ship,
                timeToShot, shotVelocity, shotPower)
        {
            setAnim(0);

            despX = random.Next(-5, 0);
            despY = random.Next(-5, 5);

            Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(39, 14);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(39, 66);
            points[5] = new Vector2(20, 60);
            collider = new Collider(camera, true, position, rotation, points, 35, frameWidth, frameHeight);
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
                //Change the direction if it has to
                timeToMove -= deltaTime;
                if (timeToMove <= 0)
                {
                    
                    despX = random.Next(-5, 5);
                    despY = random.Next(-5, 5);

                    position.X += (float)(velocity * despX * deltaTime);
                    position.Y += (float)(velocity * despY * deltaTime);

                    timeToMove = 3.0f;

                }
                else
                {
                    position.X += (float)(velocity * despX * deltaTime);
                    position.Y += (float)(velocity * despY * deltaTime);
                }

                // Shots if it has to
                timeToShot -= deltaTime;
                if (timeToShot <= 0)
                {
                    SixShots();
                    timeToShot = 4.0f;
                }

            } // if (life > 0)

            // We test that the enemy doesn't go out of the level
            position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);

        } // Update

        /// <summary>
        /// Shots 6 balls in 6 diferent directions
        /// </summary>
        private void SixShots()
        {
            setAnim(1);

            Vector2 pos = new Vector2(position.X + 25, position.Y + 25);
            float rot = 0.75f;
            //Bottom, right
            Shot s1 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Bottom, center
            pos.Y = position.Y + 33;
            pos.X = position.X;
            rot = 1.55f;
            Shot s2 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Bottom, left
            pos.Y = position.Y + 25;
            pos.X = position.X - 25;
            rot = 2.33f;
            Shot s3 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, left
            pos.Y = position.Y - 25;
            pos.X = position.X - 25;
            rot = 3.92f;
            Shot s4 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, center
            pos.Y = position.Y - 33;
            pos.X = position.X;
            rot = -1.55f;
            Shot s5 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, right
            pos.Y = position.Y - 25;
            pos.X = position.X + 25;
            rot = -0.75f;
            Shot s6 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(s1);
            shots.Add(s2);
            shots.Add(s3);
            shots.Add(s4);
            shots.Add(s5);
            shots.Add(s6);

        } // SixShots

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

    } // Class EnemyMineShotA
}
