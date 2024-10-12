using System;
using QTree.Util;

namespace QTree.RayCasting
{
    public struct Ray
    {
        public Point2D Start;
        public PointF2D Direction;

        public Ray(Point2D start, PointF2D direction)
        {
            Start = start;
            Direction = direction;
            Direction.Normalize();
        }
        public Ray(Point2D start, float angleInRadians)
        {
            Start = start;
            Direction = new((float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians));
        }

        public readonly bool IntersectsLine(Point2D p1, Point2D p2, out PointF2D intersection)
        {
            intersection = new (0f, 0f);
            float x1 = p1.X, y1 = p1.Y;
            float x2 = p2.X, y2 = p2.Y;
            float x3 = Start.X, y3 = Start.Y;
            float x4 = Start.X + Direction.X, y4 = Start.Y + Direction.Y;

            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0)
            {
                return false;
            }

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (t >= 0 && t <= 1 && u >= 0)
            {
                intersection = new (x1 + t * (x2 - x1), y1 + t * (y2 - y1));
                return true;
            }
            return false;
        }

        public static Ray BetweenVectors(Point2D start, Point2D end) =>
            new(start, (end - start).ToFloating());
    }
}
