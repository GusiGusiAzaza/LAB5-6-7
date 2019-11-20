using System;
using System.Runtime.Serialization;

namespace LAB5.Exception_Classes
{
    [Serializable]
    public class PersonArgumentException : ArgumentException
    {
        public PersonArgumentException()
        {
        }

        public PersonArgumentException(string message) : base(message)
        {
        }

        public PersonArgumentException(string message, int invalidvalue) : base(message)
        {
            IntValue = invalidvalue;
        }

        public PersonArgumentException(string message, string invalidvalue) : base(message)
        {
            StringValue = invalidvalue;
        }

        public PersonArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PersonArgumentException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public int? IntValue { get; }
        public string StringValue { get; }
    }
}