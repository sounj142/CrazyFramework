using System;
using System.Linq;
using CrazyFramework.App.BusinessServices;
using CrazyFramework.Infrastructure.Repos;
using CrazyFramework.WebAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CrazyFramework.WebAPI
{
	public static class DependencyInjection
	{
		public static IServiceCollection ConfigWebApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHealthChecks()
				.AddDbContextCheck<ApplicationDbContext>();

			services.AddScoped<ICurrentRequestContext, CurrentRequestContext>();

			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICurrentRequestContext>())
				.AddNewtonsoftJson();

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

			services.AddCors(options =>
			{
				options.AddPolicy("DevelopmentCors", builder => builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
				);
			});

			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = "https://localhost:44333";
					options.RequireHttpsMetadata = true;

					options.Audience = "CrazyWebApi";

					// set these values to enforce authentication check whether access token was expired
					options.TokenValidationParameters.ValidateLifetime = true;
					options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
				});
			services.AddAuthorization();

			return services;
		}
	}
}