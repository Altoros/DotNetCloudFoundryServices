using CloudFoundryServiceBroker.Interfaces.Entities;

namespace CloudFoundryServiceBroker.Interfaces
{
    /// <summary>
    /// Interface that defines implementation API for Cloud Foundry service
    /// </summary>
    public interface ICloudFoundryService
    {
        /// <summary>
        /// Cloud Foundry service info
        /// </summary>
        ICloudFoundryServiceInfo ServiceInfo { get;  }

        #region Operations

        /// <summary>
        /// Provision (create) instance of service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Actual meaning: Create database</remarks>
        /// <exception cref="ConflictException">ConflictException should be thrown in case if instance with requested Id already exists</exception>
        ProvisioningServiceResponse ProvisionInstance(ProvisioningServiceRequest request);

        /// <summary>
        /// Create binding to the service instance
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Create database user who can access database created with provision request</remarks>
        /// <exception cref="ConflictException">ConflictException should be thrown in case if instance with requested Id already exists</exception>
        /// <exception cref="LimitException">LimitException should be thrown in case if exceed allowed number of bindings</exception>
        CreateBindingResponse CreateBinding(CreateBindingRequest request);

        /// <summary>
        /// Remove Binding
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Remove databse user</remarks>
        /// <exception cref="NotFoundException">NotFoundException should be thrown if instance not exists</exception>
        RemoveBindingResponse RemoveBinding(RemoveBindingRequest request);

        /// <summary>
        /// Remove instance
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Remove database</remarks>
        /// <exception cref="NotFoundException">NotFoundException should be thrown if instance not exists</exception>
        RemoveBindingResponse RemoveInstance(RemoveInstanceRequest request);

        #endregion
    }
}
