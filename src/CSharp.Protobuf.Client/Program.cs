using CSharp.Protobuf.Purchase;
using CSharp.Protobuf.Service;
using Grpc.Net.Client;

namespace CSharp.Protobuf.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001"); 
            var client = new IAPService.IAPServiceClient(channel);

            // 1. MakePurchase Call
            await MakePurchaseExample(client);

            Console.WriteLine($"{Environment.NewLine}==========================================={Environment.NewLine}");

            // 2. GetPurchaseHistory Call
            await GetPurchaseHistoryExample(client);

            Console.WriteLine("All tasks have been completed. Press any key to exit.");
            Console.ReadKey();
        }

        private static async Task MakePurchaseExample(IAPService.IAPServiceClient client)
        {
            Console.WriteLine("Calling MakePurchase...");

            var request = new PurchaseRequest
            {
                UserId = "user123",
                ProductId = "product456",
                Quantity = 2
            };

            try
            {
                var response = await client.MakePurchaseAsync(request);

                Console.WriteLine("MakePurchase Response:");
                Console.WriteLine($"Status: {response.Status}");
                Console.WriteLine($"Message: {response.Message}");
                if (response.Transaction != null)
                {
                    Console.WriteLine($"Transaction ID: {response.Transaction.TransactionId}");
                    Console.WriteLine($"Product ID: {response.Transaction.ProductId}");
                    Console.WriteLine($"Quantity: {response.Transaction.Quantity}");
                    Console.WriteLine($"Price: {response.Transaction.Price}");
                    Console.WriteLine($"Currency: {response.Transaction.Currency}");
                    Console.WriteLine($"Purchase Date: {response.Transaction.PurchaseDate.ToDateTime()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling MakePurchase: {ex.Message}");
            }
        }

        private static async Task GetPurchaseHistoryExample(IAPService.IAPServiceClient client)
        {
            Console.WriteLine("Calling GetPurchaseHistory...");

            var request = new PurchaseHistoryRequest
            {
                UserId = "user123"
            };

            try
            {
                var response = await client.GetPurchaseHistoryAsync(request);

                Console.WriteLine("GetPurchaseHistory Response:");
                foreach (var transaction in response.Transactions)
                {
                    Console.WriteLine($"Transaction ID: {transaction.TransactionId}");
                    Console.WriteLine($"Product ID: {transaction.ProductId}");
                    Console.WriteLine($"Quantity: {transaction.Quantity}");
                    Console.WriteLine($"Price: {transaction.Price}");
                    Console.WriteLine($"Currency: {transaction.Currency}");
                    Console.WriteLine($"Purchase Date: {transaction.PurchaseDate.ToDateTime()}");
                    Console.WriteLine("---------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling GetPurchaseHistory: {ex.Message}");
            }
        }
    }
}
