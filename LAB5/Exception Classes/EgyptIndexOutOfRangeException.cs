using System;
using System.Runtime.Serialization;

namespace LAB5.Exception_Classes
{
    [Serializable]
    public class EgyptIndexOutOfRangeException : ArgumentOutOfRangeException
    {
        public EgyptIndexOutOfRangeException()
        {
        }

        public EgyptIndexOutOfRangeException(string message) : base(message)
        {
        }

        public EgyptIndexOutOfRangeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EgyptIndexOutOfRangeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}