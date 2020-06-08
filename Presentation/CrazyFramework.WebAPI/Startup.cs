using CrazyFramework.App;
using CrazyFramework.Infrastructure.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrazyFramework.Infrastructure.GitHub;
using System;
using CrazyFramework.WebAPI.Helpers;

namespace CrazyFramework.WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplication();
			services.AddRepositories(Configuration, "CrazyDb");
			services.AddGitHub(Configuration); // an example of Infrastructure from third-party
			services.ConfigWebApi(Configuration);

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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors("DevelopmentCors");
			}

			app.UseCustomExceptionHandler();
			app.UseHealthChecks("/health");
			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crazy API V1");
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers().RequireAuthorization();
			});
		}
	}
}