using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace CrazyFramework.BlazoriseClient
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			Startup.ConfigureServices(builder.Services, builder.HostEnvironment);

			var host = builder.Build();

			Startup.Configure(host);

			await host.RunAsync();
		}
	}
}