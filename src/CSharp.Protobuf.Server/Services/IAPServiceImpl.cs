using CSharp.Protobuf.Purchase;
using CSharp.Protobuf.Service;
using CSharp.Protobuf.Transaction;
using Grpc.Core;

namespace CSharp.Protobuf.Server.Services
{
    public class IAPServiceImpl : IAPService.IAPServiceBase
    {
        private readonly List<TransactionInfo> _transactionStore = new();

        public override Task<PurchaseResponse> MakePurchase(PurchaseRequest request, ServerCallContext context)
        {
            Console.WriteLine($"[Received] MakePurchase request for User: {request.UserId}, Product: {request.ProductId}, Quantity: {request.Quantity} {Environment.NewLine}");

            if (request.Quantity <= 0)
            {
                return Task.FromResult(new PurchaseResponse
                {
                    Status = "FAILURE",
                    Message = "Quantity must be greater than 0."
                });
            }

            var transaction = new TransactionInfo
            {
                TransactionId = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Price = 9.99 * request.Quantity,
                Currency = "USD",
                PurchaseDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
            };

            _transactionStore.Add(transaction);

            Console.WriteLine($"[Send Response] TransactionId: {transaction.TransactionId}  Status : SUCCESS  {Environment.NewLine}");

            return Task.FromResult(new PurchaseResponse
            {
                Transaction = transaction,
                Status = "SUCCESS",
                Message = "Purchase completed successfully."
            });
        }

        public override Task<PurchaseHistoryResponse> GetPurchaseHistory(PurchaseHistoryRequest request, ServerCallContext context)
        {
            Console.WriteLine($"[Received] GetPurchaseHistory request for User: {request.UserId} {Environment.NewLine}");

            var userTransactions = _transactionStore.Where(t => t.UserId == request.UserId).ToList();

            var response = new PurchaseHistoryResponse();
            response.Transactions.AddRange(userTransactions);

            Console.WriteLine($"[Send Response] Transactions Count: {response.Transactions.Count} {Environment.NewLine}");

            return Task.FromResult(response);
        }
    }
}
