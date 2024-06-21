namespace CSharp.RestAPI.Repository.Models
{
    public class ProductDetail
    {
        public long ProductDetailId { get; set; }
        public long ProductId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
    }

}
