using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace CrazyFramework.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			Startup.ConfigureServices(builder.Services, builder.HostEnvironment);

			await builder
				.Build()
				.UseLoadingBar()
				.RunAsync();
		}
	}
}