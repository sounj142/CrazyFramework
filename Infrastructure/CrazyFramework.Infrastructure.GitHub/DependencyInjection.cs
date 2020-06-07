using CrazyFramework.App.Infrastructure.Github;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyFramework.Infrastructure.GitHub
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddGitHub(
			this IServiceCollection services,
			IConfiguration configuration
			)
		{
			services.AddScoped<IGitHubClient, GitHubClient>();

			return services;
		}
	}
}