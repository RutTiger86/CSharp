namespace CSharp.RestAPI.Repository.Models
{
    public class ProductStock
    {
        public long ProductStockId { get; set; }
        public long ProductId { get; set; }
        public string Position { get; set; }
        public int StockQuantity { get; set; }
    }

}
