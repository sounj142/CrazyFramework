using CrazyFramework.App.Common;
using CrazyFramework.BusinessServices.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyFramework.BusinessServices
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IDateTime, DateTimeService>();

			return services;
		}
	}
}