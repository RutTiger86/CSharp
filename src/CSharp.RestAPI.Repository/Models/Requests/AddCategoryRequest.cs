using System.ComponentModel.DataAnnotations;

namespace CSharp.RestAPI.Repository.Models.Requests
{
    public class AddCategoryRequest
    {
        [Required (AllowEmptyStrings =false)]
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
