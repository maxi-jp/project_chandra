using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter.MapEditor
{
    /// <summary>
    /// This class control a Sprite that has some differents states (like a button or text that
    /// change when you click over itself).
    /// </summary>
    public class ItemChanger
    {
        /// <summary>
        /// Different number of items in the Sprite.
        /// </summary>
        private int numberOfItems;
        /// <summary>
        /// Different number of states for each item.
        /// </summary>
        private int numberOfStates;
        /// <summary>
        /// The Sprite.
        /// </summary>
        private Sprite sprite;
        /// <summary>
        /// The rectangle that delimit the current Item.
        /// </summary>
        private Rectangle currentRectangle;
        /// <summary>
        /// Displacement of the rectangle.
        /// </summary>
        private int displacement;
        /// <summary>
        /// Height of the Item.
        /// </summary>
        private int heightItem;
        /// <summary>
        /// It's true when we click the Item with the button left mouse and false in other case.
        /// </summary>
        private Boolean isClicked;


        //-------------------------------------------------------------------------


        /// <summary>
        /// Builder.
        /// </summary>
        /// <param name="numItems">Number of items in the texture.</param>
        /// <param name="numStates">Number the states for each item.</param>
        /// <param name="texture">Texture</param>
        /// <param name="position">Position of the Item</param>
        public ItemChanger(int numItems, int numStates, Texture2D texture, Vector2 position)
        {
            numberOfItems = numItems;
            numberOfStates = numStates;

            displacement = 0;
            heightItem = texture.Height / (numStates * numItems);
            currentRectangle = new Rectangle(0, 0, texture.Width, heightItem);

            sprite = new Sprite(true, position, 0f, texture, currentRectangle);

            isClicked = false;
        }


        //-------------------------------------------------------------------------


        /// <summary>
        /// Method Update
        /// </summary>
        public void Update()
        {
            if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                isClicked = false;

            Rectangle rectangleItem = new Rectangle((int) sprite.getPosition().X - sprite.texture.Width / 2,
                                                    (int) sprite.getPosition().Y - heightItem / 2,
                                                    sprite.texture.Width,
                                                    heightItem);
            if (rectangleItem.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                currentRectangle.Y = (displacement + 1) * heightItem;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !isClicked)
                {
                    displacement = (displacement + 2) % 8;
                    isClicked = true;
                }
            }
            else
            {
                currentRectangle.Y = displacement * heightItem;
            }

            sprite.SetRectangle(currentRectangle);
        }

        /// <summary>
        /// Method Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.DrawRectangle(spriteBatch);
        }
    }//ItemChanger
}
