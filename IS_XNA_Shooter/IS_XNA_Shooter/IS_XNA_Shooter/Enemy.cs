using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase padre enemigo de la que heredan todos los enemigos.
    abstract class Enemy : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */
        public Collider collider;

        protected int life;         // vida
        protected float velocity;   // velocidad
        protected Ship player;    // jugador
        private bool active;        // si no esta activo no se muestra
        
        //Shots
        protected List<Shot> shots;
        protected float shotVelocity = 500f;
        protected int shotPower = 200;

        protected float timeToShot = 1.7f; // tiempo minimo entre disparos en segundos
        

        /* ------------------- CONSTRUCTORES ------------------- */
        public Enemy(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Ship player, 
            int shotPower, float shotVelocity, float timeToShot)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            this.velocity = velocity;
            this.life = life;
            this.player = player;
            active = false;

            this.shotPower = shotPower;
            this.shotVelocity = shotVelocity;
            this.timeToShot = timeToShot;
            this.shots = new List<Shot>();

           
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            collider.Update(position, rotation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (SuperGame.debug)
                collider.Draw(spriteBatch);
        }

        public bool IsDead()
        {
            return (life <= 0);
        }

        public void Damage(int i)
        {
            life -= i;
        }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool aux)
        {
            active = aux;
        }

        public void SetActive()
        {
            active = true;
        }

        public void unsetActive()
        {
            active = false;
        }

        public void SetPlayer(Ship player)
        {
            this.player = player;
        }

        public void Kill()
        {
            life = -1;
        }

        public List<Shot> getShots()
        {
            return shots;
        }
        protected void EnemyShot(Vector2 position)
        {
            /*if (ControlMng.isControllerActive())
            {
                GamePad.SetVibration(PlayerIndex.One, 0.1f, 0.2f);
                timeVibShotAux = timeVibShot;
            }*/

            Shot nuevo = new Shot(camera, level, position, rotation, GRMng.frameWidthESBullet, GRMng.frameHeightESBullet,
                        GRMng.numAnimsESBullet, GRMng.frameCountESBullet, GRMng.loopingESBullet, SuperGame.frameTime8,
                        GRMng.textureESBullet, SuperGame.shootType.normal, shotVelocity, shotPower);

            shots.Add(nuevo);
            
        }

    } // class Enemy
}
