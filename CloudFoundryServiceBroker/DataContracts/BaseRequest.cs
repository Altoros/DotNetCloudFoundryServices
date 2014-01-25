using System.Runtime.Serialization;
using System.Web.Routing;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class BaseRequest
    {
        [DataMember(Order = 0, Name = "service_id", IsRequired = true)]
        public string ServiceId { get; set; }

        [DataMember(Order = 1, Name = "plan_id", IsRequired = true)]
        public string PlanId { get; set; }

        public override string ToString()
        {
            return string.Format("ServiceTypeId: {0}; PlanId: {1}", ServiceId, PlanId);
        }
    }
}
