using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class GameA : Game
    {
        /* ------------------------------------------------------------- */
        /*                           ATTRIBUTES                          */
        /* ------------------------------------------------------------- */
        private Sprite aimPointSprite;
        private BackgroundGameA backGround;
        private String levelName;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public GameA(SuperGame mainGame, Player player, String levelName, Texture2D textureAim,
            Texture2D textureBg, Evolution evolution)
            : base(mainGame, player, evolution)
        {
            hud = new IngameHudA(GRMng.hudBase, mainGame.player.GetLife());
            level = new LevelA(camera, levelName, enemies);
            backGround = new BackgroundGameA(camera, level);
            this.levelName = levelName;
            camera.setLevel(level);

            ship = EnemyFactory.GetShipByName("ShipA", this, camera, level, evolution, shots);

            level.setShip(ship);

            aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);

            camera.setShip(ship);
        }

        /* ------------------------------------------------------------- */
        /*                            METHODS                            */
        /* ------------------------------------------------------------- */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // actualizamos posicion del puntero:
            aimPointSprite.position.X = Mouse.GetState().X;
            aimPointSprite.position.Y = Mouse.GetState().Y;
            
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            backGround.Draw(spriteBatch);

            base.Draw(spriteBatch); // Ship, enemies, shots

            spriteBatch.DrawString(SuperGame.fontMotorwerk, "Enemies left: " + enemies.Count(),
                new Vector2(25, 18), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            aimPointSprite.Draw(spriteBatch); // aim point

            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Ship=" + ship.position + ".",
                    new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                // number of enemies:
                spriteBatch.DrawString(SuperGame.fontDebug, "Enemies in game = " + enemies.Count() + ".",
                    new Vector2(5, 27), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

        } // Draw

    } // class GameA
}
