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
        public GameADefense(SuperGame mainGame, Player player, String levelName, Texture2D textureAim,
            Texture2D textureBg, float shipVelocity, int shipLife, Vector2 housePosition, short frameWidth, short frameHeight,
            short numAnim, short[] frameCount, bool[] looping, float frametime, Texture2D texture, short frameLifeBarWidth, 
            short frameLifeBarHeight, short numAnimLifeBar, short[] frameCountLifeBar, bool[] loopingLifeBar,
            float frametimeLifeBar, Texture2D textureLifeBar, int houseLife, Evolution evolution)
            : base(mainGame, player, levelName, textureAim, textureBg, evolution)
        {
            
            house = new Base(camera, level, housePosition, 0, frameWidth, frameHeight, numAnim, frameCount,
                 looping, frametime, texture, frameLifeBarWidth, frameLifeBarHeight, numAnimLifeBar, frameCountLifeBar,
                 loopingLifeBar, frametimeLifeBar, textureLifeBar, houseLife);

            ((LevelA)level).SetBase(house);
        }

        /* ------------------------------------------------------------- */
        /*                            METHODS                            */
        /* ------------------------------------------------------------- */

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            house.Update(deltaTime);

            // enemies vs house colisions
            for (int i = 0; i < enemies.Count(); i++)
            {
                if (enemies[i].IsColisionable() && (house.collider.Collision(enemies[i].collider)
                    /*|| enemies[i].collider.Collision(house.collider)*/))
                {
                    // the house has been hit by an enemy
                    ((LevelA)level).DamageBase(enemies[i].GetLife());
                    enemies[i].Kill();
                }
            }
           
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); // Ship, enemies, shots

            house.Draw(spriteBatch);

        } // Draw

        protected override bool GameOverCondition()
        {
            return (base.GameOverCondition() || (((LevelA)level).GetBase().GetLife() <= 0));
        }

    } // class GameADefense
}
