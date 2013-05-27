using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class RectangleMap
    {
        public int width;
        public int height;
        public List<Rectangle> rectangleList;

        public RectangleMap(int width, int height, List<Rectangle> rectangleList)
        {
            this.width = width;
            this.height = height;
            this.rectangleList = rectangleList;
        }

        public void Draw(SpriteBatch spriteBatch, float displacement)
        {
            // se pinta el primero de otro color para diferenciar las listas
            spriteBatch.Draw(GRMng.bluepixeltrans,
                    new Rectangle(rectangleList[0].X + (int)displacement,
                        rectangleList[0].Y,
                        rectangleList[0].Width,
                        rectangleList[0].Height),
                    Color.White);
            for (int i = 1; i<rectangleList.Count; i++)
                spriteBatch.Draw(GRMng.redpixeltrans,
                    new Rectangle(rectangleList[i].X + (int)displacement,
                        rectangleList[i].Y,
                        rectangleList[i].Width,
                        rectangleList[i].Height),
                    Color.White);
            /*foreach (Rectangle rec in rectangleList)
                spriteBatch.Draw(GRMng.redpixeltrans,
                    new Rectangle(rec.X + displacement, rec.Y, rec.Width, rec.Height),
                    Color.White);*/
        } // Draw

        public void UpdateRectanglesScale(float scale)
        {
            width = (int)(width * scale);
            height = (int)(height * scale);

            Rectangle rec;
            for (int i=0; i<rectangleList.Count(); i++)
            {
                rec = rectangleList[i];
                rec.X = (int)(rectangleList[i].X * scale);
                rec.Y = (int)(rectangleList[i].Y * scale);
                rec.Width = (int)(rectangleList[i].Width * scale);
                rec.Height = (int)(rectangleList[i].Height * scale);
                rectangleList[i] = rec;
            }
        } // UpdateRectanglesScale

    } // class RectangleMap
}
