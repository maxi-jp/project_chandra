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
        Vector2 p1;
        Vector2 p2;
        Vector2 p1Orig;
        Vector2 p2Orig;
        Shot shot;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyLaserA(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Ship player,
            int shotPower, float shotVelocity, float timeToShot)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, 50, 100, player, 1, 80f, 3.0f)
        {
            setAnim(3);

            Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(40, 13);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(40, 65);
            points[5] = new Vector2(20, 60);


            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);

            //For the Laser
            Rectangle rect = new Rectangle(0, 0, 2000, 2);
            p1 = new Vector2();
            p2 = new Vector2();
            p1Orig = new Vector2(0, 0);
            p2Orig = new Vector2(320, 0);
            shot = new Shot(camera, level, p1, rotation, GRMng.frameWidthELBullet, GRMng.frameHeightELBullet,
                        GRMng.numAnimsELBullet, GRMng.frameCountELBullet, GRMng.loopingELBullet, SuperGame.frameTime8,
                        GRMng.textureELBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(shot);

        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            
                  
            //It rotates always
            rotation +=0.4f * deltaTime;

            
            timeToShot -= deltaTime;
            if (timeToShot <= 0)
            { 
                timeToShot = 3.0f;
                shooting = !shooting;
                if (shooting)
                    shots.Add(shot);
                else shots.Remove(shot);
            }

            //it shoots if it has to
            if (shooting)
                LaserShot();
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

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            shot.position = p2;
            shot.rotation = rotation;

            

        }

    }//Class EnemyMineShot
}
