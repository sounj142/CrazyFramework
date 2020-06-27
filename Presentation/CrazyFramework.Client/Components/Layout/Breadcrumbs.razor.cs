using System;
using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;

namespace CrazyFramework.Client.Components.Layout
{
	public partial class Breadcrumbs : IDisposable
	{
		[Inject]
		public AppState appState { get; set; }

		[Inject]
		public NavigationManager navigationManager { get; set; }

		[Inject]
		public AuthenticationStateProvider AuthStateProvider { get; set; }

		// This is just for demo and is VERY Hackish  for several reasons
		// Blazor does not have page implemented for client side... as far as I know so we split page names with underscore for now.
		// Path system is hack as well as the Url.. Maybe you have a better solution?

		public bool IsLoggedIn = false;

		[Parameter]
		public string RootLinkTitle { get; set; }

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		private string[] paths;
		private string baseUrl;

		protected override async Task OnParametersSetAsync()
		{
			IsLoggedIn = false;
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				IsLoggedIn = true;
			}
		}

		protected override async Task OnInitializedAsync()
		{
			baseUrl = navigationManager.BaseUri;
			await BuildBreadcrumbsAsync();
			navigationManager.LocationChanged += OnLocationChanges;
			await base.OnInitializedAsync();
		}

		private void OnLocationChanges(object sender, LocationChangedEventArgs e) => BuildBreadcrumbsAsync();

		private async Task BuildBreadcrumbsAsync()
		{
			string uri = navigationManager.Uri.Split('?')[0].Replace(baseUrl, string.Empty).Trim();

			if (IsLoggedIn)
			{
				//*TODO1 await appState.SaveLastVisitedUri(uri);
			}

			paths = String.IsNullOrEmpty(uri) ? new string[] { } : uri.Split('/');
			StateHasChanged();
		}

		public void Dispose()
		{
			navigationManager.LocationChanged -= OnLocationChanges;
		}
	}
}