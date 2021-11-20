using System;

namespace QTree.Exceptions
{
    public class OutOfQuadException : Exception
    {
        public OutOfQuadException(string message) : base(message) { }
    }
}
