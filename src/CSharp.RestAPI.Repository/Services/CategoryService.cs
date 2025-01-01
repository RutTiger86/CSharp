using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Repositories;
using System.IO.Pipes;

namespace CSharp.RestAPI.Repository.Services
{
    public interface ICategoryService
    {
        BaseResponse<long> AddCategory(AddCategoryRequest addCategory);

        bool CategoryExists(long categoryId);
    }

    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository categoryRepository)
        {
            this.log = logger;
            this.categoryRepository = categoryRepository;
        }

        public BaseResponse<long> AddCategory(AddCategoryRequest addCategory)
        {
            try
            {
                return new BaseResponse<long>()
                {
                    Result = true,
                    ErrorCode = (int)ErrorCode.SUCCESS,
                    ErrorMessage = ErrorCode.SUCCESS.ToString(),
                    Data = categoryRepository.InsertCategory(addCategory)
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

        public bool CategoryExists(long categoryId)
        {
            try
            {
                return categoryRepository.CategoryExists(categoryId);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return false;
            }
        }
    }
}
