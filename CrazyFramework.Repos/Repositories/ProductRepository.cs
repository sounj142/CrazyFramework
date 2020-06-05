using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.Repos.Models.Products;
using CrazyFramework.Repos.Mapper;
using System.Threading.Tasks;

namespace CrazyFramework.Repos.Repositories
{
	internal class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Create(Product product)
		{
			var productDAO = product.MapToDAO();
			_dbContext.Entry(productDAO).State = EntityState.Added;

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			var productDAO = product.MapToDAO();
			// TODO: check what happends if product does not exist in db?
			_dbContext.Entry(productDAO).State = EntityState.Modified;

			await _dbContext.SaveChangesAsync();
		}

		public async Task<Product> GetById(Guid id)
		{
			var productDAO = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
			return productDAO.MapToDomain();
		}
	}
}