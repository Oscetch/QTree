using BenchmarkDotNet.Attributes;
using QTree.Interfaces;
using QTree.RayCasting;
using QTree.Util;

namespace QTree.Benchmarking
{
    public class RayCastBenchmarks
    {
        [Params(10_000)]
        public int ItemsInTree;

        [Params(5, 10, 100)]
        public int Depth;

        [Params(5, 50, 100)]
        public int SplitLimit;

        private IQuadTree<string> _qTree;
        private Ray _testRay;
        private string searchObject = "49:49";

        [GlobalSetup(Target = nameof(RayCastPoint))]
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
            _testRay = new Ray(Point2D.Zero, new Point2D(widthAndHeight));
        }

        [GlobalSetup(Target = nameof(DynamicTreeRayCastPoint))]
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
            _testRay = new Ray(Point2D.Zero, new Point2D(widthAndHeight));
        }


        [Benchmark]
        public RayHit<IQuadTreeObject<string>>? RayCastPoint() => _qTree.RayCast(_testRay).FirstOrDefault(x => x.Object.Object == searchObject);

        [Benchmark]
        public RayHit<IQuadTreeObject<string>>? DynamicTreeRayCastPoint() => _qTree.RayCast(_testRay).FirstOrDefault(x => x.Object.Object == searchObject);
    }
}
