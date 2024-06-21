namespace CSharp.RestAPI.Repository.Models
{
    public class ProductInfo
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long CategoryId { get; set; }
        public long ProductDetailId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }
        public List<ProductStock> Stocks { get; set; }
    }
}
