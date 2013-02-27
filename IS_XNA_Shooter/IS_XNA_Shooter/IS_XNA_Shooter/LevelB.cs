using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class LevelB : Level
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------
        private int numLevel;   //número de nivel
        //private Camera camera;  //cámara
        private List<Rectangle> listRectCollider;



        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public LevelB(Camera camera, int numLevel)
            : base()
        {
            this.numLevel = numLevel;
            this.enemies = new List<Enemy>();
            this.camera = camera;
            width = SuperGame.screenWidth*2;
            height = SuperGame.screenHeight;
            timeLeftEnemy = new List<float>();
            listRectCollider = new List<Rectangle>();

            //Enemigo
            Enemy e1 = new EnemyWeakB(camera, this, new Vector2(SuperGame.screenWidth + 100, 
                new Random().Next(SuperGame.screenHeight)), (float)Math.PI, GRMng.frameWidthEW2, 
                GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2, GRMng.loopingEW2, SuperGame.frameTime12, 
                GRMng.textureEW2, -200, 100, null);
            e1.setActive();
            enemies = new List<Enemy>();
            enemies.Add(e1);
        }



        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        //devuelve la lista de rectangulos de colisión del parallax donde se juega
        public List<Rectangle> getRectangles()
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
