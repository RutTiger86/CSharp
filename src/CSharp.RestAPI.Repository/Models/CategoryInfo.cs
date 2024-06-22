namespace CSharp.RestAPI.Repository.Models
{
    public class CategoryInfo
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<CategoryInfo> ChildCategory { get; set; }
    }
}
