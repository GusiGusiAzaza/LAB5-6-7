using System;
using System.Runtime.Serialization;

namespace LAB5.Exception_Classes
{
    [Serializable]
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException()
        {
        }

        public PersonNotFoundException(string message) : base(message)
        {
        }

        public PersonNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PersonNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}