using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class MenuItemSelectionButton : MenuItem
    {

        private bool selected;

        public MenuItemSelectionButton(bool middlePosition, Vector2 position, Texture2D texture,
            Rectangle rectIddle, Rectangle rectSelected, Rectangle rectPushed)
            : base(middlePosition, position, texture, rectIddle, rectSelected, rectPushed)
        {
            selected = false;
        }

        public MenuItemSelectionButton(bool middlePosition, Vector2 position, float rotation,
            Texture2D texture, Rectangle rectIddle, Rectangle rectSelected, Rectangle rectPushed)
            : base(middlePosition, position, rotation, texture, rectIddle, rectSelected, rectPushed)
        {
            selected = false;
        }

        public override void Update(int X, int Y)
        {
            /*if (!selected)
            {
                if (rectangle.Contains(X, Y))
                    rectActual = rectSelected;
                else
                    rectActual = rectIddle;

                if (preshed && !rectangle.Contains(X, Y))
                {
                    rectActual = rectIddle;
                    preshed = selected = false;
                }
            }
            */

        }

        public override bool Click(int X, int Y)
        {
            if (rectangle.Contains(X, Y))
            {
                preshed = true;
                //selected = true;
                rectActual = rectPushed;
                return true;
            }
            else
                return false;
            /*
            if (rectangle.Contains(X, Y))
            {
                preshed = true;
                rectActual = rectPushed;
                return true;
            }
            else
                return false;*/
        }

        public override bool Unclick(int X, int Y)
        {
            if (rectangle.Contains(X, Y) && preshed)
            {
                //preshed = false;
                selected = true;
                return true;
            }
            else
                return false;
        }

        public void SetSelected()
        {
            selected = true;
            rectActual = rectPushed;
        }

        public void SetSelected(bool aux)
        {
            selected = aux;
            if (selected)
                rectActual = rectPushed;
            else
                rectActual = rectIddle;
        }

    } // class MenuItemSelectionButton
}
