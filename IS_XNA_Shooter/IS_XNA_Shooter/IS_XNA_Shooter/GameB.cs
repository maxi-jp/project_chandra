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
        private playState currentState;
        private List<Collider> colliderList; //lista de colliders para crashlist
        private BackgroundGameB backGroundB; //Fondo con los parallax
        private BackgroundGameA backGroundA;
        private Texture2D textureAim;
        private List<int> levelList;
        private int currentLevel=0;
        public Sprite spriteGetReady;
        public Sprite spriteNum;
        private float timeToResumeAux=5f;

        public enum playState
        {
            beginLevel,
            playing,
            shipDestroyed,
            levelComplete
        };

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameB(SuperGame mainGame, int numLevel, Texture2D textureAim, float shipVelocity, int shipLife)
            : base(mainGame, shipVelocity, shipLife)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight, mainGame.player.GetLife());
            camera = new Camera();
            shots = new List<Shot>();
            this.textureAim = textureAim;
            this.shipVelocity = shipVelocity;
            this.shipLife = shipLife;
            levelList = new List<int>();
            levelList.Add(0); levelList.Add(1);
            levelList.Add(0); levelList.Add(1);
            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
               GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));
            initLevelB(1);
        }

        private void initLevelB(int numLevel)
        {
            currentState = playState.beginLevel;
            colliderList = new List<Collider>();
            level = new LevelB(camera, numLevel);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
            // crashList = ((LevelB)level).getRectangles();
            backGroundB = new BackgroundGameB(level);
            //backGroundA.Dispose();
            backGroundA = null;
            Ship = new ShipB(camera, ((LevelB)level), new Vector2(150f,380f), 0, puntosColliderShip(), GRMng.frameWidthPA2,
                GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1, GRMng.loopingPA1, SuperGame.frameTime24,
                GRMng.texturePA1, shipVelocity + 200, shipLife, shots);
            //level.setShip(Ship);
            camera.setShip(ship);
        }

        private void initLevelA(int numLevel, Texture2D textureAim)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight, 3); // three lifes because yes
            level = new LevelA(camera, numLevel, enemies);
            backGroundA = new BackgroundGameA(camera, level);
            //backGroundB.Dispose();
            backGroundB = null;

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
            ship = new ShipA(camera, level, Vector2.Zero, 0, points, GRMng.frameWidthPA1, GRMng.frameHeightPA1,
                GRMng.numAnimsPA1, GRMng.frameCountPA1, GRMng.loopingPA1, SuperGame.frameTime24, 
                GRMng.texturePA1, shipVelocity + 200, shipLife, shots);


            //aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);


            level.setShip(ship);
            camera.setShip(ship);            
        }


        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currentState)
            {
                case playState.beginLevel:
                    timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0)
                    {
                        currentState = playState.playing;
                        timeToResumeAux = 5f;
                    }
                    else if (timeToResumeAux >= 5f * 2 / 3)
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    else if (timeToResumeAux >= 5f / 3)
                        spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));
                
                    else
                        spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));
                    if (backGroundB != null)
                        backGroundB.Update(deltaTime);
                    break;
                case playState.playing:
                    base.Update(gameTime);
                    if (level.getFinish())
                    {
                        currentLevel++;
                        if (currentLevel< levelList.Count)
                            initNextLevel(levelList[currentLevel]);
                        currentState = playState.beginLevel;
                    }
                    if (backGroundB!=null)
                        backGroundB.Update(deltaTime);
                    break;
                case playState.shipDestroyed:
                    break;
                case playState.levelComplete:
                    break;
            }
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
            //dibuja Ship, enemigos y balas
            if (backGroundA != null)
                backGroundA.Draw(spriteBatch);
            base.Draw(spriteBatch);

            switch (currentState)
            {
                case playState.beginLevel:
                    spriteGetReady.DrawRectangle(spriteBatch);
                    spriteNum.DrawRectangle(spriteBatch);
                    break;
                case playState.playing:
                    if (SuperGame.debug)
                    {
                        spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                            new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(SuperGame.fontDebug, "Ship=" + Ship.position + ".",
                            new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    }
                    break;
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
