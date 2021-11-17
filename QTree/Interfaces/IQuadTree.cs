using QTree.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree.Interfaces
{
    public interface IQuadTree<T>
    {
        void AddRange(params (int x, int y, int width, int height, T obj)[] objects);
        void AddRange(params (Rectangle bounds, T obj)[] objects);
        void AddRange(params IQuadTreeObject<T>[] objects);
        void Add(int x, int y, int width, int height, T obj);
        void Add(Rectangle bounds, T obj);
        void Add(IQuadTreeObject<T> qTreeObj);
        bool Remove(IQuadTreeObject<T> qTreeObj);
        void Clear();
        List<IQuadTreeObject<T>> FindNode(int x, int y, int width, int height);
        List<IQuadTreeObject<T>> FindNode(Rectangle rectangle);
        List<T> FindObject(int x, int y, int width, int height);
        List<T> FindObject(Rectangle rectangle);
        List<Rectangle> GetQuads();
    }
}
