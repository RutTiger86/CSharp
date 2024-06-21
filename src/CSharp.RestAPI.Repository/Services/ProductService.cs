using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Repositories;

namespace CSharp.RestAPI.Repository.Services
{
    public interface IProductService
    {
        BaseResponse<ProductOverview> GetProductOverview();
    }

    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(ILogger<ProductService> logger, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            this.log = logger;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public BaseResponse<ProductOverview> GetProductOverview()
        {
            try
            {
                return new BaseResponse<ProductOverview>()
                {
                    Result = true,
                    ErrorCode = (int)ErrorCode.Success,
                    ErrorMessage = ErrorCode.Success.ToString(),
                    Data = new ProductOverview()
                    {
                        Categories = categoryRepository.SelectProductCategoryInfos(),
                        Products = productRepository.SelectProductInfos()
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductOverview>()
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.SystemException,
                    ErrorMessage = ex.ToString(),
                    Data = null
                };
            }
        }

    }
}
