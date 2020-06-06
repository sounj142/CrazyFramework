using CrazyFramework.Core.Repositories;
using CrazyFramework.Repos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrazyFramework.Repos.IntegrationTests")]

namespace CrazyFramework.Repos
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRepositories(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}