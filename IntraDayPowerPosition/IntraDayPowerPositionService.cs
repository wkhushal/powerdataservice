using System;
using System.Configuration;
using System.Threading.Tasks;
using System.ServiceProcess;
using Autofac;

namespace IntraDayPowerPosition
{
    public partial class IntraDayPowerPosition : ServiceBase
    {
        private static readonly Lazy<log4net.ILog> log = new Lazy<log4net.ILog>(() => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));


        private impl.IntraDayPowerPosition _intraDayPowerPosition;

        private static IContainer Container { get; set; }

        public IntraDayPowerPosition()
        {
            RegisterDependencies();
        }

        public void StartService(string[] args)
        {
            this.OnStart(args);
        }

        public void StopService()
        {
            this.OnStop();
        }

        #region protected members

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new impl.IntraDayPowerPosition()).As<IIntraDayPowerPosition>();
            Container = builder.Build();

        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer()
            {  
                Interval =  60000 * (int.TryParse(ConfigurationManager.AppSettings["ServiceIntervalMinutes"], out var intervalInMinutes) ? 
                            intervalInMinutes : 1 )  
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            using (var scope = Container.BeginLifetimeScope())
            {
                var powerPosition = scope.Resolve<IIntraDayPowerPosition>();
                powerPosition.StartService();
            }
        }

        private async void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            log.Value.Info("Timer Elapsed");
            using (var scope = Container.BeginLifetimeScope())
            {
                var powerPosition = scope.Resolve<IIntraDayPowerPosition>();
                await powerPosition.OnExecuteAsync(args.SignalTime.Date).ConfigureAwait(false);
            }
        }

        protected override void OnStop()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var powerPosition = scope.Resolve<IIntraDayPowerPosition>();
                powerPosition.StopService();
            }
        }
        #endregion

    }
}
