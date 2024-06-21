namespace CSharp.RestAPI.Repository.Models
{
    public class ProductCategory
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }
    }

}
