using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class FinalBoss1Turret1 : Enemy
    {
        /// <summary>
        /// Time passed since the last shot of turret
        /// </summary>
        private float lastTimeShot;

        /// <summary>
        /// Speed of rotation
        /// </summary>
        private float rotationVelocity;

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
        public FinalBoss1Turret1(Camera camera, Level level, Vector2 position, Ship Ship)
            : base(camera, level, position, 0, GRMng.frameWidthFinalBoss1Turret1, GRMng.frameHeightFinalBoss1Turret1, 
            GRMng.numAnimsFinalBoss1Turret1, GRMng.frameCountFinalBoss1Turret1, GRMng.loopingFinalBoss1Turret1, 
            SuperGame.frameTime12, GRMng.textureFinalBoss1Turret1, 0, 0, 100, 1, Ship)
        {
            this.shots = new List<Shot>();
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

            lastTimeShot += deltaTime;

            float pX = position.X;
            float pY = position.Y;
            float distance = frameWidth / 2 + 10;

            float dY = -ship.position.Y + position.Y;
            float dX = -ship.position.X + position.X;

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

            if (lastTimeShot >= 1f && !IsDead())
            {
                lastTimeShot = 0;
                buildShot(pX, pY);
            }

            if (lastTimeShot >= 0.8f && !IsDead())
                setAnim(1);

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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Shot s in shots)
                s.Draw(spriteBatch);

            base.Draw(spriteBatch);
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

            float radius = Math.Abs(frameHeight/2 * frameHeight/2 + frameWidth/2 * frameWidth/2);

            collider = new Collider(camera, true, position, rotation, points, radius, frameWidth, frameHeight);

        } 
        
        /// <summary>
        /// Builds a shot
        /// </summary>
        private void buildShot(float x, float y)
        {
            setAnim(0);

            Vector2 shotPos = new Vector2(x, y);
            Shot shot = new Shot(camera, level, shotPos, rotation, GRMng.frameWidthFinalBoss1Turret1Shot, 
                GRMng.frameHeightFinalBoss1Turret1Shot, GRMng.numAnimsFinalBoss1Turret1Shot, 
                GRMng.frameCountFinalBoss1Turret1Shot, GRMng.loopingFinalBoss1Turret1Shot, SuperGame.frameTime8, 
                GRMng.textureFinalBoss1Turret1Shot, SuperGame.shootType.normal, 200, 10);
            shots.Add(shot);
        }

        public override void Damage(int i)
        {
            base.Damage(i);

            if (life <= 0)
                setAnim(2, -1);
        }
    }
}
