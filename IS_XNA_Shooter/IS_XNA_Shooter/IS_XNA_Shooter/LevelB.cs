using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class represents a scroll level
    /// </summary>
    class LevelB : Level
    {
        //-------------------------
        //----    Atributes    ----
        //-------------------------

        /// <summary>
        /// Number of level
        /// </summary>
        private int numLevel;
        /// <summary>
        /// A list of collider rectangles from the level
        /// </summary>
        private List<List<Rectangle>> listRectCollider;



        //-----------------------
        //----    Builder    ----
        //-----------------------

        /// <summary>
        /// The builder of the level
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="numLevel"></param>
        public LevelB(Camera camera, int numLevel, List<Shot> shots)
            : base()
        {
            this.numLevel = numLevel;
            this.enemies = new List<Enemy>();
            this.camera = camera;
            width = SuperGame.screenWidth*2;
            height = SuperGame.screenHeight;
            timeLeftEnemy = new List<float>();
            listRectCollider = new List<List<Rectangle>>();

            //readRectangles();

            Enemy e = new FinalBoss1(camera, this, new Vector2(SuperGame.screenWidth - 40, SuperGame.screenHeight/2), (float)Math.PI, GRMng.frameWidthEW2, GRMng.frameHeightEW2, 
                GRMng.numAnimsEW2, GRMng.frameCountEW2, GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 40, 100, 1, null, shots);
            e.setActive();

            enemies = new List<Enemy>();
            enemies.Add(e);
        }



        //--------------------------------
        //----    Public functions    ----
        //--------------------------------
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            /*int i=0; // iterator for the list of enemies
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
            }*/
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void readRectangles()
        {
            int positionX1;
            int positionY1;
            int positionX2;
            int positionY2;

            XmlDocument lvl = XMLLvlMng.rect1;

            XmlNodeList level = lvl.GetElementsByTagName("level");

            XmlNodeList listaRectangles =
                        ((XmlElement)level[0]).GetElementsByTagName("rectangle");
                foreach (XmlElement nodo in listaRectangles)
                {

                    XmlAttributeCollection RectangleN = nodo.Attributes;


                    positionX1 = (int)Convert.ToDouble(RectangleN[0].Value);
                    positionY1 = (int)Convert.ToDouble(RectangleN[1].Value);
                    positionX2 = (int)Convert.ToDouble(RectangleN[2].Value);
                    positionY2 = (int)Convert.ToDouble(RectangleN[3].Value);

                    if (positionX1==0 & positionY1==0){ // detectamos el primer rectangulo de un nuevo layer
                        listRectCollider.Add(new List<Rectangle>());
                    }
                    Rectangle rectangulo = new Rectangle(positionX1, positionY1, positionX2, positionY2);
                    listRectCollider[listRectCollider.Count-1].Add(rectangulo); // añadimos a la lista actual los rectangulos
                    

                }
        }

        //devuelve la lista de rectangulos de colisión del parallax donde se juega
        public List<List<Rectangle>> getRectangles()
        {
            return listRectCollider;
        }

        //devuelve la lista de enemigos del nivel
        public List<Enemy> getEnemies()
        {
            return enemies;
        }

    } // class LevelB
}
