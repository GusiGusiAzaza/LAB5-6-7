using System;
using System.Runtime.Serialization;

namespace LAB5.Exception_Classes
{
    [Serializable]
    public class EgyptException : Exception
    {
        public EgyptException()
        {
        }

        public EgyptException(string message) : base(message)
        {
        }

        public EgyptException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EgyptException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}