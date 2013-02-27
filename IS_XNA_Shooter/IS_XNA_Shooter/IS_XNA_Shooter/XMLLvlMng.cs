using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace IS_XNA_Shooter
{
    class XMLLvlMng // XML Level Manager
    {
        public static XmlDocument lvl1;     // XML level 1

        public XMLLvlMng()
        {
            // TODO: Complete member initialization
        }

        public void LoadContent(int i)
        {
            // i:
            // 0=GameAg

            switch (i)
            {
                case 0: // menu:
                    lvl1 = new XmlDocument();
                    lvl1.Load("../../../../IS_XNA_ShooterContent/Levels/level1.xml");
                    break;
            }
        } // LoadContent

       

        public void UnloadContent(int i)
        {
            // i:
            // 0=GameA

            switch (i)
            {
                case 0: // menu:
                    //lvl1.Dispose();
                    lvl1 = null;
                    break;
            } 
        } // UnloadContent

    } // class XMLLvlMng
}
