using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.Repos.Mapper;
using System.Threading.Tasks;
using CrazyFramework.Core.Common.Exceptions;
using Microsoft.Extensions.Logging;
using CrazyFramework.Repos.Models.Products;

namespace CrazyFramework.Repos.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<ProductRepository> _logger;

		public ProductRepository(ApplicationDbContext dbContext, ILogger<ProductRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		private DbSet<ProductDAO> ProductsDbSet => _dbContext.Set<ProductDAO>();

		private IQueryable<Product> ProductsNoTrackingDbSet => ProductsDbSet.AsNoTracking()
				.Select(p => new Product
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				});

		public async Task<Product> GetById(Guid id)
		{
			var product = await ProductsNoTrackingDbSet
				.FirstOrDefaultAsync(p => p.Id == id);

			return product;
		}

		public async Task<IList<Product>> GetAll()
		{
			var products = await ProductsNoTrackingDbSet
				.OrderBy(p => p.Name)
				.ToListAsync();

			return products;
		}

		public async Task Create(Product product)
		{
			var productDAO = product.MapToDAO();
			ProductsDbSet.Add(productDAO);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			var productDAO = await ProductsDbSet
				.FirstOrDefaultAsync(p => p.Id == product.Id);

			if (productDAO == null)
			{
				_logger.LogInformation($"Update rejected. Product ({product.Id}) was not found.");
				throw new NotFoundException("Product", product.Id);
			}

			product.MapToDAO(productDAO);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			var productDAO = await ProductsDbSet
				.FirstOrDefaultAsync(p => p.Id == id);

			if (productDAO == null)
			{
				_logger.LogInformation($"Deletion rejected. Product ({id}) was not found.");
				throw new NotFoundException("Product", id);
			}

			ProductsDbSet.Remove(productDAO);

			await _dbContext.SaveChangesAsync();
		}
	}
}