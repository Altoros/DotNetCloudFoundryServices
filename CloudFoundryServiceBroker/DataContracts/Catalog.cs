using System.Linq;
using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.DataContracts
{
    [DataContract]
    public class Catalog
    {
        [DataMember(Order=0, Name = "services", IsRequired = true)]
        public Service[] Services { get; set; }

        public override string ToString()
        {
            return string.Format("Catalog Services: {0}", string.Join("; ", Services.Select(x => x.ToString()).ToList()));
        }
    }
}
