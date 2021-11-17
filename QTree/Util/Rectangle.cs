using System;
using System.Collections.Generic;

namespace QTree.Util
{
    public struct Rectangle
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;

        public static Rectangle Create(int x, int y, int width, int height)
            => new(y, y + height, x, x + width);

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            Rectangle rectangle;
            rectangle.Top = Math.Min(a.Top, b.Top);
            rectangle.Bottom = Math.Max(a.Bottom, b.Bottom);
            rectangle.Left = Math.Min(a.Left, b.Left);
            rectangle.Right = Math.Max(a.Right, b.Right);
            return rectangle;
        }

        public Rectangle(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = Left;
            y = Top;
            width = Right - Left;
            height = Bottom - Top;
        }

        public Point2D GetCenter()
        {
            Point2D center;
            center.X = Left + ((Right - Left) / 2);
            center.Y = Top + ((Bottom - Top) / 2);
            return center;
        }

        public Rectangle Union(Rectangle other)
        {
            return Union(this, other);
        }

        public bool Overlaps(Rectangle other)
        {
            return Left < other.Right
                && Right > other.Left
                && Bottom > other.Top
                && Top < other.Bottom;
        }

        public bool Contains(Rectangle other)
        {
            return Left <= other.Left
                && Right >= other.Right
                && Bottom >= other.Bottom
                && Top <= other.Top;
        }

        public bool Contains(Point2D point2D)
        {
            return Left <= point2D.X
                && Right >= point2D.X
                && Bottom >= point2D.Y
                && Top <= point2D.Y;
        }

        public Dictionary<Point2D, Rectangle> Split(int rows, int columns)
        {
            Deconstruct(out var x, out var y, out var width, out var heigth);

            var cellHeight = heigth / rows;
            var cellWidth = width / columns;

            var result = new Dictionary<Point2D, Rectangle>();

            for (var cx = 0; cx < columns; cx++)
            {
                for (var cy = 0; cy < rows; cy++)
                {
                    result[new Point2D(cx, cy)] = Create(x + (cx * cellWidth),
                        y + (cy * cellHeight),
                        cellWidth,
                        cellHeight);
                }
            }

            return result;
        }
    }
}
