using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IS_XNA_Shooter.Input;

namespace IS_XNA_Shooter.MapEditor
{
    /// <summary>
    /// This class control a Sprite with an input keyboard.
    /// </summary>
    public class ItemInput
    {

        public enum State
        {
            sizeScreen,
            mapScreen
        }

        //******************************
        //********    CONSTS    ********
        //******************************

        private const int SIZE_MIN_VALUE = 1000, SIZE_MAX_VALUE = 10000;

        //**********************************
        //********    ATTRIBUTES    ********
        //**********************************
        
        // The box to write.
        private Sprite spriteBox;
        // The Sprite of the text.
        private SpriteFont spriteFont;
        // The rectangle that delimit the current Item.
        private Rectangle currentRectangle;
        // The handles keyboard to write the text.
        private KeyboardHandler keyboardInput;
        // Contorl if we can write here.
        private bool isClicked;
        // The state
        private State currentState;


        //-------------------------------------------------------------------------


        public ItemInput(Vector2 position, State currentState)
        {
            isClicked = false;
            spriteBox = new Sprite(false, position, 0f, GRMng.boxSizesMapEditor2,
                new Rectangle(0, GRMng.boxSizesMapEditor2.Height / 2, GRMng.boxSizesMapEditor2.Width, 
                    GRMng.boxSizesMapEditor2.Height / 2));
            spriteFont = GRMng.fontText;
            currentRectangle = new Rectangle((int)position.X, (int)position.Y, GRMng.boxSizesMapEditor2.Width,
                    GRMng.boxSizesMapEditor2.Height / 2);
            keyboardInput = new KeyboardHandler(10);
            this.currentState = currentState;
            if (currentState == State.mapScreen)
                keyboardInput.setText("1");
        }


        //-------------------------------------------------------------------------


        /// <summary>
        /// Tell us the input value
        /// </summary>
        /// <returns></returns>
        public int getValue()
        {
            String aux = keyboardInput.getText();
            if (aux != "")
                return Convert.ToInt32(aux);
            else
                return 0;
        }

        public void setText(String valor)
        {
            keyboardInput.setText(valor);
        }


        //-------------------------------------------------------------------------


        /// <summary>
        /// Method Update
        /// </summary>
        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isClicked = currentRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                if (isClicked) spriteBox.SetRectangle(new Rectangle(0, 0, 
                    GRMng.boxSizesMapEditor2.Width, GRMng.boxSizesMapEditor2.Height / 2));
                else spriteBox.SetRectangle(new Rectangle(0, GRMng.boxSizesMapEditor2.Height / 2,
                    GRMng.boxSizesMapEditor2.Width, GRMng.boxSizesMapEditor2.Height / 2));
            }
            if (isClicked)
                keyboardInput.Update();
        }

        /// <summary>
        /// Method Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            String aux = keyboardInput.getText();
            spriteBox.DrawRectangle(spriteBatch);
            spriteBatch.DrawString(spriteFont, keyboardInput.getText(), 
                new Vector2(currentRectangle.X + 10, currentRectangle.Y), Color.Black, 0f, Vector2.Zero,
                1.2f, SpriteEffects.None, 0f);
            if (currentState == State.sizeScreen)
                if (aux == "") spriteBox.SetColor(0, 0, 255, 0);
                else if (getValue() < SIZE_MIN_VALUE || getValue() > SIZE_MAX_VALUE) spriteBox.SetColor(255, 0, 0, 0);
                else spriteBox.SetColor(0, 255, 0, 0);
            else if (currentState == State.mapScreen)
                if (aux == "") spriteBox.SetColor(0, 0, 255, 0);            
                else if (getValue() < 0) spriteBox.SetColor(255, 0, 0, 0);
                else spriteBox.SetColor(0, 255, 0, 0);
        }

        //-----------------------------------------------------------

        /// <summary>
        /// Return the value
        /// </summary>
        /// <returns></returns>
        public Vector2 getPosition() 
        {
            return spriteBox.getPosition();
        }

        public bool isInRange()
        {
            if (currentState == State.sizeScreen)
                return getValue() >= SIZE_MIN_VALUE && getValue() <= SIZE_MAX_VALUE;
            else
                return getValue() > 0;
        }
    }//ItemChanger
}
