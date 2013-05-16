using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        private SuperGame mainGame;
        private Vector2 displacementLevel;
        private Vector2 lastPositionMouse;

        SpriteFont spriteDebug;

        
        
        public MainMapEditor(String levelType, int width, int height, SuperGame mainGame) {
            spriteDebug = GRMng.fontText;

            this.levelType = levelType;
          /*  this.height = width;
            this.width = height;*/
            this.height = 1040;
            this.width = 1040;

            this.mainGame = mainGame;

            //Malla de Texturas
            whitePixel = GRMng.whitepixel;
            textureCell = GRMng.textureCell;

            background = new Sprite(true, new Vector2(SuperGame.screenWidth/2, SuperGame.screenHeight/2), 0f, GRMng.textureBg00);

            displacementLevel = new Vector2(0, 0);
            lastPositionMouse = new Vector2();
        }



    


        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                displacementLevel.X += Mouse.GetState().X - lastPositionMouse.X;
                displacementLevel.Y += Mouse.GetState().Y - lastPositionMouse.Y;
                if (displacementLevel.X > 0) displacementLevel.X = 0;
                if (displacementLevel.X < 1000 - width - 20) displacementLevel.X = 1000 - width - 20;

                if (displacementLevel.Y > 0) displacementLevel.Y = 0;
                if (displacementLevel.Y < 500 - height - 20) displacementLevel.Y = 500 - height - 20;
            }
            lastPositionMouse.X = Mouse.GetState().X;
            lastPositionMouse.Y = Mouse.GetState().Y;
        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
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

            background.Draw(spriteBatch);
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(SuperGame.screenWidth / 20, SuperGame.screenHeight / 6, 1000, 500), Color.Black);
            // grid del suelo
            for (int i = origXNatLeft; i < width + origXNatLeft; i += textureCell.Width)
                for (int j = origYNatUp; j < height + origYNatUp; j += textureCell.Height)
                {
                    int widthSourceRectangle = endXScreen - i;
                    int heightSourceRectangle = endYScreen - j;

                    if (i < origXScreen && i + 80 > origXScreen || j < origYScreen && j + 80 > origYScreen)
                    {
                        int a = (int)(i - displacementLevel.X) % 80;
                        int b = (int)(j - displacementLevel.Y) % 80;
                        spriteBatch.Draw(textureCell, new Vector2(i - displacementLevel.X, j - displacementLevel.Y), new Rectangle(a, b, widthSourceRectangle, heightSourceRectangle), Color.White);
                    }
                    else if (i <= origXScreen && i + 80 <= origXScreen || j <= origYScreen && j + 80 <= origYScreen) { }
                    else
                    {


                        if (widthSourceRectangle > 80) widthSourceRectangle = 80;
                        if (heightSourceRectangle > 80) heightSourceRectangle = 80;
                        if (widthSourceRectangle < 0) widthSourceRectangle = 0;
                        if (heightSourceRectangle < 0) heightSourceRectangle = 0;
                        spriteBatch.Draw(textureCell, new Vector2(i, j), new Rectangle(0, 0, widthSourceRectangle, heightSourceRectangle), Color.White);
                    }
                } 

            drawFrameEnemies(spriteBatch);

            drawFrameProperties(spriteBatch);

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

            spriteBatch.DrawString(spriteDebug, "(" + displacementLevel.X + ", " + displacementLevel.Y + ")", Vector2.Zero, Color.White);
        }

        private void drawFrameEnemies(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10, GRMng.frameWidthEW1, SuperGame.screenHeight - 10), Color.Black);
            mainGame.loadGRManager("LevelA1");

            int acum = 0;
            spriteBatch.Draw(GRMng.textureEW1, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10, GRMng.frameWidthEW1 +acum, GRMng.frameHeightEW1), new Rectangle(0, 0, GRMng.frameWidthEW1, GRMng.frameHeightEW1), Color.White);
            acum = acum + GRMng.frameHeightEW1;
            spriteBatch.Draw(GRMng.textureEW2, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10 + acum, GRMng.frameWidthEW2, GRMng.frameHeightEW2), new Rectangle(0, 0, GRMng.frameWidthEW2, GRMng.frameHeightEW2), Color.White);
            acum = acum + GRMng.frameHeightEW2;
            spriteBatch.Draw(GRMng.textureEB1, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10 + acum, GRMng.frameWidthEB1, GRMng.frameHeightEB1), new Rectangle(0, 0, GRMng.frameWidthEB1, GRMng.frameHeightEB1), Color.White);
            acum = acum + GRMng.frameHeightEB1;
            spriteBatch.Draw(GRMng.textureES, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10 + acum, GRMng.frameWidthES, GRMng.frameHeightES), new Rectangle(0, 0, GRMng.frameWidthES, GRMng.frameHeightES), Color.White);
            acum = acum + GRMng.frameHeightES;
            spriteBatch.Draw(GRMng.textureEMS, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10 + acum, GRMng.frameWidthEMS, GRMng.frameHeightEMS), new Rectangle(0, 0, GRMng.frameWidthEMS, GRMng.frameHeightEMS), Color.White);
            acum = acum + GRMng.frameHeightEMS;
            spriteBatch.Draw(GRMng.textureEL, new Rectangle(SuperGame.screenWidth - SuperGame.screenWidth / 10, 10 + acum, GRMng.frameWidthEL, GRMng.frameHeightEL), new Rectangle(0, 0, GRMng.frameWidthEL, GRMng.frameHeightEL), Color.White);
            acum = acum + GRMng.frameHeightEL;
            

        }

        private void drawFrameProperties(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(SuperGame.screenWidth / 20, SuperGame.screenHeight - SuperGame.screenHeight / 6, 1000, 100), Color.Black);
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
