using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class ProvisioningRequest
    {
        [DataMember(Order=0, Name="service_id", IsRequired = true)]
        public string ServiceId { get; set; }

        [DataMember(Order=1, Name="plan_id", IsRequired = true)]
        public string PlanId { get; set; }

        [DataMember(Order = 2, Name = "organization_guid", IsRequired = true)]
        public string Organization { get; set; }

        [DataMember(Order=3, Name="space_guid", IsRequired = true)]
        public string Space { get; set; }

        public override string ToString()
        {
            return string.Format("ServiceTypeId: {0}; PlanId: {1}; Organization: {2}; Space: {3}", ServiceId, PlanId, Organization, Space);
        }
    }
}
