using Microsoft.Xna.Framework;
using QTree.MonoGame.Common.Extensions;
using QTree.MonoGame.Common.Interfaces;
using QTree.MonoGame.Common.RayCasting;
using System;
using System.Collections.Generic;

namespace QTree.MonoGame.Common
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

        public IEnumerable<IQuadTreeObject<T>> FindNode(int x, int y)
        {
            return FindNode(new Point(x, y));
        }

        public abstract IEnumerable<IQuadTreeObject<T>> FindNode(Point point);

        public IEnumerable<T> FindObject(int x, int y)
        {
            return FindObject(new Point(x, y));
        }

        public abstract IEnumerable<T> FindObject(Point point);

        public IEnumerable<RayHit<IQuadTreeObject<T>>> RayCast(QTreeRay ray)
        {
            if (!DoesRayIntersectQuad(ray)) yield break;

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.IntersectsRay(ray, out var intersection))
                {
                    yield return new RayHit<IQuadTreeObject<T>>(intersection, obj);
                }
            }

            if (!IsSplit) yield break;

            foreach (var hit in TL.RayCast(ray))
            {
                yield return hit;
            }
            foreach (var hit in TR.RayCast(ray))
            {
                yield return hit;
            }
            foreach (var hit in BL.RayCast(ray))
            {
                yield return hit;
            }
            foreach (var hit in BR.RayCast(ray))
            {
                yield return hit;
            }
        }

        protected abstract bool DoesRayIntersectQuad(QTreeRay ray);

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
