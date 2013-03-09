using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class manage the final boss heroe 1
    /// </summary>
    class FinalBossHeroe1 : Enemy
    {
        /// <summary>
        /// Enum for the states of the enemy
        /// </summary>
        private enum State
        {
            move,
            attack
        };

        /// <summary>
        /// The destiny point where the enemy will move it
        /// </summary>
        private Vector2 destiny;

        /// <summary>
        /// It marks the times of enemy to move it or attack to us
        /// </summary>
        private float totalTime;

        /// <summary>
        /// It marks the last time that the enemy shots us
        /// </summary>
        private float shotLastTime = 0.5f;

        /// <summary>
        /// State of the enemy.
        /// </summary>
        private State currentState;

        /// <summary>
        /// List of shots in the level
        /// </summary>
        private List<Shot> shots;

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Builds the class final boss heroe 1
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="level"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="numAnim"></param>
        /// <param name="frameCount"></param>
        /// <param name="looping"></param>
        /// <param name="frametime"></param>
        /// <param name="texture"></param>
        /// <param name="velocity"></param>
        /// <param name="life"></param>
        /// <param name="value"></param>
        /// <param name="Ship"></param>
        public FinalBossHeroe1(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, int value, Ship Ship, List<Shot> shots)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, value, Ship)
        {
            this.position = new Vector2(level.width/2 - GRMng.frameWidthFBH1 / 2 + 400, 
                level.height / 2 - GRMng.frameHeightFBH1 / 2 + 400);            
            this.rotation = 0;
            this.velocity = 500;
            this.life = 1;
            destiny = position;
            currentState = State.attack;
            totalTime = 0;
            this.shots = shots;
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates the final boss heroe 1
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            totalTime += deltaTime;

            switch (currentState) 
            {
                case State.attack :
                    attack(deltaTime);
                    if (totalTime > 1)
                    {
                        currentState = State.move;
                        Random random = new Random();
                        destiny.X = random.Next(GRMng.frameWidthFBH1, level.width - GRMng.frameWidthFBH1);
                        destiny.Y = random.Next(GRMng.frameHeightFBH1, level.height - GRMng.frameHeightFBH1);
                    }
                    break;

                case State.move :
                    move(deltaTime);
                    if (Math.Abs(destiny.X - position.X) < 10 && Math.Abs(destiny.Y - position.Y) < 10)
                    {
                        currentState = State.attack;
                        totalTime = 0;
                    }
                    break;

                default :                    
                    break;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// The enemy looks to our ship and attack us
        /// </summary>
        /// <param name="deltaTime"></param>
        private void attack(float deltaTime)
        {
            shotLastTime += deltaTime;

            float pX = position.X;
            float pY = position.Y;
            float distance = GRMng.frameWidthFBH1 / 2 + 10;

            float dY = -Ship.position.Y + position.Y;
            float dX = -Ship.position.X + position.X;

            float gyre = Math.Abs((float)Math.Atan(dY / dX));

            //look to first clock
            if (dX <= 0 && dY <= 0)
            {
                rotation = gyre;
                pX += distance * (float)Math.Cos(gyre);
                pY += distance * (float)Math.Sin(gyre);
            }
            //look to second clock
            else if (dX >= 0 && dY <= 0)
            {
                rotation = (float)Math.PI - gyre;
                pX -= distance * (float)Math.Cos(gyre);
                pY += distance * (float)Math.Sin(gyre);
            }
            //look to third clock
            else if (dX >= 0 && dY >= 0)
            {
                rotation = (float)Math.PI + gyre;
                pX -= distance * (float)Math.Cos(gyre);
                pY -= distance * (float)Math.Sin(gyre);
            }
            //look to fourth clock
            else
            {
                rotation = -gyre;
                pX += distance * (float)Math.Cos(gyre);
                pY -= distance * (float)Math.Sin(gyre);
            }

            if (shotLastTime >= 0.2f)
            {
                shotLastTime = 0;

                Vector2 shotPos = new Vector2(pX, pY);
                Shot shot = new Shot(camera, level, shotPos, rotation, GRMng.frame_width_shot_fbh1, GRMng.frame_height_shot_fbh1, 
                    GRMng.num_anims_shot_fbh1, GRMng.frame_count_shot_fbh1, GRMng.looping_shot_fbh1, SuperGame.frameTime8, GRMng.texture_shot_fbh1, 
                    SuperGame.shootType.normal, 500, 100);
                shots.Add(shot);
            }
        }

        /// <summary>
        /// The enemy moves to other point
        /// </summary>
        /// <param name="deltaTime"></param>
        private void move(float deltaTime)
        {
            float dY = -destiny.Y + position.Y;
            float dX = -destiny.X + position.X;

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
}
