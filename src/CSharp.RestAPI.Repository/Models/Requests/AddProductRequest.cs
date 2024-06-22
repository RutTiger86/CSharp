using System.ComponentModel.DataAnnotations;

namespace CSharp.RestAPI.Repository.Models.Requests
{
    public class AddProductRequest
    {
        [Required (AllowEmptyStrings =false)]
        public string ProductName { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
    }
}
