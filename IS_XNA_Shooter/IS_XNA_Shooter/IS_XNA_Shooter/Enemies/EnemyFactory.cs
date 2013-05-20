using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    static class EnemyFactory
    {
        /// <summary>
        /// Static method that returns and Enemy in function on the String parameter
        /// </summary>
        /// <param name="name">The string that represents the name of the enemy</param>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The actual level of the game</param>
        /// <param name="ship">The player's ship</param>
        /// <param name="position">Initial position of the enemy</param>
        /// <param name="time">respawn time of the enemy</param>
        /// <returns>The new Enemy</returns>
        public static Enemy GetEnemyByName(String name, Camera camera, Level level, Ship ship,
            Vector2 position, float time)
        {
            Enemy enemy = null;

            switch (name)
            {
                case "EnemyWeakA":
                    enemy = new EnemyWeakA
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW1,
                        GRMng.frameHeightEW1,
                        GRMng.numAnimsEW1,
                        GRMng.frameCountEW1,
                        GRMng.loopingEW1,
                        SuperGame.frameTime12,
                        GRMng.textureEW1,
                        time,
                        100,    /* velocity */
                        100,    /* life */
                        10,      /* value */
                        ship
                    );
                    break;

                case "EnemyWeakADefense":
                    enemy = new EnemyWeakADefense
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW1,
                        GRMng.frameHeightEW1,
                        GRMng.numAnimsEW1,
                        GRMng.frameCountEW1,
                        GRMng.loopingEW1,
                        SuperGame.frameTime12,
                        GRMng.textureEW1,
                        time,
                        100,    /* velocity */
                        100,    /* life */
                        10,      /* value */
                        ship,
                        null    /* house */
                    );
                    break;

                case "EnemyWeakB":
                    enemy = new EnemyWeakB
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW1,
                        GRMng.frameHeightEW1,
                        GRMng.numAnimsEW1,
                        GRMng.frameCountEW1,
                        GRMng.loopingEW1,
                        SuperGame.frameTime12,
                        GRMng.textureEW1,
                        time,
                        300,    /* velocity */
                        100,    /* life */
                        10,      /* value */
                        ship    /* player's ship */
                    );
                    break;

                case "EnemyBeamA":
                    enemy = new EnemyBeamA
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEB1,
                        GRMng.frameHeightEB1,
                        GRMng.numAnimsEB1,
                        GRMng.frameCountEB1,
                        GRMng.loopingEB1,
                        SuperGame.frameTime12,
                        GRMng.textureEB1,
                        time,   /* timeToSpawn */
                        1000,   /* velocity */
                        100,    /* life */
                        40,      /* value */
                        ship    /* player's ship */
                    );
                    break;

                case "EnemyBeamADefense":
                    enemy = new EnemyBeamADefense
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEB1,
                        GRMng.frameHeightEB1,
                        GRMng.numAnimsEB1,
                        GRMng.frameCountEB1,
                        GRMng.loopingEB1,
                        SuperGame.frameTime12,
                        GRMng.textureEB1,
                        time,   /* timeToSpawn */
                        1000,   /* velocity */
                        100,    /* life */
                        40,      /* value */
                        ship,   /* player's ship */
                        null
                    );
                    break;

                case "EnemyWeakShotA":
                    enemy = new EnemyWeakShotA
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW2,
                        GRMng.frameHeightEW2,
                        GRMng.numAnimsEW2,
                        GRMng.frameCountEW2,
                        GRMng.loopingEW2,
                        SuperGame.frameTime12,
                        GRMng.textureEW2,
                        time,   /* timeToSpawn */
                        100,    /* velocity */
                        100,    /* life */
                        20,      /* value */
                        ship,   /* player's ship */
                        2,      /* timeToShot */
                        300,    /* shotVelocity */
                        200     /* shotPower */
                    );
                    break;

                case "EnemyWeakShotADefense":
                    enemy = new EnemyWeakShotADefense
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW2,
                        GRMng.frameHeightEW2,
                        GRMng.numAnimsEW2,
                        GRMng.frameCountEW2,
                        GRMng.loopingEW2,
                        SuperGame.frameTime12,
                        GRMng.textureEW2,
                        time,   /* timeToSpawn */
                        100,    /* velocity */
                        100,    /* life */
                        20,      /* value */
                        ship,   /* player's ship */
                        2,      /* timeToShot */
                        300,    /* shotVelocity */
                        200,    /* shotPower */
                        null    /* house */
                    );
                    break;

                case "EnemyWeakShotB":
                    enemy = new EnemyWeakShotB
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEW2,
                        GRMng.frameHeightEW2,
                        GRMng.numAnimsEW2,
                        GRMng.frameCountEW2,
                        GRMng.loopingEW2,
                        SuperGame.frameTime12,
                        GRMng.textureEW2,
                        time,
                        300,    /* velocity */
                        100,    /* life */
                        20,      /* value */
                        ship,   /* player's ship */
                        2,      /* timeToShot */
                        450,    /* shotVelocity */
                        200     /* shotPower */
                    );
                    break;

                case "EnemyMineShotA":
                    enemy = new EnemyMineShotA
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEMS,
                        GRMng.frameHeightEMS,
                        GRMng.numAnimsEMS,
                        GRMng.frameCountEMS,
                        GRMng.loopingEMS,
                        SuperGame.frameTime12,
                        GRMng.textureEMS,
                        time,
                        20,     /* velocity */
                        100,    /* life */
                        30,      /* value */
                        ship,   /* player's ship */
                        4,      /* timeToShot */
                        140,    /* shotVelocity */
                        200     /* shotPower */
                    );
                    break;

                case "EnemyMineShotB":
                    enemy = new EnemyMineShotB
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEMS,
                        GRMng.frameHeightEMS,
                        GRMng.numAnimsEMS,
                        GRMng.frameCountEMS,
                        GRMng.loopingEMS,
                        SuperGame.frameTime12,
                        GRMng.textureEMS,
                        time,
                        20,     /* velocity */
                        100,    /* life */
                        30,      /* value */
                        ship,   /* player's ship */
                        3,      /* timeToShot */
                        140,    /* shotVelocity */
                        200     /* shotPower */
                    );
                    break;

                case "EnemyScaredA":
                    enemy = new EnemyScaredA
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthES,
                        GRMng.frameHeightES,
                        GRMng.numAnimsES,
                        GRMng.frameCountES,
                        GRMng.loopingES,
                        SuperGame.frameTime12,
                        GRMng.textureES,
                        time,
                        200,    /* velocity */
                        100,    /* life */
                        50,      /* value */
                        ship,   /* player's ship */
                        4,      /* timeToShot */
                        300,    /* shotVelocity */
                        200     /* shotPower */
                    );
                    break;

                case "EnemyScaredADefense":
                    enemy = new EnemyScaredADefense
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthES,
                        GRMng.frameHeightES,
                        GRMng.numAnimsES,
                        GRMng.frameCountES,
                        GRMng.loopingES,
                        SuperGame.frameTime12,
                        GRMng.textureES,
                        time,
                        200,    /* velocity */
                        100,    /* life */
                        50,      /* value */
                        ship,   /* player's ship */
                        4,      /* timeToShot */
                        300,    /* shotVelocity */
                        200,    /* shotPower */
                        null    /* base */
                    );
                    break;

                case "EnemyLaserA":
                    enemy = new EnemyLaserA
                    (    
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEL,
                        GRMng.frameHeightEL,
                        GRMng.numAnimsEL,
                        GRMng.frameCountEL,
                        GRMng.loopingEL,
                        SuperGame.frameTime12,
                        GRMng.textureEL,
                        time,
                        100,    /* velocity */
                        100,    /* life */
                        70,      /* value */
                        ship    /* player's ship */
                    );
                    break;

                case "EnemyLaserB":
                    enemy = new EnemyLaserB
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthEL,
                        GRMng.frameHeightEL,
                        GRMng.numAnimsEL,
                        GRMng.frameCountEL,
                        GRMng.loopingEL,
                        SuperGame.frameTime12,
                        GRMng.textureEL,
                        time,
                        100,    /* velocity */
                        100,    /* life */
                        70,      /* value */
                        ship    /* player's ship */
                    );
                    break;

                case "FinalBossHeroe1":
                    enemy = new FinalBossHeroe1
                    (
                        camera,
                        level,
                        position,   /* initial position in the level */
                        0,          /* initial rotation */
                        GRMng.frameWidthHeroe1,
                        GRMng.frameHeightHeroe1,
                        GRMng.numAnimsHeroe1,
                        GRMng.frameCountHeroe1,
                        GRMng.loopingHeroe1,
                        SuperGame.frameTime12, 
                        GRMng.textureHeroe1,
                        time,
                        1000,   /* velocity */
                        10,      /* life */
                        200,      /* value */
                        ship    /* player's ship */
                    );
                    break;

                default:
                    throw new NotSupportedException("Enemy not found in the Factory");
            }

            return enemy;
        }

    } // class EnemyFactory
}
