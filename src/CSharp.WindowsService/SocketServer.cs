using CSharp.WindowsService.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CSharp.WindowsService
{
    public class SocketServer : BackgroundService
    {
        private readonly ILogger<SocketServer> logger;
        private ISocketService serverService;

        public SocketServer(ISocketService serverService, ILogger<SocketServer> logger)
        {
            this.logger = logger;
            this.serverService = serverService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("SocketServer 서비스가 시작되었습니다.");
            await serverService.StartAsync(stoppingToken);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("서비스가 정지 명령을 받았습니다.");

            if (serverService != null)
            {
                try
                {
                    await serverService.StopAsync();
                    logger.LogInformation("서버가 정상적으로 종료되었습니다.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "서버 종료 중 오류가 발생했습니다.");
                }
            }

            await base.StopAsync(cancellationToken);
        }



    }
}
