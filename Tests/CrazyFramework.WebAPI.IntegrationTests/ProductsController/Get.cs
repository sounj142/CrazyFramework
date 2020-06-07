using CrazyFramework.App.Dtos.Products;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.WebAPI.IntegrationTests.ProductsController
{
	public class Get : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactory<Startup> _factory;

		public Get(CustomWebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task Get_ShouldReturnProductsFromDataseCorrectly()
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync(TestConstants.ProductApiBaseUrl);
			var products = await response.DeserializeResponseContent<ProductDto[]>();

			// Assert
			response.EnsureSuccessStatusCode(); // Status Code 200-299
			var productsDb = await Helper.GetAllProductsFromDatabase();

			Assert.Equal(products.Length, productsDb.Length);

			for (var i = 0; i < products.Length; i++)
			{
				Assert.Equal(products[i].Id, productsDb[i].Id);
				Assert.Equal(products[i].Name, productsDb[i].Name);
				Assert.Equal(products[i].Price, productsDb[i].Price);
			}
		}
	}
}