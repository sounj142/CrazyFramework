using CrazyFramework.Core.Repositories;
using CrazyFramework.Repos.Models.Products;
using CrazyFramework.Repos.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.Repos.IntegrationTests.Repositories
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
			_dateTimeMock.Setup(m => m.UtcNow)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.UserId)
				.Returns(TestConstants.CurrentUserId);

			var dbProducts = TestConstants.GetProducts();

			await ApplicationDbContextMockFactory.SeedProductsData(_dbContext, dbProducts);

			var productRepository = new ProductRepository(_dbContext, _loggerMock.Object);

			// Act
			var products = await productRepository.GetAll();

			// Assert
			Assert.Equal(2, products.Count);
			Assert.True(dbProducts[0].Name == products[0].Name && dbProducts[0].Price == products[0].Price && dbProducts[0].Id == products[0].Id);
			Assert.True(dbProducts[1].Name == products[1].Name && dbProducts[1].Price == products[1].Price && dbProducts[1].Id == products[1].Id);
		}
	}
}