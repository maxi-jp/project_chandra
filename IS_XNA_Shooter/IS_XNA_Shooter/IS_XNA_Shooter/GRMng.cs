﻿using System;
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

        /* ------------------- ENEMYWEAK NARANJA ------------------- */
        public static Texture2D textureEW1;
        public static short frameWidthEW1 = 20;
        public static short frameHeightEW1 = 20;
        public static short numAnimsEW1 = 1;
        public static short[] frameCountEW1 = { 4 };
        public static bool[] loopingEW1 = { true };

        /* ------------------- ENEMYWEAK VERDE ------------------- */
        public static Texture2D textureEW2;
        public static short frameWidthEW2 = 80;
        public static short frameHeightEW2 = 80;
        public static short numAnimsEW2 = 3;
        public static short[] frameCountEW2 = { 1, 4, 6 };
        public static bool[] loopingEW2 = { true, true, false };

        /* ------------------- ENEMYBEAM MORADO ------------------- */
        public static Texture2D textureEB1;
        public static short frameWidthEB1 = 80;
        public static short frameHeightEB1 = 80;
        public static short numAnimsEB1 = 3;
        public static short[] frameCountEB1 = { 1, 4, 6 };
        public static bool[] loopingEB1 = { true, true, false };

        /* ------------------- PLAYERA1 ------------------- */
        public static Texture2D texturePA1;
        public static short frameWidthPA1 = 40;
        public static short frameHeightPA1 = 40;
        public static short numAnimsPA1 = 2;
        public static short[] frameCountPA1 = { 1, 5 };
        public static bool[] loopingPA1 = { true, false };

        /* ------------------- PLAYERA2 ------------------- */
        public static Texture2D texturePA2;
        public static short frameWidthPA2 = 80;
        public static short frameHeightPA2 = 80;
        public static short numAnimsPA2 = 3;
        public static short[] frameCountPA2 = { 1, 4, 5 };
        public static bool[] loopingPA2 = { true, true, false };

        /* ------------------- LASER 1 ------------------- */
        public static Texture2D textureL1;
        public static short frameWidthL1 = 14;
        public static short frameHeightL1 = 3;
        public static short numAnimsL1 = 1;
        public static short[] frameCountL1 = { 3 };
        public static bool[] loopingL1 = { true };

        /* ------------------- EXPLOSION VERDE ------------------- */
        public static Texture2D textureExplosion1;
        public static short frameWidthEx1 = 100;
        public static short frameHeightEx1 = 100;
        public static short frameCountEx1 = 8;

        /* ------------------- HUB A ------------------- */
        public static Texture2D hubLeft;
        public static Texture2D hubCenter;
        public static Texture2D hubRight;

        /* ------------------- MENÚ ------------------- */
        public static Texture2D menuMain;
        public static Texture2D menuHistory;
        public static Texture2D menuArcade;
        public static Texture2D menuConfig;

        /* ------------------- MENÚ INGAME ------------------- */
        public static Texture2D menuIngame;
        public static Texture2D getready321;    // textura cuenta atrás menú

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

                    texturePA1 = content.Load<Texture2D>("Graphics/Ships/playerShotAnim");
                    texturePA2 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureAim = content.Load<Texture2D>("Graphics/aimpoint");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    textureExplosion1 = content.Load<Texture2D>("Graphics/Explosions/sprites_explosion100x100");
                    textureEW1 = content.Load<Texture2D>("Graphics/Ships/EnemyWeakAnim");
                    textureEW2 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    textureEB1 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy02_80x80");
                    textureCell = content.Load<Texture2D>("Graphics/celdasuelo");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");
                    break;

                case 3: // GameB nivel 1
                    LoadIngameMenu();

                    texturePA2 = content.Load<Texture2D>("Graphics/Ships/sprites80x80");
                    textureL1 = content.Load<Texture2D>("Graphics/laserShotAnim");
                    textureExplosion1 = content.Load<Texture2D>("Graphics/Explosions/sprites_explosion100x100");
                    textureEW2 = content.Load<Texture2D>("Graphics/Ships/sprites_enemy01_80x80");
                    textureRed = content.Load<Texture2D>("Graphics/Rojazo");
                    textureBgGame1A = content.Load<Texture2D>("Graphics/Backgrounds/backgroundTile1");
                    textureBg00 = content.Load<Texture2D>("Graphics/Backgrounds/bg00");
                    textureBg01 = content.Load<Texture2D>("Graphics/Backgrounds/bg01");
                    textureBg02 = content.Load<Texture2D>("Graphics/Backgrounds/bg02");
                    textureBg03 = content.Load<Texture2D>("Graphics/Backgrounds/bg03");
                    textureBgCol1 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable1");
                    textureBgCol2 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable2");
                    textureBgCol3 = content.Load<Texture2D>("Graphics/Backgrounds/LayerColisionable3");
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
                    texturePA2 = null;
                    textureAim = null;
                    textureL1 = null;
                    textureExplosion1 = null;
                    textureEW1 = null;
                    textureEW2 = null;
                    textureEB1 = null;
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

                    texturePA2 = null;
                    textureL1 = null;
                    textureExplosion1 = null;
                    textureEW2 = null;
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
            //menuMain.Dispose();
            menuMain = null;
            //menuHistory.Dispose();
            menuHistory = null;
            //menuArcade.Dispose();
            menuArcade = null;
            //menuConfig.Dispose();
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