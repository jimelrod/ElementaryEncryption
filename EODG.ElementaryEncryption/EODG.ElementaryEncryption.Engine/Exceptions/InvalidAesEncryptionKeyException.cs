using System;

namespace EODG.ElementaryEncryption.Engine.Exceptions
{
    public class InvalidAesEncryptionKeyException : Exception
    {
        public InvalidAesEncryptionKeyException()
        {
        }

        public InvalidAesEncryptionKeyException(string message)
        : base(message)
        {
        }

        public InvalidAesEncryptionKeyException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
