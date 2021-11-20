using Microsoft.Xna.Framework;
using QTree.Interfaces;

namespace QTree
{
    public class QuadTreeObject<T> : IQuadTreeObject<T>
    {
        public Rectangle Bounds { get; }
        public T Object { get; }
        public QuadId Id { get; }

        public QuadTreeObject(Rectangle bounds, T obj)
        {
            Id = new QuadId();
            Bounds = bounds;
            Object = obj;
        }

        public QuadTreeObject(int x, int y, int width, int height, T obj)
            : this(new Rectangle(x, y, width, height), obj)
        {
        }
    }
}
