using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This class manage the input keyboard to write.
    /// </summary>
    class KeyboardHandler
    {
        private Keys[] lastPressedKeys;
        private String text;
        private int limitLength;


        //--------------------------------------------------------------------------


        /// <summary>
        /// Builder.
        /// </summary>
        public KeyboardHandler()
        {
            lastPressedKeys = new Keys[0];
            text = "";
            limitLength = 9999;
        }

        public KeyboardHandler(int limit)
        {
            lastPressedKeys = new Keys[0];
            text = "";
            limitLength = limit;
        }


        //--------------------------------------------------------------------------


        private void OnKeyDown(Keys key)
        {
            //do stuff
        }

        private void OnKeyUp(Keys key)
        {
            if ((key == Keys.NumPad0 || key == Keys.D0) && text.Length < limitLength) text += '0';
            else if ((key == Keys.NumPad1 || key == Keys.D1) && text.Length < limitLength) text += '1';
            else if ((key == Keys.NumPad2 || key == Keys.D2) && text.Length < limitLength) text += '2';
            else if ((key == Keys.NumPad3 || key == Keys.D3) && text.Length < limitLength) text += '3';
            else if ((key == Keys.NumPad4 || key == Keys.D4) && text.Length < limitLength) text += '4';
            else if ((key == Keys.NumPad5 || key == Keys.D5) && text.Length < limitLength) text += '5';
            else if ((key == Keys.NumPad6 || key == Keys.D6) && text.Length < limitLength) text += '6';
            else if ((key == Keys.NumPad7 || key == Keys.D7) && text.Length < limitLength) text += '7';
            else if ((key == Keys.NumPad8 || key == Keys.D8) && text.Length < limitLength) text += '8';
            else if ((key == Keys.NumPad9 || key == Keys.D9) && text.Length < limitLength) text += '9';
            else if ((key == Keys.A) && text.Length < limitLength) text += 'A';
            else if ((key == Keys.B) && text.Length < limitLength) text += 'B';
            else if ((key == Keys.C) && text.Length < limitLength) text += 'C';
            else if ((key == Keys.D) && text.Length < limitLength) text += 'D';
            else if ((key == Keys.E) && text.Length < limitLength) text += 'E';
            else if ((key == Keys.F) && text.Length < limitLength) text += 'F';
            else if ((key == Keys.G) && text.Length < limitLength) text += 'G';
            else if ((key == Keys.H) && text.Length < limitLength) text += 'H';
            else if ((key == Keys.I) && text.Length < limitLength) text += 'I';
            else if ((key == Keys.J) && text.Length < limitLength) text += 'J';
            else if ((key == Keys.K) && text.Length < limitLength) text += 'K';
            else if ((key == Keys.L) && text.Length < limitLength) text += 'L';
            else if ((key == Keys.M) && text.Length < limitLength) text += 'M';
            else if ((key == Keys.N) && text.Length < limitLength) text += 'N';
            else if ((key == Keys.O) && text.Length < limitLength) text += 'O';
            else if ((key == Keys.P) && text.Length < limitLength) text += 'P';
            else if ((key == Keys.Q) && text.Length < limitLength) text += 'Q';
            else if ((key == Keys.R) && text.Length < limitLength) text += 'R';
            else if ((key == Keys.S) && text.Length < limitLength) text += 'S';
            else if ((key == Keys.T) && text.Length < limitLength) text += 'T';
            else if ((key == Keys.U) && text.Length < limitLength) text += 'U';
            else if ((key == Keys.V) && text.Length < limitLength) text += 'V';
            else if ((key == Keys.W) && text.Length < limitLength) text += 'W';
            else if ((key == Keys.X) && text.Length < limitLength) text += 'X';
            else if ((key == Keys.Y) && text.Length < limitLength) text += 'Y';
            else if ((key == Keys.Z) && text.Length < limitLength) text += 'Z';
            else if (key == Keys.Back && text.Length > 0) 
                text = text.Substring(0, text.Length - 1);
        }


        //---------------------------------------------------------------------------


        public String getText()
        {
            return text;
        }

        public void setText(String text)
        {
            this.text = text;
        }


        //---------------------------------------------------------------------------


        /// <summary>
        /// Method Update.
        /// </summary>
        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }

            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

    }//class KeyboardHandler
}
