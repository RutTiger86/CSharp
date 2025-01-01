using CSharp.SocketNetwork.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.SocketNetwork.Services
{
    public class ClientService(string ip, int port) : IClientService
    {
        private readonly string ip = ip;
        private readonly int port = port;
        private readonly Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public async Task StartAsync()
        {
            await clientSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), port));
            Console.WriteLine($"Connected to server {ip}:{port}");
        }

        public async Task SendMessageAsync(byte[] buffer)
        {

            var bufferLength = BitConverter.GetBytes(buffer.Length);

            byte[] finalMessage = new byte[bufferLength.Length + buffer.Length];
            Array.Copy(bufferLength, 0, finalMessage, 0, bufferLength.Length);
            Array.Copy(buffer, 0, finalMessage, bufferLength.Length, buffer.Length);

            await clientSocket.SendAsync(finalMessage, SocketFlags.None);

            byte[] receiveBuffer = new byte[buffer.Length + 10 ];
            int recievedBytes = await clientSocket.ReceiveAsync(receiveBuffer, SocketFlags.None);
            string receivedTest = Encoding.UTF8.GetString(receiveBuffer,0, recievedBytes);
            Console.WriteLine(receivedTest);
        }

        public async Task StopAsync()
        { 
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            Console.WriteLine("Client disconnected.");
            await Task.CompletedTask;
        }
    }
}
