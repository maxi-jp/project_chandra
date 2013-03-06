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
        /// <summary>
        /// Number of level
        /// </summary>
        private int numLevel;
        /// <summary>
        /// A list of collider rectangles from the level
        /// </summary>
        private List<List<Rectangle>> listRectCollider;

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Builds the level B
        /// </summary>
        /// <param name="camera"> Camera </param>
        /// <param name="numLevel"> Number of level </param>
        public LevelB(Camera camera, int numLevel)
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

            Vector2 position = new Vector2(SuperGame.screenWidth - GRMng.frameWidthFB1 / 2, 
                                           SuperGame.screenHeight / 2 - GRMng.frameHeightFB1 / 2);
            Enemy e = new FinalBoss1(camera, this, position, 0, GRMng.frameWidthFB1, GRMng.frameHeightFB1, 
                GRMng.numAnimsFB1, GRMng.frameCountFB1, GRMng.loopingFB1, SuperGame.frameTime12, GRMng.textureFB1, -50, 100, 1, null);
            e.setActive();
            enemies = new List<Enemy>();
            enemies.Add(e);
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the enemies in the level B
        /// </summary>
        /// <param name="deltaTime"> Time between last time and this </param>
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

        /// <summary>
        /// Draw the enemies in the level B
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Read the list of rectangles in the file .xml
        /// </summary>
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

        /// <summary>
        /// Get a list of rectangles to collision in the game B
        /// </summary>
        /// <returns></returns>
        public List<List<Rectangle>> getRectangles()
        {
            return listRectCollider;
        }

        /// <summary>
        /// Get a list of enemies
        /// </summary>
        /// <returns></returns>
        public List<Enemy> getEnemies()
        {
            return enemies;
        }

    } // class LevelB
}
