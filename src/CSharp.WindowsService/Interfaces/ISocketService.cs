namespace CSharp.WindowsService.Interfaces
{
    public interface ISocketService
    {
        Task StartAsync(CancellationToken stoppingToken);

        Task StopAsync();

    }
}
