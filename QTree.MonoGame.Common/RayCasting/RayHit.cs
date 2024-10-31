using Microsoft.Xna.Framework;

namespace QTree.MonoGame.Common.RayCasting
{
    public class RayHit<T>
    {
        public Vector2 Hit;
        public T Object;

        internal RayHit(Vector2 hit, T @object)
        {
            Hit = hit;
            Object = @object;
        }
    }
}
