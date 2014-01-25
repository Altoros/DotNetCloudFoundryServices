using System.ServiceModel;
using System.ServiceModel.Web;
using CloudFoundryServiceBroker.DataContracts;

namespace CloudFoundryServiceBroker
{
    /// <summary>
    /// Service Contract definition for Cloud Foudry Service Broker
    /// </summary>
    [ServiceContract]
    public interface ICloudFoundryServiceBroker
    {
        /// <summary>
        /// Returns broker's catalog of services
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, 
            UriTemplate = "v2/catalog")]
        Catalog FetchCatalog();

        /// <summary>
        /// Create new instance of a service
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "v2/service_instances/{serviceInstanceId}")]
        ProvisioningResponse ProvisionInstance(string serviceInstanceId, ProvisioningRequest request);

        /// <summary>
        /// Create application binding to a service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "v2/service_instances/{serviceInstanceId}/service_bindings/{id}")]
        BindingResponse CreateBinding(string serviceInstanceId, string id, BindingRequest request);

        ///// <summary>
        ///// Remove binding to service instance
        ///// </summary>
        ///// <param name="serviceInstanceId"></param>
        ///// <param name="id"></param>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[OperationContract(IsOneWay = false)]
        //[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        //    UriTemplate = "v2/service_instances/{serviceInstanceId}/service_bindings/{id}")]
        //BaseResponse RemoveBinding(string serviceInstanceId, string id, BaseRequest request);

        /// <summary>
        /// Remove binding to service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="id"></param>
        /// <param name="serviceTypeId"></param>
        /// <param name="planId"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "v2/service_instances/{serviceInstanceId}/service_bindings/{id}?service_Id={serviceTypeId}&plan_id={planId}")]
        BaseResponse RemoveBindingParams(string serviceInstanceId, string id, string serviceTypeId, string planId);

        ///// <summary>
        ///// Remove service instance
        ///// </summary>
        ///// <param name="serviceInstanceId"></param>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[OperationContract(IsOneWay = false)]
        //[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        //    UriTemplate = "v2/service_instances/{serviceInstanceId}")]
        //BaseResponse RemoveInstance(string serviceInstanceId, BaseRequest request);

        /// <summary>
        /// Remove service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="serviceTypeId"></param>
        /// <param name="planId"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "v2/service_instances/{serviceInstanceId}?service_Id={serviceTypeId}&plan_id={planId}")]
        BaseResponse RemoveInstanceParams(string serviceInstanceId, string serviceTypeId, string planId);
    }
}
