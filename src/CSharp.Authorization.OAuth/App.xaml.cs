﻿using CommunityToolkit.Mvvm.Messaging;
using CSharp.Authorization.OAuth.Messages;
using CSharp.Authorization.OAuth.Messages.Logins;
using CSharp.Authorization.OAuth.Models.Google;
using CSharp.Authorization.OAuth.Models.Logins;
using CSharp.Authorization.OAuth.ViewModels.Logins;
using CSharp.Authorization.OAuth.Views.Logins;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Windows;

namespace CSharp.Authorization.OAuth
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
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindows>();
            services.AddScoped<LoginWindowModel>();
            services.AddScoped<ILoginService, LoginService>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await host.StartAsync();
                      
            SettingMessage();

            LogOutProcess();
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

        private void LogInProcess(GoogleUserInfo userInfo)
        {
            host.Services.GetRequiredService<LoginWindows>().Hide();
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Visible;
        }

        private void LogOutProcess() 
        {
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Hidden;
            host.Services.GetRequiredService<LoginWindows>().Show();
        }

        private void ProgramShutdownProcess()
        {
            try
            {           
                log.Info("ProgramShutdownProcess");
                host.Services.GetRequiredService<LoginWindows>().Close();
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
