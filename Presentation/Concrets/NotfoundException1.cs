using System.Runtime.Serialization;

namespace Persistance.Concrets
{
    [Serializable]
    internal class NotfoundException : Exception
    {
        public NotfoundException()
        {
        }

        public NotfoundException(string? message) : base(message)
        {
        }

        public NotfoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotfoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}