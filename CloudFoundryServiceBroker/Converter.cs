using System;
using System.Linq;
using CloudFoundryServiceBroker.DataContracts;
using System.Collections.Generic;
using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;

namespace CloudFoundryServiceBroker
{
    /// <summary>
    /// Converters
    /// </summary>
    static class Converter
    {
        public static Service[] Convert(IEnumerable<ICloudFoundryService> cloudFoundryServices)
        {
            return cloudFoundryServices.Select(x => new Service
            {
                Id = x.ServiceInfo.Id.ToString(),
                Bindable = x.ServiceInfo.Bindable,
                Description = x.ServiceInfo.Description,
                Name = x.ServiceInfo.Name.ToLower().Replace(' ', '_'),
                Requires = x.ServiceInfo.Requires,
                Tags = x.ServiceInfo.Tags,
                Metadata = x.ServiceInfo.Metadata,
                Plans = x.ServiceInfo.Plans.Select(p => new Plan { Id = p.Id.ToString(), Name = p.Name.ToLower().Replace(' ', '_'), Description = p.Description, Metadata = p.Metadata }).ToArray()
            }).ToArray();
        }

        public static ProvisioningServiceRequest Convert(string serviceInstanceId,  ProvisioningRequest request)
        {
            var serviceRequest = new ProvisioningServiceRequest {ServiceInstanceId = serviceInstanceId};
            if (request != null)
            {
                Guid uid;
                if (!Guid.TryParse(request.ServiceId ?? string.Empty, out uid)) uid = Guid.Empty;
                serviceRequest.ServiceTypeId = uid;
                if (!Guid.TryParse(request.PlanId ?? string.Empty, out uid)) uid = Guid.Empty;
                serviceRequest.PlanId = uid;
                if (!Guid.TryParse(request.Organization ?? string.Empty, out uid)) uid = Guid.Empty;
                serviceRequest.OrganizationId = uid;
                if (!Guid.TryParse(request.Space ?? string.Empty, out uid)) uid = Guid.Empty;
                serviceRequest.SpaceId = uid;
            }
            return serviceRequest;
        }

        public static ProvisioningResponse Convert(ProvisioningServiceResponse response)
        {
            return new ProvisioningResponse { Url = response.Url }; 
        }

        public static CreateBindingRequest Convert(string serviceInstanceId, string bindingId, BindingRequest request)
        {
            var binding = new CreateBindingRequest { ServiceInstanceId = serviceInstanceId, BindingInstanceId = bindingId};
            if (request != null)
            {
                Guid uid;
                if (!Guid.TryParse(request.ServiceId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.ServiceTypeId = uid;
                if (!Guid.TryParse(request.PlanId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.PlanId = uid;
                if (!Guid.TryParse(request.Application ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.ApplicationId = uid;
            }
            return binding;
        }

        public static BindingResponse Convert(CreateBindingResponse response)
        {
            return new BindingResponse {Credentials = response.Credentials, LogUrl = response.LogUrl};
        }

        public static RemoveBindingRequest Convert(string serviceInstanceId, string bindingId, BaseRequest request)
        {
            var binding = new RemoveBindingRequest { ServiceInstanceId = serviceInstanceId, BindingInstanceId = bindingId};
            if (request != null)
            {
                Guid uid;
                if (!Guid.TryParse(request.ServiceId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.ServiceTypeId = uid;
                if (!Guid.TryParse(request.PlanId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.PlanId = uid;
            }
            return binding;
        }

        public static BaseResponse Convert(RemoveBindingResponse response)
        {
            return new BaseResponse();
        }

        public static RemoveInstanceRequest Convert(string serviceInstanceId, BaseRequest request)
        {
            var binding = new RemoveInstanceRequest { ServiceInstanceId = serviceInstanceId };
            if (request != null)
            {
                Guid uid;
                if (!Guid.TryParse(request.ServiceId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.ServiceTypeId = uid;
                if (!Guid.TryParse(request.PlanId ?? string.Empty, out uid)) uid = Guid.Empty;
                binding.PlanId = uid;
            }
            return binding;
        }

        public static BaseResponse Convert(RemoveInstanceResponse response)
        {
            return new BaseResponse();
        }
    }
}
