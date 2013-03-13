using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class ShipB : Ship
    {
        //---------------------------
        //----    Constructor    ----
        //---------------------------
        public ShipB(Camera camera, LevelB level, Vector2 position, float rotation, Vector2[] colliderPoints,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, List<Shot> shots)
            : base(camera, level, position, rotation, colliderPoints, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, shots)
        {
            //this.collider = new Collider(camera, true, position, rotation, GRMng.pointsP1, frameWidth, frameHeight);
        }



        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // comprobamos que el Ship no se salga del nivel
            position.X = MathHelper.Clamp(position.X, 0 + collider.Width / 2, SuperGame.screenWidth - collider.Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + collider.Height / 2, SuperGame.screenHeight - collider.Height / 2);
        }
    }
}
