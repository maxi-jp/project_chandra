using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter.Input
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
            else if (key == Keys.Back && text.Length > 0) 
                text = text.Substring(0, text.Length - 1);
        }


        //---------------------------------------------------------------------------


        public String getText()
        {
            return text;
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
