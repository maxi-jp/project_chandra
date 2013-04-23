using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    
    /// <summary>
    /// Enemy parent class 
    /// </summary>
    abstract class Enemy : Animation
    {
        /// <summary>
        /// Enemy's collider
        /// </summary>
        public Collider collider;
        
        /// <summary>
        /// player's ship
        /// </summary>
        protected Ship ship;     

        /// <summary>
        /// indicates the time when the Enemy has to turn active
        /// </summary>
        protected float timeToSpawn; 

       
        // Enemy stats:
        
        /// <summary>
        /// Enemy's life
        /// </summary>
        protected int life;

        /// <summary>
        /// Enemy's velocity
        /// </summary>
        protected float velocity;

        /// <summary>
        /// the points you obtain if you kill it
        /// </summary>
        protected int value;

        
        // control variables:
       
        /// <summary>
        /// if it isn't active you cant see it
        /// </summary>
        protected bool active;
       
        /// <summary>
        /// indicates if the Enemy is colisionable or not
        /// </summary>
        protected bool colisionable;
        
        /// <summary>
        /// indicates if the Enemy is no longer necesary in the Game
        /// </summary>
        protected bool erasable;
        
       
        /// <summary>
        /// Enemy's constructor
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the enemy</param>
        /// <param name="rotation">The rotation of the enemy</param>
        /// <param name="frameWidth">The width of each frame of the enemy's animation</param>
        /// <param name="frameHeight">The height of each frame of the enemy's animation </param>
        /// <param name="numAnim">The number of the enemy's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the enemy</param>
        /// <param name="timeToSpawn">The time that the enemy has to wait for appear in the game</param>
        /// <param name="velocity">The velocity of the enemy</param>
        /// <param name="life">The life of the enemy</param>
        /// <param name="value">The points you obtain if you kill it</param>
        /// <param name="ship">The player's ship</param>
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

        /// <summary>
        /// Updates the logic of the enemy
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
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

        /// <summary>
        /// Discounts the enemy's time to appear
        /// </summary>
        /// <param name="deltaTime">The time since the last UpdateTimeToSpawn</param>
        public void UpdateTimeToSpawn(float deltaTime)
        {
            timeToSpawn -= deltaTime;
            if (timeToSpawn <= 0)
                SetActive();
        }

        /// <summary>
        /// Draws the enemy 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (SuperGame.debug && colisionable)
                collider.Draw(spriteBatch);
        }

        /// <summary>
        /// Controlls if the enemy is out of the screen.
        /// </summary>
        /// <returns> True if it is out of the screen and False if it isn't </returns>
        private bool outOfScreen()
        {   
            return (position.X > level.width || position.X < 0 || position.Y > level.height || position.Y < 0);
               
        }

        /// <summary>
        /// Controlls if the enemy is dead
        /// </summary>
        /// <returns>True if it's dead and False if it isn't</returns>
        public bool isDead()
        {
            return (life <= 0);
        }

        /// <summary>
        /// Causes damage to the enemy
        /// </summary>
        /// <param name="i">The amount of damage that the enemy receives</param>
        public virtual void Damage(int i)
        {
            if (i == -1)
                life = 0;
            else
                life -= i;

            if (life <= 0)
                colisionable = false;
        }

         
        /// <summary>
        /// This method is called when the enemy its destroyed
        /// it has to play its death animation and its shots must
        /// be erase from the game. eg when the player loses a life
        /// </summary>
        public virtual void Kill()
        {
            life = 0;
            Damage(0);
        }

        /// <summary>
        /// Says if the enemy is active
        /// </summary>
        /// <returns>True if it's active and False if it in't</returns>
        public bool IsActive()
        {
            return active;
        }

        /// <summary>
        /// Puts active and colisionable True or False
        /// </summary>
        /// <param name="aux">The parameter to introduce in the parameters</param>
        public void SetActive(bool aux)
        {
            active = aux;
            colisionable = aux;
        }

        /// <summary>
        /// Actives the enemy 
        /// </summary>
        public void SetActive()
        {
            active = true;
            colisionable = true;
        }

        /// <summary>
        /// Desactivate the eemy
        /// </summary>
        public void UnsetActive()
        {
            active = false;
        }

        /// <summary>
        /// Says if the enemy is colisionable
        /// </summary>
        /// <returns>Colisionable's atribute of the enemy </returns>
        public bool IsColisionable()
        {
            return colisionable;
        }

        /// <summary>
        /// Says if the enemy is erasable
        /// </summary>
        /// <returns>Erasable's atribute of the enemy</returns>
        public bool IsErasable()
        {
            return erasable;
        }

        /// <summary>
        /// Sets the player´s ship to one specific
        /// </summary>
        /// <param name="ship">The specific ship</param>
        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }

        /// <summary>
        /// This method is called when we want to force the enemy
        /// to be erase from the game, eg when it get lost in the space
        /// </summary>
        public void ForceKill()
        {
            life = -1;
            active = false;
            colisionable = false;
            erasable = true;
        }

        /// <summary>
        /// Evaluates the enemy's condition to dissapear of the game
        /// </summary>
        /// <returns>True if the enemy is ready to dissapear and False if it isn't</returns>
        public virtual bool DeadCondition()
        {
            return (!animActive);
        }

        /// <summary>
        /// Returns the Enemy's life
        /// </summary>
        /// <returns>The life of the enemy</returns>
        public int GetLife()
        {
            return this.life;
        }

    } // class Enemy
}
