﻿using CrazyFramework.Infrastructure.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.Infrastructure.Repos.IntegrationTests
{
	public class ApplicationDbContextTests : TestsBase
	{
		[Fact]
		public async Task SaveChangesAsync_WhenCreateProduct_ShouldAutomaticlySetCreatedDateAndCreatedBy()
		{
			// Arrange
			_dateTimeMock.Setup(m => m.Now)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.GetCurrentUserName())
				.Returns(TestConstants.CurrentUserName);

			var product = new ProductDAO
			{
				Id = Guid.NewGuid(),
				Name = "Toyota",
				Price = 100.2M
			};

			var productsDbSet = _dbContext.Set<ProductDAO>();

			// Act
			productsDbSet.Add(product);
			await _dbContext.SaveChangesAsync();

			// Assert
			var products = await productsDbSet.ToListAsync();
			Assert.Single(products);

			var responseProduct = products[0];
			Assert.Equal(product.Id, responseProduct.Id);
			Assert.Equal(product.Name, responseProduct.Name);
			Assert.Equal(product.Price, responseProduct.Price);

			Assert.Equal(responseProduct.CreatedBy, TestConstants.CurrentUserName);
			Assert.Equal(responseProduct.CreatedDate, TestConstants.FixUtcNow);

			Assert.Null(responseProduct.LastModifiedBy);
			Assert.Null(responseProduct.LastModifyDate);
		}

		[Fact]
		public async Task SaveChangesAsync_WhenUpdateProduct_ShouldAutomaticlySetUpdatedDateAndUpdatedByButCreatedDateAndCreatedByShoudNotChange()
		{
			const string newProductName = "New name";
			var lastUpdatedDate = DateTimeOffset.UtcNow;
			var lastUpdatedBy = "aax1bb";

			// Arrange
			_dateTimeMock.Setup(m => m.Now)
				.Returns(TestConstants.FixUtcNow);
			_currentRequestContextMock.Setup(m => m.GetCurrentUserName())
				.Returns(TestConstants.CurrentUserName);

			var productId = Guid.NewGuid();
			var productToCreate = new ProductDAO
			{
				Id = productId,
				Name = "Toyota",
				Price = 100.2M
			};

			var productsDbSet = _dbContext.Set<ProductDAO>();

			productsDbSet.Add(productToCreate);
			await _dbContext.SaveChangesAsync();

			_dateTimeMock.Setup(m => m.Now)
				.Returns(lastUpdatedDate);
			_currentRequestContextMock.Setup(m => m.GetCurrentUserName())
				.Returns(lastUpdatedBy);

			var productToUpdate = await productsDbSet.FirstAsync(p => p.Id == productId);
			productToUpdate.Name = newProductName;

			// Act
			await _dbContext.SaveChangesAsync();

			var product = await productsDbSet.FirstAsync(p => p.Id == productId);

			// Assert
			var products = await productsDbSet.ToListAsync();
			Assert.Single(products);
			var responseProduct = products[0];

			Assert.Equal(product.Id, productId);
			Assert.Equal(product.Name, newProductName);
			Assert.Equal(product.Price, responseProduct.Price);

			Assert.Equal(responseProduct.CreatedBy, TestConstants.CurrentUserName);
			Assert.Equal(responseProduct.CreatedDate, TestConstants.FixUtcNow);

			Assert.Equal(responseProduct.LastModifiedBy, lastUpdatedBy);
			Assert.Equal(responseProduct.LastModifyDate, lastUpdatedDate);
		}
	}
}