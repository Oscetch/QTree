using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTree.Exceptions
{
    public class OutOfQuadException : Exception
    {
        public OutOfQuadException(string message) : base(message) { }
    }
}
