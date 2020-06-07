using CrazyFramework.App.Entities.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyFramework.App.Repositories
{
	public interface IProductRepository
	{
		Task<Product> GetById(Guid id);

		Task<IList<Product>> GetAll();

		Task Create(Product product);

		Task Update(Product product);

		Task Delete(Guid id);
	}
}