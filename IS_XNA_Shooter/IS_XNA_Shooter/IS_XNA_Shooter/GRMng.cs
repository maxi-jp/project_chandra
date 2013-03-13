using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        /* ------------------- ENEMYBEAM MORADO ------------------- */
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
        /* ------------------- BULLET LASER-------------------- */
        public static Texture2D textureELBullet;
        public static short frameWidthELBullet = 600;
        public static short frameHeightELBullet = 2;
        public static short numAnimsELBullet = 1;
        public static short[] frameCountELBullet = { 1 };
        public static bool[] loopingELBullet = { true };

        /* ------------------- FINAL BOSS 1-------------------- */
        public static Texture2D textureFinalBoss1;
        public static short frameWidthFinalBoss1 = 240;
        public static short frameHeightFinalBoss1 = 240;
        public static short numAnimsFinalBoss1 = 1;
        public static short[] frameCountFinalBoss1 = { 3 };
        public static bool[] loopingFinalBoss1 = { true };

        /* ------------------- FINAL BOSS 1-------------------- */
        public static Texture2D textureBFB;
        public static short frameWidthBFB = 40;
        public static short frameHeightBFB = 25;
        public static short numAnimsBFB = 2;
        public static short[] frameCountBFB = { 2, 3 };
        public static bool[] loopingBFB = { true };

        /* ------------------- FINAL BOSS HEROE-------------------- */
        public static Texture2D textureFinalBossHeroe;
        public static short frameWidthFinalBossHeroe = 80;
        public static short frameHeightFinalBossHeroe = 60;
        public static short numAnimsFinalBossHeroe = 1;
        public static short[] frameCountFinalBossHeroe = { 1 };
        public static bool[] loopingFinalBossHeroe = {true};

        /* ------------------- BULLET LASER BOSS-------------------- */
        public static Texture2D textureLaserBoss;
        public static short frameWidthLaserBoss = 512;
        public static short frameHeightLaserBoss = 16;
        public static short numAnimsLaserBoss = 2;
        public static short[] frameCountLaserBoss = { 1, 2 };
        public static bool[] loopingLaserBoss = { true };

        /* ------------------- BULLET LASER Heroe-------------------- */
        public static Texture2D textureELBulletHeroe;
        public static short frameWidthELBulletHeroe = 2080;
        public static short frameHeightELBulletHeroe = 2;
        public static short numAnimsELBulletHeroe = 1;
        public static short[] frameCountELBulletHeroe = { 1 };
        public static bool[] loopingELBulletHeroe = { true };

        #endregion

        #region PLAYER
        /* ------------------- PLAYERA1 ------------------- */
        public static Texture2D texturePA1;
        public static short frameWidthPA1 = 80;
        public static short frameHeightPA1 = 80;
        public static short numAnimsPA1 = 3;
        public static short[] frameCountPA1 = { 1, 4, 5 };
        public static bool[] loopingPA1 = { true, true, false };

        /* ------------------- LASER 1 ------------------- */
        public static Texture2D textureL1;
        public static short frameWidthL1 = 14;
        public static short frameHeightL1 = 3;
        public static short numAnimsL1 = 1;
        public static short[] frameCountL1 = { 3 };
        public static bool[] loopingL1 = { true };
        #endregion

        #region OTHERS
        /* ------------------- EXPLOSION VERDE ------------------- */
        public static Texture2D textureExplosion1;
        public static short frameWidthEx1 = 100;
        public static short frameHeightEx1 = 100;
        public static short frameCountEx1 = 8;
        #endregion

        #region HUBS
        /* ------------------- HUB A ------------------- */
        public static Texture2D hubLeft;
        public static Texture2D hubCenter;
        public static Texture2D hubRight;
        #endregion

        #region MENUS
        /* ------------------- MENÚ ------------------- */
        public static Texture2D menuMain;
        public static Texture2D menuHistory;
        public static Texture2D menuArcade;
        public static Texture2D menuConfig;

        /* ------------------- MENÚ INGAME ------------------- */
        public static Texture2D menuIngame;
        public static Texture2D getready321;    // textura cuenta atrás menú
        #endregion

        public GRMng(ContentManager content)
        {
            this.content = content;
        }

        public void LoadContent(int i)
        {
            // i:
            // 0=Menú principal
            // 1=Menú ingame
            // 2=GameA nivel 1
            // 3=GameB nivel 1

            switch (i)
            {
                case 0: // menu:
                    LoadMenu();
                    break;

                case 1: // menuIngame:
                    LoadIngameMenu();
                    break;

                case 2: // GameA nivel 1
                    LoadIngameMenu();

                    hubCenter = content.Load<Texture2D>("Graphics/Hub/center_720");
                    hubLeft = content.Load<Texture2D>("Graphics/Hub/left_720");
                    hubRight = content.Load<Texture2D>("Graphics/Hub/right_720");

                    texturePA1 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureAim = content.Load<Texture2D>("Graphics/aimpoint");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    
                    textureExplosion1 = content.Load<Texture2D>("Graphics/Explosions/sprites_explosion100x100");
                    
                    textureEW1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    textureEB1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy02_80x80");
                    textureES = content.Load<Texture2D>("Graphics/Ships/EnemyScared");
                    textureESBullet = content.Load<Texture2D>("Graphics/scaredBullet");
                    textureEMS =  content.Load<Texture2D>("Graphics/Ships/mineAnimation");
                    textureEMSBullet = content.Load<Texture2D>("Graphics/mineShot");
                    textureELBullet = content.Load<Texture2D>("Graphics/yellowpixel");
                    textureELBulletHeroe = content.Load<Texture2D>("Graphics/yellowpixel");
                    
                    textureCell = content.Load<Texture2D>("Graphics/celdasuelo");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");

                    textureFinalBossHeroe = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBossHeroe1");

                    break;

                case 3: // GameB nivel 1
                    LoadIngameMenu();

                    texturePA1 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    textureExplosion1 = content.Load<Texture2D>("Graphics/Explosions/sprites_explosion100x100");
                    
                    textureEW1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    
                    textureRed = content.Load<Texture2D>("Graphics/Rojazo");
                    
                    textureBgGame1A = content.Load<Texture2D>("Graphics/Backgrounds/backgroundTile1");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");
                    textureBgCol1 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable1");
                    textureBgCol2 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable2");
                    textureBgCol3 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable3");


                    textureLaserBoss = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBoss1Laser");
                    textureFinalBoss1 = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBoss1");
                    textureBFB = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBossPhase2");
                    textureFinalBossHeroe = content.Load<Texture2D>("Graphics/Ships/Final Boss 1/finalBossHeroe1");
                 
                    

                                      
                   

                    break;
            }

            whitepixel = content.Load<Texture2D>("Graphics/whitepixel");
            redpixel = content.Load<Texture2D>("Graphics/redpixel");
            blackpixeltrans = content.Load<Texture2D>("Graphics/blackpixeltransparent");

        } // LoadContent

        public void UnloadContent(int i)
        {
            // i:
            // 0=Menú principal
            // 1=Menú ingame
            // 2=GameA nivel 1
            // 3=GameB nivel 1

            switch (i)
            {
                case 0: // menu:
                    UnloadMenu();
                    break;

                case 1: // menuIngame:
                    UnloadIngameMenu();
                    break;

                case 2: // GameA nivel 1

                    hubCenter = null;
                    hubLeft = null;
                    hubRight = null;

                    texturePA1 = null;
                    textureAim = null;
                    textureL1 = null;
                    textureExplosion1 = null;

                    textureEW1 = null;
                    textureEB1 = null;
                    textureES = null;
                    textureESBullet = null;
                    textureEMS = null;
                    textureEMSBullet = null;
                    textureELBullet = null;

                    textureCell = null;
                    textureRed = null;

                    textureBg00 = null;
                    textureBg01 = null;
                    textureBg02 = null;
                    textureBg03 = null;

                    break;

                case 3: // GameB nivel 1

                    hubCenter = null;
                    hubLeft = null;
                    hubRight = null;

                    texturePA1 = null;
                    textureL1 = null;

                    textureExplosion1 = null;

                    textureEW1 = null;

                    textureRed = null;
                    textureBgGame1A = null;
                    textureBg00 = null;
                    textureBg01 = null;
                    textureBg02 = null;
                    textureBg03 = null;
                    textureBgCol1 = null;
                    textureBgCol2 = null;
                    textureBgCol3 = null;

                    break;
            }
        } // UnloadContent

        private void LoadMenu()
        {
            menuMain = content.Load<Texture2D>("Graphics/Menu/main");
            menuHistory = content.Load<Texture2D>("Graphics/Menu/history");
            menuArcade = content.Load<Texture2D>("Graphics/Menu/arcade");
            menuConfig = content.Load<Texture2D>("Graphics/Menu/configuration");
        }

        private void UnloadMenu()
        {
            menuMain = null;
            menuHistory = null;
            menuArcade = null;
            menuConfig = null;
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