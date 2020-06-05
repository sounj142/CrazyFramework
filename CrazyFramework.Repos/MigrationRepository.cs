using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CrazyFramework.Repos
{
	public class MigrationRepository
	{
		public static async Task MigrateDatabase(IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				try
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
					await dbContext.Database.MigrateAsync();
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationRepository>>();
					logger.LogError(ex, "An error occurred while migrating database.");
				}
			}
		}

		public static async Task SeedInitialData(IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				try
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

					await ApplicationDbContextSeed.SeedAsync(dbContext);
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationRepository>>();
					logger.LogError(ex, "An error occurred while migrating database.");
				}
			}
		}
	}
}