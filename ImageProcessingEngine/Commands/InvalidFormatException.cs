using System;
using System.Runtime.Serialization;

namespace ImageProcessingEngine.Commands
{
    public class InvalidFormatException : Exception
    {
        #region Constructors

        public InvalidFormatException()
        {
        }

        public InvalidFormatException(string message) : base(message)
        {
        }

        public InvalidFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}