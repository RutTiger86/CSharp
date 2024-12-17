namespace CSharp.WindowsService
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using CSharp.WindowsService.Interfaces;
    using CSharp.WindowsService.Services;
    using log4net;
    using log4net.Config;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Main(string[] args)
        {
            var log4netConfigPath = Path.Combine(AppContext.BaseDirectory, "log4net.config");
            XmlConfigurator.Configure(new FileInfo(log4netConfigPath));

            if (args.Length > 0)
            {
                string command = args[0].ToLower();

                switch (command)
                {
                    case "-install":
                        if (args.Length > 1)
                        {
                            string serviceName = args[1];
                            InstallService(serviceName);
                        }
                        else
                        {
                            log.Info("Error: 서비스명을 입력하세요. 예) -install MyService");
                        }
                        return;
                    case "-debug":
                        log.Info("디버그 모드 실행 중...");
                        CreateHostBuilder(args).Build().Run();
                        return;

                    default:
                        log.Info("알 수 없는 명령입니다.");
                        log.Info("사용법: -install {서비스명}, -debug");
                        return;
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            log.Info($"서비스 HostBuilder 생성...");

            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();

                logging.AddLog4Net("log4net.config");
            }).ConfigureServices(ConfigureServices) // DI 구성 메서드 호출
            .UseWindowsService(); // Windows 서비스 모드 활성화

            return builder;
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddHostedService<SocketServer>();
            services.AddSingleton<ISocketService, ServerService>();
        }

        private static void InstallService(string serviceName)
        {
            try
            {
                log.Info($"서비스 '{serviceName}'를 설치 중...");
                string exePath = Process.GetCurrentProcess().MainModule?.FileName ?? throw new InvalidOperationException("실행 파일 경로를 찾을 수 없습니다.");
                Process.Start("sc.exe", $"create {serviceName} binPath= \"{exePath}\" start= auto").WaitForExit();
                log.Info($"서비스 '{serviceName}' 설치 완료.");
            }
            catch (Exception ex)
            {
                log.Info($"서비스 설치 중 오류 발생: {ex.Message}");
            }
        }
    }
}
