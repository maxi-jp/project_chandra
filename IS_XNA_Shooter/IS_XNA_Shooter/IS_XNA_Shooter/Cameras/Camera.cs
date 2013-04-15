using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class that manage the game's camera
    /// </summary>
    class Camera
    {
        /// <summary>
        /// Says if the camera follows the player's ship
        /// </summary>
        private bool followShip;
       
        /// <summary>
        /// Player's ship
        /// </summary>
        private Ship Ship;
        
        /// <summary>
        /// Level
        /// </summary>
        private Level level;
        
        /// <summary>
        /// Window's center point
        /// </summary>
        private Vector2 screenCenter;
        
        /// <summary>
        /// Camera's displacement (window's position of the 0,0 world's coordinates)
        /// </summary>
        public Vector2 displacement;
        
        /// <summary>
        /// Camera's position (Camera's center point)
        /// </summary>
        public Vector2 position;



        /// <summary>
        /// Camera's constructor
        /// </summary>
        public Camera()
        {
            position = new Vector2();
            screenCenter = new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2);
            displacement = new Vector2();

            followShip = true;
        }

        /// <summary>
        /// Camera's constructor
        /// </summary>
        /// <param name="Ship">Player's ship</param>
        /// <param name="X">Camera's X position</param>
        /// <param name="Y">Camera's Y position</param>
        public Camera(Ship Ship, int X, int Y)
        {
            this.Ship = Ship;
            position.X = X;
            position.Y = Y;

            followShip = true;
        }



        /// <summary>
        /// Set the level
        /// </summary>
        /// <param name="level">The parameter to set the level</param>
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

        /// <summary>
        /// Set the player's ship
        /// </summary>
        /// <param name="Ship">The parameter to set the player's ship</param>
        public void setShip(Ship Ship)
        {
            this.Ship = Ship;
            if (level != null && level is LevelA)
            {
                position = Ship.position;
            }
        }

        /// <summary>
        /// Updates the logic of the camera
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
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

        /// <summary>
        /// The player's ship is always in the center of the camera
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        private void movement1(float deltaTime)
        {
            //position = Ship.position;
            //position.X += 100 * deltaTime ;
        }

        /// <summary>
        /// The levels have to measure 800x800 at least.
        /// The camera has to change depending on the player's ship.
        /// When there are 400 pixels to get any wall, the camera's velocity decreases to the half
        /// </summary>
        private void movement2()
        {
            if (Ship.position.X < level.width/2)
                position.X = (Ship.position.X + level.width / 2) / 2;
            else if (Ship.position.X > level.width - level.width / 2)
                position.X = level.width - (level.width - Ship.position.X + level.width / 2) / 2;
            else
                position.X = Ship.position.X;

            if (Ship.position.Y < level.height/2)
                position.Y = (Ship.position.Y + level.height / 2) / 2;
            else if (Ship.position.Y > level.height - level.height / 2)
                position.Y = level.height - (level.height - Ship.position.Y + level.height / 2) / 2;
            else
                position.Y = Ship.position.Y;
        }
    } // Camera
}
