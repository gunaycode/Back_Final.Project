using System.Runtime.Serialization;

namespace Persistance.Concrets
{
    [Serializable]
    internal class FileTypeException : Exception
    {
        public FileTypeException()
        {
        }

        public FileTypeException(string? message) : base(message)
        {
        }

        public FileTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FileTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}