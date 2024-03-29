﻿using System;
using System.Collections.Generic;

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
        public int Top => Y;
        public int Left => X;

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

        public bool Contains(int x, int y)
        {
            return Left <= x
                && Right >= x
                && Bottom >= y
                && Top <= y;
        }

        public void Split(out Rectangle topLeft,
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
    }
}
