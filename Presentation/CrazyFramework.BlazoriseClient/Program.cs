using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Toolbelt.Blazor.Extensions.DependencyInjection;

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

			host.Services
				.UseBootstrapProviders()
				.UseFontAwesomeIcons();

			await host
				.UseLoadingBar()
				.RunAsync();
		}
	}
}