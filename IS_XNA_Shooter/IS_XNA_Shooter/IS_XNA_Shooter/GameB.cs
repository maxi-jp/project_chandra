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

        // private List<List<Rectangle>> crashList;  //objetos de colisión en el parallax donde se juega
        private List<Collider> colliderList; //lista de colliders para crashlist
        private BackgroundGameB backGround; //Fondo con los parallax

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameB(int numLevel, Texture textureAim, float ShipVelocity, int ShipLife)
            : base(ShipVelocity, ShipLife)
        {
            camera = new Camera();
            shots = new List<Shot>();
            colliderList = new List<Collider>();
            level = new LevelB(camera, numLevel);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
           // crashList = ((LevelB)level).getRectangles();
            backGround = new BackgroundGameB(level);

            Ship = new ShipB(camera, ((LevelB)level), Vector2.Zero, 0, puntosColliderShip(), GRMng.frameWidthPA2,
                GRMng.frameHeightPA2, GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24,
                GRMng.texturePA2, ShipVelocity + 200, ShipLife, shots);

            level.setShip(Ship);
            camera.setShip(Ship);            
        }


        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //actualiza background
            backGround.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //dibuja background
            backGround.Draw(spriteBatch);
            //dibuja Ship, enemigos y balas
            base.Draw(spriteBatch);
            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Ship=" + Ship.position + ".",
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
