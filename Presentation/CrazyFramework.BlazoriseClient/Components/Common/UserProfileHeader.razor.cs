using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.BlazoriseClient.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CrazyFramework.BlazoriseClient.Components.Common
{
	public partial class UserProfileHeader
	{
		[Inject]
		public NavigationManager navigationManager { get; set; }

		[Inject]
		public SignOutSessionStateManager SignOutManager { get; set; }

		private UserInfo userInfo;

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			userInfo = null;
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				userInfo = new UserInfo
				{
					UserName = user.Identity.Name
				};
			}
		}

		private async Task LogoutClick()
		{
			await SignOutManager.SetSignOutState();
			navigationManager.NavigateTo(Settings.LogoutPath);
		}
	}
}