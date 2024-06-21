using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Repositories;
using CSharp.RestAPI.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CSharp.RestAPI.Repository.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        public ProductController(ILogger<ProductController> logger,IProductService productService)
        {
            this.log = logger;
            this.productService = productService;
        }

        [Route("ProductOverview")]
        [HttpGet]
        public BaseResponse<ProductOverview> GetProductOverview()
        {           
            return productService.GetProductOverview();
        }
    }
}
