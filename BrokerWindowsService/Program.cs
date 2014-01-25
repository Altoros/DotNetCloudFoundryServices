using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace BrokerWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && (args[0] ?? string.Empty).Trim().ToUpperInvariant() == "-D")
            {
                Console.WriteLine("Running service in debug console mode");
                using (var host = new ServiceHost(typeof (CloudFoundryServiceBroker.CloudFoundryServiceBroker)))
                {
                    host.Open();
                    Console.WriteLine("Broker service successfully started");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey(true);
                    host.Close();
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                {
                    new BrokerService()
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
