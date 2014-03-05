using CloudFoundryServiceBroker.Common;
using CloudFoundryServiceBroker.DataContracts;
using CloudFoundryServiceBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace CloudFoundryServiceBroker
{
    /// <summary>
    /// Cloud Foundry service implementation
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CloudFoundryServiceBroker : ICloudFoundryServiceBroker, IDisposable
    {
        #region Variables

        private CompositionContainer _compositionContainer;
        //[ImportMany]
        //private IEnumerable<Lazy<ICloundFoundryServicePlugin, ICloundFoundryServicePluginMetadata>> _plugins; 
        [ImportMany(typeof(ICloundFoundryServicePlugin))]
        private IEnumerable<ICloundFoundryServicePlugin> _plugins; //This property will be filled by MEF

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CloudFoundryServiceBroker()
        {
            var catalog = new AggregateCatalog();

            var path = ConfigurationManager.AppSettings["ExternalServicesPath"];
            if (string.IsNullOrEmpty(path)) path = AssemblyDirectory;
            if (!Path.IsPathRooted(path)) path = Path.Combine(AssemblyDirectory, path);
            Logger.InfoFormat("Looking for service plugins in '{0}'", path);

            EnsureCatalog(path);

            CreateCatalog(catalog.Catalogs, path);

            _compositionContainer = new CompositionContainer(catalog);
            try
            {
                _compositionContainer.ComposeParts(this);
            }
            catch (CompositionException ex)
            {
                Logger.ErrorFormat("[CompositionException] Failed to collect plugins: {0} {1}", ex, ex.Errors);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("[Generic] Failed to collect plugins: {0}", ex);
            }

            if (_plugins == null) _plugins = new List<ICloundFoundryServicePlugin>();
            Plugins = _plugins.Where(x => x.Service != null && x.Authenticator != null).ToDictionary(x => x.Service.ServiceInfo.Id, x => x);

            foreach(var x in Plugins) Logger.InfoFormat("Plugin found: Key: {0} Type: {1}", x.Key, x.Value.Service.GetType());
            if(Plugins.Count == 1) ModuleManager.Register(Plugins.First().Value.Authenticator);
            else ModuleManager.Register<ICredentialsValidator>(new AllowAllCredentialsValidator());


            //CreateBinding was called (Service: 2d199cc2-418f-44b5-90d0-2661ee6efef3; Id: f1efc4ff-7b8d-47d6-aeed-2160ba1bf57a; ServiceTypeId: 78a2c443-f6e3-42c1-8004-bb0dc3c01a84; PlanId: 3d91602a-7f16-4e31-8fbb-89c894119eb2; Application: 52873cf2-6bd9-466b-b001-58fd1d0d37e3)
            //const string sid = "78a2c443-f6e3-42c1-8004-bb0dc3c01a84";
            ////const string pid = "3d91602a-7f16-4e31-8fbb-89c894119eb2"; //unlim
            //const string pid = "65A213D9-CE05-48DD-9C0D-CB4F1006EF6E"; //1Gb + 1 user
            ////const string pid = "C5B8CF1F-4055-4ACB-B050-B0E9DFD67B33"; //4Gb + 3 user
            //var si = Guid.NewGuid().ToString();
            //var pi1 = Guid.NewGuid().ToString();
            //var pi2 = Guid.NewGuid().ToString();
            //var pi3 = Guid.NewGuid().ToString();
            //var pi4 = Guid.NewGuid().ToString();
            //var cat = FetchCatalog();
            //var provisioningResult = ProvisionInstance(si,
            //    new ProvisioningRequest
            //    {
            //        ServiceId = sid,
            //        PlanId = pid,
            //        Organization = Guid.NewGuid().ToString(),
            //        Space = Guid.NewGuid().ToString()
            //    });
            //var res1 = CreateBinding(si, pi1, new BindingRequest { ServiceId = sid, PlanId = pid, Application = Guid.NewGuid().ToString() });
            //var res2 = CreateBinding(si, pi2, new BindingRequest { ServiceId = sid, PlanId = pid, Application = Guid.NewGuid().ToString() });
            //var res3 = CreateBinding(si, pi3, new BindingRequest { ServiceId = sid, PlanId = pid, Application = Guid.NewGuid().ToString() });
            //var res4 = CreateBinding(si, pi4, new BindingRequest { ServiceId = sid, PlanId = pid, Application = Guid.NewGuid().ToString() });

            //BaseResponse br = null;
            //if(res4 != null) br = RemoveBinding(si, pi4, new BaseRequest { ServiceId = sid, PlanId = pid });
            //if(res3 != null) br = RemoveBinding(si, pi3, new BaseRequest { ServiceId = sid, PlanId = pid });
            //if(res2 != null) br = RemoveBinding(si, pi2, new BaseRequest { ServiceId = sid, PlanId = pid });
            //if(res1 != null) br = RemoveBinding(si, pi1, new BaseRequest { ServiceId = sid, PlanId = pid });

            //var provisioningClenupResult = RemoveInstance(si, new BaseRequest { ServiceId = sid, PlanId = pid });
            //if (cat != null && res1 != null && provisioningResult != null && br != null && provisioningClenupResult != null)
            //{
            //    System.Diagnostics.Debug.WriteLine("test Passed");
            //}
        }

        private void CreateCatalog(ICollection<ComposablePartCatalog> catalogs, string path)
        {
            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                try
                {
                    var ac = new AssemblyCatalog(file);
                    // This line will throw exception if assembly cannot be loaded
                    var parts = ac.Parts.ToList();
                    if(parts.Count == 0){} //Suppress warning with Pure method call
                    catalogs.Add(ac);
                }
                catch (Exception e)
                {
                    Logger.ErrorFormat("Load plugin error: {0}", e);
                }
            }
        }

        #endregion

        /// <summary>
        /// Get information about services catalog
        /// </summary>
        /// <returns></returns>
        public Catalog FetchCatalog()
        {
            Logger.Info("FetchCatalog was called");
            //throw new SecurityTokenValidationException("Invalid credentials!");

            var services = Converter.Convert(Plugins.Select(x=>x.Value.Service));
            var catalog = new Catalog { Services = services };
            Logger.InfoFormat("Fetch Catalog result: {0}", catalog);
            return catalog;
        }
        /// <summary>
        /// Create service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProvisioningResponse ProvisionInstance(string serviceInstanceId, ProvisioningRequest request)
        {
            Logger.InfoFormat("ProvisionInstance was called (Id: {0}; {1})", serviceInstanceId, request);

            var provision = Converter.Convert(serviceInstanceId, request);
            if (provision.ServiceTypeId == Guid.Empty || !Plugins.ContainsKey(provision.ServiceTypeId))
            {
                SetStatus(HttpStatusCode.NotFound);
                return new ProvisioningResponse { ErrorDescription = "Invalid Service Type" };
            }

            try
            {
                var service = Plugins[provision.ServiceTypeId].Service;

                if (provision.PlanId == Guid.Empty || service.ServiceInfo.Plans.All(x => x.Id != provision.PlanId))
                {
                    SetStatus(HttpStatusCode.NotFound);
                    return new ProvisioningResponse {ErrorDescription = "Invalid Service Plan"};
                }

                var response = service.ProvisionInstance(provision);
                var r =  Converter.Convert(response);
                
                Logger.InfoFormat("Provisioning successfully created: {0}", r);
                SetStatus(HttpStatusCode.Created);
                return r;
            }
            catch (ConflictException ex)
            {
                Logger.Error("Provisioning conflict detected");
                SetStatus(HttpStatusCode.Conflict);
                return new ProvisioningResponse { ErrorDescription = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Provisioning error: {0}", ex);
                SetStatus(HttpStatusCode.InternalServerError);
                return new ProvisioningResponse { ErrorDescription = ex.Message };
            }
        }
        /// <summary>
        /// Create binding to a service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public BindingResponse CreateBinding(string serviceInstanceId, string id, BindingRequest request)
        {
            Logger.InfoFormat("CreateBinding was called (Service: {0}; Id: {1}; {2})", serviceInstanceId, id, request);

            var data = Converter.Convert(serviceInstanceId, id, request);
            if (data.ServiceTypeId == Guid.Empty || !Plugins.ContainsKey(data.ServiceTypeId))
            {
                SetStatus(HttpStatusCode.NotFound);
                return new BindingResponse { ErrorDescription = "Invalid Service Type" };
            }

            try
            {
                var service = Plugins[data.ServiceTypeId].Service;

                if (data.PlanId == Guid.Empty || service.ServiceInfo.Plans.All(x => x.Id != data.PlanId))
                {
                    SetStatus(HttpStatusCode.NotFound);
                    return new BindingResponse { ErrorDescription = "Invalid Service Plan" };
                }

                var response = service.CreateBinding(data);

                var r = Converter.Convert(response); 
                Logger.InfoFormat("Binding successfully created: {0}", r);
                SetStatus(HttpStatusCode.Created);
                return r;
            }
            catch (ConflictException ex)
            {
                Logger.Error("Binding conflict detected");
                SetStatus(HttpStatusCode.Conflict);
                return new BindingResponse { ErrorDescription = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Create binding error: {0}", ex);
                SetStatus(HttpStatusCode.InternalServerError);
                return new BindingResponse { ErrorDescription = ex.Message };
            }
        }

        /// <summary>
        /// Remove binding from service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="id"></param>
        /// <param name="serviceTypeId"></param>
        /// <param name="planId"></param>
        /// <returns></returns>
        public BaseResponse RemoveBindingParams(string serviceInstanceId, string id, string serviceTypeId, string planId)
        {
            return RemoveBinding(serviceInstanceId, id, new BaseRequest{ServiceId = serviceTypeId, PlanId = planId});
        }
        /// <summary>
        /// Remove binding from service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse RemoveBinding(string serviceInstanceId, string id, BaseRequest request)
        {
            Logger.InfoFormat("RemoveBinding was called (Service: {0}; Id: {1}; {2})", serviceInstanceId, id, request);

            var data = Converter.Convert(serviceInstanceId, id, request);
            //if (data.ServiceTypeId == Guid.Empty || !Plugins.ContainsKey(data.ServiceTypeId))
            //{
            //    SetStatus(HttpStatusCode.NotFound);
            //    return new BaseResponse { ErrorDescription = "Invalid Service Type" };
            //}

            try
            {
                var service = Plugins[data.ServiceTypeId].Service;

                //if (data.PlanId == Guid.Empty || service.Plans.All(x => x.Id != data.PlanId))
                //{
                //    SetStatus(HttpStatusCode.NotFound);
                //    return new BaseResponse { ErrorDescription = "Invalid Service Plan" };
                //}

                var response = service.RemoveBinding(data);

                Logger.Info("Binding successfully removed");
                SetStatus(HttpStatusCode.Created);
                return Converter.Convert(response);
            }
            catch (NotFoundException ex)
            {
                Logger.Error("Binding not found");
                SetStatus(HttpStatusCode.NotFound);
                return new BaseResponse { ErrorDescription = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Remove binding error: {0}", ex);
                SetStatus(HttpStatusCode.InternalServerError);
                return new BaseResponse { ErrorDescription = ex.Message };
            }
        }

        /// <summary>
        /// Remove service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="serviceTypeId"></param>
        /// <param name="planId"></param>
        /// <returns></returns>
        public BaseResponse RemoveInstanceParams(string serviceInstanceId, string serviceTypeId, string planId)
        {
            return RemoveInstance(serviceInstanceId, new BaseRequest {ServiceId = serviceTypeId, PlanId = planId});
        }
        /// <summary>
        /// Remove service instance
        /// </summary>
        /// <param name="serviceInstanceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse RemoveInstance(string serviceInstanceId, BaseRequest request)
        {
            Logger.InfoFormat("RemoveInstance was called (Id: {0}; {1})", serviceInstanceId, request);
            var data = Converter.Convert(serviceInstanceId, request);
            //if (data.ServiceTypeId == Guid.Empty || !Plugins.ContainsKey(data.ServiceTypeId))
            //{
            //    SetStatus(HttpStatusCode.NotFound);
            //    return new BaseResponse { ErrorDescription = "Invalid Service Type" };
            //}

            try
            {
                var service = Plugins[data.ServiceTypeId].Service;

                //if (data.PlanId == Guid.Empty || service.Plans.All(x => x.Id != data.PlanId))
                //{
                //    SetStatus(HttpStatusCode.NotFound);
                //    return new BaseResponse { ErrorDescription = "Invalid Service Plan" };
                //}

                var response = service.RemoveInstance(data);

                Logger.Info("Instance successfully removed");
                SetStatus(HttpStatusCode.Created);
                return Converter.Convert(response);
            }
            catch (NotFoundException ex)
            {
                Logger.Error("Service Instance not found");
                SetStatus(HttpStatusCode.NotFound);
                return new BaseResponse { ErrorDescription = ex.Message };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Remove instance error: {0}", ex);
                SetStatus(HttpStatusCode.InternalServerError);
                return new BaseResponse { ErrorDescription = ex.Message };
            }
        }

        #region Properties

        /// <summary>
        /// erturn list of currently available plugins
        /// </summary>
        public Dictionary<Guid, ICloundFoundryServicePlugin> Plugins { get; private set; }

        private string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #endregion

        #region Private methods

        private void EnsureCatalog(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Logger.WarningFormat("Unable to enshure plugins catalog availability: {0}", ex);
            }
        }

        private void SetStatus(HttpStatusCode code)
        {
            var context = WebOperationContext.Current;
            if (context != null)
            {
                context.OutgoingResponse.StatusCode = code;
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            if (_compositionContainer != null)
            {
                _compositionContainer.Dispose();
                _compositionContainer = null;
            }
        }

        #endregion
    }
}
