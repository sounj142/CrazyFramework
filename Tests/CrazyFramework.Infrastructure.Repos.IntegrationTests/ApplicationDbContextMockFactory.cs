using CrazyFramework.App.BusinessServices;
using CrazyFramework.App.Common;
using CrazyFramework.Infrastructure.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyFramework.Infrastructure.Repos.IntegrationTests
{
	internal static class ApplicationDbContextMockFactory
	{
		public static ApplicationDbContext Create(ICurrentRequestContext currentRequestContext, IDateTime dateTime)
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new ApplicationDbContext(
				options,
				currentRequestContext,
				dateTime);

			context.Database.EnsureCreated();

			return context;
		}

		public static async Task SeedProductsData(ApplicationDbContext context, IList<ProductDAO> products)
		{
			context.Set<ProductDAO>().AddRange(products);
			await context.SaveChangesAsync();
		}

		public static void Destroy(ApplicationDbContext context)
		{
			context.Database.EnsureDeleted();

			context.Dispose();
		}
	}
}