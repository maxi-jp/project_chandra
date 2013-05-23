using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Xml.Linq;

namespace IS_XNA_Shooter
{
    class MenuScores
    {
        private SuperGame mainGame;

        // state atributes
        private enum MenuScoresState
        {
            Showing,
            Inserting
        }
        private MenuScoresState currentState;

        private enum GameTypes
        {
            Scroll,
            Survival,
            Defense,
            Killer
        }
        private GameTypes currentTypeSelected;
        private int currentLevenNumSelected;

        // graphics atributes
        private Texture2D splash01;
        private Sprite splash02;
        private Sprite titleSprite;
        private float splash02RotationVelocity = -0.02f;

        private Texture2D blackpixel;
        private Rectangle screenRectangle;

        private MenuItemSelectionButton itemScroll, itemSurvival, itemDefense, itemKiller;
        private MenuItemSelectionButton itemLevel1, itemLevel2, itemLevel3;

        private Vector2 textNamePosition, textScorePosition;

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
        private List<Row> listSelected;

        private XmlDocument xmlScores;

        // atributos necesarios cuando se va a intruducir una nueva puntuación
        //private String playerName;
        private int playerScore, scorePosition;
        private KeyboardHandler keyboardHandler;
        private Rectangle blackRect;

        public MenuScores(SuperGame mainGame)
        {
            this.mainGame = mainGame;

            textNamePosition = new Vector2((SuperGame.screenWidth / 2) - 200, 160);
            textScorePosition = new Vector2((SuperGame.screenWidth / 2) + 200, 160);

            backButtonPosition = new Vector2(5, SuperGame.screenHeight - 45);

            splash01 = GRMng.menuSplash01; // the main background
            splash02 = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight + 10),
                1, GRMng.menuSplash02); // the planet
            titleSprite = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, 50),
                0, GRMng.menuScores, new Rectangle(0, 0, 512, 92)); // the title: "SCORES"

            blackpixel = GRMng.blackpixeltrans;
            screenRectangle = new Rectangle(0, 0, SuperGame.screenWidth, SuperGame.screenHeight);

            itemScroll = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) - 300, 130),
                GRMng.menuScores, new Rectangle(0, 92, 170, 40), new Rectangle(170, 92, 170, 40), new Rectangle(340, 92, 170, 40));
            itemSurvival = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) - 100, 130),
                GRMng.menuScores, new Rectangle(0, 132, 170, 40), new Rectangle(170, 132, 170, 40), new Rectangle(340, 132, 170, 40));
            itemDefense = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) + 100, 130),
                GRMng.menuScores, new Rectangle(0, 172, 170, 40), new Rectangle(170, 172, 170, 40), new Rectangle(340, 172, 170, 40));
            itemKiller = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) + 300, 130),
                GRMng.menuScores, new Rectangle(0, 212, 170, 40), new Rectangle(170, 212, 170, 40), new Rectangle(340, 212, 170, 40));

            itemLevel1 = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) - 420, (SuperGame.screenHeight / 2) - 160), MathHelper.ToRadians(-90),
                GRMng.menuScores, new Rectangle(0, 252, 170, 40), new Rectangle(170, 252, 170, 40), new Rectangle(0, 252, 170, 40));
            itemLevel2 = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) - 420, SuperGame.screenHeight / 2), MathHelper.ToRadians(-90),
                GRMng.menuScores, new Rectangle(170, 292, 170, 40), new Rectangle(0, 292, 170, 40), new Rectangle(170, 292, 170, 40));
            itemLevel3 = new MenuItemSelectionButton(true, new Vector2((SuperGame.screenWidth / 2) - 420, (SuperGame.screenHeight / 2) + 160), MathHelper.ToRadians(-90),
                GRMng.menuScores, new Rectangle(340, 292, 170, 40), new Rectangle(340, 252, 170, 40), new Rectangle(340, 292, 170, 40));

            itemBack = new MenuItem(false, backButtonPosition, GRMng.menuMain,
                new Rectangle(120, 360, 120, 40), new Rectangle(240, 360, 120, 40), new Rectangle(360, 360, 120, 40));
        }

        public void Load()
        {
            LevelA1List = new List<Row>();
            LevelB1List = new List<Row>();
            LevelC1List = new List<Row>();
            LevelADefense1List = new List<Row>();

            currentState = MenuScoresState.Showing;
            currentTypeSelected = GameTypes.Killer;
            currentLevenNumSelected = 1;
            listSelected = LevelA1List;

            LoadXml();
        } // Load

        public void Update(int X, int Y, float deltaTime)
        {
            splash02.rotation += splash02RotationVelocity * deltaTime;

            switch (currentState)
            {
                case MenuScoresState.Showing:
                    itemScroll.Update(X, Y);
                    itemSurvival.Update(X, Y);
                    itemDefense.Update(X, Y);
                    itemKiller.Update(X, Y);
                    itemBack.Update(X, Y);
                    break;

                case MenuScoresState.Inserting:
                    keyboardHandler.Update();
                    if (ControlMng.enterPreshed)
                    {
                        // introducimos el score en la lista y se actualiza el xml
                        Row nueva = new Row();
                        nueva.playerName = keyboardHandler.getText();
                        nueva.score = playerScore;
                        listSelected[scorePosition] = nueva;
                        SaveXml();

                        currentState = MenuScoresState.Showing;
                    }
                    break;

            } // switch

        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splash01, Vector2.Zero, Color.White); // splash
            splash02.Draw(spriteBatch); // planet
            titleSprite.DrawRectangle(spriteBatch); // SCORES title

            switch (currentState)
            {
                case MenuScoresState.Showing:

                    itemScroll.Draw(spriteBatch);
                    itemSurvival.Draw(spriteBatch);
                    itemDefense.Draw(spriteBatch);
                    itemKiller.Draw(spriteBatch);

                    itemLevel1.Draw(spriteBatch);
                    itemLevel2.Draw(spriteBatch);
                    itemLevel3.Draw(spriteBatch);

                    /*spriteBatch.DrawString(SuperGame.fontMotorwerk, "LevelA1:", new Vector2(10, 30),
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);*/
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "Name:", textNamePosition, Color.White, 0,
                        Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "Score:", textScorePosition, Color.White, 0,
                        Vector2.Zero, 1, SpriteEffects.None, 0);
                    for (int i = 0; i < listSelected.Count; i++)
                    {
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, listSelected[i].playerName,
                            new Vector2(textNamePosition.X, textNamePosition.Y + (25 * (i + 2))),
                            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, listSelected[i].score.ToString(),
                            new Vector2(textScorePosition.X, textNamePosition.Y + (25 * (i + 2))),
                            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    }
                    break;

                case MenuScoresState.Inserting:

                    itemScroll.Draw(spriteBatch);
                    itemSurvival.Draw(spriteBatch);
                    itemDefense.Draw(spriteBatch);
                    itemKiller.Draw(spriteBatch);

                    itemLevel1.Draw(spriteBatch);
                    itemLevel2.Draw(spriteBatch);
                    itemLevel3.Draw(spriteBatch);

                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "Name:", textNamePosition, Color.White, 0,
                        Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "Score:", textScorePosition, Color.White, 0,
                        Vector2.Zero, 1, SpriteEffects.None, 0);
                    for (int i = 0; i < listSelected.Count; i++)
                    {
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, listSelected[i].playerName,
                            new Vector2(textNamePosition.X, textNamePosition.Y + (25 * (i + 2))),
                            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                        spriteBatch.DrawString(SuperGame.fontMotorwerk, listSelected[i].score.ToString(),
                            new Vector2(textScorePosition.X, textNamePosition.Y + (25 * (i + 2))),
                            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    }

                    spriteBatch.Draw(blackpixel, blackRect, Color.White);
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "NEW RECORD!",
                        new Vector2((SuperGame.screenWidth / 2) - SuperGame.fontMotorwerk.MeasureString("NEW RECORD!").X / 2,
                            (SuperGame.screenHeight / 2) - 90), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "ENTER YOUR NAME: " + keyboardHandler.getText(),
                        new Vector2((SuperGame.screenWidth / 2) - 170,
                            (SuperGame.screenHeight / 2) - SuperGame.fontMotorwerk.MeasureString("A").Y / 2),
                        Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(SuperGame.fontMotorwerk, "YOUR SCORE: " + playerScore + " POINTS",
                        new Vector2((SuperGame.screenWidth / 2) - SuperGame.fontMotorwerk.MeasureString("YOUR SCORE: " + playerScore + " POINTS").X / 2,
                            (SuperGame.screenHeight / 2) + 50), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    
                    break;

            } // switch

            itemBack.Draw(spriteBatch);

        } // Draw

        public void Click(int X, int Y)
        {
            switch (currentState)
            {
                case MenuScoresState.Showing:
                    itemScroll.Click(X, Y);
                    itemSurvival.Click(X, Y);
                    itemDefense.Click(X, Y);
                    itemKiller.Click(X, Y);

                    itemBack.Click(X, Y);
                    break;
            } // switch
            
        } // Click

        public void Unclick(int X, int Y)
        {
            switch (currentState)
            {
                case MenuScoresState.Showing:
                    if (itemScroll.Unclick(X, Y))
                    {
                        itemSurvival.SetSelected(false);
                        itemDefense.SetSelected(false);
                        itemKiller.SetSelected(false);

                        currentTypeSelected = GameTypes.Scroll;
                        listSelected = LevelB1List;
                    }
                    if (itemSurvival.Unclick(X, Y))
                    {
                        itemScroll.SetSelected(false);
                        itemDefense.SetSelected(false);
                        itemKiller.SetSelected(false);

                        currentTypeSelected = GameTypes.Survival;
                        listSelected = LevelC1List;
                    }
                    if (itemDefense.Unclick(X, Y))
                    {
                        itemScroll.SetSelected(false);
                        itemSurvival.SetSelected(false);
                        itemKiller.SetSelected(false);

                        currentTypeSelected = GameTypes.Defense;
                        listSelected = LevelADefense1List;
                    }
                    if (itemKiller.Unclick(X, Y))
                    {
                        itemScroll.SetSelected(false);
                        itemSurvival.SetSelected(false);
                        itemDefense.SetSelected(false);

                        currentTypeSelected = GameTypes.Killer;
                        listSelected = LevelA1List;
                    }
                    if (itemBack.Unclick(X, Y))
                    {
                        Audio.PlayEffect("digitalAcent01");
                        mainGame.ReturnFromScores();
                    }
                    break;
            } // switch
            
        } // Unclick

        public void AddNewScore(String levelName, int newScore)
        {
            switch (levelName)
            {
                case "LevelA1":
                    itemScroll.SetSelected(false);
                    itemSurvival.SetSelected(false);
                    itemDefense.SetSelected(false);
                    itemKiller.SetSelected();
                    currentTypeSelected = GameTypes.Killer;
                    listSelected = LevelA1List;
                    break;
                case "LevelADefense1":
                    itemScroll.SetSelected(false);
                    itemSurvival.SetSelected(false);
                    itemDefense.SetSelected();
                    itemKiller.SetSelected(false);
                    currentTypeSelected = GameTypes.Defense;
                    listSelected = LevelADefense1List;
                    break;
                case "LevelB1":
                    itemScroll.SetSelected();
                    itemSurvival.SetSelected(false);
                    itemDefense.SetSelected(false);
                    itemKiller.SetSelected(false);
                    currentTypeSelected = GameTypes.Scroll;
                    listSelected = LevelB1List;
                    break;
                case "LevelC1":
                    itemScroll.SetSelected(false);
                    itemSurvival.SetSelected();
                    itemDefense.SetSelected(false);
                    itemKiller.SetSelected(false);
                    currentTypeSelected = GameTypes.Survival;
                    listSelected = LevelC1List;
                    break;
            }
            playerScore = newScore;

            // se mira si la nueva puntuación es un record (supera a alguna de las 10 primeras)
            int scorePos = 10;
            bool escape = false;
            while((scorePos > 0) && (!escape))
            {
                if (listSelected[scorePos - 1].score < playerScore)
                    scorePos--;
                else
                    escape = true;
            }

            if (scorePos < 10)
            {
                scorePosition = scorePos;
                keyboardHandler = new KeyboardHandler(12);
                blackRect = new Rectangle((SuperGame.screenWidth / 2) - 200,
                    (SuperGame.screenHeight / 2) - 100, 400, 200); 
                currentState = MenuScoresState.Inserting;
            }
            else
                currentState = MenuScoresState.Showing;
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
                        // TODO launch an exception
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

        private void SaveXml()
        {
            String fileName = "../../../../IS_XNA_ShooterContent/scores.xml";

            XDocument miXML = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Level Edited"));
            XElement scores = new XElement("scores");
            
            XElement level = new XElement("level");
            level.Add(new XAttribute("name", "LevelA1"));
            for (int i = 0; i < LevelA1List.Count; i++)
            {
                level.Add(new XElement("entry",
                    new XAttribute("player", LevelA1List[i].playerName),
                    new XAttribute("score", LevelA1List[i].score)));
            }
            scores.Add(level);

            level = new XElement("level");
            level.Add(new XAttribute("name", "LevelB1"));
            for (int i = 0; i < LevelB1List.Count; i++)
            {
                level.Add(new XElement("entry",
                    new XAttribute("player", LevelB1List[i].playerName),
                    new XAttribute("score", LevelB1List[i].score)));
            }
            scores.Add(level);

            level = new XElement("level");
            level.Add(new XAttribute("name", "LevelC1"));
            for (int i = 0; i < LevelC1List.Count; i++)
            {
                level.Add(new XElement("entry",
                    new XAttribute("player", LevelC1List[i].playerName),
                    new XAttribute("score", LevelC1List[i].score)));
            }
            scores.Add(level);

            level = new XElement("level");
            level.Add(new XAttribute("name", "LevelADefense1"));
            for (int i = 0; i < LevelADefense1List.Count; i++)
            {
                level.Add(new XElement("entry",
                    new XAttribute("player", LevelADefense1List[i].playerName),
                    new XAttribute("score", LevelADefense1List[i].score)));
            }
            scores.Add(level);

            miXML.Add(scores);

            miXML.Save(fileName);

        } // SaveXml

    } // class MenuScores
}
