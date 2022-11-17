﻿using QTree.Interfaces;
using QTree.Util;
using System;
using System.Collections.Generic;

namespace QTree
{
    public sealed class DynamicQuadTree<T> : QuadTreeBase<T, DynamicQuadTree<T>>
    {
        private Rectangle? _bounds;
        private Point2D _quadStartCenter;

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
                _bounds = _bounds.Value.Union(qTreeObj.Bounds);
            }

            if (IsSplit)
            {
                if(TryGetChildToAddTo(qTreeObj.Bounds, out var child))
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

        public override List<IQuadTreeObject<T>> FindNode(Rectangle rectangle)
        {
            var items = new List<IQuadTreeObject<T>>();
            if (!_bounds.HasValue || !rectangle.Overlaps(_bounds.Value))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Overlaps(rectangle))
                {
                    items.Add(obj);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindNode(rectangle));
                items.AddRange(TR.FindNode(rectangle));
                items.AddRange(BL.FindNode(rectangle));
                items.AddRange(BR.FindNode(rectangle));
            }

            return items;
        }

        public override List<IQuadTreeObject<T>> FindNode(int x, int y)
        {
            var items = new List<IQuadTreeObject<T>>();
            if (!_bounds.HasValue || !_bounds.Value.Contains(x, y))
            {
            return items;
        }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(x, y))
                {
                    items.Add(obj);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindNode(x, y));
                items.AddRange(TR.FindNode(x, y));
                items.AddRange(BL.FindNode(x, y));
                items.AddRange(BR.FindNode(x, y));
            }

            return items;
        }

        public override List<T> FindObject(Rectangle rectangle)
        {
            var items = new List<T>();
            if (!_bounds.HasValue || !rectangle.Overlaps(_bounds.Value))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Overlaps(rectangle))
                {
                    items.Add(obj.Object);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindObject(rectangle));
                items.AddRange(TR.FindObject(rectangle));
                items.AddRange(BL.FindObject(rectangle));
                items.AddRange(BR.FindObject(rectangle));
            }

            return items;
        }

        public override List<T> FindObject(int x, int y)
        {
            var items = new List<T>();
            if (!_bounds.HasValue || !_bounds.Value.Contains(x, y))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(x, y))
                {
                    items.Add(obj.Object);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindObject(x, y));
                items.AddRange(TR.FindObject(x, y));
                items.AddRange(BL.FindObject(x, y));
                items.AddRange(BR.FindObject(x, y));
            }

            return items;
        }

        public override bool Remove(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.HasValue || !_bounds.Value.Overlaps(qTreeObj.Bounds))
            {
                return false;
            }

            for (var i = 0; i < InternalObjects.Count; i++)
            {
                if (InternalObjects[i].Id.Id == qTreeObj.Id.Id)
                {
                    InternalObjects.RemoveAt(i);
                    return true;
                }
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

        public override List<Rectangle> GetQuads()
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
            if(newBounds.Right < _quadStartCenter.X && newBounds.Bottom < _quadStartCenter.Y)
            {
                child = TL;
                return true;
            }
            if(newBounds.Left > _quadStartCenter.X && newBounds.Bottom < _quadStartCenter.Y)
            {
                child = TR;
                return true;
            }
            if(newBounds.Right < _quadStartCenter.X && newBounds.Top > _quadStartCenter.Y)
            {
                child = BL;
                return true;
            }
            if(newBounds.Left > _quadStartCenter.X && newBounds.Top > _quadStartCenter.Y)
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
            _quadStartCenter = _bounds.Value.GetCenter();

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

            for (var i = 0; i < InternalObjects.Count; i++)
            {
                var obj = InternalObjects[i];
                foreach(var (tree, bounds) in initialBoundsList)
                {
                    if (!bounds.Contains(obj.Bounds))
                    {
                        continue;
                    }

                    tree.Add(obj);
                    InternalObjects.RemoveAt(i);
                    i--;
                    break;
                }
            }

            IsSplit = true;
        }
    }
}
