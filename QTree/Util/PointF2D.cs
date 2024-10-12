using System;

namespace QTree.Util
{
    public struct PointF2D(float x, float y)
    {
        public float X = x;
        public float Y = y;

        public void Normalize()
        {
            float num = 1f / MathF.Sqrt(X * X + Y * Y);
            X *= num;
            Y *= num;
        }

        public static float Distance(PointF2D p1, PointF2D p2)
        {
            float num = p1.X - p2.X;
            float num2 = p1.Y - p2.Y;
            return MathF.Sqrt(num * num + num2 * num2);
        }
    }
}
