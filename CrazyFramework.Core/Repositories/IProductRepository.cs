using CrazyFramework.Core.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Repositories
{
	public interface IProductRepository
	{
		Task Create(Product product);

		Task Update(Product product);

		Task<Product> GetById(Guid id);
	}
}