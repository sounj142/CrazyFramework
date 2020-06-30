using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos
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

			services.AddDefaultIdentity<UserDAO>(options =>
					options.SignIn.RequireConfirmedAccount = true
				)
				//.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddApiAuthorization<UserDAO, ApplicationDbContext>();
			//.AddApiAuthorization<UserDAO, ApplicationDbContext>(options =>
			//{
			//	options.IdentityResources["openid"].UserClaims.Add("name");
			//	options.ApiResources.Single().UserClaims.Add("name");
			//	options.IdentityResources["openid"].UserClaims.Add("role");
			//	options.ApiResources.Single().UserClaims.Add("role");
			//});

			//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

			//services.Configure<IdentityOptions>(options =>
			//	options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}