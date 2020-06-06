using CrazyFramework.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
			Assert.True(product.Name == responseProduct.Name && product.Price == responseProduct.Price && product.Id == responseProduct.Id);
			Assert.True(responseProduct.CreatedBy == TestConstants.CurrentUserId && responseProduct.CreatedDate == TestConstants.FixUtcNow);
			Assert.True(responseProduct.LastModifiedBy == null && responseProduct.LastModifyDate == null);
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
			Assert.True(product.Name == newProductName && product.Price == responseProduct.Price && product.Id == responseProduct.Id);
			Assert.True(responseProduct.CreatedBy == TestConstants.CurrentUserId && responseProduct.CreatedDate == TestConstants.FixUtcNow);
			Assert.True(responseProduct.LastModifiedBy == lastUpdatedBy && responseProduct.LastModifyDate == lastUpdatedDate);
		}
	}
}