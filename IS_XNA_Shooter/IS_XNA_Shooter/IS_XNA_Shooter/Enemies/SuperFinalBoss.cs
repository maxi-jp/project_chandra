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
        /// Time between laser shots
        /// </summary>
        private float timeToShotLaser = 1.8f, timeToShotLaserAux = 1.8f;

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
            FIVE,   // tercera fase de ataque (ataque como TWO pero un poco más rapido)
            SIX,    // le explota la cara
            SEVEN,  // dice "this is even my final form"
            EIGHT,  // animación de cara de samer saliendo
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

        private bool armNextStateStop = false;

        private enum TurretState
        {
            MOVING_UP,
            MOVING_DOWN,
            STOP
        };
        private TurretState currentTurretState;

        private int life1 = 20000;
        private int life2 = 30000;
        private int life3 = 35000;
        private int life4 = 30000;

        private Vector2 basePosition = new Vector2(SuperGame.screenWidth - 200, SuperGame.screenHeight / 2);
        private Vector2 initialPosition = new Vector2(SuperGame.screenWidth + 300, SuperGame.screenHeight / 2);
        private float enteringVelocity = 40;

        // contadores
        private float timeSpeaking1 = 0;
        private float timeSpeaking1End = 10.0f;
        private float timeSpeaking2 = 0;
        private float timeSpeaking2End = 4.0f;
        private float timeLine1Start = 0.0f; // my name is github montoya
        private float timeLine1End = 2.5f;
        private float timeLine2Start = 3.5f; // you erased my repository
        private float timeLine2End = 2.5f;
        private float timeLine3Start = 6.0f; // prepare to die!
        private float timeLine3End = 3.0f;
        private float timeLine4Start; // this isn't even my final form
        private float timeLine4End = 3.0f;
        private float timeFaceExploiting = 3.0f;
        private float timeSamerSaliendo = 3.0f;

        // animaciones aclarar
        private byte transpLine1 = 0;
        private bool line1IsFading = false;
        private byte transpLine2 = 0;
        private bool line2IsFading = false;
        private byte transpLine3 = 0;
        private bool line3IsFading = false;
        private byte transpLine4 = 0;
        private bool line4IsFading = false;

        // explosion de la cara:
        private Animation explosionFaceAnimation;
        private ParticleSystemCamera particles;
        private int particlesCount = 22;
        private Vector2 facePosition = new Vector2(-85, -135);

        //private Collider collider;

        public SuperFinalBoss(Camera camera, Level level)
            : base(camera, level, Vector2.Zero, 0, 0, 0, 0, null, null, SuperGame.frameTime12,
                GRMng.textureSuperFinalBoss1, 0, 0, 100000/*life*/, 99999/*value*/, null/*ship*/)
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
                animOpenChestFramesCount, false, 1.0f / 4.0f);

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

            explosionFaceAnimation = new Animation(camera, level, true, position, 0, GRMng.textureSuperFinalBossExplosion,
                GRMng.frameWidthSuperFinalBossExplosion, GRMng.frameHeightSuperFinalBossExplosion,
                GRMng.numAnimsSuperFinalBossExplosion, GRMng.frameCountSuperFinalBossExplosion,
                GRMng.loopingSuperFinalBossExplosion, SuperGame.frameTime12);
            Rectangle[] rectangles = new Rectangle[4];
            rectangles[0] = new Rectangle(0, 0, 64, 64);
            rectangles[1] = new Rectangle(64, 0, 64, 64);
            rectangles[2] = new Rectangle(0, 64, 64, 64);
            rectangles[3] = new Rectangle(64, 64, 64, 64);
            particles = new ParticleSystemCamera(camera, level, GRMng.textureSmoke01, rectangles, particlesCount, position);

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
                    if ((armTexture.rotation <= 0) && (!armNextStateStop))
                        currentArmState = ArmState.MOVING_UP;
                    else if ((armTexture.rotation <= 0) && (armNextStateStop))
                        currentArmState = ArmState.STOP;
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

                    // disparos de la torreta
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

                    // disparos de la torreta
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

                    // disparo laser
                    timeToShotLaserAux -= deltaTime;
                    if (shootingLaser)//It shoots if it has to
                    {
                        LaserShot();
                        laser.Update(deltaTime);

                        //rotation += 0.2f * deltaTime;//Rotates slowly
                    }
                    /*else if (timeToShotLaserAux > 1)//Spin
                        rotation += 0.45f * deltaTime;*/
                    else if (timeToShotLaserAux < 0.6f)//Prepare to shoot
                    {   // falta medio segundo para que dispare
                        armTexture.SetColor(255, 255, 96, 255);
                        /*if (currentAnim != 1)
                            setAnim(1);*/
                        //rotation += 0.2f * deltaTime;//Rotates slowly
                    }
                    if (timeToShotLaserAux <= 0)
                    {
                        //setAnim(0);
                        timeToShotLaserAux = timeToShotLaser;
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
                        else
                            armTexture.SetColor(255, 255, 255, 255);
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
                case State.FOUR: // vuelve convertido en fede1
                    position.X -= deltaTime * enteringVelocity * 2.0f;
                    position.Y += (float)Math.Sin(position.Y * 10);

                    animIddle2.position = position;
                    animIddle2.rotation = rotation;

                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;

                    if (position.X <= basePosition.X)
                        ChangeState(State.FOUR, State.FIVE);
                    break;
                case State.FIVE: // tercera fase de ataque (ataque como TWO pero un poco más rapido)
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;
                    animIddle3.position = position;
                    animIddle3.rotation = rotation;

                    // disparos torreta
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

                    // disparo laser
                    timeToShotLaserAux -= deltaTime;
                    if (shootingLaser)//It shoots if it has to
                    {
                        LaserShot();
                        laser.Update(deltaTime);

                        //rotation += 0.2f * deltaTime;//Rotates slowly
                    }
                    /*else if (timeToShotLaserAux > 1)//Spin
                        rotation += 0.45f * deltaTime;*/
                    else if (timeToShotLaserAux < 0.5f)//Prepare to shoot
                    {   // falta medio segundo para que dispare
                        armTexture.SetColor(255, 255, 96, 255);
                        /*if (currentAnim != 1)
                            setAnim(1);*/
                        //rotation += 0.2f * deltaTime;//Rotates slowly
                    }
                    if (timeToShotLaserAux <= 0)
                    {
                        //setAnim(0);
                        timeToShotLaserAux = timeToShotLaser;
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
                        else
                            armTexture.SetColor(255, 255, 255, 255);
                    }
                    break;
                case State.SIX: // le explota la cara
                    timeFaceExploiting -= deltaTime;
                    if (timeFaceExploiting <= 0)
                    {
                        ChangeState(State.SIX, State.SEVEN);
                    }
                    turretTexture.position = position + turretPosition;
                    armTexture.position = position + armPosition;
                    particles.Update(deltaTime, facePosition+position, /*rotation*/(float)Math.PI);
                    explosionFaceAnimation.position = facePosition + position;
                    explosionFaceAnimation.Update(deltaTime);

                    position.Y += (float)Math.Sin(position.Y * 10);

                    animIddle3.position = position;
                    animIddle3.rotation = rotation;

                    armTexture.position = position + armPosition;
                    turretTexture.position = position + turretPosition;
                    break;
                case State.SEVEN: // dice "this is even my final form"
                    timeSpeaking2 += deltaTime;

                    // lineas de texto
                    if (line4IsFading)
                    {   // esconder texto
                        if (transpLine4 > 1)
                            transpLine4 -= 2;
                        else
                        {
                            transpLine4 = 0;
                            line4IsFading = false;
                        }
                        text4.SetTransparency(transpLine4);
                    }
                    else if ((timeSpeaking2 >= timeLine4Start) && (timeSpeaking2 < timeLine4End + timeLine4Start))
                    {   // presentar texto
                        if (transpLine4 < 252)
                            transpLine4 += 4;
                        else
                            transpLine4 = 255;
                        text4.SetTransparency(transpLine4);
                    }
                    else if (timeSpeaking2 >= timeLine4Start + timeLine4End)
                        line4IsFading = true;

                    animIddle4.Update(deltaTime);

                    if (timeSpeaking2 >= timeSpeaking2End)
                        ChangeState(State.SEVEN, State.EIGHT);
                    break;
                case State.EIGHT: // animación de cara de samer saliendo
                    timeSamerSaliendo -= deltaTime;

                    if (timeSamerSaliendo <= 0)
                        ChangeState(State.EIGHT, State.NINE);

                    animOpenChest.Update(deltaTime);
                    break;
                case State.NINE:
                    animFiringLaser.Update(deltaTime);
                    break;
            } // switch

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

            // laser shot
            if (shootingLaser)
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

            collider.Update(position, rotation);
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);

            foreach (Shot s in turretShots)
                s.Draw(spriteBatch);

            if (shootingLaser)
                laser.Draw(spriteBatch);

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
                case State.ONE: // primera fase de ataque
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.TWO: // segunda fase de ataque (con laser)
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.THREE: // se marcha
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle1.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.FOUR: // vuelve convertido en fede1
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle2.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.FIVE: // tercera fase de ataque (ataque como TWO pero un poco más rapido)
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle2.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.SIX: // le explota la cara
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle3.DrawRectangle(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    particles.Draw(spriteBatch);
                    explosionFaceAnimation.Draw(spriteBatch);
                    break;
                case State.SEVEN: // dice "this is even my final form"
                    armTexture.DrawRectangle(spriteBatch);
                    animIddle4.Draw(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    text4.DrawRectangle(spriteBatch);
                    break;
                case State.EIGHT: // animación de cara de samer saliendo
                    armTexture.DrawRectangle(spriteBatch);
                    animOpenChest.Draw(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
                case State.NINE:
                    armTexture.DrawRectangle(spriteBatch);
                    animFiringLaser.Draw(spriteBatch);
                    turretTexture.DrawRectangle(spriteBatch);
                    break;
            } // switch

            if (SuperGame.debug)
            {
                collider.Draw(spriteBatch);
                // Frame counters
                spriteBatch.DrawString(SuperGame.fontDebug, "EnemyState = " + currentState.ToString() + ".",
                    new Vector2(5, 39), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "EnemyLife1 = " + life1 + ".",
                    new Vector2(5, 51), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "EnemyLife2 = " + life2 + ".",
                    new Vector2(5, 63), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "EnemyLife3 = " + life3 + ".",
                    new Vector2(5, 75), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

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
                colisionable = true;
                currentState = State.ONE;
                currentTurretState = TurretState.MOVING_UP;
            }
            else if (prevState == State.ONE && nextState == State.TWO)
            {
                currentState = State.TWO;
            }
            else if (prevState == State.TWO && nextState == State.THREE)
            {
                colisionable = false;
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
                armTexture.SetColor(255, 255, 255, 255);
                currentState = State.FOUR;
            }
            else if (prevState == State.FOUR && nextState == State.FIVE)
            {
                colisionable = true;
                currentTurretState = TurretState.MOVING_UP;
                currentArmState = ArmState.MOVING_UP;
                currentState = State.FIVE;
            }
            else if (prevState == State.FIVE && nextState == State.SIX)
            {
                colisionable = false;
                armTexture.SetColor(255, 255, 255, 255);
                currentArmState = ArmState.MOVING_DOWN;
                armTexture.SetColor(255, 255, 255, 255);
                armNextStateStop = true;
                currentTurretState = TurretState.STOP;
                currentState = State.SIX;
            }
            else if (prevState == State.SIX && nextState == State.SEVEN)
            {
                currentState = State.SEVEN;
            }
            else if (prevState == State.SEVEN && nextState == State.EIGHT)
            {
                currentState = State.EIGHT;
            }
            else if (prevState == State.EIGHT && nextState == State.NINE)
            {
                colisionable = true;
                currentState = State.NINE;
                currentArmState = ArmState.MOVING_DOWN;
                armNextStateStop = false;
                currentTurretState = TurretState.MOVING_UP;
            }
        } // ChangeState

        private void ChangeState(State nextState)
        {
            if (nextState == State.ENTERING)
            {
                colisionable = false;
                armTexture.SetColor(255, 255, 255, 255);
                position = initialPosition;
                currentArmState = ArmState.STOP;
                currentTurretState = TurretState.STOP;
            }
            else if (nextState == State.SPEAKING1)
            {
                colisionable = false;
                armTexture.SetColor(255, 255, 255, 255);
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
                colisionable = true;
                armTexture.SetColor(255, 255, 255, 255);
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                position = basePosition;
            }
            else if (nextState == State.TWO)
            {
                colisionable = true;
                armTexture.SetColor(255, 255, 255, 255);
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                position = basePosition;
            }
            else if (nextState == State.THREE)
            {
                colisionable = false;
                armTexture.SetColor(255, 255, 255, 255);
                currentTurretState = TurretState.STOP;
                currentArmState = ArmState.STOP;
            }
            else if (nextState == State.FOUR)
            {
                position = initialPosition;
                colisionable = false;
                armTexture.SetColor(255, 255, 255, 255);
                currentTurretState = TurretState.STOP;
                currentArmState = ArmState.STOP;
            }
            else if (nextState == State.FIVE)
            {
                colisionable = true;
                currentArmState = ArmState.MOVING_UP;
                currentTurretState = TurretState.MOVING_UP;
                armTexture.SetColor(255, 255, 255, 255);
                armTexture.rotation = 0;
                turretTexture.rotation = 0;
                animIddle2.position = basePosition;
                position = basePosition;
            }
            else if (nextState == State.SIX)
            {
                explosionFaceAnimation.setAnim(0, -1);
                timeFaceExploiting = 2.0f;
                colisionable = false;
                currentArmState = ArmState.MOVING_DOWN;
                armTexture.SetColor(255, 255, 255, 255);
                armNextStateStop = true;
                currentTurretState = TurretState.STOP;
            }
            else if (nextState == State.SEVEN)
            {
                colisionable = false;
                currentArmState = ArmState.MOVING_DOWN;
                armNextStateStop = true;
                currentTurretState = TurretState.STOP;
                armTexture.SetColor(255, 255, 255, 255);
            }
            else if (nextState == State.EIGHT)
            {
                timeSamerSaliendo = 3.0f;
                colisionable = false;
                currentArmState = ArmState.MOVING_DOWN;
                armNextStateStop = true;
                currentTurretState = TurretState.STOP;
                armTexture.SetColor(255, 255, 255, 255);
            }
            else if (nextState == State.NINE)
            {
                colisionable = true;
                currentArmState = ArmState.MOVING_DOWN;
                armNextStateStop = false;
                currentTurretState = TurretState.MOVING_UP;
            }
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

        public override void Damage(int i)
        {
            if (i == -1)
                life = life1 = life2 = life3 = 0;
            else
            {
                switch (currentState)
                {
                    case State.ONE:
                        life1 -= i;
                        if (life1 <= 0)
                            ChangeState(State.ONE, State.TWO);
                        break;
                    case State.TWO:
                        life2 -= i;
                        if (life2 <= 0)
                            ChangeState(State.TWO, State.THREE);
                        break;
                    case State.FIVE:
                        life3 -= i;
                        if (life3 <= 0)
                            ChangeState(State.FIVE, State.SIX);
                        break;
                    case State.SEVEN:
                        life4 -= i;
                        if (life4 <= 0)
                            ChangeState(State.SEVEN, State.EIGHT);
                        break;
                }
                life -= i;
            }
        }
    } // class SuperFinalBoss
}
