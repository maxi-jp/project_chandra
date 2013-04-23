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
        private List<RectangleMap> listRecMap;
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

            listRecMap = new List<RectangleMap>();

            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            level = new LevelB(camera, numLevel, enemies, listRecMap);
            rectangleMap = ((LevelB)level).GetLevelMap();
            backGround = new BackgroundGameB((LevelB)level);

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
        }

        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            scrollPosition += scrollVelocity * deltaTime;

            for (int i = 0; i < powerUpList.Count; i++)
            {
                powerUpList[i].UpdatePosition(powerUpList[i].getPosition() + new Vector2(-deltaTime*200, 0));
                //+  new Vector2(scrollPosition,0)
            }

                backGround.Update(deltaTime);

            // player-walls(rectangles) collision:
            int cont = 0;
            Rectangle recAux;
            for (int i = 0; i < listRecMap.Count(); i++)
            {
                for (int j = 0; j < listRecMap[i].rectangleList.Count; j++)
                {
                    recAux = new Rectangle(
                        listRecMap[i].rectangleList[j].X - (int)scrollPosition + cont,
                        listRecMap[i].rectangleList[j].Y,
                        listRecMap[i].rectangleList[j].Width,
                        listRecMap[i].rectangleList[j].Height);
                    for (int k = 0; k < ship.collider.points.Length; k++)
                    { 
                        if (recAux.Contains((int)ship.collider.points[k].X, (int)ship.collider.points[k].Y))
                            ship.Kill();
                    }
                }
                cont += listRecMap[rectangleMap[i]].width;
            }
            // TODO: hay que descargar la mayoría de casos

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            backGround.Draw(spriteBatch, scrollPosition);

            base.Draw(spriteBatch); // Ship, enemies, shots

            if (SuperGame.debug)
            {
                int cont = 0;
                /*for (int i = 0; i < listRecMap.Count; i++)
                {
                    listRecMap[i].Draw(spriteBatch, -(int)scrollPosition + cont);
                    cont += listRecMap[i].width;
                }*/
                for (int i = 0; i < rectangleMap.Length; i++)
                {
                    listRecMap[rectangleMap[i]].Draw(spriteBatch, -(int)scrollPosition + cont);
                    cont += listRecMap[rectangleMap[i]].width;
                }

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
