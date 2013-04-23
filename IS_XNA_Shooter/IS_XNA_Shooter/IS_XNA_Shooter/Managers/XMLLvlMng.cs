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
        //public static XmlDocument lvl1A;     // XML level 1 A
        //public static XmlDocument lvl1B;     // XML level 1 B
        //public static XmlDocument lvl1C;     // XML level 1 C
        //public static XmlDocument rect1;    // rectangles level 1 side scroll mode
        //public static XmlDocument rect2;    // rectangles level 2 side scroll mode (DORITO)
        //public static XmlDocument dialog1;  // dialog of the first level of mode history
        public static XmlDocument xmlEnemies;
        public static XmlDocument xmlRectangles;
        public static XmlDocument xmlDialogs;

        /// <summary>
        /// Constructor for XMLLvl manager
        /// </summary>
        public XMLLvlMng()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Load the content of the game indicated by the param i
        /// </summary>
        /// <param name="cad">indicator for the content loaded</param>
        public void LoadContent(String cad)
        {
            switch (cad)
            {
                case "LevelA1": // GameA Level 1
                    xmlEnemies = new XmlDocument();
                    xmlEnemies.Load("../../../../IS_XNA_ShooterContent/Levels/level1A.xml");
                    break;

                case "LevelB1": // GameB Level 1
                    xmlRectangles = new XmlDocument();
                    xmlRectangles.Load("../../../../IS_XNA_ShooterContent/Levels/levelRectangle1.xml");
                    xmlEnemies = new XmlDocument();
                    xmlEnemies.Load("../../../../IS_XNA_ShooterContent/Levels/level1B.xml");
                    break;

                case "Dialogs": // dialogs
                    xmlDialogs = new XmlDocument();
                    xmlDialogs.Load("../../../../IS_XNA_ShooterContent/Levels/dialog1.xml");
                    break;

                case "LevelB2": // GameB Level 2 DORITO
                    xmlRectangles = new XmlDocument();
                    xmlRectangles.Load("../../../../IS_XNA_ShooterContent/Levels/levelRectangle2.xml");
                    break;

                case "LevelADefense1": // GameA Defense 1
                    xmlEnemies = new XmlDocument();
                    xmlEnemies.Load("../../../../IS_XNA_ShooterContent/Levels/level1ADefense.xml");
                    break;
            }

        } // LoadContent

       /// <summary>
       /// Unload the content of the XML level manager
       /// </summary>
       /// <param name="cad">indicates the resources to unload</param>
        public void UnloadContent(String cad)
        {
            switch (cad)
            {
                case "LevelA1": // GameA Level 1
                    xmlEnemies = null;
                    break;

                case "LevelB1": // GameB Level 1
                    xmlRectangles = null;
                    xmlEnemies = null;
                    break;

                case "Dialogs": // dialogs
                    xmlDialogs = null;
                    break;

                case "LevelB2": // GameB Level 2 DORITO
                    xmlRectangles = null;
                    break;

                case "LevelADefense1": // GameA Defense 1
                    xmlEnemies = null;
                    break;
            }
        } // UnloadContent

    } // class XMLLvlMng
}
