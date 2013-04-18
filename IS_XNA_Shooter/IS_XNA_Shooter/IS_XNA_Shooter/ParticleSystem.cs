using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class Particle : SpriteCamera
    {
        /// <summary>
        /// The actual age of the particle
        /// </summary>
        public float actualAge;

        /// <summary>
        /// Dead time for the particle
        /// </summary>
        public float deadAge;

        /// <summary>
        /// Aceleration vector for the movement
        /// </summary>
        public Vector2 aceleration;

        /// <summary>
        /// Indicates the transparency of the texture
        /// </summary>
        public byte alpha;

        /// <summary>
        /// Time since the last fadeout
        /// </summary>
        public float timeSinceTheLastFadeout;

        /// <summary>
        /// Constructor for a Particle
        /// </summary>
        /// <param name="camera">Camera of the actual game</param>
        /// <param name="level">The actual Level of the game</param>
        /// <param name="position">Initial position for the particle</param>
        /// <param name="rotation">Initial rotation for the particle</param>
        /// <param name="texture">Texture to show</param>
        /// <param name="aceleration">Indicates the velocity of movement</param>
        public Particle(Camera camera, Level level, Vector2 position,
            Texture2D texture, Rectangle rectangleTexture)
            : base(camera, level, true, position, 0, texture, rectangleTexture)
        {
            this.aceleration = Vector2.Zero;
            alpha = 255;
            timeSinceTheLastFadeout = 0.0f;
        }

        public void Move()
        {
            this.position += this.aceleration;
        }

    } // class Particle

    class ParticleSystem
    {
        /// <summary>
        /// The actual Camera of the game
        /// </summary>
        private Camera camera;

        /// <summary>
        /// The actual Level of the game
        /// </summary>
        private Level level;

        /// <summary>
        /// Texture for all the particles
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Rectangles which delimites the sprites in the texture
        /// </summary>
        private Rectangle[] rectanglesTexture;

        /// <summary>
        /// Maximun number of particles of the system
        /// </summary>
        private int particleCount;

        /// <summary>
        /// Array which contains all the particles in the manager
        /// </summary>
        private Particle[] particles;

        /// <summary>
        /// The source position por the particles
        /// </summary>
        private Vector2 sourcePosition;

        /// <summary>
        /// Time that has passed since the creation of the last particle
        /// </summary>
        private float timeSinceLastParticle;

        private const int MAX_ACELERATION_X = 1;
        private const int MAX_ACELERATION_Y = 5;
        private const int MIN_ACELERATION_Y = 1;
        private const int MAX_DEFLECTION_INITIALPOSITION = 0;
        private const float MAX_DEFLECTION_DEAD = 0.4f;
        private const float MAX_DEFLECTION_SCALE = 0.5f;
        private const byte MAX_ALPHA = 255;
        private const byte MAX_DEFLECTION_INITIALALPHA = 64;
        private const float INITIAL_DEAD_AGE = 0.9f;
        private const float FADEOUT_INTERVAL = 0.04f;
        private const byte FADEOUT_DECREMENT = 12;
        private const float FADEOUT_INCREMENT = 2.22f;
        private const float FADEOUT_DECREMENT_INITIAL_TIME = 0.20f;
        private const float PARTICLE_CREATION_INTERVAL = 1.0f;

        /// <summary>
        /// A Random object for generate random values
        /// </summary>
        private Random rnd;

        /// <summary>
        /// Constructor for the Particle System
        /// </summary>
        /// <param name="camera">Camera of the actual game</param>
        /// <param name="level">The actual Level of the game</param>
        /// <param name="texture">the texture for all the particles</param>
        /// <param name="rectanglesTexture">Array which contains the rectangles of sprites in the texture</param>
        /// <param name="particleCount">Indicates de maximun number of particles for the system</param>
        /// <param name="sourcePosition">The position of the source of the particles</param>
        public ParticleSystem(Camera camera, Level level, Texture2D texture, Rectangle[] rectanglesTexture,
            int particleCount, Vector2 sourcePosition)
        {
            this.camera = camera;
            this.level = level;
            this.texture = texture;
            this.rectanglesTexture = rectanglesTexture;
            this.particleCount = particleCount;
            particles = new Particle[particleCount];
            this.sourcePosition = sourcePosition;
            timeSinceLastParticle = 0.0f;
            rnd = new Random();
        }

        /// <summary>
        /// Update the logic of all the particles
        /// </summary>
        /// <param name="deltaTime">Time since the last Update</param>
        public void Update(float deltaTime, Vector2 actualPosition)
        {
            sourcePosition = actualPosition;

            // Encourage the particles "alive"
            EncourageParticles(deltaTime);

            // re-parameterize the particles "dead"
            ReParameterizeParticles(deltaTime);
        }

        /// <summary>
        /// Draw all the particles
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch of the game</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                    particles[i].DrawRectangle(spriteBatch);
                    /*spriteBatch.Draw(textura,
                        particulas[i].posicion, null,
                        new Color(255, 255, 255, (byte)MathHelper.Clamp(particulas[i].alfa, 0, 255)), 0.0f, Vector2.Zero, particulas[i].escala,
                        SpriteEffects.None, 0);*/
            }
        }

        /// <summary>
        /// Moves the particles as its parameters
        /// </summary>
        /// <param name="deltaTime">Time since the last Update</param>
        private void EncourageParticles(float deltaTime)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                {
                    // move the particles
                    particles[i].actualAge += deltaTime;
                    particles[i].Move();

                    // apply fadeout
                    if (particles[i].actualAge >= particles[i].deadAge * FADEOUT_DECREMENT_INITIAL_TIME)
                    {
                        if (particles[i].timeSinceTheLastFadeout >= FADEOUT_INTERVAL)
                        {
                            particles[i].timeSinceTheLastFadeout = 0.0f;
                            particles[i].alpha -= FADEOUT_DECREMENT;
                            particles[i].SetTransparency(particles[i].alpha);
                        }
                    }
                    /*else
                    {
                        if (particles[i].alpha < MAX_ALPHA)
                            particles[i].alpha += FADEOUT_INCREMENT;
                    }*/

                    particles[i].timeSinceTheLastFadeout += deltaTime;
                }
            }

        }  // EncourageParticles

        /// <summary>
        /// Parameterizes the "dead" particles or not initialized
        /// </summary>
        /// <param name="deltaTime">Time since the last Update</param>
        private void ReParameterizeParticles(float deltaTime)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (timeSinceLastParticle >= PARTICLE_CREATION_INTERVAL)
                {
                    // Instantiate a new particle if it is not yet
                    if (particles[i] == null)
                    {
                        int recIndex = ObtainRandom(rectanglesTexture.Length);
                        particles[i] = new Particle(camera, level, sourcePosition, texture,
                            rectanglesTexture[recIndex]);
                    }

                    // Only re-parameterize "dead" particles
                    if (particles[i].actualAge >= particles[i].deadAge)
                    {
                        // Reinitialize the age
                        particles[i].actualAge = 0.0f;

                        // Reinitialize the alpha
                        particles[i].alpha = MAX_ALPHA;
                        particles[i].alpha -= (Byte)ObtainRandom(MAX_DEFLECTION_INITIALALPHA);
                        particles[i].SetTransparency(particles[i].alpha);
                        particles[i].timeSinceTheLastFadeout = 0.0f;

                        // Set the initial position
                        particles[i].position = sourcePosition; /*+
                            new Vector2((float)ObtainRandom(MAX_DEFLECTION_INITIALPOSITION),
                                (float)ObtainRandom(MAX_DEFLECTION_INITIALPOSITION));*/

                        // Set the initial rotation
                        particles[i].rotation = ObtainRandom((float)(Math.PI * 2));

                        // Set a random texture
                        particles[i].SetRectangle(rectanglesTexture[ObtainRandom(rectanglesTexture.Length)]);

                        // Establish acceleration
                        //particles[i].aceleration.X = (float)ObtainRandom(MAX_ACELERATION_X);
                        //particles[i].aceleration.Y = -ObtainRandom(MAX_ACELERATION_Y) - MIN_ACELERATION_Y;
                        //particles[i].aceleration.Normalize();
                        particles[i].aceleration = Vector2.Zero;

                        //if (ObtainRandom(10) <= 5)
                        //    particles[i].aceleration.X *= -1;

                        // Establish the dead age
                        particles[i].deadAge = INITIAL_DEAD_AGE;
                        particles[i].deadAge += ObtainRandom(MAX_DEFLECTION_DEAD);

                        // Establish the deviation in the scale (we want particles smaller than others)
                        particles[i].scale = 1.0f;
                        particles[i].scale += ObtainRandom(MAX_DEFLECTION_SCALE);

                        timeSinceLastParticle = 0.0f;
                    }
                }
                else
                    timeSinceLastParticle += deltaTime;
            }

        } // ReParameterizeParticles


        /// <summary>
        /// Returns a new random int number
        /// </summary>
        /// <param name="maxValue">Indicates the maximun value posible</param>
        /// <returns>the random value</returns>
        private int ObtainRandom(int maxValue)
        {
            return rnd.Next(maxValue);
        }

        /// <summary>
        /// Returns a new random float number
        /// </summary>
        /// <param name="maxValue">Indicates the maximun value posible</param>
        /// <returns>the random value</returns>
        private float ObtainRandom(float maxValue)
        {
            return (float)(rnd.NextDouble()) * maxValue;
        }

    } // class ParticleSystem
}
