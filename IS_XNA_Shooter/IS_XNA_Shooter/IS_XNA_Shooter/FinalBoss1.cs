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
        private List<Enemy> enemies;
        private float seconds;
        private bool movingToCenter;
        

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
        /// <param name="ship"></param>
        public FinalBoss1(Camera camera, Level level,Vector2 position, List<Shot> shots, List <Enemy> enemies)
            : base (camera, level, position, (float)Math.PI, GRMng.frameWidthFinalBoss1, GRMng.frameHeightFinalBoss1, 
                GRMng.numAnimsFinalBoss1, GRMng.frameCountFinalBoss1, GRMng.loopingFinalBoss1, SuperGame.frameTime12,
                GRMng.textureFinalBoss1, 0, 40, 100000, 1, null)
        {
            phase = 2;
            down = true;
            movingToCenter = true;
            seconds = 10f;
            
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

            this.enemies = enemies;

            
        }

        // ------------------------------------
        // -----     Public functions     -----
        // ------------------------------------
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (phase == 1) phase1(deltaTime);
            if (phase == 2) phase2(deltaTime);
        }

        // -------------------------------------
        // -----     Private functions     -----
        // -------------------------------------

        private void phase1(float deltaTime)
        {
            if (down) 
                position.Y += deltaTime * velocity;
            else 
                position.Y -= deltaTime * velocity;
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

        private void phase2(float deltaTime)
        {
            if (movingToCenter){

                if (position.Y < SuperGame.screenHeight / 2) { position.Y += deltaTime * velocity *3; }
                else if (position.Y > SuperGame.screenHeight / 2) { position.Y -= deltaTime * velocity * 3; }

                if (position.X < (SuperGame.screenWidth * 4 / 6)) { position.X += deltaTime * velocity * 3; }
                else if (position.X > (SuperGame.screenWidth * 4 / 6)) { position.X -= deltaTime * velocity * 3; }

               if (position.X == this.frameWidth / 2 && position.Y == this.frameHeight / 2) { movingToCenter = false; }
            }

            //Front shot
            if (timeToShotFrontAux <= 0)
            {
                ShotFront();
                timeToShotFrontAux = 0.35f;
            }
            timeToShotFrontAux -= deltaTime;

            //Wings shot
            if (timeToShotWingsAux <= 0)
            {
                ShotWings();
                timeToShotWingsAux = 0.35f;
            }
            timeToShotWingsAux -= deltaTime;

            seconds +=  deltaTime;
            if (seconds > 5)
            {
                Vector2 positionEnemy = new Vector2(position.X , position.Y  + this.frameHeight / 2); ///(470, 100);
                Enemy e = new BotFinalBoss(camera, level, positionEnemy, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 150, 100, 1, ship, shots,true);
                e.SetActive();
                enemies.Add(e);

                Vector2 positionEnemy1 = new Vector2(position.X, position.Y + this.frameHeight / 2); //(470, 300);
                Enemy e1 = new BotFinalBoss(camera, level, positionEnemy1, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 150, 100, 1, ship, shots,false);
                e1.SetActive();
                enemies.Add(e1);

                Vector2 positionEnemy2 = new Vector2(position.X , position.Y  - this.frameHeight / 2);//(470, 500);
                Enemy e2 = new BotFinalBoss(camera, level, positionEnemy2, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 150, 100, 1, ship, shots,true);
                e2.SetActive();
                enemies.Add(e2);

                Vector2 positionEnemy3 = new Vector2(position.X , position.Y  - this.frameHeight / 2);
                Enemy e3 = new BotFinalBoss(camera, level, positionEnemy3, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 150, 100, 1, ship, shots,false);
                e3.SetActive();
                enemies.Add(e3);

                seconds = 0;
            }
        }

        /// <summary>
        /// change the direction of the enemy when it achieve the limit of the screen
        /// </summary>
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
            Shot nuevo = new Shot(camera, level, new Vector2(position.X -120 , position.Y), rotation, GRMng.frameWidthL1, GRMng.frameHeightL1,
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
    }
}
