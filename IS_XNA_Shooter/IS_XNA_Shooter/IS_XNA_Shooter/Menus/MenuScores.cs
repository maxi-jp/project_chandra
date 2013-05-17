using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;

namespace IS_XNA_Shooter
{
    class MenuScores
    {
        private SuperGame mainGame;

        // state atributes
        private enum MenuScoresState
        {
            LevelA1,
            LevelB1,
            LevelC1,
            LevelADefense1
        }
        private MenuScoresState currentState;

        // graphics atributes
        private Texture2D splash01;
        private Sprite splash02;
        private Sprite titleSprite;
        private float splash02RotationVelocity = -0.02f;

        private Texture2D blackpixel;
        private Rectangle screenRectangle;

        private Vector2 backButtonPosition; // posicion de la opcion "back"
        private MenuItem itemBack;

        // scores atributes
        private struct Row
        {
            public String playerName;
            public int score;
        };
        private List<Row> LevelA1List;
        private List<Row> LevelB1List;
        private List<Row> LevelC1List;
        private List<Row> LevelADefense1List;

        private XmlDocument xmlScores;

        public MenuScores(SuperGame mainGame)
        {
            this.mainGame = mainGame;
            currentState = MenuScoresState.LevelA1;

            backButtonPosition = new Vector2(5, SuperGame.screenHeight - 45);

            splash01 = GRMng.menuSplash01; // the main background
            splash02 = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight + 10),
                1, GRMng.menuSplash02); // the planet
            titleSprite = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, 50),
                0, GRMng.menuScores, new Rectangle(0, 0, 512, 92));

            blackpixel = GRMng.blackpixeltrans;
            screenRectangle = new Rectangle(0, 0, SuperGame.screenWidth, SuperGame.screenHeight);

            itemBack = new MenuItem(false, backButtonPosition, GRMng.menuMain,
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));
        }

        public void Load()
        {
            LevelA1List = new List<Row>();
            LevelB1List = new List<Row>();
            LevelC1List = new List<Row>();
            LevelADefense1List = new List<Row>();

            LoadXml();
        } // Load

        public void Update(int X, int Y, float deltaTime)
        {
            splash02.rotation += splash02RotationVelocity * deltaTime;

            itemBack.Update(X, Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splash01, Vector2.Zero, Color.White); // splash
            splash02.Draw(spriteBatch); // planet
            titleSprite.DrawRectangle(spriteBatch); // SCORES title

            switch (currentState)
            {
                case MenuScoresState.LevelA1:
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "LevelA1:", new Vector2(10, 30),
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    for (int i=0; i<LevelA1List.Count; i++)
                    {
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, "Score: " + LevelA1List[i].score,
                            new Vector2(30, 30 * (i+1)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, "Name: " + LevelA1List[i].playerName,
                            new Vector2(220, 30 * (i+1)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    }
                    break;
            } // switch

            itemBack.Draw(spriteBatch);

        } // Draw

        public void Click(int X, int Y)
        {
            itemBack.Click(X, Y);
        }

        public void Unclick(int X, int Y)
        {
            if (itemBack.Unclick(X, Y))
            {
                Audio.PlayEffect("digitalAcent01");
                mainGame.ReturnFromScores();
            }
        }

        private void LoadXml()
        {
            xmlScores = new XmlDocument();
            xmlScores.Load("../../../../IS_XNA_ShooterContent/scores.xml");

            XmlNodeList level = xmlScores.GetElementsByTagName("scores");

            // variables auxiliares para la lectura de los layers
            String name, levelName; // player name
            int score;
            XmlAttributeCollection levelNode, entryNode;
            List<Row> currentListLevel = null;
            Row currentRow;

            XmlNodeList listLevels = ((XmlElement)level[0]).GetElementsByTagName("level");
            foreach (XmlElement nodo in listLevels)
            {
                levelNode = nodo.Attributes;
                levelName = levelNode[0].Value; // ej "LevelA1"

                switch (levelName)
                {
                    case "LevelA1":         currentListLevel = LevelA1List;         break;
                    case "LevelB1":         currentListLevel = LevelB1List;         break;
                    case "LevelC1":         currentListLevel = LevelC1List;         break;
                    case "LevelADefense1":  currentListLevel = LevelADefense1List;  break;
                    default:
                        // TODO lauch an exception
                        break;
                }

                XmlNodeList entryList = nodo.GetElementsByTagName("entry");
                foreach (XmlElement nodo1 in entryList)
                {
                    entryNode = nodo1.Attributes;
                    name = entryNode[0].Value;
                    score = (int)Convert.ToDouble(entryNode[1].Value);

                    currentRow = new Row();
                    currentRow.playerName = name;
                    currentRow.score = score;
                    currentListLevel.Add(currentRow);
                }
            }

        } // LoadXml

    } // class MenuScores
}
