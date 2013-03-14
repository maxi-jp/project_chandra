using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    class LevelA : Level
    {
        //private Camera camera;
        private Texture2D whitePixel;
        private Texture2D textureBg;

        private bool testingEnemies;

        public LevelA(Camera camera, int num, List<Enemy> enemies)
            : base()
        {
            testingEnemies = false;

            this.camera = camera;

            switch (num)
            {
                case 0: // Level for testing enemies
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    testingEnemies = true;

                    break;

                case 1:
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    LeerArchivoXML(0,0);
                    
                    break;
            }

            whitePixel = GRMng.whitepixel;
            textureBg = GRMng.textureCell;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            int i = 0; // iterator for the list of enemies
            bool stillAlive = false; // is true if there is any enemie alive
            //the next loop searches an enemy alive for controlling the end of level 
            if (!levelFinished)
            {
                while (i < enemies.Count && !stillAlive)
                {
                    if (enemies[i] != null && !enemies[i].isDead())
                    {
                        stillAlive = true;
                    }
                    i++;
                }
                if (!stillAlive)
                    levelFinished = true;
            }

            if (testingEnemies)
            {
                TestEnemies();
            }

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // grid del suelo
            for (int i = 0; i < width; i += textureBg.Width)
                for (int j = 0; j < height; j += textureBg.Height)
                    spriteBatch.Draw(textureBg, new Vector2(i + camera.displacement.X, j + camera.displacement.Y),
                        Color.White);

            // linea de arriba:
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y,
                width, 1), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la derecha:
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y,
                1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de abajo:
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X + width, (int)camera.displacement.Y,
                1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la izquierda:
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y + height,
                width, 1), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void setShip(Ship ship)
        {
            base.setShip(ship);
        }

        private void TestEnemies()
        {
            Enemy enemy;

            // EnemyWeak:
            if (ControlMng.f1Preshed)
            {
                enemy = new EnemyWeak(camera, this, new Vector2(20, 20), 0, GRMng.frameWidthEW1,
                    GRMng.frameHeightEW1, GRMng.numAnimsEW1, GRMng.frameCountEW1, GRMng.loopingEW1,
                    SuperGame.frameTime12, GRMng.textureEW1, 0, 100, 100, 1, ship);
                enemies.Add(enemy);
            }

            // EnemyWeakShot:
            if (ControlMng.f2Preshed)
            {
                enemy = new EnemyWeakShot(camera, this, new Vector2(20, 20), 0, GRMng.frameWidthEW2,
                    GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2, GRMng.loopingEW2,
                    SuperGame.frameTime12, GRMng.textureEW2, 0, 100, 100, 1, ship);
                enemies.Add(enemy);
            }

            // EnemyBeamA:
            if (ControlMng.f3Preshed)
            {
                enemy = new EnemyBeamA(camera, this, new Vector2(60, 60), 0, GRMng.frameWidthEB1,
                    GRMng.frameHeightEB1, GRMng.numAnimsEB1, GRMng.frameCountEB1, GRMng.loopingEB1,
                    SuperGame.frameTime12, GRMng.textureEB1, 0, 1000, 100, 1, ship);
                enemies.Add(enemy);
            }

            // EnemyMineShot
            if (ControlMng.f4Preshed)
            {
                enemy = new EnemyMineShot(camera, this, new Vector2(60, 60), 0, GRMng.frameWidthEMS,
                    GRMng.frameHeightEMS, GRMng.numAnimsEMS, GRMng.frameCountEMS, GRMng.loopingEMS,
                    SuperGame.frameTime12, GRMng.textureEMS, 0, 20, 100, 1, ship);
                enemies.Add(enemy);
            }

            // EnemyLaser
            if (ControlMng.f5Preshed)
            {
                enemy = new EnemyLaserA(camera, this, new Vector2(60, 60), 0, GRMng.frameWidthES,
                    GRMng.frameHeightES, GRMng.numAnimsES, GRMng.frameCountES, GRMng.loopingES,
                    SuperGame.frameTime12, GRMng.textureES, 0, 100, 100, 1, ship);
                enemies.Add(enemy);
            }

            // EnemyScared
            if (ControlMng.f6Preshed)
            {
                enemy = new EnemyScared(camera, this, new Vector2(60, 60), 0, GRMng.frameWidthES,
                    GRMng.frameHeightES, GRMng.numAnimsES, GRMng.frameCountES, GRMng.loopingES,
                    SuperGame.frameTime12, GRMng.textureES, 0, 200, 100, 1, ship);
                enemies.Add(enemy);
            }

        } // TestEnemies

    } // class LevelA
}
