using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class Animation : SpriteCamera
    {
        /* ------------------- ATRIBUTOS ------------------- */
        // coleccion de imágenes que representan la animación
        //protected Texture2D spriteStrip; Texture2D esta en Sprite

        // Tiempo desde el ultimo update del frame
        protected float elapsedTime;

        // Tiempo de frame
        protected float frameTime;

        // número de frames que contiene la animación
        protected short[] frameCount;

        // Índice del frame mostrado en el momento
        protected short currentFrame;

        // Animación actual
        protected short currentAnim;

        // Animación anterior
        protected short prevAnim;

        // next animation to be played
        protected short nextAnim;

        // Área de la tira imagen que vamos a mostrar
        Rectangle sourceRect = new Rectangle();

        // Área donde vamos a mostrar la imagen en el juego
        Rectangle destinationRect = new Rectangle();

        // Anchura del frame
        protected short frameWidth;

        // Altura del frame
        protected short frameHeight;

        // punto medio de cada cuadro
        protected Vector2 middlePoint;

        // Número de animaciones
        protected int numAnim;

        // estado de la Animation
        public bool animActive;

        // Determina si la animación continúa o se desctiva después de ejecutarse
        protected bool[] looping;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Animation(Camera camera, Level level, bool middlePosition, Vector2 position, float rotation,
            Texture2D texture, short frameWidth, short frameHeight, short numAnim, short[] frameCount,
            bool[] looping, float frametime)
            : base (camera, level, middlePosition, position, rotation, texture)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            middlePoint = new Vector2(frameWidth / 2, frameHeight / 2);
            this.numAnim = numAnim;
            this.frameCount = frameCount;
            this.looping = looping;
            this.frameTime = frametime;

            currentAnim = prevAnim = 0;

            // Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            // Set the Animation to active by default
            animActive = true;
        }

        /* ------------------- MÉTODOS ------------------- */
        public virtual void Update (float deltaTime)
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
                if (currentFrame == frameCount[currentAnim])
                {
                    currentFrame = 0;
                    // If we are not looping deactivate the animation
                    if (!looping[currentAnim])
                    {
                        if (nextAnim == -1)
                            animActive = false;
                        else
                            currentAnim = prevAnim;
                    }
                }

                // Reset the elapsed time to zero
                elapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * frameWidth, currentAnim * frameHeight, frameWidth, frameHeight);

            // Set the correct position in the display screen
            //destinationRect = new Rectangle((int)Position.X - (int)FrameWidth / 2,
            //(int)Position.Y - (int)FrameHeight / 2,
            destinationRect = new Rectangle((int)position.X + (int)camera.displacement.X /*+ (int)frameWidth / 2*/,
                (int)position.Y + (int)camera.displacement.Y /*+ (int)frameHeight / 2*/,
                (int)frameWidth, (int)frameHeight);
        }   // end Update()


        public override void Draw (SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (animActive)
                spriteBatch.Draw(texture, destinationRect, sourceRect, color, rotation,
                    middlePoint, SpriteEffects.None, 1);
        }

        protected void setAnim(short i)
        {
            if (currentAnim != i)
                prevAnim = currentAnim;
            currentAnim = i;

            currentFrame = 0;
            elapsedTime = 0;
        }

        protected void setAnim(short i, short j)
        {
            nextAnim = j;
            setAnim(i);
        }

    } // class Animation
}
