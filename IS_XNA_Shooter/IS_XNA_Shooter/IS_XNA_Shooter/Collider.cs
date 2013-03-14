using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class Collider
    {
        /* ------------------- ATRIBUTOS ------------------- */
        public Vector2 position, prevPosition;
        private float rotation;
        private Rectangle rectangle;
        public int Width, Height;
        public float radius;
        private bool middlePos;
        private Camera camera;
        private Vector2[] pointsOrig;
        public Vector2[] points;
        private Vector2 pivotPoint; // centro del objeto (para la rotación)
        private Matrix rotationMatrix; // matriz de rotación

        /* ------------------- CONSTRUCTORES ------------------- */
        public Collider(Camera camera, bool middlePos, Vector2 position, float rotation, Vector2[] points,
            float radius, int frameWidth, int frameHeight)
        {
            this.camera = camera;
            this.middlePos = middlePos;
            this.position = position;
            this.rotation = rotation;
            this.radius = radius;
            Width = frameWidth;
            Height = frameHeight;

            prevPosition = new Vector2();

            //radius = (float)Math.Sqrt(((frameWidth / 2) * (frameWidth / 2)) + ((frameHeight / 2) * (frameHeight / 2)));
            if (middlePos)
                pivotPoint = new Vector2(frameWidth / 2, frameHeight / 2);
            else
                pivotPoint = Vector2.Zero;

            this.points = points;
            
            pointsOrig = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++)
                pointsOrig[i] = new Vector2(points[i].X, points[i].Y);

            rectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }

        // constructura sin lista de puntos
        public Collider(Camera camera, bool middlePos, Vector2 position, float rotation,
            int frameWidth, int frameHeight)
        {
            this.camera = camera;
            this.middlePos = middlePos;
            this.position = position;
            this.rotation = rotation;
            Width = frameWidth;
            Height = frameHeight;

            radius = (float)Math.Sqrt(((frameWidth / 2) * (frameWidth / 2)) + ((frameHeight / 2) * (frameHeight / 2)));
            if (middlePos)
                pivotPoint = new Vector2(frameWidth / 2, frameHeight / 2);
            else
                pivotPoint = Vector2.Zero;

            points = new Vector2[4];
            points[0] = new Vector2(0, 0);
            points[1] = new Vector2(frameWidth, 0);
            points[2] = new Vector2(frameWidth, frameHeight);
            points[3] = new Vector2(0, frameHeight);

            pointsOrig = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++)
                pointsOrig[i] = new Vector2(points[i].X, points[i].Y);

            rectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(Vector2 newPosition, float newRotation)
        {
            prevPosition = position;
            position = newPosition;
            rotation = newRotation;

            if (middlePos)
            {
                rectangle.X = (int)newPosition.X - Width / 2;
                rectangle.Y = (int)newPosition.Y - Height / 2;

                UpdatePoints();
            }
            else
            {
                rectangle.X = (int)newPosition.X;
                rectangle.Y = (int)newPosition.Y;
            }
        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }

        // comprueba si this esta colisionando con el Collider other
        public bool Collision(Collider other)
        {
            float AX = MathHelper.Distance(other.position.X, position.X);
            float AY = MathHelper.Distance(other.position.Y, position.Y);

            bool collision = false;
            int cont, it = 0;

            // PRIMERA COMPROBACIÓN (distancia de Manhattan):
            // miramos si la distancia de separación entre los colliders
            // es menor que la suma de los radios.
            if ((AX + AY) < (other.radius + radius))
            {
                // SEGUNDA COMPROBACIÓN (teorema de Pitágoras):
                // se mira si la distancia real entre los position de ambos collider
                // es menor que la suma de sus radios.
                if (((position.X - other.position.X) * (position.X - other.position.X) +
                    (position.Y - other.position.Y) * (position.Y - other.position.Y)) <
                    (radius + other.radius) * (radius + other.radius))
                {
                    // TERCERA COMPROBACIÓN (distancias de cada punto a las rectas del collider this):
                    // por cada uno de los puntos que componen other se mira si esta
                    // dentro de this.
                    while (!collision && it < other.points.Length)
                    {
                        // calcular ecuaciones de cada una de las 4 rectas y - y1 = ((y2 - y1) / (x2 - x1)) * (x - x1)
                        // calcular la distancia del punto other.points[i] con cada
                        // una de esas 4 rectas
                        // si todas son positivas el punto esta dentro, si no, no.
                        cont = points.Length;
                        for (int j = 0; j < points.Length; j++)
                        {
                            if (DistancePointToSegment(points[j], points[(j + 1) % points.Length], other.points[it]) < 0)
                                cont--;
                        }
                        if (cont == 0)
                            return true;
                        it++;
                    }
                    return false;
                }
                else return false;
                //return rectangle.Intersects(other.getRectangle());
            }
            else return false;
        }

        // Check colision between two colliders, but only checking if the position of the
        // collider other is inside the collider this
        public bool CollisionPoint(Collider other)
        {
            float AX = MathHelper.Distance(other.position.X, position.X);
            float AY = MathHelper.Distance(other.position.Y, position.Y);

            float d = 0;
            int cont = points.Length;

            // 1st: Manhattan distance
            if ((AX < radius) || (AY < radius))
            {
                // 2nd: distance point to segments
                for (int i = 0; i < points.Length; i++)
                {
                    d = DistancePointToSegment(points[i], points[(i + 1) % points.Length], other.position);
                    if (d < 0)
                        cont--;
                }
                if (cont == 0)
                    return true;
                else // 3rd: all segments intersections
                {
                    // http://pier.guillen.com.mx/algorithms/07-geometricos/07.4-interseccion_segmentos.htm
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (TwoSegmentIntersects(points[i], points[(i + 1) % points.Length],
                            other.prevPosition, other.position))
                            return true;
                    }
                    return false;
                }
            }
            else return false;
        }

        public bool CollisionTwoPoints(Vector2 A, Vector2 B)
        {
            for (int i = 0; i < points.Length; i++)
                if (TwoSegmentIntersects(points[i], points[(i + 1) % points.Length],
                    A, B))
                    return true;
            return false;
        }

        // comprueba si el punto other esta dentro de this
        public bool Collision(Vector2 other)
        {
            float AX = MathHelper.Distance(other.X, position.X);
            float AY = MathHelper.Distance(other.Y, position.Y);

            float d = 0;

            int cont = points.Length;
            if ((AX < radius) || (AY < radius))
            {
                //return rectangle.Contains((int)other.X, (int)other.Y);
                for (int i = 0; i < points.Length; i++)
                {
                    d = DistancePointToSegment(points[i], points[(i + 1) % points.Length], other);
                    if (d < 0)
                        cont--;
                }
                return (cont == 0);
            }
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(GRMng.whitepixel, new Rectangle(rectangle.X+(int)camera.displacement.X, rectangle.Y + (int)camera.displacement.Y,
                rectangle.Width, rectangle.Height), null, Color.White);*/
            for (int i = 0; i < points.Length; i++)
                spriteBatch.Draw(GRMng.redpixel, points[i] + camera.displacement, Color.White);
        }

        private void UpdatePoints()
        {
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Vector2.Transform(pointsOrig[i] - pivotPoint, rotationMatrix);
                points[i] += position;
            }
        }

        private float DistancePointToSegment(Vector2 A, Vector2 B, Vector2 p)
        {
            /*
            Vector2 d = B - A;
            float length = d.Length();
            if (length == 0)
                return (p - A).Length();
            d.Normalize();
            float intersect = Vector2.Dot((p - A), d);
            if (intersect < 0)
                return (p - A).Length();
            if (intersect > length) return (p - B).Length();
                return (p - (A + d * intersect)).Length();*/

            /*
            //vector de la recta normalizado
            Vector2 v = B - A;
            v.Normalize();

            //determinar el punto de la línéa más próximo p
            float distanceAlongLine = Vector2.Dot(p, v) - Vector2.Dot(A, v);
            Vector2 nearestPoint;
            if (distanceAlongLine < 0)
            {
                //el punto más cercano es A
                nearestPoint = A;
            }
            else if (distanceAlongLine > Vector2.Distance(A, B))
            {
                //el punto más cercano es B
                nearestPoint = B;
            }
            else
            {
                //el punto más cercano está entre A y B... A + d  * ( ||B-A|| )
                nearestPoint = A + distanceAlongLine * v;
            }

            //Calcular la distancia entre lo dos puntos
            float dist = Vector2.Distance(nearestPoint, p);
            return dist;*/

            // http://forums.codeguru.com/showthread.php?194400-Distance-between-point-and-line-segment
            // http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
            return (((B.X - A.X)*(A.Y - p.Y) - (A.X - p.X)*(B.Y - A.Y)) /
                (float)(Math.Sqrt((B.X - A.X)*(B.X - A.X) + (B.Y - A.Y)*(B.Y - A.Y))));
        }

        private bool TwoSegmentIntersects(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            float dx, dy, dx1, dy1, dx2, dy2, side1, side2;

            // side is > 0 if two points are in the same side of a line
            // < 0 if the points are in each side of the line

            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;
            dx1 = p3.X - p1.X;
            dy1 = p3.Y - p1.Y;
            dx2 = p4.X - p2.X;
            dy2 = p4.Y - p2.Y;
            side1 = (dx * dy1 - dy * dx1) * (dx * dy2 - dy * dx2);

            dx = p4.X - p3.X;
            dy = p4.Y - p3.Y;
            dx1 = p1.X - p3.X;
            dy1 = p1.Y - p3.Y;
            dx2 = p2.X - p4.X;
            dy2 = p2.Y - p4.Y;
            side2 = (dx * dy1 - dy * dx1) * (dx * dy2 - dy * dx2);

            return ((side1 < 0) && (side2 < 0));
        }

        private bool Inside()
        {
            return false;
        }

    } // class Collider
}
