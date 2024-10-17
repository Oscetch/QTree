using Microsoft.Xna.Framework;

namespace QTree.MonoGame.Common.Interfaces
{
    public interface IQuadTreeObject<T>
    {
        Rectangle Bounds { get; }
        T Object { get; }
    }
}
