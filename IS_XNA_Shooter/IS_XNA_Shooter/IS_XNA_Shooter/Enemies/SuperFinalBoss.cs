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

        private SpriteCamera animIddle1, animIddle2, animIddle3;
        private ComplexAnimation animIddle4; // abriendo la boca
        private short animIddle4FramesCount = 4;
        private ComplexAnimation animOpenChest;
        private short animOpenChestFramesCount = 14;
        private ComplexAnimation animFiringLaser;
        private SpriteCamera turretTexture;

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
            animIddle1 = new SpriteCamera(camera, level, true, position, rotation, textureAnim1, new Rectangle(0, 0, 306, 432));
            // iddle2 fede face
            animIddle2 = new SpriteCamera(camera, level, true, position, rotation, textureAnim1, new Rectangle(306, 0, 306, 432));
            // iddle3 fede face 2
            animIddle3 = new SpriteCamera(camera, level, true, position, rotation, textureAnim1, new Rectangle(612, 0, 306, 432));

            // iddle2 fede face 3 bostezando
            AnimRect[] animArray1 = new AnimRect[animIddle4FramesCount];
            animArray1[0] = new AnimRect(612, 0, 306, 432, textureAnim1);
            animArray1[1] = new AnimRect(0, 432, 306, 432, textureAnim1);
            animArray1[2] = new AnimRect(306, 432, 306, 432, textureAnim1);
            animArray1[3] = new AnimRect(612, 0, 306, 432, textureAnim1);
            animIddle4 = new ComplexAnimation(camera, level, true, position, rotation, animArray1,
                animIddle4FramesCount, false, SuperGame.frameTime12);

            // opening chest
            AnimRect[] animArray2 = new AnimRect[animOpenChestFramesCount];
            animArray2[0] = new AnimRect(0, 0, 306, 432, textureAnim2);
            animArray2[1] = new AnimRect(306, 0, 306, 432, textureAnim2);
            animArray2[2] = new AnimRect(612, 0, 306, 432, textureAnim2);
            animArray2[3] = new AnimRect(0, 432, 306, 432, textureAnim2);
            animArray2[4] = new AnimRect(306, 432, 306, 432, textureAnim2);
            animArray2[5] = new AnimRect(612, 432, 306, 432, textureAnim2);
            animArray2[6] = new AnimRect(0, 0, 306, 432, textureAnim3);
            animArray2[7] = new AnimRect(306, 0, 306, 432, textureAnim3);
            animArray2[8] = new AnimRect(612, 0, 306, 432, textureAnim3);
            animArray2[9] = new AnimRect(0, 432, 306, 432, textureAnim3);
            animArray2[10] = new AnimRect(306, 432, 306, 432, textureAnim3);
            animArray2[11] = new AnimRect(612, 432, 306, 432, textureAnim3);
            animArray2[12] = new AnimRect(0, 0, 306, 432, textureAnim4);
            animArray2[13] = new AnimRect(306, 0, 306, 432, textureAnim4);
            animOpenChest = new ComplexAnimation(camera, level, true, position, rotation, animArray2,
                animOpenChestFramesCount, true, SuperGame.frameTime8);


            // firing laser
            /*animFiringLaser = new List<AnimRect>();
            AnimRect rect19 = new AnimRect(306, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect19);
            AnimRect rect20 = new AnimRect(612, 0, 306, 432, textureAnim2);
            animOpenChest.Add(rect20);
            AnimRect rect21 = new AnimRect(0, 432, 306, 432, textureAnim2);
            animOpenChest.Add(rect21);*/

        } // SuperFinalBoss

        public override void Update(float deltaTime)
        {
            //base.Update(deltaTime);
            switch (currentState)
            {
                case State.ONE:
                    animOpenChest.Update(deltaTime);
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
                    /*spriteBatch.Draw(animIddle1.texture, position, animIddle1.rect, Color.White,
                        rotation, drawPoint, Program.scale, SpriteEffects.None, 0);*/
                    //animIddle1.DrawRectangle(spriteBatch);
                    animOpenChest.Draw(spriteBatch);
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
