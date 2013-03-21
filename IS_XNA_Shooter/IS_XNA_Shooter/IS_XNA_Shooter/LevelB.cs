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
        private List<RectangleMap> listRecMap;
        private int[] rectangleMap;

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

            if (numLevel == 0)
            {
                // level for testing enemies
                //TODO: hacer nivel de testeo de enemigos
                testingEnemies = true;
            }
            else
            {
                width = SuperGame.screenWidth*2;
                height = SuperGame.screenHeight;
                ShipInitPosition = new Vector2(100, SuperGame.screenHeight / 2);

                ReadRectangles();
                LeerArchivoXML(1, 0);

                this.enemies = enemies;
                

                //Enemigo
               /* Enemy e1 = new EnemyWeakB(camera, this, new Vector2(SuperGame.screenWidth + 100, 
                    50)/*new Random().Next(SuperGame.screenHeight))*//*, (float)Math.PI, GRMng.frameWidthEW1, 
                    GRMng.frameHeightEW1, GRMng.numAnimsEW1, GRMng.frameCountEW1, GRMng.loopingEW1, SuperGame.frameTime12, 
                    GRMng.textureEW1, 1, -200, 100, 1, null);
                e1.SetActive();

                enemies.Add(e1);*/
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
            int nR, lW, lH; // rectangle list map component
            int rX, rY, rW, rH; // rectangle components

            XmlDocument lvl = XMLLvlMng.rect1;

            XmlNodeList level = lvl.GetElementsByTagName("level");

            // variables auxiliares para la lectura del XML
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
            
            /*XmlNodeList listaRectangles =
                        ((XmlElement)level[0]).GetElementsByTagName("rectangle");
            foreach (XmlElement nodo in listaRectangles)
            {
                RectangleN = nodo.Attributes;

                rX = (int)Convert.ToDouble(RectangleN[0].Value);
                rY = (int)Convert.ToDouble(RectangleN[1].Value);
                rW = (int)Convert.ToDouble(RectangleN[2].Value);
                rH = (int)Convert.ToDouble(RectangleN[3].Value);

                if (rX == 0 && rY == 0)
                {
                    // detectamos el primer rectangulo de un nuevo layer
                    listRectCollider.Add(new List<Rectangle>());
                }
                Rectangle rectangulo = new Rectangle(rX, rY, rW, rH);

                // añadimos a la lista actual los rectangulos
                listRectCollider[listRectCollider.Count-1].Add(rectangulo); 
            }*/

            // lista de mapas de rectángulos
            int ind;
            XmlAttributeCollection mapN;
            XmlNodeList mapList = ((XmlElement)level[0]).GetElementsByTagName("map");
            rectangleMap = new int[mapList.Count];
            for (int i = 0; i<mapList.Count; i++)
            {
                mapN = mapList.Item(i).Attributes;
                ind = (int)Convert.ToInt32(mapN[0].Value);
                rectangleMap[i] = ind;
            }
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
