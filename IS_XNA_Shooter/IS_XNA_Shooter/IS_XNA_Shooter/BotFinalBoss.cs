using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class BotFinalBoss:Enemy
    {
       // ------------------------------
        // -----     Attributes     -----
        // ------------------------------

        bool down;

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

        protected int life;

        // ---------------------------
        // -----     BUILDER     -----
        // ---------------------------

        /// <summary>
        /// This is the builder for the class BotFinalBoss
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
        public BotFinalBoss(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, int value, Ship Ship, List<Shot> shots, bool down)
            : base (camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, 0, velocity, life, value, Ship)
        {
            this.down = down;
                      
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

            //Collider points
            colliderPoints();
       }

        // ------------------------------------
        // -----     Public functions     -----
        // ------------------------------------
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //Front shot
            if (timeToShotFrontAux <= 0)
                ShotFront();
            timeToShotFrontAux -= deltaTime;

            //Wings shot
            if (timeToShotWingsAux <= 0)
                ShotWings();
            timeToShotWingsAux -= deltaTime;

            //Bot move
            if (down)
                position.Y += deltaTime * velocity * 3;
            else
                position.Y -= deltaTime * velocity * 3;
            changeDirection();

            if (position.X < SuperGame.screenWidth/2) { position.X += deltaTime * velocity; }
            else if (position.X > SuperGame.screenWidth/2) { position.X -= deltaTime * velocity; }

        }

        // -------------------------------------
        // -----     Private functions     -----
        // -------------------------------------

        private void changeDirection()
        {
            if (position.Y + this.frameHeight / 2 >= SuperGame.screenHeight)
                down = false;
            else if (position.Y - this.frameHeight / 2 <= 0)
                down = true;
        }

        private void ShotFront()
        {
            Audio.PlayEffect("laserShot01");
            //x - 20 
            Shot nuevo = new Shot(camera, level, new Vector2(position.X -60 , position.Y), rotation, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
                GRMng.textureL1, SuperGame.shootType.normal, shotFrontVelocity, shotFrontPower);

            shots.Add(nuevo);
            timeToShotFrontAux = timeToShotFront;
        }

        private void ShotWings()
        {
            float angle;
            float angleUp;
            float angleDown;
            if (ship.position.X != position.X)
            {
                double dist = (position.Y - ship.position.Y) / (ship.position.X - position.X);
               angle = rotation - (float)Math.Atan(dist);
               if (ship.position.X - position.X > 0) { angle += (float)Math.PI; angle = angle % (float)(Math.PI * 2); }
            }
            else
            {
                if (ship.position.Y > position.Y)
                    
                    angle = (float)Math.PI / 2;
                    
                else if (ship.position.Y < position.Y)
                    angle = (float)-Math.PI / 2;
                else 
                    angle = 0f;
            }

            angleUp = angle;
            angleDown = angle;
            // up
            if(angle > Math.PI+Math.PI/5)
            {
                angleDown =(float)(Math.PI + Math.PI / 5 );
                angleUp = angle;
                    }
             else if (angle < Math.PI - Math.PI / 5)
                {
                    angleUp = (float) (Math.PI - Math.PI /5);
                    angleDown = angle; 
                }
           

                Audio.PlayEffect("laserShot01");
            //position.X - 10, position.Y + collider.Height / 2
            Shot nuevo = new Shot(camera, level, new Vector2(position.X , position.Y  + this.frameHeight / 2), angleDown, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8, GRMng.textureL1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);
            shots.Add(nuevo);
           
           
            //position.X - 10, position.Y - collider.Height / 2
            nuevo = new Shot(camera, level, new Vector2(position.X , position.Y - this.frameHeight / 2), angleUp, GRMng.frameWidthL1, GRMng.frameHeightL1,
                GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8, GRMng.textureL1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);
            shots.Add(nuevo);
            

            timeToShotWingsAux = timeToShotWings;
        }




        private void colliderPoints()
        {
            Vector2[] points = new Vector2[4];
            points[0] = new Vector2(29, 12);
            points[1] = new Vector2(8, 23);
            points[2] = new Vector2(1, 12);
            points[3] = new Vector2(8, 2);


            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);
        }

        public override void Damage(int i)
        {
            life -= i;

            if (life <= 0)
            {
                colisionable = false;
                setAnim(0, -1);
            }
        }

    }




}
