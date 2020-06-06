using CrazyFramework.Core.Repositories;
using CrazyFramework.Repos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrazyFramework.Repos.IntegrationTests")]
[assembly: InternalsVisibleTo("CrazyFramework.WebAPI.IntegrationTests")]

namespace CrazyFramework.Repos
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