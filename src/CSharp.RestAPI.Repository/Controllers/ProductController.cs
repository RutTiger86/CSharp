using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.RestAPI.Repository.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(ILogger<ProductController> logger,IProductService productService, ICategoryService categoryService)
        {
            this.log = logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [Route("ProductOverview")]
        [HttpGet]
        public BaseResponse<ProductOverview> GetProductOverview()
        {           
            return productService.GetProductOverview();
        }

        [HttpPost]
        public BaseResponse<long> AddProduct([FromBody] AddProductRequest request)
        {
            if (!categoryService.CategoryExists(request.CategoryId))
            {
                return new BaseResponse<long>
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.CATEGORY_NOT_EXISTS,
                    ErrorMessage = "category does not exist.",
                    Data = -1
                };
            }

            return productService.AddProduct(request);
        }

        [Route("ProductStock")]
        [HttpPost]
        public BaseResponse<long> AddProductStock([FromBody] AddProductStockRequest request)
        {
            if (!productService.ProductExists(request.ProductId))
            {
                return new BaseResponse<long>
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.PRODUCT_NOT_EXISTS,
                    ErrorMessage = "Product does not exist.",
                    Data = -1
                };
            }

            return productService.AddProductStock(request);
        }
    }
}
