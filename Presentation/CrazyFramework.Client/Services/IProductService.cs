using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyFramework.Dtos.Products;

namespace CrazyFramework.Client.Services
{
	public interface IProductService
	{
		Task<IList<ProductDto>> GetProducts();
	}
}