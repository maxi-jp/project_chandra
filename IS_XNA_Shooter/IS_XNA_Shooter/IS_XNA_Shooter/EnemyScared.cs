using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase para el tipo de enemigo asustadizo
    class EnemyScared : Enemy
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private bool isRotating = false;
        private bool isFleeing = false;

        private float rotationPosition;
        private Matrix rotationMatrix; // matriz de rotación

        private List<Shot> shots;
        private float timeToShot = 4.0f;
        private float shotVelocity = 300f;
        private int shotPower = 10;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyScared(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life, int value,
            Ship ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, ship)
        {
            setAnim(3);

            Vector2[] points = new Vector2[5];
            points[0] = new Vector2(20, 0);
            points[1] = new Vector2(70, 40);
            points[2] = new Vector2(20, 80);
            points[3] = new Vector2(10, 71);
            points[4] = new Vector2(10, 9);

            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);

            shots = new List<Shot>();
        }
        
        /* ------------------- METHODS ------------------- */
        /*
        Si la nave del jugador no nos mira en un rango de +- PI/8, avanzamos hacia ella y disparamos.      
        Si no, cambiamos nuestro color a rojo, nos giramos, y huimos.
        */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
                float dY = -ship.position.Y + position.Y;
                float dX = -ship.position.X + position.X;

                float gyre = (float)Math.Atan(dY / dX);

                if (isRotating)
                {
                    //Si ha llegado a la posición final del giro, deja de rotar
                    if ((rotation < (rotationPosition + ((float)Math.PI) / 12)) && (rotation > rotationPosition - ((float)Math.PI) / 12))
                    {//----todavía no funciona la comprobación esta -----
                        isRotating = false;

                        //Cambia el color
                        if (isFleeing)
                            setAnim(4);
                        else
                            setAnim(3);

                    }
                    //Si no, sigue girando
                    else
                    {
                        rotation += 10 * deltaTime;
                        rotation = rotation % (2 * (float)Math.PI);
                    }
                }
                //Si no está en fase de rotación
                else
                {
                    float r = ((float)Math.PI) / 8;
                    float h = (float)Math.Sqrt(dY * dY + dX * dX);
                    float ang = 0;
                    float rot = ship.rotation;

                    //Si estamos huyendo
                    if (isFleeing)
                    {

                        // comprobamos que el player no se salga del nivel
                        position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, level.width - collider.Width / 2);
                        position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, level.height - collider.Height / 2);

                        //1er cuadrante
                        if (dX < 0 && dY > 0)
                        {

                            ang = (float)Math.Abs(Math.Acos(dY / h));
                            ang += (float)Math.PI / 2;
                            //Si la nave del jugador no nos mira, empezamos a girar para perseguirle
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = false;
                                //Cambia el color
                                setAnim(2);
                            }

                        }
                        //2º cuadrante
                        else if (dX >= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            //Si la nave del jugador no nos mira, empezamos a girar para perseguirle
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Cambia el color
                                setAnim(2);
                            }
                        }
                        //3er cuadrante
                        else if (dX > 0 && dY < 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang = -ang;
                            //Si la nave del jugador no nos mira, empezamos a girar para perseguirle
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Cambia el color
                                setAnim(2);
                            }
                        }
                        //4º cuadrante
                        else if (dX <= 0 && dY <= 0)
                        {

                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang += (float)Math.PI;

                            if (ship.rotation < 0)
                                rot += 2 * (float)Math.PI;

                            //Si la nave del jugador no nos mira, empezamos a girar para perseguirle
                            if (!(ang < rot + r && ang > rot - r))
                            {
                                if (dX >= 0)
                                    rotationPosition = (float)Math.PI + gyre;
                                else
                                    rotationPosition = gyre;

                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;

                                isRotating = true;
                                isFleeing = false;
                                //Cambia el color
                                setAnim(2);
                            }
                        }

                        //si no, seguimos huyendo
                        if (ang < rot + r && ang > rot - r)
                        {

                            Flee(dX, gyre, deltaTime);
                        }

                    }
                    //Si no estamos huyendo
                    else
                    {
                        //Dispara si toca
                        timeToShot -= deltaTime;
                        if (timeToShot <= 0)
                        {
                            TwoShots();
                            timeToShot = 1.7f;
                        }
                        //1er cuadrante
                        if (dX <= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Acos(dY / h));
                            ang += (float)Math.PI / 2;

                            //Si la nave del jugador nos mira, empezamos a girar para huir
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Cambia el color
                                setAnim(2);
                            }
                        }
                        //2º cuadrante
                        else if (dX >= 0 && dY >= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));

                            //Si la nave del jugador nos mira, empezamos a girar para huir
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {

                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Cambia el color
                                setAnim(2);
                            }
                        }
                        //3er cuadrante
                        else if (dX >= 0 && dY <= 0)
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang = -ang;

                            //Si la nave del jugador nos mira, empezamos a girar para huir
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Cambia el color
                                setAnim(2);
                            }
                        }
                        //4º cuadrante if (dX <= 0 && dY <= 0)
                        else
                        {
                            ang = (float)Math.Abs(Math.Asin(dY / h));
                            ang += (float)Math.PI;

                            //Si la nave del jugador nos mira, empezamos a girar para huir
                            if (ang < ship.rotation + r && ang > ship.rotation - r)
                            {
                                rotationPosition = ship.rotation;
                                if (rotationPosition < 0)
                                    rotationPosition += 2 * (float)Math.PI;
                                isRotating = true;
                                isFleeing = true;
                                //Cambia el color
                                setAnim(2);
                            }
                        }

                        if (!(ang < ship.rotation + r && ang > ship.rotation - r))
                        {
                            Chase(dX, gyre, deltaTime);
                        }
                    }
                }
            } // if (isRotating)

            // shots:
            for (int i = 0; i < shots.Count(); i++)
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
            }
                       
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Shot shot in shots)
                shot.Draw(spriteBatch);
        }

        private void TwoShots() 
        {
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            Vector2 p1Orig = new Vector2(20, 13);
            Vector2 p2Orig = new Vector2(20, -13);

            Vector2 p1 = new Vector2();
            Vector2 p2 = new Vector2();

            p1 = Vector2.Transform(p1Orig, rotationMatrix);
            p1 += position;

            p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 += position;

            //base.EnemyShot(p1);
            //base.EnemyShot(p2);
            Shot nuevo = new Shot(camera, level, p1, rotation, GRMng.frameWidthESBullet,
                GRMng.frameHeightESBullet, GRMng.numAnimsESBullet, GRMng.frameCountESBullet,
                GRMng.loopingESBullet, SuperGame.frameTime8, GRMng.textureESBullet,
                SuperGame.shootType.normal, shotVelocity, shotPower);
            shots.Add(nuevo);
            nuevo = new Shot(camera, level, p2, rotation, GRMng.frameWidthESBullet,
                GRMng.frameHeightESBullet, GRMng.numAnimsESBullet, GRMng.frameCountESBullet,
                GRMng.loopingESBullet, SuperGame.frameTime8, GRMng.textureESBullet,
                SuperGame.shootType.normal, shotVelocity, shotPower);
            shots.Add(nuevo);
        }

        //Función para perseguir al jugador
        private void Chase(float dX, float gyre, float deltaTime)
        {

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

        //Funcion para huir del jugador
        private void Flee(float dX, float gyre, float deltaTime)
        {

            if (dX < 0)
            {

                rotation = (float)Math.PI + gyre;
                position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime * 2);
                position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime * 2);
            }
            else
            {
                rotation = gyre;
                position.X += (float)(velocity * Math.Cos(gyre) * deltaTime * 2);
                position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime * 2);
            }

        }

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                setAnim(5, -1);
                Audio.PlayEffect("brokenBone02");
            }
        }

        public override bool DeadCondition()
        {
            // the dead condition of this enemy is when its death animation has ended
            // and the shots shoted when it was alive are no longer active
            return (!animActive && (shots.Count() == 0));
        }

    } // class EnemyScared
}
