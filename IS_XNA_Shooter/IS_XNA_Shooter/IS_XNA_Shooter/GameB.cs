using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    //Clase que gestiona el GameB (scroll)
    class GameB : Game
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------
        private List<List<Rectangle>> listRectCollider;
        private int[] rectangleMap;

        private BackgroundGameB backGround; //Fondo con los parallax

        private float scrollVelocity;
        private float scrollPosition;

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameB(SuperGame mainGame, Player player, int numLevel, Texture2D textureAim,
            float shipVelocity, int shipLife)
            : base(mainGame, player, shipVelocity, shipLife)
        {
            scrollVelocity = 100;
            scrollPosition = 0;

            listRectCollider = new List<List<Rectangle>>();

            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            level = new LevelB(camera, numLevel, enemies, listRectCollider);
            rectangleMap = (LevelB)level.GetLevelMap();
            backGround = new BackgroundGameB(level);

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
            ship = new ShipB(this, camera, level, Vector2.Zero, 0, points,
                GRMng.frameWidthPA1, GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1,
                GRMng.loopingPA1, SuperGame.frameTime24, GRMng.texturePA1,
                shipVelocity, shipLife, shots);

            level.setShip(ship);

            camera.setShip(ship);

            /*levelList = new List<int>();
            levelList.Add(0); levelList.Add(1);
            levelList.Add(0); levelList.Add(1);

            colliderList = new List<Collider>();
            enemies = ((LevelB)level).getEnemies();*/
        }

        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            scrollPosition += scrollVelocity * deltaTime;
            /*if (level.getFinish())
            {
                currentLevel++;
                if (currentLevel< levelList.Count)
                    initNextLevel(levelList[currentLevel]);
            }*/

            backGround.Update(deltaTime);

        } // Update

        /*private void initNextLevel(int level)
        {
            enemies.Clear();
                switch (level)
                {
                    case 0:
                        initLevelB(1);
                        break;
                    case 1:
                        initLevelA(1, textureAim);
                        break;
                }
        }*/

        public override void Draw(SpriteBatch spriteBatch)
        {
            backGround.Draw(spriteBatch);

            for (int i = 0; i < listRectCollider[rectangleMap[0]].Count; i++)
            {
                spriteBatch.Draw(GRMng.redpixel, listRectCollider[rectangleMap[0]][i], new Color(128, 128, 128, 128));
            }

            base.Draw(spriteBatch); // Ship, enemies, shots

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

    }//GameB
}
