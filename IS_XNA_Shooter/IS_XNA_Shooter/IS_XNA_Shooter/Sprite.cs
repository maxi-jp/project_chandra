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
        public float scale;
        protected Vector2 drawPoint;

        // graphic resources
        public Texture2D texture;
        public Color color;

        protected Rectangle rectangle;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Sprite(bool middlePosition, Vector2 position, float rotation, Texture2D texture)
        {
            this.position = position;
            this.rotation = rotation;
            scale = 1;
            scale = scale * Program.scale;
            this.texture = texture;

            // origin to draw the image
            if (middlePosition)
                drawPoint = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                drawPoint = Vector2.Zero;

            color = Color.White;
        }

        public Sprite(bool middlePosition, Vector2 position, float rotation, Texture2D texture,
            Rectangle rectangle)
        {
            this.position = position;
            this.rotation = rotation;
            scale = 1;
            scale = scale * Program.scale;
            this.texture = texture;
            this.rectangle = rectangle;
            if (middlePosition)
                drawPoint = new Vector2(rectangle.Width / 2/* + rectangle.X*/,
                    rectangle.Height / 2 /*+ rectangle.Y*/);
            else
                drawPoint = Vector2.Zero;

            color = Color.White;
        }

        /* ------------------- MÉTODOS ------------------- */
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, drawPoint,
                scale, SpriteEffects.None, 0);
        }

        public virtual void DrawRectangle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, color, rotation, drawPoint,
                scale, SpriteEffects.None, 0);
        }

        public void SetRectangle(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public void SetTransparency(byte i)
        {
            color = new Color(i, i, i, i);
        }

    } // class Sprite
}
