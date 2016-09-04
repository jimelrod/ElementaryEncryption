using System;

namespace EODG.ElementaryEncryption.Engine.Exceptions
{
    class InvalidInitializationVectorException : Exception
    {
        public InvalidInitializationVectorException()
        {
        }

        public InvalidInitializationVectorException(string message)
        : base(message)
        {
        }

        public InvalidInitializationVectorException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}