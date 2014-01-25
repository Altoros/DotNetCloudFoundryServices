using System.ComponentModel.Composition;
using CloudFoundryServiceBroker.Interfaces;

namespace FakeServiceClient
{
    [Export(typeof(ICloundFoundryServicePlugin))]
    public class FakePlugin : ICloundFoundryServicePlugin
    {
        public FakePlugin()
        {
            Service = new FakeService();
            Authenticator = new FakeServiceAuthorization();
        }

        public ICloudFoundryService Service { get; private set; }
        public ICredentialsValidator Authenticator { get; private set; }
    }
}
