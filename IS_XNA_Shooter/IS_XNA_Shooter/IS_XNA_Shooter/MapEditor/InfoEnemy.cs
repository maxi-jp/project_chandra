using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS_XNA_Shooter.MapEditor
{
    public class InfoEnemy
    {
        public String type;
        public int positionX;
        public int positionY;
        public int time;
        public int frameWidth;
        public int frameHeight;

        public InfoEnemy(String type, int positionX, int positionY, int time, int frameWidth, int frameHeight) {
            this.type = type;
            this.positionX = positionX;
            this.positionY = positionY;
            this.time = time;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
        }



    }
}
