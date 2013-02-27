using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase padre enemigo de la que heredan todos los enemigos.
    abstract class Enemy : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */
        public Collider collider;

        protected int life;         // vida
        protected float velocity;   // velocidad
        protected Player player;    // jugador
        private bool active;        // si no esta activo no se muestra

        /* ------------------- CONSTRUCTORES ------------------- */
        public Enemy(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Player player)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime)
        {
            this.velocity = velocity;
            this.life = life;
            this.player = player;
            active = false;

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            /*Vector2[] points = new Vector2[4];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(60, 35);
            points[2] = new Vector2(60, 45);
            points[3] = new Vector2(20, 60);*/
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            collider.Update(position, rotation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (SuperGame.debug)
                collider.Draw(spriteBatch);
        }

        public bool isDead()
        {
            return (life <= 0);
        }

        public void damage(int i)
        {
            life -= i;
        }

        public bool isActive()
        {
            return active;
        }

        public void setActive(bool aux)
        {
            active = aux;
        }

        public void setActive()
        {
            active = true;
        }

        public void unsetActive()
        {
            active = false;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
        }

        public void kill()
        {
            life = -1;
        }

    } // class Enemy
}
