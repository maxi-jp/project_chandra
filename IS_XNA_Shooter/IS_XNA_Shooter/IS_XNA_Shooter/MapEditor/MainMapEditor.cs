using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter.MapEditor
{
    class MainMapEditor
    {
        Level level;
        String levelType;
        int width;
        int height;

        Texture2D whitePixel;
        Texture2D textureCell;

        Texture2D backgroundLevel;
        Sprite background;

        private Vector2 displacementLevel;

        
        
        public MainMapEditor(String levelType, int width, int height) {

            this.levelType = levelType;
          /*  this.height = width;
            this.width = height;*/
            this.height = 1040;
            this.width = 1040;

            //Malla de Texturas
            whitePixel = GRMng.whitepixel;
            textureCell = GRMng.textureCell;

            background = new Sprite(true, new Vector2(SuperGame.screenWidth/2, SuperGame.screenHeight/2), 0f, GRMng.textureBg00);

            displacementLevel = new Vector2(0,-500);
        }



    


        public void Update()
        {
 




        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(SuperGame.screenWidth / 20, SuperGame.screenHeight / 6, 1000, 500), Color.Black);
            // grid del suelo
            for (int i = SuperGame.screenWidth / 20 + 10 + (int)displacementLevel.X; i < width + SuperGame.screenWidth / 20 + 10 + (int)displacementLevel.X; i += textureCell.Width)
                for (int j = SuperGame.screenHeight / 6 + 10 + (int)displacementLevel.Y; j < height + SuperGame.screenHeight / 6 + 10 + (int)displacementLevel.Y; j += textureCell.Height)
                {   int widthSourceRectangle = 1000 + SuperGame.screenWidth / 20 - i;
                    int heightSourceRectangle = 500 + SuperGame.screenHeight / 6 - j;
                    if (widthSourceRectangle > 80) widthSourceRectangle = 80;
                    if (heightSourceRectangle > 80) heightSourceRectangle = 80;
                    if (widthSourceRectangle < 0) widthSourceRectangle = 0;
                    if (heightSourceRectangle < 0) heightSourceRectangle = 0;
                    spriteBatch.Draw(textureCell, new Vector2(i, j), new Rectangle(0, 0, widthSourceRectangle, heightSourceRectangle), Color.White);
                } 
                
            

            int origXNatLeft = SuperGame.screenWidth / 20 + 10 + (int)displacementLevel.X;
            int origXNatRight = origXNatLeft + width;
            int origYNatUp = SuperGame.screenHeight / 6 + 10 + (int)displacementLevel.Y;
            int origYNatDown = origYNatUp + height;

            int origXScreen = SuperGame.screenWidth / 20;
            int origYScreen = SuperGame.screenHeight / 6;
            int endXScreen = SuperGame.screenWidth / 20 + 1000;
            int endYScreen = SuperGame.screenHeight / 6 + 500;

            int origenXRealLeft = Math.Max(origXNatLeft, origXScreen);
            int origenXRealRight = Math.Max(origXNatRight, origXScreen);
            int origenYRealUp = Math.Max(origYNatUp, origYScreen);
            int origenYRealDown = Math.Max(origYNatDown, origYScreen);

            int tamWidthExtra = origXScreen - origXNatLeft;
            if (tamWidthExtra < 0) tamWidthExtra = 0;
            int tamHeightExtra = origYScreen - origYNatUp;
            if (tamHeightExtra < 0) tamHeightExtra = 0;

            // linea de arriba:
            if ( -origYScreen + origYNatUp >= 0)
                spriteBatch.Draw(whitePixel, new Rectangle(origenXRealLeft, origenYRealUp,
                    Math.Min(endXScreen - origXNatLeft - tamWidthExtra, width - tamWidthExtra), 1), 
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);


            // linea de la derecha:
            if (-endXScreen + origXNatRight <= 0)
            spriteBatch.Draw(whitePixel, new Rectangle(origenXRealRight, origenYRealUp,
                1, Math.Min(endYScreen - origYNatUp - tamHeightExtra, height - tamHeightExtra)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);


            // linea de abajo:
            if (-endYScreen + origYNatDown <= 0)
                spriteBatch.Draw(whitePixel, new Rectangle(origenXRealLeft, origenYRealDown,
                    Math.Min(endXScreen - origXNatLeft - tamWidthExtra, width - tamWidthExtra), 1), 
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);


            // linea de la izquierda:
                if ( -origXScreen + origXNatLeft >= 0)
                spriteBatch.Draw(whitePixel, new Rectangle(origenXRealLeft, origenYRealUp,
                    1, Math.Min(endYScreen - origYNatUp - tamHeightExtra, height - tamHeightExtra)), 
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }


        private void addEnemies(Enemy enemy)
        {
           /*   // EnemyWeak:
            if (ControlMng.f1Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyWeakA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyWeakShot:
            if (ControlMng.f2Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyWeakShotA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyBeamA:
            if (ControlMng.f3Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyBeamA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // EnemyMineShotA
            if (ControlMng.f4Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyMineShotA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyLaserA
            if (ControlMng.f5Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyLaserA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // EnemyScaredA
            if (ControlMng.f6Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyMineShotA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f7Preshed)
            {
                enemy = new FinalBoss1Turret1(camera, this, new Vector2(60, 60), ship);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f8Preshed)
            {
                enemy = new FinalBoss1Turret2(camera, this, new Vector2(60, 60), ship);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f9Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("FinalBossHeroe1", camera, this, ship, new Vector2(60, 60), 0);
                ((FinalBossHeroe1)enemy).SetEnemies(enemies);
                enemies.Add(enemy);
            }

            if (ControlMng.f10Preshed)
            {
                pow1 = new PowerUp(camera,this,new Vector2(40,40),0,GRMng.powerTexture,80,80,3,new short[]{6,6,6},new bool[]{true,true,true},SuperGame.frameTime12,0);
                pow2 = new PowerUp(camera, this, new Vector2(40, 120), 0, GRMng.powerTexture, 80, 80, 3, new short[] { 6, 6, 6 }, new bool[] { true, true, true }, SuperGame.frameTime12, 1);
                pow3 = new PowerUp(camera, this, new Vector2(40, 200), 0, GRMng.powerTexture, 80, 80, 3, new short[] { 6, 6, 6 }, new bool[] { true, true, true }, SuperGame.frameTime12, 2);

            }

            if (house != null && enemy != null)
                ((EnemyADefense)(enemy)).SetBase(house);

        } // TestEnemies*/

 

  
    }


}
}
