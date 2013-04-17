using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyDataTypes
{
    /// <summary>
    /// Used only for the XML
    /// </summary>
    class LevelXML
    {
        public struct enemyS
        {
            public String type;
            public float positionX;
            public float positionY;
            public float time;
        }


        public String title;
        public float width;
        public float height;
        public enemyS[] enemyList;
    }
}
