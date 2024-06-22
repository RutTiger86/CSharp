using System.ComponentModel.DataAnnotations;

namespace CSharp.RestAPI.Repository.Models.Requests
{
    public class AddProductStockRequest
    {
        public long ProductId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Position { get; set; }
        public int StockQuantity { get; set; }
    }
}
