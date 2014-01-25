using System.Runtime.Serialization;
using CloudFoundryServiceBroker.Interfaces.Entities;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    [KnownType(typeof(PlanMetadata))]
    public class Plan
    {
        [DataMember(Order = 0, Name = "id", IsRequired = true)]
        public string Id { get; set; }

        [DataMember(Order = 1, Name = "name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Order = 2, Name = "description", IsRequired = true)]
        public string Description { get; set; }

        [DataMember(Order = 3, Name = "metadata", IsRequired = false)]
        public object Metadata { get; set; }

        public override string ToString()
        {
            return string.Format("Plan [ Id: {0}; Name: {1}; Description: {2}; Metadata:{3} ]", Id, Name, Description, Metadata);
        }
    }
}
