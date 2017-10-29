using System;
using System.Configuration;
using System.Threading.Tasks;
using Autofac;
using ReportManager;
using PowerDataAccess;
using PowerReportGenerator;

namespace IntraDayPowerPosition.impl
{
    internal class IntraDayPowerPosition : IIntraDayPowerPosition
    {
        private static readonly Lazy<log4net.ILog> log = new Lazy<log4net.ILog>(() => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

        private static IContainer Container { get; set; }

        public void StartService()
        {
            RegisterDependencies();
        }

        public void StopService()
        {
        }
        
        public async Task OnExecuteAsync(DateTime tenor)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var reportManager = scope.Resolve<IReportManager>();
                await reportManager.GeneratePowerDayAheadPositionsReportAsync(tenor).ConfigureAwait(false);
            }
        }

        #region protected members
        internal void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new Services.PowerService()).As<Services.IPowerService>();
            builder.Register(c => new PowerDataAccess.dtoConverter.DtoConverter()).As<PowerDataAccess.dtoConverter.IDtoConverter>();
            builder.Register(c => new PowerDataAccess.PowerDataAccess(c.Resolve<Services.IPowerService>(),
                c.Resolve<PowerDataAccess.dtoConverter.IDtoConverter>())).As<IPowerDataAccess>();
            builder.Register(c => new PowerReportGenerator.PowerReportGenerator(ConfigurationManager.AppSettings["ReportLocation"], -1))
                .As<IPowerReportGenerator>();
            builder.Register(c => new ReportManager.ReportManager(c.Resolve<IPowerReportGenerator>(),
                c.Resolve<IPowerDataAccess>())).As<IReportManager>();
            Container = builder.Build();
            
        }
        #endregion
    }
}
