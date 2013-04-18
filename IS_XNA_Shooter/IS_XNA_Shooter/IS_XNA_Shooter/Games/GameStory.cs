using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace IS_XNA_Shooter
{
    class GameStory : Game
    {
        //-------------------------
        //----    Atributos    ----
        //-------------------------

        /*// private List<List<Rectangle>> crashList;  //objetos de colisión en el parallax donde se juega
        private playState currentState;
        // for levelB
        private List<RectangleMap> listRecMap; //lista de colliders para crashlist
        private int[] rectangleMap;
        private float scrollVelocity;
        private float scrollPosition;
        //for levelB
        private BackgroundGameB backGroundB; //Fondo con los parallax
        private BackgroundGameA backGroundA;
        private Texture2D textureAim;
        private List<int> levelList;
        private int currentLevel=0;
        public Sprite spriteGetReady;
        public Sprite spriteNum;
        private float timeToResumeAux;*/

        
        public enum StoryState
        {
            levelDialog,
            beginLevel,
            playing,
            shipDestroyed,
            levelComplete
        };

        private struct Talk
        {
            // 0=Chandra, 1=Captain, 2=Robot
            public int whoTalk;
            public String whatSaid;
        };

        private StoryState currentState;

        private Game currentGame;
        private GRMng grManager;
        private Audio audio;
        private XMLLvlMng LvlMng;

        public Sprite spriteGetReady;
        public Sprite spriteNum;
        private float timeToResumeAux;

        // Attributes for the conversations 
        private int currentConver = 0;
        private int currentParagraph = 0;
        private List<List<Talk>> gameDialog;

        private String[] levelList;
        private int currentLevel;

        public GameStory(SuperGame mainGame, GRMng grManager, Audio audio, XMLLvlMng LvlMng,
            Player player, float shipVelocity, int shipLife)
            : base(mainGame, player, shipVelocity, shipLife)
        {
            this.grManager = grManager;
            this.audio = audio;
            this.LvlMng = LvlMng;

            audio.LoadContent(1);
            grManager.LoadContent(98); // characters portraits sprites

            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
                GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));

            gameDialog = new List<List<Talk>>();
            LvlMng.LoadContent(2); // Load the dialogs
            readConversationXML(0);
            setTimeToResume();

            currentLevel = 0;
            levelList = new String[] { "B1", "A1", "B2", "A2" };
            InitGame(levelList[currentLevel]);
            currentState = StoryState.levelDialog;
        }

        /// <summary>
        /// new builder including the evolution
        /// </summary>
        /// <param name="mainGame"></param>
        /// <param name="grManager"></param>
        /// <param name="audio"></param>
        /// <param name="LvlMng"></param>
        /// <param name="player"></param>
        /// <param name="evolution"></param>
        public GameStory(SuperGame mainGame, GRMng grManager, Audio audio, XMLLvlMng LvlMng,
           Player player, Evolution evolution)
            : base(mainGame, player, evolution)
        {
            this.grManager = grManager;
            this.audio = audio;
            this.LvlMng = LvlMng;

            audio.LoadContent(1);
            grManager.LoadContent(98); // characters portraits sprites

            spriteGetReady = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 - 90), 0,
                GRMng.getready321, new Rectangle(0, 0, 512, 80));
            spriteNum = new Sprite(true, new Vector2(SuperGame.screenWidth / 2, SuperGame.screenHeight / 2 + 80), 0,
                GRMng.getready321, new Rectangle(0, 80, 170, 150));

            gameDialog = new List<List<Talk>>();
            LvlMng.LoadContent(2); // Load the dialogs
            readConversationXML(0);
            setTimeToResume();

            currentLevel = 0;
            levelList = new String[] { "B1", "A1", "B2", "A2" };
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
                    if (timeToResumeAux <= 0)
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
                        timeToResumeAux = 3f;
                    }
                    else if (timeToResumeAux >= 5f * 2 / 3)
                        spriteNum.SetRectangle(new Rectangle(341, 80, 170, 150));
                    else if (timeToResumeAux >= 5f / 3)
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
                            InitGame(levelList[currentLevel]);
                            currentState = StoryState.levelDialog;
                            timeToResumeAux = 5f;
                        }
                        else
                            mainGame.ExitToMenu();
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
            switch (cad)
            {
                case "B1":
                    grManager.LoadContent(4); // Load the gameB's level 1 resources
                    LvlMng.LoadContent(1); // Load the rectangles

                    currentGame = new GameB(mainGame, player, 1, GRMng.textureAim, evolution);
                    break;

                case "A1":
                    grManager.LoadContent(3); // Load the gameA's level 1 resources
                    LvlMng.LoadContent(0); // Load the levelA's enemies

                    currentGame = new GameA(mainGame, player, 1, GRMng.textureAim, GRMng.textureCell,
                        shipVelocity, shipLife);
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
                        XmlDocument dialog = XMLLvlMng.dialog1;

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

    } // class GameB
}
