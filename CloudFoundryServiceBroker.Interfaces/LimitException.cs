using System;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Limit exception should be thrown when request exceed limitation of current plan
    /// </summary>
    public class LimitException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LimitException() : base("Exceed allowed number of users")
        {
            
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public LimitException(string message) : base(message)
        {
            
        }
    }
}
