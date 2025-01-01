using AutoFixture;
using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Repositories;
using CSharp.RestAPI.Repository.Services;
using CSharp.RestAPI.RepositoryTests;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CSharp.RestAPI.CSharp.RestAPI.RepositoryTests.Services
{
    [TestClass()]
    public class ProductServiceTests : BaseTests
    {
        private ILogger<ProductService> logger;
        private Mock<IProductRepository> productRepository;
        private Mock<ICategoryRepository> categoryRepository;
        private Fixture fixture;
        private ProductService service;

        [TestInitialize]
        public void Setup()
        {
            logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            }).CreateLogger<ProductService>();

            fixture = new Fixture(); // 클래스 필드 초기화
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());


            productRepository = new Mock<IProductRepository>();
            categoryRepository = new Mock<ICategoryRepository>();
            service = new ProductService(logger, categoryRepository.Object, productRepository.Object);
        }

        [TestMethod("물품 정보 조회 테스트_정상조회")]
        public void GetProductOverviewTest()
        {
            // Arrange             

            var categoryInfos = new List<CategoryInfo>();

            for(int i = 0; i < 3; i++)
            {
                var parentCategory = fixture.Build<CategoryInfo>().Create();
                var childCategory = fixture.Build<CategoryInfo>().Create();
                parentCategory.ChildCategory = new List<CategoryInfo> { childCategory };

                categoryInfos.Add(parentCategory);
            }
            
            var productInfos = fixture.Build<List<ProductInfo>>().Create();

            var expectedResult = new BaseResponse<ProductOverview>()
            {
                Result = true,
                ErrorCode = (int)ErrorCode.SUCCESS,
                ErrorMessage = ErrorCode.SUCCESS.ToString(),
                Data = new ProductOverview()
                {
                    Categories = categoryInfos,
                    Products = productInfos
                }
            };

            categoryRepository.Setup(x => x.SelectProductCategoryInfos()).Returns(categoryInfos);
            productRepository.Setup(x => x.SelectProductInfos()).Returns(productInfos);

            // Act
            var serviceResult = service.GetProductOverview();

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.SelectProductCategoryInfos(), Times.Once);
            productRepository.Verify(x => x.SelectProductInfos(), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 정보 조회 테스트_예외발생")]
        public void GetProductOverviewTest_Exception()
        {
            // Arrange             
            Exception testException = new Exception("TestException");           

            var expectedResult = new BaseResponse<ProductOverview>()
            {
                Result = false,
                ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                ErrorMessage = testException.ToString(),
                Data = null
            };

            categoryRepository.Setup(x => x.SelectProductCategoryInfos()).Throws(testException);

            // Act
            var serviceResult = service.GetProductOverview();

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.SelectProductCategoryInfos(), Times.Once);
            productRepository.Verify(x => x.SelectProductInfos(), Times.Never);
            serviceResult.Should().BeEquivalentTo(expectedResult, x => x.Excluding(p=> p.ErrorMessage));
        }

        [TestMethod("물품 추가 테스트_정상추가")]
        public void AddProductTest()
        {
            // Arrange
            var productRequest = fixture.Build<AddProductRequest>().Create();

            var productId = 1L;

            var expectedResult = new BaseResponse<long>()
            {
                Result = true,
                ErrorCode = (int)ErrorCode.SUCCESS,
                ErrorMessage = ErrorCode.SUCCESS.ToString(),
                Data = productId
            };

            productRepository.Setup(x => x.InsertProduct(productRequest)).Returns(productId);

            // Act
            var serviceResult = service.AddProduct(productRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.InsertProduct(productRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 추가 테스트_예외발생")]
        public void AddProductTest_Exception()
        {
            // Arrange
            Exception testException = new Exception("TestException");

            var productRequest = fixture.Build<AddProductRequest>().Create();

            var expectedResult = new BaseResponse<long>()
            {
                Result = false,
                ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                ErrorMessage = testException.ToString(),
                Data = -1
            };

            productRepository.Setup(x => x.InsertProduct(productRequest)).Throws(testException);

            // Act
            var serviceResult = service.AddProduct(productRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.InsertProduct(productRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult, x => x.Excluding(p => p.ErrorMessage));
        }

        [TestMethod("물품 재고 추가 테스트_정상추가")]
        public void AddProductStockTest()
        {
            // Arrange
            var productRequest = fixture.Build<AddProductStockRequest>().Create();

            var productStockId = 1L;

            var expectedResult = new BaseResponse<long>()
            {
                Result = true,
                ErrorCode = (int)ErrorCode.SUCCESS,
                ErrorMessage = ErrorCode.SUCCESS.ToString(),
                Data = productStockId
            };

            productRepository.Setup(x => x.InsertProductStock(productRequest)).Returns(productStockId);

            // Act
            var serviceResult = service.AddProductStock(productRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.InsertProductStock(productRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }


        [TestMethod("물품 재고 추가 테스트_예외발생")]
        public void AddProductStockTest_Exception()
        {
            // Arrange
            Exception testException = new Exception("TestException");

            var productStockRequest = fixture.Build<AddProductStockRequest>().Create();

            var expectedResult = new BaseResponse<long>()
            {
                Result = false,
                ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                ErrorMessage = testException.ToString(),
                Data = -1
            };

            productRepository.Setup(x => x.InsertProductStock(productStockRequest)).Throws(testException);

            // Act
            var serviceResult = service.AddProductStock(productStockRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.InsertProductStock(productStockRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult, x => x.Excluding(p => p.ErrorMessage));
        }

        [TestMethod("물품 등록 체크 테스트")]
        public void ProductExistsTest()
        {
            // Arrange
            var productId = 1L;

            var expectedResult = true;

            productRepository.Setup(x => x.ProductExists(productId)).Returns(expectedResult);

            // Act
            var serviceResult = service.ProductExists(productId);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.ProductExists(productId), Times.Once);
            Assert.AreEqual(expectedResult, serviceResult);
        }

        [TestMethod("물품 등록 체크 테스트_예외발생")]
        public void ProductExistsTest_Exception()
        {
            // Arrange
            var productId = 1L;

            Exception testException = new Exception("TestException");

            var expectedResult = false;

            productRepository.Setup(x => x.ProductExists(productId)).Throws(testException);

            // Act
            var serviceResult = service.ProductExists(productId);

            // Assert
            Assert.IsNotNull(serviceResult);
            productRepository.Verify(x => x.ProductExists(productId), Times.Once);
            Assert.AreEqual(expectedResult, serviceResult);
        }

    }
}