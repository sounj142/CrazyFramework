using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CrazyFramework.Dtos.Products;

namespace CrazyFramework.Client.Services
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
	}
}