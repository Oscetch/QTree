using Microsoft.VisualStudio.TestTools.UnitTesting;
using QTree.Util;

namespace QTree.Test
{
    [TestClass]
    public class RectangleTests
    {
        [DataTestMethod]
        [DataRow(48, 48, 4, 4, true)] // slight overlap top left
        [DataRow(52, 52, 4, 4, true)] // complete overlap
        [DataRow(45, 45, 4, 4, false)] // left side
        [DataRow(65, 45, 4, 4, false)] // right side
        [DataRow(55, 65, 4, 4, false)] // under
        [DataRow(55, 45, 4, 4, false)] // over
        [DataRow(45, 45, 20, 20, true)] // bigger
        public void TestOverlap(int x, int y, int width, int height, bool isOverlapping)
        {
            var sut = Rectangle.Create(50, 50, 10, 10);
            var testData = Rectangle.Create(x, y, width, height);

            Assert.AreEqual(isOverlapping, sut.Overlaps(testData));
        }

        [DataTestMethod]
        [DataRow(54, 54, 2, 2, true)] // center
        [DataRow(45, 45, 4, 4, false)] // left side
        [DataRow(65, 45, 4, 4, false)] // right side
        [DataRow(55, 65, 4, 4, false)] // under
        [DataRow(55, 45, 4, 4, false)] // over
        [DataRow(45, 45, 20, 20, false)] // bigger
        public void TestRectangleContains(int x, int y, int width, int height, bool isContaining)
        {
            var sut = Rectangle.Create(50, 50, 10, 10);
            var testData = Rectangle.Create(x, y, width, height);

            Assert.AreEqual(isContaining, sut.Contains(testData));
        }

        [DataTestMethod]
        [DataRow(55, 55, true)] // center
        [DataRow(51, 50, true)] // corner
        [DataRow(45, 45, false)] // left side
        [DataRow(65, 45, false)] // right side
        [DataRow(55, 65, false)] // under
        [DataRow(55, 45, false)] // over
        public void TestPoint2DContains(int x, int y, bool isContaining)
        {
            var sut = Rectangle.Create(50, 50, 10, 10);
            var testData = new Point2D(x, y);

            Assert.AreEqual(isContaining, sut.Contains(testData));
        }

        [DataTestMethod]
        [DataRow(54, 54, 2, 2, 50, 50, 10, 10)]
        [DataRow(45, 45, 4, 4, 45, 45, 15, 15)]
        public void TestUnion(int x, int y, int width, int height,
            int ex, int ey, int ew, int eh)
        {
            var sut = Rectangle.Create(50, 50, 10, 10);
            var testData = Rectangle.Create(x, y, width, height);
            var expectedRectangle = Rectangle.Create(ex, ey, ew, eh);

            var resultRectangle = sut.Union(testData);
            resultRectangle.Deconstruct(out var ax, out var ay, out var aw, out var ah);

            Assert.AreEqual(ex, ax);
            Assert.AreEqual(ey, ay);
            Assert.AreEqual(ew, aw);
            Assert.AreEqual(eh, ah);
            Assert.AreEqual(expectedRectangle, resultRectangle);
        }

        [TestMethod]
        public void TestSplit()
        {
            var sut = Rectangle.Create(0, 0, 10, 10);
            for(var x = 2; x < 10; x++)
            {
                for(var y = 2; y < 10; y++)
                {
                    var split = sut.Split(y, x);
                    Assert.AreEqual(x * y, split.Count);
                }
            }
        }
    }
}
