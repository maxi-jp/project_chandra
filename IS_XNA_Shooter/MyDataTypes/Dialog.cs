using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDataTypes
{
    /// <summary>
    /// The structure of the dialog XML that is used in the game.
    /// </summary>
    class Dialog
    {
        /// <summary>
        /// Paragraph that will be shown in the game.
        /// </summary>
        public struct paragraphS
        {
            /// <summary>
            /// Indicates who talks.
            /// </summary>
            public int who;

            /// <summary>
            /// The text of who is talking.
            /// </summary>
            public String text;
        }

        /// <summary>
        /// Struct of the conversation.
        /// </summary>
        public struct conver
        {
            /// <summary>
            /// Array of the paragraphs that conforms the conversation.
            /// </summary>
            public paragraphS[] paragraph;
        }

        /// <summary>
        /// Array of conversations that conforms a level.
        /// </summary>
        public conver[] conversation;
    }
}
