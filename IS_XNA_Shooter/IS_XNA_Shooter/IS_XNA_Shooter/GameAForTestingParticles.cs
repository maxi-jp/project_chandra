using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    class GameAForTestingParticles : Game
    {
        private Sprite aimPointSprite;
        private BackgroundGameA backGround;
        private int num;

        public GameAForTestingParticles(SuperGame mainGame, Player player, int num, Texture2D textureAim,
            Texture2D textureBg, float shipVelocity, int shipLife)
            : base(mainGame, player, shipVelocity, shipLife)
        {
            hub = new IngameHubA(GRMng.hubBase, mainGame.player.GetLife());
            level = new LevelA(camera, num, enemies);
            backGround = new BackgroundGameA(camera, level);
            this.num = num;
            camera.setLevel(level);

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(15, 35);
            points[1] = new Vector2(26, 33);
            points[2] = new Vector2(34, 15);
            points[3] = new Vector2(65, 30);
            points[4] = new Vector2(65, 50);
            points[5] = new Vector2(34, 66);
            points[6] = new Vector2(26, 47);
            points[7] = new Vector2(15, 45);
            ship = new ShipA(this, camera, level, Vector2.Zero, 0, points,
                GRMng.frameWidthPA1, GRMng.frameHeightPA1, GRMng.numAnimsPA1, GRMng.frameCountPA1,
                GRMng.loopingPA1, SuperGame.frameTime24, GRMng.texturePA1,
                shipVelocity, shipLife, shots);

            level.setShip(ship);

            aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);

            camera.setShip(ship);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // actualizamos posicion del puntero:
            aimPointSprite.position.X = Mouse.GetState().X;
            aimPointSprite.position.Y = Mouse.GetState().Y;

            TestParticles();

        } // Update()

        public override void Draw(SpriteBatch spriteBatch)
        {
            backGround.Draw(spriteBatch);

            base.Draw(spriteBatch); // Ship, enemies, shots
            
            aimPointSprite.Draw(spriteBatch); // aim point

            spriteBatch.DrawString(SuperGame.fontDebug,
                "Press FX to increase value, press FX+mouse.rightclick for decrease value \n" +
                "F1: Number of Particles = " + ship.particles.GetParticleCount() + "\n" +
                "F2: PARTICLE_CREATION_INTERVAL = " + ship.particles.PARTICLE_CREATION_INTERVAL + "\n" +
                "F3: INITIAL_DEAD_AGE = " + ship.particles.INITIAL_DEAD_AGE + "\n" +
                "F4: FADEOUT_DECREMENT_INITIAL_TIME = " + ship.particles.FADEOUT_DECREMENT_INITIAL_TIME + "\n" +
                "F5: FADEOUT_INCREMENT = " + ship.particles.FADEOUT_INCREMENT + "\n" +
                "F6: FADEOUT_DECREMENT = " + ship.particles.FADEOUT_DECREMENT + "\n" +
                "F7: INITIAL_GROWTH_INCREMENT = " + ship.particles.INITIAL_GROWTH_INCREMENT + "\n" +
                "F8: MAX_DEFLECTION_GROWTH = " + ship.particles.MAX_DEFLECTION_GROWTH + "\n" +
                "F9: MAX_ACELERATION = " + ship.particles.MAX_ACELERATION,
                new Vector2(5, 5), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

        } // Draw()

        private void TestParticles()
        {
            // Number of Particles:
            if (ControlMng.f1Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.SetParticlesNumber(ship.particles.GetParticleCount() + 2);
            else if (ControlMng.f1Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.SetParticlesNumber(ship.particles.GetParticleCount() - 2);

            // PARTICLE_CREATION_INTERVAL:
            if (ControlMng.f2Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.PARTICLE_CREATION_INTERVAL += 0.10f;
            else if (ControlMng.f2Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.PARTICLE_CREATION_INTERVAL -= 0.10f;

            // INITIAL_DEAD_AGE:
            if (ControlMng.f3Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.INITIAL_DEAD_AGE += 0.10f;
            else if (ControlMng.f3Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.INITIAL_DEAD_AGE -= 0.10f;

            // FADEOUT_DECREMENT_INITIAL_TIME
            if (ControlMng.f4Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.FADEOUT_DECREMENT_INITIAL_TIME += 0.05f;
            else if (ControlMng.f4Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.FADEOUT_DECREMENT_INITIAL_TIME -= 0.05f;

            // FADEOUT_INCREMENT
            if (ControlMng.f5Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.FADEOUT_INCREMENT += 1;
            else if (ControlMng.f5Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.FADEOUT_INCREMENT -= 1;

            // FADEOUT_DECREMENT
            if (ControlMng.f6Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.FADEOUT_DECREMENT += 1;
            else if (ControlMng.f6Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.FADEOUT_DECREMENT -= 1;

            // INITIAL_GROWTH_INCREMENT
            if (ControlMng.f7Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.INITIAL_GROWTH_INCREMENT += 0.005f;
            else if (ControlMng.f7Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.INITIAL_GROWTH_INCREMENT -= 0.005f;

            // MAX_DEFLECTION_GROWTH
            if (ControlMng.f8Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.MAX_DEFLECTION_GROWTH += 0.005f;
            else if (ControlMng.f8Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.MAX_DEFLECTION_GROWTH -= 0.005f;

            // MAX_ACELERATION
            if (ControlMng.f9Preshed && Mouse.GetState().RightButton == ButtonState.Released)
                ship.particles.MAX_ACELERATION += 1;
            else if (ControlMng.f9Preshed && Mouse.GetState().RightButton == ButtonState.Pressed)
                ship.particles.MAX_ACELERATION -= 1;

        } // TestParticles

    } // class GameAForTestingParticles
}
