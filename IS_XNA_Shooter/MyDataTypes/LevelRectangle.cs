using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDataTypes
{
    class LevelRectangle
    {
        /// <summary>
        /// Attributes that determines a image and for which level is.
        /// </summary>
        public struct BGLayerS
        {
            /// <summary>
            /// The level correspondent image.
            /// </summary>
            public int nivel;

            /// <summary>
            /// Velocity of the BGLayer.
            /// </summary>
            public float velocidad;

            /// <summary>
            /// Number of Images that includes.
            /// </summary>
            public int numPNG;

            /// <summary>
            /// List of the names of images.
            /// </summary>
            public String[] png;
        }

        /// <summary>
        /// List of the Background image attributes.
        /// </summary>
        public struct listBS
        {
            /// <summary>
            /// List the items with their attributes.
            /// </summary>
            public BGLayerS[] BGLayer;
        }

        /// <summary>
        /// Atributes that determinates a rectangle.
        /// </summary>
        public struct rectangleS
        {
            /// <summary>
            /// Coordinate X.
            /// </summary>
            public int x;
            /// <summary>
            /// Coordinate Y.
            /// </summary>
            public int y;
            /// <summary>
            /// Widht of the rectangle.
            /// </summary>
            public int width;
            /// <summary>
            /// Height of the rectangle.
            /// </summary>
            public int height;
        }

        /// <summary>
        /// Contains the rectangles and aditional information
        /// </summary>
        public struct listaRectS
        {
            /// <summary>
            /// Number of Rectangles in this list.
            /// </summary>
            public int numRectangles;

            /// <summary>
            /// Total widht of the layer.
            /// </summary>
            public int width;

            /// <summary>
            /// Total height of the layer.
            /// </summary>
            public int height;
            
            /// <summary>
            /// List of the rectangles that conform a layer.
            /// </summary>
            public rectangleS[] rectangle;
        }

        /// <summary>
        /// Specifies the name of the images for the layers in a level
        /// </summary>
        public listBS listaBackgrounds;

        /// <summary>
        ///List of the rectangles that determines the colliders of the level
        /// </summary>
        public listaRectS[] listaRectangles;

        /// <summary>
        /// Determines the order in which the level backgrounds appear
        /// </summary>
        public int[] mapaRectangles;
    }
}
