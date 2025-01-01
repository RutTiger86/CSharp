using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Repositories;

namespace CSharp.RestAPI.Repository.Services
{
    public interface IProductService
    {
        BaseResponse<ProductOverview> GetProductOverview();
        BaseResponse<long> AddProduct(AddProductRequest addProduct);

        BaseResponse<long> AddProductStock(AddProductStockRequest addProductStock);

        bool ProductExists(long productId);
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
                    ErrorCode = (int)ErrorCode.SUCCESS,
                    ErrorMessage = ErrorCode.SUCCESS.ToString(),
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
                    ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                    ErrorMessage = ex.ToString(),
                    Data = null
                };
            }
        }


        public BaseResponse<long> AddProduct(AddProductRequest addProduct)
        {
            try
            {
                return new BaseResponse<long>()
                {
                    Result = true,
                    ErrorCode = (int)ErrorCode.SUCCESS,
                    ErrorMessage = ErrorCode.SUCCESS.ToString(),
                    Data = productRepository.InsertProduct(addProduct)
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new BaseResponse<long>()
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                    ErrorMessage = ex.ToString(),
                    Data = -1
                };
            }
        }


        public BaseResponse<long> AddProductStock(AddProductStockRequest addProductStock)
        {
            try
            {
                return new BaseResponse<long>()
                {
                    Result = true,
                    ErrorCode = (int)ErrorCode.SUCCESS,
                    ErrorMessage = ErrorCode.SUCCESS.ToString(),
                    Data = productRepository.InsertProductStock(addProductStock)
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new BaseResponse<long>()
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                    ErrorMessage = ex.ToString(),
                    Data = -1
                };
            }
        }

        public bool ProductExists(long productId)
        {
            try
            {
                return productRepository.ProductExists(productId);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return false;
            }
        }
    }
}
