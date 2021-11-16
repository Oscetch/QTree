using QTree.Extensions;
using QTree.Interfaces;
using QTree.Util;
using System;
using System.Collections.Generic;

namespace QTree
{
    public sealed class QuadTree<T>
    {
        private readonly int _splitLimit;
        private readonly int _depthLimit;
        private readonly int _depth;

        private List<IQuadTreeObject<T>> _internalObjects;
        private Rectangle _bounds;

        private bool _isSplit;

        private QuadTree<T> _tl;
        private QuadTree<T> _tr;
        private QuadTree<T> _bl;
        private QuadTree<T> _br;

        public QuadTree(Rectangle bounds, int splitLimit = 5, int depthLimit = 100)
            : this(bounds, 0, splitLimit, depthLimit)
        {
        }

        public QuadTree(int x, int y, int width, int height, int splitLimit = 5, int depthLimit = 100)
            : this(Rectangle.Create(x, y, width, height), 0, splitLimit, depthLimit)
        {
        }

        private QuadTree(Rectangle bounds, int depth, int splitLimit, int depthLimit)
        {
            _bounds = bounds;
            _depth = depth;
            _splitLimit = splitLimit;
            _depthLimit = depthLimit;
            _internalObjects = new List<IQuadTreeObject<T>>();
        }

        public void AddRange(params (int x, int y, int width, int height, T obj)[] objects)
        {
            foreach(var obj in objects)
            {
                Add(new QuadTreeObject<T>(obj.x, obj.y, obj.width, obj.height, obj.obj));
            }
        }

        public void AddRange(params (Rectangle bounds, T obj)[] objects)
        {
            foreach(var obj in objects)
            {
                Add(new QuadTreeObject<T>(obj.bounds, obj.obj));
            }
        }

        public void AddRange(params IQuadTreeObject<T>[] objects)
        {
            foreach(var obj in objects)
            {
                Add(obj);
            }
        }

        public void Add(int x, int y, int width, int height, T obj)
        {
            Add(new QuadTreeObject<T>(x, y, width, height, obj));
        }

        public void Add(Rectangle bounds, T obj)
        {
            Add(new QuadTreeObject<T>(bounds, obj));
        }

        public void Add(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Contains(qTreeObj.Bounds))
            {
                throw new Exception("Attempted to add object outside of the qtree bounds");
            }

            if (_isSplit)
            {
                if (!TryAddChild(qTreeObj))
                {
                    _internalObjects.Add(qTreeObj);
                }
                return;
            }

            _internalObjects.Add(qTreeObj);
            if (_internalObjects.Count >= _splitLimit)
            {
                Split();
            }
        }

        public bool Remove(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Overlaps(qTreeObj.Bounds))
            {
                return false;
            }

            for(var i = 0; i < _internalObjects.Count; i++)
            {
                if(_internalObjects[i].Id.Id == qTreeObj.Id.Id)
                {
                    _internalObjects.RemoveAt(i);
                    return true;
                }
            }

            if (!_isSplit)
            {
                return false;
            }

            return _tl.Remove(qTreeObj)
                || _tr.Remove(qTreeObj)
                || _bl.Remove(qTreeObj)
                || _br.Remove(qTreeObj);
        }

        public void Clear()
        {
            _internalObjects.Clear();
            if (!_isSplit)
            {
                return;
            }

            _tl = null;
            _tr = null;
            _bl = null;
            _br = null;

            _isSplit = false;
        }

        public List<IQuadTreeObject<T>> FindNode(int x, int y, int width, int height)
        {
            return FindNode(Rectangle.Create(x, y, width, height));
        }

        public List<IQuadTreeObject<T>> FindNode(Rectangle rectangle)
        {
            var items = new List<IQuadTreeObject<T>>();
            if (!rectangle.Overlaps(_bounds))
            {
                return items;
            }

            foreach (var obj in _internalObjects)
            {
                if (obj.Bounds.Overlaps(rectangle))
                {
                    items.Add(obj);
                }
            }

            if (_isSplit)
            {
                items.AddRange(_tl.FindNode(rectangle));
                items.AddRange(_tr.FindNode(rectangle));
                items.AddRange(_bl.FindNode(rectangle));
                items.AddRange(_br.FindNode(rectangle));
            }

            return items;
        }

        public List<T> FindObject(int x, int y, int width, int height)
        {
            return FindObject(Rectangle.Create(x, y, width, height));
        }

        public List<T> FindObject(Rectangle rectangle)
        {
            var items = new List<T>();
            if (!rectangle.Overlaps(_bounds))
            {
                return items;
            }

            foreach(var obj in _internalObjects)
            {
                if (obj.Bounds.Overlaps(rectangle))
                {
                    items.Add(obj.Object);
                }
            }

            if (_isSplit)
            {
                items.AddRange(_tl.FindObject(rectangle));
                items.AddRange(_tr.FindObject(rectangle));
                items.AddRange(_bl.FindObject(rectangle));
                items.AddRange(_br.FindObject(rectangle));
            }

            return items;
        }

        private bool TryAdd(IQuadTreeObject<T> qTreeObj)
        {
            if (!_bounds.Overlaps(qTreeObj.Bounds))
            {
                return false;
            }
            if (_isSplit)
            {
                if (!TryAddChild(qTreeObj))
                {
                    _internalObjects.Add(qTreeObj);
                }

                return true;
            }

            _internalObjects.Add(qTreeObj);
            if(_internalObjects.Count >= _splitLimit)
            {
                Split();
            }
            return true;
        }

        private bool TryAddChild(IQuadTreeObject<T> qTreeObj)
        {
            return _tl.TryAdd(qTreeObj) || _tr.TryAdd(qTreeObj) || _bl.TryAdd(qTreeObj) || _br.TryAdd(qTreeObj);
        }

        private void Split()
        {
            if (_isSplit)
            {
                throw new InvalidOperationException("Tried to split tree more than once");
            }

            var newBounds = _bounds.Split(2, 2);
            var newDepth = _depth + 1;

            _tl = new QuadTree<T>(newBounds[Point2D.Zero], newDepth, _splitLimit, _depthLimit);
            _tr = new QuadTree<T>(newBounds[new Point2D(1, 0)], newDepth, _splitLimit, _depthLimit);
            _bl = new QuadTree<T>(newBounds[new Point2D(0, 1)], newDepth, _splitLimit, _depthLimit);
            _br = new QuadTree<T>(newBounds[new Point2D(1)], newDepth, _splitLimit, _depthLimit);

            for(var i = 0; i < _internalObjects.Count; i++)
            {
                var obj = _internalObjects[i];
                if (TryAddChild(obj))
                {
                    _internalObjects.RemoveAt(i);
                    i--;
                }
            }

            _isSplit = true;
        }
    }
}
