using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    class GameADefense : GameA
    {

         /* ------------------------------------------------------------- */
        /*                           ATTRIBUTES                          */
        /* ------------------------------------------------------------- */

        private Base house;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public GameADefense(SuperGame mainGame, Player player, int num, Texture2D textureAim,
            Texture2D textureBg, float shipVelocity, int shipLife, Vector2 housePosition, short frameWidth, short frameHeight,
            short numAnim, short[] frameCount, bool[] looping, float frametime, Texture2D texture, short frameLifeBarWidth, 
            short frameLifeBarHeight, short numAnimLifeBar, short[] frameCountLifeBar, bool[] loopingLifeBar,
            float frametimeLifeBar, Texture2D textureLifeBar, int houseLife)
            : base(mainGame, player,num,textureAim, textureBg,  shipVelocity, shipLife)
        {
            
            house = new Base(camera, level, housePosition, 0, frameWidth, frameHeight, numAnim, frameCount,
                 looping, frametime, texture, frameLifeBarWidth, frameLifeBarHeight, numAnimLifeBar, frameCountLifeBar,
                 loopingLifeBar, frametimeLifeBar, textureLifeBar , houseLife);

            level.setBase(house);
        }

        /* ------------------------------------------------------------- */
        /*                            METHODS                            */
        /* ------------------------------------------------------------- */

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            house.Update(deltaTime);
           
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch); // Ship, enemies, shots

            house.Draw(spriteBatch);

           

        } // Draw

    }
}
