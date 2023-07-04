using System.Runtime.Serialization;

namespace Persistance.Concrets
{
    [Serializable]
    internal class ReservationCreationException : Exception
    {
        public ReservationCreationException()
        {
        }

        public ReservationCreationException(string? message) : base(message)
        {
        }

        public ReservationCreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ReservationCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}