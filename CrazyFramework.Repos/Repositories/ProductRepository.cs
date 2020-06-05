using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.Repos.Mapper;
using System.Threading.Tasks;
using CrazyFramework.Core.Common.Exceptions;

namespace CrazyFramework.Repos.Repositories
{
	internal class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		private IQueryable<Product> Products => _dbContext.Products
				.Select(p => new Product
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				});

		private IQueryable<Product> ProductsNoTracking => Products.AsNoTracking();

		public async Task<Product> GetById(Guid id)
		{
			var product = await ProductsNoTracking
				.FirstOrDefaultAsync(p => p.Id == id);

			return product;
		}

		public async Task<IList<Product>> GetAll()
		{
			var products = await ProductsNoTracking
				.ToListAsync();

			return products;
		}

		public async Task Create(Product product)
		{
			var productDAO = product.MapToDAO();
			_dbContext.Products.Add(productDAO);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			var productDAO = await _dbContext.Products
				.FirstOrDefaultAsync(p => p.Id == product.Id);

			if (productDAO == null)
			{
				throw new NotFoundException("Product", product.Id);
			}

			product.MapToDAO(productDAO);

			await _dbContext.SaveChangesAsync();
		}
	}
}