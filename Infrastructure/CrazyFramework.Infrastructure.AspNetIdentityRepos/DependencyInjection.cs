using System.Linq;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Repositories;
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
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.SignIn.RequireConfirmedEmail = false;
					options.SignIn.RequireConfirmedPhoneNumber = false;

					options.Lockout.MaxFailedAccessAttempts = 10;

					options.User.RequireUniqueEmail = true;

					options.Password.RequireDigit = false;
					options.Password.RequiredUniqueChars = 0;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
				})
				//.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddApiAuthorization<UserDAO, ApplicationDbContext>(options =>
				{
					//options.IdentityResources["openid"].UserClaims.Add("name");
					//options.ApiResources.Single().UserClaims.Add("name");
					//options.IdentityResources["openid"].UserClaims.Add("role");
					//options.ApiResources.Single().UserClaims.Add("role");

					options.Clients.First().AccessTokenLifetime = 7200;
				});
			//.AddApiAuthorization<UserDAO, ApplicationDbContext>()

			//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

			//services.Configure<IdentityOptions>(options =>
			//	options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}