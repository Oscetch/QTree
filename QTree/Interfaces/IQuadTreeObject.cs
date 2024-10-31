using QTree.Util;

namespace QTree.Interfaces
{
    public interface IQuadTreeObject<T>
    {
        Rectangle Bounds { get; }
        T Object { get; }
    }
}
