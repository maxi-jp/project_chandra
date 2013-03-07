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
            float frametime, Texture2D texture, float timeToSpawn, float velocity, Player player,
            int shotPower, float shotVelocity, float timeToShot, int life, int value, Ship Ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, Ship)
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

            // comprobamos que el player no se salga del nivel
            position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);
        }

    }//Class EnemyMineShot
}
