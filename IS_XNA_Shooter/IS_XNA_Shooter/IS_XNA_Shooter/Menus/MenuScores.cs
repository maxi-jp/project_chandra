using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class MenuScores
    {
        private SuperGame mainGame;

        private struct Row
        {
            public String playerName;
            public int score;
        };

        private List<Row> LevelA1List;
        private List<Row> LevelB1List;
        private List<Row> LevelC1List;
        private List<Row> LevelADefense1List;

        private Texture2D blackpixel;
        private Rectangle screenRectangle;

        public MenuScores(SuperGame mainGame)
        {
            this.mainGame = mainGame;

            blackpixel = GRMng.blackpixeltrans;
            screenRectangle = new Rectangle(0, 0, SuperGame.screenWidth, SuperGame.screenHeight);
        }

        public void Load()
        {
            LevelA1List = new List<Row>();
            LevelB1List = new List<Row>();
            LevelC1List = new List<Row>();
            LevelADefense1List = new List<Row>();

            Row row = new Row();
            row.playerName = "Pepito";
            row.score = 125;
            LevelA1List.Add(row);
        } // Load

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blackpixel, screenRectangle, Color.White);

            spriteBatch.DrawString(SuperGame.fontMotorwerk, "SCORES",
                new Vector2((SuperGame.screenWidth / 2) - (SuperGame.fontMotorwerk.MeasureString("SCORES").X / 2), 15),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.DrawString(SuperGame.fontMotorwerk, "LevelA1:", new Vector2(10, 30),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            foreach (Row row in LevelA1List)
            {
                spriteBatch.DrawString(SuperGame.fontMotorwerk, "Score: " + row.score, new Vector2(30, 50),
                    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontMotorwerk, "Name: " + row.playerName, new Vector2(220, 50),
                    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        } // Draw

    } // class MenuScores
}
