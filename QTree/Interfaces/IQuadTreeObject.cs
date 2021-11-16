using QTree.Util;

namespace QTree.Interfaces
{
    public interface IQuadTreeObject<T>
    {
        QuadId Id { get; }
        Rectangle Bounds { get; }
        T Object { get; }
    }
}
