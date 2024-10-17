using Microsoft.Xna.Framework;
using QTree.MonoGame.Common.Extensions;
using QTree.MonoGame.Common.Interfaces;
using QTree.MonoGame.Common.RayCasting;
using System;
using System.Collections.Generic;

namespace QTree.MonoGame.Common
{
    public sealed class DynamicQuadTree<T> : QuadTreeBase<T, DynamicQuadTree<T>>
    {
        private Rectangle? _bounds;
        private Point _quadStartCenter;

        public DynamicQuadTree(int splitLimit = 5, int depthLimit = 100)
            : base(0, splitLimit, depthLimit)
        {
        }

        private DynamicQuadTree(int depth, int splitLimit, int depthLimit)
            : base(depth, splitLimit, depthLimit)
        {
        }

        public override void Add(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.HasValue)
            {
                _bounds = qTreeObj.Bounds;
            }
            else if (!_bounds.Value.Contains(qTreeObj.Bounds))
            {
                _bounds = Rectangle.Union(_bounds.Value, qTreeObj.Bounds);
            }

            if (IsSplit)
            {
                if (TryGetChildToAddTo(qTreeObj.Bounds, out var child))
                {
                    child.Add(qTreeObj);
                }
                else
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

        public override IEnumerable<IQuadTreeObject<T>> FindNode(Point point)
        {
            if (!_bounds.HasValue || !_bounds.Value.Contains(point))
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
            if (!_bounds.HasValue || !rectangle.Intersects(_bounds.Value))
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
            if (!_bounds.HasValue || !_bounds.Value.Contains(point))
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
            if (!_bounds.HasValue || !rectangle.Intersects(_bounds.Value))
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

        public override bool Remove(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.HasValue || !_bounds.Value.Intersects(qTreeObj.Bounds))
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

        public override IEnumerable<Rectangle> GetQuads()
        {
            var quads = new List<Rectangle>();
            if (!_bounds.HasValue)
            {
                return quads;
            }
            quads.Add(_bounds.Value);

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

        public override void Clear()
        {
            base.Clear();
            _bounds = null;
        }

        private bool TryGetChildToAddTo(Rectangle newBounds, out IQuadTree<T> child)
        {
            if (newBounds.Right < _quadStartCenter.X && newBounds.Bottom < _quadStartCenter.Y)
            {
                child = TL;
                return true;
            }
            if (newBounds.Left > _quadStartCenter.X && newBounds.Bottom < _quadStartCenter.Y)
            {
                child = TR;
                return true;
            }
            if (newBounds.Right < _quadStartCenter.X && newBounds.Top > _quadStartCenter.Y)
            {
                child = BL;
                return true;
            }
            if (newBounds.Left > _quadStartCenter.X && newBounds.Top > _quadStartCenter.Y)
            {
                child = BR;
                return true;
            }

            child = null;
            return false;
        }

        private void Split()
        {
            if (Depth >= DepthLimit)
            {
                return;
            }
            if (IsSplit)
            {
                throw new InvalidOperationException("Tried to split tree more than once");
            }
            _quadStartCenter = _bounds.Value.Center;

            var newDepth = Depth + 1;

            TL = new DynamicQuadTree<T>(newDepth, SplitLimit, DepthLimit);
            TR = new DynamicQuadTree<T>(newDepth, SplitLimit, DepthLimit);
            BL = new DynamicQuadTree<T>(newDepth, SplitLimit, DepthLimit);
            BR = new DynamicQuadTree<T>(newDepth, SplitLimit, DepthLimit);

            _bounds.Value.Split(out var tl, out var tr, out var bl, out var br);
            var initialBoundsList = new (IQuadTree<T> tree, Rectangle bounds)[]
            {
                (TL, tl),
                (TR, tr),
                (BL, bl),
                (BR, br)
            };


            InternalObjects.RemoveWhere(x => 
            {
                foreach (var (tree, bounds) in initialBoundsList)
                {
                    if (!bounds.Contains(x.Bounds))
                    {
                        continue;
                    }

                    tree.Add(x);
                    return true;
                }
                return false;
            });

            IsSplit = true;
        }

        protected override bool DoesRayIntersectQuad(QTreeRay ray) =>
            _bounds.HasValue && (_bounds.Value.Contains(ray.Start) || _bounds.Value.IntersectsRayFast(ray));
    }
}
