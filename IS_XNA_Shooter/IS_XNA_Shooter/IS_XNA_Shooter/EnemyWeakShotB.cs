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
    class EnemyWeakShotB : Enemy
    {
        /// <summary>
        /// Shots' list
        /// </summary>
        private List<Shot> shots;

        /// <summary>
        /// Time between two different shots
        /// </summary>
        private float timeToShot = 2.0f;

        /// <summary>
        /// Time between two different shots (aux)
        /// </summary>
        private float timeToShotAux;

        /// <summary>
        /// Shot velocity
        /// </summary>
        private float shotVelocity = 300f;

        /// <summary>
        /// Shot power
        /// </summary>
        private int shotPower = 200;

        /// <summary>
        /// EnemyWeakShot's constructor
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
        public EnemyWeakShotB(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, (float)Math.PI,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime,
                texture, timeToSpawn, velocity, life, value, ship)
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

            setAnim(1);

            timeToShotAux = timeToShot;

            shots = new List<Shot>();
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
                position.X -= deltaTime * velocity;
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
            } // if life > 0

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

        } // Update

        /// <summary>
        /// Draws the enemy 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Shot s in shots)
                s.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

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
            // the dead condition of this enemy is when its death animation has ended
            // and the shots shoted when it was alive are no longer active
            return (!animActive && (shots.Count() == 0));
        }

    } // class EnemyWeakShotB
}
