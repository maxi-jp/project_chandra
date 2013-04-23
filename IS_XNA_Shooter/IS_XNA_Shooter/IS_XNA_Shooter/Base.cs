using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class Base : Animation
    {
        /// <summary>
        /// Base's collider
        /// </summary>
        public Collider collider;

        /// <summary>
        /// Base's life
        /// </summary>
        protected int life;

        /// <summary>
        /// Base's initial life
        /// </summary>
        protected int initialLife;

        /// <summary>
        /// Base's life bar
        /// </summary>
        protected LifeBar lifeBar;

        // control variables:

        /// <summary>
        /// if it isn't active you cant see it
        /// </summary>
        protected bool active;

        /// <summary>
        /// indicates if the Base is colisionable or not
        /// </summary>
        protected bool colisionable;

        /// <summary>
        /// indicates if the Base is no longer necesary in the Game
        /// </summary>
        protected bool erasable;

        /// <summary>
        /// Constructor for house
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the Base</param>
        /// <param name="rotation">The rotation of the Base</param>
        /// <param name="frameWidth">The width of each frame of the Base's animation</param>
        /// <param name="frameHeight">The height of each frame of the Base's animation </param>
        /// <param name="numAnim">The number of the Base's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the Base</param>
        /// <param name="life">The life of the Base</param>
        public Base(Camera camera, Level level, Vector2 position, float rotation,
                    short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
                    float frametime, Texture2D texture, short frameLifeBarWidth, short frameLifeBarHeight, short numAnimLifeBar, short[] frameCountLifeBar, bool[] loopingLifeBar,
                    float frametimeLifeBar, Texture2D textureLifeBar, int life) 
                    : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                            frameCount, looping, frametime)
        {
            initialLife = life;
            this.life = life;
            this.lifeBar = new LifeBar(camera, level, new Vector2(position.X, position.Y -90), 0, frameLifeBarWidth, frameLifeBarHeight, numAnimLifeBar, frameCountLifeBar,
                 loopingLifeBar, frametimeLifeBar, textureLifeBar);
            setAnim(1);
            active = true;
            colisionable = true;
            erasable = false;
            Vector2[] points = new Vector2[4];
            points[0] = new Vector2(0, 0);
            points[1] = new Vector2(122, 0);
            points[2] = new Vector2(122, 128);
            points[3] = new Vector2(0, 128);

            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);
        }

        //---------------------------- Procedures -----------

        //Lowers the life of the house by the amount i
        public void Damage(int i)
        {
                life -= i;

                if (life <= 0)
                    Kill();
        }

        public void Kill()
        {
            setAnim(2);
        }

        public int GetLife()
        {
            return this.life;
        }

        /// <summary>
        /// Updates the logic of the Base
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            lifeBar.Update(deltaTime);
            collider.Update(position, rotation);

            int percentage = (life * 100) / initialLife;
            if (percentage == 100) lifeBar.setAnimAux(0);
            else if (percentage > 85) lifeBar.setAnimAux(1);
            else if (percentage > 70) lifeBar.setAnimAux(2);
            else if (percentage > 60) lifeBar.setAnimAux(3);
            else if (percentage > 50) lifeBar.setAnimAux(4);
            else if (percentage > 40) lifeBar.setAnimAux(5);
            else if (percentage > 30) lifeBar.setAnimAux(6);
            else if (percentage > 15) lifeBar.setAnimAux(7);
            else if (percentage > 0) lifeBar.setAnimAux(8);
            else lifeBar.setAnimAux(9);
                
        }

        /// <summary>
        /// Draws the house 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            lifeBar.Draw(spriteBatch);
            
            if (SuperGame.debug && colisionable)
                collider.Draw(spriteBatch);

        }
    }
}
