using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CloudFoundryServiceBroker.Interfaces.Entities;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    [KnownType(typeof(ServiceMetadata))]
    public class Service
    {
        [DataMember(Order=0, Name="id", IsRequired = true)]
        public string Id { get; set; }

        [DataMember(Order=1, Name="name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Order=2, Name="description", IsRequired = true)]
        public string Description { get; set; }

        [DataMember(Order=3, Name="bindable", IsRequired = true)]
        public bool Bindable { get; set; }

        [DataMember(Order=4, Name="tags", IsRequired = false)]
        public string[] Tags { get; set; }

        [DataMember(Order=5, Name="metadata", IsRequired = false)]
        public object Metadata { get; set; }

        [DataMember(Order=6, Name="requires", IsRequired = false)]
        public string[] Requires { get; set; }

        [DataMember(Order=7, Name="plans", IsRequired = true)]
        public Plan[] Plans { get; set; }

        public override string ToString()
        {
            return string.Format("Service [ Id: {0}; Name: {1}; Description: {2}; Bindable: {3}; Tags: {4}; Metadata: {5}; Requires: {6}; Plans: {7}; ]",
                Id, Name, Description, Bindable, 
                Tags==null?"null":string.Join(",",Tags), 
                Metadata, 
                Requires==null?"null":string.Join(",", Requires),
                Plans == null ? "null" : string.Join(";", Plans.Select(x => x.ToString()).ToList()));
        }
    }
}
