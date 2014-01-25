using CloudFoundryServiceBroker.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Cloud Foundry service info
    /// </summary>
    public interface ICloudFoundryServiceInfo
    {
        /// <summary>
        /// Unique service identifier
        /// </summary>
        /// <remarks>An identifier used to correlate this service in future requests to the catalog. This must be unique within Cloud Foundry, using a GUID is recommended.</remarks>
        Guid Id { get; }

        /// <summary>
        /// Service name
        /// </summary>
        /// <remarks>The CLI-friendly name of the service that will appear in the catalog. All lowercase, no spaces</remarks>
        string Name { get; }

        /// <summary>
        /// A short description of the service that will appear in the catalog
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Whether service can be bound to applications
        /// </summary>
        bool Bindable { get; }

        /// <summary>
        /// A list of permissions that the user would have to give the service, if they provision it. The only permission currently supported is syslog_drain
        /// </summary>
        string[] Tags { get; }

        /// <summary>
        /// A list of metadata for a service offering
        /// </summary>
        /// <remarks>There should be used object that can be serialized at proper JSON (expected by CF)</remarks>
        ServiceMetadata Metadata { get; }

        /// <summary>
        /// A list of permissions that the user would have to give the service, if they provision it. The only permission currently supported is syslog_drain
        /// </summary>
        string[] Requires { get; }

        /// <summary>
        /// A list of plans for this service
        /// </summary>
        IEnumerable<PlanEntity> Plans { get; }
    }
}
