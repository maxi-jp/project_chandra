using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    class GameC:Game
    {
        private Sprite aimPointSprite;
        private BackgroundGameA backGround;
        private String levelName;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public GameC(SuperGame mainGame, Player player, String levelName, Texture2D textureAim,
            Texture2D textureBg, Evolution evolution)
            : base(mainGame, player, evolution)
        {
            hud = new IngameHudA(GRMng.hudBase, mainGame.player.GetLife());
            level = new LevelC(camera, levelName, enemies);
            backGround = new BackgroundGameA(camera, level);
            this.levelName = levelName;
            
            camera.setLevel(level);

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(15, 35);
            points[1] = new Vector2(26, 33);
            points[2] = new Vector2(34, 15);
            points[3] = new Vector2(65, 30);
            points[4] = new Vector2(65, 50);
            points[5] = new Vector2(34, 66);
            points[6] = new Vector2(26, 47);
            points[7] = new Vector2(15, 45);

            ship = new ShipA(this, camera, level, Vector2.Zero, 0, points,
                GRMng.frameWidthPA1, GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1,
                GRMng.loopingPA1, SuperGame.frameTime24, GRMng.texturePA1, GRMng.texturePA1_shield,
                evolution, shots);

            level.setShip(ship);

            aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);

            camera.setShip(ship);

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

    } // class GameC

}

