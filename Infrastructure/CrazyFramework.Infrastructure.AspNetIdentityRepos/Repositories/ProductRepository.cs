using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Mapper;
using System.Threading.Tasks;
using CrazyFramework.App.Common.Exceptions;
using Microsoft.Extensions.Logging;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Products;
using CrazyFramework.App.Entities.Products;
using CrazyFramework.App.Infrastructure.Repos;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Repositories
{
	internal class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<ProductRepository> _logger;
		private DbSet<ProductDAO> ProductsDbSet => _dbContext.Set<ProductDAO>();

		public ProductRepository(ApplicationDbContext dbContext, ILogger<ProductRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<Product> GetById(Guid id)
		{
			var product = await ProductsDbSet.AsNoTracking()
				.Select(p => new
				{
					p.Id,
					p.Name,
					p.Price
				})
				.FirstOrDefaultAsync(p => p.Id == id);

			return product == null ? null : new Product(id: product.Id, name: product.Name, price: product.Price);
		}

		public async Task<IList<Product>> GetAll()
		{
			var products = (await ProductsDbSet.AsNoTracking()
				.Select(p => new
				{
					p.Id,
					p.Name,
					p.Price
				})
				.OrderBy(p => p.Name)
				.ToListAsync())
				.Select(p => new Product(id: p.Id, name: p.Name, price: p.Price))
				.ToList();

			return products;
		}

		public async Task Create(Product product)
		{
			var productDAO = product.MapToDAO();
			ProductsDbSet.Add(productDAO);

			_logger.LogInformation("Creating product {@ProductId}, {@ProductName}", product.Id, product.Name);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			var productDAO = await ProductsDbSet
				.FirstOrDefaultAsync(p => p.Id == product.Id);

			_logger.LogInformation("Updating product {@ProductId}, {@ProductName}", product.Id, product.Name);

			if (productDAO == null)
			{
				_logger.LogWarning("Update rejected. Product {@ProductId} was not found.", product.Id);
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
				_logger.LogWarning("Deletion rejected. Product {@ProductId} was not found.", id);
				throw new NotFoundException("Product", id);
			}

			_logger.LogInformation("Deleting product {@ProductId}, {@ProductName}", productDAO.Id, productDAO.Name);

			ProductsDbSet.Remove(productDAO);
			await _dbContext.SaveChangesAsync();
		}
	}
}