using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class BindingRequest
    {
        [DataMember(Order = 0, Name = "service_id", IsRequired = true)]
        public string ServiceId { get; set; }

        [DataMember(Order = 1, Name = "plan_id", IsRequired = true)]
        public string PlanId { get; set; }

        [DataMember(Order = 2, Name = "app_guid", IsRequired = false)]
        public string Application { get; set; }

        public override string ToString()
        {
            return string.Format("ServiceTypeId: {0}; PlanId: {1}; Application: {2}", ServiceId, PlanId, Application);
        }
    }

}
