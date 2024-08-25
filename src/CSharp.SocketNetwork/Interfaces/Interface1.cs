namespace CSharp.SocketNetwork.Interfaces
{
    public interface IClientService : ISocketService
    {
        Task SendMessageAsync(byte[] buffer);
    }
}
