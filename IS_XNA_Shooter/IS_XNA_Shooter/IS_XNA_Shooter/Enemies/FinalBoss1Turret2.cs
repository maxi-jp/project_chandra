using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class FinalBoss1Turret2 : Enemy
    {
        /// <summary>
        /// Time passed since the last shot of turret
        /// </summary>
        private float lastTimeShot;

        /// <summary>
        /// Speed of rotation
        /// </summary>
        private float rotationVelocity;
        
        //-----------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Builder of FinalBoss1Turret
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
        /// <param name="shots"></param>
        public FinalBoss1Turret2(Camera camera, Level level, Vector2 position, Ship Ship)
            : base(camera, level, position, 0, GRMng.frameWidthFinalBoss1Turret2, GRMng.frameHeightFinalBoss1Turret2, 
            GRMng.numAnimsFinalBoss1Turret2, GRMng.frameCountFinalBoss1Turret2, GRMng.loopingFinalBoss1Turret2, 
            SuperGame.frameTime12, GRMng.textureFinalBoss1Turret2, 0, 0, 100, 1, Ship)
        {
            lastTimeShot = 0;
            rotationVelocity = 0;

            addCollider();
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates the turret
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (rotationVelocity < 40)
                rotationVelocity += 5 * deltaTime;
            else if (velocity <= 0)
                velocity = 200;

            if (!IsDead())
            {
                rotation += rotationVelocity * deltaTime;

                float dY = position.Y - ship.position.Y;
                float dX = position.X - ship.position.X;

                float gyre = Math.Abs((float)Math.Atan(dY / dX));

                //look to first clock
                if (dX <= 0 && dY <= 0)
                {
                    position.X += velocity * deltaTime * (float)Math.Cos(gyre);
                    position.Y += velocity * deltaTime * (float)Math.Sin(gyre);
                }
                //look to second clock
                else if (dX >= 0 && dY <= 0)
                {
                    position.X -= velocity * deltaTime * (float)Math.Cos(gyre);
                    position.Y += velocity * deltaTime * (float)Math.Sin(gyre);
                }
                //look to third clock
                else if (dX >= 0 && dY >= 0)
                {
                    position.X -= velocity * deltaTime * (float)Math.Cos(gyre);
                    position.Y -= velocity * deltaTime * (float)Math.Sin(gyre);
                }
                //look to fourth clock
                else
                {
                    position.X += velocity * deltaTime * (float)Math.Cos(gyre);
                    position.Y -= velocity * deltaTime * (float)Math.Sin(gyre);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Adds points of collision
        /// </summary>
        private void addCollider()
        {
            Vector2[] points = new Vector2[4];
            points[0] = new Vector2(0, 0);
            points[1] = new Vector2(frameWidth, 0);
            points[2] = new Vector2(frameWidth, frameHeight);
            points[3] = new Vector2(0, frameHeight);

            float radius = (float)Math.Sqrt(frameHeight/2 * frameHeight/2 + frameWidth/2 * frameWidth/2);
            collider = new Collider(camera, true, position, rotation, points, radius, frameWidth, frameHeight);
        }

        public override void  Damage(int i)
        {
            base.Damage(i);

            if (life <= 0) 
                setAnim(1, -1);
        }
    }
}
