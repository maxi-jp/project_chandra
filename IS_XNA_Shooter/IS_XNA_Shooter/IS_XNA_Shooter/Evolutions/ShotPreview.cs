using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace IS_XNA_Shooter.Evolutions
{
    class ShotPreview
    {
        public const int NUM_ANIMATION = 3,
            WIDTH_SHOT = 14,
            HEIGHT_SHOT = 3;
        private const float TIME_ANIMATION = 0.05f;

        private Texture2D texture;
        private Rectangle animationRectangle;
        private Vector2 position;
        private int animation;
        private float speed,
            timeAnim;


        //---------------------------------------------------------


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


        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setSpeed(float speed)
        {
            this.speed = speed;
        }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, WIDTH_SHOT, HEIGHT_SHOT), animationRectangle, Color.White);
        }
    }
}
