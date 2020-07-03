using CrazyFramework.Infrastructure.Repos.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.Infrastructure.Repos.IntegrationTests.Repositories
{
	public class ProductRepositoryTests : TestsBase
	{
		private readonly Mock<ILogger<ProductRepository>> _loggerMock;

		public ProductRepositoryTests()
		{
			_loggerMock = new Mock<ILogger<ProductRepository>>();
		}

		[Fact]
		public async Task GetAll_ShouldReturnProductList()
		{
			// Arrange
			_dateTimeMock.Setup(m => m.Now)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.GetCurrentUserName())
				.Returns(TestConstants.CurrentUserName);

			var dbProducts = TestConstants.GetProducts();

			await ApplicationDbContextMockFactory.SeedProductsData(_dbContext, dbProducts);

			var productRepository = new ProductRepository(_dbContext, _loggerMock.Object);

			// Act
			var products = await productRepository.GetAll();

			// Assert
			Assert.Equal(2, products.Count);

			Assert.Equal(dbProducts[0].Id, products[0].Id);
			Assert.Equal(dbProducts[0].Name, products[0].Name);
			Assert.Equal(dbProducts[0].Price, products[0].Price);

			Assert.Equal(dbProducts[1].Id, products[1].Id);
			Assert.Equal(dbProducts[1].Name, products[1].Name);
			Assert.Equal(dbProducts[1].Price, products[1].Price);
		}
	}
}