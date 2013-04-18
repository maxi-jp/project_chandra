using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    static class EnemyFactory
    {
        public static Enemy GetEnemyByName(String name, Camera camera, Level level, Ship ship,
            Vector2 position, float time, House house)
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
                        1,      /* value */
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
                        1,      /* value */
                        ship,
                        house
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
                        100,    /* velocity */
                        100,    /* life */
                        1,      /* value */
                        ship
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
                        1,      /* value */
                        ship    /* player's ship */
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
                        1,      /* value */
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
                        1,      /* value */
                        ship,   /* player's ship */
                        2,      /* timeToShot */
                        300,    /* shotVelocity */
                        200,     /* shotPower */
                        house
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
                        100,    /* velocity */
                        100,    /* life */
                        1,      /* value */
                        ship,   /* player's ship */
                        2,      /* timeToShot */
                        300,    /* shotVelocity */
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
                        1,      /* value */
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
                        1,      /* value */
                        ship,   /* player's ship */
                        4,      /* timeToShot */
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
                        1,      /* value */
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
                        1,      /* value */
                        ship,   /* player's ship */
                        4,      /* timeToShot */
                        300,    /* shotVelocity */
                        200,     /* shotPower */
                        house
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
                        SuperGame.frameTime10,
                        GRMng.textureEL,
                        time,
                        100,    /* velocity */
                        100,    /* life */
                        1,      /* value */
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
                        1,      /* life */
                        2,      /* value */
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
