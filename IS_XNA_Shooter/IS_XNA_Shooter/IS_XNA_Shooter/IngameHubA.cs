using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class IngameHubA : IngameHub
    {
        /* ------------------- ATRIBUTOS ------------------- */
        private Texture2D textureLeft, textureCenter, textureRight;
        private Vector2 positionLeft, positionRight;
        private Rectangle rectangleCenter;
        private int size;

        /* ------------------- CONSTRUCTORES ------------------- */
        public IngameHubA(Texture2D textureLeft, Texture2D textureCenter, Texture2D textureRight)
            : base()
        {
            size = 140;
            this.textureLeft = textureLeft;
            this.textureCenter = textureCenter;
            this.textureRight = textureRight;

            positionLeft = new Vector2(SuperGame.screenWidth / 2 - size / 2 - textureLeft.Width, 0);
            positionRight = new Vector2(SuperGame.screenWidth / 2 + size / 2, 0);
            rectangleCenter = new Rectangle(SuperGame.screenWidth / 2 - size / 2, 0, size, textureCenter.Height);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureLeft, positionLeft, Color.White);
            spriteBatch.Draw(textureCenter, rectangleCenter, Color.White);
            spriteBatch.Draw(textureRight, positionRight, Color.White);
        }

    } // class IngameHubA
}
