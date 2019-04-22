using System;
using System.Runtime.Serialization;

namespace ConsoleApp2
{
    [Serializable]
    public sealed class InvalidGenreException : Exception
    {
        public InvalidGenreException() { }
        public InvalidGenreException(string message) : base(message) { }
        public InvalidGenreException(string message, Exception inner) : base(message, inner) { }
        private InvalidGenreException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }


}
