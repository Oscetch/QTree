using Microsoft.Xna.Framework;
using QTree.MonoGame.Common.RayCasting;

namespace QTree.MonoGame.Common.Extensions
{
    public static class RectangleExtensions
    {
        public static void Split(this Rectangle rectangle,
            out Rectangle topLeft,
            out Rectangle topRight,
            out Rectangle bottomLeft,
            out Rectangle bottomRight)
        {
            var halfWidth = rectangle.Width / 2;
            var halfHeight = rectangle.Height / 2;
            var rightX = rectangle.X + halfWidth;
            var bottomY = rectangle.Y + halfHeight;

            topLeft = new Rectangle(rectangle.X, rectangle.Y, halfWidth, halfHeight);
            topRight = new Rectangle(rightX, rectangle.Y, halfWidth, halfHeight);
            bottomLeft = new Rectangle(rectangle.X, bottomY, halfWidth, halfHeight);
            bottomRight = new Rectangle(rightX, bottomY, halfWidth, halfHeight);
        }

        private static Vector2[] GetEdges(this Rectangle rectangle) =>
        [
            new Vector2(rectangle.Left, rectangle.Top),
            new Vector2(rectangle.Right, rectangle.Top),
            new Vector2(rectangle.Left, rectangle.Bottom),
            new Vector2(rectangle.Right, rectangle.Bottom),
        ];

        public static bool IntersectsRay(this Rectangle rectangle, QTreeRay ray, out Vector2 intersection)
        {
            intersection = Vector2.Zero;
            var foundIntersection = false;
            var minDistance = float.MaxValue;
            var edges = rectangle.GetEdges();
            for (var i = 0; i < edges.Length; i++)
            {
                var p1 = edges[i];
                var p2 = edges[(i + 1) % edges.Length];

                if (ray.IntersectsLine(p1, p2, out var newIntersection))
                {
                    foundIntersection = true;
                    var distance = Vector2.Distance(ray.Start, newIntersection);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        intersection = newIntersection;
                    }
                }
            }
            return foundIntersection;
        }

        public static bool IntersectsRayFast(this Rectangle rectangle, QTreeRay ray)
        {
            var edges = rectangle.GetEdges();
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
