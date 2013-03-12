using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class FinalBoss1Turret : Enemy
    {
        /// <summary>
        /// Time passed since the last shot of turret
        /// </summary>
        private float lastTimeShot;

        /// <summary>
        /// List of shots of the turret
        /// </summary>
        private List<Shot> shots;
        
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
        public FinalBoss1Turret(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, int value, Ship Ship, List<Shot> shots)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, value, Ship)
        {
            this.position = position;            
            this.rotation = 0;
            this.velocity = 0;
            this.life = 100;
            this.shots = shots;

            lastTimeShot = 0;

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

            lastTimeShot += deltaTime;

            float pX = position.X;
            float pY = position.Y;
            float distance = GRMng.frameWidthFB1T / 2 + 10;

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

            if (lastTimeShot >= 1f)
            {
                lastTimeShot = 0;
                buildShot(pX, pY);
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
            points[1] = new Vector2(0, frameHeight);
            points[2] = new Vector2(frameWidth, frameHeight);
            points[3] = new Vector2(frameWidth, 0);
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);

        }
        
        /// <summary>
        /// Builds a shot
        /// </summary>
        private void buildShot(float x, float y)
        {
            Vector2 shotPos = new Vector2(x, y);
            Shot shot = new Shot(camera, level, shotPos, rotation, GRMng.frameWidthSFB1T, GRMng.frameHeightSFB1T,
                GRMng.numAnimsSFB1T, GRMng.frameCountSFB1T, GRMng.loopingSFB1T, SuperGame.frameTime8, GRMng.textureSFB1T,
                SuperGame.shootType.normal, 100, 100);
            shots.Add(shot);
        }
    }
}
