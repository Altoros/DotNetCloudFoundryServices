using CloudFoundryServiceBroker.Interfaces;

namespace CloudFoundryServiceBroker.Common
{
    /// <summary>
    /// Credentials validator implementation that just allow everything
    /// </summary>
    public class AllowAllCredentialsValidator : ICredentialsValidator
    {
        /// <summary>
        /// Perform validation of user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Always return true</returns>
        public bool Validate(string userName, string password)
        {
            return true;
        }
    }
}
