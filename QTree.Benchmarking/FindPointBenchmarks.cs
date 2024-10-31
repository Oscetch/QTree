using BenchmarkDotNet.Attributes;
using QTree.Interfaces;
using QTree.Util;

namespace QTree.Benchmarking
{
    public class FindPointBenchmarks
    {
        [Params(10_000)]
        public int ItemsInTree;

        [Params(5, 10, 100)]
        public int Depth;

        [Params(5, 50, 100)]
        public int SplitLimit;

        private IQuadTree<string> _qTree;
        private Point2D _testPoint;

        [GlobalSetup(Target = nameof(FindPoint))]
        public void Setup()
        {
            var widthAndHeight = (int)Math.Sqrt(ItemsInTree);
            _qTree = new QuadTree<string>(new Rectangle(0, 0, widthAndHeight, widthAndHeight), SplitLimit, Depth);
            for (int x = 0; x < widthAndHeight; x++)
            {
                for (int y = 0; y < widthAndHeight; y++)
                {
                    _qTree.Add(new Rectangle(x, y, 1, 1), $"{x}:{y}");
                }
            }
            _testPoint = new Point2D(widthAndHeight / 2 + 1, widthAndHeight / 2 + 1);
        }

        [GlobalSetup(Target = nameof(DynamicTreeFindPoint))]
        public void DynamicTreeSetup()
        {
            var widthAndHeight = (int)Math.Sqrt(ItemsInTree);
            _qTree = new DynamicQuadTree<string>(SplitLimit, Depth);
            for (int x = 0; x < widthAndHeight; x++)
            {
                for (int y = 0; y < widthAndHeight; y++)
                {
                    _qTree.Add(new Rectangle(x, y, 1, 1), $"{x}:{y}");
                }
            }
            _testPoint = new Point2D(widthAndHeight / 2 + 1, widthAndHeight / 2 + 1);
        }


        [Benchmark]
        public IQuadTreeObject<string>? FindPoint() => _qTree.FindNode(_testPoint).FirstOrDefault();

        [Benchmark]
        public IQuadTreeObject<string>? DynamicTreeFindPoint() => _qTree.FindNode(_testPoint).FirstOrDefault();
    }
}
