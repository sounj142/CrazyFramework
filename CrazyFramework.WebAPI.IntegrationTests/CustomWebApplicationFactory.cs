using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CrazyFramework.Repos;
using CrazyFramework.Repos.Models.Products;

namespace CrazyFramework.WebAPI.IntegrationTests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				// Build the service provider.
				var serviceProvider = services.BuildServiceProvider();

				// wait for migration data
				MigrationRepository.MigrateDatabase(serviceProvider).Wait();
				MigrationRepository.SeedInitialData(serviceProvider).Wait();

				PrepareSampleData(serviceProvider);
			})
			.UseEnvironment("Test");
		}

		private void PrepareSampleData(IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				var productsDbSet = dbContext.Set<ProductDAO>();
				if (!productsDbSet.Any())
				{
					productsDbSet.AddRange(new[]
						{
							new ProductDAO
							{
								Id = Guid.NewGuid(),
								Name = "Toyota",
								Price = 100.2M
							},
							new ProductDAO
							{
								Id = Guid.NewGuid(),
								Name = "Honda",
								Price = 38
							}
						});
					dbContext.SaveChanges();
				}
			}
		}
	}
}