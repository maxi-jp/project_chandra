using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase heredada de Sprite para objetos que se pintan con referia a la cámara del juego
    class SpriteCamera : Sprite
    {
        /* ------------------- ATRIBUTOS ------------------- */
        protected Camera camera;
        protected Level level;

        /* ------------------- CONSTRUCTORES ------------------- */
        public SpriteCamera (Camera camera, Level level, bool middlePosition, Vector2 position,
            float rotation, Texture2D texture)
            : base(middlePosition, position, rotation, texture)
        {
            this.camera = camera;
            this.level = level;
        }

        public SpriteCamera(Camera camera, Level level, bool middlePosition, Vector2 position,
            float rotation, Texture2D texture, Rectangle rectTexture)
            : base(middlePosition, position, rotation, texture, rectTexture)
        {
            this.camera = camera;
            this.level = level;
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position + camera.displacement, null, color, rotation,
                base.drawPoint, scale, SpriteEffects.None, 0);
        }

        public override void DrawRectangle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position + camera.displacement, rectangle, color, rotation,
                base.drawPoint, scale, SpriteEffects.None, 0);
        }

    } // SpriteCamera
}
