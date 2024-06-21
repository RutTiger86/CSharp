namespace CSharp.RestAPI.Repository.Models
{
    public class ProductCategoryInfo : CategoryInfo
    {
        public List<ProductStock> ChildCategory { get; set; }
    }
}
