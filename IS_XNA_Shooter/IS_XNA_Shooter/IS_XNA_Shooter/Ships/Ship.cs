using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase padre de jugador de la que heredan los 2 tipos de jugador
    abstract class Ship : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */
        protected Game game;

        public Collider collider;
        public ParticleSystem particles;
        private int particlesCount = 20;
        private Vector2 exhaustPipePosition;
        private Vector2 exhaustPipePositionOrig;
        /// <summary>
        /// Rotation matrix for the exhaust pipe
        /// </summary>
        private Matrix rotationMatrix;

        protected float velocity;
        protected Vector2 movement;
        protected List<Shot> shots;
        protected float shotVelocity;
        protected int shotPower;
        protected int life;

        protected float timeToShot; // tiempo minimo entre disparos en segundos
        protected float timeToShotAux;

        private float timeVibShot = 0.1f; // tiempo que vibra el mando tras disparo
        private float timeVibShotAux;

        private float timePowerUpRed = 0;
        public float timePowerUpBlue = 0; //    public de momento

        // state of the ship
        protected enum shipState
        {
            ONNORMAL,
            ONDYING,
            ONINVINCIBLE
        };
        protected shipState currentState; // current state of the enemy

        private float timeDying = 2, timeDyingAux;
        private float timeInvencible = 3, timeInvencibleAux;
        private byte transparency;

        /* ------------------- CONSTRUCTORES ------------------- */
        /*public Ship(Game game, Camera camera, Level level, Vector2 position, float rotation,
            Vector2[] colliderPoints,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture,
            float velocity, int life, List<Shot> shots)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            movement = new Vector2(1, 0);

            this.game = game;
            this.velocity = velocity;
            this.life = life;
            this.shots = shots;

            collider = new Collider(camera, true, position, rotation, colliderPoints, 35, frameWidth, frameHeight);

            exhaustPipePosition = exhaustPipePositionOrig = new Vector2(-34, 0);

            Rectangle [] rectangles = new Rectangle[4];
            rectangles[0] = new Rectangle(0, 0, 64, 64);
            rectangles[1] = new Rectangle(64, 0, 64, 64);
            rectangles[2] = new Rectangle(0, 64, 64, 64);
            rectangles[3] = new Rectangle(64, 64, 64, 64);
            particles = new ParticleSystem(camera, level, GRMng.textureSmoke01, rectangles, 20, position);

            currentState = shipState.ONNORMAL;

            timeInvencibleAux = timeInvencible;
            timeDyingAux = timeDying;
            transparency = 0;

            setAnim(1);
        }*/

        public Ship(Game game, Camera camera, Level level, Vector2 position, float rotation,
            Vector2[] colliderPoints,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture,
            Evolution evolution, List<Shot> shots)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            movement = new Vector2(1, 0);

            this.game = game;

            this.velocity = evolution.getSpeedShip() ;
            this.life = (int)evolution.getLife();
            this.shotVelocity = evolution.getSpeedShot();
            this.shotPower = (int)evolution.getPowerAttack();
            this.timeToShot = evolution.getCadence();

            this.shots = shots;

            collider = new Collider(camera, true, position, rotation, colliderPoints, 35, frameWidth, frameHeight);

            exhaustPipePosition = exhaustPipePositionOrig = new Vector2(-34, 0);

            Rectangle[] rectangles = new Rectangle[4];
            rectangles[0] = new Rectangle(0, 0, 64, 64);
            rectangles[1] = new Rectangle(64, 0, 64, 64);
            rectangles[2] = new Rectangle(0, 64, 64, 64);
            rectangles[3] = new Rectangle(64, 64, 64, 64);
            particles = new ParticleSystem(camera, level, GRMng.textureSmoke01, rectangles, particlesCount, position);

            currentState = shipState.ONNORMAL;

            timeInvencibleAux = timeInvencible;
            timeDyingAux = timeDying;
            transparency = 0;

            setAnim(1);
        }

        /* ------------------- MÉTODOS ------------------- */
        /*public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // movimiento del jugado
            movement = Vector2.Zero;
            float disp = 0;
            double gyre = 0;

            if (ControlMng.isControllerActive()) // GamePad
            {
                if (timeVibShotAux <= 0)
                    GamePad.SetVibration(ShipIndex.One, 0, 0);

                movement = GamePad.GetState(ShipIndex.One).ThumbSticks.Left;
                movement.Y = -movement.Y;

                disp = GamePad.GetState(ShipIndex.One).Triggers.Right;
                if (GamePad.GetState(ShipIndex.One).IsButtonDown(Buttons.RightTrigger))
                    disp = 1;

                timeVibShotAux -= deltaTime;
            }
            else // teclado
            {
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlUp))
                {
                    movement.Y = -1;
                    gyre = Math.PI / 2;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlDown))
                {
                    movement.Y = 1;
                    gyre = Math.PI / 2;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlLeft))
                {
                    movement.X = -1;
                    gyre = 0;
                }
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlRight))
                {
                    movement.X = 1;
                    gyre = 0;
                }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    disp = 1;
            }

            // movimiento:
            if (movement.X != 0 && movement.Y != 0)
                gyre = Math.Abs(Math.Atan(movement.Y / movement.X));

            //if ((movement.X + movement.Y) > 1)
            //movement.Normalize();

            
            position.X += movement.X * (float)Math.Cos(gyre) * velocity * deltaTime;
            position.Y += movement.Y * (float)Math.Sin(gyre) * velocity * deltaTime;

            // disparos:
            if ((disp > 0) && (timeToShotAux <= 0))
                ShipShot(disp);

            timeToShotAux -= deltaTime;

            collider.Update(position, rotation);
        }*/

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            switch (currentState)
            {
                case shipState.ONNORMAL:
                    Move(deltaTime);

                    rotationMatrix = Matrix.CreateRotationZ(rotation);
                    exhaustPipePosition = Vector2.Transform(exhaustPipePositionOrig, rotationMatrix);
                    exhaustPipePosition += position;

                    collider.Update(position, rotation);
                    particles.Update(deltaTime, exhaustPipePosition, rotation);
                    break;

                case shipState.ONDYING:
                    timeDyingAux -= deltaTime;
                    if (timeDyingAux <= 0)
                    {
                        timeDyingAux = timeDying;
                        Respawn();
                    }

                    break;

                case shipState.ONINVINCIBLE:
                    timeInvencibleAux -= deltaTime;
                    if (timeInvencibleAux <= 0)
                    {
                        timeInvencibleAux = timeInvencible;
                        transparency = 255;
                        SetTransparency(transparency);

                        frameTime = SuperGame.frameTime24;
                        currentState = shipState.ONNORMAL;
                        particles.SetParticlesNumber(particlesCount);
                    }
                    else
                    {
                        transparency += (byte)(deltaTime * 120);
                        SetTransparency(transparency);
                    }
                    
                    Move(deltaTime);
                    collider.Update(position, rotation);

                    break;
            }
            
        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            particles.Draw(spriteBatch);
            base.Draw(spriteBatch);

            if (timePowerUpBlue > 0 & timePowerUpRed > 0)
                SetColor(255, 0, 255, 255);
            else if (timePowerUpRed > 0)
                SetColor(255,0,0,255);
            else if (timePowerUpBlue > 0)
                SetColor(0,0,255,255);
            else SetColor(255, 255, 255, 255);
            // TODO: must fix! así se está estableciendo el color en cada Draw
            //     cuando solo debería de hacerse una vez: cuando se coje el power up

            if (SuperGame.debug)
                collider.Draw(spriteBatch);
        } // Draw

        private void Move(float deltaTime)
        {
            movement = Vector2.Zero;
            float disp = 0;

            if (ControlMng.isControllerActive()) // GamePad
            {
                if (timeVibShotAux <= 0)
                    GamePad.SetVibration(PlayerIndex.One, 0, 0);

                movement = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
                movement.Y = -movement.Y;

                disp = GamePad.GetState(PlayerIndex.One).Triggers.Right;
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger))
                    disp = 1;

                timeVibShotAux -= deltaTime;
            }
            else // keyboard
            {
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlUp))
                    movement.Y = -1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlDown))
                    movement.Y = 1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlLeft))
                    movement.X = -1;
                if (Keyboard.GetState().IsKeyDown(ControlMng.controlRight))
                    movement.X = 1;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    disp = 1;
            }

            // final movement:
            if ((movement.X + movement.Y) > 1)
                movement.Normalize();
            position.X += movement.X * velocity * deltaTime;
            position.Y += movement.Y * velocity * deltaTime;

            // shots:
            if ((disp > 0) && (timeToShotAux <= 0))
                ShipShot(disp);

            // power ups:
            if (timePowerUpRed > 0)
                timePowerUpRed -= deltaTime;
            if (timePowerUpBlue > 0)
                timePowerUpBlue -= deltaTime;

            timeToShotAux -= deltaTime;
        }

        private void ShipShot(float disp)
        {
            /*if (ControlMng.isControllerActive())
            {
                GamePad.SetVibration(ShipIndex.One, 0.1f, 0.2f);
                timeVibShotAux = timeVibShot;
            }*/

            setAnim(2);
            Shot nuevo;
            if (timePowerUpRed > 0)
            {
                Audio.PlayEffect("laser_shot_red");
                nuevo = new Shot(camera, level, position, rotation, GRMng.frameWidthShotFinalBossHeroe, GRMng.frameHeightShotFinalBossHeroe,
                    GRMng.numAnimsShotFinalBossHeroe, GRMng.frameCountShotFinalBossHeroe, GRMng.loopingShotFinalBossHeroe, SuperGame.frameTime8, GRMng.textureShotFinalBossHeroe,
                    SuperGame.shootType.red, shotVelocity, (int)(shotPower * 1.5f));
            }
            else
            {
                Audio.PlayEffect("laserShot01");
                nuevo = new Shot(camera, level, position, rotation, GRMng.frameWidthL1, GRMng.frameHeightL1,
                    GRMng.numAnimsL1, GRMng.frameCountL1, GRMng.loopingL1, SuperGame.frameTime8,
                    GRMng.textureL1, SuperGame.shootType.normal, shotVelocity, shotPower);
            }
            shots.Add(nuevo);
            timeToShotAux = timeToShot;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public int GetLife()
        {
            return life;
        }

        public void Damage(int i)
        {
            if (currentState == shipState.ONNORMAL && !SuperGame.godMode && timePowerUpBlue <= 0)
            {
                life -= i;

                if (life <= 0)
                    Kill();
            }
        }

        public void Kill()
        {
            if (currentState == shipState.ONNORMAL)
            {
                game.PlayerDead();

                currentState = shipState.ONDYING;

                particles.SetParticlesNumber(0);

                Audio.PlayEffect("tackled1");
                frameTime = timeDying / frameCount[3];
                setAnim(3);
            }
        }

        private void Respawn()
        {
            currentState = shipState.ONINVINCIBLE;
            transparency = 0;
            SetTransparency(transparency);
            setAnim(1);
        }

        public void EraseShots()
        {
            shots.Clear();
        }

        public void CatchPowerUp(short type)
        {
            switch (type)
            {
                case 0:     // blue invencivility
                    Audio.PlayEffect("PowerUpBlue");
                    timePowerUpBlue = 6f;
                    break;
                case 1:     // red red shoot
                    Audio.PlayEffect("PowerUpRed");
                    timePowerUpRed = 8f;
                    break;
                case 2:
                    Audio.PlayEffect("PowerUpGreen");
                    break;
            }
        }

    } // class Ship
}
