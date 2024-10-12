using QTree.Interfaces;
using QTree.Util;

namespace QTree
{
    public class QuadTreeObject<T>(Rectangle bounds, T obj) : IQuadTreeObject<T>
    {
        public Rectangle Bounds { get; } = bounds;
        public T Object { get; } = obj;
        public QuadId Id { get; } = new QuadId();

        public QuadTreeObject(int x, int y, int width, int height, T obj)
            : this(new Rectangle(x, y, width, height), obj)
        {
        }
    }
}
