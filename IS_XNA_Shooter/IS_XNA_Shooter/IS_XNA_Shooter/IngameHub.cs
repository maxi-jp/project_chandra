using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class IngameHub
    {
        /* ------------------- ATRIBUTOS ------------------- */
        protected int lifesBase; // number of lifes of the Ship
        protected int lifesActual;

        /* ------------------- CONSTRUCTORES ------------------- */
        public IngameHub(int playerLifes)
        {
            this.lifesBase = playerLifes;
            this.lifesActual = playerLifes;
        }

        /* ------------------- MÉTODOS ------------------- */
        public virtual void Update(float deltaTime)
        {
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void PlayerLosesLive()
        {
            lifesActual--;
        }

        public virtual void PlayerEarnsLife()
        {
            lifesActual++;

            if (lifesActual > lifesBase)
                lifesBase = lifesActual;
        }

    } // class IngameHub
}
