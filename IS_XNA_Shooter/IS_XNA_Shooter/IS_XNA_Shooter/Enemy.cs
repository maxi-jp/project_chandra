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
        /* ------------------- ATTRIBUTES ------------------- */
        public Collider collider;
        protected Ship ship;        // player ship

        protected float timeToSpawn;    // indicates the time when the Enemy has to turn active

        // Enemy stats:
        protected int life;         // vida
        protected float velocity;   // velocidad
        
        //Shots
        protected List<Shot> shots;
        protected float shotVelocity = 500f;
        protected int shotPower = 200;

        protected float timeToShot = 1.7f; // tiempo minimo entre disparos en segundos
        

        // control variables:
        protected bool active;      // si no esta activo no se muestra
        protected bool colisionable;// indicates if the Enemy is colisionable or not
        protected bool erasable;    // indicates if the Enemy is no longer necesary in the Game
        
        /* ------------------- CONSTRUCTORS ------------------- */
        public Enemy(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, int shotPower, float shotVelocity, float timeToShot, Ship ship)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            this.velocity = velocity;
            this.timeToSpawn = timeToSpawn;
            this.life = life;
            this.value = value;
            this.ship = ship;
            this.shotPower = shotPower;
            this.shotVelocity = shotVelocity;
            this.timeToShot = timeToShot;
            
            this.shots = new List<Shot>();
            
            active = false;
            colisionable = false;
            erasable = false;
           
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!animActive)
                erasable = true;

            if (colisionable)
                collider.Update(position, rotation);

            if (outOfScreen())
                Kill();
        } // Update

        public void UpdateTimeToSpawn(float deltaTime)
        {
            timeToSpawn -= deltaTime;
            if (timeToSpawn <= 0)
                SetActive();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (SuperGame.debug && colisionable)
                collider.Draw(spriteBatch);
        }

        private bool outOfScreen()
        {   //if the enemy is out the screen it is killed
            return (position.X > level.width || position.X < 0 || position.Y > level.height || position.Y < 0);
               
        }

        public bool isDead()
        {
            return (life <= 0);
        }

        public virtual void Damage(int i)
        {
            life -= i;

            if (life <= 0)
                colisionable = false;
        }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool aux)
        {
            active = aux;
            colisionable = aux;
        }

        public void SetActive()
        {
            active = true;
            colisionable = true;
        }

        public void UnsetActive()
        {
            active = false;
        }

        public bool IsColisionable()
        {
            return colisionable;
        }

        public bool IsErasable()
        {
            return erasable;
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }

        public void Kill()
        {
            life = -1;
            active = false;
            colisionable = false;
            erasable = true;
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
