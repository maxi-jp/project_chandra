using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace IS_XNA_Shooter
{
    //Clase para el tipo enemigo mina que lanza 6 bolas
    class EnemyMineShot : Enemy
    {
         /* ------------------- ATTRIBUTES ------------------- */
        private float timeToMove = 3.0f;
        private float despX = 1.0f;
        private float despY = -1.0f;

        private List<Shot> shots;
        private float timeToShot = 4.0f;
        private float shotVelocity = 140f;
        private int shotPower = 200;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyMineShot(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, ship)
        {
            setAnim(0);

            Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(39, 14);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(39, 66);
            points[5] = new Vector2(20, 60);
            collider = new Collider(camera, true, position, rotation, points, 35, frameWidth, frameHeight);

            this.shots = new List<Shot>();
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
                //Cambia de dirección si toca
                timeToMove -= deltaTime;
                if (timeToMove <= 0)
                {
                    Random random = new Random();
                    despX = random.Next(-5, 5);
                    despY = random.Next(-5, 5);

                    position.X += (float)(velocity * despX * deltaTime);
                    position.Y += (float)(velocity * despY * deltaTime);

                    timeToMove = 3.0f;

                }
                else
                {
                    position.X += (float)(velocity * despX * deltaTime);
                    position.Y += (float)(velocity * despY * deltaTime);
                }

                //Dispara si toca
                timeToShot -= deltaTime;
                if (timeToShot <= 0)
                {
                    SixShots();
                    timeToShot = 4.0f;
                }

            } // if (life > 0)
            
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

            // comprobamos que el enemigo no se salga del nivel
            position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Shot shot in shots)
                shot.Draw(spriteBatch);
        }

        //Dispara 6 tiros desde el centro de la nave al exterior en 6 direcciones diferentes
        private void SixShots()
        {
            setAnim(1);

            Vector2 pos = new Vector2(position.X + 25, position.Y + 25);
            float rot = 0.75f;
            //Bottom, right
            Shot s1 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Bottom, center
            pos.Y = position.Y + 33;
            pos.X = position.X;
            rot = 1.55f;
            Shot s2 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Bottom, left
            pos.Y = position.Y + 25;
            pos.X = position.X - 25;
            rot = 2.33f;
            Shot s3 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, left
            pos.Y = position.Y - 25;
            pos.X = position.X - 25;
            rot = 3.92f;
            Shot s4 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, center
            pos.Y = position.Y - 33;
            pos.X = position.X;
            rot = -1.55f;
            Shot s5 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            //Top, right
            pos.Y = position.Y - 25;
            pos.X = position.X + 25;
            rot = -0.75f;
            Shot s6 = new Shot(camera, level, pos, rot, GRMng.frameWidthEMSBullet, GRMng.frameHeightEMSBullet,
               GRMng.numAnimsEMSBullet, GRMng.frameCountEMSBullet, GRMng.loopingEMSBullet, SuperGame.frameTime8,
               GRMng.textureEMSBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(s1);
            shots.Add(s2);
            shots.Add(s3);
            shots.Add(s4);
            shots.Add(s5);
            shots.Add(s6);

        } // SixShots

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                setAnim(2, -1);
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

    }//Class EnemyMineShot
}
