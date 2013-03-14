using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace IS_XNA_Shooter
{
    //Class for the enemy that shoots one laser
    class EnemyLaserA : Enemy
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private Boolean shooting = true;
        private Matrix rotationMatrix;

        //For the Laser
        private Rectangle rect;
        private Vector2 p1, p2, p3;
        private Vector2 p1Orig, p2Orig, p3Orig;

        private float shotVelocity = 80f;
        private float timeToShot = 3.0f;
        private int shotPower = 1;
        private Shot shot;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyLaserA(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, 50, 100, value, ship)
        {
            setAnim(3);

            Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(40, 13);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(40, 65);
            points[5] = new Vector2(20, 60);
            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);

            //For the Laser
            Rectangle rect = new Rectangle(0, 0, 2000, 2);
            p1 = new Vector2();
            p2 = new Vector2();
            p3 = new Vector2();
            
            p1Orig = new Vector2(0, 0);
            p2Orig = new Vector2(320, 0);
            p3Orig = new Vector2(600, 0);

            shot = new Shot(camera, level, p1, rotation, GRMng.frameWidthELBulletA, GRMng.frameHeightELBullet,
                GRMng.numAnimsELBullet, GRMng.frameCountELBullet, GRMng.loopingELBullet, SuperGame.frameTime8,
                GRMng.textureELBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
                // it allways rotates
                rotation += 0.4f * deltaTime;

                timeToShot -= deltaTime;
                if (timeToShot <= 0)
                {
                    timeToShot = 3.0f;
                    shooting = !shooting;
                    /*if (shooting)
                        shots.Add(shot);
                    else shots.Remove(shot);*/
                }

                //it shoots if it has to
                if (shooting)
                {
                    LaserShot();
                    shot.Update(deltaTime);
                }
            }

            // shots:
            if (shooting)
            {
                shot.Update(deltaTime);
                //shot-player colisions
                if (ship.collider.CollisionTwoPoints(p1, p3))
                {
                    // the player is hitted:
                    ship.Damage(shot.GetPower());

                    // the shot must be erased only if it hasn't provoked the
                    // player ship death, otherwise the shot will had be removed
                    // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                    /*if (ship.GetLife() > 0)
                        shots.RemoveAt(i);*/
                }
            }

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shooting)
                shot.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        //The enemy Shoot a Laser, we calculate previously the rectangle of it
        private void LaserShot() 
        { 
            //The calculation of the rectangle
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            int width = level.width + 200;

            p1 = Vector2.Transform(p1Orig, rotationMatrix);
            p1 += position;

            p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 += position;

            p3 = Vector2.Transform(p3Orig, rotationMatrix);
            p3 += position;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            shot.position = p2;
            shot.rotation = rotation;

        }

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                shooting = false;
                setAnim(5, -1);
                Audio.PlayEffect("brokenBone02");
            }
        }

    } // Class EnemyMineShot
}
