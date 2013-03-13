using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class EnemyWeakB : Enemy
    {

        public EnemyWeakB(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship Ship)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, velocity, life, value, Ship)
        {
            Vector2[] points = new Vector2[7];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 40);
            points[4] = new Vector2(49, 51);
            points[5] = new Vector2(32, 57);
            points[6] = new Vector2(21, 57);
            collider = new Collider(camera, true, position, rotation, points, 40, frameWidth, frameHeight);

            setAnim(1);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            position.X += deltaTime * velocity;
        }

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                setAnim(2, -1);
                Audio.PlayEffect("brokenBone01");
            }
        }
        

        
    }
}
