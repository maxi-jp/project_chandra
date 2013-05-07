using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace IS_XNA_Shooter.Evolutions
{
    /// <summary>
    /// Shot class fot he preview frame in the evolution screen
    /// </summary>
    class ShotPreview
    {
        /// <summary>
        /// Parameters for the animation of the shot
        /// </summary>
        public const int NUM_ANIMATION = 3,
            WIDTH_SHOT = 14,
            HEIGHT_SHOT = 3;

        /// <summary>
        /// Parameter for the animation of the shot
        /// </summary>
        private const float TIME_ANIMATION = 0.05f;

        /// <summary>
        /// Texture of the shot
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The animation in the image
        /// </summary>
        private Rectangle animationRectangle;

        /// <summary>
        /// The position in the preview frame
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Tell us what animation we have
        /// </summary>
        private int animation;

        /// <summary>
        /// The speed of the shot
        /// </summary>
        private float speed;

        /// <summary>
        /// How many time have the curent animation
        /// </summary>
        private float timeAnim;


        //---------------------------------------------------------

        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="content"></param>
        public ShotPreview(ContentManager content)
        {
            // initial animation and the time for changing the animation
            animation = 0;
            timeAnim = 0;

            // initialize texture
            texture = content.Load<Texture2D>("Graphics/laserShotAnim");

            // initialize animation rectangle
            animationRectangle = new Rectangle(0, 0, WIDTH_SHOT, HEIGHT_SHOT);

            //initialize the position of the shot
            position = Vector2.Zero;

            //initialize the speed of the shot
            speed = 0;
        }


        //---------------------------------------------------------

        /// <summary>
        /// Set the current position in the preview frame
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Return the position in the preview frame
        /// </summary>
        /// <returns></returns>
        public Vector2 getPosition()
        {
            return position;
        }

        /// <summary>
        /// Set the current speed
        /// </summary>
        /// <param name="speed"></param>
        public void setSpeed(float speed)
        {
            this.speed = speed;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            //update the animation
            timeAnim += deltaTime;
            if (timeAnim >= TIME_ANIMATION)
            {
                if (animation < NUM_ANIMATION - 1) animation++;
                else animation = 0;
                animationRectangle.X = animation * WIDTH_SHOT;
                timeAnim = 0;
            }

            // update the position
            position.X += (speed * deltaTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, WIDTH_SHOT, HEIGHT_SHOT), animationRectangle, Color.White);
        }
    }
}
