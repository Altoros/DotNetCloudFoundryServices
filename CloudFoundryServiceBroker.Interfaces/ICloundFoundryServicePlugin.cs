namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Interface that define actual implementation of service plugin for Cloud Foundry
    /// </summary>
    public interface ICloundFoundryServicePlugin
    {
        /// <summary>
        /// Service instance
        /// </summary>
        ICloudFoundryService Service { get; }
        /// <summary>
        /// User Authentificator instance
        /// </summary>
        /// <remarks>user credentials validator</remarks>
        ICredentialsValidator Authenticator { get; }
    }
}
