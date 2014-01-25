using System;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Exception that should be thrown by service implementation in case of instance conflict
    /// </summary>
    public class ConflictException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConflictException() : base("Conflict detected: instance with provided identifier already exists")
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ConflictException(string message) : base(message)
        {
            
        }
    }
}
