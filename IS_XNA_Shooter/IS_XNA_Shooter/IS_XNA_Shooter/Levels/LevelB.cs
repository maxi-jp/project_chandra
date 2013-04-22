using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace IS_XNA_Shooter
{
    class LevelB : Level
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------
        // Atributes for the Rectangles map
        private List<RectangleMap> listRecMap;
        private int[] rectangleMap;

        // Atributes for the Background layers
        private struct bgLayer
        {
            public int level;
            public int velocity;
            public int numPng;
            public String[/*numPng*/] pngList;
        };
        private int numLayers;
        private bgLayer[/*numLayers*/] backgroundList;

        private bool testingEnemies;

        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public LevelB(Camera camera, int numLevel, List<Enemy> enemies,
            List<RectangleMap> listRecMap)
            : base(camera, numLevel, enemies)
        {
            testingEnemies = false;

            this.listRecMap = listRecMap;
            this.enemies = enemies;

            switch (numLevel)
            {
                case 0: // level for testing enemies
                    //TODO: hacer nivel de testeo de enemigos
                    testingEnemies = true;
                    break;

                case 1: // LevelB 1
                    width = SuperGame.screenWidth * 2;
                    height = SuperGame.screenHeight;
                    ShipInitPosition = new Vector2(100, SuperGame.screenHeight / 2);

                    ReadRectangles();     // load the rectangle map
                    LeerArchivoXML(1, 0); // load the enemies
                    break;

                case 2: // LevelB 2 DORITO
                    width = SuperGame.screenWidth * 2;
                    height = SuperGame.screenHeight;
                    ShipInitPosition = new Vector2(100, SuperGame.screenHeight / 2);

                    //rectangleMap = new int[0];
                    ReadRectangles();     // load the rectangle map

                    // DORITO IS GOING TO KILL YOU MOTHER****ER
                    Vector2 positionFinalBoss = new Vector2(SuperGame.screenWidth - GRMng.frameWidthFinalBoss1/2,
                        SuperGame.screenHeight / 2);
                    Enemy finalBoss = new FinalBoss1(camera, this, positionFinalBoss, enemies);
                    finalBoss.SetActive();

                    enemies.Add(finalBoss);
                    break;
            }
        }

        public LevelB(Camera camera, int numLevel, List<Enemy> enemies)
            : base(camera, numLevel, enemies)
        {
            width = SuperGame.screenWidth*2;
            height = SuperGame.screenHeight;

            this.enemies = enemies;
            //ReadRectangles();

            //Enemigo
            /*Enemy e1 = new EnemyLaserB(camera, this, new Vector2(SuperGame.screenWidth - 100, 
                50)/*new Random().Next(SuperGame.screenHeight))*//*, (float)Math.PI, GRMng.frameWidthEL, 
                GRMng.frameHeightEL, GRMng.numAnimsEL, GRMng.frameCountEL, GRMng.loopingEL, SuperGame.frameTime10, 
                GRMng.textureEL, 1, -200, 100, 1, null);
            e1 = new FinalBoss1(camera, this, new Vector2(SuperGame.screenWidth - GRMng.frameWidthFinalBoss1, SuperGame.screenHeight / 2), enemies);
            e1.SetActive();
            enemies.Add(e1);
            */
            Vector2 positionFinalBoss = new Vector2(SuperGame.screenWidth - GRMng.frameWidthFinalBoss1/2,
                                                    SuperGame.screenHeight / 2);
            Enemy finalBoss = new FinalBoss1(camera, this, positionFinalBoss, enemies);
            finalBoss.SetActive();

            enemies.Add(finalBoss);

        }


        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(float deltaTime)
        {

            int i=0; // iterator for the list of enemies
            bool stillAlive = false; // is true if there is any enemie alive
            //the next loop searches an enemy alive for controlling the end of level 
            if (!levelFinished)
            {
                while (i < enemies.Count && !stillAlive)
                {
                    if (enemies[i] != null && !enemies[i].isDead())
                    {
                        stillAlive = true;
                    }
                    i++;
                }
                if (!stillAlive)
                    levelFinished = true;
            }
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void ReadRectangles()
        {
            // get the reference of the XML rectangle map:
            XmlDocument lvl = null;
            switch (numLevel)
            {
                case 1: // levelB 1
                    lvl = XMLLvlMng.rect1;
                    break;

                case 2: // levelB 2 (DORITO)
                    lvl = XMLLvlMng.rect2;
                    break;
            }

            XmlNodeList level = lvl.GetElementsByTagName("level");

            // variables auxiliares para la lectura de los layers
            int nivel, vel, numpngs, i=0;
            XmlAttributeCollection bgLayerList, pngNode;

            XmlNodeList listBgs = ((XmlElement)level[0]).GetElementsByTagName("listaBackgrounds");
            numLayers = (int)Convert.ToDouble(listBgs.Item(0).Attributes[0].Value);
            backgroundList = new bgLayer[numLayers];

            XmlNodeList listsBg = ((XmlElement)level[0]).GetElementsByTagName("BGLayer");
            foreach (XmlElement nodo3 in listsBg)
            {
                bgLayerList = nodo3.Attributes;

                nivel = (int)Convert.ToDouble(bgLayerList[0].Value);
                vel = (int)Convert.ToDouble(bgLayerList[1].Value);
                numpngs = (int)Convert.ToDouble(bgLayerList[2].Value);

                XmlNodeList listaPngs = nodo3.GetElementsByTagName("png");
                String[] pngListAux = new String[numpngs];
                int j = 0;
                foreach (XmlElement nodo4 in listaPngs)
                {
                    pngNode = nodo4.Attributes;

                    pngListAux[j] = pngNode[0].Value;
                    j++;
                }
                backgroundList[i].level = nivel;
                backgroundList[i].velocity = vel;
                backgroundList[i].numPng = numpngs;
                backgroundList[i].pngList = pngListAux;

                i++;
            }

            // variables auxiliares para la lectura de los rectángulos
            int nR, lW, lH; // rectangle list map component
            int rX, rY, rW, rH; // rectangle components
            XmlAttributeCollection rectangleList, rectangleNode;
            Rectangle recAux;
            List<Rectangle> listAux;
            RectangleMap recMapAux;

            XmlNodeList listsRect = ((XmlElement)level[0]).GetElementsByTagName("listaRectangles");
            foreach (XmlElement nodo1 in listsRect)
            {
                listAux = new List<Rectangle>();

                rectangleList = nodo1.Attributes;

                nR = (int)Convert.ToDouble(rectangleList[0].Value);
                lW = (int)Convert.ToDouble(rectangleList[1].Value);
                lH = (int)Convert.ToDouble(rectangleList[2].Value);

                XmlNodeList listaRectangles = nodo1.GetElementsByTagName("rectangle");
                foreach (XmlElement nodo2 in listaRectangles)
                {
                    rectangleNode = nodo2.Attributes;

                    rX = (int)Convert.ToDouble(rectangleNode[0].Value);
                    rY = (int)Convert.ToDouble(rectangleNode[1].Value);
                    rW = (int)Convert.ToDouble(rectangleNode[2].Value);
                    rH = (int)Convert.ToDouble(rectangleNode[3].Value);

                    recAux = new Rectangle(rX, rY, rW, rH);
                    listAux.Add(recAux);
                }

                recMapAux = new RectangleMap(lW, lH, listAux);
                listRecMap.Add(recMapAux);
            }

            // lista de mapas de rectángulos
            int ind;
            XmlAttributeCollection mapN;
            XmlNodeList mapList = ((XmlElement)level[0]).GetElementsByTagName("map");
            rectangleMap = new int[mapList.Count];
            for (int k = 0; k<mapList.Count; k++)
            {
                mapN = mapList.Item(k).Attributes;
                ind = (int)Convert.ToInt32(mapN[0].Value);
                rectangleMap[k] = ind;
            }

        } // ReadRectangles

        public BackgroundLayerB InitializeBGLayerC()
        {
            if (backgroundList[0].numPng == 0)
                return null;
            else
            {
                BackgroundLayerB bgLayerC;

                List<Texture2D> texturesC = new List<Texture2D>();
                for (int i = 0; i < backgroundList[0].numPng; i++)
                    texturesC.Add(GRMng.GetTextureById(backgroundList[0].pngList[i]));
                bgLayerC = new BackgroundLayerB(rectangleMap, texturesC, 0, false, 1, true);

                return bgLayerC;
            }
        }

        public List<BackgroundLayerB> InitializeBGLayers()
        {
            List<BackgroundLayerB> bgLayerList = new List<BackgroundLayerB>();

            for (int i = 1; i < numLayers; i++)
            {
                BackgroundLayerB bgLayer;
                int[] textureIndex = new int[backgroundList[i].numPng];
                for (int j = 0; j < backgroundList[i].numPng; j++)
                    textureIndex[j] = j;
                List<Texture2D> textures0 = new List<Texture2D>();
                for (int k = 0; k < backgroundList[i].numPng; k++)
                    textures0.Add(GRMng.GetTextureById(backgroundList[i].pngList[k]));
                bgLayer = new BackgroundLayerB(textureIndex, textures0, backgroundList[i].velocity, true, 1.4f, false);
                bgLayerList.Add(bgLayer);
            }

            return bgLayerList;
        }

        //devuelve la lista de rectangulos de colisión del parallax donde se juega
        public List<List<Rectangle>> getRectangles()
        {
            return null;// listRectCollider;
        }

        public int[] GetLevelMap()
        {
            return rectangleMap;
        }

        //devuelve la lista de enemigos del nivel
        public List<Enemy> getEnemies()
        {
            return enemies;
        }

    } // class LevelB
}
