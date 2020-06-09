using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CrazyFramework.Infrastructure.Repos;
using CrazyFramework.Infrastructure.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace CrazyFramework.WebAPI.IntegrationTests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				// Remove the app's ApplicationDbContext registration.
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType ==
						typeof(DbContextOptions<ApplicationDbContext>));

				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				// Add ApplicationDbContext using a test database for testing
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
						TestConstants.DbConnection,
						b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
					);

				services.AddAuthentication("Test")
						.AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
							"Test", options => { });

				// Build the service provider.
				var serviceProvider = services.BuildServiceProvider();

				// wait for migration data
				MigrationRepository.MigrateDatabase(serviceProvider).Wait();
				MigrationRepository.SeedInitialData(serviceProvider).Wait();

				// seed some sample data
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