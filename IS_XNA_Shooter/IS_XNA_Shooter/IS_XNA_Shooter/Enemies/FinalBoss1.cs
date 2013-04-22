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
        private Boolean singletonLaserVelocityBoss;
        private Boolean singletonEnemyHeroe;
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
        private float secondsBot;
        private float secondsHeroe;
        private bool movingToCenter;
        private bool movingToBack;

        //Laser
        private float shotVelocity = 80f;
        private float timeToShot = 3.0f;
        private float timeShoting = 0.5f;
        private int shotPower = 1;
        private bool shooting;
        Shot shot;

        // red dye
        private bool isDyeingRed;
        private float timeDyeingRed = 0.12f;
        private float timeDyeingRedAux;
        private Color dyeingColor = new Color(255, 63, 63);

        // ---------------------------
        // -----     BUILDER     -----
        // ---------------------------

        /// <summary>
        /// This is the builder for the class FinalBoss1
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
        /// <param name="velocity">The velocity of the enemy</param>
        /// <param name="life">The life of the enemy</param>
        /// <param name="value">The points you obtain if you kill it</param>
        /// <param name="ship">The player's ship</param>
        public FinalBoss1(Camera camera, Level level,Vector2 position, List <Enemy> enemies)
            : base (camera, level, position, (float)Math.PI, GRMng.frameWidthFinalBoss1, GRMng.frameHeightFinalBoss1, 
                GRMng.numAnimsFinalBoss1, GRMng.frameCountFinalBoss1, GRMng.loopingFinalBoss1, SuperGame.frameTime12,
                GRMng.textureFinalBoss1, 0, 40, 40000, 1, null)
        {
            //collider points
            colliderPoints();
            
            phase = 1;
            down = true;
            movingToCenter = true;
            movingToBack = true;
            singletonEnemyHeroe = true;
            singletonLaserVelocityBoss = true;
            secondsBot = 500;
            secondsHeroe = 500;
            
            //Front shot
            timeToShotFront = 1f;
            shotFrontPower = 200;
            shotFrontVelocity = 500f;

            //Wing shots
            timeToShotWings = 1f;
            shotWingsPower = 200;
            shotWingsVelocity = 500f;

            //Shots' list
            shots = new List<Shot>();

            this.enemies = enemies;

        /*    //LASER Phase 3
            shot = new Shot(camera, level, position, rotation, GRMng.frameWidthLaserBoss, GRMng.frameHeightLaserBoss,
    GRMng.numAnimsLaserBoss, GRMng.frameCountLaserBoss, GRMng.loopingLaserBoss, SuperGame.frameTime8,
    GRMng.textureLaserBoss, SuperGame.shootType.normal, 1, 1);*/

            isDyeingRed = false;
            timeDyeingRedAux = timeDyeingRed;
        }

        // ------------------------------------
        // -----     Public functions     -----
        // ------------------------------------
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (phase == 1 && !isDead()) phase1(deltaTime);
            if (phase == 2 && !isDead()) phase2(deltaTime);
            if (phase == 3 && !isDead()) phase3(deltaTime);

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

            if (isDyeingRed)
            {
                timeDyeingRedAux -= deltaTime;
                if (timeDyeingRedAux <= 0)
                {
                    color = Color.White;
                    isDyeingRed = false;
                }
            }

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Shot s in shots)
                s.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Damage(int i)
        {
            base.Damage(i);

            if (!isDyeingRed)
            {
                isDyeingRed = true;
                timeDyeingRedAux = timeDyeingRed;
                SetTransparency(128);
                color = dyeingColor;
            }

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                shooting = false;
                setAnim(1, -1);
                Audio.PlayEffect("brokenBone02");
            }
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

            if (life < 25000) 
                phase = 2;
        }

        private void phase2(float deltaTime)
        {
            if (movingToCenter){

                if (position.Y < SuperGame.screenHeight / 2) { position.Y += deltaTime * velocity *1.2f; }
                else if (position.Y > SuperGame.screenHeight / 2) { position.Y -= deltaTime * velocity * 1.2f; }

                if (position.X < (SuperGame.screenWidth * 4 / 6)) { position.X += deltaTime * velocity * 1.2f; }
                else if (position.X > (SuperGame.screenWidth * 4 / 6)) { position.X -= deltaTime * velocity * 1.2f; }

               if (position.X == this.frameWidth / 2 && position.Y == this.frameHeight / 2) { movingToCenter = false; }
            }

            //Front shot
            if (timeToShotFrontAux <= 0)
            {
                ShotFront();
                //timeToShotFrontAux = 0.35f;
            }
            timeToShotFrontAux -= deltaTime;

            //Wings shot
            if (timeToShotWingsAux <= 0)
            {
                ShotWings();
               // timeToShotWingsAux = 0.35f;
            }
            timeToShotWingsAux -= deltaTime;

            secondsBot +=  deltaTime;
            if (secondsBot > 7)
            {
                Vector2 positionEnemy = new Vector2(position.X , position.Y  + this.frameHeight / 2); ///(470, 100);
                Enemy e = new BotFinalBoss(camera, level, positionEnemy, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots,true);
                e.SetActive();
                enemies.Add(e);

                Vector2 positionEnemy1 = new Vector2(position.X, position.Y + this.frameHeight / 2); //(470, 300);
                Enemy e1 = new BotFinalBoss(camera, level, positionEnemy1, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots,false);
                e1.SetActive();
                enemies.Add(e1);

                Vector2 positionEnemy2 = new Vector2(position.X , position.Y  - this.frameHeight / 2);//(470, 500);
                Enemy e2 = new BotFinalBoss(camera, level, positionEnemy2, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots,true);
                e2.SetActive();
                enemies.Add(e2);

                Vector2 positionEnemy3 = new Vector2(position.X , position.Y  - this.frameHeight / 2);
                Enemy e3 = new BotFinalBoss(camera, level, positionEnemy3, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots,false);
                e3.SetActive();
                enemies.Add(e3);

                secondsBot = 0;
            }

            if (life < 15000) phase = 3;
        }


        private void phase3(float deltaTime)
        {

            
            if (movingToBack)
            {

               

                if (position.X + this.frameWidth/2 < SuperGame.screenWidth) { position.X += deltaTime * velocity*1.2f; }
                else if (position.X + this.frameWidth/2 > SuperGame.screenWidth) { position.X -= deltaTime * velocity*1.2f; }

                if ((position.X + this.frameWidth/2 == SuperGame.screenWidth)) { movingToBack = false; }
            }

            if (position.Y < ship.position.Y) { position.Y += deltaTime * velocity; }
            else if (position.Y > ship.position.Y) { position.Y -= deltaTime * velocity; }

            //Front shot
            if (timeToShotFrontAux <= 0)
            {
                ShotFront();
                timeToShotFrontAux = 1f;
            }
            timeToShotFrontAux -= deltaTime;

            //Wings shot
            if (timeToShotWingsAux <= 0)
            {
                ShotWings();
                timeToShotWingsAux = 1f;
            }
            timeToShotWingsAux -= deltaTime;

            secondsBot += deltaTime;
            if (secondsBot > 10)
            {
                Vector2 positionEnemy = new Vector2(position.X, position.Y + this.frameHeight / 2); ///(470, 100);
                Enemy e = new BotFinalBoss(camera, level, positionEnemy, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots, true);
                e.SetActive();
                enemies.Add(e);

                Vector2 positionEnemy1 = new Vector2(position.X, position.Y + this.frameHeight / 2); //(470, 300);
                Enemy e1 = new BotFinalBoss(camera, level, positionEnemy1, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots, false);
                e1.SetActive();
                enemies.Add(e1);

                Vector2 positionEnemy2 = new Vector2(position.X, position.Y - this.frameHeight / 2);//(470, 500);
                Enemy e2 = new BotFinalBoss(camera, level, positionEnemy2, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots, true);
                e2.SetActive();
                enemies.Add(e2);

                Vector2 positionEnemy3 = new Vector2(position.X, position.Y - this.frameHeight / 2);
                Enemy e3 = new BotFinalBoss(camera, level, positionEnemy3, (float)Math.PI, GRMng.frameWidthBFB, GRMng.frameHeightBFB,
                    GRMng.numAnimsBFB, GRMng.frameCountBFB, GRMng.loopingBFB, SuperGame.frameTime12, GRMng.textureBFB, 50, 1, 1, ship, shots, false);
                e3.SetActive();
                enemies.Add(e3);

                secondsBot = 0;
            }

            secondsHeroe += deltaTime;
            if (secondsHeroe > 12)
            {
                Vector2 positionEnemy4 = new Vector2(position.X, position.Y + this.frameHeight / 2);
                Enemy e4 = new EnemyFinalHeroe2(camera, level, positionEnemy4, (float)Math.PI, GRMng.frameWidthHeroe1, GRMng.frameHeightHeroe1,
                    GRMng.numAnimsHeroe1, GRMng.frameCountHeroe1, GRMng.loopingHeroe1, SuperGame.frameTime12, GRMng.textureHeroe1, 0, 400, 100, 1, ship, true);
                e4.SetActive();
                enemies.Add(e4);

                Vector2 positionEnemy5 = new Vector2(position.X, position.Y + this.frameHeight / 2);
                Enemy e5 = new EnemyFinalHeroe2(camera, level, positionEnemy5, (float)Math.PI, GRMng.frameWidthHeroe1, GRMng.frameHeightHeroe1,
                    GRMng.numAnimsHeroe1, GRMng.frameCountHeroe1, GRMng.loopingHeroe1, SuperGame.frameTime12, GRMng.textureHeroe1, 0, 400, 100, 1, ship, false);
                e5.SetActive();
                enemies.Add(e5);

                Vector2 positionEnemy6 = new Vector2(position.X, position.Y - this.frameHeight / 2);
                Enemy e6 = new EnemyFinalHeroe2(camera, level, positionEnemy6, (float)Math.PI, GRMng.frameWidthHeroe1, GRMng.frameHeightHeroe1,
                    GRMng.numAnimsHeroe1, GRMng.frameCountHeroe1, GRMng.loopingHeroe1, SuperGame.frameTime12, GRMng.textureHeroe1, 0, 400, 100, 1, ship, false);
                e6.SetActive();
                enemies.Add(e6);

                Vector2 positionEnemy7 = new Vector2(position.X, position.Y - this.frameHeight / 2);
                Enemy e7 = new EnemyFinalHeroe2(camera, level, positionEnemy7, (float)Math.PI, GRMng.frameWidthHeroe1, GRMng.frameHeightHeroe1,
                    GRMng.numAnimsHeroe1, GRMng.frameCountHeroe1, GRMng.loopingHeroe1, SuperGame.frameTime12, GRMng.textureHeroe1, 0, 400, 100, 1, ship, true);
                e7.SetActive();
                enemies.Add(e7);

                secondsHeroe = 0;
             }


            timeToShot -= deltaTime;
            if (timeToShot <= 0)
            {

                if (timeShoting > 0)
                {
                    shooting = true;
                    LaserShot();
                    shot.Update(deltaTime);
                    timeShoting -= deltaTime;
                    //More speed if it's shooting
                    if (singletonLaserVelocityBoss) {velocity = velocity * 3f; singletonLaserVelocityBoss = !singletonLaserVelocityBoss;}

                }
                else
                {
                    shooting = false;
                    timeShoting = 0.5f;
                    timeToShot = 3.0f;
                    velocity = velocity / 1.05f;
                    //When the laser do not shoot , the velocity get the before value
                    if (!singletonLaserVelocityBoss) { velocity = velocity / 3f; singletonLaserVelocityBoss = !singletonLaserVelocityBoss; }
                }
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
            Shot nuevo = new Shot(camera, level, new Vector2(position.X , position.Y  + this.frameHeight / 2), angleDown, GRMng.frameWidthShotFinalBoss1, 
                GRMng.frameHeightShotFinalBoss1, GRMng.numAnimsShotFinalBoss1, GRMng.frameCountShotFinalBoss1, GRMng.loopingShotFinalBoss1, SuperGame.frameTime8, 
                GRMng.textureShotFinalBoss1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);

            float radius = (float)(Math.Sqrt(GRMng.frameHeightShotFinalBoss1/2 * GRMng.frameHeightShotFinalBoss1/2 +
                GRMng.frameWidthShotFinalBoss1/2 * GRMng.frameWidthShotFinalBoss1/2));

            //I'm making a copy because collider midifies the points
            Vector2[] pointsCollider = new Vector2[GRMng.colliderShotFinalBoss1.Length];
            for (int i = 0; i < GRMng.colliderShotFinalBoss1.Length; i++)
                pointsCollider[i] = GRMng.colliderShotFinalBoss1[i];

            nuevo.setCollider(pointsCollider, radius);

            shots.Add(nuevo);
           
           
            //position.X - 10, position.Y - collider.Height / 2
            nuevo = new Shot(camera, level, new Vector2(position.X , position.Y - this.frameHeight / 2), angleUp, GRMng.frameWidthShotFinalBoss1, 
                GRMng.frameHeightShotFinalBoss1, GRMng.numAnimsShotFinalBoss1, GRMng.frameCountShotFinalBoss1, GRMng.loopingShotFinalBoss1, SuperGame.frameTime8, 
                GRMng.textureShotFinalBoss1, SuperGame.shootType.normal, shotWingsVelocity, shotWingsPower);
            shots.Add(nuevo);

            radius = (float)(Math.Sqrt(GRMng.frameHeightShotFinalBoss1 / 2 * GRMng.frameHeightShotFinalBoss1 / 2 +
               GRMng.frameWidthShotFinalBoss1 / 2 * GRMng.frameWidthShotFinalBoss1 / 2));

            //I'm making a copy because collider midifies the points
            pointsCollider = new Vector2[GRMng.colliderShotFinalBoss1.Length];
            for (int i = 0; i < GRMng.colliderShotFinalBoss1.Length; i++)
                pointsCollider[i] = GRMng.colliderShotFinalBoss1[i];

            nuevo.setCollider(pointsCollider, radius);

            timeToShotWingsAux = timeToShotWings;
        }

        private void colliderPoints()
        {
            Vector2[] points = new Vector2[4];            
            points[0] = new Vector2(60, 20);
            points[1] = new Vector2(238, 112);
            points[2] = new Vector2(238, 126);
            points[3] = new Vector2(60, 220);

            float radius = (float)Math.Sqrt(frameWidth / 2 * frameWidth / 2 + frameHeight / 2 * frameHeight / 2);
            collider = new Collider(camera, true, position, rotation, points, radius, frameWidth, frameHeight);
        }
 		private void LaserShot()
        {
            //The calculation of the rectangle
            // rotationMatrix = Matrix.CreateRotationZ(rotation);
            //  int width = level.width + 800;

            //p1 = Vector2.Transform(p1Orig, rotationMatrix);
            /*  p1 = p1Orig;
              p1 += position;

             //p2 = Vector2.Transform(p2Orig, rotationMatrix);
              p2 = p2Orig;
              p2 += position;

              rect.X = (int)position.X;
              rect.Y = (int)position.Y;*/

            // shot.position = position;

            //setAnim(2);

            Vector2 positionShot = new Vector2(position.X - 1040, position.Y);

     /*       shot = new Shot(camera, level, positionShot, rotation, GRMng.frameWidthLaserBoss, GRMng.frameHeightLaserBoss,
                GRMng.numAnimsLaserBoss, GRMng.frameCountLaserBoss, GRMng.loopingLaserBoss, SuperGame.frameTime8,
                GRMng.textureLaserBoss, SuperGame.shootType.normal, shotVelocity, shotPower);*/

            shot = new Shot(camera, level, positionShot, rotation, GRMng.frameWidthELBulletHeroe, GRMng.frameHeightELBulletHeroe,
                GRMng.numAnimsELBulletHeroe, GRMng.frameCountELBulletHeroe, GRMng.loopingELBulletHeroe, SuperGame.frameTime8,
                GRMng.textureELBulletHeroe, SuperGame.shootType.normal, shotVelocity, shotPower);
            
            //   shot.rotation = rotation;

        }

    } // Class FinalBoss1
}
