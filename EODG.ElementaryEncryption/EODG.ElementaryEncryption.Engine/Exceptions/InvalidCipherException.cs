using System;

namespace EODG.ElementaryEncryption.Engine.Exceptions
{
    public class InvalidCipherException : Exception
    {
        public InvalidCipherException()
        {
        }

        public InvalidCipherException(string message)
        : base(message)
        {
        }

        public InvalidCipherException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
