using AutoFixture;
using CSharp.RestAPI.Repository.Enums;
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
    public class CategoryServiceTests : BaseTests
    {
        private ILogger<CategoryService> logger;
        private Mock<ICategoryRepository> categoryRepository;
        private Fixture fixture;
        private CategoryService service;

        [TestInitialize]
        public void Setup()
        {
            logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            }).CreateLogger<CategoryService>();

            fixture = new Fixture(); // 클래스 필드 초기화
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            categoryRepository = new Mock<ICategoryRepository>();
            service = new CategoryService(logger, categoryRepository.Object);
        }

        [TestMethod("카테고리 추가 테스트_정상추가")]
        public void AddCategoryTest()
        {
            // Arrange
            var categoryRequest = fixture.Build<AddCategoryRequest>().Create();

            var categoryId = 1L;

            var expectedResult = new BaseResponse<long>()
            {
                Result = true,
                ErrorCode = (int)ErrorCode.SUCCESS,
                ErrorMessage = ErrorCode.SUCCESS.ToString(),
                Data = categoryId
            };

            categoryRepository.Setup(x => x.InsertCategory(categoryRequest)).Returns(categoryId);

            // Act
            var serviceResult = service.AddCategory(categoryRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.InsertCategory(categoryRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod("카테고리 추가 테스트_예외발생")]
        public void AddCategoryTest_Exception()
        {
            // Arrange
            var categoryRequest = fixture.Build<AddCategoryRequest>().Create();

            var testException = new Exception("Test Exception");
           
            var expectedResult = new BaseResponse<long>()
            {
                Result = false,
                ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                ErrorMessage = testException.ToString(),
                Data = -1
            };

            categoryRepository.Setup(x => x.InsertCategory(categoryRequest)).Throws(testException);

            // Act
            var serviceResult = service.AddCategory(categoryRequest);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.InsertCategory(categoryRequest), Times.Once);
            serviceResult.Should().BeEquivalentTo(expectedResult, x=> x.Excluding(p=> p.ErrorMessage));
        }

        [TestMethod("물품 등록 체크 테스트")]
        public void CategoryExistsTest()
        {
            // Arrange
            var categoryId = 1L;

            var expectedResult = true;

            categoryRepository.Setup(x => x.CategoryExists(categoryId)).Returns(expectedResult);

            // Act
            var serviceResult = service.CategoryExists(categoryId);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.CategoryExists(categoryId), Times.Once);
            Assert.AreEqual(expectedResult, serviceResult);
        }

        [TestMethod("물품 등록 체크 테스트_예외발생")]
        public void CategoryExistsTest_Exception()
        {
            // Arrange
            var categoryId = 1L;

            var testException = new Exception("Test Exception");

            var expectedResult = false;

            categoryRepository.Setup(x => x.CategoryExists(categoryId)).Throws(testException);

            // Act
            var serviceResult = service.CategoryExists(categoryId);

            // Assert
            Assert.IsNotNull(serviceResult);
            categoryRepository.Verify(x => x.CategoryExists(categoryId), Times.Once);
            Assert.AreEqual(expectedResult, serviceResult);
        }
    }
}