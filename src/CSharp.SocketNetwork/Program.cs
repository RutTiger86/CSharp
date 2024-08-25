using CSharp.SocketNetwork.Interfaces;
using CSharp.SocketNetwork.Service;
using CSharp.SocketNetwork.Servies;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

public class Program
{
    private static Dictionary<string, ISocketService> serverServices = new Dictionary<string, ISocketService>();
    private static Dictionary<string, IClientService> clientServices = new Dictionary<string, IClientService>();

    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        while (true)
        {
            Console.WriteLine("Enter command: [create/send/state/stop] [parameters]");
            string? command = Console.ReadLine();

            (bool validResult, string[] commandParts) = ValidComand(command);

            if (!validResult)
            {
                Console.WriteLine("Invalid command format.");
                continue;
            }

            switch (commandParts[0].ToLower())
            {
                case "create":
                    await HandleCreateCommand(serviceProvider, commandParts[1]);
                    break;

                case "send":
                    await HandleSendCommand(commandParts[1]);
                    break;

                case "state":
                    HandleStateCommand();
                    break;

                case "stop":
                    await HandleStopCommand(commandParts[1]);
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }

    private static (bool validResult, string[] commandParts) ValidComand(string? command)
    {
        if (command == null)
        {
            Console.WriteLine("command is null");
            return (false, new string[0]);
        }

        var commandParts = command.Split(' ', 2);

        switch (commandParts[0].ToLower())
        {
            case "create":
            case "send":
            case "stop":
                if (commandParts.Length < 2)
                {
                    Console.WriteLine("Invalid command format.");
                    return (false, commandParts);
                }
                return (true, commandParts);
            case "state":
                return (true, commandParts);
            default:
                return (false, commandParts);
        }
    }
    private static async Task HandleCreateCommand(IServiceProvider serviceProvider, string parameters)
    {
        (bool validResult, string[] paramParts) = ValidCreateComand(parameters);

        if (!validResult)
        {
            return;
        }

        var type = paramParts[0];
        var name = paramParts[1];
        var endpoint = paramParts[2].Split(',');
        string ip = endpoint[0];
        int port = int.Parse(endpoint[1]);

        if (type.Equals("Server", StringComparison.OrdinalIgnoreCase))
        {
            if (serverServices.ContainsKey(name))
            {
                Console.WriteLine($"Service with name {name} already exists.");
                return;
            }
            int maxClients = 10;

            if (paramParts.Length > 3 && int.TryParse(paramParts[3], out int parsedMaxClients))
            {
                maxClients = parsedMaxClients;
            }

            CreateServer(serviceProvider, name, ip, port, maxClients);
        }
        else if (type.Equals("Client", StringComparison.OrdinalIgnoreCase))
        {
            if (clientServices.ContainsKey(name))
            {
                Console.WriteLine($"Service with name {name} already exists.");
                return;
            }

            await CreateClient(serviceProvider, name, ip, port);
        }
        else
        {
            Console.WriteLine("Invalid type. Must be 'Server' or 'Client'.");
        }
    }

    private static (bool validResult, string[] paramParts) ValidCreateComand(string parameters)
    {
        var paramParts = parameters.Split(' ');

        if (paramParts.Length < 3)
        {
            Console.WriteLine("Create command format: [Server/Client] [Name] [IP],[Port] [MaxClients(optional)]");
            return (false, paramParts);
        }

        var type = paramParts[0];
        var name = paramParts[1];
        var endpoint = paramParts[2].Split(',');

        if (endpoint.Length != 2 || !int.TryParse(endpoint[1], out int port))
        {
            Console.WriteLine("Invalid IP or Port.");
            return (false, paramParts);
        }
        return (true, paramParts);
    }

    private static void CreateServer(IServiceProvider serviceProvider, string name, string ip, int port, int maxClients = 10)
    {
        var server = new ServerService(ip, port, maxClients);
        serverServices.Add(name, server);
        _ = server.StartAsync(); // 비동기로 서버 시작
        Console.WriteLine($"Server {name} created on {ip}:{port} with max {maxClients} clients.");
    }

    private static async Task CreateClient(IServiceProvider serviceProvider, string name, string ip, int port)
    {
        var client = new ClientService(ip, port);
        clientServices.Add(name, client);
        await client.StartAsync();
        Console.WriteLine($"Client {name} connected to {ip}:{port}");
    }


    private static async Task HandleSendCommand(string parameters)
    {
        var paramParts = parameters.Split(' ', 2);
        if (paramParts.Length != 2)
        {
            Console.WriteLine("Send command format: [Name] [Message]");
            return;
        }

        var name = paramParts[0];
        var message = paramParts[1];
        if (!clientServices.ContainsKey(name))
        {
            Console.WriteLine($"No service found with name {name}");
            return;
        }

        var service = clientServices[name];
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        await service.SendMessageAsync(buffer);
    }

    private static void HandleStateCommand()
    {
        if (serverServices.Count == 0 && clientServices.Count == 0)
        {
            Console.WriteLine("No active services.");
            return;
        }

        Console.WriteLine("Active Server Services:");
        foreach (var serviceName in serverServices.Keys)
        {
            Console.WriteLine($"- {serviceName}");
        }

        Console.WriteLine("Active Client Services:");
        foreach (var serviceName in clientServices.Keys)
        {
            Console.WriteLine($"- {serviceName}");
        }
    }

    private static async Task HandleStopCommand(string parameters)
    {

        if (serverServices.ContainsKey(parameters))
        {
            var service = serverServices[parameters];
            await service.StopAsync();
            serverServices.Remove(parameters);
        }
        else if (clientServices.ContainsKey(parameters))
        {
            var service = clientServices[parameters];
            await service.StopAsync();
            serverServices.Remove(parameters);
        }
        else
        {
            Console.WriteLine($"No service found with name {parameters}");
        }
    }
}
