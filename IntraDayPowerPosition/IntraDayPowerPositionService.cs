using System;
using System.ServiceProcess;
using Autofac;

namespace IntraDayPowerPosition
{
    public partial class prive : ServiceBase
    {
        private impl.IntraDayPowerPosition _intraDayPowerPosition;

        private static IContainer Container { get; set; }

        public prive()
        {
            InitializeComponent();
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
                Interval = 10000 // 5 seconds  
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            using (var scope = Container.BeginLifetimeScope())
            {
                var powerPosition = scope.Resolve<IIntraDayPowerPosition>();
                powerPosition.StartService();
            }
        }

        private void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            Console.WriteLine("Timer Elapsed");

            //Execute

            using (var scope = Container.BeginLifetimeScope())
            {
                var powerPosition = scope.Resolve<IIntraDayPowerPosition>();
                powerPosition.OnExecute(args.SignalTime.Date);
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
