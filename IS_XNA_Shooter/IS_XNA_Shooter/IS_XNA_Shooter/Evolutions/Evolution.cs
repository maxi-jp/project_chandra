using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Update our ship at the begin of each game.
    /// </summary>
    class Evolution
    {
        private const float LIFE = 50,
                            POWER_ATTACK = 50,
                            SPEED_SHIP = 10,
                            SPEED_SHOT = 10,
                            CADENCE = 0.1f;


        //-------------------------------------------------------------------------------


        private float life,
                    powerAttack,
                    speedShip,
                    speedShot,
                    cadence;
        private List<Boolean> lifeUpdate, powerAttackUpdate, speedShipUpdate, speedShotUpdate, cadenceUpdate;


        //-------------------------------------------------------------------------------


        public Evolution()
        {
            life = 100;
            powerAttack = 200;
            speedShip = 200;
            speedShot = 250;
            cadence = 1;

            initializeList(lifeUpdate, 10);
            initializeList(powerAttackUpdate, 10);
            initializeList(speedShipUpdate, 10);
            initializeList(speedShotUpdate, 10);
            initializeList(cadenceUpdate, 10);
        }


        //-------------------------------------------------------------------------------


        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(0, 0);
            spriteBatch.DrawString(SuperGame.fontDebug, "life: " + life, pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.DrawString(SuperGame.fontDebug, "power attack: " + powerAttack, pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.DrawString(SuperGame.fontDebug, "speed ship: " + speedShip, pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.DrawString(SuperGame.fontDebug, "speed shot: " + speedShot, pos, Color.White);
            pos += new Vector2(0, 20);
            spriteBatch.DrawString(SuperGame.fontDebug, "cadence: " + cadence, pos, Color.White);
        }


        //-------------------------------------------------------------------------------


        private void initializeList(List<Boolean> list, int size)
        {
            list = new List<Boolean>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(false);
            }
        }

    }//class Evolution
}
