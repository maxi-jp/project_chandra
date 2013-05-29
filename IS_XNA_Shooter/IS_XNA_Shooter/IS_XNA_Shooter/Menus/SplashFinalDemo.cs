using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class SplashFinalDemo
    {
        private Sprite splashFondo; // fondo
        private Sprite splashLogo; // logo project chandra
        private Sprite splashEscudos;   // escudos UCM y fdi
        private Sprite splashQr; // código qr
        private Sprite splashWeb; // dirección web

        private enum State
        {
            ONE,    // aparece logo
            TWO,    // aparecen escudos y sube el logo
            THREE,  // aparecen el qr y la web
            FOUR
        }
        private State currentState;

        private byte logoTransp = 0;
        private float logoScale = 0;
        private float timeStateOne = 4.0f;

        private byte escudosTransp = 0;
        private float logoFinalPosY = 75;
        private float timeStateTwo = 3.0f;

        private byte qrTransp = 0;
        private float timeStateThree = 3.0f;

        public SplashFinalDemo()
        {
            
        }

        public void Initialize()
        {
            currentState = State.ONE;

            splashFondo = new Sprite(false, new Vector2(), 0, GRMng.menuSplash01);
            splashLogo = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2), 0, GRMng.splash_demofin_1);
            splashEscudos = new Sprite(false,
                new Vector2(SuperGame.screenWidth - GRMng.splash_demofin_2.Width, SuperGame.screenHeight - GRMng.splash_demofin_2.Height),
                0, GRMng.splash_demofin_2);
            splashQr = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2), 0, GRMng.splash_demofin_3);
            splashWeb = new Sprite(true,
                new Vector2(SuperGame.screenWidth / 2, (SuperGame.screenHeight / 2) + (GRMng.splash_demofin_3.Height / 2) + 25),
                0, GRMng.splash_demofin_4);

            splashLogo.SetTransparency(0);
            splashLogo.scale = logoScale;
            splashEscudos.SetTransparency(0);
            splashQr.SetTransparency(0);
        }

        public void Update(float deltaTime)
        {
            switch (currentState)
            {
                case State.ONE:
                    timeStateOne -= deltaTime;
                    if (timeStateOne <= 0)
                        currentState = State.TWO;
                    else
                    {
                        if (logoTransp < 254)
                        {
                            logoTransp += 2;
                            splashLogo.SetTransparency(logoTransp);
                        }
                        if (logoScale < 1)
                        {
                            logoScale += 0.1f;
                            splashLogo.scale = logoScale;
                        }
                    }
                    break;
                case State.TWO:
                    timeStateTwo -= deltaTime;
                    if (timeStateTwo <= 0)
                        currentState = State.THREE;
                    else
                    {
                        if (escudosTransp < 254)
                        {
                            escudosTransp += 2;
                            splashEscudos.SetTransparency(escudosTransp);
                        }
                        if (splashLogo.position.Y > logoFinalPosY)
                            splashLogo.position.Y -= 2;
                    }
                    break;
                case State.THREE:
                    timeStateThree -= deltaTime;
                    if (timeStateThree <= 0)
                        currentState = State.FOUR;
                    else
                    {
                        if (qrTransp < 254)
                        {
                            qrTransp += 2;
                            splashQr.SetTransparency(qrTransp);
                        }
                    }
                    break;
                case State.FOUR:

                    break;
            }
        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
            splashFondo.Draw(spriteBatch);

            switch (currentState)
            {
                case State.ONE:
                    splashLogo.Draw(spriteBatch);
                    break;
                case State.TWO:
                    splashEscudos.Draw(spriteBatch);
                    splashLogo.Draw(spriteBatch);
                    break;
                case State.THREE:
                    splashEscudos.Draw(spriteBatch);
                    splashQr.Draw(spriteBatch);
                    splashLogo.Draw(spriteBatch);
                    break;
                case State.FOUR:
                    splashEscudos.Draw(spriteBatch);
                    splashQr.Draw(spriteBatch);
                    splashWeb.Draw(spriteBatch);
                    splashLogo.Draw(spriteBatch);
                    break;
            }
        } // Draw

    } // class SplashFinalDemo
}
