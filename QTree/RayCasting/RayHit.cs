using QTree.Util;

namespace QTree.RayCasting
{
    public class RayHit<T>
    {
        public PointF2D Hit;
        public T Object;

        internal RayHit(PointF2D hit, T @object)
        {
            Hit = hit;
            Object = @object;
        }
    }
}
