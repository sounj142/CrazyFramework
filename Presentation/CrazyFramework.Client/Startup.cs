using System;
using System.Net.Http;
using CrazyFramework.Client.Models;
using CrazyFramework.Client.Services;
using FluentValidation;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CrazyFramework.Client.Providers;

namespace CrazyFramework.Client
{
	public static class Startup
	{
		public static void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment environment)
		{
			services.AddHttpClient("CrazyFramework.API", (sp, client) =>
				{
					client.BaseAddress = new Uri(environment.BaseAddress);
					client.EnableIntercept(sp);
				})
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CrazyFramework.API"));

			services.AddTransient<IProductService, ProductService>();

			services.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			services.AddSingleton<AppState>();

			services.AddLoadingBar();
			services.AddMatToaster(config =>
			{
				config.Position = MatToastPosition.BottomRight;
				config.PreventDuplicates = true;
				config.NewestOnTop = true;
				config.ShowCloseButton = true;
				config.MaximumOpacity = 95;
				config.VisibleStateDuration = 3000;
			});

			services.AddValidatorsFromAssemblyContaining<Program>();
		}
	}
}