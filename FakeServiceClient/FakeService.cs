using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;
using FakeServiceClient.Properties;
using System.IO;

namespace FakeServiceClient
{
    public class FakeService : ICloudFoundryService
    {
        private readonly FakeServiceInfo _serviceInfo;

        public FakeService()
        {
            _serviceInfo = new FakeServiceInfo();
        }

        public ICloudFoundryServiceInfo ServiceInfo
        {
            get { return _serviceInfo; }
        }

        public ProvisioningServiceResponse ProvisionInstance(ProvisioningServiceRequest request)
        {
            return new ProvisioningServiceResponse { Url = Path.Combine(Settings.Default.BaseUrl, "Dashboard") };
        }

        public CreateBindingResponse CreateBinding(CreateBindingRequest request)
        {
            return new CreateBindingResponse { Credentials = new Credentials("user", "pass" ,"address", "port", "database"), LogUrl = Path.Combine(Settings.Default.BaseUrl, "Log") };
        }

        public RemoveBindingResponse RemoveBinding(RemoveBindingRequest request)
        {
            return new RemoveBindingResponse();
        }

        public RemoveBindingResponse RemoveInstance(RemoveInstanceRequest request)
        {
            return new RemoveBindingResponse();
        }
    }
}
