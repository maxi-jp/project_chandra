using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class LevelA : Level
    {
        //private Camera camera;
        private Texture2D whitePixel;
        private Texture2D textureBg;

        public LevelA(Camera camera, int num, List<Enemy> enemies)
            : base()
        {
            this.camera = camera;

            switch (num)
            {
                case 1:
                    width = 1200;
                    height = 800;
                    playerInitPosition = new Vector2(width / 2, height / 2);
                    this.enemies = enemies;
                    timeLeftEnemy = new List<float>();

                    /*Enemy enemy1 = new EnemyWeak(camera, this, new Vector2(100, 100), 0,
                        GRMng.frameWidthEW1, GRMng.frameHeightEW1, GRMng.numAnimsEW1, GRMng.frameCountEW1,
                        GRMng.loopingEW1, SuperGame.frameTime12, GRMng.textureEW1, 80, 100, null);
                    enemies.Add(enemy1);
                    timeLeftEnemy.Add(2);*/
                    /* Para test de colisiones (enemigos quietos)
                    Enemy enemy1 = new EnemyWeak(camera, this, new Vector2(100, 100), 0,
                        GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                        GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 0, 100, null);
                    enemies.Add(enemy1);
                    timeLeftEnemy.Add(1);
                    Enemy enemy2 = new EnemyWeak(camera, this, new Vector2(300, 100), 0,
                        GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                        GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 0, 100, null);
                    enemies.Add(enemy2);
                    timeLeftEnemy.Add(1);
                    Enemy enemy3 = new EnemyWeak(camera, this, new Vector2(500, 100), 0,
                        GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                        GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 0, 100, null);
                    enemies.Add(enemy3);
                    timeLeftEnemy.Add(1);
                    Enemy enemy4 = new EnemyWeak(camera, this, new Vector2(700, 100), 0,
                        GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                        GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 0, 100, null);
                    enemies.Add(enemy4);
                    timeLeftEnemy.Add(1);*/

                    /*Enemy enemy;
                    for (int i = 0; i < 40; i++)
                    {
                        enemy = new EnemyWeak(camera, this, new Vector2(100, 100), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, null);
                        enemies.Add(enemy);
                        timeLeftEnemy.Add(1+i);
                    }
                    for (int i = 0; i < 40; i++)
                    {
                        enemy = new EnemyWeak(camera, this, new Vector2(1000, 100), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, null);
                        enemies.Add(enemy);
                        timeLeftEnemy.Add(1 + i);
                    }
                    for (int i = 0; i < 40; i++)
                    {
                        enemy = new EnemyWeak(camera, this, new Vector2(100, 700), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, null);
                        enemies.Add(enemy);
                        timeLeftEnemy.Add(1+i);
                    }
                    for (int i = 0; i < 40; i++)
                    {
                        enemy = new EnemyWeak(camera, this, new Vector2(1000, 700), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, null);
                        enemies.Add(enemy);
                        timeLeftEnemy.Add(1 + i);
                    }*/

                    LeerArchivoXML(0,0);
                    
                    break;
            }

            whitePixel = GRMng.whitepixel;
            textureBg = GRMng.textureCell;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // grid del suelo
            for (int i = 0; i < width; i += textureBg.Width)
                for (int j = 0; j < height; j += textureBg.Height)
                    spriteBatch.Draw(textureBg, new Vector2(i + camera.displacement.X, j + camera.displacement.Y), Color.White);


            // linea de arriba:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, 0, width, 1), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y, width, 1),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la derecha:
            //spriteBatch.Draw(whitePixel, new Rectangle(width, 0, 1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y, 1, height),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de abajo:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, height, width, 1), null, Color.White, w0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X + width, (int)camera.displacement.Y, 1, height),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la izquierda:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, 0, 1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y + height, width, 1),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
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

        }

        public override void setPlayer(Player player) {
            base.setPlayer(player);
        }
    }
}
