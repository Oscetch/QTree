using QTree.RayCasting;
using System;

namespace QTree.Util
{
    public struct Rectangle
    {
        public int X;
        public int Y;
        public int Bottom;
        public int Right;
        public int Width;
        public int Height;
        public readonly int Top => Y;
        public readonly int Left => X;

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            Rectangle rectangle;
            rectangle.Y = Math.Min(a.Y, b.Y);
            rectangle.Bottom = Math.Max(a.Bottom, b.Bottom);
            rectangle.X = Math.Min(a.X, b.X);
            rectangle.Right = Math.Max(a.Right, b.Right);
            rectangle.Width = rectangle.Right - rectangle.X;
            rectangle.Height = rectangle.Bottom - rectangle.Y;
            return rectangle;
        }

        public Rectangle(int x, int y, int width, int height)
            : this(x, y, width, height, x + width, y + height)
        {
        }

        private Rectangle(int x, int y, int width, int height, int right, int bottom)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Bottom = bottom;
            Right = right;
        }

        public readonly Point2D GetCenter()
        {
            Point2D center;
            center.X = Left + ((Right - Left) / 2);
            center.Y = Top + ((Bottom - Top) / 2);
            return center;
        }

        public readonly Rectangle Union(Rectangle other)
        {
            return Union(this, other);
        }

        public readonly bool Overlaps(Rectangle other)
        {
            return Left < other.Right
                && Right > other.Left
                && Bottom > other.Top
                && Top < other.Bottom;
        }

        public readonly bool Contains(Rectangle other)
        {
            return Left <= other.Left
                && Right >= other.Right
                && Bottom >= other.Bottom
                && Top <= other.Top;
        }

        public readonly bool Contains(Point2D point2D)
        {
            return Left <= point2D.X
                && Right >= point2D.X
                && Bottom >= point2D.Y
                && Top <= point2D.Y;
        }

        public readonly bool Contains(int x, int y)
        {
            return Left <= x
                && Right >= x
                && Bottom >= y
                && Top <= y;
        }

        public readonly void Split(out Rectangle topLeft,
            out Rectangle topRight,
            out Rectangle bottomLeft,
            out Rectangle bottomRight)
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            var rightX = X + halfWidth;
            var bottomY = Y + halfHeight;

            topLeft = new Rectangle(X, Y, halfWidth, halfHeight, rightX, bottomY);
            topRight = new Rectangle(rightX, Y, halfWidth, halfHeight, Right, bottomY);
            bottomLeft = new Rectangle(X, bottomY, halfWidth, halfHeight, rightX, Bottom);
            bottomRight = new Rectangle(rightX, bottomY, halfWidth, halfHeight, Right, Bottom);
        }

        private readonly Point2D[] GetEdges() =>
        [
            new Point2D(Left, Top),
            new Point2D(Right, Top),
            new Point2D(Left, Bottom),
            new Point2D(Right, Bottom),
        ];

        public readonly bool IntersectsRay(Ray ray, out PointF2D intersection)
        {
            intersection = new PointF2D(0, 0);
            var foundIntersection = false;
            var minDistance = float.MaxValue;
            var edges = GetEdges();
            for (var i = 0; i < edges.Length; i++)
            {
                var p1 = edges[i];
                var p2 = edges[(i + 1) % edges.Length];

                if (ray.IntersectsLine(p1, p2, out var newIntersection))
                {
                    foundIntersection = true;
                    var distance = PointF2D.Distance(ray.Start, newIntersection);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        intersection = newIntersection;
                    }
                }
            }
            return foundIntersection;
        }

        public readonly bool IntersectsRayFast(Ray ray)
        {
            var edges = GetEdges();
            for (var i = 0; i < edges.Length; i++)
            {
                var p1 = edges[i];
                var p2 = edges[(i + 1) % edges.Length];

                if (ray.IntersectsLine(p1, p2, out _))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
