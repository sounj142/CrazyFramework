using System;
using System.Net.Http;
using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Providers;
using CrazyFramework.BlazoriseClient.Models;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CrazyFramework.BlazoriseClient.Shared;

namespace CrazyFramework.BlazoriseClient
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
			services.AddTransient<IJobTitleService, JobTitleService>();

			services.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			services.AddSingleton<AppState>();
			services.AddSingleton<NotificationService>();

			services.AddLoadingBar();

			services.AddValidatorsFromAssemblyContaining<Program>();

			services
				.AddBlazorise(options =>
				{
					options.ChangeTextOnKeyPress = true;
				})
				.AddBootstrapProviders()
				.AddFontAwesomeIcons();
		}

		public static void Configure(WebAssemblyHost host)
		{
			host.Services
				.UseBootstrapProviders()
				.UseFontAwesomeIcons();

			host.UseLoadingBar();
		}
	}
}