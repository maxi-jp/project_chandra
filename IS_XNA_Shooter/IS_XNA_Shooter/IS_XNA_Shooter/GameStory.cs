using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace IS_XNA_Shooter
{
    //Clase que gestiona el GameB (scroll)
    class GameStory : Game
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------

        // private List<List<Rectangle>> crashList;  //objetos de colisión en el parallax donde se juega
        private playState currentState;
        // for levelB
        private List<RectangleMap> listRecMap; //lista de colliders para crashlist
        private int[] rectangleMap;
        private float scrollVelocity;
        private float scrollPosition;
        //for levelB
        private BackgroundGameB backGroundB; //Fondo con los parallax
        private BackgroundGameA backGroundA;
        private Texture2D textureAim;
        private List<int> levelList;
        private int currentLevel=0;
        public Sprite spriteGetReady;
        public Sprite spriteNum;
        private float timeToResumeAux;
        private int currentConver=0;
        private int currentParagraph=0;
        private List<List<Talk>> gameDialog;

        public enum playState
        {
            levelDialog,
            beginLevel,
            playing,
            shipDestroyed,
            levelComplete
        };

        private struct Talk
        {
            // 0=Chandra, 1=Captain, 2=Robot
            public int whoTalk;
            public String whatSaid;
        }

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public GameStory(SuperGame mainGame, Player player, int numLevel, Texture2D textureAim, float shipVelocity, int shipLife)
            : base(mainGame,player, shipVelocity, shipLife)
        {
            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            camera = new Camera();
            shots = new List<Shot>();
            this.textureAim = textureAim;
            this.shipVelocity = shipVelocity;
            this.shipLife = shipLife;
            levelList = new List<int>();
            levelList.Add(0); levelList.Add(1); levelList.Add(0); // add 3 levels
            gameDialog = new List<List<Talk>>();
            readConversationXML(0);
            setTimeToResume();
            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
               GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));
            initLevelB(1);
        }

        /// <summary>
        /// set timeToResumeAux to an apropiate time corresponding to the lenght of the text
        /// </summary>
        private void setTimeToResume()
        {
            if (currentConver < gameDialog.Count && currentParagraph < gameDialog[currentConver].Count) //if not out of range
            timeToResumeAux= gameDialog[currentConver][currentParagraph].whatSaid.Length * 0.07f;
        }

        private void initLevelB(int numLevel)
        {
            scrollVelocity = 100;
            scrollPosition = 0;
            currentState = playState.levelDialog;

            listRecMap = new List<RectangleMap>();

            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            level = new LevelB(camera, numLevel, enemies, listRecMap);
            rectangleMap = ((LevelB)level).GetLevelMap();
            backGroundB = new BackgroundGameB(level);

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

            /*listRectCollider = new List<List<Rectangle>>();
            level = new LevelB(camera,numLevel,enemies,listRectCollider);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
            // crashList = ((LevelB)level).getRectangles();
            backGroundB = new BackgroundGameB(level);
            //backGroundA.Dispose();
            backGroundA = null;

            ship = new ShipB(this,camera, ((LevelB)level), new Vector2(150f,380f), 0, puntosColliderShip(), GRMng.frameWidthPA1,
                GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1, GRMng.loopingPA1, SuperGame.frameTime24,
                GRMng.texturePA1, shipVelocity + 200, shipLife, shots);
            level.setShip(ship);
            camera.setShip(ship);*/
        }

        private void initLevelA(int numLevel, Texture2D textureAim)
        {
            hub = new IngameHubA(GRMng.hubBase, 3); // three lifes because yes
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
            ship = new ShipA(this,camera, level, Vector2.Zero, 0, points, GRMng.frameWidthPA1, GRMng.frameHeightPA1,
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
                case playState.levelDialog:

                    timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0)
                    {
                        currentParagraph++; //update the current conversation 
                        setTimeToResume();
                        if (currentParagraph >= gameDialog[currentConver].Count)
                        {
                            currentConver++;
                            currentParagraph = 0;
                            currentState = playState.beginLevel;
                            timeToResumeAux = 3f;
                        }
                    }
                    /*if (backGroundB != null)
                        backGroundB.Update(deltaTime);*/
                    break;

                case playState.beginLevel:
                    //base.Update(gameTime);
                    timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0)
                    {
                        currentState = playState.playing;
                        timeToResumeAux = 3f;
                    }
                    else if (timeToResumeAux >= 5f * 2 / 3)
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    else if (timeToResumeAux >= 5f / 3)
                        spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));
                
                    else
                        spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));
                    /*if (backGroundB != null)
                        backGroundB.Update(deltaTime);*/
                    break;
                case playState.playing:
                    base.Update(gameTime);
                    if (level.getFinish())
                    {
                        currentState = playState.levelComplete;
                    }
                    /*if (backGroundB!=null)
                        backGroundB.Update(deltaTime);*/
                    break;
                case playState.shipDestroyed:
                    break;
                case playState.levelComplete:
                    base.Update(gameTime);
                     timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0)
                    {
                        ship.EraseShots();
                        currentLevel++;
                        if (currentLevel < levelList.Count)
                            initNextLevel(levelList[currentLevel]);
                        else
                            mainGame.ExitToMenu();
                        currentState = playState.levelDialog;
                        timeToResumeAux = 5f;
                    }
                    else if (timeToResumeAux >= 5f * 2 / 3)
                    {
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    }
                    else if (timeToResumeAux >= 5f / 3)
                        spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));

                    else
                        spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));
                    break;
            }
            if (backGroundB != null)
            {
                scrollPosition += scrollVelocity * deltaTime;
                backGroundB.Update(deltaTime);
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
            {
                backGroundB.Draw(spriteBatch, scrollPosition);
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
                }
                //dibuja Ship, enemigos y balas
                if (backGroundA != null)
                    backGroundA.Draw(spriteBatch);
                base.Draw(spriteBatch);
                switch (currentState)
                {
                    case playState.levelDialog:

                        spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(100, 500, 1100, 200), Color.White);
                        switch (gameDialog[currentConver][currentParagraph].whoTalk)
                        {
                            case 0:
                                spriteBatch.Draw(GRMng.texturePilot, new Rectangle(110, 510, 280, 180), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(GRMng.textureCaptain, new Rectangle(110, 510, 280, 180), Color.White);
                                break;
                        }

                        //DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
                        spriteBatch.DrawString(SuperGame.fontDebug, gameDialog[currentConver][currentParagraph].whatSaid, new Vector2(400, 510), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                        break;

                    case playState.beginLevel:

                        spriteGetReady.DrawRectangle(spriteBatch);
                        spriteNum.DrawRectangle(spriteBatch);
                        break;

                    case playState.playing:

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
                        break;
                    case playState.levelComplete:
                        spriteGetReady.DrawRectangle(spriteBatch);
                        spriteNum.DrawRectangle(spriteBatch);
                        break;

                }
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

        /// <summary>
        /// Reads the conversation of the level to show it.
        /// </summary>
        /// <param name="level">indicates the corresponding conversation of the level</param>
        private void readConversationXML(int level)
        {
            // Utilizar nombres de fichero y nodos XML idénticos a los que se guardaron

            try
            {
                //  Leer los datos del archivo
                switch (level) 
                {
                    case 0:
                Talk talk;
                int i = 1;
                XmlDocument dialog = XMLLvlMng.dialog1;

                XmlNodeList conversation = dialog.GetElementsByTagName("conversation" + i);

                while (conversation != null)
                {

                    XmlNodeList lista =
                        ((XmlElement)conversation[0]).GetElementsByTagName("paragraph");
                    List<Talk> conver = new List<Talk>();
                    foreach (XmlElement nodo in lista)
                    {
                        talk = new Talk();
                        XmlAttributeCollection paragraph = nodo.Attributes;
                        talk.whoTalk = Convert.ToInt16(paragraph[0].Value);
                        talk.whatSaid = Convert.ToString(paragraph[1].Value);
                        
                        conver.Add(talk);

                    }
                    gameDialog.Add(conver);
                    i++;
                    conversation = dialog.GetElementsByTagName("conversation" + i);
                    }
                        break;
                }

            }
            catch (Exception e)
            {/*
                        System.Diagnostics.Debug.WriteLine("Excepción al leer fichero XML: " + e.Message);
                        if (e.Data != null)
                        {
                            System.Diagnostics.Debug.WriteLine("    Detalles extras:");
                            foreach (DictionaryEntry entrada in e.Data)
                                Console.WriteLine("        La clave es '{0}' y el valor es: {1}", entrada.Key, entrada.Value);
                        }*/
            }

        }
    }//GameB
}
