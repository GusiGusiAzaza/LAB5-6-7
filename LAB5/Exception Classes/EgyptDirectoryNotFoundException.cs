using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace LAB5.Exception_Classes
{
    [Serializable]
    public class EgyptDirectoryNotFoundException : DirectoryNotFoundException
    {
        public readonly List<string> Invalidpaths = new List<string>();

        public EgyptDirectoryNotFoundException()
        {
        }

        public EgyptDirectoryNotFoundException(string message) : base(message)
        {
        }

        public EgyptDirectoryNotFoundException(string message, List<string> paths) : base(message)
        {
            Invalidpaths = paths;
        }

        public EgyptDirectoryNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EgyptDirectoryNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}