using CSharp.RestAPI.Repository.DataContext;
using CSharp.RestAPI.Repository.Models;
using System.Data;

namespace CSharp.RestAPI.Repository.Repositories
{
    public interface IProductRepository
    {
        List<ProductInfo> SelectProductInfos();
    }

    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(ILogger<ProductRepository> logger, TemporaryDataContext dataContext)
        {
            this.log = logger;
            this.dataContext = dataContext;
        }

        public List<ProductInfo> SelectProductInfos()
        {
            var productInfos = from product in dataContext.dataSet.Tables["Product"].AsEnumerable()
                               join category in dataContext.dataSet.Tables["ProductCategory"].AsEnumerable()
                               on product.Field<long>("CategoryId") equals category.Field<long>("CategoryId")
                               join detail in dataContext.dataSet.Tables["ProductDetail"].AsEnumerable()
                               on product.Field<long>("ProductId") equals detail.Field<long>("ProductId")
                               select new ProductInfo
                               {
                                   ProductId = product.Field<long>("ProductId"),
                                   ProductName = product.Field<string>("ProductName"),
                                   ProductDetailId = detail.Field<long>("ProductDetailId"),
                                   CategoryId = product.Field<long>("CategoryId"),
                                   Description = detail.Field<string>("Description"),
                                   Manufacturer = detail.Field<string>("Manufacturer"),
                                   CategoryName = category.Field<string>("CategoryName"),
                                   ParentCategoryId = category.Field<long?>("ParentCategoryId"),
                                   Stocks = (from stock in dataContext.dataSet.Tables["ProductStock"].AsEnumerable()
                                             where stock.Field<long>("ProductId") == product.Field<long>("ProductId")
                                             select new ProductStock
                                             {
                                                 ProductStockId= stock.Field<long>("ProductStockId"),
                                                 ProductId = stock.Field<long>("ProductId"),
                                                 StockQuantity = stock.Field<int>("StockQuantity"),
                                                 Position = stock.Field<string>("Position")
                                             }).ToList()
                               };

            return productInfos.ToList();
        }
    }
}
