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
        private float turretRotationVelocity = 0.3f;
        private float maxTurretRotation = 0.25f * (float)Math.PI;
        private float minTurretRotation = -0.2f * (float)Math.PI;

        // disparos torreta
        private List<Shot> turretShots;
        private float turretShotVelocity = 300;
        private float timeToTurretShot = 0.6f, timeToTurretShot2 = 0.4f, timeToTurretShotAux = 0.4f;
        private int turretShotPower = 100;

        // brazo
        private SpriteCamera armTexture;
        private Vector2 armPosition = new Vector2(-153, -95);
        private float armRotationVelocity = 0.3f;
        private float maxArmRotation = 0.6f * (float)Math.PI;

        // texturas texto
        private SpriteCamera text1; // my name is github montoya
        private SpriteCamera text2; // you erased my repository
        private SpriteCamera text3; // prepare to die!
        private SpriteCamera text4; // this isn't even my final form

        //For the Laser
        /// <summary>
        /// Laser's rectanlge
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// Laser's actual points 
        /// </summary>
        private Vector2 p1, p2, p3;

        /// <summary>
        /// Laser's original points
        /// </summary>
        private Vector2 p1Orig, p2Orig, p3Orig;

        /// <summary>
        /// Time between shots
        /// </summary>
        private float timeToShot = 1.0f;

        /// <summary>
        /// Shot power
        /// </summary>
        private int shotPower = 1000;

        /// <summary>
        /// Enemy's shot, in this case a laser
        /// </summary>
        private Shot laser;

        /// <summary>
        /// Time to set the shot
        /// </summary>
        private float shootingCont = 0.1f;

        /// <summary>
        /// To set shootingCont only once
        /// </summary>
        private Boolean shootingContSet = false;

        /// <summary>
        /// Says if it has to shoot or not
        /// </summary>
        private Boolean shootingLaser = false;

        /// <summary>
        /// Rotatio Matrix for the laser's points
        /// </summary>
        private Matrix rotationMatrix;

        private enum State
        {
            ENTERING,
            SPEAKING1,
            ONE,    // primera fase de ataque
            TWO,    // segunda fase de ataque (con laser)
            THREE,  // se marcha
            FOUR,   // vuelve convertido en fede1
            FIVE,   // tercera fase de ataque
            SIX,
            SEVEN,
            EIGHT,
            NINE
        };
        private State currentState;

        private enum ArmState
        {
            MOVING_UP,
            MOVING_DOWN,
            STOP
        };
        private ArmState currentArmState;

        private enum TurretState
        {
            MOVING_UP,
            MOVING_DOWN,
            STOP
        };
        private TurretState currentTurretState;

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
            currentTurretState = TurretState.STOP;

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

            turretTexture = new SpriteCamera(camera, level, true, position, 0, textureAnim1,
                new Rectangle(918, 0, 64, 64));
            //turretTexture.SetDrawPoint(new Vector2());

            turretShots = new List<Shot>();

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

            //For the Laser
            Rectangle rect = new Rectangle(0, 0, 2000, 2);
            p1 = new Vector2();
            p2 = new Vector2();
            p3 = new Vector2();

            p1Orig = new Vector2(175, 0);
            p2Orig = new Vector2(495, 0);
            p3Orig = new Vector2(775, 0);

            laser = new Shot(camera, level, p1, armTexture.rotation , GRMng.frameWidthELBulletA, GRMng.frameHeightELBullet,
                GRMng.numAnimsELBullet, GRMng.frameCountELBullet, GRMng.loopingELBullet, SuperGame.frameTime8,
                GRMng.textureELBullet, SuperGame.shootType.normal, 100, shotPower);

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
            else if (ControlMng.f9Preshed)
                ChangeState(State.SEVEN);
            else if (ControlMng.f10Preshed)
                ChangeState(State.EIGHT);
            else if (ControlMng.f11Preshed)
                ChangeState(State.NINE);

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

            switch (currentTurretState)
            {
                case TurretState.MOVING_UP:
                    turretTexture.rotation += turretRotationVelocity * deltaTime;
                    if (turretTexture.rotation >= maxTurretRotation)
                        currentTurretState = TurretState.MOVING_DOWN;
                    break;
                case TurretState.MOVING_DOWN:
                    turretTexture.rotation -= turretRotationVelocity * deltaTime;
                    if (turretTexture.rotation <= minTurretRotation)
                        currentTurretState = TurretState.MOVING_UP;
                    break;
            } // switch (currentTurretState)

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
                case State.ONE: // primera fase de ataque
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;
                    animIddle1.position = position;
                    animIddle1.rotation = rotation;

                    timeToTurretShotAux -= deltaTime;
                    if (timeToTurretShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, turretTexture.position, turretTexture.rotation + (float)Math.PI,
                            GRMng.frameWidthL1, GRMng.frameHeightL1, GRMng.numAnimsL1, GRMng.frameCountL1,
                            GRMng.loopingL1, SuperGame.frameTime10, GRMng.textureL1, SuperGame.shootType.normal,
                            turretShotVelocity, turretShotPower);
                        turretShots.Add(shot);
                        Audio.PlayEffect("laserShot02");
                        timeToTurretShotAux = timeToTurretShot;
                    }
                    break;
                case State.TWO: // segunda fase de ataque (con laser)
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;
                    animIddle1.position = position;
                    animIddle1.rotation = rotation;

                    timeToTurretShotAux -= deltaTime;
                    if (timeToTurretShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, turretTexture.position, turretTexture.rotation + (float)Math.PI,
                            GRMng.frameWidthL1, GRMng.frameHeightL1, GRMng.numAnimsL1, GRMng.frameCountL1,
                            GRMng.loopingL1, SuperGame.frameTime10, GRMng.textureL1, SuperGame.shootType.normal,
                            turretShotVelocity, turretShotPower);
                        turretShots.Add(shot);
                        Audio.PlayEffect("laserShot02");
                        timeToTurretShotAux = timeToTurretShot;
                    }
                    
                    timeToShot -= deltaTime;

                	if (shootingLaser)//It shoots if it has to
                	{
                    LaserShot();
                    laser.Update(deltaTime);

                    rotation += 0.2f * deltaTime;//Rotates slowly

                }
                else if (timeToShot > 1)//Spin
                    rotation += 0.45f * deltaTime;
                else if (timeToShot > 0)//Prepare to shoot
                {
                    if (currentAnim != 1) setAnim(1);

                    rotation += 0.2f * deltaTime;//Rotates slowly
                }
                if (timeToShot <= 0)
                {
                    setAnim(0);
                    timeToShot = 1.0f;
                    shootingLaser = !shootingLaser;
                    if (shootingLaser)
                    {
                        if (!shootingContSet)
                        {
                            shootingContSet = true;
                            shootingCont = 0.1f;
                        }
                        LaserShot();
                    }
                }
            if (shootingLaser)// shots
            {
                laser.Update(deltaTime);
                //shot-player colisions
                if (shootingCont >= 0)
                    shootingCont -= deltaTime;
                else
                    if (ship.collider.CollisionTwoPoints(p1, p3))
                    {
                        // the player is hitted:
                        ship.Damage(laser.GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        /*if (ship.GetLife() > 0)
                            shots.RemoveAt(i);*/
                    }
            }

            
                    break;
                case State.THREE: // se marcha
                    position.X += deltaTime * enteringVelocity * 1.8f;
                    position.Y += (float)Math.Sin(position.Y * 10);

                    animIddle1.position = position;
                    animIddle1.rotation = rotation;
                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;

                    if (position.X >= initialPosition.X)
                        ChangeState(State.THREE, State.FOUR);
                    break;
                case State.FOUR: // ENTRA FEDE
                    position.X -= deltaTime * enteringVelocity * 2.0f;
                    position.Y += (float)Math.Sin(position.Y * 10);

                    animIddle2.position = position;
                    animIddle2.rotation = rotation;

                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;

                    if (position.X <= basePosition.X)
                        ChangeState(State.FOUR, State.FIVE);
                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;

                    if (position.X <= basePosition.X)
                        ChangeState(State.FOUR, State.FIVE);

                }
                case State.FIVE: // FEDE ATACA
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;
                    animIddle3.position = position;
                    animIddle3.rotation = rotation;

                    timeToTurretShotAux -= deltaTime;
                    if (timeToTurretShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, turretTexture.position, turretTexture.rotation + (float)Math.PI,
                            GRMng.frameWidthL1, GRMng.frameHeightL1, GRMng.numAnimsL1, GRMng.frameCountL1,
                            GRMng.loopingL1, SuperGame.frameTime10, GRMng.textureL1, SuperGame.shootType.normal,
                            turretShotVelocity, turretShotPower);
                        turretShots.Add(shot);
                        Audio.PlayEffect("laserShot02");
                        timeToTurretShotAux = timeToTurretShot2;
                    }
                    break;
                case State.SIX:
                    animIddle4.Update(deltaTime);
                    break;
                case State.SEVEN:
                    animOpenChest.Update(deltaTime);
                    break;
                case State.EIGHT:
                    animFiringLaser.Update(deltaTime);
                    break;
            } // switch

            if (shootingLaser)// shots
            {
                laser.Update(deltaTime);
                //shot-player colisions
                if (shootingCont >= 0)
                    shootingCont -= deltaTime;
                else
                    if (ship.collider.CollisionTwoPoints(p1, p3))
                    {
                        // the player is hitted:
                        ship.Damage(laser.GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        /*if (ship.GetLife() > 0)
                            shots.RemoveAt(i);*/
                    }
            }
                 
                case State.FIVE: // FEDE ATACA
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;

                    timeToTurretShotAux -= deltaTime;
                    if (timeToTurretShotAux <= 0)
                    {
                        Shot shot = new Shot(camera, level, turretTexture.position, turretTexture.rotation + (float)Math.PI,
                            GRMng.frameWidthL1, GRMng.frameHeightL1, GRMng.numAnimsL1, GRMng.frameCountL1,
                            GRMng.loopingL1, SuperGame.frameTime10, GRMng.textureL1, SuperGame.shootType.normal,
                            turretShotVelocity, turretShotPower);
                        turretShots.Add(shot);
                        Audio.PlayEffect("laserShot02");
                        timeToTurretShotAux = timeToTurretShot2;
                    }
                case State.SIX:
                case State.SEVEN:
                case State.EIGHT:

            // shots:
            for (int i = 0; i < turretShots.Count(); i++)
            {
                turretShots[i].Update(deltaTime);
                if (!turretShots[i].IsActive())
                    turretShots.RemoveAt(i);
                else  // shots-player colisions
                {
                    if (ship.collider.Collision(turretShots[i].position))
                    {
                        // the player is hit:
                        ship.Damage(turretShots[i].GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        if (ship.GetLife() > 0)
                            turretShots.RemoveAt(i);
                    }

                }
            }

            collider.Update(position, rotation);
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);

            foreach (Shot s in turretShots)
                s.Draw(spriteBatch);

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
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.THREE:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.FOUR:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle2.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.FIVE:
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle2.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.SIX:
                    animIddle3.DrawRectangle(spriteBatch);
                    break;
                case State.SEVEN:
                    animIddle4.Draw(spriteBatch);
                    break;
                case State.EIGHT:
                    animOpenChest.Draw(spriteBatch);
                    break;
                case State.NINE:
                    animFiringLaser.Draw(spriteBatch);
                    break;
            } // switch

            if (SuperGame.debug)
                collider.Draw(spriteBatch);

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
                currentTurretState = TurretState.MOVING_UP;
            }
            else if (prevState == State.ONE && nextState == State.TWO)
            {
                currentState = State.TWO;
            }
            else if (prevState == State.TWO && nextState == State.THREE)
            {
                currentTurretState = TurretState.STOP;
                currentArmState = ArmState.STOP;
                currentState = State.THREE;
            }
            else if (prevState == State.THREE && nextState == State.FOUR)
            {
                animIddle2.position = position;
                animIddle2.rotation = rotation;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                currentState = State.FOUR;
            }
            else if (prevState == State.FOUR && nextState == State.FIVE)
            {
                currentTurretState = TurretState.MOVING_UP;
                currentArmState = ArmState.MOVING_UP;
                currentState = State.FIVE;
            }
        } // ChangeState

        private void ChangeState(State nextState)
        {
            if (nextState == State.ENTERING)
            {
                position = initialPosition;
                currentArmState = ArmState.STOP;
                currentTurretState = TurretState.STOP;
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
                currentTurretState = TurretState.STOP;
            }
            else if (nextState == State.ONE)
            {
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                position = basePosition;
            }
            else if (nextState == State.TWO)
            {
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                position = basePosition;
            }
            else if (nextState == State.THREE)
            {
                currentTurretState = TurretState.STOP;
                currentArmState = ArmState.STOP;
            }
            else if (nextState == State.FOUR)
            {
                currentTurretState = TurretState.STOP;
                currentArmState = ArmState.STOP;
            }
            else if (nextState == State.FIVE)
            {
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                position = basePosition;
            }
            else
                position = basePosition;
            currentState = nextState;
        }

        /// <summary>
        /// Kills the enemy and its shots
        /// </summary>
        public override void Kill()
        {
            base.Kill();

            turretShots.Clear();
        }

        /// <summary>
        /// Calculates the enemy's laser points and rectangle
        /// </summary>
        private void LaserShot()
        {
            //The calculation of the rectangle
            rotationMatrix = Matrix.CreateRotationZ(armTexture.rotation - (float)Math.PI * 3 / 2);
            int width = level.width + 200;

            p1 = Vector2.Transform(p1Orig, rotationMatrix);
            p1 += armTexture.position;

            p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 += armTexture.position;

            p3 = Vector2.Transform(p3Orig, rotationMatrix);
            p3 += armTexture.position;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            laser.position = p2;
            laser.rotation = armTexture.rotation - (float)Math.PI * 3 / 2;

        }
    } // class SuperFinalBoss
}
