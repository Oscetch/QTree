using Microsoft.Xna.Framework;

namespace QTree.Interfaces
{
    public interface IQuadTreeObject<T>
    {
        QuadId Id { get; }
        Rectangle Bounds { get; }
        T Object { get; }
    }
}
