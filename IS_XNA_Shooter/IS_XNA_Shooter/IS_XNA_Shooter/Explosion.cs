using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class Explosion : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */

        /* ------------------- CONSTRUCTORES ------------------- */
        public Explosion(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short frameCount, float frametime, Texture2D texture)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, 1, null, null,
                frametime)
        {
            base.frameCount = new short[1];
            base.frameCount[0] = frameCount;
            base.looping = new bool[1];
            base.looping[0] = false;

            setAnim(0);
            active = true;

            Audio.PlayEffect("brokenBone01");
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool isActive()
        {
            return active;
        }

    } // class Explosion
}
