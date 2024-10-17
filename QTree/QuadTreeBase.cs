using QTree.Interfaces;
using QTree.RayCasting;
using QTree.Util;
using System;
using System.Collections.Generic;

namespace QTree
{
    public abstract class QuadTreeBase<T, TTree>(int depth, int splitLimit, int depthLimit) : IQuadTree<T> where TTree : QuadTreeBase<T, TTree>
    {
        protected int SplitLimit { get; } = splitLimit;
        protected int DepthLimit { get; } = depthLimit;
        protected int Depth { get; } = depth;

        protected HashSet<IQuadTreeObject<T>> InternalObjects { get; } = [];

        protected bool IsSplit { get; set; }

        protected TTree TL { get; set; }
        protected TTree TR { get; set; }
        protected TTree BL { get; set; }
        protected TTree BR { get; set; }

        public void AddRange(params (int x, int y, int width, int height, T obj)[] objects)
        {
            foreach (var obj in objects)
            {
                Add(new QuadTreeObject<T>(obj.x, obj.y, obj.width, obj.height, obj.obj));
            }
        }

        public void AddRange(params (Rectangle bounds, T obj)[] objects)
        {
            foreach (var obj in objects)
            {
                Add(new QuadTreeObject<T>(obj.bounds, obj.obj));
            }
        }

        public void AddRange(params IQuadTreeObject<T>[] objects)
        {
            foreach (var obj in objects)
            {
                Add(obj);
            }
        }

        public virtual void Add(int x, int y, int width, int height, T obj)
        {
            Add(new QuadTreeObject<T>(x, y, width, height, obj));
        }

        public virtual void Add(Rectangle bounds, T obj)
        {
            Add(new QuadTreeObject<T>(bounds, obj));
        }

        public abstract void Add(IQuadTreeObject<T> qTreeObj);

        public abstract bool Remove(IQuadTreeObject<T> qTreeObj);

        public virtual void Clear()
        {
            InternalObjects.Clear();
            if (!IsSplit)
            {
                return;
            }

            TL = null;
            TR = null;
            BL = null;
            BR = null;

            IsSplit = false;
        }

        public IEnumerable<IQuadTreeObject<T>> FindNode(int x, int y, int width, int height)
        {
            return FindNode(new Rectangle(x, y, width, height));
        }

        public abstract IEnumerable<IQuadTreeObject<T>> FindNode(Rectangle rectangle);

        public IEnumerable<T> FindObject(int x, int y, int width, int height)
        {
            return FindObject(new Rectangle(x, y, width, height));
        }

        public abstract IEnumerable<T> FindObject(Rectangle rectangle);

        public abstract IEnumerable<Rectangle> GetQuads();

        public abstract IEnumerable<IQuadTreeObject<T>> FindNode(int x, int y);

        public IEnumerable<IQuadTreeObject<T>> FindNode(Point2D point)
        {
            return FindNode(point.X, point.Y);
        }

        public abstract IEnumerable<T> FindObject(int x, int y);

        public IEnumerable<T> FindObject(Point2D point)
        {
            return FindObject(point.X, point.Y);
        }

        public void RayCast(Ray ray, Func<IQuadTreeObject<T>, PointF2D, RaySearchOption> rayCastPredicate) => RayCastInternal(ray, rayCastPredicate);

        protected abstract bool DoesRayIntersectQuad(Ray ray);

        protected bool RayCastInternal(Ray ray, Func<IQuadTreeObject<T>, PointF2D, RaySearchOption> rayCastPredicate)
        {
            if (!DoesRayIntersectQuad(ray)) return true;
            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.IntersectsRay(ray, out var intersection) && rayCastPredicate(obj, intersection) == RaySearchOption.STOP)
                {
                    return false;
                }
            }

            if (!IsSplit) return true;

            return TL.RayCastInternal(ray, rayCastPredicate)
                && TR.RayCastInternal(ray, rayCastPredicate)
                && BL.RayCastInternal(ray, rayCastPredicate)
                && BR.RayCastInternal(ray, rayCastPredicate);
        }

        public int GetDepth()
        {
            return GetDepth(-1);
        }

        private int GetDepth(int current)
        {
            var currentWithThis = current + 1;
            if (!IsSplit)
            {
                return currentWithThis;
            }
            var fromHere = 0;
            fromHere = Math.Max(fromHere, TL.GetDepth(currentWithThis));
            fromHere = Math.Max(fromHere, TR.GetDepth(currentWithThis));
            fromHere = Math.Max(fromHere, BL.GetDepth(currentWithThis));
            return Math.Max(fromHere, BR.GetDepth(currentWithThis));
        }
    }
}
