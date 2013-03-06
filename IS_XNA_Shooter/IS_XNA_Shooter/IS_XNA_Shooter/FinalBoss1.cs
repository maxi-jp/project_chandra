using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class for the first final boss
    /// </summary>
    class FinalBoss1 : Enemy
    {
        // ---------------------------
        // -----     BUILDER     -----
        // ---------------------------

        /// <summary>
        /// This is the builder for the class FinalBoss1
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="level"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="numAnim"></param>
        /// <param name="frameCount"></param>
        /// <param name="looping"></param>
        /// <param name="frametime"></param>
        /// <param name="texture"></param>
        /// <param name="velocity"></param>
        /// <param name="life"></param>
        /// <param name="value"></param>
        /// <param name="Ship"></param>
        public FinalBoss1(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, int value, Ship Ship)
            : base (camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, value, Ship)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            position.X += deltaTime * velocity;
        }
    }
}
