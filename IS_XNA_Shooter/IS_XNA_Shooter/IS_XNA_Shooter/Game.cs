using System;
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
        protected Level level;
        protected IngameHub hub;
        protected Ship player;
        protected float playerVelocity = 200f;
        protected List<Enemy> enemies;
        protected List<Shot> shots;
        protected List<Explosion> explosions;
        protected Camera camera;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public Game (float playerVelocity)
        {
            camera = new Camera();
            enemies = new List<Enemy>();
            shots = new List<Shot>();
            explosions = new List<Explosion>();

            Audio.PlayMusic(1);
        }

        /* ------------------------------------------------------------- */
        /*                            MÉTODOS                            */
        /* ------------------------------------------------------------- */
        public virtual void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            level.Update(deltaTime);    // nivel
            player.Update(deltaTime);   // player
            foreach (Enemy e in enemies)// enemigos
                if (e.IsActive())
                {
                    e.Update(deltaTime);
                    List<Shot> enemyShotsAux = e.getShots();
                    if (enemyShotsAux != null)
                    {
                        for (int i = 0; i < enemyShotsAux.Count(); i++)// disparos
                        {
                            enemyShotsAux[i].Update(deltaTime);
                            if (!enemyShotsAux[i].isActive())
                                enemyShotsAux.RemoveAt(i);
                        }
                    }
                }
            for (int i = 0; i < shots.Count(); i++)// disparos
            {
                shots[i].Update(deltaTime);
                if (!shots[i].isActive())
                    shots.RemoveAt(i);
            }
            for (int i = 0; i < explosions.Count(); i++)// explosiones
            {
                explosions[i].Update(deltaTime);
                if (!explosions[i].isActive())
                    explosions.RemoveAt(i);
            }

            // colisiones balas-enemigos:
            for (int i = 0; i < enemies.Count(); i++)
            {
                for (int j = 0; j < shots.Count(); j++)
                {
                    if (enemies[i].IsActive() && shots[j].isActive() && enemies[i].collider.collision(shots[j].position))
                    //if (enemies[i].isActive() && shots[j].isActive() && enemies[i].collider.collision(shots[j].collider))
                    {
                        Enemy eAux = enemies[i];
                        Shot sAux = shots[j];
                        // nueva explosión:
                        Explosion newExp = new Explosion(camera, level, eAux.position, eAux.rotation, GRMng.frameWidthEx1,
                            GRMng.frameHeightEx1, GRMng.frameCountEx1, SuperGame.frameTime24, GRMng.textureExplosion1);
                        explosions.Add(newExp);

                        eAux.SetActive(false);
                        eAux.Damage(sAux.getPower());
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
            player.Draw(spriteBatch);
            foreach (Explosion e in explosions)
                if (e.isActive())
                    e.Draw(spriteBatch);
           // hub.Draw(spriteBatch);
        }

    } // class Game
}
