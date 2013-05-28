using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class ComplexAnimation
    {
         /* ------------------- ATRIBUTOS ------------------- */

        // número de frames de la animación
        private short frameCount;

        // array que contiene cada frame de la animación
        private AnimRect[/*frameCount*/] animArray;

        // tamaño de los frames:
        private int frameWidth, frameHeight;

        // indica si la animación se repite
        protected bool looping;

        // Tiempo desde el ultimo update del frame
        protected float elapsedTime;

        // Tiempo de frame
        protected float frameTime;

        // Índice del frame mostrado en el momento
        protected short currentFrame;

        // Área donde vamos a mostrar la imagen en el juego
        Rectangle destinationRect = new Rectangle();

        // punto medio de cada cuadro
        protected Vector2 middlePoint;

        // estado de la Animation
        public bool animActive;

        public bool animFinished;

        private Color color;

        // atributos del juego
        private Camera camera;
        private Level level;
        public Vector2 position;
        public float rotation;

        /* ------------------- CONSTRUCTORES ------------------- */
        public ComplexAnimation(Camera camera, Level level, bool middlePosition, Vector2 position, float rotation,
            AnimRect[/*frameCount*/] animArray, short frameCount, bool looping, float frametime)
        {
            this.camera = camera;
            this.level = level;
            this.position = position;
            this.rotation = rotation;

            color = Color.White;

            this.animArray = animArray;

            frameWidth = animArray[0].rect.Width;
            frameHeight = animArray[0].rect.Height;
            middlePoint = new Vector2(frameWidth / 2, frameHeight / 2);
            this.frameCount = frameCount;
            this.looping = looping;
            this.frameTime = frametime;

            // Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            // Set the Animation to active by default
            animActive = true;
            animFinished = false;
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update (float deltaTime)
        {
            // Do not update the game if we are not active
            if (!animActive)
                return;

            // Update the elapsed time
            elapsedTime += deltaTime;

            // If the elapsed time is larger than the frame time
            // we need to switch frames
            if (elapsedTime > frameTime)
            {
                // Move to the next frame
                currentFrame++;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    // If we are not looping deactivate the animation
                    if (!looping)
                    {
                        animActive = false;
                        animFinished = true;
                    }
                }

                // Reset the elapsed time to zero
                elapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            //sourceRect = new Rectangle(currentFrame * frameWidth, currentAnim * frameHeight, frameWidth, frameHeight);

            // Set the correct position in the display screen
            //destinationRect = new Rectangle((int)Position.X - (int)FrameWidth / 2,
            //(int)Position.Y - (int)FrameHeight / 2,
            destinationRect = new Rectangle((int)position.X + (int)camera.displacement.X /*+ (int)frameWidth / 2*/,
                (int)position.Y + (int)camera.displacement.Y /*+ (int)frameHeight / 2*/,
                (int)frameWidth, (int)frameHeight);
        }   // end Update()


        public void Draw (SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (animActive)
                spriteBatch.Draw(animArray[currentFrame].texture, destinationRect, animArray[currentFrame].rect, color, rotation,
                    middlePoint, SpriteEffects.None, 1);
        }

        /*protected void setAnim(short i)
        {
            if (currentAnim != i)
                prevAnim = currentAnim;
            currentAnim = i;

            currentFrame = 0;
            elapsedTime = 0;
        }*/

        /*protected void setAnim(short i, short j)
        {
            nextAnim = j;
            setAnim(i);
        }*/
    } // ComplexAnimation
}
