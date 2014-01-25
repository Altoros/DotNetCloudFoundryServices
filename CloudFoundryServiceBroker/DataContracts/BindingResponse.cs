using System.Runtime.Serialization;
using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    [KnownType(typeof(Credentials))]
    public class BindingResponse : ErrorResponse
    {
        [DataMember(Order=0, Name="credentials", IsRequired = false)]
        public object Credentials { get; set; }

        [DataMember(Order = 0, Name = "syslog_drain_url", IsRequired = false)]
        public string LogUrl { get; set; }

        public override string ToString()
        {
            return string.Format("Credentials: {0}; LogUrl: {1}", Credentials, LogUrl);
        }
    }
}
