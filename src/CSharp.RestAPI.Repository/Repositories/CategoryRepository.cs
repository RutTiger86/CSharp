using CSharp.RestAPI.Repository.DataContext;
using CSharp.RestAPI.Repository.Models;
using System.Data;

namespace CSharp.RestAPI.Repository.Repositories
{
    public interface ICategoryRepository
    {
        List<ProductCategoryInfo> SelectProductCategoryInfos();
    }

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(ILogger<CategoryRepository> logger, TemporaryDataContext dataContext)
        {
            this.log = logger;
            this.dataContext = dataContext;
        }

        public List<ProductCategoryInfo> SelectProductCategoryInfos()
        {
            var productCategoryInfos = from category in dataContext.dataSet.Tables["ProductCategory"].AsEnumerable()
                                       select new ProductCategoryInfo
                                       {
                                           CategoryId = category.Field<long>("CategoryId"),
                                           CategoryName = category.Field<string>("CategoryName"),
                                           ChildCategory = (from product in dataContext.dataSet.Tables["Product"].AsEnumerable()
                                                            join stock in dataContext.dataSet.Tables["ProductStock"].AsEnumerable()
                                                            on product.Field<long>("ProductId") equals stock.Field<long>("ProductId")
                                                            where product.Field<long>("CategoryId") == category.Field<long>("CategoryId")
                                                            select new ProductStock
                                                            {
                                                                ProductId = stock.Field<long>("ProductId"),
                                                                StockQuantity = stock.Field<int>("StockQuantity"),
                                                                Position = stock.Field<string>("Position")
                                                            }).ToList()
                                       };

            return productCategoryInfos.ToList();
        }
    }
}
