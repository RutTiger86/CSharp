using CSharp.RestAPI.Repository.DataContext;
using CSharp.RestAPI.Repository.Models;
using CSharp.RestAPI.Repository.Models.Requests;
using System.Data;

namespace CSharp.RestAPI.Repository.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryInfo> SelectProductCategoryInfos();

        long InsertCategory(AddCategoryRequest addCategory);
        bool CategoryExists(long categoryId);
    }

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(ILogger<CategoryRepository> logger, TemporaryDataContext dataContext)
        {
            this.log = logger;
            this.dataContext = dataContext;
        }

        public List<CategoryInfo> SelectProductCategoryInfos()
        {

            DataTable categoryTable = dataContext.dataSet.Tables["ProductCategory"];

            var categories = categoryTable.AsEnumerable()
                .Where(row => row.IsNull("ParentCategoryId"))
                .Select(row => new CategoryInfo
                {
                    CategoryId = row.Field<long>("CategoryId"),
                    CategoryName = row.Field<string>("CategoryName"),
                    ChildCategory = GetChildCategories(row.Field<long>("CategoryId"))
                }).ToList();

            return categories;
        }

        private List<CategoryInfo> GetChildCategories(long parentId)
        {
            DataTable categoryTable = dataContext.dataSet.Tables["ProductCategory"];
            var childCategories = categoryTable.AsEnumerable()
                .Where(row => row.Field<long?>("ParentCategoryId") == parentId)
                .Select(row => new CategoryInfo
                {
                    CategoryId = row.Field<long>("CategoryId"),
                    CategoryName = row.Field<string>("CategoryName"),
                    ChildCategory = GetChildCategories(row.Field<long>("CategoryId"))
                }).ToList();

            return childCategories;
        }

        public long InsertCategory(AddCategoryRequest addCategory)
        {
            DataTable categoryTable = dataContext.dataSet.Tables["ProductCategory"];

            // 새로운 카테고리 ID 생성
            long newCategoryId = categoryTable.AsEnumerable().Max(row => row.Field<long>("CategoryId")) + 1;

            // 새로운 카테고리 추가
            DataRow newCategoryRow = categoryTable.NewRow();
            newCategoryRow["CategoryId"] = newCategoryId;
            newCategoryRow["CategoryName"] = addCategory.CategoryName;
            newCategoryRow["ParentCategoryId"] = addCategory.ParentCategoryId.HasValue ? addCategory.ParentCategoryId.Value : (object)DBNull.Value;

            categoryTable.Rows.Add(newCategoryRow);

            return newCategoryId;
        }

        public bool CategoryExists(long categoryId)
        {
            DataTable categoryTable = dataContext.dataSet.Tables["ProductCategory"];
            return categoryTable.AsEnumerable().Any(row => row.Field<long>("CategoryId") == categoryId);
        }
    }
}
