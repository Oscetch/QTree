using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree
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
