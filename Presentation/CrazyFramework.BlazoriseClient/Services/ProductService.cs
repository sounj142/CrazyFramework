using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CrazyFramework.Dtos.Products;

namespace CrazyFramework.BlazoriseClient.Services
{
	public class ProductService : ServiceBase, IProductService
	{
		public ProductService(HttpClient httpClient) : base(httpClient)
		{
		}

		public Task<IList<ProductDto>> GetProducts()
		{
			return Get<IList<ProductDto>>("api/products");
		}

		public Task CreateProduct(CreateProductDto product)
		{
			return Post("api/products", product);
		}

		public Task UpdateProduct(UpdateProductDto product)
		{
			return Put($"api/products/{product.Id}", product);
		}

		public Task RemoveProduct(Guid productId)
		{
			return Delete($"api/products/{productId}");
		}
	}
}