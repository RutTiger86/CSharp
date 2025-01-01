using CSharp.SocketNetwork.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.SocketNetwork.Services
{
    public class ServerService(string ip, int port, int maxClients) : ISocketService
    {
        private readonly string ip = ip;
        private readonly int port = port;
        private readonly int maxClients = maxClients;
        private readonly Socket listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private readonly List<Socket> connectedClients = [];
        private readonly SemaphoreSlim clientSemaphore = new(maxClients, maxClients);
        private bool isRunning = false;

        public async Task StartAsync()
        {
            listenSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            listenSocket.Listen(maxClients);
            isRunning = true;
            Console.WriteLine($"Server started on {ip}:{port} with max {maxClients} clients.");

            while (isRunning)
            {
                var clientSocket = await listenSocket.AcceptAsync();

                if (await clientSemaphore.WaitAsync(0))  // 클라이언트 수를 초과하지 않는지 확인
                {
                    connectedClients.Add(clientSocket);
                    Console.WriteLine("Client connected.");
                    _ = HandleClientAsync(clientSocket);
                }
                else
                {
                    await HandleMaxClientsExceeded(clientSocket);
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
                Console.WriteLine("Client was rejected due to maxClients limit.");
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
            clientSemaphore.Release(); // 사용한 Semaphore 자원 해제
            Console.WriteLine("Server stopped.");
        }

        private async Task HandleClientAsync(Socket clientSocket)
        {
            byte[] buffer = new byte[4];

            byte[] fullMessage = new byte[0];
            int totalBytesRead = 0;
            bool isHeader = true;
            while (isRunning)
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
                        buffer = new byte[125];
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
                            Console.WriteLine($"Received: {receivedText}");

                            string response = "Echo: " + receivedText;
                            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
                            await clientSocket.SendAsync(responseBuffer, SocketFlags.None);

                        }
                    }
                }
                catch (SocketException)
                {
                    break;  // 클라이언트와의 연결이 끊어졌을 때 예외 처리
                }
            }

            connectedClients.Remove(clientSocket);
            clientSocket.Close();
            clientSemaphore.Release();  // 클라이언트 연결 해제 시 Semaphore 해제
            Console.WriteLine("Client disconnected and removed from the list.");
        }
    }
}
