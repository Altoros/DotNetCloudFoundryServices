using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using CloudFoundryServiceBroker;

namespace BrokerWindowsService
{
    public partial class BrokerService : ServiceBase
    {
        #region Variables

        private ServiceHost _serviceHost;
        private CloudFoundryServiceBroker.CloudFoundryServiceBroker _instance;

        #endregion

        public BrokerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("Starting Broker WCF Service");
            try
            {
                if (_serviceHost != null) _serviceHost.Close();

                _instance = new CloudFoundryServiceBroker.CloudFoundryServiceBroker();
                _serviceHost = new ServiceHost(_instance);
                //_serviceHost = new WebServiceHost(_instance);
                _serviceHost.Open();
                Logger.Info("Broker WCF Service Successfully Started");
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Unable to start Broker WCF Service: {0}", ex);
            }
        }

        protected override void OnStop()
        {
            Logger.InfoFormat("Stopping Broker WCF Service ({0})", _serviceHost == null ? "NULL" : _serviceHost.GetType().ToString());
            if (_serviceHost != null)
            {
                try
                {
                    _serviceHost.Close();
                    _serviceHost = null;
                    if (_instance != null)
                    {
                        var disp = _instance as IDisposable;
                        if(disp != null) disp.Dispose();
                        _instance = null;
                    }
                    Logger.Info("Broker WCF Service Successfully Stopped");
                }
                catch (Exception ex)
                {
                    Logger.InfoFormat("Unable to stop Broker WCF Service: {0}", ex);
                }
            }
        }
    }
}
