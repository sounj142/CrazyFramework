using System;
using System.Net.Http;
using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using CrazyFramework.Client.Providers;
using CrazyFramework.Client.Services;
using FluentValidation;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CrazyFramework.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			//builder.Services
			//	.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
			//		.EnableIntercept(sp)
			//	);

			builder.Services.AddHttpClient("CrazyFramework.API", (sp, client) =>
			{
				client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
				client.EnableIntercept(sp);
			})
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CrazyFramework.API"));

			builder.Services.AddTransient<IProductService, ProductService>();

			builder.Services.AddApiAuthorization();

			//builder.Services.AddApiAuthorization()
			//	.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

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

			builder.Services.AddValidatorsFromAssemblyContaining<Program>();

			await builder
				.Build()
				.UseLoadingBar()
				.RunAsync();
		}
	}
}