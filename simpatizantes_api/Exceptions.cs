using System;

namespace simpatizantes_api.Exceptions
{
    public class SessionExistsException : Exception
    {
        public SessionExistsException() : base("Session already exists.") { }
        public SessionExistsException(string message) : base(message) { }
        public SessionExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
