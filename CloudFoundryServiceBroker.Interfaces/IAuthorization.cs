using System.ServiceModel;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Interface that provide possibility to override Authorization approach
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// Execute context validation
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="operationContext"></param>
        /// <returns></returns>
        bool CheckAccess(string userName, string password, OperationContext operationContext);
    }
}
