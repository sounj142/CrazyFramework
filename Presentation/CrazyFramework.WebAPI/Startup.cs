using CrazyFramework.App;
using CrazyFramework.Infrastructure.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrazyFramework.Infrastructure.GitHub;
using CrazyFramework.WebAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Serilog;

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
			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

			services.AddApplication();
			services.AddRepositories(Configuration, "CrazyDb");
			services.AddGitHub(Configuration); // an example of Infrastructure from third-party
			services.ConfigWebApi(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors("DevelopmentCors");
			}

			app.UseSerilogRequestLogging();

			app.UseCustomExceptionHandler();
			app.UseHealthChecks("/health");
			app.UseHttpsRedirection();

			// Register the Swagger generator and the Swagger UI middlewares
			app.UseOpenApi();
			app.UseSwaggerUi3();

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