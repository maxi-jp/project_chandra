using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the first final boss
    /// </summary>
    class FinalBoss1 : Enemy
    {
        // ------------------------------
        // -----     Attributes     -----
        // ------------------------------

        /// <summary>
        /// Tell us the phase of the enemy (1, 2 or 3)
        /// </summary>
        private int phase;
        /// <summary>
        /// Tell us if the enemy have to go down or up.
        /// </summary>
        private Boolean down;
        /// <summary>
        /// Time between shots
        /// </summary>
        protected float timeToShotFront, timeToShotWings;
        /// <summary>
        /// Time until next shot
        /// </summary>
        protected float timeToShotFrontAux, timeToShotWingsAux;
        /// <summary>
        /// 
        /// </summary>
        protected int shotFrontPower, shotWingsPower;
        /// <summary>
        /// 
        /// </summary>
        protected float shotFrontVelocity, shotWingsVelocity;
        /// <summary>
        /// 
        /// </summary>
        private List<Shot> shots;

        // ---------------------------
        // -----     BUILDER     -----
        // ---------------------------

        /// <summary>
        /// This is the builder for the class FinalBoss1
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
        public FinalBoss1(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, int value, Ship Ship, List<Shot> shots)
            : base (camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, value, Ship)
        {
            phase = 1;
            down = true;

            //Front shot
            timeToShotFront = 0.5f;
            shotFrontPower = 200;
            shotFrontVelocity = 500f;

            //Wing shots
            timeToShotWings = 0.5f;
            shotWingsPower = 200;
            shotWingsVelocity = 500f;

            //Shots' list
            this.shots = shots;
        }

        // ------------------------------------
        // -----     Public functions     -----
        // ------------------------------------
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (phase == 1) phase1(deltaTime);
        }

        // -------------------------------------
        // -----     Private functions     -----
        // -------------------------------------

        private void phase1(float deltaTime)
        {
            if (down) position.Y += deltaTime * velocity;
            else position.Y -= deltaTime * velocity;
            changeDirection();

            //Front shot
            if (timeToShotFrontAux <= 0)
                ShotFront();
            timeToShotFrontAux -= deltaTime;

            //Wings shot
            if (timeToShotWingsAux <= 0)
                ShotWings();
            timeToShotWingsAux -= deltaTime;
        }

        /// <summary>
        /// change the direction of the enemy when it achieve the limit of the screen
        /// </summary>
        private void changeDirection()
        {
            if (position.Y + collider.Height / 2 >= SuperGame.screenHeight)
                down = false;
            else if (position.Y - collider.Height / 2 <= 0)
                down = true;
        }

        private void ShotFront()
        {
            Audio.PlayEffect("laserShot01");

            Shot nuevo = new Shot(camera, level, new Vector2(position.X - 20, position.Y), rotation, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
                GRMng.textureL1, SuperGame.shootType.normal, shotFrontVelocity, shotFrontPower);

            shots.Add(nuevo);
            timeToShotFrontAux = timeToShotFront;
        }

        private void ShotWings()
        {
            float angle;
            if (Ship.position.X != position.X)
            {
                double dist = (position.Y - Ship.position.Y) / (Ship.position.X - position.X);
                angle = rotation - (float)Math.Atan(dist);
            }
            else
            {
                if (Ship.position.Y > position.Y)
                    angle = (float)Math.PI / 2;
                else if (Ship.position.Y < position.Y)
                    angle = (float)-Math.PI / 2;
                else 
                    angle = 0f;
            }

                Audio.PlayEffect("laserShot01");

            Shot nuevo = new Shot(camera, level, new Vector2(position.X - 10, position.Y + collider.Height / 2), angle, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8, GRMng.textureL1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);
            shots.Add(nuevo);

            nuevo = new Shot(camera, level, new Vector2(position.X - 10, position.Y - collider.Height / 2), angle, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8, GRMng.textureL1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);
            shots.Add(nuevo);

            timeToShotWingsAux = timeToShotWings;
        }
    }
}
