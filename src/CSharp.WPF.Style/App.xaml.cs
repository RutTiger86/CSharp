using CSharp.WPF.Style.Views;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;

namespace CSharp.WPF.Style
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                context.HostingEnvironment.ApplicationName = "CSharp.Authorization.OAuth";
                ConfigureServices(services);
            }).Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // ViewModels
            services.AddSingleton<MainWindowModel>();

            // Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ViewA>();
            services.AddSingleton<ViewB>();
            services.AddSingleton<ViewC>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            host.Services.GetRequiredService<MainWindow>().Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            base.OnExit(e);
        }
    }

}
