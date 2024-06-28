using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using CSharp.WPF.MVVM.Messages.Login;
using CSharp.WPF.MVVM.Models.Users;
using CSharp.WPF.MVVM.Services.Login;
using CSharp.WPF.MVVM.ViewModels.Login;
using CSharp.WPF.MVVM.ViewModels.ViewA;
using CSharp.WPF.MVVM.ViewModels.ViewB;
using CSharp.WPF.MVVM.Views.Login;
using CSharp.WPF.MVVM.Views.ViewA;
using CSharp.WPF.MVVM.Views.ViewB;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Windows;

namespace CSharp.WPF.MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IRecipient<ProgramShutDownMessage>, IRecipient<LogoutMessage>, IRecipient<LoginMessage>
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
            services.AddSingleton<LoginWindowModel>();
            services.AddSingleton<ViewAViewModel>();
            services.AddSingleton<ViewBViewModel>();

            // Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<ViewA>();
            services.AddSingleton<ViewB>();

            // Services
            services.AddScoped<ILoginService, LoginService>();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SettingMessage();

            LogOutProcess();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            base.OnExit(e);
        }


        private void SettingMessage()
        {
            try
            {
                WeakReferenceMessenger.Default.Register<ProgramShutDownMessage>(this);
                WeakReferenceMessenger.Default.Register<LogoutMessage>(this);
                WeakReferenceMessenger.Default.Register<LoginMessage>(this);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void Receive(ProgramShutDownMessage message)
        {
            try
            {
                log.Info("Receive ProgramShutDown Message");
                ProgramShutdownProcess();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
        public void Receive(LogoutMessage message)
        {
            LogOutProcess();
        }

        public void Receive(LoginMessage message)
        {
            LogInProcess(message.Value);
        }

        private void LogInProcess(UserInfo userInfo)
        {
            host.Services.GetRequiredService<LoginWindow>().Hide();
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Visible;
        }

        private void LogOutProcess()
        {
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Hidden;
            host.Services.GetRequiredService<LoginWindow>().Show();
        }

        private void ProgramShutdownProcess()
        {
            try
            {
                log.Info("ProgramShutdownProcess");
                host.Services.GetRequiredService<LoginWindow>().Close();
                host.Services.GetRequiredService<MainWindow>().Close();
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
    }

}
