using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
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
        protected IngameHud hud;

        protected Camera camera;
        protected Level level;

        protected Ship ship;
        protected float shipVelocity;
        protected int shipLife;

        protected List<Enemy> enemies, enemiesBot;
        protected List<Shot> shots;
        protected List<Explosion> explosions;
        protected List<PowerUp> powerUpList;

        protected Evolution evolution;
        protected float timeToGameOver = 1.5f;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        /*public Game (SuperGame mainGame, Player player, float shipVelocity, int shipLife)
        {
            this.mainGame = mainGame;
            this.player = player;
            this.shipLife = shipLife;
            this.shipVelocity = shipVelocity;

            camera = new Camera();
            enemies = new List<Enemy>();
            enemiesBot = new List<Enemy>();
            shots = new List<Shot>();
            explosions = new List<Explosion>();
            powerUpList = new List<PowerUp>();
            
            //Audio.PlayMusic(1);
        }*/

        public Game(SuperGame mainGame, Player player, Evolution evolution)
        {
            this.mainGame = mainGame;
            this.player = player;
            //this.shipLife = shipLife;
            //this.shipVelocity = shipVelocity;
            this.evolution = evolution;

            camera = new Camera();
            enemies = new List<Enemy>();
            enemiesBot = new List<Enemy>();
            shots = new List<Shot>();
            explosions = new List<Explosion>();
            powerUpList = new List<PowerUp>();
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

            for (int i = 0; i < enemiesBot.Count(); i++)   // enemies
            {
                if (enemiesBot[i].IsErasable())
                    enemiesBot.RemoveAt(i);
                else if (enemiesBot[i].IsActive())
                    enemiesBot[i].Update(deltaTime);
                else
                    enemiesBot[i].UpdateTimeToSpawn(deltaTime);
            }

            for (int i = 0; i < powerUpList.Count; i++)    // power ups
            {
                powerUpList[i].Update(deltaTime);
                if (powerUpList[i].IsErasable())
                    powerUpList.RemoveAt(i);
            }

            for (int i = 0; i < shots.Count(); i++)     // shots
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
            }

            // player-shots vs enemies collisions:
            for (int i = 0; i < enemies.Count(); i++)
            {
                for (int j = 0; j < shots.Count(); j++)
                {
                    if (enemies[i].IsColisionable() && shots[j].IsActive() &&
                        enemies[i].collider.CollisionPoint(shots[j].collider))
                    //if (enemies[i].isActive() && shots[j].isActive() && enemies[i].collider.collision(shots[j].collider))
                    {                       
                        enemies[i].Damage(shots[j].GetPower());
                        if (enemies[i].isDead())
                        {
                            player.EarnPoints(enemies[i].GetValue());
                        }
                        PowerUp powerUp = enemies[i].GetPowerUp();
                        if (powerUp != null)
                            powerUpList.Add(powerUp);
                        if (shots[j].type == SuperGame.shootType.normal)
                            shots.RemoveAt(j);
                    }
                }
            }

            for (int i = 0; i < powerUpList.Count; i++) // powerUps for the ship
            {
                if (powerUpList[i].IsActive())
                {
                    if (ship.collider.Collision(powerUpList[i].collider)
                        /*|| powerUpList[i].collider.Collision(ship.collider)*/)
                    {
                        ship.CatchPowerUp(powerUpList[i].GetType());
                        if (powerUpList[i].GetType() == 2) //green power
                        {
                            for (int j = 0; j < enemies.Count(); j++)
                                if (enemies[j].IsActive() && !(enemies[j].GetType() == typeof(FinalBoss1) || enemies[j].GetType() == typeof(EnemyFinalHeroe2) ||
                                     enemies[j].GetType() == typeof(BotFinalBoss) || enemies[j].GetType() == typeof(FinalBossHeroe1) ||
                                     enemies[j].GetType() == typeof(FinalBoss1Turret2) || enemies[j].GetType() == typeof(FinalBoss1Turret1)))
                                    enemies[j].Damage(200);
                        }
                        powerUpList[i].ShowBanner();
                    }
                }
            }

            if (!SuperGame.godMode)
            {
                // enemies-player collision:
                for (int i = 0; i < enemies.Count(); i++)
                {
                    if (enemies[i].IsColisionable() && (ship.collider.Collision(enemies[i].collider)
                        /*|| enemies[i].collider.Collision(ship.collider)*/))
                    {
                        // the player has been hit by an enemy
                        if (ship.timePowerUpBlue > 0)
                            enemies[i].Damage(200);
                        else
                            ship.Kill();
                    }
                }
            }

            camera.Update(deltaTime);   // cámara

            if (SuperGame.debug)
            {
                if (ControlMng.kPreshed)
                    PlayerDead();
                if (ControlMng.lPreshed)
                {
                    // add one life to the player
                    player.EarnLife();
                    hud.PlayerEarnsLife();
                }
            }

            if (GameOverCondition())
            //if (player.GetLife() == 0) ||(level.GetBase() != null && level.GetBase().GetLife() <= 0))
            {
                timeToGameOver -= deltaTime;
                if (timeToGameOver <= 0)
                    mainGame.GameOver();

            }
        } // Update

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);

            foreach (PowerUp pow in powerUpList)
                pow.Draw(spriteBatch);

            foreach (Enemy e in enemies)
                if (e.IsActive())
                    e.Draw(spriteBatch);

            foreach (Enemy e in enemiesBot)
                if (e.IsActive())
                    e.Draw(spriteBatch);

            foreach (Shot shot in shots)    // player shots
                shot.Draw(spriteBatch);

            ship.Draw(spriteBatch);

            String score= "Score = " + player.GetActualScore(); 
            spriteBatch.DrawString(SuperGame.fontMotorwerk, score,
                    new Vector2(SuperGame.screenWidth/2-(score.ToString().Length)-(SuperGame.fontMotorwerk.ToString().Length), 69), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            hud.Draw(spriteBatch);
        } // Draw

        // This methods is called when the ship of the player has been
        // hitted by an enemy shot and its life is < 0
        public virtual void PlayerDead()
        {
            //mainGame.TargetElapsedTime = TimeSpan.FromTicks(2000000);
            player.LoseLife();
            hud.PlayerLosesLive();

            // All the enemies and the shots must be erased:
            for (int i = 0; i < enemies.Count(); i++)
                if (enemies[i].IsActive() && !(enemies[i].GetType() == typeof(FinalBoss1) || enemies[i].GetType() == typeof(EnemyFinalHeroe2) ||
                     enemies[i].GetType() == typeof(BotFinalBoss) || enemies[i].GetType() == typeof(FinalBossHeroe1) ||
                     enemies[i].GetType() == typeof(FinalBoss1Turret2) || enemies[i].GetType() == typeof(FinalBoss1Turret1)))
                    enemies[i].Kill();
            shots.Clear();
        }

        public virtual bool IsFinished()
        {
            return level.IsFinished();
        }

        protected virtual bool GameOverCondition()
        {
            return (player.GetLife() == 0);
        }

    } // class Game
}
