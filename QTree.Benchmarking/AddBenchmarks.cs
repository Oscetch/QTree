using BenchmarkDotNet.Attributes;
using QTree.Interfaces;
using QTree.Util;

namespace QTree.Benchmarking
{
    public class AddBenchmarks
    {
        [Params(10_000)]
        public int ItemsInTree;

        [Params(5, 10, 100)]
        public int Depth;

        [Params(5, 50, 100)]
        public int SplitLimit;

        private IQuadTree<string> _qTree;
        private QuadTreeObject<string> _objectToAdd;

        [GlobalSetup(Target = nameof(AddPoint))]
        public void Setup()
        {
            var widthAndHeight = (int)(Math.Sqrt(ItemsInTree) * 2);
            _qTree = new QuadTree<string>(new Rectangle(0, 0, widthAndHeight, widthAndHeight), SplitLimit, Depth);
            for (int x = 0; x < widthAndHeight; x += 2)
            {
                for (int y = 0; y < widthAndHeight; y += 2)
                {
                    _qTree.Add(new Rectangle(x, y, 1, 1), string.Empty);
                }
            }

            var addBounds = new Rectangle(widthAndHeight - 1, widthAndHeight - 1, 1, 1);
            _objectToAdd = new QuadTreeObject<string>(addBounds, $"added object");
        }

        [GlobalSetup(Target = nameof(DynamicTreeAddPoint))]
        public void DynamicTreeSetup()
        {
            var widthAndHeight = (int)Math.Sqrt(ItemsInTree) * 2;
            _qTree = new DynamicQuadTree<string>(SplitLimit, Depth);
            for (int x = 0; x < widthAndHeight; x += 2)
            {
                for (int y = 0; y < widthAndHeight; y += 2)
                {
                    _qTree.Add(new Rectangle(x, y, 1, 1), string.Empty);
                }
            }

            var addBounds = new Rectangle(widthAndHeight - 1, widthAndHeight - 1, 1, 1);
            _objectToAdd = new QuadTreeObject<string>(addBounds, $"added object");
        }

        [Benchmark]
        public void AddPoint() => _qTree.Add(_objectToAdd);

        [Benchmark]
        public void DynamicTreeAddPoint() => _qTree.Add(_objectToAdd);
    }
}
