using System;
using System.Linq;
using CrazyFramework.App.BusinessServices;
using CrazyFramework.Infrastructure.AspNetIdentityRepos;
using CrazyFramework.API.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authentication;

namespace CrazyFramework.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection ConfigWebApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHealthChecks()
				.AddDbContextCheck<ApplicationDbContext>();

			services.AddScoped<ICurrentRequestContext, CurrentRequestContext>();

			services.AddControllersWithViews()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICurrentRequestContext>())
				.AddNewtonsoftJson();

			services.AddRazorPages();

			// Customise default API behaviour
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			// Register the Swagger services
			services.AddSwaggerDocument(configure =>
			{
				configure.Title = "Crazy API";
				configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
				{
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = OpenApiSecurityApiKeyLocation.Header,
					Description = "Type into the textbox: Bearer {your JWT token}."
				});

				configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
			});

			services.AddAuthentication()
				.AddIdentityServerJwt();

			return services;
		}
	}
}