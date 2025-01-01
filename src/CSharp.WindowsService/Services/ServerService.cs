using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using CSharp.WindowsService.Commons;
using CSharp.WindowsService.Interfaces;
using CSharp.WindowsService.Models;
using Microsoft.Extensions.Logging;

namespace CSharp.WindowsService.Services
{
    public class ServerService : ISocketService
    {
        private readonly Socket listenSocket;
        private readonly List<Socket> connectedClients;
        private readonly SemaphoreSlim clientSemaphore;
        private bool isRunning;
        private readonly ILogger logger;
        private readonly ServerConfig serverConfig;

        public ServerService(ILogger<ServerService> logger)
        {
            this.logger = logger;
            serverConfig = LoadServerConfig(StaticsProperty.ConfigFilePath);

            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connectedClients = [];
            clientSemaphore = new SemaphoreSlim(serverConfig.MaxClients, serverConfig.MaxClients);  // MaxClients에 따라 동기화 처리
            isRunning = false;
        }

        private static ServerConfig LoadServerConfig(string filePath)
        {
            string fullPath = Path.IsPathRooted(filePath)
                ? filePath
                : Path.Combine(AppContext.BaseDirectory, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"설정 파일을 찾을 수 없습니다: {filePath}");

            try
            {

                var doc = XDocument.Load(fullPath);


                var serverElement = doc.Element("configuration")?.Element("server");
                if (serverElement == null)
                    throw new InvalidOperationException("설정 파일에서 <server> 섹션을 찾을 수 없습니다.");


                var serverIp = serverElement.Element("ServerIp")?.Value ?? throw new InvalidOperationException("서버 IP 설정이 누락되었습니다.");
                var port = int.Parse(serverElement.Element("port")?.Value ?? throw new InvalidOperationException("포트 설정이 누락되었습니다."));
                var maxClients = int.Parse(serverElement.Element("maxClients")?.Value ?? throw new InvalidOperationException("MaxClients 설정이 누락되었습니다."));
                var maxBytes = int.Parse(serverElement.Element("maxBytesPerRequest")?.Value ?? throw new InvalidOperationException("MaxBytesPerRequest 설정이 누락되었습니다."));

                return new ServerConfig
                {
                    ServerIp = serverIp,
                    Port = port,
                    MaxClients = maxClients,
                    MaxBytesPerRequest = maxBytes
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("설정 파일 파싱 중 오류가 발생했습니다.", ex);
            }
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            listenSocket.Bind(new IPEndPoint(IPAddress.Parse(serverConfig.ServerIp), serverConfig.Port));
            listenSocket.Listen(serverConfig.MaxClients);
            isRunning = true;
            logger.LogInformation($"Server started on {serverConfig.ServerIp}:{serverConfig.Port} with max {serverConfig.MaxClients} clients.");

            while (isRunning && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var clientSocket = await listenSocket.AcceptAsync();

                    if (await clientSemaphore.WaitAsync(0))
                    {
                        connectedClients.Add(clientSocket);
                        logger.LogInformation("Client connected.");
                        _ = HandleClientAsync(clientSocket, stoppingToken);
                    }
                    else
                    {
                        await HandleMaxClientsExceeded(clientSocket);
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("서버가 중지 요청을 받았습니다.");
                    break; 
                }
                catch(SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
                {
                    logger.LogInformation("소켓 작업이 취소되었습니다. 서버가 종료 중입니다.");
                    break;
                }
                catch (SocketException ex)
                {
                    logger.LogError(ex, "소켓 오류 발생");
                    continue;
                }
            }
        }

        private async Task HandleMaxClientsExceeded(Socket clientSocket)
        {
            try
            {
                string message = "Pool이 가득 찼습니다. 연결이 종료됩니다.";
                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                await clientSocket.SendAsync(messageBuffer, SocketFlags.None);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                logger.LogInformation("Client was rejected due to maxClients limit.");
            }
        }

        public async Task StopAsync()
        {
            isRunning = false;

            var disconnectMessage = Encoding.UTF8.GetBytes("Server is shutting down.");
            foreach (var client in connectedClients)
            {
                await client.SendAsync(disconnectMessage, SocketFlags.None);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }

            connectedClients.Clear();
            listenSocket.Close();
            while (clientSemaphore.CurrentCount < serverConfig.MaxClients)
            {
                clientSemaphore.Release(); // 사용한 Semaphore 자원 해제
            }
            logger.LogInformation("Server stopped.");
        }

        private async Task HandleClientAsync(Socket clientSocket, CancellationToken stoppingToken)
        {
            byte[] buffer = new byte[4];

            byte[] fullMessage = [];
            int totalBytesRead = 0;
            bool isHeader = true;
            string clientIp = ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();

            while (isRunning && !stoppingToken.IsCancellationRequested)
            {
                try
                {

                    int receivedBytes = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);

                    if (receivedBytes == 0)
                    {
                        break;  // 클라이언트가 연결을 끊었을 때
                    }

                    if (isHeader)
                    {
                        int messageLength = BitConverter.ToInt32(buffer, 0);
                        fullMessage = new byte[messageLength];
                        totalBytesRead = 0;
                        buffer = new byte[serverConfig.MaxBytesPerRequest];
                        isHeader = false;
                    }
                    else
                    {
                        Array.Copy(buffer, 0, fullMessage, totalBytesRead, receivedBytes);
                        totalBytesRead += receivedBytes;

                        if (totalBytesRead == fullMessage.Length)
                        {
                            isHeader = true;
                            buffer = new byte[4];

                            string receivedText = Encoding.UTF8.GetString(fullMessage);
                            logger.LogInformation($"[{clientIp}] Received: {receivedText}");

                            string response = "Echo: " + receivedText;
                            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
                            logger.LogInformation($"[{clientIp}] EchoSend: {response}");
                            await clientSocket.SendAsync(responseBuffer, SocketFlags.None);

                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("서버가 중지 요청을 받았습니다.");
                    break; 
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
                { 
                    logger.LogInformation("소켓 작업이 취소되었습니다. 서버가 종료 중입니다.");
                    break;
                }
                catch (SocketException ex)
                {
                    logger.LogError(ex, "소켓 오류 발생");
                    break;
                }
            }

            connectedClients.Remove(clientSocket);
            clientSocket.Close();
            clientSemaphore.Release();  
            logger.LogInformation("Client disconnected and removed from the list.");
        }
    }
}
