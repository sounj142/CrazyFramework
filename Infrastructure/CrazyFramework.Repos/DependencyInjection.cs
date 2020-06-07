using CrazyFramework.App.Repositories;
using CrazyFramework.Infrastructure.Repos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrazyFramework.Infrastructure.Repos.IntegrationTests")]
[assembly: InternalsVisibleTo("CrazyFramework.WebAPI.IntegrationTests")]

namespace CrazyFramework.Infrastructure.Repos
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRepositories(
			this IServiceCollection services,
			IConfiguration configuration,
			string connectionStringName
			)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
						configuration.GetConnectionString(connectionStringName),
						b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
					);

			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}