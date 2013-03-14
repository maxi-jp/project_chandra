using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase para el tipo de enemigo debil
    class EnemyWeakShot : Enemy
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private List<Shot> shots;
        private float timeToShot = 2.0f;
        private float timeToShotAux;
        private float shotVelocity = 300f;
        private int shotPower = 200;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyWeakShot(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, rotation,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime,
                texture, timeToSpawn, velocity, life, value, ship)
        {
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            collider = new Collider(camera, true, position, rotation, points, 35, frameWidth, frameHeight);

            setAnim(1);

            timeToShotAux = timeToShot;

            shots = new List<Shot>();
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
                float dY = -ship.position.Y + position.Y;
                float dX = -ship.position.X + position.X;

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

                timeToShotAux -= deltaTime;
                if (timeToShotAux <= 0)
                {
                    Shot shot = new Shot(camera, level, position, rotation, GRMng.frameWidthL2, GRMng.frameHeightL2,
                        GRMng.numAnimsL2, GRMng.frameCountL2, GRMng.loopingL2, SuperGame.frameTime12,
                        GRMng.textureL2, SuperGame.shootType.normal, shotVelocity, shotPower);
                    shots.Add(shot);
                    setAnim(2);

                    timeToShotAux = timeToShot;
                }
            } // if life > 0

            // shots:
            for (int i = 0; i < shots.Count(); i++)
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
                else  // shots-player colisions
                {
                    if (ship.collider.collision(shots[i].position))
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

            if (life <= 0)
            {
                setAnim(3, -1);
                Audio.PlayEffect("brokenBone01");
            }
        }

        public override void Kill()
        {
            base.Kill();

            shots.Clear();
        }

        public override bool DeadCondition()
        {
            // the dead condition of this enemy is when its death animation has ended
            // and the shots shoted when it was alive are no longer active
            return (!animActive && (shots.Count() == 0));
        }

    } // class EnemyWeak
}
