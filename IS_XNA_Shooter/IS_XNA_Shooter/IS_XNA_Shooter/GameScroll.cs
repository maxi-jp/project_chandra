using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    //Clase que gestiona el GameScroll 
    class GameScroll : Game
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------
        // private List<List<Rectangle>> crashList;  //objetos de colisión en el parallax donde se juega
        private List<Collider> colliderList; //lista de colliders para crashlist
        private BackgroundGameB backGroundB; //Fondo con los parallax

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameScroll(SuperGame mainGame, int numLevel, Texture2D textureAim, float ShipVelocity, int ShipLife, Player player)
            : base(mainGame, player, ShipVelocity,ShipLife)
        {
            camera = new Camera();
            shots = new List<Shot>();
            this.shipVelocity = ShipVelocity;
            initLevelB(1);
        }

        private void initLevelB(int numLevel)
        {
            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            colliderList = new List<Collider>();
            level = new LevelB(camera, numLevel, enemies, null);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
            // crashList = ((LevelB)level).getRectangles();
            backGroundB = new BackgroundGameB(level);

            ship = new ShipB(this, camera, ((LevelB)level), Vector2.Zero, 0, puntosColliderShip(), GRMng.frameWidthPA1,
                GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1, GRMng.loopingPA1, SuperGame.frameTime24,
                GRMng.texturePA1, shipVelocity + 200, shipLife, shots);
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

            //actualiza background
            if (backGroundB != null)
                backGroundB.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //dibuja background
            if (backGroundB != null)
                backGroundB.Draw(spriteBatch);
            //dibuja player, enemigos y balas
            base.Draw(spriteBatch);
            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Player=" + ship.position + ".",
                    new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }



        //--------------------------------
        //----    Métodos privados    ----
        //--------------------------------

        //Método privado que calcula los puntos de colisión de la nave
        private Vector2[] puntosColliderShip()
        {
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(15, 35);
            points[1] = new Vector2(26, 33);
            points[2] = new Vector2(34, 15);
            points[3] = new Vector2(65, 30);
            points[4] = new Vector2(65, 50);
            points[5] = new Vector2(34, 66);
            points[6] = new Vector2(26, 47);
            points[7] = new Vector2(15, 45);

            return points;
        }


    }//GameB
}
