namespace CSharp.WindowsService.Models
{
    public class ServerConfig
    {
        public string ServerIp { get; set; }
        public int Port { get; set; }
        public int MaxClients { get; set; }
        public int MaxBytesPerRequest { get; set; }
    }
}
