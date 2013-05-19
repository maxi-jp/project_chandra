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
                else if (getValue() < 1000 || getValue() > 10000) spriteBox.SetColor(255, 0, 0, 0);
                else spriteBox.SetColor(0, 255, 0, 0);
            else if (currentState == State.mapScreen)
                if (aux == "") spriteBox.SetColor(0, 0, 255, 0);            
                else if (getValue() < 0) spriteBox.SetColor(255, 0, 0, 0);
                else spriteBox.SetColor(0, 255, 0, 0);
        }

        //-----------------------------------------------------------

        public Vector2 getPosition() 
        {
            return spriteBox.getPosition();
        }
    }//ItemChanger
}
