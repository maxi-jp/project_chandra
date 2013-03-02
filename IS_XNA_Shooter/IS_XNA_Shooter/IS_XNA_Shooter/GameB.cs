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
        private BackgroundGameB backGroundB; //Fondo con los parallax
        private BackgroundGameA backGroundA;
        private Sprite aimPointSprite;// ESTO LO HEMOS COPIADO DEL A // WE HAVE COPIATED THIS FROM THE A
        private Texture2D textureAim;
        private List<int> levelList;
        private int currentLevel=0;

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameB(Texture2D textureAim, float playerVelocity)
            : base(playerVelocity)
        {
            camera = new Camera();
            shots = new List<Shot>();
            this.textureAim = textureAim;
            this.playerVelocity = playerVelocity;
            levelList = new List<int>();
            levelList.Add(0); levelList.Add(1); levelList.Add(0);
            initLevelB(1);
        }

        private void initLevelB(int numLevel)
        {
            colliderList = new List<Collider>();


            level = new LevelB(camera, numLevel);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
            // crashList = ((LevelB)level).getRectangles();
            backGroundB = new BackgroundGameB(level);

            player = new PlayerB(camera, ((LevelB)level), Vector2.Zero, 0, puntosColliderPlayer(), GRMng.frameWidthPA2,
                GRMng.frameHeightPA2, GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24,
                GRMng.texturePA2, playerVelocity + 200, shots);
            level.setPlayer(player);
            camera.setPlayer(player);
        }

        private void initLevelA(int numLevel, Texture2D textureAim)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight);
            level = new LevelA(camera, numLevel, enemies);
            backGroundA = new BackgroundGameA(camera, level);

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
            player = new PlayerA(camera, level, Vector2.Zero, 0, points, GRMng.frameWidthPA2, GRMng.frameHeightPA2,
                GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24, GRMng.texturePA2,
                playerVelocity, shots);

            level.setPlayer(player);

            aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);

            camera.setPlayer(player);
        }



        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (level.getFinish())
            {
                currentLevel++;
                if (currentLevel< levelList.Count)
                    initNextLevel(levelList[currentLevel]);
            }

            //actualiza background
            if (backGroundB!=null)
                backGroundB.Update(deltaTime);
        }

        private void initNextLevel(int level)
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //dibuja background
            if (backGroundB != null)
                backGroundB.Draw(spriteBatch);
            if (backGroundA != null)
                backGroundA.Draw(spriteBatch);
            //dibuja player, enemigos y balas
            base.Draw(spriteBatch);
            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Player=" + player.position + ".",
                    new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
         }



        //--------------------------------
        //----    Métodos privados    ----
        //--------------------------------

        //Método privado que calcula los puntos de colisión de la nave
        private Vector2[] puntosColliderPlayer()
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
