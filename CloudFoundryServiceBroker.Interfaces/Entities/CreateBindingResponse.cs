namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Create binding response
    /// </summary>
    public class CreateBindingResponse
    {
        /// <summary>
        /// Binding credentials information
        /// </summary>
        /// <remarks>A free-form hash of credentials that the bound application can use to access the service. Use of a uri field (such as mysql://user:password@host:port/dbname) is recommended when possible, but it is often useful to provide individual credentials components such as hostname, port, username, and password.</remarks>
        public Credentials Credentials { get; set; }
        /// <summary>
        /// A URL to which Cloud Foundry should drain logs to for the bound application. The syslog_drain permission is required for logs to be automatically wired to applications
        /// </summary>
        public string LogUrl { get; set; }
    }
}
