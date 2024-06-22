using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.RestAPI.Repository.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CSharp.RestAPI.Repository.Services;
using Microsoft.Extensions.Logging;
using CSharp.RestAPI.RepositoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Microsoft.AspNetCore.Mvc;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Enums;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Kernel;
using CSharp.RestAPI.Repository.Models;

namespace CSharp.RestAPI.Repository.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests : BaseTests
    {
        private ILogger<ProductController> logger;
        private Mock<IProductService> productService;
        private Mock<ICategoryService> categoryService;
        private Fixture fixture;
        private ProductController controller;

        [TestInitialize]
        public void Setup()
        {
            logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            }).CreateLogger<ProductController>();

            fixture = new Fixture(); // 클래스 필드 초기화
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            productService = new Mock<IProductService>();
            categoryService = new Mock<ICategoryService>();
            controller = new ProductController(logger, productService.Object, categoryService.Object);
        }

        [TestMethod("물품 정보 조회 테스트_정상조회")]
        public void GetProductOverviewTest()
        {
            // Arrange             
            var productOverview = fixture.Build<ProductOverview>()
                                    .Without(x => x.Categories) // 초기에는 Categories를 비워둠
                                    .Create();

            var categoryInfos = new List<CategoryInfo>();

            for (int i = 0; i < 3; i++)
            {
                var parentCategory = fixture.Build<CategoryInfo>().Create();
                var childCategory = fixture.Build<CategoryInfo>().Create();
                parentCategory.ChildCategory = new List<CategoryInfo> { childCategory };

                categoryInfos.Add(parentCategory);
            }

            // ProductOverview 객체의 Categories에 첫 번째 CategoryInfo 객체를 추가
            productOverview.Categories = categoryInfos;

            var expectedResult = new BaseResponse<ProductOverview>()
            {
                Result = true,
                ErrorCode = (int)ErrorCode.Success,
                ErrorMessage = ErrorCode.Success.ToString(),
                Data = productOverview
            };

            productService.Setup(x => x.GetProductOverview()).Returns(expectedResult);

            // Act
            var serviceResult = controller.GetProductOverview();

            // Assert
            Assert.IsNotNull(serviceResult);
            productService.Verify(x => x.GetProductOverview(), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 추가 테스트_정상추가")]
        public void AddProductTest()
        {
            // Arrange
            var productRequest = fixture.Build<AddProductRequest>().Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.Success,
                ErrorMessage = ErrorCode.Success.ToString(),
                Data = 1
            };

            categoryService.Setup(x => x.CategoryExists(productRequest.CategoryId)).Returns(true);

            productService.Setup(x => x.AddProduct(productRequest)).Returns(expectedResult);

            // Act
            var serviceResult = controller.AddProduct(productRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryService.Verify(x => x.CategoryExists(productRequest.CategoryId), Times.Once);
            productService.Verify(x => x.AddProduct(productRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 추가 테스트_카테고리미존재")]
        public void AddProductTest_CategoryNotExists()
        {
            // Arrange
            var productRequest = fixture.Build<AddProductRequest>().Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = false,
                ErrorCode = (int)ErrorCode.CategoryNotExists,
                ErrorMessage = "category does not exist.",
                Data = -1
            };

            categoryService.Setup(x => x.CategoryExists(productRequest.CategoryId)).Returns(false);

            // Act
            var serviceResult = controller.AddProduct(productRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryService.Verify(x => x.CategoryExists(productRequest.CategoryId), Times.Once);
            productService.Verify(x => x.AddProduct(It.IsAny<AddProductRequest>()), Times.Never);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 재고 추가 테스트")]
        public void AddProductStockTest()
        {
            // Arrange
            var productStockRequest = fixture.Build<AddProductStockRequest>().Create(); 

            var expectedResult = new BaseResponse<long>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.Success,
                ErrorMessage = ErrorCode.Success.ToString(),
                Data = 1
            };

            productService.Setup(x => x.ProductExists(productStockRequest.ProductId)).Returns(true);

            productService.Setup(x => x.AddProductStock(productStockRequest)).Returns(expectedResult);

            // Act
            var serviceResult = controller.AddProductStock(productStockRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productService.Verify(x => x.ProductExists(productStockRequest.ProductId), Times.Once);
            productService.Verify(x => x.AddProductStock(productStockRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("물품 재고 추가 테스트_물품미존재")]
        public void AddProductStockTest_ProductNotExists()
        {
            // Arrange
            var productStockRequest = fixture.Build<AddProductStockRequest>().Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = false,
                ErrorCode = (int)ErrorCode.ProductNotExists,
                ErrorMessage = "Product does not exist.",
                Data = -1
            };

            productService.Setup(x => x.ProductExists(productStockRequest.ProductId)).Returns(false);

            // Act
            var serviceResult = controller.AddProductStock(productStockRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            productService.Verify(x => x.ProductExists(productStockRequest.ProductId), Times.Once);
            productService.Verify(x => x.AddProductStock(It.IsAny<AddProductStockRequest>()), Times.Never);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}