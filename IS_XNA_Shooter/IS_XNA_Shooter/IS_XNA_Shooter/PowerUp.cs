using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class PowerUp : Animation
    {
        /// <summary>
        /// Enemy's collider
        /// </summary>
        public Collider collider;

        /// <summary>
        /// player's ship
        /// </summary>
        protected Ship ship;

        /// <summary>
        /// Type of the power.
        /// </summary>
        protected short type;

        /// <summary>
        /// Indicates if the power up is colisionable or not.
        /// </summary>
        protected bool active;

        /// <summary>
        /// Time to catch the power up.
        /// </summary>
        protected float catchable;

        /// <summary>
        /// Indicates when the power ups is going to turn off.
        /// </summary>
        private byte setOff;

        /// <summary>
        /// Indicate if the power up is colisionable.
        /// </summary>
        private Boolean collisionable;

        /// <summary>
        /// Time the banner stays visible
        /// </summary>
        private float timeForBanner;

        /// <summary>
        /// movement for the banner
        /// </summary>
        private double movement;

        /// <summary>
        /// fade out of the banner
        /// </summary>
        private int fOut;

        /// <summary>
        /// PowerUps constructor
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the powerUp</param>
        /// <param name="rotation">The rotation of the powerUp</param>
        /// <param name="texture">The texture of the powerUp</param>
        /// <param name="frameWidth">The width of each frame of the powerUp's animation</param>
        /// <param name="frameHeight">The height of each frame of the powerUp's animation </param>
        /// <param name="numAnim">The number of the powerUp's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="ship">The player's ship</param>
        public PowerUp(Camera camera, Level level, Vector2 position, float rotation, Texture2D texture,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, short type)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            active = true;
            catchable=7.5f;
            timeForBanner = 1.0f;
            collisionable = true;
            movement = -40;
            fOut = 0;

            this.type =type;
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(10, 40);
            points[1] = new Vector2(20, 60);
            points[2] = new Vector2(40, 70);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(70, 40);
            points[5] = new Vector2(60, 20);
            points[6] = new Vector2(40,10);
            points[7] = new Vector2(20,20);
            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);

            //type must be 0 = blue,1 = red,2 = green or 3 = orange
            if (type < 4 && type >= 0)
                setAnim(type);
            else 
                setAnim(0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                
                if (!collisionable && timeForBanner > 0)
                {
                    // Ask for Javi and Gallu for more information =D
                    movement += 50;
                    fOut += 5;
                    spriteBatch.Draw(GRMng.banPowerUps, new Rectangle((int)position.X + (int)camera.displacement.X - 160,
                        (int)position.Y + (int)camera.displacement.Y - 50 - (int)(Math.Log(movement)*8), 320, 80),
                        new Rectangle(0, type * 80, 320, 80), new Color(255 - fOut, 255 - fOut, 255 -fOut, 255 - fOut));
            
                }
                else 
                {
                    base.Draw(spriteBatch);
                    if (SuperGame.debug)
                    collider.Draw(spriteBatch);
                }
            }
        }

        public override void Update(float deltaTime)
        {
            if (active)
            {
                base.Update(deltaTime);
                catchable -= deltaTime;
                if (catchable <= 0f)
                    active = false;
                else if (catchable <= 2.5f)
                {
                        setOff += (byte)(deltaTime * 480);
                        SetColor(setOff, setOff, setOff,setOff);
                }

                if (collisionable)
                    collider.Update(position, 0);
                else 
                    if (timeForBanner > 0)
                {
                    timeForBanner -= deltaTime;
                }
            }
        }

        public void UpdatePosition(Vector2 position)
        {
            this.position = position;
        }


        public bool IsActive()
        {
            return active;
        }

        public bool IsErasable()
        {
            return (timeForBanner <= 0);
        }

        public void SetActive(bool b)
        {
            active = b;
        }

        public void ShowBanner()
        {
            catchable = 7.5f; // for the right painting of the banners
            collisionable = false;
        }

        public short GetType()
        {
            return type;
        }

        internal Vector2 getPosition()
        {
            return position;
        }
    }
}
