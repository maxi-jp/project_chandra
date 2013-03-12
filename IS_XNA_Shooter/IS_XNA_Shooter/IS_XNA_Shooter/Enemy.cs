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
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (DeadCondition())
                erasable = true;

            if (colisionable && collider != null)
                collider.Update(position, rotation);

            if (outOfScreen())
                ForceKill();

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

            if (SuperGame.debug && colisionable && collider != null)
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

        // this method is called when the enemy its destroyed
        // it has to play its death animation and its shots must
        // be erase from the game. eg when the player loses a life
        public virtual void Kill()
        {
            life = 0;
            Damage(0);
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

        // this method is called when we want to force the enemy
        // to be erase from the game, eg when it get lost in the space
        public void ForceKill()
        {
            life = -1;
            active = false;
            colisionable = false;
            erasable = true;
        }

        public virtual bool DeadCondition()
        {
            return (!animActive);
        }

    } // class Enemy
}
