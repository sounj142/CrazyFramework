using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CrazyFramework.API.Areas.Identity.IdentityHostingStartup))]

namespace CrazyFramework.API.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
			});
		}
	}
}