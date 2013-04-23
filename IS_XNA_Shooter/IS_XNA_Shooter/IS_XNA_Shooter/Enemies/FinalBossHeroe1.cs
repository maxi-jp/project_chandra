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

        private int duplicates;

        private bool begin = true;

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
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight,
                numAnim, frameCount, looping, frametime,
                texture, 0, 1000, 1, 10, ship)
        {
            this.position = position;
            destiny = new Vector2();
            newPosition();
            currentState = State.move;
            totalTime = 0;
            this.shots = new List<Shot>();
            setAnim(1);

            //I'm making a copy because collider midifies the points
            Vector2[] pointsCollider = new Vector2[GRMng.colliderHeroe1.Length];
            for (int i = 0; i < GRMng.colliderHeroe1.Length; i++)
                pointsCollider[i] = GRMng.colliderHeroe1[i];

            float radius = (float)Math.Sqrt(frameHeight/2 * frameHeight/2 + frameWidth/2 * frameWidth/2);

            collider = new Collider(camera, true, position, rotation, pointsCollider, radius, frameWidth, frameHeight);

            active = true;
            colisionable = true;

            duplicates = 0;

            begin = true;
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
                        if (begin)
                        {
                            currentState = State.duplicate;
                            begin = false;
                        }
                        else
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


        public void SetEnemies(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        //-----------------------------------------------------------------------------------------------------------------

        private void chooseModeAttack()
        {
            Random random = new Random();
            if (duplicates < 1)
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

                float radius = (float)Math.Sqrt(frameWidth/2 * frameWidth/2 + frameHeight/2 * frameHeight/2);

                //I'm making a copy because collider midifies the points
                Vector2[] pointsCollider = new Vector2[GRMng.colliderShotFinalBossHeroe.Length];
                for (int i = 0; i < GRMng.colliderShotFinalBossHeroe.Length; i++)
                    pointsCollider[i] = GRMng.colliderShotFinalBossHeroe[i];

                shot.setCollider(pointsCollider, radius); //hacer copia

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
                Enemy enemy = new FinalBoss1Turret1(camera, level, position, ship);
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
            duplicates += 1;
            Enemy enemy = EnemyFactory.GetEnemyByName("FinalBossHeroe1", camera, level, ship, position, 0, null);
            ((FinalBossHeroe1)enemy).SetEnemies(enemies);
            enemies.Add(enemy);
        }
    }
}
