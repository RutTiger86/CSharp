using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CSharp.RestAPI.Repository.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            this.log = logger;
            this.categoryService = categoryService;
        }

        [HttpPost]
        public BaseResponse<long> AddCategory([FromBody] AddCategoryRequest request)
        {
            if (request.ParentCategoryId.HasValue && !categoryService.CategoryExists(request.ParentCategoryId.Value))
            {
                return new BaseResponse<long>
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.CategoryNotExists,
                    ErrorMessage = "Parent category does not exist.",
                    Data = -1
                };
            }

            return categoryService.AddCategory(request);
        }
    }
}
