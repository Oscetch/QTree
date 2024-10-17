using Microsoft.Xna.Framework;
using QTree.MonoGame.Common.Exceptions;
using QTree.MonoGame.Common.Extensions;
using QTree.MonoGame.Common.Interfaces;
using QTree.MonoGame.Common.RayCasting;
using System;
using System.Collections.Generic;

namespace QTree.MonoGame.Common
{
    public sealed class QuadTree<T> : QuadTreeBase<T, QuadTree<T>>
    {
        private Rectangle _bounds;

        public QuadTree(Rectangle bounds, int splitLimit = 5, int depthLimit = 100)
            : this(bounds, 0, splitLimit, depthLimit)
        {
        }

        public QuadTree(int x, int y, int width, int height, int splitLimit = 5, int depthLimit = 100)
            : this(new Rectangle(x, y, width, height), 0, splitLimit, depthLimit)
        {
        }

        private QuadTree(Rectangle bounds, int depth, int splitLimit, int depthLimit)
            : base(depth, splitLimit, depthLimit)
        {
            _bounds = bounds;
        }

        public override void Add(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Contains(qTreeObj.Bounds))
            {
                throw new OutOfQuadException("Attempted to add object outside of the qtree bounds");
            }

            if (IsSplit)
            {
                if (!TryAddChild(qTreeObj))
                {
                    InternalObjects.Add(qTreeObj);
                }
                return;
            }

            InternalObjects.Add(qTreeObj);
            if (InternalObjects.Count >= SplitLimit)
            {
                Split();
            }
        }

        public override bool Remove(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Intersects(qTreeObj.Bounds))
            {
                return false;
            }

            if (InternalObjects.Remove(qTreeObj))
            {
                return true;
            }

            if (!IsSplit)
            {
                return false;
            }

            return TL.Remove(qTreeObj)
                || TR.Remove(qTreeObj)
                || BL.Remove(qTreeObj)
                || BR.Remove(qTreeObj);
        }

        public override IEnumerable<IQuadTreeObject<T>> FindNode(Point point)
        {
            if (!_bounds.Contains(point))
            {
                yield break;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(point))
                {
                    yield return obj;
                }
            }

            if (IsSplit)
            {
                foreach (var node in TL.FindNode(point))
                {
                    yield return node;
                }
                foreach (var node in TR.FindNode(point))
                {
                    yield return node;
                }
                foreach (var node in BL.FindNode(point))
                {
                    yield return node;
                }
                foreach (var node in BR.FindNode(point))
                {
                    yield return node;
                }
            }
        }

        public override IEnumerable<IQuadTreeObject<T>> FindNode(Rectangle rectangle)
        {
            if (!rectangle.Intersects(_bounds))
            {
                yield break;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Intersects(rectangle))
                {
                    yield return obj;
                }
            }

            if (IsSplit)
            {
                foreach (var node in TL.FindNode(rectangle))
                {
                    yield return node;
                }
                foreach (var node in TR.FindNode(rectangle))
                {
                    yield return node;
                }
                foreach (var node in BL.FindNode(rectangle))
                {
                    yield return node;
                }
                foreach (var node in BR.FindNode(rectangle))
                {
                    yield return node;
                }
            }
        }

        public override IEnumerable<T> FindObject(Point point)
        {
            if (!_bounds.Contains(point))
            {
                yield break;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(point))
                {
                    yield return obj.Object;
                }
            }

            if (IsSplit)
            {
                foreach (var obj in TL.FindObject(point))
                {
                    yield return obj;
                }
                foreach (var obj in TR.FindObject(point))
                {
                    yield return obj;
                }
                foreach (var obj in BL.FindObject(point))
                {
                    yield return obj;
                }
                foreach (var obj in BR.FindObject(point))
                {
                    yield return obj;
                }
            }
        }

        public override IEnumerable<T> FindObject(Rectangle rectangle)
        {
            if (!rectangle.Intersects(_bounds))
            {
                yield break;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Intersects(rectangle))
                {
                    yield return obj.Object;
                }
            }

            if (IsSplit)
            {
                foreach (var obj in TL.FindObject(rectangle))
                {
                    yield return obj;
                }
                foreach (var obj in TR.FindObject(rectangle))
                {
                    yield return obj;
                }
                foreach (var obj in BL.FindObject(rectangle))
                {
                    yield return obj;
                }
                foreach (var obj in BR.FindObject(rectangle))
                {
                    yield return obj;
                }
            }
        }

        public override List<Rectangle> GetQuads()
        {
            var quads = new List<Rectangle> { _bounds };
            if (!IsSplit)
            {
                return quads;
            }

            quads.AddRange(TL.GetQuads());
            quads.AddRange(TR.GetQuads());
            quads.AddRange(BL.GetQuads());
            quads.AddRange(BR.GetQuads());

            return quads;
        }

        private bool TryAdd(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Contains(qTreeObj.Bounds))
            {
                return false;
            }
            if (IsSplit)
            {
                return TryAddChild(qTreeObj) || InternalObjects.Add(qTreeObj);
            }

            InternalObjects.Add(qTreeObj);
            if (InternalObjects.Count >= SplitLimit)
            {
                Split();
            }
            return true;
        }

        private bool TryAddChild(IQuadTreeObject<T> qTreeObj)
        {
            return TL.TryAdd(qTreeObj) || TR.TryAdd(qTreeObj) || BL.TryAdd(qTreeObj) || BR.TryAdd(qTreeObj);
        }

        private void Split()
        {
            if(Depth >= DepthLimit)
            {
                return;
            }
            if (IsSplit)
            {
                throw new InvalidOperationException("Tried to split tree more than once");
            }

            _bounds.Split(out var tl, out var tr, out var bl, out var br);
            var newDepth = Depth + 1;

            TL = new QuadTree<T>(tl, newDepth, SplitLimit, DepthLimit);
            TR = new QuadTree<T>(tr, newDepth, SplitLimit, DepthLimit);
            BL = new QuadTree<T>(bl, newDepth, SplitLimit, DepthLimit);
            BR = new QuadTree<T>(br, newDepth, SplitLimit, DepthLimit);

            InternalObjects.RemoveWhere(TryAddChild);

            IsSplit = true;
        }

        protected override bool DoesRayIntersectQuad(QTreeRay ray) =>
            _bounds.Contains(ray.Start) || _bounds.IntersectsRayFast(ray);
    }
}
