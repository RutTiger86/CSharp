namespace CSharp.RestAPI.Repository.Models.Responses
{
    public class ProductOverview
    {
        public List<ProductInfo> Products { get; set; }
        public List<ProductCategoryInfo> Categories { get; set; }
    }
}
