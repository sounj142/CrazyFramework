using CrazyFramework.App.BusinessHandlers.Products.Commands.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.WebAPI.IntegrationTests.ProductsController
{
	public class Update : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactory<Startup> _factory;

		public Update(CustomWebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task Update_WhenDataValid_ShouldReturnSuccessCode()
		{
			// Arrange
			var productsDbOld = await Helper.GetAllProductsFromDatabase();
			if (productsDbOld.Length == 0)
			{
				throw new Exception("Don't have data to do this test");
			}

			var random = new Random();
			var productDaoToUpdate = productsDbOld[0];
			var updateProductData = new UpdateProductCommand
			{
				Id = productDaoToUpdate.Id,
				Name = random.Next(1, 1000000).ToString(),
				Price = random.Next(1, 1000000),
			};

			var client = _factory.CreateClient();

			// Act
			var response = await client.PutAsync($"{TestConstants.ProductApiBaseUrl}/{updateProductData.Id}", updateProductData.SerializeToStringContent());

			// Assert
			response.EnsureSuccessStatusCode(); // Status Code 200-299

			var productsDbNew = await Helper.GetAllProductsFromDatabase();
			Assert.Equal(productsDbNew.Length, productsDbOld.Length);

			var productOld = productsDbOld.First(p => p.Id == updateProductData.Id);
			Helper.ConfirmProductUpdated(
				productsDbNew: productsDbNew,
				id: updateProductData.Id,
				name: updateProductData.Name,
				price: updateProductData.Price,
				createdBy: productOld.CreatedBy,
				createdDate: productOld.CreatedDate,
				lastModifyDate: productOld.LastModifyDate);

			Helper.ConfirmProductsListNotChange(
				productsDbNew: productsDbNew,
				productsDbOld: productsDbOld,
				ignoreIds: updateProductData.Id);
		}

		[Fact]
		public async Task Update_WhenDataInValid_ShouldReturnBadRequestStatusCodeAndErrorObject()
		{
			// Arrange
			var updateProductData = new UpdateProductCommand
			{
				Id = Guid.NewGuid(),
				Name = "",
				Price = 0
			};

			var client = _factory.CreateClient();

			// Act
			var response = await client.PutAsync($"{TestConstants.ProductApiBaseUrl}/{updateProductData.Id}", updateProductData.SerializeToStringContent());

			var errors = await response.DeserializeResponseContent<Dictionary<string, string[]>>();

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.NotEmpty(errors["Name"][0]);
			Assert.NotEmpty(errors["Price"][0]);
		}
	}
}