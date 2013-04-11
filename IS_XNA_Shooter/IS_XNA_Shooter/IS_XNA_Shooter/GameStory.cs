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

        // Attributes for the conversations 
        private int currentConver = 0;
        private int currentParagraph=0;
        private List<List<Talk>> gameDialog;


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


        public GameStory(SuperGame mainGame, Player player, float shipVelocity, int shipLife)
            : base(mainGame, player, shipVelocity, shipLife)
        {
            InitGameA(1);
            currentState = StoryState.playing;
        }


        //--------------------------------
        //----    Métodos públicos    ----
        //--------------------------------
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentState)
            {
                case StoryState.playing:
                    currentGame.Update(gameTime);
                    break;

            } // switch (currentState)

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case StoryState.playing:
                    currentGame.Draw(spriteBatch);
                    break;

            } // switch (currentState)

        } // Draw

        private void InitGameA(int num)
        {
            switch (num)
            {
                case 1:
                    currentGame = new GameA(mainGame, player, 1, GRMng.textureAim, GRMng.textureCell,
                        shipVelocity, shipLife);
                    break;
            }

        } // InitGameA

        private void InitGameB(int num)
        {
            switch (num)
            {
                case 1:

                    break;
            }

        } // InitGameB

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
