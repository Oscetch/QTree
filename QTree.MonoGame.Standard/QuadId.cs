namespace QTree.MonoGame.Standard
{
    public sealed class QuadId
    {
        private static long _nextId = 0;

        public long Id { get; }

        public QuadId()
        {
            Id = _nextId++;
        }
    }
}
