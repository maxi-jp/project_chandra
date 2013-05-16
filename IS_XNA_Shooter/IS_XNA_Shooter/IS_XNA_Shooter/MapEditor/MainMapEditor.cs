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
        /// <summary>
        /// 
        /// </summary>
        Level level;
        /// <summary>
        /// 
        /// </summary>
        String levelType;
        /// <summary>
        /// 
        /// </summary>
        int width;
        /// <summary>
        /// 
        /// </summary>
        int height;
        /// <summary>
        /// 
        /// </summary>
        Texture2D whitePixel;
        /// <summary>
        /// 
        /// </summary>
        Texture2D textureCell;
        /// <summary>
        /// 
        /// </summary>
        Texture2D backgroundLevel;
        /// <summary>
        /// 
        /// </summary>
        Sprite background;

        private SuperGame mainGame;
        private Vector2 displacementLevel;
        private Vector2 lastPositionMouse;
        /// <summary>
        /// 
        /// </summary>
        SpriteFont spriteDebug;

        //Frame (player and enemies)
        private Rectangle rectFrameEnemies;

        //Attribute for frame (player and anemies)
        //player
        private Rectangle positionPA1Frame;
        private Rectangle animPA1Frame;

        //enemies
        private Rectangle positionEW1Frame;
        private Rectangle animEW1Frame;

        private Rectangle positionEW2Frame;
        private Rectangle animEW2Frame;

        private Rectangle positionEB1Frame;
        private Rectangle animEB1Frame;

        private Rectangle positionESFrame;
        private Rectangle animESFrame;

        private Rectangle positionEMSFrame;
        private Rectangle animEMSFrame;

        private Rectangle positionELFrame;
        private Rectangle animELFrame;

        //Attributes for transition (from frame to map)
        //player
        private Vector2 positionPA1Transition;
        private Rectangle animPA1Transition;
        private bool isClickPA1;

        //enemies
        private Vector2 positionEW1Transition;
        private Rectangle animEW1Transition;
        private bool isClickEW1;

        private Vector2 positionEW2Transition;
        private Rectangle animEW2Transition;
        private bool isClickEW2;

        private Vector2 positionEB1Transition;
        private Rectangle animEB1Transition;
        private bool isClickEB1;

        private Vector2 positionESTransition;
        private Rectangle animESTransition;
        private bool isClickES;

        private Vector2 positionEMSTransition;
        private Rectangle animEMSTransition;
        private bool isClickEMS;

        private Vector2 positionELTransition;
        private Rectangle animELTransition;
        private bool isClickEL;
        
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

            //local attributes
            float xOrig = SuperGame.screenWidth - SuperGame.screenWidth / 10;
            float yOrig = SuperGame.screenHeight / 20;
            float yFinal = SuperGame.screenHeight - SuperGame.screenHeight / 10;

            //rectangle for frame (player and enemies)
            rectFrameEnemies = new Rectangle((int) xOrig, (int) yOrig, (int) GRMng.frameWidthEW1, (int) yFinal);

            //initialize positions and anims (frame player and enemies)
            initFrame(xOrig, yOrig);

            //initialize positions and anims (transition from frame to map)
            initTransition();
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

            updateFrameEnemiesTransition();
        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
            int origXNatLeft = SuperGame.screenWidth / 20 + 10 + (int)displacementLevel.X;
            int origXNatRight = origXNatLeft + width;
            int origYNatUp = SuperGame.screenHeight / 15 + 10 + (int)displacementLevel.Y;
            int origYNatDown = origYNatUp + height;

            int origXScreen = SuperGame.screenWidth / 20;
            int origYScreen = SuperGame.screenHeight / 15;
            int endXScreen = SuperGame.screenWidth / 20 + 1000;
            int endYScreen = SuperGame.screenHeight / 15 + 500;

            int origenXRealLeft = Math.Max(origXNatLeft, origXScreen);
            int origenXRealRight = Math.Max(origXNatRight, origXScreen);
            int origenYRealUp = Math.Max(origYNatUp, origYScreen);
            int origenYRealDown = Math.Max(origYNatDown, origYScreen);

            int tamWidthExtra = origXScreen - origXNatLeft;
            if (tamWidthExtra < 0) tamWidthExtra = 0;
            int tamHeightExtra = origYScreen - origYNatUp;
            if (tamHeightExtra < 0) tamHeightExtra = 0;

            background.Draw(spriteBatch);
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(origXScreen, origYScreen, 1000, 500), Color.Black);
            // grid del suelo
            for (int i = origXNatLeft; i < width + origXNatLeft; i += textureCell.Width)
                for (int j = origYNatUp; j < height + origYNatUp; j += textureCell.Height)
                {
                    int widthSourceRectangle = endXScreen - i;
                    int heightSourceRectangle = endYScreen - j;
                    if (widthSourceRectangle > 80) widthSourceRectangle = 80;
                    if (heightSourceRectangle > 80) heightSourceRectangle = 80;
                    if (widthSourceRectangle < 0) widthSourceRectangle = 0;
                    if (heightSourceRectangle < 0) heightSourceRectangle = 0;

                    // if the rectangle isn't visible.
                    if (i <= origXScreen && i + 80 <= origXScreen || j <= origYScreen && j + 80 <= origYScreen) { }
                    //if the rectangle is some move up and some move left.
                    else if (i <= origXScreen && i + 80 >= origXScreen && j <= origYScreen && j + 80 >= origYScreen)
                    {
                        spriteBatch.Draw(textureCell, new Vector2(origXScreen, origYScreen),
                            new Rectangle(origXScreen - i, origYScreen - j, 80 - (origXScreen - i), 80 - (origYScreen - j)), Color.White);
                    }
                    //if the rectangle is some move left.
                    else if (i <= origXScreen && i + 80 >= origXScreen)
                    {
                        spriteBatch.Draw(textureCell, new Vector2(origXScreen, j),
                            new Rectangle(origXScreen - i, 0, 80 - (origXScreen - i), heightSourceRectangle), Color.White);
                    }
                    //if the rectangle is some move up.
                    else if (j <= origYScreen && j + 80 >= origYScreen)
                    {
                        spriteBatch.Draw(textureCell, new Vector2(i, origYScreen),
                            new Rectangle(0, origYScreen - j, widthSourceRectangle, 80 - (origYScreen - j)), Color.White);
                    }
                    //if the rectangle is visible.
                    else
                    {
                        spriteBatch.Draw(textureCell, new Vector2(i, j), new Rectangle(0, 0, widthSourceRectangle, heightSourceRectangle), Color.White);
                    }
                } 

            drawFrameEnemies(spriteBatch);
            drawFrameProperties(spriteBatch);
            drawEnemiesTransition(spriteBatch);

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

        private void initFrame(float xOrig, float yOrig)
        {
            int acum = 0;
            //player
            positionPA1Frame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthPA1, GRMng.frameHeightPA1);
            animPA1Frame = new Rectangle(0, 0, GRMng.frameWidthPA1, GRMng.frameHeightPA1);
            acum = acum + GRMng.frameHeightPA1;

            //enemies
            positionEW1Frame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthEW1, GRMng.frameHeightEW1);
            animEW1Frame = new Rectangle(0, 0, GRMng.frameWidthEW1, GRMng.frameHeightEW1);
            acum = acum + GRMng.frameHeightEW1;

            positionEW2Frame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthEW2, GRMng.frameHeightEW2);
            animEW2Frame = new Rectangle(0, 0, GRMng.frameWidthEW2, GRMng.frameHeightEW2);
            acum = acum + GRMng.frameHeightEW2;

            positionEB1Frame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthEB1, GRMng.frameHeightEB1);
            animEB1Frame = new Rectangle(0, 0, GRMng.frameWidthEB1, GRMng.frameHeightEB1);
            acum = acum + GRMng.frameHeightEB1;

            positionESFrame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthES, GRMng.frameHeightES);
            animESFrame = new Rectangle(0, 0, GRMng.frameWidthES, GRMng.frameHeightES);
            acum = acum + GRMng.frameHeightES;

            positionEMSFrame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthEMS, GRMng.frameHeightEMS);
            animEMSFrame = new Rectangle(0, 0, GRMng.frameWidthEMS, GRMng.frameHeightEMS);
            acum = acum + GRMng.frameHeightEMS;

            positionELFrame = new Rectangle((int)xOrig, (int)yOrig + acum, GRMng.frameWidthEL, GRMng.frameHeightEL);
            animELFrame = new Rectangle(0, 0, GRMng.frameWidthEL, GRMng.frameHeightEL);
            acum = acum + GRMng.frameHeightEL;
        }

        private void initTransition()
        {
            //player
            positionPA1Transition = new Vector2();
            animPA1Transition = new Rectangle(0, 0, GRMng.frameWidthPA1, GRMng.frameHeightPA1);
            isClickPA1 = false;

            //enemies
            positionEW1Transition = new Vector2();
            animEW1Transition = new Rectangle(0, 0, GRMng.frameWidthEW1, GRMng.frameHeightEW1);
            isClickEW1 = false;

            positionEW2Transition = new Vector2();
            animEW2Transition = new Rectangle(0, 0, GRMng.frameWidthEW2, GRMng.frameHeightEW2);
            isClickEW2 = false;

            positionEB1Transition = new Vector2();
            animEB1Transition = new Rectangle(0, 0, GRMng.frameWidthEB1, GRMng.frameHeightEB1);
            isClickEB1 = false;

            positionESTransition = new Vector2();
            animESTransition = new Rectangle(0, 0, GRMng.frameWidthES, GRMng.frameHeightES);
            isClickES = false;

            positionEMSTransition = new Vector2();
            animEMSTransition = new Rectangle(0, 0, GRMng.frameWidthEMS, GRMng.frameHeightEMS);
            isClickEMS = false;

            positionELTransition = new Vector2();
            animELTransition = new Rectangle(0, 0, GRMng.frameWidthEL, GRMng.frameHeightEL);
            isClickEL = false;
        }

        private void drawFrameEnemies(SpriteBatch spriteBatch)
        {
            //draw frame
            spriteBatch.Draw(GRMng.blackpixeltrans, rectFrameEnemies, Color.Black);
            //draw player and enemies over the frame
            //player
            spriteBatch.Draw(GRMng.texturePA1, positionPA1Frame, animPA1Frame, Color.White);
            //enemies
            spriteBatch.Draw(GRMng.textureEW1, positionEW1Frame, animEW1Frame, Color.White);
            spriteBatch.Draw(GRMng.textureEW2, positionEW2Frame, animEW2Frame, Color.White);
            spriteBatch.Draw(GRMng.textureEB1, positionEB1Frame, animEB1Frame, Color.White);
            spriteBatch.Draw(GRMng.textureES, positionESFrame, animESFrame, Color.White);
            spriteBatch.Draw(GRMng.textureEMS, positionEMSFrame, animEMSFrame, Color.White);
            spriteBatch.Draw(GRMng.textureEL, positionELFrame, animELFrame, Color.White);
        }
        
        private void drawEnemiesTransition(SpriteBatch spriteBatch)
        {
            //player
            if (isClickPA1)
                spriteBatch.Draw(GRMng.texturePA1, positionPA1Transition, animPA1Transition, Color.White);
            //enemies
            if (isClickEW1)
                spriteBatch.Draw(GRMng.textureEW1, positionEW1Transition, animEW1Transition, Color.White);
            if (isClickEW2)
                spriteBatch.Draw(GRMng.textureEW2, positionEW2Transition, animEW2Transition, Color.White);
            if (isClickEB1)
                spriteBatch.Draw(GRMng.textureEB1, positionEB1Transition, animEB1Transition, Color.White);
            if (isClickES)
                spriteBatch.Draw(GRMng.textureES, positionESTransition, animESTransition, Color.White);
            if (isClickEMS)
                spriteBatch.Draw(GRMng.textureEMS, positionEMSTransition, animEMSTransition, Color.White);
            if (isClickEL)
                spriteBatch.Draw(GRMng.textureEL, positionELTransition, animELTransition, Color.White);
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

        private void updateFrameEnemiesTransition()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int xMouse = Mouse.GetState().X;
                int yMouse = Mouse.GetState().Y;

                if (isClickPA1)
                {
                    positionPA1Transition.X = xMouse - GRMng.frameWidthPA1 / 2;
                    positionPA1Transition.Y = yMouse - GRMng.frameHeightPA1 / 2;
                }
                else if (isClickEW1)
                {
                    positionEW1Transition.X = xMouse - GRMng.frameWidthEW1 / 2;
                    positionEW1Transition.Y = yMouse - GRMng.frameHeightEW1 / 2;
                }
                else if (isClickEW2)
                {
                    positionEW2Transition.X = xMouse - GRMng.frameWidthEW2 / 2;
                    positionEW2Transition.Y = yMouse - GRMng.frameHeightEW2 / 2;
                }
                else if (isClickEB1)
                {
                    positionEB1Transition.X = xMouse - GRMng.frameWidthEB1 / 2;
                    positionEB1Transition.Y = yMouse - GRMng.frameHeightEB1 / 2;
                }
                else if (isClickES)
                {
                    positionESTransition.X = xMouse - GRMng.frameWidthES / 2;
                    positionESTransition.Y = yMouse - GRMng.frameHeightES / 2;
                }
                else if (isClickEMS)
                {
                    positionEMSTransition.X = xMouse - GRMng.frameWidthEMS / 2;
                    positionEMSTransition.Y = yMouse - GRMng.frameHeightEMS / 2;
                }
                else if (isClickEL)
                {
                    positionELTransition.X = xMouse - GRMng.frameWidthEL / 2;
                    positionELTransition.Y = yMouse - GRMng.frameHeightEL / 2;
                }
                else
                {
                    if (positionPA1Frame.Contains(xMouse, yMouse))
                    {
                        positionPA1Transition.X = xMouse - GRMng.frameWidthPA1 / 2;
                        positionPA1Transition.Y = yMouse - GRMng.frameHeightPA1 / 2;
                        isClickPA1 = true;
                    }
                    else if (positionEW1Frame.Contains(xMouse, yMouse))
                    {
                        positionEW1Transition.X = xMouse - GRMng.frameWidthEW1 / 2;
                        positionEW1Transition.Y = yMouse - GRMng.frameHeightEW1 / 2;
                        isClickEW1 = true;
                    }
                    else if (positionEW2Frame.Contains(xMouse, yMouse))
                    {
                        positionEW2Transition.X = xMouse - GRMng.frameWidthEW2 / 2;
                        positionEW2Transition.Y = yMouse - GRMng.frameHeightEW2 / 2;
                        isClickEW2 = true;
                    }
                    else if (positionEB1Frame.Contains(xMouse, yMouse))
                    {
                        positionEB1Transition.X = xMouse - GRMng.frameWidthEB1 / 2;
                        positionEB1Transition.Y = yMouse - GRMng.frameHeightEB1 / 2;
                        isClickEB1 = true;
                    }
                    else if (positionEMSFrame.Contains(xMouse, yMouse))
                    {
                        positionEMSTransition.X = xMouse - GRMng.frameWidthEMS / 2;
                        positionEMSTransition.Y = yMouse - GRMng.frameHeightEMS / 2;
                        isClickEMS = true;
                    }
                    else if (positionELFrame.Contains(xMouse, yMouse))
                    {
                        positionELTransition.X = xMouse - GRMng.frameWidthEL / 2;
                        positionELTransition.Y = yMouse - GRMng.frameHeightEL / 2;
                        isClickEL = true;
                    }
                    else if (positionESFrame.Contains(xMouse, yMouse))
                    {
                        positionESTransition.X = xMouse - GRMng.frameWidthES / 2;
                        positionESTransition.Y = yMouse - GRMng.frameHeightES / 2;
                        isClickES = true;
                    }
                }
            }
            else
            {
                initTransition();
            }
        }
    }
}
