using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.RestAPI.Repository.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using CSharp.RestAPI.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSharp.RestAPI.RepositoryTests;
using Moq;
using CSharp.RestAPI.Repository.Models.Requests;
using CSharp.RestAPI.Repository.Models.Responses;
using CSharp.RestAPI.Repository.Enums;
using FluentAssertions;

namespace CSharp.RestAPI.Repository.Controllers.Tests
{
    [TestClass()]
    public class CategoryControllerTests : BaseTests
    {
        private ILogger<CategoryController> logger;
        private Mock<ICategoryService> categoryService;
        private Fixture fixture;
        private CategoryController controller;

        [TestInitialize]
        public void Setup()
        {
            logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            }).CreateLogger<CategoryController>();

            fixture = new Fixture(); // 클래스 필드 초기화

            categoryService = new Mock<ICategoryService>();
            controller = new CategoryController(logger, categoryService.Object);
        }

        [TestMethod("카테고리 추가 테스트_정상추가")]
        public void AddCategoryTest()
        {
            // Arrange
            var categorytRequest = fixture.Build<AddCategoryRequest>().Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.Success,
                ErrorMessage = ErrorCode.Success.ToString(),
                Data = 1
            };
            categoryService.Setup(x => x.CategoryExists(categorytRequest.ParentCategoryId.Value)).Returns(true);
            categoryService.Setup(x => x.AddCategory(categorytRequest)).Returns(expectedResult);

            // Act
            var serviceResult = controller.AddCategory(categorytRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryService.Verify(x => x.CategoryExists(categorytRequest.ParentCategoryId.Value), Times.Once);
            categoryService.Verify(x => x.AddCategory(categorytRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("카테고리 추가 테스트_메인카테고리 없음_정상추가")]
        public void AddCategoryTest_ParentCategoryNull()
        {
            // Arrange
            var categorytRequest = fixture.Build<AddCategoryRequest>()
                .Without(x => x.ParentCategoryId) //  ParentCategoryId 없음
                .Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.Success,
                ErrorMessage = ErrorCode.Success.ToString(),
                Data = 1
            };

            categoryService.Setup(x => x.AddCategory(categorytRequest)).Returns(expectedResult);

            // Act
            var serviceResult = controller.AddCategory(categorytRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryService.Verify(x => x.CategoryExists(It.IsAny<long>()), Times.Never);
            categoryService.Verify(x => x.AddCategory(categorytRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("카테고리 추가 테스트_부모카테고리미존재_정상추가")]
        public void AddCategoryTest_ParentCategoryNotExsits()
        {
            // Arrange
            var categorytRequest = fixture.Build<AddCategoryRequest>()
                .Create();

            var expectedResult = new BaseResponse<long>
            {
                Result = false,
                ErrorCode = (int)ErrorCode.CategoryNotExists,
                ErrorMessage = "Parent category does not exist.",
                Data = -1
            };

            categoryService.Setup(x => x.CategoryExists(categorytRequest.ParentCategoryId.Value)).Returns(false);

            // Act
            var serviceResult = controller.AddCategory(categorytRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryService.Verify(x => x.CategoryExists(It.IsAny<long>()), Times.Once);
            categoryService.Verify(x => x.AddCategory(categorytRequest), Times.Never);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}