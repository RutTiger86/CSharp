using CSharp.RestAPI.Repository.DataContext;
using CSharp.RestAPI.Repository.Models;
using CSharp.RestAPI.Repository.Models.Requests;
using System.Data;

namespace CSharp.RestAPI.Repository.Repositories
{
    public interface IProductRepository
    {
        List<ProductInfo> SelectProductInfos();
        long InsertProduct(AddProductRequest addProduct);

        long InsertProductStock(AddProductStockRequest addProductStock);

        bool ProductExists(long productId);
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
                                                 ProductStockId = stock.Field<long>("ProductStockId"),
                                                 ProductId = stock.Field<long>("ProductId"),
                                                 StockQuantity = stock.Field<int>("StockQuantity"),
                                                 Position = stock.Field<string>("Position")
                                             }).ToList()
                               };

            return productInfos.ToList();
        }

        public long InsertProduct(AddProductRequest addProduct)
        {
            DataTable productTable = dataContext.dataSet.Tables["Product"];
            DataTable productDetailTable = dataContext.dataSet.Tables["ProductDetail"];

            // 새로운 상품 ID 생성
            long newProductId = productTable.AsEnumerable().Any() ? productTable.AsEnumerable().Max(row => row.Field<long>("ProductId")) + 1 : 1;

            // 새로운 상품 추가
            DataRow newProductRow = productTable.NewRow();
            newProductRow["ProductId"] = newProductId;
            newProductRow["ProductName"] = addProduct.ProductName;
            newProductRow["CategoryId"] = addProduct.CategoryId;
            productTable.Rows.Add(newProductRow);

            // 새로운 상품 상세 정보 추가
            long newProductDetailId = productDetailTable.AsEnumerable().Any() ? productDetailTable.AsEnumerable().Max(row => row.Field<long>("ProductDetailId")) + 1 : 1;
            DataRow newProductDetailRow = productDetailTable.NewRow();
            newProductDetailRow["ProductDetailId"] = newProductDetailId;
            newProductDetailRow["ProductId"] = newProductId;
            newProductDetailRow["Description"] = addProduct.Description;
            newProductDetailRow["Manufacturer"] = addProduct.Manufacturer;
            productDetailTable.Rows.Add(newProductDetailRow);

            return newProductId;
        }

        public long InsertProductStock(AddProductStockRequest addProductStock)
        {
            DataTable productTable = dataContext.dataSet.Tables["Product"];
            DataTable productStockTable = dataContext.dataSet.Tables["ProductStock"];

            if (ProductExists(addProductStock.ProductId))
            {
                long newProductStockId = productStockTable.AsEnumerable().Any() ? productStockTable.AsEnumerable().Max(row => row.Field<long>("ProductStockId")) + 1 : 1;

                DataRow newProductStockRow = productStockTable.NewRow();
                newProductStockRow["ProductStockId"] = newProductStockId;
                newProductStockRow["ProductId"] = addProductStock.ProductId;
                newProductStockRow["Position"] = addProductStock.Position;
                newProductStockRow["StockQuantity"] = addProductStock.StockQuantity;
                productStockTable.Rows.Add(newProductStockRow);

                return newProductStockId;
            }else
            {
                log.LogError("Not Exists Product ID");
                return -1;
            }
        }

        public bool ProductExists(long productId)
        {
            DataTable productTable = dataContext.dataSet.Tables["Product"];
            return productTable.AsEnumerable().Any(row => row.Field<long>("ProductId") == productId);
        }

    }
}
