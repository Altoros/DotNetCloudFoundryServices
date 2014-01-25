using System.ComponentModel.Composition;
using CloudFoundryServiceBroker.Common;
using CloudFoundryServiceBroker.Interfaces;

namespace MsSqlService
{
    [Export(typeof(ICloundFoundryServicePlugin))]
    public class MsSqlPlugin : ICloundFoundryServicePlugin
    {
        public MsSqlPlugin()
        {
            Service = new MsSqlService();
            Authenticator = new WindowsCredentialsValidator();
        }

        public ICloudFoundryService Service { get; private set; }
        public ICredentialsValidator Authenticator { get; private set; }
    }
}
