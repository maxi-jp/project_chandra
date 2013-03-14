﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    // clase abstracta de la que heredan todos los juegos
    abstract class Game
    {
        /* ------------------------------------------------------------- */
        /*                           ATTRIBUTES                          */
        /* ------------------------------------------------------------- */
        protected SuperGame mainGame;
        protected IngameHub hub;

        protected Camera camera;
        protected Level level;

        protected Ship ship;
        protected float shipVelocity;
        protected int shipLife;

        protected List<Enemy> enemies;
        protected List<Shot> shots;
        protected List<Shot> shotsEnemies;
        protected List<Explosion> explosions;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public Game (SuperGame mainGame, float shipVelocity, int shipLife)
        {
            this.mainGame = mainGame;
            this.shipLife = shipLife;
            this.shipVelocity = shipVelocity;

            camera = new Camera();
            enemies = new List<Enemy>();
            shots = new List<Shot>();
            shotsEnemies = new List<Shot>();
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

            for (int i = 0; i < shotsEnemies.Count(); i++)     // shots enemies
            {
                shotsEnemies[i].Update(deltaTime);
                if (!shotsEnemies[i].IsActive())
                    shotsEnemies.RemoveAt(i);
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
                    if (enemies[i].collider != null && enemies[i].IsColisionable() && shots[j].IsActive() && enemies[i].collider.collision(shots[j].position))
                    //if (enemies[i].isActive() && shots[j].isActive() && enemies[i].collider.collision(shots[j].collider))
                    {                       
                        // nueva explosión:
                        /*Explosion newExp = new Explosion(camera, level, enemies[i].position, enemies[i].rotation, GRMng.frameWidthEx1,
                            GRMng.frameHeightEx1, GRMng.frameCountEx1, SuperGame.frameTime24, GRMng.textureExplosion1);
                        explosions.Add(newExp);*/

                        enemies[i].Damage(shots[j].GetPower());
                        
                        shots.RemoveAt(j);
                    }
                }
            }

            // enemies-shots vs player collision:
            for (int z = 0; z < shotsEnemies.Count; z ++)
            {
                Shot shot = shotsEnemies[z];

                if (ship.collider != null && ship.IsColisionable() && shot.IsActive() && ship.collider.collision(shot.position))
                {
                    // nueva explosión:
                    /*Explosion newExp = new Explosion(camera, level, enemies[i].position, enemies[i].rotation, GRMng.frameWidthEx1,
                        GRMng.frameHeightEx1, GRMng.frameCountEx1, SuperGame.frameTime24, GRMng.textureExplosion1);
                    explosions.Add(newExp);*/

                    ship.Damage(shot.GetPower());

                    shotsEnemies.RemoveAt(z);
                }
            }

            camera.Update(deltaTime);   // cámara
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            foreach (Enemy e in enemies)
                if (e.IsActive())
                    e.Draw(spriteBatch);

            foreach (Shot shot in shots)    // player shots
                shot.Draw(spriteBatch);

            foreach (Shot shot in shotsEnemies)    // enemies shots
                shot.Draw(spriteBatch);

            ship.Draw(spriteBatch);

            /*foreach (Explosion e in explosions)
                if (e.isActive())
                    e.Draw(spriteBatch);*/

            hub.Draw(spriteBatch);
        }

    } // class Game
}
