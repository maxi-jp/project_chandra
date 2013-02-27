using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase básica para todos los objetos que se pintan en el juego
    class Sprite
    {
        /* ------------------- ATRIBUTOS ------------------- */
        public Vector2 position;
        public float rotation;
        protected Vector2 drawPoint;

        // graphic resources
        public Texture2D texture;

        private Rectangle rectangle;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Sprite(bool middlePosition, Vector2 position, float rotation, Texture2D texture)
        {
            this.position = position;
            this.rotation = rotation;
            this.texture = texture;

            // origin to draw the image
            if (middlePosition)
                drawPoint = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                drawPoint = Vector2.Zero;
        }

        public Sprite(bool middlePosition, Vector2 position, float rotation, Texture2D texture,
            Rectangle rectangle)
        {
            this.position = position;
            this.rotation = rotation;
            this.texture = texture;
            this.rectangle = rectangle;
            if (middlePosition)
                drawPoint = new Vector2(rectangle.Width / 2 + rectangle.X,
                    rectangle.Height / 2 + rectangle.Y);
            else
                drawPoint = Vector2.Zero;
        }

        /* ------------------- MÉTODOS ------------------- */
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, drawPoint,
                Program.scale, SpriteEffects.None, 0);
        }

        public virtual void DrawRectangle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, rotation, drawPoint,
                Program.scale, SpriteEffects.None, 0);
        }

        public void SetRectangle(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

    } // class Sprite
}
