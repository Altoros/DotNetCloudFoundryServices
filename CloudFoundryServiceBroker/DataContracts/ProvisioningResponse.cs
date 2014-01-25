using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class ProvisioningResponse : ErrorResponse
    {
        [DataMember(Order = 0, Name = "dashboard_url", IsRequired = false)]
        public string Url { get; set; }

        public override string ToString()
        {
            return string.Format("Url: {0}", Url);
        }
    }
}
