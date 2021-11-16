using System.Collections.Generic;
using System.Drawing;

namespace QTree.Extensions
{
    public static class RectangleExtensions
    {
        public static Dictionary<Point, Rectangle> Split(this Rectangle rectangle, int rows, int columns)
        {
            var cellHeight = rectangle.Height / rows;
            var cellWidth = rectangle.Width / columns;

            var result = new Dictionary<Point, Rectangle>();

            for(var x = 0; x < columns; x++)
            {
                for(var y = 0; y < rows; y++)
                {
                    result[new Point(x, y)] = new Rectangle(rectangle.X + (x * cellWidth), 
                        rectangle.Y + (y * cellHeight),
                        cellWidth, 
                        cellHeight);
                }
            }

            return result;
        }

        public static bool Overlaps(this Rectangle rectangleA, Rectangle rectangleB)
        {
            return rectangleA.Left < rectangleB.Right
                && rectangleA.Right > rectangleB.Left
                && rectangleA.Bottom > rectangleB.Top
                && rectangleA.Top < rectangleB.Bottom;
        }
    }
}
