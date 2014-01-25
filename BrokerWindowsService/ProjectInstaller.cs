using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using CloudFoundryServiceBroker;

namespace BrokerWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Name of the windows service
        /// </summary>
        private string ServiceName
        {
            get
            {
                var serviceName = serviceInstaller1.ServiceName;
                if (string.IsNullOrEmpty(serviceName)) serviceName = "MsSqlCloudFoundryBrokerService";
                return serviceName;
            }
        }

        /// <summary>
        /// Handler that is called after installation process completed and start windows service
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnAfterInstall(IDictionary savedState)
        {
            Logger.Info("Starting Broker Service");
            try
            {
                var controller = new ServiceController(ServiceName);
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                    Logger.Info("Broker Service Successfully Started");
                }
                else
                {
                    Logger.Info("Broker Service already started");
                }
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Fail to start Broker Service after installation: {0}", ex);
            }
        }

        /// <summary>
        /// Handler that stops windows service before Uninstallation process
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            Logger.Info("Stopping Broker Service");
            try
            {
                var controller = new ServiceController(ServiceName);
                if (controller.Status != ServiceControllerStatus.Stopped)
                {
                    controller.Stop();
                    Logger.Info("Broker Service Successfully Stopped");
                }
                else
                {
                    Logger.Info("Broker Service already stopped");
                }
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Fail to stop MorpheusPrintingService before uninstall: {0}", ex);
            }
        }

    }
}
