using System;
using System.Net.Http;
using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using CrazyFramework.Client.Providers;
using CrazyFramework.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace CrazyFramework.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services
				.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
					.EnableIntercept(sp)
				);
			builder.Services.AddTransient<IProductService, ProductService>();

			builder.Services.AddAuthorizationCore(config =>
			{
				//config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
				//config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
				//config.AddPolicy(Policies.IsReadOnly, Policies.IsUserPolicy());
				// config.AddPolicy(Policies.IsMyDomain, Policies.IsMyDomainPolicy());  Only works on the server end
			});

			builder.Services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();
			builder.Services.AddSingleton<AppState>();

			builder.Services.AddLoadingBar();
			builder.Services.AddMatToaster(config =>
			{
				config.Position = MatToastPosition.BottomRight;
				config.PreventDuplicates = true;
				config.NewestOnTop = true;
				config.ShowCloseButton = true;
				config.MaximumOpacity = 95;
				config.VisibleStateDuration = 3000;
			});

			await builder
				.Build()
				.UseLoadingBar()
				.RunAsync();
		}
	}
}