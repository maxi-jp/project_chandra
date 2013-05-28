using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class SuperFinalBoss : Enemy
    {
        // graphic resources
        private Texture2D textureAnim1, textureAnim2, textureAnim3, textureAnim4;
        private class AnimRect
        {
            public Rectangle rect;
            public Texture2D texture;

            public AnimRect(int X, int Y, int W, int H, Texture2D texture)
            {
                this.rect = new Rectangle(X, Y, W, H);
                this.texture = texture;
            }
        } // class AnimRect
        private AnimRect animIddle1, animIddle2, animIddle3;
        private List<AnimRect> animIddle4; // abriendo la boca
        private List<AnimRect> animOpenChest;
        private List<AnimRect> animFiringLaser;
        private AnimRect turretTexture;

        private enum State
        {
            ONE,    // iddle1
            TWO,    // iddle2 fede face
            THREE,  // iddle3 fede face 2
            FOUR,   // opening chest
            FIVE,   // firing laser
            SIX
        };
        private State currentState;

        public SuperFinalBoss(Camera camera, Level level)
            : base(camera, level, Vector2.Zero, 0, 0, 0, 0, null, null, SuperGame.frameTime12,
            GRMng.textureSuperFinalBoss1, 0, 0, 100000, 99999, null)
        {
            currentState = State.ONE;

            position = new Vector2(SuperGame.screenWidth - 200, SuperGame.screenHeight / 2);
            drawPoint = new Vector2(306 / 2, 432 / 2);

            textureAnim1 = GRMng.textureSuperFinalBoss1;
            textureAnim2 = GRMng.textureSuperFinalBoss2;
            textureAnim3 = GRMng.textureSuperFinalBoss3;
            textureAnim4 = GRMng.textureSuperFinalBoss4;

            // initialize animations:
            // iddle1
            animIddle1 = new AnimRect(0, 0, 306, 432, textureAnim1);
            // iddle2 fede face
            animIddle2 = new AnimRect(306, 0, 306, 432, textureAnim1);
            // iddle3 fede face 2
            animIddle3 = new AnimRect(612, 0, 306, 432, textureAnim1);

            // iddle2 fede face 3 bostezando
            animIddle4 = new List<AnimRect>();
            AnimRect rect1 = new AnimRect(612, 0, 306, 432, textureAnim1);
            animIddle4.Add(rect1);
            AnimRect rect2 = new AnimRect(0, 432, 306, 432, textureAnim1);
            animIddle4.Add(rect2);
            AnimRect rect3 = new AnimRect(306, 432, 306, 432, textureAnim1);
            animIddle4.Add(rect3);

            // opening chest
            animOpenChest = new List<AnimRect>();
            AnimRect rect5 = new AnimRect(0, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect5);
            AnimRect rect6 = new AnimRect(306, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect6);
            AnimRect rect7 = new AnimRect(612, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect7);
            AnimRect rect8 = new AnimRect(0, 432, 306, 432, textureAnim2);
            animOpenChest.Add(rect8);
            AnimRect rect9 = new AnimRect(306, 432, 306, 432, textureAnim2);
            animOpenChest.Add(rect9);
            AnimRect rect10 = new AnimRect(612, 432, 306, 432, textureAnim2);
            animOpenChest.Add(rect10);
            AnimRect rect11 = new AnimRect(0, 0, 306, 432, textureAnim3);
            animOpenChest.Add(rect11);
            AnimRect rect12 = new AnimRect(306, 0, 306, 432, textureAnim3);
            animOpenChest.Add(rect12);
            AnimRect rect13 = new AnimRect(612, 0, 306, 432, textureAnim3);
            animOpenChest.Add(rect13);
            AnimRect rect14 = new AnimRect(0, 432, 306, 432, textureAnim3);
            animOpenChest.Add(rect14);
            AnimRect rect15 = new AnimRect(306, 432, 306, 432, textureAnim3);
            animOpenChest.Add(rect15);
            AnimRect rect16 = new AnimRect(612, 432, 306, 432, textureAnim3);
            animOpenChest.Add(rect16);
            AnimRect rect17 = new AnimRect(0, 0, 306, 432, textureAnim4);
            animOpenChest.Add(rect17);
            AnimRect rect18 = new AnimRect(306, 0, 306, 432, textureAnim4);
            animOpenChest.Add(rect18);

            // firing laser
            animFiringLaser = new List<AnimRect>();
            AnimRect rect19 = new AnimRect(306, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect19);
            AnimRect rect20 = new AnimRect(612, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect20);
            AnimRect rect21 = new AnimRect(0, 432, 306, 432, textureAnim2);
            animOpenChest.Add(rect21);

        } // SuperFinalBoss

        public override void Update(float deltaTime)
        {
            //base.Update(deltaTime);
            switch (currentState)
            {
                case State.ONE:

                    break;
                case State.TWO:

                    break;
                case State.THREE:

                    break;
                case State.FOUR:

                    break;
                case State.FIVE:

                    break;
                case State.SIX:

                    break;
            } // switch
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            switch (currentState)
            {
                case State.ONE:
                    spriteBatch.Draw(animIddle1.texture, position, animIddle1.rect, Color.White,
                        rotation, drawPoint, Program.scale, SpriteEffects.None, 0);
                    break;
                case State.TWO:

                    break;
                case State.THREE:

                    break;
                case State.FOUR:

                    break;
                case State.FIVE:

                    break;
                case State.SIX:

                    break;
            } // switch
        } // Draw

    } // class SuperFinalBoss
}
