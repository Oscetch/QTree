using QTree.Util;
using System.Collections.Generic;

namespace QTree.Interfaces
{
    public interface IQuadTree<T>
    {
        /// <summary>
        /// Adds an array of objects to the quad tree
        /// </summary>
        /// <param name="objects"></param>
        void AddRange(params (int x, int y, int width, int height, T obj)[] objects);
        /// <summary>
        /// Adds an array of objects to the quad tree
        /// </summary>
        /// <param name="objects"></param>
        void AddRange(params (Rectangle bounds, T obj)[] objects);
        /// <summary>
        /// Adds an array of objects to the quad tree
        /// </summary>
        /// <param name="objects"></param>
        void AddRange(params IQuadTreeObject<T>[] objects);
        /// <summary>
        /// Adds a single object to the quad tree
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="obj"></param>
        void Add(int x, int y, int width, int height, T obj);
        /// <summary>
        /// Adds a single object to the quad tree
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="obj"></param>
        void Add(Rectangle bounds, T obj);
        /// <summary>
        /// Adds a single object to the quad tree
        /// </summary>
        /// <param name="qTreeObj"></param>
        void Add(IQuadTreeObject<T> qTreeObj);
        /// <summary>
        /// Removes an object from the quad tree
        /// </summary>
        /// <param name="qTreeObj"></param>
        /// <returns></returns>
        bool Remove(IQuadTreeObject<T> qTreeObj);
        /// <summary>
        /// Removes all objects from the quad tree
        /// </summary>
        void Clear();
        /// <summary>
        /// Retrieve all objects which contain the x and y coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        List<IQuadTreeObject<T>> FindNode(int x, int y);
        /// <summary>
        /// Retrieve all objects which contain the point coordinate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        List<IQuadTreeObject<T>> FindNode(Point2D point);
        /// <summary>
        /// Retrieve all objects which overlap with the provided bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        List<IQuadTreeObject<T>> FindNode(int x, int y, int width, int height);
        /// <summary>
        /// Retrieve all objects which overlap with the provided bounds
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        List<IQuadTreeObject<T>> FindNode(Rectangle rectangle);
        /// <summary>
        /// Retrieve all objects which contain the provided x and y coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        List<T> FindObject(int x, int y);
        /// <summary>
        /// Retrieve all objects which contain the provided point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        List<T> FindObject(Point2D point);
        /// <summary>
        /// Retrieve all objects which overlap with the provided bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        List<T> FindObject(int x, int y, int width, int height);
        /// <summary>
        /// Retrieve all objects which overlap with the provided bounds
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        List<T> FindObject(Rectangle rectangle);
        /// <summary>
        /// Retrieve all quads/rectangles/nodes in the tree
        /// </summary>
        /// <returns></returns>
        List<Rectangle> GetQuads();
    }
}
