using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Components.Users
{
	public partial class Login
	{
		public NavigationManager navigationManager { get; set; }
		public AuthenticationStateProvider authStateProvider { get; set; }
		public AppState appState { get; set; }

		private async Task LogoutClick()
		{
			//TODO1
			//appState.ClearUserProfile();
			//await ((IdentityAuthenticationStateProvider)authStateProvider).Logout();

			//if (navigationManager.IsWebAssembly())
			//	navigationManager.NavigateTo(BlazorBoilerplate.Shared.Settings.LoginPath);
		}
	}
}