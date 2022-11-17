using Microsoft.Xna.Framework;
using QTree.MonoGame.Common.Exceptions;
using QTree.MonoGame.Common.Extensions;
using QTree.MonoGame.Common.Interfaces;
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

        public override List<IQuadTreeObject<T>> FindNode(Point point)
        {
            var items = new List<IQuadTreeObject<T>>();
            if (!_bounds.Contains(point))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(point))
                {
                    items.Add(obj);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindNode(point));
                items.AddRange(TR.FindNode(point));
                items.AddRange(BL.FindNode(point));
                items.AddRange(BR.FindNode(point));
            }

            return items;
        }

        public override List<IQuadTreeObject<T>> FindNode(Rectangle rectangle)
        {
            var items = new List<IQuadTreeObject<T>>();
            if (!rectangle.Intersects(_bounds))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Intersects(rectangle))
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

        public override List<T> FindObject(Point point)
        {
            var items = new List<T>();
            if (!_bounds.Contains(point))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Contains(point))
                {
                    items.Add(obj.Object);
                }
            }

            if (IsSplit)
            {
                items.AddRange(TL.FindObject(point));
                items.AddRange(TR.FindObject(point));
                items.AddRange(BL.FindObject(point));
                items.AddRange(BR.FindObject(point));
            }

            return items;
        }

        public override List<T> FindObject(Rectangle rectangle)
        {
            var items = new List<T>();
            if (!rectangle.Intersects(_bounds))
            {
                return items;
            }

            foreach (var obj in InternalObjects)
            {
                if (obj.Bounds.Intersects(rectangle))
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
                if (!TryAddChild(qTreeObj))
                {
                    InternalObjects.Add(qTreeObj);
                }

                return true;
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

            for (var i = 0; i < InternalObjects.Count; i++)
            {
                var obj = InternalObjects[i];
                if (TryAddChild(obj))
                {
                    InternalObjects.RemoveAt(i);
                    i--;
                }
            }

            IsSplit = true;
        }
    }
}
