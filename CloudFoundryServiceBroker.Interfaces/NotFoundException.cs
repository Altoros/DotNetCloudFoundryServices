using System;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Exception that should be thrown in case when instance not found
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NotFoundException() : base("Requested instance not found")
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message)
            : base(message)
        {
            
        }
    }
}
