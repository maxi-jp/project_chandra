using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// Class of the collider
    /// </summary>
    class Collider
    {
        /// <summary>
        /// Collider's position.
        /// </summary>
        public Vector2 position;
        
        /// <summary>
        /// Collider's previous position
        /// </summary>
        public Vector2 prevPosition;
        
        /// <summary>
        /// Collider's rotation
        /// </summary>
        private float rotation;
        
        /// <summary>
        /// Collider's rectangles
        /// </summary>
        private Rectangle rectangle;
       
        /// <summary>
        /// Collider's width
        /// </summary>
        public int Width;

        /// <summary>
        /// Collider's height
        /// </summary>
        public int Height;
        
        /// <summary>
        /// Collider's radious
        /// </summary>
        public float radius;
        
        /// <summary>
        /// Collider's middle position
        /// </summary>
        private bool middlePos;
        
        /// <summary>
        /// Camera
        /// </summary>
        private Camera camera;
        
        /// <summary>
        /// Collider's Original points 
        /// </summary>
        private Vector2[] pointsOrig;
       
        /// <summary>
        /// Collider's actual points
        /// </summary>
        public Vector2[] points;
       
        /// <summary>
        /// Center of the object (for the rotation)
        /// </summary>
        private Vector2 pivotPoint;
       
        /// <summary>
        /// Matrix of the collider's points
        /// </summary>
        private Matrix rotationMatrix;

        /// <summary>
        /// Collider's constructor
        /// </summary>
        /// <param name="camera">The camera</param>
        /// <param name="middlePos">Collider's  middle position</param>
        /// <param name="position">Collider's position</param>
        /// <param name="rotation">Collider's rotation</param>
        /// <param name="points">Collider's points</param>
        /// <param name="radius">Collider's radious</param>
        /// <param name="frameWidth">Collider's frame width</param>
        /// <param name="frameHeight">Collider's frame height</param>
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

        /// <summary>
        /// Collider's constructor without list of points
        /// </summary>
        /// <param name="camera">The camera</param>
        /// <param name="middlePos">Collider's  middle position</param>
        /// <param name="position">Collider's position</param>
        /// <param name="rotation">Collider's rotation</param>
        /// <param name="frameWidth">Collider's frame width</param>
        /// <param name="frameHeight">Collider's frame height</param>
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

        /// <summary>
        /// Updates the logic of the Collider
        /// </summary>
        /// <param name="newPosition">The new collider's position</param>
        /// <param name="newRotation">The new collider's rotation</param>
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

        /// <summary>
        /// Gives the collider´s rectangle
        /// </summary>
        /// <returns>rectangle</returns>
        public Rectangle getRectangle()
        {
            return rectangle;
        }

        /// <summary>
        /// Tests if its colliding to another collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>True if there is a collision</returns>
        public bool Collision(Collider other)
        {
            float AX = MathHelper.Distance(other.position.X, position.X);
            float AY = MathHelper.Distance(other.position.Y, position.Y);

            bool collision = false;
            int cont, it = 0;

            // FIRST COMPROBATION (Manhattan's distance)
            // We look if the distance between both colliders is less than the sum of the radiuos.
            if ((AX + AY) < (other.radius + radius))
            {
                // SECOND COMPROBATION (Pitagoras' theorem)
                // We look if the real distance between both positions is less than the sum of the radious
                if (((position.X - other.position.X) * (position.X - other.position.X) +
                    (position.Y - other.position.Y) * (position.Y - other.position.Y)) <
                    (radius + other.radius) * (radius + other.radius))
                {
                    // THIRD COMPROBATION (each point distances to the collider's straight line):
                    // for each "other's" point we look if is inside of "this"
                    while (!collision && it < other.points.Length)
                    {
                        // We calculate each ecuation of the 4 straight lines:
                        // y - y1 = ((y2 - y1) / (x2 - x1)) * (x - x1)
                        // We calculate the distance of the point (other.points[i]) with each straight lines.
                        // If all are positives the point is inside, if they aren't it isn't inside.
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

        /// <summary>
        /// Check colision between two colliders, but only checking if the position of the
        /// collider other is inside the collider this
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>True if there is a collision</returns>
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

        /// <summary>
        /// Check colision between "this" and a straight line (A,B)
        /// </summary>
        /// <param name="A">The first point of the straight line</param>
        /// <param name="B">The second point of the straight line</param>
        /// <returns>True if there is a collision</returns>
        public bool CollisionTwoPoints(Vector2 A, Vector2 B)
        {
            for (int i = 0; i < points.Length; i++)
                if (TwoSegmentIntersects(points[i], points[(i + 1) % points.Length],
                    A, B))
                    return true;
            return false;
        }

        /// <summary>
        /// Check if the point is inside "this"
        /// </summary>
        /// <param name="other">the point</param>
        /// <returns>True if there is a collision</returns>
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(GRMng.whitepixel, new Rectangle(rectangle.X+(int)camera.displacement.X, rectangle.Y + (int)camera.displacement.Y,
                rectangle.Width, rectangle.Height), null, Color.White);*/
            for (int i = 0; i < points.Length; i++)
                spriteBatch.Draw(GRMng.redpixel, points[i] + camera.displacement, Color.White);
        }

        /// <summary>
        /// Update the points with the rotation matrix
        /// </summary>
        private void UpdatePoints()
        {
            rotationMatrix = Matrix.CreateRotationZ(rotation);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Vector2.Transform(pointsOrig[i] - pivotPoint, rotationMatrix);
                points[i] += position;
            }
        }

        /// <summary>
        /// The distance of the point to the segment
        /// </summary>
        /// <param name="A">First point of the segment</param>
        /// <param name="B">Second point of the segment</param>
        /// <param name="p">The point</param>
        /// <returns>The distance</returns>
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

        /// <summary>
        /// The distance between two segments
        /// </summary>
        /// <param name="p1">First point of the first segment</param>
        /// <param name="p2">Second point of the first segment</param>
        /// <param name="p3">First point of the second segment</param>
        /// <param name="p4">Second point of the second segment</param>
        /// <returns></returns>
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

        /// <summary>
        /// Inside
        /// </summary>
        /// <returns>false</returns>
        private bool Inside()
        {
            return false;
        }

    } // class Collider
}
