using System;

namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Service plan definition
    /// </summary>
    public class PlanEntity
    {
        /// <summary>
        /// An identifier used to correlate this plan in future requests to the catalog. This must be unique within Cloud Foundry.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The CLI-friendly name of the plan that will appear in the catalog. All lowercase, no spaces.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the service that will appear in the catalog.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of metadata for a service plan
        /// </summary>
        /// <remarks>There should be used object that can be serialized at proper JSON (expected by CF)</remarks>
        public PlanMetadata Metadata { get; set; }
    }
}
