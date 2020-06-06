using CrazyFramework.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.Repos.IntegrationTests
{
	public class ApplicationDbContextTests : TestsBase
	{
		[Fact]
		public async Task SaveChangesAsync_WhenCreateProduct_ShouldAutomaticlySetCreatedDateAndCreatedBy()
		{
			// Arrange
			_dateTimeMock.Setup(m => m.UtcNow)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.UserId)
				.Returns(TestConstants.CurrentUserId);

			var product = new ProductDAO
			{
				Id = Guid.NewGuid(),
				Name = "Toyota",
				Price = 100.2M
			};

			var productDAOs = _dbContext.Set<ProductDAO>();

			// Act
			productDAOs.Add(product);
			await _dbContext.SaveChangesAsync();

			// Assert
			var products = await productDAOs.ToListAsync();
			Assert.Single(products);

			var responseProduct = products[0];
			Assert.Equal(product.Id, responseProduct.Id);
			Assert.Equal(product.Name, responseProduct.Name);
			Assert.Equal(product.Price, responseProduct.Price);

			Assert.Equal(responseProduct.CreatedBy, TestConstants.CurrentUserId);
			Assert.Equal(responseProduct.CreatedDate, TestConstants.FixUtcNow);

			Assert.Null(responseProduct.LastModifiedBy);
			Assert.Null(responseProduct.LastModifyDate);
		}

		[Fact]
		public async Task SaveChangesAsync_WhenUpdateProduct_ShouldAutomaticlySetUpdatedDateAndUpdatedByButCreatedDateAndCreatedByShoudNotChange()
		{
			const string newProductName = "New name";
			DateTime lastUpdatedDate = DateTime.UtcNow;
			Guid lastUpdatedBy = Guid.NewGuid();

			// Arrange
			_dateTimeMock.Setup(m => m.UtcNow)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.UserId)
				.Returns(TestConstants.CurrentUserId);

			var product = new ProductDAO
			{
				Id = Guid.NewGuid(),
				Name = "Toyota",
				Price = 100.2M
			};

			var productDAOs = _dbContext.Set<ProductDAO>();

			// Act
			productDAOs.Add(product);
			await _dbContext.SaveChangesAsync();

			_dateTimeMock.Setup(m => m.UtcNow)
				.Returns(lastUpdatedDate);
			_currentRequestContextMock.Setup(m => m.UserId)
				.Returns(lastUpdatedBy);

			product.Name = newProductName;
			await _dbContext.SaveChangesAsync();

			// Assert
			var products = await productDAOs.ToListAsync();
			Assert.Single(products);
			var responseProduct = products[0];

			Assert.Equal(product.Id, responseProduct.Id);
			Assert.Equal(product.Name, newProductName);
			Assert.Equal(product.Price, responseProduct.Price);

			Assert.Equal(responseProduct.CreatedBy, TestConstants.CurrentUserId);
			Assert.Equal(responseProduct.CreatedDate, TestConstants.FixUtcNow);

			Assert.Equal(responseProduct.LastModifiedBy, lastUpdatedBy);
			Assert.Equal(responseProduct.LastModifyDate, lastUpdatedDate);
		}
	}
}