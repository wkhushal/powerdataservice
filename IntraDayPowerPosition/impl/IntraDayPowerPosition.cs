using System;
using Autofac;
using ReportManager;
using PowerDataAccess;
using PowerReportGenerator;

namespace IntraDayPowerPosition.impl
{
    internal class IntraDayPowerPosition : IIntraDayPowerPosition
    {
        private static IContainer Container { get; set; }

        public IntraDayPowerPosition()
        {

        }
        
        public void StartService()
        {
            RegisterDependencies();
        }

        public void StopService()
        {
        }
        
        public void OnExecute(DateTime tenor)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var reportManager = scope.Resolve<IReportManager>();
                reportManager.GeneratePowerDayAheadPositionsReport(tenor);
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
            builder.Register(c => new PowerReportGenerator.PowerReportGenerator(null, -1))
                .As<IPowerReportGenerator>();
            builder.Register(c => new ReportManager.ReportManager(c.Resolve<IPowerReportGenerator>(),
                c.Resolve<IPowerDataAccess>())).As<IReportManager>();
            Container = builder.Build();
            
        }
        #endregion


    }
}
