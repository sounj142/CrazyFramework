using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CrazyFramework.API.Areas.Identity.IdentityHostingStartup))]

namespace CrazyFramework.API.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
				services.AddScoped<IIdentityLogic, IdentityLogic>();
			});
		}
	}
}