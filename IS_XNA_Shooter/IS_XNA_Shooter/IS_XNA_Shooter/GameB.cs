using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class that manage the GameB
    /// </summary>
    class GameB : Game
    {
        /// <summary>
        /// List of colliders to crashlist
        /// </summary>
        private List<Collider> colliderList;

        /// <summary>
        /// Background to game B
        /// </summary>
        private BackgroundGameB backGroundB;

        /// <summary>
        /// Background to game A
        /// </summary>
        private BackgroundGameA backGroundA;

        /// <summary>
        /// Texture that show where is the mouse
        /// </summary>
        private Texture2D textureAim;

        /// <summary>
        /// List of type level 
        ///     0 = gameB
        ///     1 -> gameA
        /// </summary>
        private List<int> levelList;

        /// <summary>
        /// Current level in the story
        /// </summary>
        private int currentLevel=0;

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Build the GameB
        /// </summary>
        /// <param name="mainGame"> The SuperGame </param>
        /// <param name="numLevel"> The number of level </param>
        /// <param name="textureAim"> The texture aim </param>
        /// <param name="ShipVelocity"> The speed of the player ship </param>
        /// <param name="ShipLife"> The life of the player ship </param>
        public GameB(SuperGame mainGame, int numLevel, Texture2D textureAim, float ShipVelocity, int ShipLife)
            : base(mainGame, ShipVelocity, ShipLife)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight, mainGame.player.GetLife());
            camera = new Camera();
            shots = new List<Shot>();
            this.textureAim = textureAim;
            this.ShipVelocity = ShipVelocity;
            this.ShipLife=ShipLife;

            levelList = new List<int>();
            levelList.Add(0); levelList.Add(1);
            levelList.Add(0); levelList.Add(1);

            initLevelB(1);
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Goes to the game of type B
        /// </summary>
        /// <param name="numLevel"></param>
        private void initLevelB(int numLevel)
        {
            colliderList = new List<Collider>();
            level = new LevelB(camera, numLevel, shots);
            enemies = ((LevelB)level).getEnemies();
            camera.setLevel(level);
            backGroundB = new BackgroundGameB(level);
            backGroundA = null;
            Ship = new ShipB(camera, ((LevelB)level), Vector2.Zero, 0, puntosColliderShip(), GRMng.frameWidthPA2,
                GRMng.frameHeightPA2, GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24,
                GRMng.texturePA2, ShipVelocity + 200, ShipLife, shots);
            level.setShip(Ship);
            camera.setShip(Ship);
        }

        /// <summary>
        /// Goes to the game of type A
        /// </summary>
        /// <param name="numLevel"></param>
        /// <param name="textureAim"></param>
        private void initLevelA(int numLevel, Texture2D textureAim)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight, mainGame.player.GetLife());
            level = new LevelA(camera, numLevel, enemies, shots);
            backGroundA = new BackgroundGameA(camera, level);
            backGroundB = null;
            camera.setLevel(level);
            Ship = new ShipA(camera, level, Vector2.Zero, 0, puntosColliderShip(), GRMng.frameWidthPA2, GRMng.frameHeightPA2,
                GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24, 
                GRMng.texturePA2, ShipVelocity + 200, ShipLife, shots);
            level.setShip(Ship);
            camera.setShip(Ship);            
        }

        /// <summary>
        /// Get the collision points of the player ship
        /// </summary>
        /// <returns> Vector with the points </returns>
        private Vector2[] puntosColliderShip()
        {
            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(15, 35);
            points[1] = new Vector2(26, 33);
            points[2] = new Vector2(34, 15);
            points[3] = new Vector2(65, 30);
            points[4] = new Vector2(65, 50);
            points[5] = new Vector2(34, 66);
            points[6] = new Vector2(26, 47);
            points[7] = new Vector2(15, 45);

            return points;
        }

        /// <summary>
        /// Loads a new level
        /// </summary>
        /// <param name="level"> Type level to load </param>
        private void initNextLevel(int level)
        {
            enemies.Clear();
            switch (level)
            {
                case 0:
                    initLevelB(1);
                    break;
                case 1:
                    initLevelA(1, textureAim);
                    break;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update the game b
        /// </summary>
        /// <param name="gameTime"> Time between last time and this </param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (level.getFinish())
            {
                currentLevel++;
                if (currentLevel < levelList.Count)
                    initNextLevel(levelList[currentLevel]);
            }

            //update the background
            if (backGroundB != null)
                backGroundB.Update(deltaTime);
        }

        /// <summary>
        /// Draw the components in the game b
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //draw the background
            if (backGroundB != null)
                backGroundB.Draw(spriteBatch);

            //draw the ship, the enemies and the bullets
            if (backGroundA != null)
                backGroundA.Draw(spriteBatch);

            base.Draw(spriteBatch);

            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Ship=" + Ship.position + ".",
                    new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                spriteBatch.DrawString(SuperGame.fontDebug, "FinalBoss=" + enemies[0].position + ".",
                    new Vector2(5, 28), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                spriteBatch.DrawString(SuperGame.fontDebug, "ScreenHeight=" + SuperGame.screenHeight + ".",
                    new Vector2(5, 41), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }

    }//GameB
}
