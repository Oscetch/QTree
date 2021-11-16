using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree.Util
{
    public struct Point2D
    {
        public static readonly Point2D Zero = new Point2D(0);
        public static readonly Point2D One = new Point2D(1);
        public static readonly Point2D Up = new Point2D(0, -1);
        public static readonly Point2D Down = new Point2D(0, 1);
        public static readonly Point2D Left = new Point2D(-1, 0);
        public static readonly Point2D Right = new Point2D(1, 0);

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
    }
}
