using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class Asteroid : Sprite
    {
        private ParticleSystem particles;
        private int particlesCount = 40;

        private float velocity;
        private Vector2 movement;
        private int rotationVelocity;

        public Asteroid(bool middlePosition, Vector2 position, float rotation, Texture2D texture)
            : base(true, position, rotation, texture)
        {
            velocity = 200;
            rotationVelocity = -3;

            movement = new Vector2(-2, 1);

            Rectangle[] rectangles = new Rectangle[4];
            rectangles[0] = new Rectangle(0, 0, 64, 64);
            rectangles[1] = new Rectangle(64, 0, 64, 64);
            rectangles[2] = new Rectangle(0, 64, 64, 64);
            rectangles[3] = new Rectangle(64, 64, 64, 64);
            particles = new ParticleSystem(GRMng.textureSmoke01, rectangles, particlesCount, position);
            particles.PARTICLE_CREATION_INTERVAL = 1f;
            particles.MAX_ACELERATION_Y = 0;
            particles.MAX_DEFLECTION_GROWTH = 0.015f;
            particles.INITIAL_GROWTH_INCREMENT = 0.015f;
        }

        public void Update(float deltaTime)
        {
            particles.Update(deltaTime, position, rotation);
            position += movement * velocity *  deltaTime;
            rotation += rotationVelocity * deltaTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            particles.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

    } // class Asteroid
}
