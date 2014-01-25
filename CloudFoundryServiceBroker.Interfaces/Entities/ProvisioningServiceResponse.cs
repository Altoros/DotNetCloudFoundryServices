
namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Represent result of provisioning request
    /// </summary>
    public class ProvisioningServiceResponse
    {
        /// <summary>
        /// URL of service dashboard if successfully created
        /// </summary>
        public string Url { get; set; }
    }
}
