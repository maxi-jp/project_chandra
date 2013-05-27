using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // Es la clase que gestiona los parallax del juego B.
    class BackgroundLayerB
    {
        private float speed; // velocidad a la que se desplaza la capa

        private bool tileable;
        // An array of positions of the parallaxing background
        private Vector2[] positions;

        private float scale;

        private List<Texture2D> textures;
        private int[] rectangleMap;

        private bool collisionable;

        private int tileX, tileY;
        private int numtex1 = 0;
        private int numtex2;

        public BackgroundLayerB (int[] rectangleMap, List<Texture2D> textures,
            float speed, bool tileable, float scale, bool collisionable)
        {
            this.rectangleMap = rectangleMap;
            this.textures = textures;

            this.speed = speed;
            this.tileable = tileable;
            this.scale = scale;
            this.collisionable = collisionable;

            if (tileable)
            {
                // If we divide the screen with the texture width then we can determine the number of tiles need.
                // We add 1 to it so that we won't have a gap in the tiling
                positions = new Vector2[SuperGame.screenWidth / (int)(textures[0].Width*scale) + 2];
                // Set the initial positions of the parallaxing background
                for (int i = 0; i < positions.Length; i++)
                {
                    // We need the tiles to be side by side to create a tiling effect
                    positions[i] = new Vector2(i * textures[0].Width * scale, 0);
                }
            }
            else
            {
                positions = new Vector2[1];
                positions[0] = new Vector2();
            }

        }

        public void Update(float deltaTime)
        {
            // Update the positions of the background
            for (int i = 0; i < positions.Length; i++)
            {
                // Update the position of the screen by adding the speed
                positions[i].X -= speed * deltaTime;
                // Check the texture is out of view then put that texture at the end of the screen
                if (positions[i].X <= -textures[0].Width * scale)
                {
                    positions[i].X = textures[0].Width * scale * (positions.Length - 1);
                }
            }

        } // Update

        public void Draw(SpriteBatch spriteBatch, float scrollPosition)
        {
            if (tileable)
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    spriteBatch.Draw(textures[0], positions[i], null, Color.White, 0, Vector2.Zero, scale,
                        SpriteEffects.None, 1);
                }
            }
            else
            {
                int cont = (int)positions[0].X;
                if (rectangleMap != null) // OJO: ÑAPA
                for (int i = 0; i < rectangleMap.Length; i++)
                {
                    spriteBatch.Draw(textures[rectangleMap[i]], new Vector2(-scrollPosition + cont, 0), null,
                        Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                    cont += (int)(textures[rectangleMap[i]].Width * scale);
                }
            }
            // TODO: hay que comprobar si la posicion de la textura esta en pantalla para ahorrar draw calls

            //base.Draw(spriteBatch);
            /*if (tileable)
                if (collisionable)
                    for (int j = 0; j < tileY; j++)
                    {
                        if (position.X + texture.Width <= SuperGame.screenWidth / 2)// si la posicion del primer layer llega al fin
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
        */
        } // Draw

        public float GetScale()
        {
            return scale;
        }

    } // class BackgroundLayerB
}
