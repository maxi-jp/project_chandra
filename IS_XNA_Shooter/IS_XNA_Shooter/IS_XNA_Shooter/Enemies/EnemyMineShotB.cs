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
    class EnemyMineShotB : Enemy
    {
        /// <summary>
        /// Time between one direction and other
        /// </summary>
        private float timeToMove = 3.0f;

        /// <summary>
        /// Random displacement in the X axis
        /// </summary>
        private float despX = -2.0f;

        /// <summary>
        /// Random displacement in the Y axis
        /// </summary>
        private float despY= 1.0f;

        /// <summary>
        /// List of the enemy's shots
        /// </summary>
        private List<Shot> shots;

        /// <summary>
        /// Time between two different shoots
        /// </summary>
        private float timeToShot = 4.0f;

        /// <summary>
        /// Shot velocity
        /// </summary>
        private float shotVelocity = 140f;

        /// <summary>
        /// Shot power
        /// </summary>
        private int shotPower = 200;

        /// <summary>
        /// Global Random
        /// </summary>
        public static Random random = new Random();

        /// <summary>
        /// EnemyMineShot's constructor
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
        public EnemyMineShotB(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, 25, life, value, ship)
        {
            setAnim(0);

            despX = -5;
            despY = -5;

            Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(39, 14);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(39, 66);
            points[5] = new Vector2(20, 60);
            collider = new Collider(camera, true, position, rotation, points, 35, frameWidth, frameHeight);

            this.shots = new List<Shot>();
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
                    
                    despX = random.Next(-5, 0);
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

            // shots:
            for (int i = 0; i < shots.Count(); i++)
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
                else  // shots-player colisions
                {
                    if (ship.collider.Collision(shots[i].position))
                    {
                        // the player is hitted:
                        ship.Damage(shots[i].GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        if (ship.GetLife() > 0)
                            shots.RemoveAt(i);
                    }
                }
            }

            // We test that the enemy doesn't go out of the level
            //position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);

        } // Update

        /// <summary>
        /// Draws the enemy 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Shot shot in shots)
                shot.Draw(spriteBatch);
        }

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

        /// <summary>
        /// Kills the enemy and its shots
        /// </summary>
        public override void Kill()
        {
            base.Kill();

            shots.Clear();
        }

        /// <summary>
        /// The dead condition of this enemy is when its death animation has ended
        /// and the shots shoted when it was alive are no longer active
        /// </summary>
        /// <returns>dead condition</returns>
        public override bool DeadCondition()
        {
            return (!animActive && (shots.Count() == 0));
        }

    }//Class EnemyMineShotB
}
