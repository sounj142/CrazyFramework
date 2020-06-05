using CrazyFramework.Core.Services;
using CrazyFramework.WebAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CrazyFramework.WebAPI
{
	public static class DependencyInjection
	{
		public static IServiceCollection ConfigWebApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ICurrentRequestContext, CurrentRequestContext>();

			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICurrentRequestContext>())
				.AddNewtonsoftJson();

			// Customise default API behaviour
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});

			return services;
		}
	}
}