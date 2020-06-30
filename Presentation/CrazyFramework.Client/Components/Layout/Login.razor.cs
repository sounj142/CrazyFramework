using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CrazyFramework.Client.Components.Layout
{
	public partial class Login
	{
		[Inject]
		public NavigationManager navigationManager { get; set; }

		[Inject]
		public AuthenticationStateProvider authStateProvider { get; set; }

		[Inject]
		public AppState appState { get; set; }

		[Inject]
		public SignOutSessionStateManager SignOutManager { get; set; }

		private async Task LogoutClick()
		{
			await SignOutManager.SetSignOutState();
			navigationManager.NavigateTo(Settings.LogoutPath);
		}
	}
}