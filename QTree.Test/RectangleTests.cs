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
            var sut = new Rectangle(50, 50, 10, 10);
            var testData = new Rectangle(x, y, width, height);

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
            var sut = new Rectangle(50, 50, 10, 10);
            var testData = new Rectangle(x, y, width, height);

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
            var sut = new Rectangle(50, 50, 10, 10);
            var testData = new Point2D(x, y);

            Assert.AreEqual(isContaining, sut.Contains(testData));
        }

        [DataTestMethod]
        [DataRow(54, 54, 2, 2, 50, 50, 10, 10)]
        [DataRow(45, 45, 4, 4, 45, 45, 15, 15)]
        public void TestUnion(int x, int y, int width, int height,
            int ex, int ey, int ew, int eh)
        {
            var sut = new Rectangle(50, 50, 10, 10);
            var testData = new Rectangle(x, y, width, height);
            var expectedRectangle = new Rectangle(ex, ey, ew, eh);

            var resultRectangle = sut.Union(testData);

            Assert.AreEqual(ex, resultRectangle.X);
            Assert.AreEqual(ey, resultRectangle.Y);
            Assert.AreEqual(ew, resultRectangle.Width);
            Assert.AreEqual(eh, resultRectangle.Height);
            Assert.AreEqual(expectedRectangle, resultRectangle);
        }

        [TestMethod]
        public void TestSplit()
        {
            var sut = new Rectangle(0, 0, 10, 10);

            var expectedTopLeft = new Rectangle(0, 0, 5, 5);
            var expectedTopRight = new Rectangle(5, 0, 5, 5);
            var expectedBottomleft = new Rectangle(0, 5, 5, 5);
            var expectedBottomRight = new Rectangle(5, 5, 5, 5);

            sut.Split(out var resultTopLeft,
                out var resultTopRight,
                out var resultBottomLeft,
                out var resultBottomRight);

            Assert.IsTrue(RecEquals(expectedTopLeft, resultTopLeft));
            Assert.IsTrue(RecEquals(expectedTopRight, resultTopRight));
            Assert.IsTrue(RecEquals(expectedBottomleft, resultBottomLeft));
            Assert.IsTrue(RecEquals(expectedBottomRight, resultBottomRight));
        }

        private bool RecEquals(Rectangle r1, Rectangle r2)
        {
            return r1.X == r2.X
                && r1.Y == r2.Y
                && r1.Width == r2.Width
                && r1.Height == r2.Height
                && r1.Right == r2.Right
                && r1.Bottom == r2.Bottom;
        }
    }
}
