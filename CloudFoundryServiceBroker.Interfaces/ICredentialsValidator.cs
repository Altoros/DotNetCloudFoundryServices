namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Interface that allow override validation algorithm for user and passwords
    /// </summary>
    public interface ICredentialsValidator
    {
        /// <summary>
        /// Perform user name and password validation
        /// </summary>
        /// <param name="userName">User name provided to the service broker</param>
        /// <param name="password">Password provided to the service broker</param>
        /// <returns>true if user name and password are valid</returns>
        bool Validate(string userName, string password);
    }
}
