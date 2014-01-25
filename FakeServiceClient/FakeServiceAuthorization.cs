using System.ServiceModel;
using CloudFoundryServiceBroker.Interfaces;

namespace FakeServiceClient
{
    class FakeServiceAuthorization : IAuthorization, ICredentialsValidator
    {
        public bool CheckAccess(string userName, string password, OperationContext operationContext)
        {
            return true;
        }

        public bool Validate(string userName, string password)
        {
            return true;
        }
    }
}
