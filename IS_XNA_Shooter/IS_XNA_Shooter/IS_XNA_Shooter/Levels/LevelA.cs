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

        private Texture2D whitePixel;
        private Texture2D textureCell;

        private bool testingEnemies;

        // attributes for the Defense mode
        private Vector2 baseInitPosition;
        private Base house;

        public LevelA(Camera camera, String levelName, List<Enemy> enemies)
            : base(camera, levelName, enemies)
        {
            testingEnemies = false;

            switch (levelName)
            {
                case "TestEnemies": // Level for testing enemies
                    levelEndCond = LevelEndCondition.infinite;
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    testingEnemies = true;
                    break;

                case "LevelA1": // Level 1 for the Defense mode
                    levelEndCond = LevelEndCondition.killemall;
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    LeerArchivoXML(0, 0);
                    break;

                case "LevelA2":
                    levelEndCond = LevelEndCondition.killemall;
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    Enemy enemy = EnemyFactory.GetEnemyByName("FinalBossHeroe1", camera, this, ship,
                        new Vector2(60, 60), 0);
                    ((FinalBossHeroe1)enemy).SetEnemies(enemies);
                    enemies.Add(enemy);
                    break;

                case "LevelADefense1":
                    levelEndCond = LevelEndCondition.killemall;
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;

                    LeerArchivoXML(0, 0);
                    break;

                case "TestParticles": // level for testing the particle system
                    width = 1200;
                    height = 800;
                    ShipInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;
                    break;
            }

            whitePixel = GRMng.whitepixel;
            textureCell = GRMng.textureCell;
        }

        public override void Update(float deltaTime)
        {
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
            // grid del suelo
            for (int i = 0; i < width; i += textureCell.Width)
                for (int j = 0; j < height; j += textureCell.Height)
                    spriteBatch.Draw(textureCell, new Vector2(i + camera.displacement.X, j + camera.displacement.Y),
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
            Enemy enemy = null;

            // EnemyWeak:
            if (ControlMng.f1Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyWeakA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyWeakShot:
            if (ControlMng.f2Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyWeakShotA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyBeamA:
            if (ControlMng.f3Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyBeamA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // EnemyMineShotA
            if (ControlMng.f4Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyMineShotA", camera, this, ship, new Vector2(20, 20), 0);
                enemies.Add(enemy);
            }

            // EnemyLaserA
            if (ControlMng.f5Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyLaserA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // EnemyScaredA
            if (ControlMng.f6Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("EnemyMineShotA", camera, this, ship, new Vector2(60, 60), 0);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f7Preshed)
            {
                enemy = new FinalBoss1Turret1(camera, this, new Vector2(60, 60), ship);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f8Preshed)
            {
                enemy = new FinalBoss1Turret2(camera, this, new Vector2(60, 60), ship);
                enemies.Add(enemy);
            }

            // Final Boss 1 Phase 4
            if (ControlMng.f9Preshed)
            {
                enemy = EnemyFactory.GetEnemyByName("FinalBossHeroe1", camera, this, ship, new Vector2(60, 60), 0);
                ((FinalBossHeroe1)enemy).SetEnemies(enemies);
                enemies.Add(enemy);
            }

            if (house != null && enemy != null)
                ((EnemyADefense)(enemy)).SetBase(house);

        } // TestEnemies

        public virtual void SetBase(Base house)
        {
            this.house = house;

            foreach (Enemy e in enemies)// enemigos
                ((EnemyADefense)e).SetBase(house);
            //house.SetPosition(BaseInitPosition);
        }

        public Base GetBase()
        {
            return this.house;
        }

        public void DamageBase(int i)
        {
            house.Damage(i);
        }

    } // class LevelA
}
