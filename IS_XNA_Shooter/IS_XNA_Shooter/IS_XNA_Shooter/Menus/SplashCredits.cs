using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class SplashCredits
    {
        private SuperGame mainGame;

        private Texture2D textureBg;
        private Sprite spriteCredits;
        private Vector2 spriteInitialPosition;
        private float spriteFinalHeight;

        private float creditsVelocity;

        public SplashCredits(SuperGame mainGame)
        {
            this.mainGame = mainGame;
        }

        public void Initialize()
        {
            textureBg = GRMng.menuSplash01;
            spriteInitialPosition = new Vector2(SuperGame.screenWidth / 2,
                (SuperGame.screenHeight + GRMng.splash_credits_1.Height / 2) + 20);
            spriteFinalHeight = -(GRMng.splash_credits_1.Height / 2) - 40;
            spriteCredits = new Sprite(true, spriteInitialPosition, 0, GRMng.splash_credits_1);
            creditsVelocity = 70.0f;

            Audio.PlayMusic(7);
        }

        public void Update(float deltaTime)
        {
            spriteCredits.position.Y -= creditsVelocity * deltaTime;
            if (spriteCredits.position.Y <= spriteFinalHeight)
                mainGame.ReturnFromCredits();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureBg, Vector2.Zero, Color.White);
            spriteCredits.Draw(spriteBatch);
        }

    } // class SplashCredits
}
