using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class represents a level with the ship of the player, the enemies...
    /// </summary>
    class Level
    {
        /// <summary>
        /// This is game's camera
        /// </summary>
        protected Camera camera;
        /// <summary>
        /// This is our ship
        /// </summary>
        protected Ship Ship;
        /// <summary>
        /// This is the initial position of our ship
        /// </summary>
        protected Vector2 ShipInitPosition;
        /// <summary>
        /// Theese are the width and the height of the level
        /// </summary>
        public int width, height;
        /// <summary>
        /// This is a list with the enemies of the level
        /// </summary>
        protected List<Enemy> enemies;
        /// <summary>
        /// This is a list with the time in which the enemies are created
        /// </summary>
        protected List<float> timeLeftEnemy;
        /// <summary>
        /// This tell us if the level has finished or not
        /// </summary>
        protected bool levelFinished = false;

        /* ------------------- BUILDERS ------------------- */

        /// <summary>
        /// An empty builder
        /// </summary>
        public Level() 
        {
        }

        /// <summary>
        /// A builder with a camera and a list of enemies
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="num"> This is the number of the level (1, 2, 3 ...) </param>
        /// <param name="enemies"></param>
        public Level(Camera camera, int num, List<Enemy> enemies)
        {
        }

        /* ------------------- FUNCTIONS ------------------- */

        /// <summary>
        /// This function updates the state of the level
        /// </summary>
        /// <param name="deltaTime"> This is the time between the before and the current update </param>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < timeLeftEnemy.Count(); i++)
            {
                timeLeftEnemy[i] -= deltaTime;
                if (!enemies[i].isActive() && timeLeftEnemy[i] <= 0 && !enemies[i].isDead())
                    enemies[i].setActive();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modoDeJuego"></param>
        /// <param name="levelModo"></param>
 	    protected void LeerArchivoXML(int modoDeJuego, int levelModo)
        {
            // Utilizar nombres de fichero y nodos XML idénticos a los que se guardaron

            try
            {
                //  Leer los datos del archivo
                String enemyType;
                float positionX;
                float positionY;
                float time;
                XmlDocument lvl = XMLLvlMng.lvl1;
              
                XmlNodeList level = lvl.GetElementsByTagName("level");

                XmlNodeList lista =
                        ((XmlElement)level[0]).GetElementsByTagName("enemy");
                foreach (XmlElement nodo in lista)
                {

                    XmlAttributeCollection enemyN = nodo.Attributes;
                    //XmlAttribute a = enemyN[1];
                    enemyType = Convert.ToString(enemyN[0].Value);
                    positionX = (float)Convert.ToDouble(enemyN[1].Value);
                    positionY = (float)Convert.ToDouble(enemyN[2].Value);
                    time = (float)Convert.ToDouble(enemyN[3].Value); //aqui casca
                    timeLeftEnemy.Add(time);

                    Enemy enemy = null;

                    if (enemyType.Equals("enemyWeak"))
                        enemy = new EnemyWeak(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, 1, null);
                    else if (enemyType.Equals("enemyBeam"))
                        enemy = new EnemyBeamA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 1000, 100, 4, null);
                    enemies.Add(enemy);
                    
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
        }   //  end LeerArchivoXML()

        /// <summary>
        /// This function gives a value to the atribute ship.
        /// </summary>
        /// <param name="Ship"></param>
        public virtual void setShip(Ship Ship)
        {
            this.Ship = Ship;
            foreach (Enemy e in enemies)// enemigos
                e.setShip(Ship);
            Ship.SetPosition(ShipInitPosition);
        }

        /// <summary>
        /// This method returns true when the level is finished.
        /// </summary>
        /// <returns> True if the level has finished, and false in another case </returns>
        public bool getFinish()
        {
            return levelFinished;
        }

        /// <summary>
        /// 
        /// </summary>
        public XMLLvlMng LvlMng { get; set; }
    } // class Level
}