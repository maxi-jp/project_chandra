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
         /* ------------------- ATRIBUTOS ------------------- */
        float timeToMove = 3.0f;
        float despX = 1.0f;
        float despY = -1.0f;

        /* ------------------- CONSTRUCTORES ------------------- */
        public EnemyMineShot(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Player player,
            int shotPower, float shotVelocity, float timeToShot)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, player, shotPower, shotVelocity, timeToShot)
        {
            setAnim(1);

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            /*Vector2[] points = new Vector2[4];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(60, 35);
            points[2] = new Vector2(60, 45);
            points[3] = new Vector2(20, 60);*/
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            //Cambia de dirección si toca
            timeToMove -= deltaTime;
            if (timeToMove <= 0)
            {
                Random random = new Random();
                despX = random.Next(-5 , 5);
                despY = random.Next(-5 , 5);

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
            // comprobamos que el player no se salga del nivel
            position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);
        }

        //Dispara 6 tiros desde el centro de la nave al exterior en 6 direcciones diferentes
        private void SixShots()
        {

            Vector2 pos = new Vector2(position.X+30,position.Y+30);
            float rot = 0.75f;

            Shot s1 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            pos.Y = position.Y;
            rot = 0.0f;
            Shot s2 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            pos.Y = position.Y - 30;
            rot = -0.75f;
            Shot s3 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            pos.X = position.X - 30;
            pos.Y = position.Y + 30;
            rot = 2.33f;
            Shot s4 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            pos.Y = position.Y ;
            rot = 3.12f;
            Shot s5 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            pos.Y = position.Y - 30;
            rot = 3.92f;
            Shot s6 = new Shot(camera, level, pos, rot, GRMng.frameWidthL1, GRMng.frameHeightL1,
               GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
               GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(s1);
            shots.Add(s2);
            shots.Add(s3);
            shots.Add(s4);
            shots.Add(s5);
            shots.Add(s6);
        }

    }//Class EnemyMineShot
}
