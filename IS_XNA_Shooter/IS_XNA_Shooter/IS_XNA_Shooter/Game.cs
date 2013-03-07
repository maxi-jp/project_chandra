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
        /*                           ATRIBUTOS                           */
        /* ------------------------------------------------------------- */
        protected SuperGame mainGame;
        protected Level level;
        protected IngameHub hub;
        protected Ship ship;
        protected float shipVelocity = 200f;
        protected int shipLife;
        protected List<Enemy> enemies;
        protected List<Shot> shots;
        protected List<Explosion> explosions;
        protected Camera camera;
        
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
            explosions = new List<Explosion>();

            //Audio.PlayMusic(1);
        }

        /* ------------------------------------------------------------- */
        /*                            MÉTODOS                            */
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
                {
                    enemies[i].Update(deltaTime);
                    List<Shot> enemyShotsAux = e.getShots();
                    if (enemyShotsAux != null)
                    {
                        for (int i = 0; i < enemyShotsAux.Count(); i++)// disparos
                        {
                            enemyShotsAux[i].Update(deltaTime);
                            if (!enemyShotsAux[i].IsActive())
                                enemyShotsAux.RemoveAt(i);
                        }
                    }
                }
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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            foreach (Enemy e in enemies)
                if (e.IsActive())
                {
                    e.Draw(spriteBatch);

                    List<Shot> enemyShotsAux = e.getShots();
                    foreach (Shot shot in enemyShotsAux)
                        shot.Draw(spriteBatch);
                }
            foreach (Shot shot in shots)
                shot.Draw(spriteBatch);

            ship.Draw(spriteBatch);

            /*foreach (Explosion e in explosions)
                if (e.isActive())
                    e.Draw(spriteBatch);*/

        }

    } // class Game
}
