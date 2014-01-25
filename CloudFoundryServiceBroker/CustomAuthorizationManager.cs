using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CloudFoundryServiceBroker.Interfaces;

namespace CloudFoundryServiceBroker
{
    class CustomAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            //Extract the Authorization header, and parse out the credentials converting the Base64 string:
            var context = WebOperationContext.Current;
            if (context == null)
            {
                throw new WebFaultException<string>("Web context is not available", HttpStatusCode.BadRequest);
            }
            var authHeader = context.IncomingRequest.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var svcCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');

                var authModule = ModuleManager.GetModule<IAuthorization>();
                if (authModule != null && svcCredentials.Length > 1)
                {
                    return authModule.CheckAccess(svcCredentials[0], svcCredentials[1], operationContext);
                }
                return base.CheckAccess(operationContext);
            }
            
            //No authorization header was provided, so challenge the client to provide before proceeding:
            context.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"CloudFoundryServiceBroker\"");
            //Throw an exception with the associated HTTP status code equivalent to HTTP status 401
            throw new WebFaultException<string>("Please provide a username and password", HttpStatusCode.Unauthorized);
        }
    }
}
