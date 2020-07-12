using System;
using System.Threading.Tasks;
using CrazyFramework.BlazoriseClient.Models;
using CrazyFramework.BlazoriseClient.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.BlazoriseClient.Components.Common
{
	public partial class NavMenu : IDisposable
	{
		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		[Inject]
		public AppState appState { get; set; }

		protected UserInfo userInfo = null;
		protected bool uiElementsVisible = true;

		protected bool siderBarVisible;

		protected override async Task OnParametersSetAsync()
		{
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				userInfo = new UserInfo
				{
					UserName = user.Identity.Name
				};
			}
		}

		protected override void OnInitialized()
		{
			siderBarVisible = appState.GetSiderBarVisible();
			appState.SiderBarChanged += OnSiderBarChanged;
		}

		private void OnSiderBarChanged()
		{
			siderBarVisible = appState.GetSiderBarVisible();
			StateHasChanged();
		}

		public void Dispose()
		{
			appState.SiderBarChanged -= OnSiderBarChanged;
		}
	}
}