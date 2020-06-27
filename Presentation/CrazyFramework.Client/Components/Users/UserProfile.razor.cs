using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Client.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Components.Users
{
	public partial class UserProfile
	{
		[Inject]
		public AuthenticationStateProvider authStateProvider { get; set; }

		public bool IsLoggedIn = false;
		private UserInfo userInfo = null;

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			userInfo = null;
			var user = (await authenticationStateTask).User;

			//TODO1 if (user.Identity.IsAuthenticated)
			//{
			//	userInfo = await ((IdentityAuthenticationStateProvider)authStateProvider).GetUserInfo();
			//}
		}
	}
}