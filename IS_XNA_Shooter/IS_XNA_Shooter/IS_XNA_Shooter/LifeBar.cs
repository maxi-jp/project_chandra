using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class LifeBar : Animation
    {

        /// <summary>
        /// Constructor for LifeBar
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the LifeBar</param>
        /// <param name="rotation">The rotation of the LifeBar</param>
        /// <param name="frameWidth">The width of each frame of the LifeBar's animation</param>
        /// <param name="frameHeight">The height of each frame of the LifeBar's animation </param>
        /// <param name="numAnim">The number of the LifeBar's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the LifeBar</param>
        public LifeBar(Camera camera, Level level, Vector2 position, float rotation,
                    short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
                    float frametime, Texture2D texture)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                            frameCount, looping, frametime)
        {
            setAnim(0);

        }

        /// <summary>
        /// Sets de animation
        /// </summary>
        /// <param name="anim">the animation's numbre</param>
        public void setAnimAux(short anim)
        {
            setAnim(anim);
        }

        /// <summary>
        /// LifeBar's update
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

        }

         /// <summary>
        /// Draws the LifeBar 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
