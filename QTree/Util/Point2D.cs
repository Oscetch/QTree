namespace QTree.Util
{
    public struct Point2D
    {
        public static readonly Point2D Zero = new(0);
        public static readonly Point2D One = new(1);
        public static readonly Point2D Up = new(0, -1);
        public static readonly Point2D Down = new(0, 1);
        public static readonly Point2D Left = new(-1, 0);
        public static readonly Point2D Right = new(1, 0);

        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2D(int value)
        {
            X = Y = value;
        }

        public readonly PointF2D ToFloating() => new(X, Y);

        public static Point2D operator +(Point2D left, Point2D right) => new(left.X + right.X, left.Y + right.Y);
        public static Point2D operator -(Point2D left, Point2D right) => new(left.X - right.X, left.Y - right.Y);
        public static implicit operator PointF2D(Point2D p) => new(p.X, p.Y);
    }
}
