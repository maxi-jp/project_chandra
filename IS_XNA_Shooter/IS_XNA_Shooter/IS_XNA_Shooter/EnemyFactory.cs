using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    static class EnemyFactory
    {
        public static Enemy GetEnemyByName(String name, Camera camera, Level level, Vector2 position, float time)
        {
            Enemy enemy = null;

            switch (name)
            {
                case "EnemyWeakA":
                    enemy = new EnemyWeakA
                    (
                        camera,
                        level,
                        position,
                        0,
                        GRMng.frameWidthEW1,
                        GRMng.frameHeightEW1,
                        GRMng.numAnimsEW1,
                        GRMng.frameCountEW1,
                        GRMng.loopingEW1,
                        SuperGame.frameTime12,
                        GRMng.textureEW1,
                        time,
                        100,
                        100,
                        1,
                        null
                    );
                    break;

                case "EnemyWeakB":
                    enemy = new EnemyWeakB
                    (
                        camera,
                        level,
                        position,
                        0,
                        GRMng.frameWidthEW1,
                        GRMng.frameHeightEW1,
                        GRMng.numAnimsEW1,
                        GRMng.frameCountEW1,
                        GRMng.loopingEW1,
                        SuperGame.frameTime12,
                        GRMng.textureEW1,
                        time,
                        100,
                        100,
                        1,
                        null
                    );
                    break;
            }

            return enemy;
        }

    } // class EnemyFactory
}
