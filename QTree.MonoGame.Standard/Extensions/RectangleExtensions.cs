using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree.MonoGame.Standard.Extensions
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
    }
}
