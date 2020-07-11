using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyFramework.Dtos.Products;

namespace CrazyFramework.BlazoriseClient.Services
{
	public interface IProductService
	{
		Task<IList<ProductDto>> GetProducts();

		Task CreateProduct(CreateProductDto product);

		Task UpdateProduct(UpdateProductDto product);

		Task RemoveProduct(Guid productId);
	}
}