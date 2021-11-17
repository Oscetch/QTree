using Microsoft.VisualStudio.TestTools.UnitTesting;
using QTree.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree.Test
{
    [TestClass]
    public class DynamicQuadTreeTests
    {
        private const int QUAD_SIZE = 50_000;

        private readonly Random _random = new();

        [TestMethod]
        public void FindTest()
        {
            // arrange
            var sut = new DynamicQuadTree<string>();
            for (var i = 0; i < 1_000_000; i++)
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
        public void OnlyReturnUniqueResultsTest()
        {
            var sut = new DynamicQuadTree<string>();
            for (var i = 0; i < 1_000_000; i++)
            {
                sut.Add(_random.Next(QUAD_SIZE), _random.Next(QUAD_SIZE), 1, 1, string.Empty);
            }

            var position = QUAD_SIZE - 5000;
            var searchArea = Rectangle.Create(position - 2000, position - 2000, 4000, 4000);
            var results = sut.FindNode(searchArea);

            Assert.IsTrue(results.Count == results.GroupBy(x => x.Id).Count());
        }

        [TestMethod]
        public void RemoveTest()
        {
            // arrange
            var sut = new DynamicQuadTree<string>();
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

        [TestMethod]
        public void LiterallyCornerCaseTest()
        {
            const string CORRECT = "CORRECT";

            var test = new DynamicQuadTree<string>();
            test.Add(490, 490, 20, 20, CORRECT);

            for (var i = 0; i < 10; i++)
            {
                test.Add(40 + (i * 20), 20, 40, 40, string.Empty);
            }

            var upperLeft = test.FindNode(495, 495, 1, 1);
            var upperRight = test.FindNode(505, 495, 1, 1);
            var bottomLeft = test.FindNode(495, 505, 1, 1);
            var bottomRight = test.FindNode(505, 505, 1, 1);

            Assert.IsTrue(upperLeft.FirstOrDefault(x => x.Object == CORRECT) != null);
            Assert.IsTrue(upperRight.FirstOrDefault(x => x.Object == CORRECT) != null);
            Assert.IsTrue(bottomLeft.FirstOrDefault(x => x.Object == CORRECT) != null);
            Assert.IsTrue(bottomRight.FirstOrDefault(x => x.Object == CORRECT) != null);
        }
    }
}
