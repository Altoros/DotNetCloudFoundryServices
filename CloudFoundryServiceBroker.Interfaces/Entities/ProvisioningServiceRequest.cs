﻿using System;

namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Service provisioning request data
    /// </summary>
    public class ProvisioningServiceRequest
    {
        /// <summary>
        /// Service instance id
        /// </summary>
        /// <remarks>Service instance is generated by Cloud Foundry and will be used with all requests to the service</remarks>
        public string  ServiceInstanceId { get; set; }
        /// <summary>
        /// Service type Id
        /// </summary>
        /// <remarks>Unique identifier that identify service type</remarks>
        public Guid ServiceTypeId { get; set; }
        /// <summary>
        /// Plan Id
        /// </summary>
        public Guid PlanId { get; set; }
        /// <summary>
        /// Organization Id
        /// </summary>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// Space Id
        /// </summary>
        public Guid SpaceId { get; set; }
    }
}
