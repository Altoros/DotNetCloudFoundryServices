using System.Linq;
using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Metadata object for service plan
    /// </summary>
    [DataContract]
    public class PlanMetadata
    {
        /// <summary>
        /// Bullets
        /// </summary>
        [DataMember(Order = 0, IsRequired = true, Name = "bullets")]
        public string[] Bullets { get; set; }
        /// <summary>
        /// Costs
        /// </summary>
        [DataMember(Order = 1, IsRequired = true, Name = "costs")]
        public Cost[] Costs { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        [DataMember(Order = 2, IsRequired = true, Name = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("PlanMetadata [ Bullets: {0}; Costs: {1}; DisplayName: {2} ]",
                Bullets == null ? "null" : string.Join(",", Bullets),
                Costs == null ? "null" : string.Join(",", Costs.Select(x => x.ToString()).ToList()), 
                DisplayName);
        }
    }

    /// <summary>
    /// Cost definition
    /// </summary>
    [DataContract(Name = "cost")]
    public class Cost
    {
        /// <summary>
        /// Amount
        /// </summary>
        [DataMember(Order = 0, IsRequired = true, Name = "amount")]
        public Amount Amount { get; set; }
        /// <summary>
        /// Unit
        /// </summary>
        [DataMember(Order = 1, IsRequired = true, Name = "unit")]
        public string Unit { get; set; }

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Cost [ Amount: {0}; Unit: {1} ]", Amount, Unit);
        }
    }
    /// <summary>
    /// Amount Definition
    /// </summary>
    [DataContract(Name="amount")]
    public class Amount
    {
        /// <summary>
        /// USD
        /// </summary>
        [DataMember(Order=0, IsRequired = false, Name="usd")]
        public float? USD { get; set; }
        /// <summary>
        /// EUR
        /// </summary>
        [DataMember(Order=1, IsRequired = false, Name="eur")]
        public float? EUR { get; set; }
        /// <summary>
        /// RUR
        /// </summary>
        [DataMember(Order=2, IsRequired = false, Name="rur")]
        public float? RUR { get; set; }

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Amount [ USD: {0}; EUR: {1}; RUR: {2} ]", USD, EUR, RUR);
        }
    }

    /*                "metadata":{
                  "bullets":[
                     "20 GB of messages",
                     "20 connections"
                  ],
                  "costs":[
                     {
                        "amount":{
                           "usd":99.0,
                           "eur":49.0
                        },
                        "unit":"MONTHLY"
                     },
                     {
                        "amount":{
                           "usd":0.99,
                           "eur":0.49
                        },
                        "unit":"1GB of messages over 20GB"
                     }
                  ],
                  "displayName":"Big Bunny"
               }
*/
}
