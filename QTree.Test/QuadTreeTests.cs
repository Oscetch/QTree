using QTree.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace QTree.Test
{
    [TestClass]
    public class QuadTreeTests
    {
        private const int QUAD_SIZE = 50_000;

        private readonly Random _random = new();

        [TestMethod]
        public void FindTest()
        {
            // arrange
            var sut = new QuadTree<string>(Rectangle.Create(0, 0, QUAD_SIZE, QUAD_SIZE));
            for(var i = 0; i < 1_000_000; i++)
            {
                sut.Add(_random.Next(QUAD_SIZE), _random.Next(QUAD_SIZE), 1, 1, string.Empty);
            }
            var position = QUAD_SIZE - 5000;
            var expected = Rectangle.Create(position, position, 1, 1);
            sut.Add(expected, "CORRECT");
            var searchArea = Rectangle.Create(position - 200, position - 200, 400, 400);

            // act
            var timer = Stopwatch.StartNew();
            var result = sut.FindObject(searchArea);
            var time = timer.ElapsedMilliseconds;
            timer.Stop();

            // assert
            Console.WriteLine($"Time: {time}");
            Assert.IsTrue(result.Any(x => x == "CORRECT"));
        }

        [TestMethod]
        public void RemoveTest()
        {
            // arrange
            var sut = new QuadTree<string>(Rectangle.Create(0, 0, QUAD_SIZE, QUAD_SIZE));
            for (var i = 0; i < 1_000_000; i++)
            {
                sut.Add(Rectangle.Create(_random.Next(QUAD_SIZE), _random.Next(QUAD_SIZE), 1, 1), string.Empty);
            }
            var position = QUAD_SIZE - 5000;
            var expected = Rectangle.Create(position, position, 1, 1);
            var toRemove = new QuadTreeObject<string>(expected, "INCORRECT");
            sut.Add(toRemove);
            var searchArea = Rectangle.Create(position - 2500, position - 2500, 5000, 5000);

            // act
            var timer = Stopwatch.StartNew();
            var wasRemoved = sut.Remove(toRemove);
            var time = timer.ElapsedMilliseconds;
            timer.Stop();
            var result = sut.FindObject(searchArea).ToList();

            // assert
            Console.WriteLine($"Time: {time}");
            Assert.IsTrue(wasRemoved);
            Assert.IsTrue(result.Count == 0 || result.All(x => x == string.Empty));
        }
    }
}
