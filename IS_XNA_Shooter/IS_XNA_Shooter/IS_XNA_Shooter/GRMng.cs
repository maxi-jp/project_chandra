using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace IS_XNA_Shooter
{
    class GRMng // Graphics Resourdes Manager
    {
        private ContentManager content;

        public static Texture2D textureAim;     // textura del puntero
        public static Texture2D textureCell;    // celdas del fondo del nivel
        public static Texture2D textureRed;     // rojazo
        public static Texture2D whitepixel;     // textura pixel blanco
        public static Texture2D redpixel;       // textura pixel rojo
        public static Texture2D blackpixeltrans;// textura pixel negro transparente

        #region BACKGROUNDS
        /* ------------------- FONDOS ------------------- */
        public static Texture2D textureBgGame1A; // tile fondo para el juego A
        public static Texture2D textureBgSpace; // dibujo espacial
        public static Texture2D textureBg00;
        public static Texture2D textureBg01;
        public static Texture2D textureBg02;
        public static Texture2D textureBg03;
        public static Texture2D textureBgCol1;
        public static Texture2D textureBgCol2;
        public static Texture2D textureBgCol3;
        #endregion

        #region ENEMIES
        /* ------------------- ENEMYWEAK VERDE ------------------- */
        public static Texture2D textureEW1;
        public static short frameWidthEW1 = 80;
        public static short frameHeightEW1 = 80;
        public static short numAnimsEW1 = 3;
        public static short[] frameCountEW1 = { 1, 4, 6 };
        public static bool[] loopingEW1 = { true, true, false };

        /* ------------------- ENEMYWEAK MORADO ------------------- */
        public static Texture2D textureEW2;
        public static short frameWidthEW2 = 80;
        public static short frameHeightEW2 = 80;
        public static short numAnimsEW2 = 4;
        public static short[] frameCountEW2 = { 1, 4, 5, 6 };
        public static bool[] loopingEW2 = { true, true, false, false };

        /* ------------------- ENEMYBEAM GRIS ------------------- */
        public static Texture2D textureEB1;
        public static short frameWidthEB1 = 80;
        public static short frameHeightEB1 = 80;
        public static short numAnimsEB1 = 3;
        public static short[] frameCountEB1 = { 1, 4, 6 };
        public static bool[] loopingEB1 = { true, true, false };

        /* ------------------- ENEMYSCARED ------------------- */
        public static Texture2D textureES;
        public static short frameWidthES = 80;
        public static short frameHeightES = 80;
        public static short numAnimsES = 6;
        public static short[] frameCountES = { 1, 1, 2, 3, 3, 5 };
        public static bool[] loopingES = { true, true, true, true, true, false };
        /* ------------------- BULLET SCARED -------------------- */
        public static Texture2D textureESBullet;
        public static short frameWidthESBullet = 10;
        public static short frameHeightESBullet = 3;
        public static short numAnimsESBullet = 1;
        public static short[] frameCountESBullet = { 1 };
        public static bool[] loopingESBullet = { true };

        /* ------------------- ENEMYMINESHOT ------------------- */
        public static Texture2D textureEMS;
        public static short frameWidthEMS = 80;
        public static short frameHeightEMS = 80;
        public static short numAnimsEMS = 3;
        public static short[] frameCountEMS = { 1, 4, 5 };
        public static bool[] loopingEMS = { true, false, false };
        /* ------------------- BULLET MINESHOT-------------------- */
        public static Texture2D textureEMSBullet;
        public static short frameWidthEMSBullet = 4;
        public static short frameHeightEMSBullet = 4;
        public static short numAnimsEMSBullet = 1;
        public static short[] frameCountEMSBullet = { 1 };
        public static bool[] loopingEMSBullet = { true };


        /* ------------------- ENEMY LASER-------------------- */
        public static Texture2D textureEL;
        public static short frameWidthEL = 80;
        public static short frameHeightEL = 75;
        public static short numAnimsEL = 6;
        public static short[] frameCountEL = { 1, 10, 1, 5 };
        public static bool[] loopingEL = { true, false, true, false };
        /* ------------------- BULLET LASER-------------------- */
        public static Texture2D textureELBullet;
        public static short frameWidthELBulletA = 600;
        public static short frameWidthELBulletB = 1300;
        public static short frameHeightELBullet = 2;
        public static short numAnimsELBullet = 1;
        public static short[] frameCountELBullet = { 1 };
        public static bool[] loopingELBullet = { true };

        /* ------------------- FINAL BOSS 1-------------------- */
        public static Texture2D textureFinalBoss1;
        public static short frameWidthFinalBoss1 = 240;
        public static short frameHeightFinalBoss1 = 240;
        public static short numAnimsFinalBoss1 = 2;
        public static short[] frameCountFinalBoss1 = { 3, 4 };
        public static bool[] loopingFinalBoss1 = { true, false };

        /* ------------------- SHOT FINAL BOSS 1-------------------- */
        public static Texture2D textureShotFinalBoss1;
        public static short frameWidthShotFinalBoss1 = 20;
        public static short frameHeightShotFinalBoss1 = 20;
        public static short numAnimsShotFinalBoss1 = 1;
        public static short[] frameCountShotFinalBoss1 = { 1 };
        public static bool[] loopingShotFinalBoss1 = { true };
        public static Vector2[] colliderShotFinalBoss1 = { new Vector2(2, 9), new Vector2(4, 4), new Vector2(9, 2), new Vector2(15, 4), new Vector2(16, 9), new Vector2(15, 14), 
                                                             new Vector2(9, 17), new Vector2(4, 14) };

        /* ------------------- BOT FINAL BOSS 1-------------------- */
        public static Texture2D textureBFB;
        public static short frameWidthBFB = 60;
        public static short frameHeightBFB = 60;
        public static short numAnimsBFB = 2;
        public static short[] frameCountBFB = { 3, 4 };
        public static bool[] loopingBFB = { true, false };

        /* ------------------- BULLET LASER Heroe-------------------- */
        public static Texture2D textureELBulletHeroe;
        public static short frameWidthELBulletHeroe = 2080;
        public static short frameHeightELBulletHeroe = 2;
        public static short numAnimsELBulletHeroe = 1;
        public static short[] frameCountELBulletHeroe = { 1 };
        public static bool[] loopingELBulletHeroe = { true };

        /* ------------------- FINAL BOSS 1 TURRET 1 -------------------- */
        public static Texture2D textureFinalBoss1Turret1;
        public static short frameWidthFinalBoss1Turret1 = 20;
        public static short frameHeightFinalBoss1Turret1 = 20;
        public static short numAnimsFinalBoss1Turret1 = 3;
        public static short[] frameCountFinalBoss1Turret1 = { 1, 3, 4 };
        public static bool[] loopingFinalBoss1Turret1 = { true, true, false };

        /* ------------------- FINAL BOSS 1 TURRET 1 SHOT -------------------- */
        public static Texture2D textureFinalBoss1Turret1Shot;
        public static short frameWidthFinalBoss1Turret1Shot = 5;
        public static short frameHeightFinalBoss1Turret1Shot = 5;
        public static short numAnimsFinalBoss1Turret1Shot = 1;
        public static short[] frameCountFinalBoss1Turret1Shot = { 1 };
        public static bool[] loopingFinalBoss1Turret1Shot = { true };

        /* ------------------- FINAL BOSS 1 TURRET 2 -------------------- */
        public static Texture2D textureFinalBoss1Turret2;
        public static short frameWidthFinalBoss1Turret2 = 90;
        public static short frameHeightFinalBoss1Turret2 = 30;
        public static short numAnimsFinalBoss1Turret2 = 2;
        public static short[] frameCountFinalBoss1Turret2 = { 1, 4 };
        public static bool[] loopingFinalBoss1Turret2 = { true, false };

        /* ------------------- FINAL BOSS HEROE anim-------------------- */
        public static Texture2D textureHeroe1;
        public static short frameWidthHeroe1 = 100;
        public static short frameHeightHeroe1 = 100;
        public static short numAnimsHeroe1 = 3;
        public static short[] frameCountHeroe1 = { 1, 3, 4 };
        public static bool[] loopingHeroe1 = { false, true, false };
        public static Vector2[] colliderHeroe1 = { new Vector2(11, 50), new Vector2(51, 30), new Vector2(89, 49), new Vector2(51, 69) };

        /* ------------------- SHOT FINAL BOSS HEROE-------------------- */
        public static Texture2D textureShotFinalBossHeroe;
        public static short frameWidthShotFinalBossHeroe = 20;
        public static short frameHeightShotFinalBossHeroe = 20;
        public static short numAnimsShotFinalBossHeroe = 1;
        public static short[] frameCountShotFinalBossHeroe = { 1 };
        public static bool[] loopingShotFinalBossHeroe = { true };
        public static Vector2[] colliderShotFinalBossHeroe = {new Vector2(3, 3), new Vector2(10, 0), new Vector2(16, 3), new Vector2(19, 10), new Vector2(16, 16), 
                                                                 new Vector2(10, 19), new Vector2(3, 16), new Vector2(0, 10) };

        #endregion

        #region PLAYER
        /* ------------------- PLAYERA1 ------------------- */
        public static Texture2D texturePA1;
        public static short frameWidthPA1 = 80;
        public static short frameHeightPA1 = 80;
        public static short numAnimsPA1 = 4;
        public static short[] frameCountPA1 = { 1, 4, 5, 5 };
        public static bool[] loopingPA1 = { true, true, false, false };
        #endregion

        #region SHOTS
        /* ------------------- LASER 1 ------------------- */
        public static Texture2D textureL1;
        public static short frameWidthL1 = 14;
        public static short frameHeightL1 = 3;
        public static short numAnimsL1 = 1;
        public static short[] frameCountL1 = { 3 };
        public static bool[] loopingL1 = { true };

        /* ------------------- LASER 2 ------------------- */
        public static Texture2D textureL2;
        public static short frameWidthL2 = 14;
        public static short frameHeightL2 = 3;
        public static short numAnimsL2 = 1;
        public static short[] frameCountL2 = { 3 };
        public static bool[] loopingL2 = { true };
        #endregion

        #region OTHERS
        /* ------------------- EXPLOSION VERDE ------------------- */
        public static Texture2D textureExplosion1;
        public static short frameWidthEx1 = 100;
        public static short frameHeightEx1 = 100;
        public static short frameCountEx1 = 8;

        /* ------------------- CHARACTERS ------------------- */
        public static Texture2D textureCaptain;
        public static Texture2D texturePilot;
        public static Texture2D texturePilotCyborg;
        #endregion

        #region HUBS
        /* ------------------- HUB A ------------------- */
        public static Texture2D hubBase;
        #endregion

        #region MENUS
        /* ------------------- START MENU ------------------- */
        public static Texture2D startSplash;

        /* ------------------- MAIN MENU ------------------- */
        public static Texture2D menuMain;
        public static Texture2D menuStory;
        public static Texture2D menuArcade;
        public static Texture2D menuConfig;
        public static Texture2D menuSplash;

        /* ------------------- INGAME MENU ------------------- */
        public static Texture2D menuIngame;
        public static Texture2D getready321;    // textura cuenta atrás menú

        /* ------------------- GAMEOVER MENU ------------------- */
        public static Texture2D menuGameOver;
        public static Texture2D gameOverSplash;
        #endregion

        #region VIDEO
        public static Video videoIntroStory;
        #endregion

        public GRMng(ContentManager content)
        {
            this.content = content;
        }

        public void LoadContent(int i)
        {
            // i:
            // 0=Start Menu
            // 1=Main Menu
            // 2=Ingame Menu
            // 3=GameA level 1
            // 4=GameB level 1
            // 99=GameOver Menu

            switch (i)
            {
                case 0: // start menu:
                    LoadStart();
                    break;

                case 1: // menu:
                    LoadMenu();
                    break;

                case 2: // menuIngame:
                    LoadIngameMenu();
                    break;

                case 3: // GameA nivel 1
                    LoadIngameMenu();

                    hubBase = content.Load<Texture2D>("Graphics/Hub/base256");
                    

                    texturePA1 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureAim = content.Load<Texture2D>("Graphics/aimpoint");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    textureL2 = content.Load<Texture2D>("Graphics/laserShotAnim2");
                    
                    textureEW1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    textureEW2 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy02_80x80");
                    textureEB1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy03_80x80");
                    textureES = content.Load<Texture2D>("Graphics/Ships/EnemyScared");
                    textureESBullet = content.Load<Texture2D>("Graphics/scaredBullet");
                    textureEMS =  content.Load<Texture2D>("Graphics/Ships/mineAnimation");
                    textureEMSBullet = content.Load<Texture2D>("Graphics/mineShot");
                    textureEL = content.Load<Texture2D>("Graphics/Ships/enemyLaser");
                    textureELBullet = content.Load<Texture2D>("Graphics/yellowpixel");
                    textureELBulletHeroe = content.Load<Texture2D>("Graphics/yellowpixel");
                    
                    textureCell = content.Load<Texture2D>("Graphics/celdasuelo");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");

                    textureFinalBoss1Turret1 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/turret1");
                    textureFinalBoss1Turret2 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/turret2");

                    textureFinalBoss1Turret1Shot = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/shotTurret1");

                    textureHeroe1 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/heroe1");
                    textureShotFinalBossHeroe = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/shotHeroe1");

                    break;

                case 4: // GameB nivel 1
                    LoadIngameMenu();

                    videoIntroStory = content.Load<Video>("Video/VideoIntroProjectChandra");

                    hubBase = content.Load<Texture2D>("Graphics/Hub/base256");

                    texturePA1 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    textureL2 = content.Load<Texture2D>("Graphics/laserShotAnim2");
                    
                    textureEW1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    textureEW2 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy02_80x80");
                    textureEB1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy03_80x80");
                    textureEMS =  content.Load<Texture2D>("Graphics/Ships/mineAnimation");
                    textureEMSBullet = content.Load<Texture2D>("Graphics/mineShot");
                    textureEL = content.Load<Texture2D>("Graphics/Ships/enemyLaser");
                    textureELBullet = content.Load<Texture2D>("Graphics/yellowpixel");
                    textureRed = content.Load<Texture2D>("Graphics/Rojazo");
                    textureCaptain = content.Load<Texture2D>("Graphics/Images/Captain");
                    texturePilot = content.Load<Texture2D>("Graphics/Images/Pilot");
                    texturePilotCyborg = content.Load<Texture2D>("Graphics/Images/PilotCyborg");
                    
                    textureBgGame1A = content.Load<Texture2D>("Graphics/Backgrounds/backgroundTile1");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");
                    textureBgCol1 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable1");
                    textureBgCol2 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable2");
                    textureBgCol3 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable3");

                    textureFinalBoss1 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBoss1");
                    textureShotFinalBoss1 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/shotFinalBoss1");
                    textureBFB = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBossPhase2");

                    break;

                case 99: // menu game over:
                    menuGameOver = content.Load<Texture2D>("Graphics/Menu/gameover");
                    gameOverSplash = content.Load<Texture2D>("Graphics/Splash/splash_gameover_2");
                    break;

            }

            whitepixel = content.Load<Texture2D>("Graphics/whitepixel");
            redpixel = content.Load<Texture2D>("Graphics/redpixel");
            blackpixeltrans = content.Load<Texture2D>("Graphics/blackpixeltransparent");

        } // LoadContent

        public void UnloadContent(int i)
        {
            // i:
            // 0=Start Menu
            // 1=Main Menu
            // 2=Ingame Menu
            // 3=GameA level 1
            // 4=GameB level 1
            // 99=GameOver Menu

            switch (i)
            {
                case 0: // start menu:
                    UnloadStart();
                    break;

                case 1: // menu:
                    UnloadMenu();
                    break;

                case 2: // menuIngame:
                    UnloadIngameMenu();
                    break;

                case 3: // GameA nivel 1

                    hubBase = null;

                    texturePA1 = null;
                    textureAim = null;
                    textureL1 = null;
                    textureL2 = null;

                    textureEW1 = null;
                    textureEW2 = null;
                    textureEB1 = null;
                    textureES = null;
                    textureESBullet = null;
                    textureEMS = null;
                    textureEMSBullet = null;
                    textureEL = null;
                    textureELBullet = null;
                    textureELBulletHeroe = null;

                    textureCell = null;
                    textureRed = null;

                    textureBg00 = null;
                    textureBg01 = null;
                    textureBg02 = null;
                    textureBg03 = null;

                    textureFinalBoss1Turret1 = null;
                    textureFinalBoss1Turret2 = null;

                    textureFinalBoss1Turret1Shot = null;

                    textureHeroe1 = null;
                    textureShotFinalBossHeroe = null;

                    break;

                case 4: // GameB nivel 1

                    hubBase = null;

                    texturePA1 = null;
                    textureL1 = null;

                    textureEW1 = null;

                    textureRed = null;
                    textureCaptain = null;
                    texturePilot = null;
                    texturePilotCyborg = null;
                    textureBgGame1A = null;
                    textureBg00 = null;
                    textureBg01 = null;
                    textureBg02 = null;
                    textureBg03 = null;
                    textureBgCol1 = null;
                    textureBgCol2 = null;
                    textureBgCol3 = null;

                    textureFinalBoss1 = null;
                    textureShotFinalBoss1 = null;
                    textureBFB = null;

                    break;

                case 99:
                    menuGameOver = null;
                    gameOverSplash = null;
                    break;

            }
        } // UnloadContent

        private void LoadStart()
        {
            startSplash = content.Load<Texture2D>("Graphics/Splash/splash_start_2");
        }

        private void UnloadStart()
        {
            startSplash = null;
        }

        private void LoadMenu()
        {
            menuMain = content.Load<Texture2D>("Graphics/Menu/main");
            menuStory = content.Load<Texture2D>("Graphics/Menu/story");
            menuArcade = content.Load<Texture2D>("Graphics/Menu/arcade");
            menuConfig = content.Load<Texture2D>("Graphics/Menu/configuration");
            menuSplash = content.Load<Texture2D>("Graphics/Splash/splash_mainmenu_2");
        }

        private void UnloadMenu()
        {
            menuMain = null;
            menuStory = null;
            menuArcade = null;
            menuConfig = null;
            menuSplash = null;
        }

        private void LoadIngameMenu()
        {
            menuIngame = content.Load<Texture2D>("Graphics/Menu/ingame");
            getready321 = content.Load<Texture2D>("Graphics/Menu/getready321");
        }

        private void UnloadIngameMenu()
        {
            //menuIngame.Dispose();
            menuIngame = null;
            getready321 = null;
        }

        public void UnloadContentGame()
        {
            UnloadContent(2);
            UnloadContent(3);
        }

    } // class GRMng
}