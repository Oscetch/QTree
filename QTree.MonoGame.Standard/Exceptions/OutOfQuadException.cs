using System;

namespace QTree.MonoGame.Standard.Exceptions
{
    public class OutOfQuadException : Exception
    {
        public OutOfQuadException(string message) : base(message) { }
    }
}
