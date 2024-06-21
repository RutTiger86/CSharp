using System.Data;

namespace CSharp.RestAPI.Repository.DataContext
{
    public class TemporaryDataContext
    {
        public DataSet dataSet { get; set; }

        public TemporaryDataContext()
        {
            dataSet = new DataSet();
            CreateCategory();
            CreateProduct();
            CreateProductDetailInfo();
            CreateProductStock();
        }

        private void CreateCategory()
        {
            // 상품 카테고리 테이블 생성
            DataTable categoryTable = new DataTable("ProductCategory");
            categoryTable.Columns.Add("CategoryId", typeof(long));
            categoryTable.Columns.Add("CategoryName", typeof(string));
            categoryTable.Columns.Add("ParentCategoryId", typeof(long));

            // 메인 카테고리 추가
            categoryTable.Rows.Add(1L, "Electronics", DBNull.Value);
            categoryTable.Rows.Add(2L, "Clothing", DBNull.Value);
            categoryTable.Rows.Add(3L, "Home Appliances", DBNull.Value);

            // 자식 카테고리 추가
            categoryTable.Rows.Add(4L, "Phones", 1L);
            categoryTable.Rows.Add(5L, "Computers", 1L);
            categoryTable.Rows.Add(6L, "Cameras", 1L);
            categoryTable.Rows.Add(7L, "Men", 2L);
            categoryTable.Rows.Add(8L, "Women", 2L);
            categoryTable.Rows.Add(9L, "Kids", 2L);
            categoryTable.Rows.Add(10L, "Kitchen", 3L);
            categoryTable.Rows.Add(11L, "Living Room", 3L);
            categoryTable.Rows.Add(12L, "Bedroom", 3L);

            dataSet.Tables.Add(categoryTable);
        }

        private void CreateProduct()
        {
            // 상품 정보 테이블 생성
            DataTable productTable = new DataTable("Product");
            productTable.Columns.Add("ProductId", typeof(long));
            productTable.Columns.Add("ProductName", typeof(string));
            productTable.Columns.Add("CategoryId", typeof(long));

            // 임의의 상품 정보 추가 (자식 카테고리당 각 2개씩)
            for (int i = 4; i <= 12; i++)
            {
                productTable.Rows.Add(i * 10 + 1, "Product" + (i * 10 + 1), i);
                productTable.Rows.Add(i * 10 + 2, "Product" + (i * 10 + 2), i);
            }

            dataSet.Tables.Add(productTable);
        }

        private void CreateProductDetailInfo()
        {
            // 상품 상세 정보 테이블 생성
            DataTable productDetailTable = new DataTable("ProductDetail");
            productDetailTable.Columns.Add("ProductDetailId", typeof(long));
            productDetailTable.Columns.Add("ProductId", typeof(long));
            productDetailTable.Columns.Add("Description", typeof(string));
            productDetailTable.Columns.Add("Manufacturer", typeof(string));

            DataTable productTable = dataSet.Tables["Product"];

            long productDetailId = 1;
            // 임의의 상품 상세 정보 추가
            foreach (DataRow row in productTable.Rows)
            {
                long productId = (long)row["ProductId"];
                productDetailTable.Rows.Add(productDetailId++, productId, $"Description for product {productId}", $"Manufacturer {productId % 3 + 1}");
            }

            dataSet.Tables.Add(productDetailTable);
        }

        private void CreateProductStock()
        {
            // 상품 재고 테이블 생성
            DataTable productStockTable = new DataTable("ProductStock");
            productStockTable.Columns.Add("ProductStockId", typeof(long));
            productStockTable.Columns.Add("ProductId", typeof(long));
            productStockTable.Columns.Add("Position", typeof(string));
            productStockTable.Columns.Add("StockQuantity", typeof(int));

            DataTable productTable = dataSet.Tables["Product"];

            long productStockId = 1;
            // 임의의 상품 재고 정보 추가
            foreach (DataRow row in productTable.Rows)
            {
                long productId = (long)row["ProductId"];
                for (int j = 1; j <= 3; j++) // 각 Product에 대해 3개의 ProductStock 추가
                {
                    productStockTable.Rows.Add(productStockId++, productId, $"Position {productStockId}", new Random().Next(1, 100)); // 임의의 위치 추가
                }
            }

            dataSet.Tables.Add(productStockTable);
        }
    }
}
