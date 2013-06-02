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
        private ComplexAnimation animIddle5; // hablando
        private short animIddle5FramesCount = 3;
        private ComplexAnimation animOpenChest;
        private short animOpenChestFramesCount = 14;
        private ComplexAnimation animFiringLaser;
        private short animFiringLaserFramesCount = 4;

        // torreta
        private SpriteCamera turretTexture;
        private Vector2 turretPosition = new Vector2(-80, 125);
        private float turretRotation;

        // brazo
        private SpriteCamera armTexture;
        private Vector2 armPosition = new Vector2(-153, -95);
        private float armRotationVelocity = 0.5f;
        private float maxArmRotation = 0.8f * (float)Math.PI;

        // texturas texto
        private SpriteCamera text1; // my name is github montoya
        private SpriteCamera text2; // you erased my repository
        private SpriteCamera text3; // prepare to die!
        private SpriteCamera text4; // this isn't even my final form

        private enum State
        {
            ENTERING,
            SPEAKING1,
            ONE,    // iddle1
            TWO,    // iddle2 fede face
            THREE,  // iddle3 fede face 2
            FOUR,   // opening chest
            FIVE,   // firing laser
            SIX
        };
        private State currentState;

        private enum ArmState
        {
            MOVING_UP,
            MOVING_DOWN,
            STOP
        };
        private ArmState currentArmState;

        private Vector2 basePosition = new Vector2(SuperGame.screenWidth - 200, SuperGame.screenHeight / 2);
        private Vector2 initialPosition = new Vector2(SuperGame.screenWidth + 300, SuperGame.screenHeight / 2);
        private float enteringVelocity = 40;

        // contadores
        private float timeSpeaking1 = 0;
        private float timeSpeaking1End = 10.0f;
        private float timeLine1Start = 0.0f; // my name is github montoya
        private float timeLine1End = 2.5f;
        private float timeLine2Start = 3.5f; // you erased my repository
        private float timeLine2End = 2.5f;
        private float timeLine3Start = 6.0f; // prepare to die!
        private float timeLine3End = 3.0f;
        private float timeLine4Start; // this isn't even my final form
        private float timeLine4End = 2.0f;

        // animaciones aclarar
        private byte transpLine1 = 0;
        private bool line1IsFading = false;
        private byte transpLine2 = 0;
        private bool line2IsFading = false;
        private byte transpLine3 = 0;
        private bool line3IsFading = false;
        private byte transpLine4 = 0;
        private bool line4IsFading = false;

        private Collider collider;

        public SuperFinalBoss(Camera camera, Level level)
            : base(camera, level, Vector2.Zero, 0, 0, 0, 0, null, null, SuperGame.frameTime12,
            GRMng.textureSuperFinalBoss1, 0, 0, 100000, 99999, null)
        {
            currentState = State.ENTERING;
            currentArmState = ArmState.STOP;

            position = initialPosition;
            drawPoint = new Vector2(306 / 2, 432 / 2);

            textureAnim1 = GRMng.textureSuperFinalBoss1;
            textureAnim2 = GRMng.textureSuperFinalBoss2;
            textureAnim3 = GRMng.textureSuperFinalBoss3;
            textureAnim4 = GRMng.textureSuperFinalBoss4;

            // initialize animations:
            // iddle1
            animIddle1 = new SpriteCamera(camera, level, true, position, rotation, textureAnim1, new Rectangle(0, 0, 306, 432));
            // iddle2 fede face
            animIddle2 = new SpriteCamera(camera, level, true, basePosition, rotation, textureAnim1, new Rectangle(306, 0, 306, 432));
            // iddle3 fede face 2
            animIddle3 = new SpriteCamera(camera, level, true, basePosition, rotation, textureAnim1, new Rectangle(612, 0, 306, 432));

            // iddle2 fede face 3 bostezando
            AnimRect[] animArray1 = new AnimRect[animIddle4FramesCount];
            animArray1[0] = new AnimRect(612, 0, 306, 432, textureAnim1);
            animArray1[1] = new AnimRect(0, 432, 306, 432, textureAnim1);
            animArray1[2] = new AnimRect(306, 432, 306, 432, textureAnim1);
            animArray1[3] = new AnimRect(612, 0, 306, 432, textureAnim1);
            animIddle4 = new ComplexAnimation(camera, level, true, basePosition, rotation, animArray1,
                animIddle4FramesCount, true, SuperGame.frameTime12);

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
            animOpenChest = new ComplexAnimation(camera, level, true, basePosition, rotation, animArray2,
                animOpenChestFramesCount, true, SuperGame.frameTime8);

            // firing laser
            AnimRect[] animArray3 = new AnimRect[animFiringLaserFramesCount];
            animArray3[0] = new AnimRect(306, 0, 306, 432, textureAnim4);
            animArray3[1] = new AnimRect(612, 0, 306, 432, textureAnim4);
            animArray3[2] = new AnimRect(0, 432, 306, 432, textureAnim4);
            animArray3[3] = new AnimRect(306, 0, 306, 432, textureAnim4);
            animFiringLaser = new ComplexAnimation(camera, level, true, basePosition, rotation, animArray3,
                animFiringLaserFramesCount, true, SuperGame.frameTime12);

            // talking
            AnimRect[] animArray5 = new AnimRect[animIddle5FramesCount];
            animArray5[0] = new AnimRect(0, 0, 306, 432, textureAnim1);
            animArray5[1] = new AnimRect(306, 432, 306, 432, textureAnim4);
            animArray5[2] = new AnimRect(612, 432, 306, 432, textureAnim4);
            animIddle5 = new ComplexAnimation(camera, level, true, basePosition, rotation, animArray5,
                animIddle5FramesCount, true, SuperGame.frameTime12);

            turretTexture = new SpriteCamera(camera, level, true, position, turretRotation, textureAnim1,
                new Rectangle(918, 0, 64, 64));
            //turretTexture.SetDrawPoint(new Vector2());

            armTexture = new SpriteCamera(camera, level, true, position, 0, textureAnim1,
                new Rectangle(612, 432, 77, 596));
            //armTexture.SetDrawPoint(new Vector2(661, 465));
            armTexture.SetDrawPoint(new Vector2(49, 33));

            text1 = new SpriteCamera(camera, level, true, new Vector2(basePosition.X - 400, basePosition.Y - 210),
                0, textureAnim1, new Rectangle(0, 864, 550, 16));
            text1.SetTransparency(transpLine1);
            text2 = new SpriteCamera(camera, level, true, new Vector2(basePosition.X - 400, basePosition.Y - 180),
                0, textureAnim1, new Rectangle(0, 880, 550, 16));
            text2.SetTransparency(transpLine2);
            text3 = new SpriteCamera(camera, level, true, new Vector2(basePosition.X - 400, basePosition.Y - 150),
                0, textureAnim1, new Rectangle(0, 896, 550, 16));
            text3.SetTransparency(transpLine3);
            text4 = new SpriteCamera(camera, level, true, new Vector2(basePosition.X - 400, basePosition.Y - 180),
                0, textureAnim1, new Rectangle(0, 912, 550, 74));
            text4.SetTransparency(transpLine4);

            // Collider
            Vector2[] points = new Vector2[19];
            points[0] = new Vector2(52, 431);
            points[1] = new Vector2(21, 406);
            points[2] = new Vector2(57, 372);
            points[3] = new Vector2(57, 307);
            points[4] = new Vector2(36, 304);
            points[5] = new Vector2(37, 254);
            points[6] = new Vector2(1, 195);
            points[7] = new Vector2(0, 121);
            points[8] = new Vector2(20, 90);
            points[9] = new Vector2(27, 74);
            points[10] = new Vector2(49, 60);
            points[11] = new Vector2(51, 30);
            points[12] = new Vector2(76, 2);
            points[13] = new Vector2(104, 0);
            points[14] = new Vector2(132, 8);
            points[15] = new Vector2(256, 73);
            points[16] = new Vector2(305, 126);
            points[17] = new Vector2(305, 286);
            points[18] = new Vector2(155, 421);
            float radius = (float)Math.Sqrt(306 / 2 * 306 / 2 + 612 / 2 * 612 / 2);
            collider = new Collider(camera, true, basePosition, rotation, points, radius, 306, 432);

        } // SuperFinalBoss

        public override void Update(float deltaTime)
        {
            //base.Update(deltaTime);

            if (ControlMng.f1Preshed)
                ChangeState(State.ENTERING);
            else if (ControlMng.f2Preshed)
                ChangeState(State.SPEAKING1);
            else if (ControlMng.f3Preshed)
                ChangeState(State.ONE);
            else if (ControlMng.f4Preshed)
                ChangeState(State.TWO);
            else if (ControlMng.f5Preshed)
               ChangeState(State.THREE);
            else if (ControlMng.f6Preshed)
                ChangeState(State.FOUR);
            else if (ControlMng.f7Preshed)
                ChangeState(State.FIVE);
            else if (ControlMng.f8Preshed)
                ChangeState(State.SIX);

            switch (currentArmState)
            {
                case ArmState.MOVING_UP:
                    armTexture.rotation += armRotationVelocity * deltaTime;
                    if (armTexture.rotation >= maxArmRotation)
                        currentArmState = ArmState.MOVING_DOWN;
                    break;
                case ArmState.MOVING_DOWN:
                    armTexture.rotation -= armRotationVelocity * deltaTime;
                    if (armTexture.rotation <= 0)
                        currentArmState = ArmState.MOVING_UP;
                    break;
            } // switch (currentArmState)

            switch (currentState)
            {
                case State.ENTERING:
                    position.X -= deltaTime * enteringVelocity;
                    position.Y += (float)Math.Sin(position.Y * 10);

                    animIddle1.position = position;
                    animIddle1.rotation = rotation;

                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;

                    if (position.X <= basePosition.X)
                        ChangeState(State.ENTERING, State.SPEAKING1);
                    break;
                case State.SPEAKING1:
                    //position.Y += (float)Math.Sin(position.Y * 5);
                    timeSpeaking1 += deltaTime;

                    // lineas de texto
                    if (line1IsFading)
                    {   // esconder texto
                        if (transpLine1 > 1)
                            transpLine1 -= 2;
                        else
                        {
                            transpLine1 = 0;
                            line1IsFading = false;
                        }
                        text1.SetTransparency(transpLine1);
                    }
                    else if ((timeSpeaking1 >= timeLine1Start) && (timeSpeaking1 < timeLine1End + timeLine1Start))
                    {   // presentar texto
                        if (transpLine1 < 252)
                            transpLine1 += 4;
                        else
                            transpLine1 = 255;
                        text1.SetTransparency(transpLine1);
                    }
                    else if (timeSpeaking1 >= timeLine1Start + timeLine1End)
                        line1IsFading = true;

                    if (line2IsFading)
                    {   // esconder texto
                        if (transpLine2 > 1)
                            transpLine2 -= 2;
                        else
                        {
                            transpLine2 = 0;
                            line2IsFading = false;
                        }
                        text2.SetTransparency(transpLine2);
                    }
                    else if ((timeSpeaking1 >= timeLine2Start) && (timeSpeaking1 < timeLine2End + timeLine2Start))
                    {   // presentar texto
                        if (transpLine2 < 252)
                            transpLine2 += 4;
                        else
                            transpLine2 = 255;
                        text2.SetTransparency(transpLine2);
                    }
                    else if (timeSpeaking1 >= timeLine2Start + timeLine2End)
                        line2IsFading = true;

                    if (line3IsFading)
                    {   // esconder texto
                        if (transpLine3 > 1)
                            transpLine3 -= 2;
                        else
                        {
                            transpLine3 = 0;
                            line3IsFading = false;
                        }
                        text3.SetTransparency(transpLine3);
                    }
                    else if ((timeSpeaking1 >= timeLine3Start) && (timeSpeaking1 < timeLine3End + timeLine3Start))
                    {   // presentar texto
                        if (transpLine3 < 252)
                            transpLine3 += 4;
                        else
                            transpLine3 = 255;
                        text3.SetTransparency(transpLine3);
                    }
                    else if (timeSpeaking1 >= timeLine3Start + timeLine3End)
                        line3IsFading = true;

                    animIddle5.position = position;
                    animIddle5.rotation = rotation;

                    armTexture.position = position + armPosition;
                    //armTexture.rotation += (float)Math.Sin(deltaTime / 10);
                    turretTexture.position = position + turretPosition;
                    //turretTexture.rotation += deltaTime * 10;

                    animIddle5.Update(deltaTime);

                    if (timeSpeaking1 >= timeSpeaking1End)
                        ChangeState(State.SPEAKING1, State.ONE);
                    break;
                case State.ONE:

                    armTexture.position = position + armPosition;
                    animIddle1.position = position;
                    animIddle1.rotation = rotation;
                    break;
                case State.TWO:
                    animIddle2.position = position;
                    animIddle2.rotation = rotation;
                    break;
                case State.THREE:
                    animIddle3.position = position;
                    animIddle3.rotation = rotation;
                    break;
                case State.FOUR:
                    animIddle4.Update(deltaTime);
                    break;
                case State.FIVE:
                    animOpenChest.Update(deltaTime);
                    break;
                case State.SIX:
                    animFiringLaser.Update(deltaTime);
                    break;
            } // switch

            collider.Update(position, rotation);
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            switch (currentState)
            {
                case State.ENTERING:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.SPEAKING1:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle5.Draw(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    text1.DrawRectangle(spriteBatch);
                    text2.DrawRectangle(spriteBatch);
                    text3.DrawRectangle(spriteBatch);
                    break;
                case State.ONE:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.TWO:
                    animIddle2.DrawRectangle(spriteBatch);
                    break;
                case State.THREE:
                    animIddle3.DrawRectangle(spriteBatch);
                    break;
                case State.FOUR:
                    animIddle4.Draw(spriteBatch);
                    break;
                case State.FIVE:
                    animOpenChest.Draw(spriteBatch);
                    break;
                case State.SIX:
                    animFiringLaser.Draw(spriteBatch);
                    break;
            } // switch

            /*if (SuperGame.debug)
                collider.Draw(spriteBatch);*/

        } // Draw

        private void ChangeState(State prevState, State nextState)
        {
            if (prevState == State.ENTERING && nextState == State.SPEAKING1)
            {
                animIddle5.position = position;
                timeSpeaking1 = 0;
                currentState = State.SPEAKING1;
                currentArmState = ArmState.MOVING_UP;
            }
            else if (prevState == State.SPEAKING1 && nextState == State.ONE)
            {
                currentState = State.ONE;
            }
        } // ChangeState

        private void ChangeState(State nextState)
        {
            if (nextState == State.ENTERING)
            {
                position = initialPosition;
                currentArmState = ArmState.STOP;
            }
            else if (nextState == State.SPEAKING1)
            {
                timeSpeaking1 = 0;
                transpLine1 = 0;
                text1.SetTransparency(transpLine1);
                transpLine2 = 0;
                text2.SetTransparency(transpLine2);
                transpLine3 = 0;
                text3.SetTransparency(transpLine3);
                line1IsFading = false;
                line2IsFading = false;
                line3IsFading = false;
                position = basePosition;
                armTexture.rotation = 0;
                currentArmState = ArmState.MOVING_UP;
            }
            else
                position = basePosition;
            currentState = nextState;
        }

    } // class SuperFinalBoss
}
