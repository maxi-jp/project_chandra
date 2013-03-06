using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase que gesiona la cámara del juego
    class Camera
    {
        /* ------------------- ATRIBUTOS ------------------- */
        private bool followPlayer;  //si la camara sigue al jugador.
        private Ship player;  //jugador
        private Level level;    //nivel
        private Vector2 screenCenter;   // punto central de la pantalla
        public Vector2 displacement;    // posicion en pantalla de las coordenadas 0,0 del mundo
        public Vector2 position; // punto central de la cámara



        /* ------------------- CONSTRUCTORES ------------------- */
        public Camera()
        {
            position = new Vector2();
            screenCenter = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2);
            displacement = new Vector2();

            followPlayer = true;
        }

        public Camera(Ship player, int X, int Y)
        {
            this.player = player;
            position.X = X;
            position.Y = Y;

            followPlayer = true;
        }



        /* ------------------- MÉTODOS PÚBLICOS ------------------- */
        public void setLevel(Level level)
        {
            this.level = level;
            //worldInit = new Vector2(level.X1, level.Y1);
            if (level is LevelB)
            {
                position.Y = level.height / 2;
                position.X = SuperGame.screenWidth / 2;
            }
        }

        public void setPlayer(Ship player)
        {
            this.player = player;
            if (level != null && level is LevelA)
            {
                position = player.position;
            }
        }

        public void Update(float deltaTime)
        {
            if (level is LevelA)
            {
                movement2();
            }
            else
            {
                movement1(deltaTime);
            }

            displacement = screenCenter - position;
        }


        //--------------------------------
        //----    MÉTODOS PRIVADOS    ----
        //--------------------------------

        //La nave siempre está en el centro de la cámara
        private void movement1(float deltaTime)
        {
            //position = player.position;
            //position.X += 100 * deltaTime ;
        }

        //Los niveles deben madir 800x800 como mínimo
        //La cámara varía en función de la nave, cuando quedan 400 pixels para llegar a alguna de las paredes, va el doble de lento que el jugador
        private void movement2()
        {
            if (player.position.X < level.width/2)
                position.X = (player.position.X + level.width / 2) / 2;
            else if (player.position.X > level.width - level.width / 2)
                position.X = level.width - (level.width - player.position.X + level.width / 2) / 2;
            else
                position.X = player.position.X;

            if (player.position.Y < level.height/2)
                position.Y = (player.position.Y + level.height / 2) / 2;
            else if (player.position.Y > level.height - level.height / 2)
                position.Y = level.height - (level.height - player.position.Y + level.height / 2) / 2;
            else
                position.Y = player.position.Y;
        }
    } // Camera
}
