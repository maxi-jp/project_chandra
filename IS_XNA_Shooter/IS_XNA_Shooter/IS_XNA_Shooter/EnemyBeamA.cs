using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the enemy that moves like a beam in GameA
    /// </summary>
    class EnemyBeamA : Enemy
    {
        /// <summary>
        /// The change of state's delay  
        /// </summary>
        private float timeToBeam = 4; 

        /// <summary>
        /// The count for change the state
        /// </summary>
        private float timeToBeamAux;

        /// <summary>
        /// An auxiliar variable
        /// </summary>
        private float gyre;

        /// <summary>
        /// An auxiliar variable
        /// </summary>
        private float dX;

        /// <summary>
        /// An auxiliar variable
        /// </summary>
        private float dY;

        
        /// <summary>
        /// The state of the enemy
        /// </summary>
        private enum enemyState
        {
            ONWAIT,
            ONBEAM
        };

        /// <summary>
        /// current state of the enemy
        /// </summary>
        private enemyState currentState;

        /// <summary>
        /// EnemyBeamA's constructor
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
        public EnemyBeamA(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base (camera, level, position, rotation,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime, texture,
                timeToSpawn, velocity, life, value, ship)
        {
            Vector2[] points = new Vector2[7];
            points[0] = new Vector2(9, 30);
            points[1] = new Vector2(23, 24);
            points[2] = new Vector2(41, 28);
            points[3] = new Vector2(72, 40);
            points[4] = new Vector2(41, 52);
            points[5] = new Vector2(23, 56);
            points[6] = new Vector2(9, 50);
            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);

            setAnim(0);
            gyre = dX = dY = 0;
            timeToBeamAux = timeToBeam;

            currentState = enemyState.ONWAIT;
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
                switch (currentState)
                {
                    case enemyState.ONWAIT:

                        timeToBeamAux -= deltaTime;
                        if (timeToBeamAux <= 0) // change the state
                        {
                            setAnim(1);
                            timeToBeamAux = timeToBeam;
                            currentState = enemyState.ONBEAM;
                        }
                        else
                        {
                            dY = -ship.position.Y + position.Y;
                            dX = -ship.position.X + position.X;
                            gyre = (float)Math.Atan(dY / dX);
                            if (dX < 0)
                                rotation = gyre;
                            else
                                rotation = (float)Math.PI + gyre;
                        }

                        break;

                    case enemyState.ONBEAM:

                        if (dX < 0) // its on the left of the player
                        {
                            position.X += (float)(velocity * Math.Cos(gyre) * deltaTime);
                            position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime);

                            if ((position.X > level.width - collider.radius) ||
                                (position.Y > level.height - collider.radius) ||
                                (position.Y < collider.radius))
                            {
                                currentState = enemyState.ONWAIT;
                                setAnim(0);
                            }
                        }
                        else // its on the right of the player
                        {
                            position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime);
                            position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime);

                            if ((position.X < collider.radius) ||
                                (position.Y > level.height - collider.radius) ||
                                (position.Y < collider.radius))
                            {
                                currentState = enemyState.ONWAIT;
                                setAnim(0);
                            }
                        }

                        break;

                } // switch (currentState)
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
                Audio.PlayEffect("brokenBone02");
            }
        }

    } // class EnemyBeamA

}
