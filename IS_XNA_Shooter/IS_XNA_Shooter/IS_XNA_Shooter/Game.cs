using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    // clase abstracta de la que heredan todos los juegos
    abstract class Game
    {
        /* ------------------------------------------------------------- */
        /*                           ATTRIBUTES                          */
        /* ------------------------------------------------------------- */
        protected SuperGame mainGame;
        protected Player player;
        protected IngameHub hub;

        protected Camera camera;
        protected Level level;

        protected Ship ship;
        protected float shipVelocity;
        protected int shipLife;

        protected List<Enemy> enemies;
        protected List<Shot> shots;
        protected List<Explosion> explosions;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public Game (SuperGame mainGame, Player player, float shipVelocity, int shipLife)
        {
            this.mainGame = mainGame;
            this.player = player;
            this.shipLife = shipLife;
            this.shipVelocity = shipVelocity;

            camera = new Camera();
            enemies = new List<Enemy>();
            shots = new List<Shot>();
            explosions = new List<Explosion>();

            //Audio.PlayMusic(1);
        }

        /* ------------------------------------------------------------- */
        /*                            METHODS                            */
        /* ------------------------------------------------------------- */
        public virtual void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            level.Update(deltaTime);

            ship.Update(deltaTime);     // Player ship

            for (int i = 0; i < enemies.Count(); i++)   // enemies
            {

                if (enemies[i].IsErasable())
                    enemies.RemoveAt(i);
                else if (enemies[i].IsActive())
                    enemies[i].Update(deltaTime);
                else
                    enemies[i].UpdateTimeToSpawn(deltaTime);
            }

            for (int i = 0; i < shots.Count(); i++)     // shots
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
            }

            /*for (int i = 0; i < explosions.Count(); i++)// explosiones
            {
                explosions[i].Update(deltaTime);
                if (!explosions[i].isActive())
                    explosions.RemoveAt(i);
            }*/

            // player-shots vs enemies collisions:
            for (int i = 0; i < enemies.Count(); i++)
            {
                for (int j = 0; j < shots.Count(); j++)
                {
                    if (enemies[i].IsColisionable() && shots[j].IsActive() && enemies[i].collider.collision(shots[j].position))
                    //if (enemies[i].isActive() && shots[j].isActive() && enemies[i].collider.collision(shots[j].collider))
                    {                       
                        // nueva explosión:
                        /*Explosion newExp = new Explosion(camera, level, eAux.position, eAux.rotation, GRMng.frameWidthEx1,
                            GRMng.frameHeightEx1, GRMng.frameCountEx1, SuperGame.frameTime24, GRMng.textureExplosion1);
                        explosions.Add(newExp);*/

                        enemies[i].Damage(shots[j].GetPower());

                        shots.RemoveAt(j);
                    }
                }
            }

            camera.Update(deltaTime);   // cámara

            if (SuperGame.debug)
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                    PlayerDead();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            foreach (Enemy e in enemies)
                if (e.IsActive())
                    e.Draw(spriteBatch);

            foreach (Shot shot in shots)    // player shots
                shot.Draw(spriteBatch);

            ship.Draw(spriteBatch);

            /*foreach (Explosion e in explosions)
                if (e.isActive())
                    e.Draw(spriteBatch);*/

            hub.Draw(spriteBatch);
        }

        // This methods is called when the ship of the player has been
        // hitted by an enemy shot and its life is < 0
        public virtual void PlayerDead()
        {
            //mainGame.TargetElapsedTime = TimeSpan.FromTicks(2000000);
            player.LoseLife();
            hub.PlayerLosesLive();

            // All the enemies and the shots must be erased:
            for (int i = 0; i < enemies.Count(); i++)
                enemies[i].Kill();
            shots.Clear();

            if (player.GetLife() == 0)
                mainGame.GameOver();
        }

    } // class Game
}
