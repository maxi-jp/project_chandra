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
        protected int value;        // puntos que da al matarlo

        // control variables:
        protected bool active;      // si no esta activo no se muestra
        protected bool colisionable;// indicates if the Enemy is colisionable or not
        protected bool erasable;    // indicates if the Enemy is no longer necesary in the Game
        
        /* ------------------- CONSTRUCTORS ------------------- */
        public Enemy(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            this.velocity = velocity;
            this.timeToSpawn = timeToSpawn;
            this.life = life;
            this.value = value;
            this.ship = ship;
            active = false;
            colisionable = false;
            erasable = false;

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

    } // class Enemy
}
