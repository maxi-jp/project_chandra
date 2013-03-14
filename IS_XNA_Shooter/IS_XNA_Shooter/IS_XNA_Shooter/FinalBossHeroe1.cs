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
            shoot,
            turret,
            duplicate
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

        private List<Enemy> enemies;

        private bool isDuplicate;

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
        public FinalBossHeroe1(Camera camera, Level level, Vector2 position, Ship Ship, List<Enemy> enemies, List<Shot> shots)
            : base(camera, level, position, 0, GRMng.frameWidthHeroe1, GRMng.frameHeightHeroe1,
            GRMng.numAnimsHeroe1, GRMng.frameCountHeroe1, GRMng.loopingHeroe1, SuperGame.frameTime12, 
            GRMng.textureHeroe1, 0, 1000, 1, 10, Ship)
        {
            this.position = position;
            destiny = new Vector2();
            newPosition();
            currentState = State.move;
            totalTime = 0;
            this.shots = shots;
            this.enemies = enemies;
            setAnim(1);

            //I'm making a copy because collider midifies the points
            Vector2[] pointsCollider = new Vector2[GRMng.colliderHeroe1.Length];
            for (int i = 0; i < GRMng.colliderHeroe1.Length; i++)
                pointsCollider[i] = GRMng.colliderHeroe1[i];

            collider = new Collider(camera, true, position, rotation, pointsCollider, frameWidth, frameHeight);

            active = true;
            colisionable = true;

            isDuplicate = false;
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Overwrite the method Damage. When it dies, the animation changes.
        /// </summary>
        /// <param name="i"></param>
        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                velocity = 0;
                setAnim(2, -1);
                Audio.PlayEffect("brokenBone01");
            }
        }

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
                case State.shoot :
                    shoot(deltaTime);
                    if (totalTime > 1)
                    {
                        currentState = State.move;
                        newPosition();
                    }
                    break;

                case State.move :
                    move(deltaTime);
                    if (Math.Abs(destiny.X - position.X) < 10 && Math.Abs(destiny.Y - position.Y) < 10)
                    {
                        chooseModeAttack();
                        totalTime = 0;
                    }
                    break;

                case State.turret :
                    currentState = State.move;
                    buildTurret();
                    newPosition();
                    break;

                case State.duplicate :
                    currentState = State.move;
                    newPosition();
                    duplicate();
                    break;

                default :                    
                    break;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        private void chooseModeAttack()
        {
            Random random = new Random();
            if (!isDuplicate)
            {
                int numState = random.Next(0, 3);
                if (numState == 0)
                    currentState = State.shoot;
                else if (numState == 1)
                    currentState = State.turret;
                else if (numState == 2)
                    currentState = State.duplicate;
            }
            else
            {
                int numState = random.Next(0, 2);
                if (numState == 0)
                    currentState = State.shoot;
                else if (numState == 1)
                    currentState = State.turret;
            }
        }

        private void newPosition()
        {
            Random random = new Random();
            destiny.X = random.Next(frameWidth, level.width - frameHeight);
            destiny.Y = random.Next(frameHeight, level.height - frameHeight);
        }

        /// <summary>
        /// The enemy looks to our ship and attack us
        /// </summary>
        /// <param name="deltaTime"></param>
        private void shoot(float deltaTime)
        {
            shotLastTime += deltaTime;

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

            if (shotLastTime >= 0.2f && !isDead())
            {
                setAnim(0);

                shotLastTime = 0;

                Vector2 shotPos = new Vector2(pX, pY);
                Shot shot = new Shot(camera, level, shotPos, rotation, GRMng.frameWidthShotFinalBossHeroe, GRMng.frameHeightShotFinalBossHeroe, 
                    GRMng.numAnimsShotFinalBossHeroe, GRMng.frameCountShotFinalBossHeroe, GRMng.loopingShotFinalBossHeroe, SuperGame.frameTime8, GRMng.textureShotFinalBossHeroe, 
                    SuperGame.shootType.normal, 500, 100);

                shot.setCollider(GRMng.colliderShotFinalBossHeroe);
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

        private void buildTurret()
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
            {
                Enemy enemy = new FinalBoss1Turret1(camera, level, position, ship, shots);
                enemies.Add(enemy);
            }
            else
            {
                Enemy enemy = new FinalBoss1Turret2(camera, level, position, ship);
                enemies.Add(enemy);
            }
        }

        private void duplicate()
        {
            isDuplicate = true;
            Enemy enemy = new FinalBossHeroe1(camera, level, position, ship, enemies, shots);
            enemies.Add(enemy);
        }
    }
}
