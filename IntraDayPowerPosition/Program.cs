using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IntraDayPowerPosition
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // change the project type to console application to make the service run as a console application
            // this way one can test the service without having to install it
            var service = new prive();
            if (Environment.UserInteractive)
            {
                service.StartService(args);
                Console.WriteLine("Press any key to stop program");
                Console.Read();

                service.StopService();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    service
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
