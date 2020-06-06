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

		private DbSet<ProductDAO> ProductDAOs => _dbContext.Set<ProductDAO>();

		private IQueryable<Product> ProductsNoTracking => ProductDAOs.AsNoTracking()
				.Select(p => new Product
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				});

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
			ProductDAOs.Add(productDAO);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			var productDAO = await ProductDAOs
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
			var productDAO = await ProductDAOs
				.FirstOrDefaultAsync(p => p.Id == id);

			if (productDAO == null)
			{
				_logger.LogInformation($"Deletion rejected. Product ({id}) was not found.");
				throw new NotFoundException("Product", id);
			}

			ProductDAOs.Remove(productDAO);

			await _dbContext.SaveChangesAsync();
		}
	}
}