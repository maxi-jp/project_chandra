using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // Es la clase que gestiona los parallax del juego B.
    class BackgroundLayerB : Sprite
    {
        private float speed; // velocidad a la que se desplaza la capa

        private bool tileable;

        private int screenWidth;
        private int screenHeight;
        private float scale;

        private int tileX, tileY;

        public BackgroundLayerB (bool middlePosition, Vector2 position,
            float rotation, Texture2D texture, float speed, bool tileable, float scale)
            : base(middlePosition, position, rotation, texture)
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
                tileX = 5;
            }
            else tileX = tileY = 1;
        }

        public void Update(float deltaTime)
        {
            position.X += speed * deltaTime;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            if (tileable)
                for (int j=0; j < tileY; j++)
                    for (int i = -1; i < tileX; i++)
                        spriteBatch.Draw(texture, position + new Vector2(texture.Width*i,texture.Height*j),
                            null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(texture, position,
                       null, Color.White, rotation, base.drawPoint, (float)(Program.scale * scale), SpriteEffects.None, 0);
        }
    }
}
