using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class House : Animation
    {
        /// <summary>
        /// House's collider
        /// </summary>
        public Collider collider;

        /// <summary>
        /// House's life
        /// </summary>
        protected int life;


        // control variables:

        /// <summary>
        /// if it isn't active you cant see it
        /// </summary>
        protected bool active;

        /// <summary>
        /// indicates if the House is colisionable or not
        /// </summary>
        protected bool colisionable;

        /// <summary>
        /// indicates if the House is no longer necesary in the Game
        /// </summary>
        protected bool erasable;

        /// <summary>
        /// Constructor for house
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the House</param>
        /// <param name="rotation">The rotation of the House</param>
        /// <param name="frameWidth">The width of each frame of the House's animation</param>
        /// <param name="frameHeight">The height of each frame of the House's animation </param>
        /// <param name="numAnim">The number of the House's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the House</param>
        /// <param name="life">The life of the House</param>
        public House(Camera camera, Level level, Vector2 position, float rotation,
                    short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
                    float frametime, Texture2D texture, int life) 
                    : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                            frameCount, looping, frametime)
        {
            this.life = life;
            setAnim(0);
            active = true;
            colisionable = true;
            erasable = false;
            Vector2[] points = new Vector2[4];
            points[0] = new Vector2(0, 0);
            points[1] = new Vector2(79, 0);
            points[2] = new Vector2(79, 79);
            points[3] = new Vector2(0, 79);

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
            level.DeadHouse();
        }

        public int GetLife()
        {
            return this.life;
        }

        /// <summary>
        /// Updates the logic of the House
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            collider.Update(position, rotation);
        }

        /// <summary>
        /// Draws the house 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (SuperGame.debug && colisionable)
                collider.Draw(spriteBatch);

        }
    }
}
