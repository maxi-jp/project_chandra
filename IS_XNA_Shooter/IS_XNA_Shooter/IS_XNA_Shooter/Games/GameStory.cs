using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    class GameStory : Game
    {
        /// <summary>
        /// Enum to indicate the state of the Story mode
        /// </summary>
        public enum StoryState
        {
            levelDialog,
            beginLevel,
            playing,
            shipDestroyed,
            levelComplete
        };

        /// <summary>
        /// Struct that represents one line of dialoge
        /// </summary>
        private struct Talk
        {
            // 0=Chandra, 1=Captain, 2=Robot, 3=Base
            public int whoTalk;
            public String whatSaid;
        };

        /// <summary>
        /// The current state of the Story mode
        /// </summary>
        private StoryState currentState;

        private Game currentGame;
        private GRMng grManager;
        private Audio audio;
        private XMLLvlMng LvlMng;

        public Sprite spriteGetReady;
        public Sprite spriteNum;
        private float timeToResume, timeToResumeAux;

        // Attributes for the conversations 
        private int currentConver = 0;
        private int currentParagraph = 0;
        private List<List<Talk>> gameDialog;

        private String[] levelList;
        private int currentLevel;

        private bool finished = false;

        /*public GameStory(SuperGame mainGame, GRMng grManager, Audio audio, XMLLvlMng LvlMng,
            Player player, float shipVelocity, int shipLife)
            : base(mainGame, player, shipVelocity, shipLife)
        {
            this.grManager = grManager;
            this.audio = audio;
            this.LvlMng = LvlMng;

            timeToResume = timeToResumeAux = SuperGame.timeToResume;

            audio.LoadContent(1);
            grManager.LoadContent("Portraits"); // characters portraits sprites

            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
                GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));

            gameDialog = new List<List<Talk>>();
            LvlMng.LoadContent(2); // Load the dialogs
            readConversationXML(0);
            setTimeToResume();

            currentLevel = 0;
            levelList = new String[] { "LevelB1", "LevelA1", "LevelB2", "LevelA2" };
            InitGame(levelList[currentLevel]);
            currentState = StoryState.levelDialog;
        }*/

        public GameStory(SuperGame mainGame, GRMng grManager, Audio audio, XMLLvlMng LvlMng,
            Player player, Evolution evolution)
            : base(mainGame, player, evolution)
        {
            this.grManager = grManager;
            this.audio = audio;
            this.LvlMng = LvlMng;

            timeToResume = timeToResumeAux = SuperGame.timeToResume;

            audio.LoadContent(1);
            grManager.LoadContent("Portraits"); // characters portraits sprites

            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
                GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));

            gameDialog = new List<List<Talk>>();
            LvlMng.LoadContent("Dialogs"); // Load the dialogs
            readConversationXML(0);
            setTimeToResume();

            currentLevel = 0;
            levelList = new String[] { "LevelB1", "LevelA1", "LevelB2", "LevelA2" };
            InitGame(levelList[currentLevel]);
            currentState = StoryState.levelDialog;
        }

        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentState)
            {
                case StoryState.levelDialog:
                    timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0 || ControlMng.leftClickPreshed || ControlMng.rightClickPreshed)
                    {
                        currentParagraph++; //update the current conversation 
                        setTimeToResume();
                        if (currentParagraph >= gameDialog[currentConver].Count)
                        {
                            currentConver++;
                            currentParagraph = 0;
                            currentState = StoryState.beginLevel;
                            timeToResumeAux = 3f;
                        }
                    }
                    break;

                case StoryState.beginLevel:
                    timeToResumeAux -= deltaTime;
                    if (timeToResumeAux <= 0)
                    {
                        currentState = StoryState.playing;
                        timeToResumeAux = timeToResume;
                    }
                    else if (timeToResumeAux >= timeToResume * 2 / 3)
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    else if (timeToResumeAux >= timeToResume / 3)
                        spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));
                    else
                        spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));
                    break;

                case StoryState.playing:
                    currentGame.Update(gameTime);
                    if (currentGame.IsFinished())
                        currentState = StoryState.levelComplete;
                    break;

                case StoryState.levelComplete:

                    timeToResumeAux -= deltaTime;

                    if (timeToResumeAux <= 0)
                    {
                        currentLevel++;
                        if (currentLevel < levelList.Length)
                        {
                            // start the next level
                            InitGame(levelList[currentLevel]);
                            currentState = StoryState.levelDialog;
                            timeToResumeAux = 5f;
                        }
                        else
                        {
                            finished = true;
                            //mainGame.ExitToMenu();
                        }
                    }
                    else if (timeToResumeAux >= 5f * 2 / 3)
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    else if (timeToResumeAux >= 5f / 3)
                        spriteNum.SetRectangle(new Rectangle(171, 80, 170, 150));
                    else
                        spriteNum.SetRectangle(new Rectangle(0, 80, 170, 150));

                    break;

            } // switch (currentState)

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case StoryState.levelDialog:

                    currentGame.Draw(spriteBatch);

                    spriteBatch.Draw(GRMng.blackpixeltrans, new Rectangle(100, 500, 1100, 200), Color.White);
                    switch (gameDialog[currentConver][currentParagraph].whoTalk)
                    {
                        case 0:
                            spriteBatch.Draw(GRMng.texturePilot, new Rectangle(110, 510, 280, 180), Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(GRMng.textureCaptain, new Rectangle(110, 510, 280, 180), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(GRMng.texturePilotCyborg, new Rectangle(110, 510, 280, 180), Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(GRMng.portrait_allyourbase, new Rectangle(110, 510, 280, 180), Color.White);
                            break;
                    }

                    spriteBatch.DrawString(SuperGame.fontDebug, gameDialog[currentConver][currentParagraph].whatSaid,
                        new Vector2(400, 510), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);

                    break;

                case StoryState.beginLevel:
                    currentGame.Draw(spriteBatch);

                    spriteGetReady.DrawRectangle(spriteBatch);
                    spriteNum.DrawRectangle(spriteBatch);

                    break;

                case StoryState.playing:
                    currentGame.Draw(spriteBatch);
                    break;

            } // switch (currentState)

        } // Draw

        private void InitGame(String cad)
        {
            // Load the content for the new level & unload the content of the previous one
            if (currentLevel > 0)
                grManager.UnloadContent(levelList[currentLevel-1]);
            grManager.LoadContent(cad);

            switch (cad)
            {
                case "LevelB1":
                    LvlMng.LoadContent(cad); // Load the rectangles
                    currentGame = new GameB(mainGame, player, cad, GRMng.textureAim, evolution);
                    break;

                case "LevelA1":
                    LvlMng.LoadContent(cad); // Load the levelA's enemies
                    currentGame = new GameA(mainGame, player, cad, GRMng.textureAim, GRMng.textureCell, evolution);
                    break;

                case "LevelB2": // final boss: DORITO FUCKER
                    LvlMng.LoadContent(cad); // Load the level map 2
                    currentGame = new GameB(mainGame, player, cad, GRMng.textureAim, evolution);
                    break;

                case "LevelA2":
                    LvlMng.LoadContent(cad); // Load the levelA's enemies
                    currentGame = new GameA(mainGame, player, cad, GRMng.textureAim, GRMng.textureCell, evolution);
                    break;
            }
            
        } // InitGame

        /// <summary>
        /// set timeToResumeAux to an apropiate time corresponding to the lenght of the text
        /// </summary>
        private void setTimeToResume()
        {
            if ((currentConver < gameDialog.Count) &&
                (currentParagraph < gameDialog[currentConver].Count)) //if not out of range
                timeToResumeAux = gameDialog[currentConver][currentParagraph].whatSaid.Length * 0.07f;
        }

        /// <summary>
        /// Reads the conversation of the level to show it.
        /// </summary>
        /// <param name="level">indicates the corresponding conversation of the level</param>
        private void readConversationXML(int level)
        {
            // Utilizar nombres de fichero y nodos XML idénticos a los que se guardaron

            try
            {
                //  Leer los datos del archivo
                switch (level)
                {
                    case 0:
                        Talk talk;
                        int i = 1;
                        XmlDocument dialog = XMLLvlMng.xmlDialogs;

                        XmlNodeList conversation = dialog.GetElementsByTagName("conversation" + i);

                        while (conversation != null)
                        {

                            XmlNodeList lista =
                                ((XmlElement)conversation[0]).GetElementsByTagName("paragraph");
                            List<Talk> conver = new List<Talk>();
                            foreach (XmlElement nodo in lista)
                            {
                                talk = new Talk();
                                XmlAttributeCollection paragraph = nodo.Attributes;
                                talk.whoTalk = Convert.ToInt16(paragraph[0].Value);
                                talk.whatSaid = Convert.ToString(paragraph[1].Value);

                                conver.Add(talk);

                            }
                            gameDialog.Add(conver);
                            i++;
                            conversation = dialog.GetElementsByTagName("conversation" + i);
                        }
                        break;
                }

            }
            catch (Exception e)
            {/*
                System.Diagnostics.Debug.WriteLine("Excepción al leer fichero XML: " + e.Message);
                if (e.Data != null)
                {
                    System.Diagnostics.Debug.WriteLine("    Detalles extras:");
                    foreach (DictionaryEntry entrada in e.Data)
                        Console.WriteLine("        La clave es '{0}' y el valor es: {1}", entrada.Key, entrada.Value);
                }*/
            }
        } // readConversationXML

        public override bool IsFinished()
        {
            // TODO: ñapa de cojones
            return finished;
        }

    } // class GameB
}
