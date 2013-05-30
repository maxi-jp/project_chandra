using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;



namespace IS_XNA_Shooter.MapEditor
{
    class MainMapEditor
    {
        private enum stateMouse
        {
            normal,
            leftClick,
            leftUnclick,
            rigthClick,
            rigthUnlick
        }

        //******************************
        //*****     ATTRIBUTES     *****
        //****************************** 
        private String levelType;
        private Sprite background;
        private Level level;
        private SuperGame mainGame;
        private Vector2 lastPositionMouse;
        private SpriteFont spriteDebug;
	    private stateMouse currentStateMouse;
	    private bool isSelectedFrameLevel;
        private bool isSelectedFrameShips;
        private bool isSelectedEnemyShip;
        private bool isMovingShip;
        private List<InfoEnemy> enemiesInfo;
        private InfoEnemy enemySelected;
        private MenuItem itemMainQuit;
        private MenuMapEditor menuMapEditor;
        private String xmlPath;

    
        #region Frame Level
        private int width;
        private int height;
        private Texture2D whitePixel;
        private Texture2D textureCell;
        private Vector2 displacementLevel;
        private int widthFrameLevel;
        private int heightFrameLevel;

        private int separator;

        private int origXScreen;
        private int origYScreen;
        private int endXScreen;
        private int endYScreen;

        private int origXNatLeft; 
        private int origXNatRight; 
        private int origYNatUp;
        private int origYNatDown; 

        private int origenXRealLeft;
        private int origenXRealRight;
        private int origenYRealUp;
        private int origenYRealDown;

        private Rectangle rectangleFrameLevel;
        #endregion;  
        
        #region Frame Right Ships
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

        #endregion;        

        #region Frame properties
        private int xOrigProp, yOrigProp, xEndProp, yEndProp;
        private ItemInput inputProp;
        private Rectangle rectSave, rectLoad, rectPreview, rectAnimSave, rectAnimLoad, rectAnimPreview; 
        #endregion


        //***************************
        //*****     BUILDER     *****
        //***************************        
        public MainMapEditor(String levelType, int width, int height, SuperGame mainGame, MenuMapEditor menuMapEditor)
        {
	        currentStateMouse = stateMouse.normal;
	        //variables frameLevel
            widthFrameLevel = 1000;
            heightFrameLevel = 500;

            if (SuperGame.resolutionMode == 3)
            {
                widthFrameLevel = 800;
            }

            enemiesInfo = new List<InfoEnemy>();

            separator = 10;

            origXScreen = SuperGame.screenWidth / 20;
            origYScreen = SuperGame.screenHeight / 15;
            endXScreen = origXScreen + widthFrameLevel;
            endYScreen = origYScreen + heightFrameLevel;

            rectangleFrameLevel = new Rectangle(origXScreen, origYScreen, widthFrameLevel, heightFrameLevel);

            spriteDebug = GRMng.fontText;

            isSelectedFrameLevel = isSelectedFrameShips = isSelectedEnemyShip = isMovingShip = false;
            this.levelType = levelType;
          /*  this.height = width;
            this.width = height;*/
            this.height = height;
            this.width = width;

            this.mainGame = mainGame;
            this.menuMapEditor = menuMapEditor;

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

            //frame properties
            initProperties();

            //buttons save, load and preview
            initButtonsSaveLoadPreview();

            //button quit
            itemMainQuit = new MenuItem(false, new Vector2(SuperGame.screenWidth - 45, 5), GRMng.menuMain,
                new Rectangle(0, 360, 40, 40), new Rectangle(40, 360, 40, 40), new Rectangle(80, 360, 40, 40));
        }




        //************************************
        //*****     PUBLIC FUNCTIONS     *****
        //************************************ 
        public void Update()
        {
	        //update mouse
            updateMouse();
            
            //mouse normal state
            if (currentStateMouse == stateMouse.normal)
            {
                isSelectedFrameLevel = isSelectedFrameShips = isMovingShip = false;
            }

            //unclick mouse button left
            if (currentStateMouse == stateMouse.leftUnclick)
            {
                //Save the ships in the frame of maps level if the enemy ships is uncliked over the frame level
                if (isSelectedFrameShips)
                {
                    saveShip();
                    isSelectedFrameLevel = false;
                }
                //return to main menu
                if (itemMainQuit.Unclick(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    Audio.PlayEffect("digitalAcent01");
                    menuMapEditor.currentState = MenuMapEditor.State.SelectTypeGame;
                }

                isMovingShip = false;
            }

            //click mouse button left
            if (currentStateMouse == stateMouse.leftClick)
            {
                //frame level
                if (!isSelectedFrameLevel && !isSelectedFrameShips && rectangleFrameLevel.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    isSelectedFrameLevel = true;
                    unselectEnemy();
                }

                //click frame right
                if (!isSelectedFrameLevel && !isSelectedFrameShips && rectFrameEnemies.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    isSelectedFrameShips = true;
                    unselectEnemy();
                }

                //click one enemy in the frame level
                if (isSelectedFrameLevel && !isMovingShip)
                {
                    int numberEnemies = 0;
                    isSelectedEnemyShip = false;
                    while (numberEnemies < enemiesInfo.Count && !isSelectedEnemyShip)
                    {
                        Rectangle rectEnemyShip = new Rectangle(enemiesInfo[numberEnemies].positionX - enemiesInfo[numberEnemies].frameWidth / 2,
                         enemiesInfo[numberEnemies].positionY - enemiesInfo[numberEnemies].frameHeight / 2,
                         enemiesInfo[numberEnemies].frameWidth, enemiesInfo[numberEnemies].frameHeight);

                        if (rectEnemyShip.Contains(Mouse.GetState().X - (int)displacementLevel.X, Mouse.GetState().Y - (int)displacementLevel.Y))
                        {
                            isSelectedEnemyShip = true;
                            isMovingShip = true;
                            enemySelected = enemiesInfo[numberEnemies];
                            inputProp.setText(Convert.ToString(enemySelected.time));
                        }
                        numberEnemies++;
                    }
                }

                //move ship selected in the frame level
                if (isMovingShip)
                {
                    enemySelected.positionX = Mouse.GetState().X - (int)displacementLevel.X;
                    enemySelected.positionY = Mouse.GetState().Y - (int)displacementLevel.Y;
                    //limit the position
                    if (enemySelected.positionX < 10 + origXScreen + enemySelected.frameWidth / 2) enemySelected.positionX = 10 + origXScreen + enemySelected.frameWidth / 2;
                    if (enemySelected.positionX > 10 + origXScreen + width - enemySelected.frameWidth / 2) enemySelected.positionX = 10 + origXScreen + width - enemySelected.frameWidth / 2;
                    if (enemySelected.positionY < 10 + origYScreen + enemySelected.frameHeight / 2) enemySelected.positionY = 10 + origYScreen + enemySelected.frameHeight / 2;
                    if (enemySelected.positionY > 10 + origYScreen + height - enemySelected.frameHeight / 2) enemySelected.positionY = 10 + origYScreen + height - enemySelected.frameHeight / 2;
                }

                //button quit cliked
                itemMainQuit.Click(Mouse.GetState().X, Mouse.GetState().Y);
            }

            //unclick button rigth
            if (currentStateMouse == stateMouse.rigthUnlick)
            {
                int i = 0;
                bool contains = false;
                while (i < enemiesInfo.Count && !contains)
                {
                    Rectangle rectEnemyShip = new Rectangle(enemiesInfo[i].positionX - enemiesInfo[i].frameWidth / 2,
                     enemiesInfo[i].positionY - enemiesInfo[i].frameHeight / 2,
                     enemiesInfo[i].frameWidth, enemiesInfo[i].frameHeight);

                    if (rectEnemyShip.Contains(Mouse.GetState().X - (int)displacementLevel.X, Mouse.GetState().Y - (int)displacementLevel.Y))
                    {
                        contains = true;
                        isMovingShip = true;
                    }
                    i++;
                }

                if (contains)
                {
                    i--;
                    enemiesInfo.Remove(enemiesInfo[i]);
                }
            }

            //update the frame of level
            updateFrameLevel();
            //frame right to select enemies
            updateFrameEnemiesTransition();
            //buttons for save, load and preview a level
            updateButtonsSaveLoadPreview();
            //menu quit
            itemMainQuit.Update(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //background
            background.Draw(spriteBatch);
            // frame level
            drawFrameLevel(spriteBatch);
            // frame rigth to select ships
            drawFrameEnemies(spriteBatch);
            drawFrameProperties(spriteBatch);
            drawEnemiesTransition(spriteBatch);
            //properties of the enemies (time to spawn)
            drawPropertiesEnemies(spriteBatch);
            //buttons for save, load and preview a level
            drawButtonsSaveLoadPreview(spriteBatch);
            //menu quit
            itemMainQuit.Draw(spriteBatch);

            //debug
            spriteBatch.DrawString(spriteDebug, "(" + displacementLevel.X + ", " + displacementLevel.Y + ")", Vector2.Zero, Color.White);
        }




        //*************************************
        //*****     PRIVATE FUNCTIONS     *****
        //************************************* 
        private void initProperties()
        {
            xOrigProp = SuperGame.screenWidth / 20;
            yOrigProp = SuperGame.screenHeight - SuperGame.screenHeight / 6;
            xEndProp = SuperGame.screenWidth - SuperGame.screenWidth / 5;
            yEndProp = SuperGame.screenHeight - SuperGame.screenHeight / 20;
            inputProp = new ItemInput(new Vector2(xOrigProp + (xEndProp - xOrigProp) / 50, yEndProp - (yEndProp - yOrigProp) / 2), ItemInput.State.mapScreen);
        }

        private void initButtonsSaveLoadPreview()
        {
            int acum = SuperGame.screenWidth / 20;
            rectSave = new Rectangle(acum, 0, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
            acum += GRMng.buttonsSaveLoadPreview.Width + SuperGame.screenWidth / 30;
            rectLoad = new Rectangle(acum, 0, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
            acum += GRMng.buttonsSaveLoadPreview.Width + SuperGame.screenWidth / 30;
            rectPreview = new Rectangle(acum, 0, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);

            rectAnimSave = new Rectangle(0, 0, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
            rectAnimLoad = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
            rectAnimPreview = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview * 2, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
        }

        private void updateMouse()
        {
            if (currentStateMouse == stateMouse.normal)
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.leftClick;
                else if (Mouse.GetState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.rigthClick;
            }
            else if (currentStateMouse == stateMouse.leftClick)
            {
                if (Mouse.GetState().LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.leftUnclick;
            }
            else if (currentStateMouse == stateMouse.rigthClick)
            {
                if (Mouse.GetState().RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.rigthUnlick;
            }
            else
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.leftClick;
                else if (Mouse.GetState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    currentStateMouse = stateMouse.rigthClick;
                else
                    currentStateMouse = stateMouse.normal;
            }
        }

        private void updateFrameLevel()
        {
            int origXScreen = SuperGame.screenWidth / 20;
            int origYScreen = SuperGame.screenHeight / 15;
            /*int widthFrameLevel = 1000;
            int heightFrameLevel = 500;*/

            if (isSelectedFrameLevel && !isSelectedEnemyShip)
            {
                //move the level.
                displacementLevel.X += Mouse.GetState().X - lastPositionMouse.X;
                displacementLevel.Y += Mouse.GetState().Y - lastPositionMouse.Y;
                //limit the position "x" of the displacement level.
                if (displacementLevel.X > 0) displacementLevel.X = 0;
                if (displacementLevel.X < widthFrameLevel - width - 20) displacementLevel.X = widthFrameLevel - width - 20;
                //limit the position "y" of the displacement level.
                if (displacementLevel.Y > 0) displacementLevel.Y = 0;
                if (displacementLevel.Y < heightFrameLevel - height - 20) displacementLevel.Y = heightFrameLevel - height - 20;
            }
            lastPositionMouse.X = Mouse.GetState().X;
            lastPositionMouse.Y = Mouse.GetState().Y;
        
        }

        private void drawFrameLevel(SpriteBatch spriteBatch)
        {
            origXNatLeft = origXScreen + separator + (int)displacementLevel.X;
            origXNatRight = origXNatLeft + width;
            origYNatUp = origYScreen + separator + (int)displacementLevel.Y;
            origYNatDown = origYNatUp + height;

            origenXRealLeft = Math.Max(origXNatLeft, origXScreen);
            origenXRealRight = Math.Max(origXNatRight, origXScreen);
            origenYRealUp = Math.Max(origYNatUp, origYScreen);
            origenYRealDown = Math.Max(origYNatDown, origYScreen);
            
            int tamWidthExtra = origXScreen - origXNatLeft;
            if (tamWidthExtra < 0) tamWidthExtra = 0;
            int tamHeightExtra = origYScreen - origYNatUp;
            if (tamHeightExtra < 0) tamHeightExtra = 0;


            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(origXScreen, origYScreen, widthFrameLevel, heightFrameLevel), Color.Black);


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

            // linea de arriba:
            if (-origYScreen + origYNatUp >= 0)
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
            if (-origXScreen + origXNatLeft >= 0)
                spriteBatch.Draw(whitePixel, new Rectangle(origenXRealLeft, origenYRealUp,
                    1, Math.Min(endYScreen - origYNatUp - tamHeightExtra, height - tamHeightExtra)),
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);


            InfoEnemy infoEnemy = null;
            String typeShip = "";
            Vector2 position = Vector2.Zero,
                positionCenter = Vector2.Zero;
            int frameWidth, frameHeight;
            for (int i = 0; i < enemiesInfo.Count; i++) {
                Color color = Color.Gray;
                if (enemySelected == enemiesInfo[i])
                    color = Color.White;

                infoEnemy = enemiesInfo[i];
                typeShip = infoEnemy.type;
                frameWidth = infoEnemy.frameWidth;
                frameHeight = infoEnemy.frameHeight;
                positionCenter.X = infoEnemy.positionX + displacementLevel.X;
                positionCenter.Y = infoEnemy.positionY + displacementLevel.Y;
                position.X = positionCenter.X - frameWidth / 2;
                position.Y = positionCenter.Y - frameHeight / 2;

               if (rectangleFrameLevel.Contains((int) positionCenter.X, (int) positionCenter.Y))
                {
                
                if (typeShip.Equals("EnemyWeakA"))
                    spriteBatch.Draw(GRMng.textureEW1, position, animEW1Transition, color);
                if (typeShip.Equals("EnemyWeakShotA"))
                    spriteBatch.Draw(GRMng.textureEW2, position, animEW2Transition, color);
                if (typeShip.Equals("EnemyBeamA"))
                    spriteBatch.Draw(GRMng.textureEB1, position, animEB1Transition, color);
                if (typeShip.Equals("EnemyScaredA"))
                    spriteBatch.Draw(GRMng.textureES, position, animESTransition, color);
                if (typeShip.Equals("EnemyMineShotA"))
                    spriteBatch.Draw(GRMng.textureEMS, position, animEMSTransition, color);
                if (typeShip.Equals("EnemyLaserA"))
                    spriteBatch.Draw(GRMng.textureEL, position, animELTransition, color);

              }
            }



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

        private void updateFrameEnemiesTransition()
        {
            if (isSelectedFrameShips)
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

        private void drawFrameProperties(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(xOrigProp, yOrigProp, xEndProp - xOrigProp, yEndProp - yOrigProp), Color.Black);
            inputProp.Update();
        }
        
        private void drawPropertiesEnemies(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GRMng.fontText, "Time to spawn (in seconds)", new Vector2(inputProp.getPosition().X, yOrigProp), Color.White);
            inputProp.Draw(spriteBatch);
        }

        private void updateButtonsSaveLoadPreview() 
        {
            if (!isMovingShip && !isSelectedFrameLevel && !isSelectedFrameShips)
            {
                if (rectSave.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (currentStateMouse == stateMouse.leftClick)
                        rectAnimSave = new Rectangle(0, 2 * (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.normal)
                        rectAnimSave = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.leftUnclick)
                    {
                        save();
                    }
                }
                else
                    rectAnimSave = new Rectangle(0, 0, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);

                if (rectLoad.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (currentStateMouse == stateMouse.leftClick)
                        rectAnimLoad = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview + 2 * (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.normal)
                        rectAnimLoad = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview + (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.leftUnclick)
                    {
                        load();
                    }
                }
                else
                    rectAnimLoad = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);

                if (rectPreview.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (currentStateMouse == stateMouse.leftClick)
                        rectAnimPreview = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview * 2 + 2 * (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.normal)
                        rectAnimPreview = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview * 2 + (int)GRMng.heightButtonsSaveLoadPreview, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
                    else if (currentStateMouse == stateMouse.leftUnclick)
                    {
                        //TODO: when you unclick the button
                        if (xmlPath != null && xmlPath != "") mainGame.NewKillerMapEditor(xmlPath);
                    }
                }
                else
                    rectAnimPreview = new Rectangle(0, (int)GRMng.heightButtonsSaveLoadPreview * GRMng.numAnimsButtonsSaveLoadPreview * 2, (int)GRMng.widthButtonsSaveLoadPreview, (int)GRMng.heightButtonsSaveLoadPreview);
            }
            
        }

        private void drawButtonsSaveLoadPreview(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(GRMng.buttonsSaveLoadPreview, rectSave, rectAnimSave, Color.White);
            spriteBatch.Draw(GRMng.buttonsSaveLoadPreview, rectLoad, rectAnimLoad, Color.White);
            spriteBatch.Draw(GRMng.buttonsSaveLoadPreview, rectPreview, rectAnimPreview, Color.White);
        }

        private void saveShip(){

            
            if (isClickEW1 && rectangleFrameLevel.Contains((int)positionEW1Transition.X + GRMng.frameWidthEW1 / 2, (int)positionEW1Transition.Y + GRMng.frameHeightEW1 / 2))
            {
                InfoEnemy e = new InfoEnemy("EnemyWeakA", (int)positionEW1Transition.X + GRMng.frameWidthEW1 / 2 - (int)displacementLevel.X, (int)positionEW1Transition.Y + GRMng.frameHeightEW1 / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthEW1, GRMng.frameHeightEW1);
                enemiesInfo.Add(e);
            }
            else if (isClickEW2 && rectangleFrameLevel.Contains((int)positionEW2Transition.X + GRMng.frameWidthEW2 / 2, (int)positionEW2Transition.Y + GRMng.frameHeightEW2 / 2))
            {
                InfoEnemy e = new InfoEnemy("EnemyWeakShotA", (int)positionEW2Transition.X + GRMng.frameWidthEW2 / 2 - (int)displacementLevel.X, (int)positionEW2Transition.Y + GRMng.frameHeightEW2 / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthEW2, GRMng.frameHeightEW2);
                enemiesInfo.Add(e);
            }
            else if (isClickEB1 && rectangleFrameLevel.Contains((int)positionEB1Transition.X + GRMng.frameWidthEB1 / 2, (int)positionEB1Transition.Y + GRMng.frameHeightEB1 / 2))
            {
                InfoEnemy e = new InfoEnemy("EnemyBeamA", (int)positionEB1Transition.X + GRMng.frameWidthEB1 / 2 - (int)displacementLevel.X, (int)positionEB1Transition.Y + GRMng.frameHeightEB1 / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthEB1, GRMng.frameHeightEB1);
                enemiesInfo.Add(e);
            }
            else if (isClickES && rectangleFrameLevel.Contains((int)positionESTransition.X + GRMng.frameWidthES / 2, (int)positionESTransition.Y + GRMng.frameHeightES / 2))
            {
                InfoEnemy e = new InfoEnemy("EnemyScaredA", (int)positionESTransition.X + GRMng.frameWidthES / 2 - (int)displacementLevel.X, (int)positionESTransition.Y + GRMng.frameHeightES / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthES, GRMng.frameHeightES);
                enemiesInfo.Add(e);
            }
            else if (isClickEMS && rectangleFrameLevel.Contains((int)positionEMSTransition.X + GRMng.frameWidthEMS / 2, (int)positionEMSTransition.Y + GRMng.frameHeightEMS/2))
            {
                InfoEnemy e = new InfoEnemy("EnemyMineShotA", (int)positionEMSTransition.X + GRMng.frameWidthEMS / 2 - (int)displacementLevel.X, (int)positionEMSTransition.Y + GRMng.frameHeightEMS / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthEMS, GRMng.frameHeightEMS);
                enemiesInfo.Add(e);
            }
            else if (isClickEL && rectangleFrameLevel.Contains((int)positionELTransition.X + GRMng.frameWidthEL / 2, (int)positionELTransition.Y + GRMng.frameHeightEL / 2))
            {
                InfoEnemy e = new InfoEnemy("EnemyLaserA", (int)positionELTransition.X + GRMng.frameWidthEL / 2 - (int)displacementLevel.X, (int)positionELTransition.Y + GRMng.frameHeightEL / 2 - (int)displacementLevel.Y, this.inputProp.getValue(), GRMng.frameWidthEL, GRMng.frameHeightEL);
                enemiesInfo.Add(e);
            }
        
        }

        private void save()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML level|*.xml";
            saveFileDialog1.Title = "Save an xml file";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                XDocument miXML = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Level Edited"));
                /*miXML.Add(new XElement("level",
                             new XAttribute("titulo", "nivel1"),
                             new XAttribute("width", width),
                         new XAttribute("height", height)));*/

                XElement level = new XElement("level");
                level.Add(new XAttribute("titulo", "nivel1"),
                            new XAttribute("width", width),
                            new XAttribute("height", height));

                XElement enemieList = new XElement("enemiesList");

                for (int i = 0; i < enemiesInfo.Count; i++)
                {
                    enemieList.Add(new XElement("enemy",
                                        new XAttribute("type", enemiesInfo[i].type),
                                        new XAttribute("positionX", enemiesInfo[i].positionX - origXScreen),
                                        new XAttribute("positionY", enemiesInfo[i].positionY - origYScreen),
                                        new XAttribute("time", enemiesInfo[i].time)));
                }

                level.Add(enemieList);

                miXML.Add(level);


                miXML.Save(saveFileDialog1.FileName);
                xmlPath = saveFileDialog1.FileName;
            }
        }


        protected void load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Utilizar nombres de fichero y nodos XML idénticos a los que se guardaron
                try
                {
                    enemiesInfo = new List<InfoEnemy>();
                    //  Leer los datos del archivo
                    XmlDocument lvl = new XmlDocument();
                    lvl.Load(openFileDialog1.FileName);

                    xmlPath = openFileDialog1.FileName;

                    XmlNodeList lista = null;

                    //get the widht and the height of level
                    lista = lvl.GetElementsByTagName("level");
                    XmlAttributeCollection level = lista[0].Attributes;
                    width = Convert.ToInt32(level[1].Value);
                    height = Convert.ToInt32(level[2].Value);

                    //get the enemies
                    lista = lvl.GetElementsByTagName("enemy");
                    String enemyType;
                    float positionX;
                    float positionY;
                    float time;
                    foreach (XmlElement nodo in lista)
                    {

                        XmlAttributeCollection enemyN = nodo.Attributes;
                        //XmlAttribute a = enemyN[1];
                        enemyType = Convert.ToString(enemyN[0].Value);
                        positionX = (float)Convert.ToDouble(enemyN[1].Value) + origXScreen;
                        positionY = (float)Convert.ToDouble(enemyN[2].Value) + origYScreen;
                        time = (float)Convert.ToDouble(enemyN[3].Value);
                        //timeLeftEnemy.Add(time);

                        InfoEnemy enemy = null;
                        // TODO: los enemigos deberían de crearse desde la EnemyFactory

                        if (enemyType.Equals("EnemyWeakA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthEW1, GRMng.frameHeightEW1);
                        else if (enemyType.Equals("EnemyBeamA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthEB1, GRMng.frameHeightEB1);
                        else if (enemyType.Equals("EnemyWeakShotA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthEW2, GRMng.frameHeightEW2);
                        else if (enemyType.Equals("EnemyMineShotA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthEMS, GRMng.frameHeightEMS);
                        else if (enemyType.Equals("EnemyLaserA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthEL, GRMng.frameHeightEL);
                        else if (enemyType.Equals("EnemyScaredA"))
                            enemy = new InfoEnemy(enemyType, (int)positionX, (int)positionY, (int)time, GRMng.frameWidthES, GRMng.frameHeightES);
                        enemiesInfo.Add(enemy);

                    }
                }
                catch (Exception e)
                {
                }
            }
        }   //  end LeerArchivoXML()

        private void unselectEnemy() {

            isSelectedEnemyShip = false;
            if (enemySelected != null)
            enemySelected.time = inputProp.getValue();
            enemySelected = null;

            
        
        }//unselecEnemy


    }//Class MainMapEditor
}
