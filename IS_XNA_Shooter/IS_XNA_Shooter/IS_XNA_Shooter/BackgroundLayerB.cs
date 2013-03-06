using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class that manage the parallax layer in the game B
    /// </summary>
    class BackgroundLayerB : Sprite
    {
        /// <summary>
        /// Movement speed of the layer
        /// </summary>
        private float speed;

        /// <summary>
        /// If we repeat the layer
        /// </summary>
        private bool tileable;

        /// <summary>
        /// Screen width
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// Screen height
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// Scale of the layer
        /// </summary>
        private float scale;

        /// <summary>
        /// List for collisions
        /// </summary>
        private List<List<Rectangle>> listForCollision;

        /// <summary>
        /// List of tectures
        /// </summary>
        private List<Texture2D> textureList;

        /// <summary>
        /// If this layer is collisionable
        /// </summary>
        private bool collisionable;

        /// <summary>
        /// Number of tiles in the axis x
        /// </summary>
        private int tileX;

        /// <summary>
        /// Number of tiles in the axis y
        /// </summary>
        private int tileY;

        /// <summary>
        /// Select a initial texture
        /// </summary>
        private int numtex1 = 0;

        /// <summary>
        /// Select a initial texture collisionable
        /// </summary>
        private int numtex2;

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Builds the BackgroundLayerB
        /// </summary>
        /// <param name="middlePosition"> Set the reference in the center </param>
        /// <param name="position"> Position </param>
        /// <param name="rotation"> Rotation </param>
        /// <param name="texture"> Texture </param>
        /// <param name="speed"> Speed </param>
        /// <param name="tileable"> Set loop </param>
        /// <param name="scale"> Scale </param>
        /// <param name="collisionable"> Set collisionable </param>
        /// <param name="listForCollision"> List of rectangles </param>
        public BackgroundLayerB (bool middlePosition, Vector2 position,
            float rotation, Texture2D texture, float speed, bool tileable, float scale, bool collisionable, List<List<Rectangle>> listForCollision)
            : base(middlePosition, position, rotation, texture)
        {
            this.speed = speed;
            this.tileable = tileable;
            this.scale = scale;
            this.collisionable = collisionable;
            
            //we add the texture of collisionable rectangles
            textureList = new List<Texture2D>();
            textureList.Add(GRMng.textureBgCol1);
            textureList.Add(GRMng.textureBgCol2);
            textureList.Add(GRMng.textureBgCol3);

            numtex2 = new Random().Next(textureList.Count);

            screenWidth = SuperGame.screenWidth;
            screenHeight = SuperGame.screenHeight;

            if (collisionable) 
            {
                this.listForCollision = listForCollision;
            }

            if (tileable)
            {
                tileY = screenHeight / texture.Height + 1;
                tileX = 1;
            }
            else tileX = tileY = 1;
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the background layer B
        /// </summary>
        /// <param name="deltaTime"> Time between the last time and this</param>
        public void Update(float deltaTime)
        {
            position.X += speed * deltaTime;
        }

        /// <summary>
        /// Draw the background layer B
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            if (tileable)
                if (collisionable)
                    for (int j = 0; j < tileY; j++)
                    {
                        if (position.X + texture.Width <= SuperGame.screenWidth / 2)// si la posicion del layer llega al fin
                        {
                            position.X = SuperGame.screenWidth / 2;
                            numtex1 = numtex2;
                            numtex2 = new Random().Next(textureList.Count);
                        }
                        spriteBatch.Draw(textureList[numtex2], position + new Vector2(texture.Width, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                        spriteBatch.Draw(textureList[numtex1], position + new Vector2(0, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                    }
                else
                {
                    for (int j = 0; j < tileY; j++)
                    {
                        if (position.X + texture.Width <= SuperGame.screenWidth / 2)// si la posicion del layer llega al fin
                            position.X = SuperGame.screenWidth / 2;

                        spriteBatch.Draw(texture, position + new Vector2(texture.Width, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                        spriteBatch.Draw(texture, position + new Vector2(0, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);
                    }
                }
            else
                spriteBatch.Draw(texture, position,
                       null, Color.White, rotation, base.drawPoint, (float)(Program.scale * scale), SpriteEffects.None, 0);
        }

    } //Class BackgroundLayerB
}
