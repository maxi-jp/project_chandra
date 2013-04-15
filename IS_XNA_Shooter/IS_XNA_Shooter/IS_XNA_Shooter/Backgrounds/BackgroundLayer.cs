using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class BackgroundLayer : SpriteCamera
    {
        private float speed; // velocidad a la que se desplaza la capa

        private bool tileable;

        private int screenWidth;
        private int screenHeight;
        private float scale;

        private int tileX, tileY;

        public BackgroundLayer (Camera camera, Level level, bool middlePosition, Vector2 position,
            float rotation, Texture2D texture, float speed, bool tileable, float scale)
            : base(camera, level, middlePosition, position, rotation, texture)
        {
            this.speed = speed;
            this.tileable = tileable;
            this.scale = scale;

            screenWidth = SuperGame.screenWidth;
            screenHeight = SuperGame.screenHeight;

            if (tileable)
            {
                tileX = screenWidth / texture.Width + 2;
                tileY = screenHeight / texture.Height + 2;
            }
            else tileX = tileY = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            if (tileable)
                for (int j=0; j < tileY; j++)
                    for (int i = -1; i < tileX; i++)
                        spriteBatch.Draw(texture, position + camera.displacement*speed + new Vector2(texture.Width*i,texture.Height*j),
                            null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(texture, position + camera.displacement * speed,
                       null, Color.White, rotation, base.drawPoint, (float)(Program.scale * scale), SpriteEffects.None, 0);
        }

    } // class BackgroundLayer
}
