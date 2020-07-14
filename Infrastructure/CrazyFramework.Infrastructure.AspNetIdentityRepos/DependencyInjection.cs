using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Repositories;
using IdentityModel;
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
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddApiAuthorization<UserDAO, ApplicationDbContext>(options =>
				{
					const string identityResourceName = "openid";
					options.IdentityResources[identityResourceName].UserClaims.Add(JwtClaimTypes.Name);
					options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Name);
					options.IdentityResources[identityResourceName].UserClaims.Add(JwtClaimTypes.Role);
					options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Role);

					options.Clients.First().AccessTokenLifetime = 7200;
				});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(JwtClaimTypes.Role);

			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IJobTitleRepository, JobTitleRepository>();

			return services;
		}
	}
}