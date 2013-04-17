using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;

namespace IS_XNA_Shooter
{
    class Level
    {
        protected Camera camera;
        protected Ship ship;
        protected Vector2 ShipInitPosition;
        public int width;
        public int height;
        protected List<Enemy> enemies;
        protected int numLevel;
        protected bool levelFinished = false;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Level() 
        { 
            //Lo ponemos para que compile LevelB ya que no tiene camara y demas atributos.
        }

        public Level(Camera camera, int num, List<Enemy> enemies)
        {
            this.camera = camera;
            this.numLevel = num;
            this.enemies = enemies;
        }

        public virtual void Update(float deltaTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

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
                XmlDocument lvl=null;
                switch (modoDeJuego)
                {
                    case 0: // game mode A
                        lvl = XMLLvlMng.lvl1A;
                        break;
                    case 1: // game mode B
                        lvl = XMLLvlMng.lvl1B;
                        break;

                    case 2: //game mode C
                        lvl = XMLLvlMng.lvl1C;
                        break;
                
                }
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
                    time = (float)Convert.ToDouble(enemyN[3].Value); 
                    //timeLeftEnemy.Add(time);

                    Enemy enemy = null;

                    if (enemyType.Equals("enemyWeakA"))
                        enemy = new EnemyWeakA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW1, GRMng.frameHeightEW1, GRMng.numAnimsEW1, GRMng.frameCountEW1,
                            GRMng.loopingEW1, SuperGame.frameTime12, GRMng.textureEW1, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyBeam"))
                        enemy = new EnemyBeamA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEB1, GRMng.frameHeightEB1, GRMng.numAnimsEB1, GRMng.frameCountEB1,
                            GRMng.loopingEB1, SuperGame.frameTime12, GRMng.textureEB1, time, 1000, 100,
                            4, null);
                    else if (enemyType.Equals("enemyWeakShotA"))
                        enemy = new EnemyWeakShotA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyMineShotA"))
                        enemy = new EnemyMineShotA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEMS, GRMng.frameHeightEMS, GRMng.numAnimsEMS, GRMng.frameCountEMS,
                            GRMng.loopingEMS, SuperGame.frameTime12, GRMng.textureEMS, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyLaserA"))
                        enemy = new EnemyLaserA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEL, GRMng.frameHeightEL, GRMng.numAnimsEL, GRMng.frameCountEL,
                            GRMng.loopingEL, SuperGame.frameTime12, GRMng.textureEL, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyScaredA"))
                        enemy = new EnemyScaredA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthES, GRMng.frameHeightES, GRMng.numAnimsES, GRMng.frameCountES,
                            GRMng.loopingES, SuperGame.frameTime12, GRMng.textureES, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyWeakB"))
                        enemy = new EnemyWeakB(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW1, GRMng.frameHeightEW1, GRMng.numAnimsEW1, GRMng.frameCountEW1,
                            GRMng.loopingEW1, SuperGame.frameTime12, GRMng.textureEW1, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyWeakShotB"))
                        enemy = new EnemyWeakShotB(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyMineShotB"))
                        enemy = new EnemyMineShotB(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEMS, GRMng.frameHeightEMS, GRMng.numAnimsEMS, GRMng.frameCountEMS,
                            GRMng.loopingEMS, SuperGame.frameTime12, GRMng.textureEMS, time, 100, 100,
                            1, null);
                    else if (enemyType.Equals("enemyLaserB"))
                        enemy = new EnemyLaserB(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEL, GRMng.frameHeightEL, GRMng.numAnimsEL, GRMng.frameCountEL,
                            GRMng.loopingEL, SuperGame.frameTime12, GRMng.textureEL, time, 100, 100,
                            1, null);

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

        public virtual void setShip(Ship ship)
        {
            this.ship = ship;
            foreach (Enemy e in enemies)// enemigos
                e.SetShip(ship);
             ship.SetPosition(ShipInitPosition);
        }

        // This method returns true when the level is finished.
        public bool getFinish()
        {
            return levelFinished;
        }

        public XMLLvlMng LvlMng { get; set; }

    } // class Level
}