using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Client.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Components.Layout
{
	public partial class NavMenu
	{
		[Inject]
		public NavigationManager navigationManager { get; set; }

		public bool IsLoggedIn { get; set; } = false;

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			IsLoggedIn = false;
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				IsLoggedIn = true;
			}

			if (user.IsInRole(Role.Administrator))
			{
				// Perform some action only available to users in the 'admin' role
				//Console.WriteLine(DefaultRoleNames.Administrator);
			}
		}
	}
}