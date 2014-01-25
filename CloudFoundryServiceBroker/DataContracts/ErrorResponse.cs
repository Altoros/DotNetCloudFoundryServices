using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Order = 0, Name = "description", IsRequired = false)]
        public string ErrorDescription { get; set; }
    }
}
